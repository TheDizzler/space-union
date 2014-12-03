using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceUnionXNA.Controllers
{
    /// <summary>
    /// Uses Rectangle to store the location and size of the nuclex controls
    /// @Author Steven
    /// </summary>
    class UIConstants
    {
        /* Scrolling background constants */
        public static Vector2 ORIGIN = new Vector2((int)0, (int)0);

        /* SpaceUnion Banners */
        public static Rectangle SU_BANNER = new Rectangle(400, 400, 800, 250);

        /* Login Menu UI Constants */
        /* UI Background */
        public static Rectangle LOGIN_WHITE_BG = new Rectangle(150, 150, 300, 225);
        public static Rectangle LOGIN_BANNER = new Rectangle(150, 300, 300, 100);

        /* Nuclex Controls */
        public static Rectangle LOGIN_ACCOUNT_LABEL = new Rectangle(-45, -125, 110, 24);
        public static Rectangle LOGIN_ACCOUNT_INPUT = new Rectangle(0, -90, 200, 24);
        public static Rectangle LOGIN_PASSWORD_LABEL = new Rectangle(0, -25, 110, 24);
        public static Rectangle LOGIN_PASSWORD_INPUT = new Rectangle(0, -55, 200, 24);
        public static Rectangle LOGIN_BUTTON = new Rectangle(-24, 0, 150, 32);
        public static Rectangle LOGIN_QUIT = new Rectangle(200, 100, 80, 32);

        /* Main Menu UI Constants */
        /* Nuclex Controls */
        public static Rectangle MAIN_LOGOUT_BTN = new Rectangle(165, -175, 70, 32);
        public static Rectangle MAIN_MULTI_BTN = new Rectangle(0, -75, 200, 32);
        public static Rectangle MAIN_OPTION_BTN = new Rectangle(0, -25, 200, 32);
        public static Rectangle MAIN_CREDIT_BTN = new Rectangle(0, 25, 200, 32);
        public static Rectangle MAIN_QUIT_BTN = new Rectangle(0, 75, 200, 32);
        public static Rectangle MAIN_PLAYER_LABEL = new Rectangle(165, -225, 70, 32);

        /* Multiplayer UI Constants */
        /* Nuclex Controls */
        public static Rectangle MULTI_LOGOUT_BTN = new Rectangle(0, 100, 70, 32);
        public static Rectangle MULTI_BROWSER_BTN = new Rectangle(0, 0, 200, 32);
        public static Rectangle MULTI_CREATE_BTN = new Rectangle(0, -100, 200, 32);

        /* Create Lobby UI Constants */
        /* UI Background */
        public static Rectangle CREATE_WHITE_BG = new Rectangle(175, 165, 350, 225);
        /* Nuclex Controls */
        public static Rectangle CREATE_BACK_BTN = new Rectangle(0, 0, 70, 32);
        public static Rectangle CREATE_LOBBY_INPUT = new Rectangle(-60, -95, 200, 30);
        public static Rectangle CREATE_LOBBY_BTN = new Rectangle(100, -95, 100, 32);

        /* Lobby Menu UI Constants */
        /* UI Background */
        public static Rectangle LOBBY_WHITE_BG = new Rectangle(325, 175, 650, 450);
        /* Nuclex Controls */
        public static Rectangle LOBBY_CHOICE_RADIO = new Rectangle(-207, -20, 16, 16);
        public const int LOBBY_CHOICE_RADIO_SPACE = 64;
        public static Rectangle LOBBY_PLAYER_LABEL = new Rectangle(150, -100, 120, 16);
        public const int LOBBY_PLAYER_LABEL_SPACE = 20;
        public static Rectangle LOBBY_READY_BTN = new Rectangle(-100, 150, 50, 50);
        public static Rectangle LOBBY_START_BTN = new Rectangle(-200, 150, 100, 60);
        public static Rectangle LOBBY_CANCEL_BTN = new Rectangle(200, 150, 100, 60);
        public static Rectangle LOBBY_SHIP_LABEL = new Rectangle(-200, -125, 120, 16);
        /* Ship grid location and size */
        public static Rectangle LOBBY_SHIP_GRID = new Rectangle(285, 40, 64, 64);
        
        /* Lobby Browser Menu UI Constants */
        /* Nuclex Controls */
        public static Rectangle BROWSE_BACK_BTN = new Rectangle(0, 260, 70, 32);
        public static Rectangle BROWSE_PREV_BTN = new Rectangle(-350, 0, 70, 32);
        public static Rectangle BROWSE_NEXT_BTN = new Rectangle(350, 0, 70, 32);
        public static Rectangle BROWSE_PAGE_LABEL = new Rectangle(0, 200, 30, 30);

        /* Option Menu UI Constants */
        /* UI Background */
        public static Rectangle OPTION_WHITE_BG = new Rectangle(287, 162, 575, 325);
        /* Nuclex Controls */
        public static Rectangle OPTION_LOGOUT_BTN = new Rectangle(175, 275, 70, 32);
        public static Rectangle OPTION_APPLY_BTN = new Rectangle(250, 275, 70, 32);
        public static Rectangle OPTION_KEYS_BTN = new Rectangle(-175, 100, 100, 25);
        /* Resoultions */
        public static Rectangle OPTION_WINDOW_LABEL = new Rectangle(-200, -65, 50, 25);
        public static Rectangle OPTION_LEFT_BTN = new Rectangle(0, -65, 25, 25);
        public static Rectangle OPTION_CURWIN_LABEL = new Rectangle(75, -65, 50, 25);
        public static Rectangle OPTION_RIGHT_BTN = new Rectangle(165, -65, 25, 25);
        /* Sound */
        public static Rectangle OPTION_SOUND_LABEL = new Rectangle(-200, 0, 50, 25);
        public static Rectangle OPTION_SOUND_OFF_BTN = new Rectangle(-115, 0, 70, 32);
        public static Rectangle OPTION_SOUND_LOW_BTN = new Rectangle(-35, 0, 70, 32);
        public static Rectangle OPTION_SOUND_MED_BTN = new Rectangle(45, 0, 70, 32);
        public static Rectangle OPTION_SOUND_HIGH_BTN = new Rectangle(125, 0, 70, 32);
        public static Rectangle OPTION_SOUND_CUR_LABEL = new Rectangle(205, 0, 50, 25);
        /* Music */
        public static Rectangle OPTION_MUSIC_LABEL = new Rectangle(-200, 50, 50, 25);
        public static Rectangle OPTION_MUSIC_OFF_BTN = new Rectangle(-115, 50, 70, 32);
        public static Rectangle OPTION_MUSIC_LOW_BTN = new Rectangle(-35, 50, 70, 32);
        public static Rectangle OPTION_MUSIC_MED_BTN = new Rectangle(45, 50, 70, 32);
        public static Rectangle OPTION_MUSIC_HIGH_BTN = new Rectangle(125, 50, 70, 32);
        public static Rectangle OPTION_MUSIC_CUR_LABEL = new Rectangle(205, 50, 50, 25);
        /* Resolution */
        public static Rectangle OPTION_RESO_LABEL = new Rectangle(-200, -100, 50, 25);
        public static Rectangle OPTION_RESO_CUR_LABEL = new Rectangle(25, -100, 50, 25);
        public static Rectangle OPTION_RESO_BTN = new Rectangle(165, -100, 25, 25);
        public static Rectangle OPTION_RESO_HIDE_LIST = new Rectangle(-8000, -8000, 0, 0);
        public static Rectangle OPTION_RESO_SHOW_LIST = new Rectangle(85, -35, 175, 100);

        /* Control Menu UI Constants */
        /* UI Background */
        public static Rectangle CONTROL_WHITE_BG = new Rectangle(287, 200, 575, 425);
        /* Nuclex Controls */
        public static Rectangle CONTROL_KEY_HIDE_LABEL = new Rectangle(4000, 4000, 200, 32);
        public static Rectangle CONTROL_KEY_LABEL = new Rectangle(-100, -150, 140, 32);
        public const int CONTROL_KEY_SPACE = 45;
        public static Rectangle CONTROL_KEY_BTN = new Rectangle(145, -150, 100, 32);
        public static Rectangle CONTROL_KEYTOENTER_BTN = new Rectangle(0, 110, 200, 32);
        public static Rectangle CONTROL_BACK_BTN = new Rectangle(165, 150, 70, 32);

        /* Credit Menu UI Constants */
        /* UI Background */
        public static Rectangle CREDIT_WHITE_BG = new Rectangle(325, 250, 650, 550);
        /* Nuclex Controls */
        public static Rectangle CREDIT_BACK_BTN = new Rectangle(0, 275, 70, 32);
    }
}
