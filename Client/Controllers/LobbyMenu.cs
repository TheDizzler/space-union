using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Nuclex.UserInterface.Controls.Desktop;
using Nuclex.Input;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;
//NETWORKING
//using Data_Structures;
using System.Threading;
using SpaceMenus;
using Data_Structures;

namespace SpaceUnionXNA.Controllers
{
    public class LobbyMenu
    {
        private Game1 game;
        public String lobbyTitle;
        LabelControl playersLabel;
        LabelControl player1Label;
        LabelControl player2Label;
        LabelControl player3Label;
        LabelControl player4Label;
        LabelControl player5Label;
        LabelControl player6Label;

        public LobbyMenu(Game1 game, String title)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            lobbyTitle = title;
            CreateMenuControls(game.mainScreen);
            game.Player.Ready = false;
            new Thread(updatePlayerList).Start();
        }

        public void Update(GameTime gameTime)
        {
            if (game.Communication.getGameStartSignal() != null)
            {
                game.GameStarted = true;
                Console.WriteLine("/////////////////////////////////// GAME STARTED /////////////////////////////////////////////////");
                game.StartGame();
            }
        }

        public void DrawMenu(GameTime gameTime)
        {
            game.gui_manager.Draw(gameTime);
        }

        private void CreateMenuControls(Screen mainScreen)
        {
            //Menu Name Label
            LabelControl menuNameLabel = new LabelControl();
            //NETWORKING
            //menuNameLabel.Text = "Lobby: " + game.roomInfo.RoomName;
            menuNameLabel.Text = "Lobby: ";

            menuNameLabel.Bounds = GuiHelper.MENU_TITLE_LABEL;
            mainScreen.Desktop.Children.Add(menuNameLabel);

            //Players Labels
            playersLabel = new LabelControl();
            playersLabel.Text = "Players:";
            playersLabel.Bounds = new UniRectangle(500.0f, 10.0f, 120.0f, 16.0f);
            mainScreen.Desktop.Children.Add(playersLabel);


            player1Label = new LabelControl();
            player1Label.Text = "Player 1";
            player1Label.Bounds = new UniRectangle(500.0f, 30.0f, 120.0f, 16.0f);
            mainScreen.Desktop.Children.Add(player1Label);

            player2Label = new LabelControl();
            player2Label.Text = "Player 2";
            player2Label.Bounds = new UniRectangle(500.0f, 45.0f, 120.0f, 16.0f);
            mainScreen.Desktop.Children.Add(player2Label);

            player3Label = new LabelControl();
            player3Label.Text = "Player 3";
            player3Label.Bounds = new UniRectangle(500.0f, 60.0f, 120.0f, 16.0f);
            mainScreen.Desktop.Children.Add(player3Label);

            player4Label = new LabelControl();
            player4Label.Text = "Player 4";
            player4Label.Bounds = new UniRectangle(500.0f, 75.0f, 120.0f, 16.0f);
            mainScreen.Desktop.Children.Add(player4Label);

            player5Label = new LabelControl();
            player5Label.Text = "Player 5";
            player5Label.Bounds = new UniRectangle(500.0f, 90.0f, 120.0f, 16.0f);
            mainScreen.Desktop.Children.Add(player5Label);

            player6Label = new LabelControl();
            player6Label.Text = "Player 6";
            player6Label.Bounds = new UniRectangle(500.0f, 105.0f, 120.0f, 16.0f);
            mainScreen.Desktop.Children.Add(player6Label);


            //Choose Chip Label
            LabelControl chooseShipLabel = new LabelControl();
            chooseShipLabel.Text = "Choose Your Ship";
            chooseShipLabel.Bounds = new UniRectangle(10.0f, 100.0f, 120.0f, 16.0f);
            mainScreen.Desktop.Children.Add(chooseShipLabel);

            //Choosing Ship Options
            ChoiceControl shipChoice_1 = new ChoiceControl();
            shipChoice_1.Bounds = new UniRectangle(10.0f, 125.0f, 120.0f, 16.0f);
            shipChoice_1.Text = "Alpha Class";
            shipChoice_1.Changed += delegate(object sender, EventArgs arguments)
            {
                game.Player.ShipChoice = 0;
                game.Communication.sendUpdateShipChoiceRequet(game.Player, game.roomInfo.RoomNumber);
            };
            mainScreen.Desktop.Children.Add(shipChoice_1);

            ChoiceControl shipChoice_2 = new ChoiceControl();
            shipChoice_2.Bounds = new UniRectangle(10.0f, 150.0f, 120.0f, 16.0f);
            shipChoice_2.Text = "Theta Class";
            shipChoice_2.Changed += delegate(object sender, EventArgs arguments)
            {
                game.Player.ShipChoice = 1;
                game.Communication.sendUpdateShipChoiceRequet(game.Player, game.roomInfo.RoomNumber);
            };
            mainScreen.Desktop.Children.Add(shipChoice_2);

            ChoiceControl shipChoice_3 = new ChoiceControl();
            shipChoice_3.Bounds = new UniRectangle(10.0f, 175.0f, 120.0f, 16.0f);
            shipChoice_3.Text = "Omega Class";
            shipChoice_3.Changed += delegate(object sender, EventArgs arguments)
            {
                game.Player.ShipChoice = 2;
                game.Communication.sendUpdateShipChoiceRequet(game.Player, game.roomInfo.RoomNumber);
            };
            mainScreen.Desktop.Children.Add(shipChoice_3);


            //Default Ship Choice
            shipChoice_1.Selected = true;
            game.Player.ShipChoice = 0;
            game.Communication.sendUpdateShipChoiceRequet(game.Player, game.roomInfo.RoomNumber);


            //Ready up Label
            LabelControl readyUpLabel = new LabelControl();
            readyUpLabel.Text = "I'm Ready!";
            readyUpLabel.Bounds = new UniRectangle(115.0f, 300.0f, 110.0f, 24.0f);
            mainScreen.Desktop.Children.Add(readyUpLabel);

            //Ready Up Button.
            OptionControl readyUpButton = new OptionControl();
            readyUpButton.Bounds = new UniRectangle(
                        new UniScalar(1.0f, -600.0f), new UniScalar(1.0f, -85.0f), 50, 50
            );
            readyUpButton.Changed += delegate(object sender, EventArgs arguments)
            {
                game.Player.Ready = readyUpButton.Selected;
                game.Communication.sendReadyStatusUpdateRequest(game.Player, game.roomInfo.RoomNumber);
                Console.WriteLine("Player is in the room: " + game.roomInfo.RoomNumber);
            };
            mainScreen.Desktop.Children.Add(readyUpButton);

            //Start Game Button
            ButtonControl startGameButton = GuiHelper.CreateButton("Start Game", -400, -75, 100, 60);
            startGameButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                RoomInfo roomInfo = (RoomInfo)game.Communication.sendRoomInfoRequest(game.Player, game.roomInfo.RoomNumber);
                if (roomInfo != null)
                    game.roomInfo = roomInfo;
                
                foreach (GameData player in game.roomInfo.Players)
                {
                    if (!player.Player.Ready)
                    {
                        return;
                    }
                }
                game.Communication.sendStartRequest(game.Player, game.roomInfo.RoomNumber);
            };

