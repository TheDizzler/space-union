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
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.Tools;

namespace SpaceUnionXNA.Controllers
{
    public class LobbyMenu
    {
        private Game1 game;
        public String lobbyTitle;
        private bool radioUpdate = false; 

        /* Ship selections */
        List<ChoiceControl> shipChoiceList;
        ChoiceControl shipChoice_1;
        ChoiceControl shipChoice_2;
        ChoiceControl shipChoice_3;
        List<ShipButton> shipSelectionList;
        const int WIDTH = 64; 
        const int HEIGHT = 64;
        int shipsPerRow;
        const int SHIPCOUNT = 3;
        ShipButton lastButton;

        /* Label controls for players */
        LabelControl playersLabel;
        List<LabelControl> playerLabelList;
        LabelControl player1Label;
        LabelControl player2Label;
        LabelControl player3Label;
        LabelControl player4Label;
        LabelControl player5Label;
        LabelControl player6Label;

        List<string> playerNameList;
       
        private Rectangle WhiteBackground;
        private Texture2D Background;
        private Texture2D TexBanner;
        private Rectangle Banner;
        
        bool isLeader = false;

        /// <summary>
        /// @Author Steven
        /// </summary>
        /// <param name="game"></param>
        /// <param name="title"></param>
        public LobbyMenu(Game1 game, String title)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            lobbyTitle = title;
            
            /* White background for UI */
            WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - UIConstants.LOBBY_WHITE_BG.X, (int)game.mainScreen.Height / 2 - UIConstants.LOBBY_WHITE_BG.Y,
                UIConstants.LOBBY_WHITE_BG.Width, UIConstants.LOBBY_WHITE_BG.Height);

            /* Scrolling background */

            /* Menu's Banner */
            TexBanner = Game1.Assets.suMultiLobby;
            Banner = new Rectangle((int)game.mainScreen.Width / 2 - UIConstants.SU_BANNER.X, (int)game.mainScreen.Height / 2 - UIConstants.SU_BANNER.Y,
                UIConstants.SU_BANNER.Width, UIConstants.SU_BANNER.Height);

            /* For constructing the list of players with LabelControls */
            playerLabelList = new List<LabelControl>();
            playerLabelList.Add(player1Label = GuiHelper.CreateLabel("Empty", 0, 0, UIConstants.LOBBY_PLAYER_LABEL.Width, UIConstants.LOBBY_PLAYER_LABEL.Height));
            playerLabelList.Add(player2Label = GuiHelper.CreateLabel("Empty", 0, 0, UIConstants.LOBBY_PLAYER_LABEL.Width, UIConstants.LOBBY_PLAYER_LABEL.Height));
            playerLabelList.Add(player3Label = GuiHelper.CreateLabel("Empty", 0, 0, UIConstants.LOBBY_PLAYER_LABEL.Width, UIConstants.LOBBY_PLAYER_LABEL.Height));
            playerLabelList.Add(player4Label = GuiHelper.CreateLabel("Empty", 0, 0, UIConstants.LOBBY_PLAYER_LABEL.Width, UIConstants.LOBBY_PLAYER_LABEL.Height));
            playerLabelList.Add(player5Label = GuiHelper.CreateLabel("Empty", 0, 0, UIConstants.LOBBY_PLAYER_LABEL.Width, UIConstants.LOBBY_PLAYER_LABEL.Height));
            playerLabelList.Add(player6Label = GuiHelper.CreateLabel("Empty", 0, 0, UIConstants.LOBBY_PLAYER_LABEL.Width, UIConstants.LOBBY_PLAYER_LABEL.Height));

            /* List of names used to correlate to the labels */
            playerNameList = new List<string>();
            playerNameList.Add("Player 1");
            playerNameList.Add("Player 2");
            playerNameList.Add("Player 3");
            playerNameList.Add("Player 4");
            playerNameList.Add("Player 5");
            playerNameList.Add("Player 6");

            /* List of Ships */
            shipSelectionList = new List<ShipButton>();
            shipSelectionList.Add(new ShipButton(new UFO(game)));
			shipSelectionList.Add(new ShipButton(new Scout(game)));
			shipSelectionList.Add(new ShipButton(new Zoid(game)));

            /* List of choices for ships */
            shipChoiceList = new List<ChoiceControl>();
            shipChoiceList.Add(shipChoice_1 = GuiHelper.CreateChoice("", 0, 0, UIConstants.LOBBY_CHOICE_RADIO.Width, UIConstants.LOBBY_CHOICE_RADIO.Height));
            shipChoiceList.Add(shipChoice_2 = GuiHelper.CreateChoice("", 0, 0, UIConstants.LOBBY_CHOICE_RADIO.Width, UIConstants.LOBBY_CHOICE_RADIO.Height));
            shipChoiceList.Add(shipChoice_3 = GuiHelper.CreateChoice("", 0, 0, UIConstants.LOBBY_CHOICE_RADIO.Width, UIConstants.LOBBY_CHOICE_RADIO.Height));

