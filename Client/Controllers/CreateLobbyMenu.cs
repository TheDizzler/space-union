﻿using System;
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

        public CreateLobbyMenu(Game1 game)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            scroll = new ScrollingBackground(Game1.Assets.background) { height = game.getScreenHeight(), width = game.getScreenWidth() };
            scroll.setPosition(new Vector2((int)0, (int)0));

            TexBanner = Game1.Assets.suMultiCreate;
            Banner = new Rectangle((int)game.mainScreen.Width / 2 - 400, (int)game.mainScreen.Height / 2 - 150 - 250, 800, 250);

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

            WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - 175, (int)game.mainScreen.Height / 2 - 165, 300, 225);
            scroll.draw(spriteBatch);
            spriteBatch.Draw(TexBanner, Banner, Color.White);
            spriteBatch.Draw(Background, WhiteBackground, Color.White * 0.75f);
            spriteBatch.End();
            game.gui_manager.Draw(gameTime);
        }

        private void CreateMenuControls(Screen mainScreen)
        {
            //Logout Button.
            ButtonControl backButton = GuiHelper.CreateButton("Back", -75, -400, 70, 32);
            backButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMultiplayerMenu();
            };
            mainScreen.Desktop.Children.Add(backButton);

            //Lobby Title Label.
            LabelControl lobbyTitleLabel = GuiHelper.CreateLabel("Lobby Title", -145, -125, 30, 30);
            mainScreen.Desktop.Children.Add(lobbyTitleLabel);

            //Lobby Title Text Entry.
            lobbyTitleInput = GuiHelper.CreateInput("", -60, -95, 200, 30);
            mainScreen.Desktop.Children.Add(lobbyTitleInput);

            //Create Lobby Button.
            ButtonControl createLobbyButton = GuiHelper.CreateButton("Create Lobby", 0, 0, 200, 32);
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