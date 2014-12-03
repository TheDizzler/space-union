using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
using SpaceUnionXNA.Tools;

namespace SpaceUnionXNA.Controllers
{
    public class OptionsMenu
    {
        private Game1 game;
        private float clientWidth;
        private float clientHeight;
        private ListControl resoList;
        private LabelControl resoTitleLabel;
        private ButtonControl applyButton;
        private LabelControl currentResoLabel;
        private LabelControl currentWinLabel;
        private LabelControl currentMusicLabel;
        private LabelControl currentSoundLabel;
        private ScrollingBackground scroll;
        private Rectangle WhiteBackground;
        private Texture2D Background;
        private Texture2D TexBanner;
        private Rectangle Banner;
        
        private bool toggleReso = false;
        private int winState = 0;

        public OptionsMenu(Game1 game)
        {
            this.game = game;
            Background = Game1.Assets.guiRectangle;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            
            scroll = new ScrollingBackground(Game1.Assets.background) { height = game.getScreenHeight(), width = game.getScreenWidth() };
            scroll.setPosition(UIConstants.ORIGIN);
            TexBanner = Game1.Assets.suOption;
            Banner = new Rectangle((int)game.mainScreen.Width / 2 - UIConstants.SU_BANNER.X, (int)game.mainScreen.Height / 2 - UIConstants.SU_BANNER.Y,
                UIConstants.SU_BANNER.Width, UIConstants.SU_BANNER.Height);

            CreateMenuControls(game.mainScreen);
            clientHeight = game.getScreenHeight();
            clientWidth = game.getScreenWidth();
        }

        public void Update(GameTime gameTime)
        {
            scroll.update();
        }

