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
    public class LoginMenu
    {
        private Game1 game;

        public LoginMenu(Game1 game)
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
            //TODO: Change Label Colors to white
            //Account Name Label
            LabelControl accountNameLabel = new LabelControl();
            accountNameLabel.Text = "Account Name";
            accountNameLabel.Bounds = new UniRectangle(200.0f, 150.0f, 110.0f, 24.0f);
            mainScreen.Desktop.Children.Add(accountNameLabel);

            //Account Name Input
            InputControl accountNameInput = new InputControl();
            accountNameInput.Bounds = new UniRectangle(200.0f, 175.0f, 200.0f, 24.0f);
            accountNameInput.Text = "";
            mainScreen.Desktop.Children.Add(accountNameInput);

            //Password Label
            LabelControl passwordLabel = new LabelControl();
            passwordLabel.Text = "Password";
            passwordLabel.Bounds = new UniRectangle(200.0f, 200.0f, 110.0f, 24.0f);
            mainScreen.Desktop.Children.Add(passwordLabel);

            //TODO: Create Password field where characters show up as black circles
            //Password Input
            InputControl passwordInput = new InputControl();
            passwordInput.Bounds = new UniRectangle(200.0f, 225.0f, 200.0f, 24.0f);
            mainScreen.Desktop.Children.Add(passwordInput);

            //TODO: Change to a contrasting color (Yellow)
            //Login Button.
            ButtonControl loginButton = GuiHelper.CreateButton("Login", -415, -50, 150, 32);
            loginButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMainMenu();
            };
            mainScreen.Desktop.Children.Add(loginButton);

            //Debug Button.
            ButtonControl multiplayerButton = GuiHelper.CreateButton("Debug Game", -700, -400, 200, 50);
            multiplayerButton.Pressed += delegate(object sender, EventArgs arguments)
            {
            };
            mainScreen.Desktop.Children.Add(multiplayerButton);

            //Button to close game.
            ButtonControl quitButton = GuiHelper.CreateButton("Quit", -15, -15, 80, 32);
            quitButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.Exit();
            };
            mainScreen.Desktop.Children.Add(quitButton);
        }
    }
}