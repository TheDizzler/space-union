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

namespace SpaceUnionXNA.Controllers
{
    public class MultiplayerMenu
    {
        private Game1 game;
        private ScrollingBackground scroll;
        private Texture2D TexBanner;
        private Rectangle Banner;

        public MultiplayerMenu(Game1 game)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            scroll = new ScrollingBackground(Game1.Assets.background) { height = game.getScreenHeight(), width = game.getScreenWidth() };
            scroll.setPosition(UIConstants.ORIGIN);

            TexBanner = Game1.Assets.suMultiplayer;
            Banner = new Rectangle((int)game.mainScreen.Width / 2 - UIConstants.SU_BANNER.X, (int)game.mainScreen.Height / 2 - UIConstants.SU_BANNER.Y,
                UIConstants.SU_BANNER.Width, UIConstants.SU_BANNER.Height);

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
            spriteBatch.End();
            game.gui_manager.Draw(gameTime);
        }

        private void CreateMenuControls(Screen mainScreen)
        {   
            //Logout Button.
            ButtonControl logoutButton = GuiHelper.CreateButton("Back",
                UIConstants.MULTI_LOGOUT_BTN.X, UIConstants.MULTI_LOGOUT_BTN.Y,
                UIConstants.MULTI_LOGOUT_BTN.Width, UIConstants.MULTI_LOGOUT_BTN.Height);
            logoutButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMainMenu();
            };
            mainScreen.Desktop.Children.Add(logoutButton);

            //Multiplayer Button.
            ButtonControl lobbyBrowserButton = GuiHelper.CreateButton("Lobby Browser",
                UIConstants.MULTI_BROWSER_BTN.X, UIConstants.MULTI_BROWSER_BTN.Y,
                UIConstants.MULTI_BROWSER_BTN.Width, UIConstants.MULTI_BROWSER_BTN.Height);
            lobbyBrowserButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterLobbyBrowserMenu();
            };
            mainScreen.Desktop.Children.Add(lobbyBrowserButton);

            //Create Lobby Button.
            ButtonControl createLobbyButton = GuiHelper.CreateButton("Create Lobby",
                UIConstants.MULTI_CREATE_BTN.X, UIConstants.MULTI_CREATE_BTN.Y,
                UIConstants.MULTI_CREATE_BTN.Width, UIConstants.MULTI_CREATE_BTN.Height);
            createLobbyButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterCreateLobbyMenu();
            };
            mainScreen.Desktop.Children.Add(createLobbyButton);
        }
    }
}