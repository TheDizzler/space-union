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
using SpaceMenus;

namespace SpaceUnionXNA.Controllers
{
    public class MainMenu
    {
        private Game1 game;

        public MainMenu(Game1 game)
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
            ButtonControl logoutButton = GuiHelper.CreateButton("Logout", -75, -400, 70, 32);
            logoutButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.Communication.sendLogoutRequest(game.Player);
                game.EnterLoginMenu();
                game.LoggedIn = false;
                Console.WriteLine("Logged out");
            };
            mainScreen.Desktop.Children.Add(logoutButton);

            //Multiplayer Button.
            ButtonControl multiplayerButton = GuiHelper.CreateButton("MULTIPLAYER", -435, -250, 200, 32);
            multiplayerButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMultiplayerMenu();
            };
            mainScreen.Desktop.Children.Add(multiplayerButton);

            //Options Button.
            ButtonControl optionsButton = GuiHelper.CreateButton("Options", -435, -200, 200, 32);
            optionsButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterOptionsMenu();
            };
            mainScreen.Desktop.Children.Add(optionsButton);

            //Credits Button.
            ButtonControl creditsButton = GuiHelper.CreateButton("Credits", -435, -150, 200, 32);
            creditsButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterCreditsMenu();
            };
            mainScreen.Desktop.Children.Add(creditsButton);

            //Button to close game.
            ButtonControl quitButton = GuiHelper.CreateButton("Quit", -435, -100, 200, 32);
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

            playerUsernameLabel.Bounds = new UniRectangle(500.0f, 50.0f, 110.0f, 24.0f);
            mainScreen.Desktop.Children.Add(playerUsernameLabel);

        }
    }
}