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
    public class MainMenu
    {
        private Game1 game;
        private ScrollingBackground scroll;

        private Rectangle Banner;
        private Texture2D TexBanner;

        public MainMenu(Game1 game)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            scroll = new ScrollingBackground(Game1.Assets.background) { height = game.getScreenHeight(), width = game.getScreenWidth() };
            scroll.setPosition(UIConstants.ORIGIN);

            TexBanner = Game1.Assets.spaceUnion;
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

        /// <summary>
        /// @Author Troy, Revised by Steven
        /// </summary>
        /// <param name="mainScreen"></param>
        private void CreateMenuControls(Screen mainScreen)
        {
            //Logout Button.
            ButtonControl logoutButton = GuiHelper.CreateButton("Logout",
                UIConstants.MAIN_LOGOUT_BTN.X, UIConstants.MAIN_LOGOUT_BTN.Y,
                UIConstants.MAIN_LOGOUT_BTN.Width, UIConstants.MAIN_LOGOUT_BTN.Height);
            logoutButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterLoginMenu();
            };
            mainScreen.Desktop.Children.Add(logoutButton);

            //Multiplayer Button.
            ButtonControl multiplayerButton = GuiHelper.CreateButton("MULTIPLAYER",
                UIConstants.MAIN_MULTI_BTN.X, UIConstants.MAIN_MULTI_BTN.Y, 
                UIConstants.MAIN_MULTI_BTN.Width, UIConstants.MAIN_MULTI_BTN.Height);
            multiplayerButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMultiplayerMenu();
            };
            mainScreen.Desktop.Children.Add(multiplayerButton);

            //Options Button.
            ButtonControl optionsButton = GuiHelper.CreateButton("Options",
                UIConstants.MAIN_OPTION_BTN.X, UIConstants.MAIN_OPTION_BTN.Y, 
                UIConstants.MAIN_OPTION_BTN.Width, UIConstants.MAIN_OPTION_BTN.Height);
            optionsButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterOptionsMenu();
            };
            mainScreen.Desktop.Children.Add(optionsButton);

            //Credits Button.
            ButtonControl creditsButton = GuiHelper.CreateButton("Credits",
                UIConstants.MAIN_CREDIT_BTN.X, UIConstants.MAIN_CREDIT_BTN.Y,
                UIConstants.MAIN_CREDIT_BTN.Width, UIConstants.MAIN_CREDIT_BTN.Height);
            creditsButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterCreditsMenu();
            };
            mainScreen.Desktop.Children.Add(creditsButton);

            //Button to close game.
            ButtonControl quitButton = GuiHelper.CreateButton("Quit",
                UIConstants.MAIN_QUIT_BTN.X, UIConstants.MAIN_QUIT_BTN.Y,
                UIConstants.MAIN_QUIT_BTN.Width, UIConstants.MAIN_QUIT_BTN.Height);
            quitButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.Exit();
            };
            mainScreen.Desktop.Children.Add(quitButton);

            //Player Username Label
            LabelControl playerUsernameLabel = new LabelControl();
            //NETWORKING
            //playerUsernameLabel.Text = game.Player.Username.ToString();
            playerUsernameLabel.Text = "DEVELOPER";

            playerUsernameLabel.Bounds = GuiHelper.CenterBound(UIConstants.MAIN_PLAYER_LABEL.X, UIConstants.MAIN_PLAYER_LABEL.Y,
                UIConstants.MAIN_PLAYER_LABEL.Width, UIConstants.MAIN_PLAYER_LABEL.Height);
            mainScreen.Desktop.Children.Add(playerUsernameLabel);

        }
    }
}