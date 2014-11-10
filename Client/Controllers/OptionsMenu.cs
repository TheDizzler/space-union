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
    public class OptionsMenu
    {
        private Game1 game;

        public OptionsMenu(Game1 game)
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
            //Menu Title Label
            
            LabelControl currentSoundLabel = new LabelControl();
            currentSoundLabel.Bounds = new UniRectangle(550.0f, 100.0f, 110.0f, 24.0f);
            LabelControl currentMusicLabel = new LabelControl();
            currentMusicLabel.Bounds = new UniRectangle(550.0f, 200.0f, 110.0f, 24.0f);
            currentSoundLabel.Text = game.currentSound;

            currentMusicLabel.Text = game.currentMusic;
            LabelControl menuTitleLabel = new LabelControl();
            menuTitleLabel.Text = "Options";
            menuTitleLabel.Bounds = GuiHelper.MENU_TITLE_LABEL;
            mainScreen.Desktop.Children.Add(menuTitleLabel);

            LabelControl soundControlLabel = new LabelControl();
            soundControlLabel.Text = "Sound";
            soundControlLabel.Bounds = new UniRectangle(50.0f, 100.0f, 110.0f, 24.0f);
            mainScreen.Desktop.Children.Add(soundControlLabel);

            LabelControl musicControlLabel = new LabelControl();
            musicControlLabel.Text = "Music";
            musicControlLabel.Bounds = new UniRectangle(50.0f, 200.0f, 110.0f, 24.0f);
            mainScreen.Desktop.Children.Add(musicControlLabel);

            ButtonControl soundOffButton = GuiHelper.CreateButton("OFF", -600, -462, 70, 32);
            soundOffButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentSoundLabel.Text = "Off";
                game.currentSound = "Off";
               
            };
            mainScreen.Desktop.Children.Add(soundOffButton);

            ButtonControl soundLowButton = GuiHelper.CreateButton("LOW", -500, -462, 70, 32);
            soundLowButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentSoundLabel.Text = "Low";
                game.currentSound = "Low";
                
            };
            mainScreen.Desktop.Children.Add(soundLowButton);

            ButtonControl soundMediumButton = GuiHelper.CreateButton("MED", -400, -462, 70, 32);
            soundMediumButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentSoundLabel.Text = "Medium";
                game.currentSound = "Medium";
                
            };
            mainScreen.Desktop.Children.Add(soundMediumButton);

            ButtonControl soundHighButton = GuiHelper.CreateButton("HIGH", -300, -462, 70, 32);
            soundHighButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentSoundLabel.Text = "High";
                game.currentSound = "High";
                
            };
            mainScreen.Desktop.Children.Add(soundHighButton);


            ButtonControl musicOffButton = GuiHelper.CreateButton("OFF", -600, -365, 70, 32);
            musicOffButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentMusicLabel.Text = "Off";
                game.currentMusic = "Off";
                
            };
            mainScreen.Desktop.Children.Add(musicOffButton);

            ButtonControl musicLowButton = GuiHelper.CreateButton("LOW", -500, -365, 70, 32);
            musicLowButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentMusicLabel.Text = "Low";
                game.currentMusic = "Low";
                
            };
            mainScreen.Desktop.Children.Add(musicLowButton);

            ButtonControl musicMediumButton = GuiHelper.CreateButton("MED", -400, -365, 70, 32);
            musicMediumButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentMusicLabel.Text = "Medium";
                game.currentMusic = "Medium";
            };
            mainScreen.Desktop.Children.Add(musicMediumButton);

            ButtonControl musicHighButton = GuiHelper.CreateButton("HIGH", -300, -365, 70, 32);
            musicHighButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentMusicLabel.Text = "High";
                game.currentMusic = "High";
            };
            mainScreen.Desktop.Children.Add(musicHighButton);
            //Logout Button.
            ButtonControl logoutButton = GuiHelper.CreateButton("Back", -75, -400, 70, 32);
            logoutButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMainMenu();
            };
            mainScreen.Desktop.Children.Add(currentSoundLabel);
            mainScreen.Desktop.Children.Add(currentMusicLabel);
            mainScreen.Desktop.Children.Add(logoutButton);
        }
    }
}