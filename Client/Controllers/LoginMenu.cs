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
using System.Threading;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA;
using SpaceUnionXNA.Animations;

namespace SpaceUnionXNA.Controllers
{
    public class LoginMenu
    {
        private Game1 game;
        private PasswordInputControl passwordInput;
        private LabelControl passwordLabel;
        private LabelControl errorText;
        private ScrollingBackground scroll;
        private bool errors = false;
        InputControl accountNameInput;
        private KeyboardState keyState;
        private bool tabFlag = true;
        
        private Rectangle WhiteBackground;
        private Texture2D Background;

        public LoginMenu(Game1 game)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
       
            scroll = new ScrollingBackground(Game1.Assets.background) { height = game.getScreenHeight(), width = game.getScreenWidth() };
            scroll.setPosition(new Vector2((int)0, (int)0));
            Background = Game1.Assets.guiRectangle;
            
            CreateMenuControls(game.mainScreen);
        }

        public void Update(GameTime gameTime)
        {
            scroll.update();
        }

        public void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - 150, (int)game.mainScreen.Height / 2 - 150, 300, 225);
            
            scroll.draw(spriteBatch);
            spriteBatch.Draw(Background, WhiteBackground, Color.White * 0.75f);
            spriteBatch.End();
            game.gui_manager.Draw(gameTime);
            keyState = Keyboard.GetState();
            
            /* Switches between account input and password input */
            if (keyState.IsKeyDown(Keys.Tab))
            {
                if (passwordInput.HasFocus)
                {
                    if (tabFlag)
                        game.mainScreen.FocusedControl = accountNameInput;
                }
                else if (accountNameInput.HasFocus && accountNameInput.Text != "" && accountNameInput.Text[accountNameInput.Text.Length - 1].CompareTo((char)Keys.Tab) == 0)
                {
                    accountNameInput.Text = accountNameInput.Text.Substring(0, accountNameInput.Text.Length - 1);
                    if (tabFlag)
                        game.mainScreen.FocusedControl = passwordInput;
                }
                tabFlag = false;
            }

            /* Ensures that switching occurs only once per tab down */
            if (keyState.IsKeyUp(Keys.Tab)) 
            {
                tabFlag = true;
            }

            passwordLabel.Text = passwordInput.GetText();
            
        }
        private void CreateMenuControls(Screen mainScreen)
        {
            //TODO: Change Label Colors to white
            //Account Name Label

            LabelControl accountNameLabel = GuiHelper.CreateLabel("Account Name", -45, -125, 110, 24);
            mainScreen.Desktop.Children.Add(accountNameLabel);

            //Error Text Label
            errorText = new LabelControl();
            errorText.Bounds = new UniRectangle(275.0f, 300.0f, 110.0f, 25.0f);
            mainScreen.Desktop.Children.Add(errorText);

            //Account Name Input
            accountNameInput = GuiHelper.CreateInput("", 0, -90, 200, 24);
            mainScreen.Desktop.Children.Add(accountNameInput);

            //Password Label
            passwordLabel = GuiHelper.CreateLabel("Password", 0, -25, 110, 24);
            mainScreen.Desktop.Children.Add(passwordLabel);

            //TODO: Create Password field where characters show up as black circles
            //Password Input
            passwordInput = GuiHelper.CreatePasswordInput(0, -55, 200, 24);
            mainScreen.Desktop.Children.Add(passwordInput);

            //TODO: Change to a contrasting color (Yellow)
            //Login Button.
            ButtonControl loginButton = GuiHelper.CreateButton("Login", -24, 0, 150, 32);
            loginButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                errorText.Text = "";
                errors = false;
                if (accountNameInput.Text == "")
                {
                    errorText.Text += "Username field cannot be empty!\n";
                    errors = true;
                }
                if (passwordInput.Text == "")
                {
                    errorText.Text += "Password field cannot be empty!";
                    errors = true;
                }
                if (errors)
                {
                    return;
                }
                /*
                game.Player.Username = accountNameInput.Text;
                game.Player.Password = passwordInput.GetText();

                game.Communication.sendLoginRequest(game.Player);
                Thread.Sleep(2000);
                game.Player = game.Communication.getPlayer();
                Console.WriteLine(game.Player.Username);
                if (game.Player != null)*/
                game.EnterMainMenu();

            };
            mainScreen.Desktop.Children.Add(loginButton);

            //Debug Button.
            ButtonControl multiplayerButton = GuiHelper.CreateButton("Debug Game", -300, -300, 200, 50);
            multiplayerButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.StartGame();
            };
            mainScreen.Desktop.Children.Add(multiplayerButton);

            //Ship Select Button.
            ButtonControl shipSelectButton = GuiHelper.CreateButton("Select Ship", -300, -225, 200, 50);
            shipSelectButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterShipSelectionScreen();
            };
            mainScreen.Desktop.Children.Add(shipSelectButton);

            //Button to close game.
            ButtonControl quitButton = GuiHelper.CreateButton("Quit", 200, 100, 80, 32);
            quitButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.Exit();
            };
            mainScreen.Desktop.Children.Add(quitButton);
        }
    }
}