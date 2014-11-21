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

namespace SpaceUnionXNA.Controllers
{
    public class ControlMenu
    {
        private Game1 game;
        /* Key order mapped to the Enum in Ship.cs */
        private ButtonControl keyButton0;
        private ButtonControl keyButton1;
        private ButtonControl keyButton2;
        private ButtonControl keyButton3;
        private ButtonControl keyButton4;

        private ButtonControl currentKeyChange;
        private InputControl keyToEnter;
        private ButtonControl backButton;
        
        private bool changingKey = false;
        private string oldKey;
        private bool eraseHelpText = true;
        private int keyToChange = 0;

        public ControlMenu(Game1 game)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            CreateMenuControls(game.mainScreen);
        }

        public void Update(GameTime gameTime)
        {
            /* Changes the input field to blank after user clicks on the field */
            if (keyToEnter.HasFocus && eraseHelpText)
            {
                keyToEnter.Text = "";
                eraseHelpText = false;
                changingKey = true;
            }

            /* Occurs after a key has been typed into the field and binds the new key */
            if (changingKey && keyToEnter.Text != "")
            {
                char c;
                c = char.ToUpper(keyToEnter.Text[0]);
                validateKey();
                currentKeyChange.Text = c.ToString();
                changingKey = false;
                game.keylist[keyToChange] = (Keys)((int)c);
                keyToEnter.Bounds = GuiHelper.CenterBound(4000, 4000, 200, 32);
            }
        }

        public void DrawMenu(GameTime gameTime)
        {
           
            game.gui_manager.Draw(gameTime);
        }


        private void CreateMenuControls(Screen mainScreen)
        {
            //Forward key
            LabelControl forwardLabel = GuiHelper.CreateLabel("Forward Thrust", -100, -175, 140, 32);
            mainScreen.Desktop.Children.Add(forwardLabel);
            keyButton0 = GuiHelper.CreateButton(game.keylist[0].ToString(), 145, -175, 70, 32);
            keyButton0.Pressed += delegate(object sender, EventArgs arguments)
            { 
                changeKey(0, keyButton0);       
            };
            mainScreen.Desktop.Children.Add(keyButton0);

            //Left key
            LabelControl leftLabel = GuiHelper.CreateLabel("Turn Left", -100, -130, 140, 32);
            mainScreen.Desktop.Children.Add(leftLabel);
            keyButton1 = GuiHelper.CreateButton(game.keylist[1].ToString(), 145, -130, 70, 32);
            keyButton1.Pressed += delegate(object sender, EventArgs arguments)
            {
                changeKey(1, keyButton1);
            };
            mainScreen.Desktop.Children.Add(keyButton1);

            //Right key
            LabelControl rightLabel = GuiHelper.CreateLabel("Turn Right", -100, -85, 140, 32);
            mainScreen.Desktop.Children.Add(rightLabel);
            keyButton2 = GuiHelper.CreateButton(game.keylist[2].ToString(), 145, -85, 70, 32);
            keyButton2.Pressed += delegate(object sender, EventArgs arguments)
            {
                changeKey(2, keyButton2);
            };
            mainScreen.Desktop.Children.Add(keyButton2);

            //Fire key
            LabelControl fireLabel = GuiHelper.CreateLabel("Primary Fire", -100, -40, 140, 32);
            mainScreen.Desktop.Children.Add(fireLabel);
            keyButton3 = GuiHelper.CreateButton(game.keylist[3].ToString(), 145, -40, 70, 32);
            keyButton3.Pressed += delegate(object sender, EventArgs arguments)
            {
                changeKey(3, keyButton3);
            };
            mainScreen.Desktop.Children.Add(keyButton3);

            //Alternate Fire key
            LabelControl altFireLabel = GuiHelper.CreateLabel("Secondary Fire", -100, 5, 140, 32);
            mainScreen.Desktop.Children.Add(altFireLabel);
            keyButton4 = GuiHelper.CreateButton(game.keylist[4].ToString(), 145, 5, 70, 32);
            keyButton4.Pressed += delegate(object sender, EventArgs arguments)
            {
                changeKey(4, keyButton4);
            };
            mainScreen.Desktop.Children.Add(keyButton4);

            // User input for key change
            keyToEnter = GuiHelper.CreateInput("", 4000, 4000, 200, 32);
            mainScreen.Desktop.Children.Add(keyToEnter);

            // Back button
            backButton = GuiHelper.CreateButton("Back", 165, 150, 70, 32);
            backButton.Pressed += delegate(object sender, EventArgs arugments)
            {
                game.EnterOptionsMenu();
            };
            mainScreen.Desktop.Children.Add(backButton);
        }

        private void applyChanges()
        {
            game.StartGame();
        }

        /// <summary>
        /// Sets the key index and sets the changing key
        /// </summary>
        /// <param name="keyIndex"></param>
        /// <param name="currentButton"></param>
        private void changeKey(int keyIndex, ButtonControl currentButton)
        {
            keyToEnter.Bounds = GuiHelper.CenterBound(0, 80, 200, 32);
            currentKeyChange = currentButton;
            keyToChange = keyIndex;
            oldKey = keyToEnter.Text;
            keyToEnter.Text = "Enter a new key";
        }
    }
}
