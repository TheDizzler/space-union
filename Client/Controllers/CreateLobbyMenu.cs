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
using SpaceUnionXNA;
using SpaceUnionXNA.Animations;

//NETWORKING
//using Data_Structures;

namespace SpaceUnionXNA.Controllers
{
    public class CreateLobbyMenu
    {
        private Game1 game;
        public String lobbyTitle;
        private ScrollingBackground scroll;
        private Rectangle WhiteBackground;
        private Texture2D Background;
        private InputControl lobbyTitleInput;
        private Texture2D TexBanner;
        private Rectangle Banner;

        /// <summary>
        /// @Author Troy, Edited by Steven
        /// </summary>
        /// <param name="game"></param>
        public CreateLobbyMenu(Game1 game)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            scroll = new ScrollingBackground(Game1.Assets.background) { height = game.getScreenHeight(), width = game.getScreenWidth() };
            scroll.setPosition(UIConstants.ORIGIN);
            WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - UIConstants.CREATE_WHITE_BG.X, (int)game.mainScreen.Height / 2 - UIConstants.CREATE_WHITE_BG.Y,
                UIConstants.CREATE_WHITE_BG.Width, UIConstants.CREATE_WHITE_BG.Height);

            TexBanner = Game1.Assets.suMultiCreate;
            Banner = new Rectangle((int)game.mainScreen.Width / 2 - UIConstants.SU_BANNER.X, (int)game.mainScreen.Height / 2 - UIConstants.SU_BANNER.Y,
                UIConstants.SU_BANNER.Width, UIConstants.SU_BANNER.Height);

            Background = Game1.Assets.guiRectangle;
            CreateMenuControls(game.mainScreen);
        }

        public void Update(GameTime gameTime)
        {
            scroll.update();
        }

        public void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            scroll.draw(spriteBatch);
            spriteBatch.Draw(TexBanner, Banner, Color.White);
            spriteBatch.Draw(Background, WhiteBackground, Color.White * 0.75f);
            spriteBatch.End();
            game.gui_manager.Draw(gameTime);
        }

        /// <summary>
        /// @Author Troy, Edited by Steven
        /// </summary>
        /// <param name="mainScreen"></param>
        private void CreateMenuControls(Screen mainScreen)
        {
            //Logout Button.
            ButtonControl backButton = GuiHelper.CreateButton("Back",
                UIConstants.CREATE_BACK_BTN.X, UIConstants.CREATE_BACK_BTN.Y,
                UIConstants.CREATE_BACK_BTN.Width, UIConstants.CREATE_BACK_BTN.Height);
            backButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMultiplayerMenu();
            };
            mainScreen.Desktop.Children.Add(backButton);

            //Lobby Title Text Entry.
            lobbyTitleInput = GuiHelper.CreateInput("",
                UIConstants.CREATE_LOBBY_INPUT.X, UIConstants.CREATE_LOBBY_INPUT.Y,
                UIConstants.CREATE_LOBBY_INPUT.Width, UIConstants.CREATE_LOBBY_INPUT.Height);
            mainScreen.Desktop.Children.Add(lobbyTitleInput);

            //Create Lobby Button.
            ButtonControl createLobbyButton = GuiHelper.CreateButton("Create Lobby",
                UIConstants.CREATE_LOBBY_BTN.X, UIConstants.CREATE_LOBBY_BTN.Y,
                UIConstants.CREATE_LOBBY_BTN.Width, UIConstants.CREATE_LOBBY_BTN.Height);
            createLobbyButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterLobbyMenu();
                //NETWORKING
                /*
                if(lobbyTitleInput.Text != null){
                    
                    Data lobbyData = game.Communication.sendRoomCreationRequest(game.Player, lobbyTitleInput.Text);
                    
                    if (lobbyData.Type == 10)
                    {
                        game.roomInfo = (RoomInfo)lobbyData;
                        game.EnterLobbyMenu();
                    }
                    else if (lobbyData.Type == 7)
                    {
                        Console.WriteLine("cannot create lobby");
                    }
                }
                 */
                game.EnterLobbyMenu();
            };
            mainScreen.Desktop.Children.Add(createLobbyButton);
        }

    }
}