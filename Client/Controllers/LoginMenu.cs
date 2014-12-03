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
		
		private bool errors = false;
		InputControl accountNameInput;
		private KeyboardState keyState;
		private bool tabFlag = true;
		
		private Rectangle WhiteBackground;
		private Texture2D Background;

		private Rectangle Banner;
		private Texture2D TexBanner;
        private Texture2D Loading;
        private float RotationAngle;

        /* Testing */
        private Vector3 origin;
        private Vector3 screenpos;
        
        /// <summary>
        /// @Author Troy, Edited by Steven, Konstantin
        /// </summary>
        /// <param name="game"></param>
		public LoginMenu(Game1 game)
		{
			this.game = game;
			game.mainScreen.Desktop.Children.Clear(); //Clear the gui
			
			Background = Game1.Assets.guiRectangle;
			WhiteBackground = new Rectangle(
				(int)game.mainScreen.Width / 2 - UIConstants.LOGIN_WHITE_BG.X, 
				(int)game.mainScreen.Height / 2 - UIConstants.LOGIN_WHITE_BG.Y, 
				UIConstants.LOGIN_WHITE_BG.Width, UIConstants.LOGIN_WHITE_BG.Height);

			TexBanner = Game1.Assets.suBanner;
            Loading = Game1.Assets.loading;
			Banner = new Rectangle(
				(int)game.mainScreen.Width / 2 - UIConstants.LOGIN_BANNER.X, 
				(int)game.mainScreen.Height / 2 - UIConstants.LOGIN_BANNER.Y,
				UIConstants.LOGIN_BANNER.Width, UIConstants.LOGIN_BANNER.Height);

            origin.X = TexBanner.Width / 2;
            origin.Y = TexBanner.Height / 2;
            screenpos.X = game.getScreenWidth() / 2;
            screenpos.Y = game.getScreenHeight() / 2;


			CreateMenuControls(game.mainScreen);
		}

		public void Update(GameTime gameTime)
		{
			game.scroll.update();
            /* Sets the rotation speed of the banner/loading */
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            RotationAngle += elapsed;
            float circle = MathHelper.Pi * 2;
            RotationAngle = RotationAngle % circle;

		}

        /// <summary>
        /// @Author Troy, Edited by Steven
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
		public void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			game.scroll.draw(spriteBatch);
			spriteBatch.Draw(Background, WhiteBackground, Color.White * 0.75f);
            spriteBatch.End();

            /* Horizontal rotation banner 
             * Added by Steven */
            Matrix matrix = Matrix.CreateTranslation(-game.getScreenWidth() / 2, 0, 0) *
                            Matrix.CreateRotationY(RotationAngle) *
                            Matrix.CreateTranslation(game.getScreenWidth() / 2, 0, 0) *
                            Matrix.CreateScale(1, 1, 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, matrix);
			spriteBatch.Draw(TexBanner, Banner, Color.White);
            spriteBatch.End();
			
            // For the 2nd banner to show up
            matrix = Matrix.CreateTranslation(-game.getScreenWidth() / 2, 0, 0) *
                     Matrix.CreateRotationY(RotationAngle + (float)Math.PI) *
                     Matrix.CreateTranslation(game.getScreenWidth() / 2, 0, 0) *
                     Matrix.CreateScale(1, 1, 0);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, matrix);
            spriteBatch.Draw(Loading, Banner, Color.White);
			spriteBatch.End();


			game.gui_manager.Draw(gameTime);
			keyState = Keyboard.GetState();
			
            /* Switches between account input and password input 
             * Added by Steven */
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

            /* Ensures that switching occurs only once per tab down 
             * Added by Steven */
			if (keyState.IsKeyUp(Keys.Tab)) 
			{
				tabFlag = true;
			}

			passwordLabel.Text = passwordInput.GetText();
			
		}

        /// <summary>
        /// @Author Troy, Edited by Steven
        /// </summary>
        /// <param name="mainScreen"></param>
		private void CreateMenuControls(Screen mainScreen)
		{
			//TODO: Change Label Colors to white
			//Account Name Label

			LabelControl accountNameLabel = GuiHelper.CreateLabel("Account Name", 
				UIConstants.LOGIN_ACCOUNT_LABEL.X, UIConstants.LOGIN_ACCOUNT_LABEL.Y,
				UIConstants.LOGIN_ACCOUNT_LABEL.Width, UIConstants.LOGIN_ACCOUNT_LABEL.Height);
			mainScreen.Desktop.Children.Add(accountNameLabel);

			//Error Text Label
			errorText = new LabelControl();
			errorText.Bounds = new UniRectangle(275.0f, 300.0f, 110.0f, 25.0f);
			mainScreen.Desktop.Children.Add(errorText);

			//Account Name Input
			accountNameInput = GuiHelper.CreateInput("",
				UIConstants.LOGIN_ACCOUNT_INPUT.X, UIConstants.LOGIN_ACCOUNT_INPUT.Y,
				UIConstants.LOGIN_ACCOUNT_INPUT.Width, UIConstants.LOGIN_ACCOUNT_INPUT.Height);
			mainScreen.Desktop.Children.Add(accountNameInput);

			//Password Label
			passwordLabel = GuiHelper.CreateLabel("Password",
				UIConstants.LOGIN_PASSWORD_LABEL.X, UIConstants.LOGIN_PASSWORD_LABEL.Y,
				UIConstants.LOGIN_ACCOUNT_LABEL.Width, UIConstants.LOGIN_PASSWORD_LABEL.Height);
			mainScreen.Desktop.Children.Add(passwordLabel);

			//TODO: Create Password field where characters show up as black circles
			//Password Input
			passwordInput = GuiHelper.CreatePasswordInput(UIConstants.LOGIN_PASSWORD_INPUT.X, UIConstants.LOGIN_PASSWORD_INPUT.Y,
				UIConstants.LOGIN_PASSWORD_INPUT.Width, UIConstants.LOGIN_PASSWORD_INPUT.Height);
			mainScreen.Desktop.Children.Add(passwordInput);

			//TODO: Change to a contrasting color (Yellow)
			//Login Button.
			ButtonControl loginButton = GuiHelper.CreateButton("Login",
				UIConstants.LOGIN_BUTTON.X, UIConstants.LOGIN_BUTTON.Y,
				UIConstants.LOGIN_BUTTON.Width, UIConstants.LOGIN_BUTTON.Height);
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
			ButtonControl quitButton = GuiHelper.CreateButton("Quit",
				UIConstants.LOGIN_QUIT.X, UIConstants.LOGIN_QUIT.Y, 
				UIConstants.LOGIN_QUIT.Width, UIConstants.LOGIN_QUIT.Height);
			quitButton.Pressed += delegate(object sender, EventArgs arguments)
			{
				game.Exit();
			};
			mainScreen.Desktop.Children.Add(quitButton);
		}
	}
}