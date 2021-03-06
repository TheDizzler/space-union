﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    /// <summary>
    /// Allows the user to set their own key bindings, changes persist as long as the game is still running
    /// @Author Steven
    /// </summary>
	public class ControlMenu
	{
		private Game1 game;

		private Rectangle WhiteBackground;
		private Texture2D Background;
		private Texture2D TexBanner;
		private Rectangle Banner;

		/* Key order mapped to the Enum in Ship.cs */
		private List<ButtonControl> keyButtonList;
		private ButtonControl keyButton0;
		private ButtonControl keyButton1;
		private ButtonControl keyButton2;
		private ButtonControl keyButton3;
		private ButtonControl keyButton4;

		private List<LabelControl> keyLabelList;
		private LabelControl forwardLabel;
		private LabelControl leftLabel;
		private LabelControl rightLabel;
		private LabelControl fireLabel;
		private LabelControl altFireLabel;
		
		private ButtonControl currentKeyChange;
		private InputControl keyToEnter;
		private ButtonControl backButton;
		private LabelControl keyToEnterLabel;
		private KeyboardState keyState;

		private bool changingKey = false;
		private string oldKey;
		private int keyToChange = 0;
		private List<Keys> nonInputKeys;

		private Screen screen;

		public ControlMenu(Game1 game)
		{
			this.game = game;
			game.mainScreen.Desktop.Children.Clear(); //Clear the gui
			screen = game.mainScreen;
			
			TexBanner = Game1.Assets.suOptionKeys;
			Banner = new Rectangle((int)game.mainScreen.Width / 2 - UIConstants.SU_BANNER.X, (int)game.mainScreen.Height / 2 - UIConstants.SU_BANNER.Y,
				UIConstants.SU_BANNER.Width, UIConstants.SU_BANNER.Height);

			Background = Game1.Assets.guiRectangle;
			
			nonInputKeys = new List<Keys>();
			nonInputKeys.Add(Keys.LeftControl);
			nonInputKeys.Add(Keys.LeftAlt);
			nonInputKeys.Add(Keys.LeftShift);
			nonInputKeys.Add(Keys.RightAlt);
			nonInputKeys.Add(Keys.RightControl);
			nonInputKeys.Add(Keys.RightShift);

			keyLabelList = new List<LabelControl>();
			keyLabelList.Add(forwardLabel = GuiHelper.CreateLabel("Forward Thrust", 0, 0, UIConstants.CONTROL_KEY_LABEL.Width, UIConstants.CONTROL_KEY_LABEL.Height));
			keyLabelList.Add(leftLabel = GuiHelper.CreateLabel("Turn Left", 0, 0, UIConstants.CONTROL_KEY_LABEL.Width, UIConstants.CONTROL_KEY_LABEL.Height));
			keyLabelList.Add(rightLabel = GuiHelper.CreateLabel("Turn Right", 0, 0, UIConstants.CONTROL_KEY_LABEL.Width, UIConstants.CONTROL_KEY_LABEL.Height));
			keyLabelList.Add(fireLabel = GuiHelper.CreateLabel("Primary Fire", 0, 0, UIConstants.CONTROL_KEY_LABEL.Width, UIConstants.CONTROL_KEY_LABEL.Height));
			keyLabelList.Add(altFireLabel = GuiHelper.CreateLabel("Secondary Fire", 0, 0, UIConstants.CONTROL_KEY_LABEL.Width, UIConstants.CONTROL_KEY_LABEL.Height));

			keyButtonList = new List<ButtonControl>();
			keyButtonList.Add(keyButton0 = GuiHelper.CreateButton(game.keylist[0].ToString(), 0, 0, UIConstants.CONTROL_KEY_BTN.Width, UIConstants.CONTROL_KEY_BTN.Height));
			keyButtonList.Add(keyButton1 = GuiHelper.CreateButton(game.keylist[1].ToString(), 0, 0, UIConstants.CONTROL_KEY_BTN.Width, UIConstants.CONTROL_KEY_BTN.Height));
			keyButtonList.Add(keyButton2 = GuiHelper.CreateButton(game.keylist[2].ToString(), 0, 0, UIConstants.CONTROL_KEY_BTN.Width, UIConstants.CONTROL_KEY_BTN.Height));
			keyButtonList.Add(keyButton3 = GuiHelper.CreateButton(game.keylist[3].ToString(), 0, 0, UIConstants.CONTROL_KEY_BTN.Width, UIConstants.CONTROL_KEY_BTN.Height));
			keyButtonList.Add(keyButton4 = GuiHelper.CreateButton(game.keylist[4].ToString(), 0, 0, UIConstants.CONTROL_KEY_BTN.Width, UIConstants.CONTROL_KEY_BTN.Height));

			CreateMenuControls(game.mainScreen);
		}

		public void Update(GameTime gameTime)
		{
			game.scroll.update();
		}

		public void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - UIConstants.CONTROL_WHITE_BG.X, 
				(int)game.mainScreen.Height / 2 - UIConstants.CONTROL_WHITE_BG.Y,
				UIConstants.CONTROL_WHITE_BG.Width, UIConstants.CONTROL_WHITE_BG.Height);
			game.scroll.draw(spriteBatch);
			spriteBatch.Draw(TexBanner, Banner, Color.White);
			spriteBatch.Draw(Background, WhiteBackground, Color.White * 0.75f);
			spriteBatch.End();

			game.gui_manager.Draw(gameTime);
			keyState = Keyboard.GetState();

			/* Used to allow for modifier keys to be bound */
			if (changingKey)
			{
				foreach (Keys key in nonInputKeys)
				{
					if (keyState.IsKeyDown(key))
						keyToEnter.Text = "hi"; // Random text
				}
			}

			/* Occurs after a key has been typed into the field and binds the new key */
			if (changingKey && keyToEnter.Text != "")
			{
				if (validateKey(keyState.GetPressedKeys()))
				{
					currentKeyChange.Text = keyState.GetPressedKeys()[0].ToString();
					game.keylist[keyToChange] = keyState.GetPressedKeys()[0];

					keyToEnter.Bounds = GuiHelper.CenterBound(
						UIConstants.CONTROL_KEY_HIDE_LABEL.X, UIConstants.CONTROL_KEY_HIDE_LABEL.Y,
						UIConstants.CONTROL_KEY_HIDE_LABEL.Width, UIConstants.CONTROL_KEY_HIDE_LABEL.Height);
					keyToEnterLabel.Text = "";
					
					changingKey = false;
				}
				else
				{
					keyToEnterLabel.Text = keyState.GetPressedKeys()[0].ToString() + " Key already in use, enter a new key";
					keyToEnter.Text = "";
				}
			}
		}


		private void CreateMenuControls(Screen mainScreen)
		{
            /* Constructs the labels and key buttons UI */
			int i = 0;

			foreach (LabelControl lbl in keyLabelList)
			{
				lbl.Bounds = GuiHelper.CenterBound(UIConstants.CONTROL_KEY_LABEL.X, UIConstants.CONTROL_KEY_LABEL.Y + i * UIConstants.CONTROL_KEY_SPACE,
					UIConstants.CONTROL_KEY_LABEL.Width, UIConstants.CONTROL_KEY_LABEL.Height);
				mainScreen.Desktop.Children.Add(lbl);
				keyButtonList[i].Bounds = GuiHelper.CenterBound(UIConstants.CONTROL_KEY_BTN.X, UIConstants.CONTROL_KEY_BTN.Y + i * UIConstants.CONTROL_KEY_SPACE,
					UIConstants.CONTROL_KEY_BTN.Width, UIConstants.CONTROL_KEY_BTN.Height);
				i++;
			}
			keyButton0.Pressed += delegate(object sender, EventArgs arguments)
			{ 
				changeKey(0, keyButton0, forwardLabel);       
			};
			mainScreen.Desktop.Children.Add(keyButton0);

			keyButton1.Pressed += delegate(object sender, EventArgs arguments)
			{
				changeKey(1, keyButton1, leftLabel);
			};
			mainScreen.Desktop.Children.Add(keyButton1);

			keyButton2.Pressed += delegate(object sender, EventArgs arguments)
			{
				changeKey(2, keyButton2, rightLabel);
			};
			mainScreen.Desktop.Children.Add(keyButton2);

			keyButton3.Pressed += delegate(object sender, EventArgs arguments)
			{
				changeKey(3, keyButton3, fireLabel);
			};
			mainScreen.Desktop.Children.Add(keyButton3);

			keyButton4.Pressed += delegate(object sender, EventArgs arguments)
			{
				changeKey(4, keyButton4, altFireLabel);
			};
			mainScreen.Desktop.Children.Add(keyButton4);

			keyToEnterLabel = GuiHelper.CreateLabel("",
				UIConstants.CONTROL_KEYTOENTER_BTN.X, UIConstants.CONTROL_KEYTOENTER_BTN.Y,
				UIConstants.CONTROL_KEYTOENTER_BTN.Width, UIConstants.CONTROL_KEYTOENTER_BTN.Height);
			mainScreen.Desktop.Children.Add(keyToEnterLabel);

			// Used to determine if the user pressed a key
			keyToEnter = GuiHelper.CreateInput("", UIConstants.CONTROL_KEY_HIDE_LABEL.X, UIConstants.CONTROL_KEY_HIDE_LABEL.Y,
				UIConstants.CONTROL_KEY_HIDE_LABEL.Width, UIConstants.CONTROL_KEY_HIDE_LABEL.Height);
			mainScreen.Desktop.Children.Add(keyToEnter);

			// Back button
			backButton = GuiHelper.CreateButton("Back",
				UIConstants.CONTROL_BACK_BTN.X, UIConstants.CONTROL_BACK_BTN.Y,
				UIConstants.CONTROL_BACK_BTN.Width, UIConstants.CONTROL_BACK_BTN.Height);
			backButton.Pressed += delegate(object sender, EventArgs arugments)
			{
				game.EnterOptionsMenu();
			};
			mainScreen.Desktop.Children.Add(backButton);

			ButtonControl debug = GuiHelper.CreateButton("Debug", 165, 190, 70, 32);
			debug.Pressed += delegate(object sender, EventArgs arugments)
			{
				game.StartGame();
			};
			mainScreen.Desktop.Children.Add(debug);
		}

		/// <summary>
		/// Sets the key index and sets the changing key
        /// @Author Steven
		/// </summary>
		/// <param name="keyIndex"></param>
		/// <param name="currentButton"></param>
		private void changeKey(int keyIndex, ButtonControl currentButton, LabelControl currentLabel)
		{
			keyToChange = keyIndex;
			currentKeyChange = currentButton;
			oldKey = keyToEnter.Text;
			
			keyToEnter.Text = "";
			screen.FocusedControl = keyToEnter;
			
			keyToEnterLabel.Text = "Enter a new key for " + currentLabel.Text;
			changingKey = true;
		}

		/// <summary>
		/// Checks for any existing keys already in use
        /// @Author Steven
		/// </summary>
		/// <param name="keys"></param>
		/// <returns></returns>
		private bool validateKey(Keys[] keys)
		{
			foreach (Keys key in game.keylist)
			{
				if (key == keys[0])
					return false;
			}
			return true;
		}
	}
}
