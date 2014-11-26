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
            scroll.setPosition(new Vector2((int)0, (int)0));

            TexBanner = Game1.Assets.suMultiplayer;
            Banner = new Rectangle((int)game.mainScreen.Width / 2 - 400, (int)game.mainScreen.Height / 2 - 150 - 250, 800, 250);

            CreateMenuControls(game.mainScreen);
        }

        public void Update(GameTime gameTime)
        {
            scroll.update();
        }

        public void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - 150, (int)game.mainScreen.Height / 2 - 150, 300, 225);
            scroll.draw(spriteBatch);
            spriteBatch.Draw(TexBanner, Banner, Color.White);
            //spriteBatch.Draw(Background, WhiteBackground, Color.White * 0.75f);
            spriteBatch.End();
            game.gui_manager.Draw(gameTime);
        }

        private void CreateMenuControls(Screen mainScreen)
        {   
            //Logout Button.
            ButtonControl logoutButton = GuiHelper.CreateButton("Back", 0, 100, 70, 32);
            logoutButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMainMenu();
            };
            mainScreen.Desktop.Children.Add(logoutButton);

            //Multiplayer Button.
            ButtonControl lobbyBrowserButton = GuiHelper.CreateButton("Lobby Browser", 0, 0, 200, 32);
            lobbyBrowserButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterLobbyBrowserMenu();
            };
            mainScreen.Desktop.Children.Add(lobbyBrowserButton);

            //Create Lobby Button.
            ButtonControl createLobbyButton = GuiHelper.CreateButton("Create Lobby", 0, -100, 200, 32);
            createLobbyButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterCreateLobbyMenu();
            };
            mainScreen.Desktop.Children.Add(createLobbyButton);
        }
    }
}