        public void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - UIConstants.OPTION_WHITE_BG.X, 
                (int)game.mainScreen.Height / 2 - UIConstants.OPTION_WHITE_BG.Y,
                UIConstants.OPTION_WHITE_BG.Width, UIConstants.OPTION_WHITE_BG.Height);
            scroll.draw(spriteBatch);
            spriteBatch.Draw(TexBanner, Banner, Color.White);
            spriteBatch.Draw(Background, WhiteBackground, Color.White * 0.75f);
            spriteBatch.End();
            game.gui_manager.Draw(gameTime);
        }

        /// <summary>
        /// Creates the user interface
        /// @Written by Steven
        /// </summary>
        /// <param name="mainScreen"></param>
        private void CreateMenuControls(Screen mainScreen)
        {
            //Logout Button.
            ButtonControl logoutButton = GuiHelper.CreateButton("Back",
                UIConstants.OPTION_LOGOUT_BTN.X, UIConstants.OPTION_LOGOUT_BTN.Y,
                UIConstants.OPTION_LOGOUT_BTN.Width, UIConstants.OPTION_LOGOUT_BTN.Height);
            logoutButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMainMenu();
            };
            mainScreen.Desktop.Children.Add(logoutButton);

            //Apply Changes button.
            applyButton = GuiHelper.CreateButton("Apply",
                UIConstants.OPTION_APPLY_BTN.X, UIConstants.OPTION_APPLY_BTN.Y,
                UIConstants.OPTION_APPLY_BTN.Width, UIConstants.OPTION_APPLY_BTN.Height);
            applyButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                applyChanges();
            };
            mainScreen.Desktop.Children.Add(applyButton);

            createResolutionDropDown(mainScreen);        
            createWindowState(mainScreen);
            createSoundVolume(mainScreen);

            //Controls
            ButtonControl keyBindings = GuiHelper.CreateButton("Key Bindings",
                UIConstants.OPTION_KEYS_BTN.X, UIConstants.OPTION_KEYS_BTN.Y,
                UIConstants.OPTION_KEYS_BTN.Width, UIConstants.OPTION_KEYS_BTN.Height);
            keyBindings.Pressed += delegate(object sender, EventArgs arugments)
            {
                game.EnterControlMenu();
            };
            mainScreen.Desktop.Children.Add(keyBindings);
        }

        /// <summary>
        /// Constructs the interface of resolution selection
        /// @Author Steven
        /// </summary>
        /// <param name="mainScreen"></param>
        private void createWindowState(Screen mainScreen) {
            LabelControl winTitleLabel = GuiHelper.CreateLabel("Display Mode",
                UIConstants.OPTION_WINDOW_LABEL.X, UIConstants.OPTION_WINDOW_LABEL.Y,
                UIConstants.OPTION_WINDOW_LABEL.Width, UIConstants.OPTION_WINDOW_LABEL.Height);
            mainScreen.Desktop.Children.Add(winTitleLabel);

            ButtonControl leftWinToggle = GuiHelper.CreateButton("<",
                UIConstants.OPTION_LEFT_BTN.X, UIConstants.OPTION_LEFT_BTN.Y,
                UIConstants.OPTION_LEFT_BTN.Width, UIConstants.OPTION_LEFT_BTN.Height);
            leftWinToggle.Pressed += delegate(object sender, EventArgs arugments)
            {
                toggleWindowState(-1);
            };
            mainScreen.Desktop.Children.Add(leftWinToggle);

            currentWinLabel = GuiHelper.CreateLabel(game.windowState,
                UIConstants.OPTION_CURWIN_LABEL.X, UIConstants.OPTION_CURWIN_LABEL.Y,
                UIConstants.OPTION_CURWIN_LABEL.Width, UIConstants.OPTION_CURWIN_LABEL.Height);
            mainScreen.Desktop.Children.Add(currentWinLabel);

            ButtonControl rightWinToggle = GuiHelper.CreateButton(">",
                UIConstants.OPTION_RIGHT_BTN.X, UIConstants.OPTION_RIGHT_BTN.Y,
                UIConstants.OPTION_RIGHT_BTN.Width, UIConstants.OPTION_RIGHT_BTN.Height);
            rightWinToggle.Pressed += delegate(object sender, EventArgs arugments)
            {
                toggleWindowState(1);
            };
            mainScreen.Desktop.Children.Add(rightWinToggle);
        }

        /// <summary>
        /// Constructs the volume interface for both Music and Sound
        /// @Author Konstantin and Edited by Steven
        /// </summary>
        /// <param name="mainScreen"></param>
        private void createSoundVolume(Screen mainScreen)
        {
            /* Sound volume control */
            LabelControl soundControlLabel = GuiHelper.CreateLabel("Sound",
                UIConstants.OPTION_SOUND_LABEL.X, UIConstants.OPTION_SOUND_LABEL.Y,
                UIConstants.OPTION_SOUND_LABEL.Width, UIConstants.OPTION_SOUND_LABEL.Height);
            mainScreen.Desktop.Children.Add(soundControlLabel);

            ButtonControl soundOffButton = GuiHelper.CreateButton("OFF",
                UIConstants.OPTION_SOUND_OFF_BTN.X, UIConstants.OPTION_SOUND_OFF_BTN.Y,
                UIConstants.OPTION_SOUND_OFF_BTN.Width, UIConstants.OPTION_SOUND_OFF_BTN.Height);
            soundOffButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentSoundLabel.Text = "Off";
                game.currentSound = "Off";
            };
            mainScreen.Desktop.Children.Add(soundOffButton);

            ButtonControl soundLowButton = GuiHelper.CreateButton("LOW",
                UIConstants.OPTION_SOUND_LOW_BTN.X, UIConstants.OPTION_SOUND_LOW_BTN.Y,
                UIConstants.OPTION_SOUND_LOW_BTN.Width, UIConstants.OPTION_SOUND_LOW_BTN.Height);
            soundLowButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentSoundLabel.Text = "Low";
                game.currentSound = "Low";
            };
            mainScreen.Desktop.Children.Add(soundLowButton);

            ButtonControl soundMediumButton = GuiHelper.CreateButton("MED",
                UIConstants.OPTION_SOUND_MED_BTN.X, UIConstants.OPTION_SOUND_MED_BTN.Y,
                UIConstants.OPTION_SOUND_MED_BTN.Width, UIConstants.OPTION_SOUND_MED_BTN.Height);
            soundMediumButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentSoundLabel.Text = "Medium";
                game.currentSound = "Medium";
            };
            mainScreen.Desktop.Children.Add(soundMediumButton);

            ButtonControl soundHighButton = GuiHelper.CreateButton("HIGH",
                UIConstants.OPTION_SOUND_HIGH_BTN.X, UIConstants.OPTION_SOUND_HIGH_BTN.Y,
                UIConstants.OPTION_SOUND_HIGH_BTN.Width, UIConstants.OPTION_SOUND_HIGH_BTN.Height);
            soundHighButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentSoundLabel.Text = "High";
                game.currentSound = "High";
            };
            mainScreen.Desktop.Children.Add(soundHighButton);

            currentSoundLabel = GuiHelper.CreateLabel(game.currentSound,
                UIConstants.OPTION_SOUND_CUR_LABEL.X, UIConstants.OPTION_SOUND_CUR_LABEL.Y,
                UIConstants.OPTION_SOUND_CUR_LABEL.Width, UIConstants.OPTION_SOUND_CUR_LABEL.Height);
            mainScreen.Desktop.Children.Add(currentSoundLabel);

            /* Music volume control */
            LabelControl musicControlLabel = GuiHelper.CreateLabel("Music",
                UIConstants.OPTION_MUSIC_LABEL.X, UIConstants.OPTION_MUSIC_LABEL.Y,
                UIConstants.OPTION_MUSIC_LABEL.Width, UIConstants.OPTION_MUSIC_LABEL.Height);
            mainScreen.Desktop.Children.Add(musicControlLabel);

            ButtonControl musicOffButton = GuiHelper.CreateButton("OFF",
                UIConstants.OPTION_MUSIC_OFF_BTN.X, UIConstants.OPTION_MUSIC_OFF_BTN.Y,
                UIConstants.OPTION_MUSIC_OFF_BTN.Width, UIConstants.OPTION_MUSIC_OFF_BTN.Height);
            musicOffButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentMusicLabel.Text = "Off";
                game.currentMusic = "Off";
            };
            mainScreen.Desktop.Children.Add(musicOffButton);

            ButtonControl musicLowButton = GuiHelper.CreateButton("LOW",
                UIConstants.OPTION_MUSIC_LOW_BTN.X, UIConstants.OPTION_MUSIC_LOW_BTN.Y,
                UIConstants.OPTION_MUSIC_LOW_BTN.Width, UIConstants.OPTION_MUSIC_LOW_BTN.Height);
            musicLowButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentMusicLabel.Text = "Low";
                game.currentMusic = "Low";

            };
            mainScreen.Desktop.Children.Add(musicLowButton);

            ButtonControl musicMediumButton = GuiHelper.CreateButton("MED",
                UIConstants.OPTION_MUSIC_MED_BTN.X, UIConstants.OPTION_MUSIC_MED_BTN.Y,
                UIConstants.OPTION_MUSIC_MED_BTN.Width, UIConstants.OPTION_MUSIC_MED_BTN.Height);
            musicMediumButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentMusicLabel.Text = "Medium";
                game.currentMusic = "Medium";
            };
            mainScreen.Desktop.Children.Add(musicMediumButton);

            ButtonControl musicHighButton = GuiHelper.CreateButton("HIGH",
                UIConstants.OPTION_MUSIC_HIGH_BTN.X, UIConstants.OPTION_MUSIC_HIGH_BTN.Y,
                UIConstants.OPTION_MUSIC_HIGH_BTN.Width, UIConstants.OPTION_MUSIC_HIGH_BTN.Height);
            musicHighButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                currentMusicLabel.Text = "High";
                game.currentMusic = "High";
            };
            mainScreen.Desktop.Children.Add(musicHighButton);

            currentMusicLabel = GuiHelper.CreateLabel(game.currentMusic,
                UIConstants.OPTION_MUSIC_CUR_LABEL.X, UIConstants.OPTION_MUSIC_CUR_LABEL.Y,
                UIConstants.OPTION_MUSIC_CUR_LABEL.Width, UIConstants.OPTION_MUSIC_CUR_LABEL.Height);
            mainScreen.Desktop.Children.Add(currentMusicLabel);
        }

        /// <summary>
        /// Constructs the drop down menu for available resolutions
        /// @Author Steven
        /// </summary>
        /// <param name="mainScreen"></param>
        private void createResolutionDropDown(Screen mainScreen)
        {
            resoTitleLabel = GuiHelper.CreateLabel("Resolution",
                UIConstants.OPTION_RESO_LABEL.X, UIConstants.OPTION_RESO_LABEL.Y,
                UIConstants.OPTION_RESO_LABEL.Width, UIConstants.OPTION_RESO_LABEL.Height);
            mainScreen.Desktop.Children.Add(resoTitleLabel);

            currentResoLabel = GuiHelper.CreateLabel(game.width + "x" + game.height,
                UIConstants.OPTION_RESO_CUR_LABEL.X, UIConstants.OPTION_RESO_CUR_LABEL.Y,
                UIConstants.OPTION_RESO_CUR_LABEL.Width, UIConstants.OPTION_RESO_CUR_LABEL.Height);
            mainScreen.Desktop.Children.Add(currentResoLabel);

            //Toggle resolution list button.
            ButtonControl toggleResoButton = GuiHelper.CreateButton("V",
                UIConstants.OPTION_RESO_BTN.X, UIConstants.OPTION_RESO_BTN.Y,
                UIConstants.OPTION_RESO_BTN.Width, UIConstants.OPTION_RESO_BTN.Height);
            toggleResoButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                if (toggleReso)
                {
                    resoList.Bounds = GuiHelper.CenterBound(UIConstants.OPTION_RESO_HIDE_LIST.X, UIConstants.OPTION_RESO_HIDE_LIST.Y,
                        UIConstants.OPTION_RESO_HIDE_LIST.Width, UIConstants.OPTION_RESO_HIDE_LIST.Height);
                }
                else
                {
                    resoList.Bounds = GuiHelper.CenterBound(UIConstants.OPTION_RESO_SHOW_LIST.X, UIConstants.OPTION_RESO_SHOW_LIST.Y,
                        UIConstants.OPTION_RESO_SHOW_LIST.Width, UIConstants.OPTION_RESO_SHOW_LIST.Height);
                }
                toggleReso = !toggleReso;
            };
            mainScreen.Desktop.Children.Add(toggleResoButton);

            //Resolution list
            resoList = new ListControl();
            resoList.SelectionMode = ListSelectionMode.Single;

            foreach (String reso in Resolution.GetScreenResolutions())
            {
                resoList.Items.Add(reso);
            }
            resoList.Bounds = GuiHelper.CenterBound(UIConstants.OPTION_RESO_HIDE_LIST.X, UIConstants.OPTION_RESO_HIDE_LIST.Y,
                UIConstants.OPTION_RESO_HIDE_LIST.Width, UIConstants.OPTION_RESO_HIDE_LIST.Height);

            resoList.SelectionChanged += delegate(object sender, EventArgs arguments)
            {
                if (resoList.SelectedItems.Count != 0)
                {
                    currentResoLabel.Text = resoList.Items[resoList.SelectedItems[0]].ToString();
                    currentWinLabel.Text = "Windowed";
                    winState = 0;
                    toggleReso = false;
                }
                resoList.Bounds = GuiHelper.CenterBound(UIConstants.OPTION_RESO_HIDE_LIST.X, UIConstants.OPTION_RESO_HIDE_LIST.Y,
                    UIConstants.OPTION_RESO_HIDE_LIST.Width, UIConstants.OPTION_RESO_HIDE_LIST.Height);
            };
            mainScreen.Desktop.Children.Add(resoList);
        }

        /// <summary>
        /// Toggles between Windowed, Borderless and Fullscreen
        /// @Author Steven
        /// </summary>
        /// <param name="num"></param>
        private void toggleWindowState(int num)
        {
            winState += num;
            if (winState < 0)
                winState = 2;
            else if (winState > 2)
                winState = 0;
            switch (winState)
            {
                case 0:
                    currentWinLabel.Text = "Windowed";
                    currentResoLabel.Text = game.width + "x" + game.height;
                    break;
                case 1:
                    currentWinLabel.Text = "Borderless";
                    currentResoLabel.Text = Resolution.GetCurrentScreenResolution() + " (Borderless)";
                    break;
                case 2:
                    currentWinLabel.Text = "Fullscreen";
                    currentResoLabel.Text = Resolution.GetCurrentScreenResolution() + " (Fullscreen)";
                    break;
            }
            
        }

        /// <summary>
        /// Apply the changes and updates the screen
        /// @Author Steven and Edited by Konstantin
        /// </summary>
        private void applyChanges()
        {
            string width = "";
            string height = "";
            bool xFound = false;

            for (int i = 0; i < currentResoLabel.Text.Length; i++)
            {
                if (Char.IsDigit(currentResoLabel.Text[i]))
                {
                    if (xFound)
                    {
                        height += currentResoLabel.Text[i];
                    }
                    else
                    {
                        width += currentResoLabel.Text[i];
                    }
                }
                else
                {
                    xFound = true;
                }
            }

            game.setScreenSize(int.Parse(width), int.Parse(height), currentWinLabel.Text);
            scroll = new ScrollingBackground(Game1.Assets.background) { height = game.getScreenHeight(), width = game.getScreenWidth() };
            scroll.setPosition(UIConstants.ORIGIN);
            Banner = new Rectangle((int)game.mainScreen.Width / 2 - UIConstants.SU_BANNER.X, (int)game.mainScreen.Height / 2 - UIConstants.SU_BANNER.Y, 
                UIConstants.SU_BANNER.Width, UIConstants.SU_BANNER.Height);
            CreateMenuControls(game.mainScreen);
        }
    }
}