            if (game.Player.Username != game.roomInfo.Host)
            {
                startGameButton.Enabled = false;
            }
            mainScreen.Desktop.Children.Add(startGameButton);

            //Cancel Button
            ButtonControl cancelGameButton = GuiHelper.CreateButton("Cancel", -200, -75, 100, 60);
            cancelGameButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                //NETWORKING
                game.Communication.sendRoomExitRequest(game.Player, game.roomInfo.RoomNumber);
                game.EnterLobbyBrowserMenu();
            };
            mainScreen.Desktop.Children.Add(cancelGameButton);

        }

        private void updatePlayerList()
        {
            int roomNumber = game.roomInfo.RoomNumber;
            game.Player.GameRoom = roomNumber;

            while (game.currentGameState == SpaceUnionXNA.Game1.GameState.Lobby)
            {
                if (game.GameStarted)
                    break;

                Console.WriteLine("Updating room info...");
                //NETWORKING
                RoomInfo roomInfo = (RoomInfo)game.Communication.sendRoomInfoRequest(game.Player, roomNumber);
                if (roomInfo != null)
                {
                    game.roomInfo = roomInfo;
                    playersLabel.Text = "Players: ";
                    int counter = 1;
                    foreach (GameData lobbyPlayer in game.roomInfo.Players)
                    {
                        switch (counter)
                        {
                            case (1):
                                player1Label.Text = lobbyPlayer.Player.Username + " Ready: " + lobbyPlayer.Player.Ready + "Ship: " + lobbyPlayer.Player.ShipChoice;
                                break;
                            case (2):
                                player2Label.Text = lobbyPlayer.Player.Username + " Ready: " + lobbyPlayer.Player.Ready + "Ship: " + lobbyPlayer.Player.ShipChoice;
                                break;
                            case (3):
                                player3Label.Text = lobbyPlayer.Player.Username + " Ready: " + lobbyPlayer.Player.Ready + "Ship: " + lobbyPlayer.Player.ShipChoice;
                                break;
                            case (4):
                                player4Label.Text = lobbyPlayer.Player.Username + " Ready: " + lobbyPlayer.Player.Ready + "Ship: " + lobbyPlayer.Player.ShipChoice;
                                break;
                            case (5):
                                player5Label.Text = lobbyPlayer.Player.Username + " Ready: " + lobbyPlayer.Player.Ready + "Ship: " + lobbyPlayer.Player.ShipChoice;
                                break;
                            case (6):
                                player6Label.Text = lobbyPlayer.Player.Username + " Ready: " + lobbyPlayer.Player.Ready + "Ship: " + lobbyPlayer.Player.ShipChoice;
                                break;
                        }
                        counter++;
                    }
                    Thread.Sleep(500);
                }
            }
        }
    }
}