            /* Sets the default selected ship */
            lastButton = shipSelectionList[0];
            shipSelectionList[0].selected = true;
            shipChoiceList[0].Selected = true;

            setGridDisplay(game.getScreenWidth() - UIConstants.LOBBY_SHIP_GRID.X, game.getScreenHeight() - UIConstants.LOBBY_SHIP_GRID.Y);

            CreateMenuControls(game.mainScreen);
            Background = Game1.Assets.guiRectangle;
            new Thread(updatePlayerList).Start();
        }

        /// <summary>
        /// @Author Steven
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
			game.scroll.update();
            MouseState mouseState = Mouse.GetState();

            foreach (ShipButton btn in shipSelectionList)
            {
                btn.update(mouseState);
            }

            /* Allows for feedback of selecting a ship and sets the selected ship */
            for (int i = 0; i < SHIPCOUNT; i++)
            {
                for (int j = 0; j < SHIPCOUNT; j++)
                {
                    /* Due to how BaseButton works, at most 2 ships will be selected
                     * which results in this logic to compensate for that fact 
                     */
                    if (shipSelectionList[i].selected && shipSelectionList[j].selected && i != j)
                    {
                        if (lastButton == shipSelectionList[j])
                        {
                            //selectedShip = shipSelectionList[i].getShip();
                            selectship(i);
                            shipSelectionList[j].selected = false;
                            lastButton = shipSelectionList[i];
                        }
                        else
                        {
                            selectship(j);
                            //selectedShip = shipSelectionList[j].getShip();
                            shipSelectionList[i].selected = false;
                            lastButton = shipSelectionList[j];
                        }
                        //displaySelectedShip(selectedShip);
                    }
                    if (shipSelectionList[i].hover())
                    {
                        //displayHoverShip(shipSelectionList[i].getShip());
                    }
                }
            }
        }

        public void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
           
			game.scroll.draw(spriteBatch);
            spriteBatch.Draw(TexBanner, Banner, Color.White);
            spriteBatch.Draw(Background, WhiteBackground, Color.White * 0.75f);

            foreach (ShipButton btn in shipSelectionList)
            {
                btn.draw(spriteBatch);
            }
            spriteBatch.End();
            game.gui_manager.Draw(gameTime);
        }

        /// <summary>
        /// Sets the choice control to selected for the ship
        /// @Author Steven
        /// </summary>
        /// <param name="index"></param>
        private void selectship(int index)
        {
            foreach (ChoiceControl ctrl in shipChoiceList)
            {
                ctrl.Selected = false;
            }

            shipChoiceList[index].Selected = true;
        }

        /// <summary>
        /// Sets the ship icon clicked on to be selected
        /// @Author Steven
        /// </summary>
        /// <param name="index"></param>
        private void selectShipIcon(int index)
        {
            foreach (ShipButton btn in shipSelectionList)
            {
                if (btn.selected == true)
                {
                    lastButton = btn;
                }
                btn.selected = false;
            }

            shipSelectionList[index].selected = true;
        }

        /// <summary>
        /// Sets the ships in a grid fashion
        /// @Author Steven
        /// </summary>
        public void setGridDisplay(int screenWidth, int screenHeight)
        {
            shipsPerRow = (screenWidth - (WIDTH * 2)) / WIDTH;
            float currentShipsPerRow = shipsPerRow;
            int shipsPerLastRow = (int)(SHIPCOUNT % shipsPerRow);
            float shipsPerColumn = (float)Math.Ceiling((float)(SHIPCOUNT / shipsPerRow));

            /* Sets the ship's icon size and then its position on the screen based on how many ships there are */
            for (int i = 0; i < SHIPCOUNT; i++)
            {
                if (i == SHIPCOUNT - shipsPerLastRow)
                {
                    currentShipsPerRow = shipsPerLastRow;
                }
                shipSelectionList[i].height = HEIGHT;
                shipSelectionList[i].width = WIDTH;
                shipSelectionList[i].setPosition(
                    new Vector2(/* X Coordinate */
                               ((i % shipsPerRow * WIDTH)            // Sets each ship side by side from left to right starting from 0
                              + (screenWidth / 2)                    // Moves all ships towards the center
                              - (WIDTH * (currentShipsPerRow / 2))), // Moves all ships back by half the ships total width
                                /* Y Coordinate */
                                (i / shipsPerRow * HEIGHT)           // Sets each row from top to bottem starting from 0
                              + ((screenHeight / 2) - 75)           // Moves the rows towards the center
                              - (HEIGHT * (shipsPerColumn / 2))));   // Moves the rows back half the rows total height
            }
        }

        /// <summary>
        /// @Author Troy, Edited by Steven
        /// </summary>
        /// <param name="mainScreen"></param>
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
            playersLabel = GuiHelper.CreateLabel("Players:", 
                UIConstants.LOBBY_PLAYER_LABEL.X, UIConstants.LOBBY_PLAYER_LABEL.Y - UIConstants.LOBBY_PLAYER_LABEL_SPACE,
                UIConstants.LOBBY_PLAYER_LABEL.Width, UIConstants.LOBBY_PLAYER_LABEL.Height);
            mainScreen.Desktop.Children.Add(playersLabel);
            
            int i = 0;

            foreach (LabelControl ctrl in playerLabelList)
            {
                //ctrl.Text = playerNameList[i]; // Use if using the List<string> to store player names
                ctrl.Bounds = GuiHelper.CenterBound(
                    UIConstants.LOBBY_PLAYER_LABEL.X, UIConstants.LOBBY_PLAYER_LABEL.Y + i * UIConstants.LOBBY_PLAYER_LABEL_SPACE,
                    UIConstants.LOBBY_PLAYER_LABEL.Width, UIConstants.LOBBY_PLAYER_LABEL.Height);
                mainScreen.Desktop.Children.Add(ctrl);
                i++;
            }

            shipChoice_1.Changed += delegate(object sender, EventArgs arguments)
            {
                selectShipIcon(0);
            };
            shipChoice_2.Changed += delegate(object sender, EventArgs arguments)
            {
                selectShipIcon(1);
            }; 
            shipChoice_3.Changed += delegate(object sender, EventArgs arguments)
            {
                selectShipIcon(2);
            }; 

            createShipSelection(mainScreen);

            //Ready Up Button.
            OptionControl readyUpButton = GuiHelper.CreateOption("I'm Ready!", 
                UIConstants.LOBBY_READY_BTN.X, UIConstants.LOBBY_READY_BTN.Y,
                UIConstants.LOBBY_READY_BTN.Width, UIConstants.LOBBY_READY_BTN.Height);
            mainScreen.Desktop.Children.Add(readyUpButton);

            //Start Game Button
            ButtonControl startGameButton = GuiHelper.CreateButton("Start Game",
                UIConstants.LOBBY_START_BTN.X, UIConstants.LOBBY_START_BTN.Y,
                UIConstants.LOBBY_START_BTN.Width, UIConstants.LOBBY_START_BTN.Height);
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
            ButtonControl cancelGameButton = GuiHelper.CreateButton("Cancel",
                UIConstants.LOBBY_CANCEL_BTN.X, UIConstants.LOBBY_CANCEL_BTN.Y,
                UIConstants.LOBBY_CANCEL_BTN.Width, UIConstants.LOBBY_CANCEL_BTN.Height);
            cancelGameButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                //NETWORKING
                //game.Communication.sendRoomExitRequest(game.Player, game.roomInfo.RoomNumber);
                game.EnterLobbyBrowserMenu();
            };
            mainScreen.Desktop.Children.Add(cancelGameButton);

        }

        /// <summary>
        /// Builds the UI for the ships to be selected
        /// @Author Steven
        /// </summary>
        /// <param name="mainScreen"></param>
        private void createShipSelection(Screen mainScreen)
        {
            //Choose Ship Label
            LabelControl chooseShipLabel = GuiHelper.CreateLabel("Choose Your Ship",
                UIConstants.LOBBY_SHIP_LABEL.X, UIConstants.LOBBY_SHIP_LABEL.Y,
                UIConstants.LOBBY_SHIP_LABEL.Width, UIConstants.LOBBY_SHIP_LABEL.Height);
            mainScreen.Desktop.Children.Add(chooseShipLabel);

            int i = 0;

            foreach (ChoiceControl choice in shipChoiceList)
            {
                choice.Bounds = GuiHelper.CenterBound(UIConstants.LOBBY_CHOICE_RADIO.X + i * UIConstants.LOBBY_CHOICE_RADIO_SPACE,
                    UIConstants.LOBBY_CHOICE_RADIO.Y, UIConstants.LOBBY_CHOICE_RADIO.Width, UIConstants.LOBBY_CHOICE_RADIO.Height);
                mainScreen.Desktop.Children.Add(choice);
                i++;
            }
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