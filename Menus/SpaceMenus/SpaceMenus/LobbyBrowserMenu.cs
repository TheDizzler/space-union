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
    public class LobbyBrowserMenu
    {
        private Game1 game;

        public LobbyBrowserMenu(Game1 game)
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
            menuTitleLabel.Text = "Lobby Browser";
            menuTitleLabel.Bounds = GuiHelper.MENU_TITLE_LABEL;
            mainScreen.Desktop.Children.Add(menuTitleLabel);
        }
    }
}