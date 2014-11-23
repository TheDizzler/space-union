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
using SpaceUnionXNA;
using SpaceUnionXNA.Animations;

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
        private ScrollingBackground scroll;
        private Rectangle WhiteBackground;
        private Texture2D Background;
        bool isLeader = false;

        public LobbyMenu(Game1 game, String title)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            lobbyTitle = title;
            scroll = new ScrollingBackground(Game1.Assets.background) { height = game.getScreenHeight(), width = game.getScreenWidth() };
            scroll.setPosition(new Vector2((int)0, (int)0));
            CreateMenuControls(game.mainScreen);
            Background = Game1.Assets.guiRectangle;
            new Thread(updatePlayerList).Start();
        }

        public void Update(GameTime gameTime)
        {
            scroll.update();
        }

        public void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - 150, (int)game.mainScreen.Height / 2 - 150, 300, 225);

            //WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - 162, (int)game.mainScreen.Height / 2 - 162, 575, 325);
            WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - 325, (int)game.mainScreen.Height / 2 - 325, 650, 650);

            scroll.draw(spriteBatch);
            spriteBatch.Draw(Background, WhiteBackground, Color.White * 0.75f);
            //spriteBatch.Draw(Background, WhiteBackground, Color.White * 0.75f);
            //spriteBatch.Draw(Background, WhiteBackground, Color.White * 0.75f);
            spriteBatch.End();
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
            playersLabel = GuiHelper.CreateLabel("Players:", 150, -200, 120, 16);
            mainScreen.Desktop.Children.Add(playersLabel);

            player1Label = GuiHelper.CreateLabel("Player 1", 150, -180, 120, 16);
            mainScreen.Desktop.Children.Add(player1Label);

            player2Label = GuiHelper.CreateLabel("Player 2", 150, -160, 120, 16);
            mainScreen.Desktop.Children.Add(player2Label);

            player3Label = GuiHelper.CreateLabel("Player 3", 150, -140, 120, 16);
            mainScreen.Desktop.Children.Add(player3Label);

            player4Label = GuiHelper.CreateLabel("Player 4", 150, -120, 120, 16);
            mainScreen.Desktop.Children.Add(player4Label);

            player5Label = GuiHelper.CreateLabel("Player 5", 150, -100, 120, 16);
            mainScreen.Desktop.Children.Add(player5Label);
            
            player6Label = GuiHelper.CreateLabel("Player 6", 150, -80, 120, 16);
            mainScreen.Desktop.Children.Add(player6Label);

            //Choose Chip Label
            LabelControl chooseShipLabel = GuiHelper.CreateLabel("Choose Your Ship", -200, -200, 120, 16);
            mainScreen.Desktop.Children.Add(chooseShipLabel);

            //Choosing Ship Options
            ChoiceControl shipChoice_1 = GuiHelper.CreateChoice("Alpha Class", -200, -180, 120, 16);
            mainScreen.Desktop.Children.Add(shipChoice_1);

            ChoiceControl shipChoice_2 = GuiHelper.CreateChoice("Beta Class", -200, -160, 120, 16);
            mainScreen.Desktop.Children.Add(shipChoice_2);

            ChoiceControl shipChoice_3 = GuiHelper.CreateChoice("Charlie Class", -200, -140, 120, 16);
            mainScreen.Desktop.Children.Add(shipChoice_3);

            //Ready Up Button.
            OptionControl readyUpButton = GuiHelper.CreateOption("I'm Ready!", -100, 150, 50, 50);
            mainScreen.Desktop.Children.Add(readyUpButton);

            //Start Game Button
            ButtonControl startGameButton = GuiHelper.CreateButton("Start Game", -200, 150, 100, 60);
            startGameButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                if (readyUpButton.Selected == true 
                    && (shipChoice_1.Selected == true || shipChoice_2.Selected == true || shipChoice_3.Selected == true))
                {
                    menuNameLabel.Text = "You are ready";
                }
                else
                {
                    menuNameLabel.Text = "You are not ready";
                }
            };
            if (!isLeader)
            {
                startGameButton.Enabled = false; 
            }

            mainScreen.Desktop.Children.Add(startGameButton);

            //Cancel Button
            ButtonControl cancelGameButton = GuiHelper.CreateButton("Cancel", 200, 150, 100, 60);
            cancelGameButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                //NETWORKING
                //game.Communication.sendRoomExitRequest(game.Player, game.roomInfo.RoomNumber);
                game.EnterLobbyBrowserMenu();
            };
            mainScreen.Desktop.Children.Add(cancelGameButton);

        }

        private void updatePlayerList()
        {
            while (game.currentGameState == SpaceUnionXNA.Game1.GameState.Lobby)
            {
                //NETWORKING
                /*
                game.roomInfo = (RoomInfo)game.Communication.sendRoomInfoRequest(game.Player, game.roomInfo.RoomNumber);
                playersLabel.Text = "Players: ";
                foreach (KeyValuePair<string, GameData> lobbyPlayer in game.roomInfo.Players.ToArray())
                {
                    playersLabel.Text += lobbyPlayer.Key;
                }
                Thread.Sleep(3000);
                 */
            }
        }
    }
}