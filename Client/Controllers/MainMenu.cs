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
            ButtonControl logoutButton = GuiHelper.CreateButton("Logout", 165, -175, 70, 32);
            logoutButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterLoginMenu();
            };
            mainScreen.Desktop.Children.Add(logoutButton);

            //Multiplayer Button.
            ButtonControl multiplayerButton = GuiHelper.CreateButton("MULTIPLAYER", 0, -75, 200, 32);
            multiplayerButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMultiplayerMenu();
            };
            mainScreen.Desktop.Children.Add(multiplayerButton);

            //Options Button.
            ButtonControl optionsButton = GuiHelper.CreateButton("Options", 0, -25, 200, 32);
            optionsButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterOptionsMenu();
            };
            mainScreen.Desktop.Children.Add(optionsButton);

            //Credits Button.
            ButtonControl creditsButton = GuiHelper.CreateButton("Credits", 0, 25, 200, 32);
            creditsButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterCreditsMenu();
            };
            mainScreen.Desktop.Children.Add(creditsButton);

            //Button to close game.
            ButtonControl quitButton = GuiHelper.CreateButton("Quit", 0, 75, 200, 32);
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

            playerUsernameLabel.Bounds = GuiHelper.CenterBound(165, -225, 70, 32);
            mainScreen.Desktop.Children.Add(playerUsernameLabel);

        }
    }
}