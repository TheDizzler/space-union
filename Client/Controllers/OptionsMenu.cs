﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

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
    /// <summary>
    /// Source: http://stackoverflow.com/questions/10039339/get-the-supported-screen-resolutions-in-xna
    /// Used to find all supported resolutions by the corresponding computer's graphic driver
    /// Used over the built in XNA for easier filtering of resolutions
    /// XNA will display all resolutions including for each refresh rate supported spooling out duplicate resolutions
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct DEVMODE
    {
        private const int CCHDEVICENAME = 0x20;
        private const int CCHFORMNAME = 0x20;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
        public string dmFormName;

        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    }

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
        private Point centerPoint;
        private bool toggleReso = false;
        private int winState = 0;

        public OptionsMenu(Game1 game)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            CreateMenuControls(game.mainScreen);
            clientHeight = game.getScreenHeight();
            clientWidth = game.getScreenWidth();
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
            LabelControl menuTitleLabel = new LabelControl();
            menuTitleLabel.Text = "Options";
            menuTitleLabel.Bounds = GuiHelper.MENU_TITLE_LABEL;
            mainScreen.Desktop.Children.Add(menuTitleLabel);

            //Logout Button.
            ButtonControl logoutButton = GuiHelper.CreateButton("Back", 175, 275, 70, 32);
            logoutButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMainMenu();
            };
            mainScreen.Desktop.Children.Add(logoutButton);

            //Apply Changes button.
            applyButton = GuiHelper.CreateButton("Apply", 250, 275, 70, 32);
            applyButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                applyChanges();
            };
            mainScreen.Desktop.Children.Add(applyButton);

            createResolutionDropDown(mainScreen);        
            createWindowState(mainScreen);
        }

        private void createWindowState(Screen mainScreen) {
            LabelControl winTitleLabel = GuiHelper.CreateLabel("Display Mode", -100, -65, 50, 25);
            mainScreen.Desktop.Children.Add(winTitleLabel);

            ButtonControl leftWinToggle = GuiHelper.CreateButton("<", 0, -65, 25, 25);
            leftWinToggle.Pressed += delegate(object sender, EventArgs arugments)
            {
                toggleWindowState(-1);
            };
            mainScreen.Desktop.Children.Add(leftWinToggle);

            currentWinLabel = GuiHelper.CreateLabel(game.windowState, 75, -65, 50, 25);
            mainScreen.Desktop.Children.Add(currentWinLabel);

            ButtonControl rightWinToggle = GuiHelper.CreateButton(">", 165, -65, 25, 25);
            rightWinToggle.Pressed += delegate(object sender, EventArgs arugments)
            {
                toggleWindowState(1);
            };
            mainScreen.Desktop.Children.Add(rightWinToggle);
        }

        /// <summary>
        /// Constructs the drop down menu for available resolutions
        /// </summary>
        /// <param name="mainScreen"></param>
        private void createResolutionDropDown(Screen mainScreen)
        {
            resoTitleLabel = GuiHelper.CreateLabel("Resolution", -100, -100, 50, 25);
            mainScreen.Desktop.Children.Add(resoTitleLabel);

            currentResoLabel = GuiHelper.CreateLabel(game.width + "x" + game.height, 25, -100, 50, 25);
            mainScreen.Desktop.Children.Add(currentResoLabel);

            //Toggle resolution list button.
            ButtonControl toggleResoButton = GuiHelper.CreateButton("V", 165, -100, 25, 25);
            toggleResoButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                if (toggleReso)
                {
                    resoList.Bounds = GuiHelper.CenterBound(-8000, -8000, 0, 0);
                }
                else
                {
                    resoList.Bounds = GuiHelper.CenterBound(85, -35, 175, 100);
                }
                toggleReso = !toggleReso;
            };
            mainScreen.Desktop.Children.Add(toggleResoButton);

            //Resolution list
            resoList = new ListControl();
            resoList.SelectionMode = ListSelectionMode.Single;

            foreach (String reso in GetScreenResolutions())
            {
                resoList.Items.Add(reso);
            }
            resoList.Bounds = GuiHelper.CenterBound(-8000, -8000, 0, 100);

            resoList.SelectionChanged += delegate(object sender, EventArgs arguments)
            {
                if (resoList.SelectedItems.Count != 0)
                {
                    currentResoLabel.Text = resoList.Items[resoList.SelectedItems[0]].ToString();
                    currentWinLabel.Text = "Windowed";
                    winState = 0;
                    toggleReso = false;
                }
                resoList.Bounds = new UniRectangle(-1000.0f, -1000.0f, 200.0f, 100.0f);
            };
            mainScreen.Desktop.Children.Add(resoList);
        }

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
                    currentResoLabel.Text = GetCurrentScreenResolution() + " (Borderless)";
                    break;
                case 2:
                    currentWinLabel.Text = "Fullscreen";
                    currentResoLabel.Text = GetCurrentScreenResolution() + " (Fullscreen)";
                    break;
            }
            
        }

        /// <summary>
        /// Apply the changes and updates the screen
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
            CreateMenuControls(game.mainScreen);
        }

        /// <summary>
        /// Source: http://stackoverflow.com/questions/10039339/get-the-supported-screen-resolutions-in-xna
        /// </summary>
        /// <param name="lpszDeviceName"></param>
        /// <param name="iModeNum"></param>
        /// <param name="lpDevMode"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        /// <summary>
        /// Source: http://stackoverflow.com/questions/10039339/get-the-supported-screen-resolutions-in-xna
        /// </summary>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        /// <summary>
        /// Source: http://stackoverflow.com/questions/10039339/get-the-supported-screen-resolutions-in-xna
        /// </summary>
        /// <returns></returns>
        public static List<string> GetScreenResolutions()
        {
            var resolutions = new List<string>();

            try
            {
                var devMode = new DEVMODE();
                int i = 0;

                while (EnumDisplaySettings(null, i, ref devMode))
                {
                    if (devMode.dmPelsWidth >= 800 && devMode.dmPelsHeight >= 600)
                    {
                        resolutions.Add(string.Format("{0}x{1}", devMode.dmPelsWidth, devMode.dmPelsHeight));
                    }
                    i++;
                }

                resolutions = resolutions.Distinct(StringComparer.InvariantCulture).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not get screen resolutions.");
            }

            return resolutions;
        }

        /// <summary>
        /// Source: http://stackoverflow.com/questions/10039339/get-the-supported-screen-resolutions-in-xna
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentScreenResolution()
        {
            int width = GetSystemMetrics(0x00);
            int height = GetSystemMetrics(0x01);

            return string.Format("{0}x{1}", width, height);
        }
    }
}