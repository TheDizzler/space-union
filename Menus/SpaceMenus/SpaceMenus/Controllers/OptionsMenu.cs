using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
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

namespace SpaceMenus
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
        private OptionControl fullscreenChoice;
        private bool toggleReso = false;

        public OptionsMenu(Game1 game)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            CreateMenuControls(game.mainScreen);
            clientHeight = game.getScreenSize().Height;
            clientWidth = game.getScreenSize().Width;
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
            ButtonControl logoutButton = GuiHelper.CreateButton("Back", -75, -400, 70, 32);
            logoutButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMainMenu();
            };
            mainScreen.Desktop.Children.Add(logoutButton);

            //Apply Changes button.
            applyButton = GuiHelper.CreateButton("Apply", -400, -400, 70, 32);
            applyButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                applyChanges();
            };
            mainScreen.Desktop.Children.Add(applyButton);

            //Fullscreen 
            fullscreenChoice = new OptionControl();
            fullscreenChoice.Bounds = new UniRectangle(230.0f, 34.0f, 50.0f, 24.0f);
            fullscreenChoice.Text = "Fullscreen";
            fullscreenChoice.Changed += delegate(object sender, EventArgs arguments)
            {
                applyFullscreen();
            };
            mainScreen.Desktop.Children.Add(fullscreenChoice);

            createResolutionDropDown(mainScreen);
        }

        /// <summary>
        /// Constructs the drop down menu for available resolutions
        /// </summary>
        /// <param name="mainScreen"></param>
        private void createResolutionDropDown(Screen mainScreen)
        {
            resoTitleLabel = new LabelControl();
            resoTitleLabel.Text = game.getScreenSize().Width + "x" + game.getScreenSize().Height;
            resoTitleLabel.Bounds = new UniRectangle(0.0f, 35.0f, 50.0f, 24.0f);
            mainScreen.Desktop.Children.Add(resoTitleLabel);

            //Toggle resolution list button.
            ButtonControl toggleResoButton = GuiHelper.CreateButton("V", -460, -350, 25, 25);
            toggleResoButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                if (toggleReso)
                {
                    resoList.Bounds = new UniRectangle(-1000.0f, -1000.0f, 0.0f, 0.0f);
                }
                else
                {
                    resoList.Bounds = new UniRectangle(0.0f, 70.0f, 200.0f, 100.0f);
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
            
            resoList.Bounds = new UniRectangle(-1000.0f, -1000.0f, 200.0f, 100.0f);
            resoList.SelectionChanged += delegate(object sender, EventArgs arguments)
            {
                if (resoList.SelectedItems.Count != 0)
                {
                    resoTitleLabel.Text = resoList.Items[resoList.SelectedItems[0]].ToString();
                    fullscreenChoice.Selected = false;
                    toggleReso = false;
                }
                resoList.Bounds = new UniRectangle(-1000.0f, -1000.0f, 200.0f, 100.0f);
            };
            mainScreen.Desktop.Children.Add(resoList);
        }

        private void applyFullscreen()
        {
            if (fullscreenChoice.Selected)
            {
                resoTitleLabel.Text = GetCurrentScreenResolution() + " (Fullscreen)";
            }
            else
            {
                resoTitleLabel.Text = game.getScreenSize().Width + "x" + game.getScreenSize().Height;
            }
        }

        private void applyChanges()
        {
            string width = "";
            string height = "";
            bool xFound = false;

            for (int i = 0; i < resoTitleLabel.Text.Length; i++)
            {
                if (Char.IsDigit(resoTitleLabel.Text[i]))
                {
                    if (xFound)
                    {
                        height += resoTitleLabel.Text[i];
                    }
                    else
                    {
                        width += resoTitleLabel.Text[i];
                    }
                }
                else
                {
                    xFound = true;
                }
            }

            game.setScreenSize(int.Parse(width), int.Parse(height), fullscreenChoice.Selected);
                
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