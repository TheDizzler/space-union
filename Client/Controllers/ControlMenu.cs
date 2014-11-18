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
        private ButtonControl testButton;
        private InputControl testLabel;
        private ButtonControl applyChange;
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
            if (testLabel.HasFocus && eraseHelpText)
            {
                testLabel.Text = "";
                eraseHelpText = false;
                changingKey = true;
            }

            if (changingKey && testLabel.Text != "")
            {
                char c;
                c = char.ToUpper(testLabel.Text[0]);
                testButton.Text = c.ToString();
                changingKey = false;
                testLabel.Enabled = false;
                game.keylist[keyToChange] = (Keys)((int)c);
            }
        }

        public void DrawMenu(GameTime gameTime)
        {
           
            game.gui_manager.Draw(gameTime);
        }

        private void CreateMenuControls(Screen mainScreen)
        {
            //Logout Button.
            testButton = GuiHelper.CreateButton(game.keylist[0].ToString(), 165, -175, 70, 32);
            testButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                changeKey(0);       
            };
            mainScreen.Desktop.Children.Add(testButton);

            //test key
            testLabel = GuiHelper.CreateInput("", 165, 0, 70, 32);
            mainScreen.Desktop.Children.Add(testLabel);

            applyChange = GuiHelper.CreateButton("Apply", 165, 40, 70, 32);
            applyChange.Pressed += delegate(object sender, EventArgs arugments)
            {
                applyChanges();
            };
            mainScreen.Desktop.Children.Add(applyChange);
        }

        private void applyChanges()
        {
            game.StartGame();
        }

        private void changeKey(int keyIndex)
        {
            keyToChange = 0;
            oldKey = testLabel.Text;
            testLabel.Text = "Enter a new key";
        }
    }
}
