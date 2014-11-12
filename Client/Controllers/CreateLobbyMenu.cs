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
using SpaceMenus;
//NETWORKING
//using Data_Structures;

namespace SpaceUnionXNA.Controllers
{
    public class CreateLobbyMenu
    {
        private Game1 game;
        public String lobbyTitle;
        private InputControl lobbyTitleInput;

        public CreateLobbyMenu(Game1 game)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            CreateMenuControls(game.mainScreen);
        }

        public void Update(GameTime gameTime)
        {

        }

        public void DrawMenu(GameTime gameTime)
        {
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

            //Menu Title Label
            LabelControl menuTitleLabel = new LabelControl();
            menuTitleLabel.Text = "Create A New Lobby";
            menuTitleLabel.Bounds = GuiHelper.MENU_TITLE_LABEL;
            mainScreen.Desktop.Children.Add(menuTitleLabel);

            //Lobby Title Label.
            LabelControl lobbyTitleLabel = new LabelControl();
            lobbyTitleLabel.Text = "Lobby Title";
            lobbyTitleLabel.Bounds = new UniRectangle(200.0f, 150.0f, 110.0f, 24.0f);
            mainScreen.Desktop.Children.Add(lobbyTitleLabel);

            //Lobby Title Text Entry.
            lobbyTitleInput = new InputControl();
            lobbyTitleInput.Bounds = new UniRectangle(200.0f, 175.0f, 200.0f, 24.0f);
            lobbyTitleInput.Text = "";
            mainScreen.Desktop.Children.Add(lobbyTitleInput);

            //Create Lobby Button.
            ButtonControl createLobbyButton = GuiHelper.CreateButton("Create Lobby", -435, -150, 200, 32);
            createLobbyButton.Pressed += delegate(object sender, EventArgs arguments)
            {
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