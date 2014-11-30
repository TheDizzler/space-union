using System;
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
    public class ControlMenu
    {
        private Game1 game;

        private Rectangle WhiteBackground;
        private Texture2D Background;
        /* Key order mapped to the Enum in Ship.cs */
        private ButtonControl keyButton0;
        private ButtonControl keyButton1;
        private ButtonControl keyButton2;
        private ButtonControl keyButton3;
        private ButtonControl keyButton4;

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
            CreateMenuControls(game.mainScreen);
            screen = game.mainScreen;
            
            Background = Game1.Assets.guiRectangle;
            
            nonInputKeys = new List<Keys>();
            nonInputKeys.Add(Keys.LeftControl);
            nonInputKeys.Add(Keys.LeftAlt);
            nonInputKeys.Add(Keys.LeftShift);
            nonInputKeys.Add(Keys.RightAlt);
            nonInputKeys.Add(Keys.RightControl);
            nonInputKeys.Add(Keys.RightShift);
        }

        public void Update(GameTime gameTime)
        {
			game.scroll.update();
        }

        public void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - 287, (int)game.mainScreen.Height / 2 - 212, 575, 425);
			game.scroll.draw(spriteBatch);
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

                    keyToEnter.Bounds = GuiHelper.CenterBound(4000, 4000, 200, 32);
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
            //Forward key
            LabelControl forwardLabel = GuiHelper.CreateLabel("Forward Thrust", -100, -175, 140, 32);
            mainScreen.Desktop.Children.Add(forwardLabel);
            keyButton0 = GuiHelper.CreateButton(game.keylist[0].ToString(), 145, -175, 100, 32);
            keyButton0.Pressed += delegate(object sender, EventArgs arguments)
            { 
                changeKey(0, keyButton0, forwardLabel);       
            };
            mainScreen.Desktop.Children.Add(keyButton0);

            //Left key
            LabelControl leftLabel = GuiHelper.CreateLabel("Turn Left", -100, -130, 140, 32);
            mainScreen.Desktop.Children.Add(leftLabel);
            keyButton1 = GuiHelper.CreateButton(game.keylist[1].ToString(), 145, -130, 100, 32);
            keyButton1.Pressed += delegate(object sender, EventArgs arguments)
            {
                changeKey(1, keyButton1, leftLabel);
            };
            mainScreen.Desktop.Children.Add(keyButton1);

            //Right key
            LabelControl rightLabel = GuiHelper.CreateLabel("Turn Right", -100, -85, 140, 32);
            mainScreen.Desktop.Children.Add(rightLabel);
            keyButton2 = GuiHelper.CreateButton(game.keylist[2].ToString(), 145, -85, 100, 32);
            keyButton2.Pressed += delegate(object sender, EventArgs arguments)
            {
                changeKey(2, keyButton2, rightLabel);
            };
            mainScreen.Desktop.Children.Add(keyButton2);

            //Fire key
            LabelControl fireLabel = GuiHelper.CreateLabel("Primary Fire", -100, -40, 140, 32);
            mainScreen.Desktop.Children.Add(fireLabel);
            keyButton3 = GuiHelper.CreateButton(game.keylist[3].ToString(), 145, -40, 100, 32);
            keyButton3.Pressed += delegate(object sender, EventArgs arguments)
            {
                changeKey(3, keyButton3, fireLabel);
            };
            mainScreen.Desktop.Children.Add(keyButton3);

            //Alternate Fire key
            LabelControl altFireLabel = GuiHelper.CreateLabel("Secondary Fire", -100, 5, 140, 32);
            mainScreen.Desktop.Children.Add(altFireLabel);
            keyButton4 = GuiHelper.CreateButton(game.keylist[4].ToString(), 145, 5, 100, 32);
            keyButton4.Pressed += delegate(object sender, EventArgs arguments)
            {
                changeKey(4, keyButton4, altFireLabel);
            };
            mainScreen.Desktop.Children.Add(keyButton4);

            // User input for key change
            keyToEnterLabel = GuiHelper.CreateLabel("", 0, 110, 200, 32);
            mainScreen.Desktop.Children.Add(keyToEnterLabel);

            // Used to determine if the user pressed a key
            keyToEnter = GuiHelper.CreateInput("", 4000, 4000, 200, 32);
            mainScreen.Desktop.Children.Add(keyToEnter);

            // Back button
            backButton = GuiHelper.CreateButton("Back", 165, 150, 70, 32);
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
