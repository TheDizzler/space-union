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

namespace SpaceMenus
{
    public class MultiplayerMenu
    {
        private Game1 game;

        public MultiplayerMenu(Game1 game)
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
            //Menu Title Label
            LabelControl menuTitleLabel = new LabelControl();
            menuTitleLabel.Text = "Multiplayer";
            menuTitleLabel.Bounds = GuiHelper.MENU_TITLE_LABEL;
            mainScreen.Desktop.Children.Add(menuTitleLabel);
            
            //Logout Button.
            ButtonControl logoutButton = GuiHelper.CreateButton("Back", -75, -400, 70, 32);
            logoutButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMainMenu();
            };
            mainScreen.Desktop.Children.Add(logoutButton);

            //Multiplayer Button.
            ButtonControl lobbyBrowserButton = GuiHelper.CreateButton("Lobby Browser", -435, -250, 200, 32);
            lobbyBrowserButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterLobbyBrowserMenu();
            };
            mainScreen.Desktop.Children.Add(lobbyBrowserButton);

            //Create Lobby Button.
            ButtonControl createLobbyButton = GuiHelper.CreateButton("Create Lobby", -435, -200, 200, 32);
            createLobbyButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterCreateLobbyMenu();
            };
            mainScreen.Desktop.Children.Add(createLobbyButton);
        }
    }
}