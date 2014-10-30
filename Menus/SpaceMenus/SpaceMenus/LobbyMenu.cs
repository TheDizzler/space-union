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
    public class LobbyMenu
    {
        private Game1 game;
        public String lobbyTitle;

        public LobbyMenu(Game1 game, String title)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            lobbyTitle = title;
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
            //Back Button.
            ButtonControl backButton = GuiHelper.CreateButton("Back", -75, -400, 70, 32);
            backButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterLobbyBrowserMenu();
            };
            mainScreen.Desktop.Children.Add(backButton);

            //Menu Name Label
            LabelControl menuNameLabel = new LabelControl();
            menuNameLabel.Text = "Lobby: " + lobbyTitle;
            menuNameLabel.Bounds = GuiHelper.MENU_TITLE_LABEL;
            mainScreen.Desktop.Children.Add(menuNameLabel);


        }
    }
}