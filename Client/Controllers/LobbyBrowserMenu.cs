﻿using System;
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

//NETWORKING
//using Data_Structures;
using SpaceUnionXNA.Tools;


using SpaceUnionXNA.Gui;
using System.Threading;
using SpaceUnionXNA.Animations;

namespace SpaceUnionXNA.Controllers
{
    public class LobbyBrowserMenu
    {
        private Game1 game;
        
        static int rowRectSizeY = 15;
        static int rowsBeforeScroll = 19;
        static int columns = 6;
        LabelControl pagesLabel;
        static int columnWidth = 100;
        int totalWidth = columns * columnWidth;
        double totalHeight = (rowsBeforeScroll + 1) * rowRectSizeY;
        Table LobbyBrowserTable;
        
        private Rectangle WhiteBackground;
        ButtonControl prevPageButton;
        ButtonControl nextPageButton;
        private Texture2D TexBanner;
        private Rectangle Banner;

        public LobbyBrowserMenu(Game1 game)
        {
            this.game = game;
            int rowRectOriginY = (int)(game.mainScreen.Height/2 - totalHeight/2);
            int rowRectOriginX = (int)game.mainScreen.Width/2 - totalWidth/2;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            
            LobbyBrowserTable = new Table(columns, new string[] { "Lobby Name", "Game Type", "Host Name", "Players", "Max. Players", "Ping" }, rowRectOriginX, rowRectOriginY, rowRectSizeY, rowsBeforeScroll, columnWidth, true, game.mainScreen);

            TexBanner = Game1.Assets.suMultiBrowse;
            Banner = new Rectangle((int)game.mainScreen.Width / 2 - UIConstants.SU_BANNER.X, (int)game.mainScreen.Height / 2 - UIConstants.SU_BANNER.Y,
                UIConstants.SU_BANNER.Width, UIConstants.SU_BANNER.Height);

            CreateMenuControls(game.mainScreen);
        }



        public void Update(GameTime gameTime)
        {
			game.scroll.update();
            var newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.D1))
            {
                LobbyBrowserTable.CreateNewRow(new string[] { "CST", "Team Battle", "Konstantin", "3", "6", "100" });
                Thread.Sleep(200);
            }


            if (newState.IsKeyDown(Keys.D0))
            {
                LobbyBrowserTable.RemoveLastRow();
                Thread.Sleep(200);
            }

            if (newState.IsKeyDown(Keys.D9))
            {
                LobbyBrowserTable.Clear();
                Thread.Sleep(200);
            }

            if (LobbyBrowserTable.currentPage == 1)
            {
                prevPageButton.Enabled = false;
            }
            else
            {
                prevPageButton.Enabled = true;
            }
            if (LobbyBrowserTable.currentPage == LobbyBrowserTable.maxPage)
            {
                nextPageButton.Enabled = false;
            }
            else
            {
                nextPageButton.Enabled = true;
            }
            pagesLabel.Text = LobbyBrowserTable.currentPage + "/" + LobbyBrowserTable.maxPage;
        }

        public void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
			game.scroll.draw(spriteBatch);
            LobbyBrowserTable.draw(spriteBatch);
            spriteBatch.Draw(TexBanner, Banner, Color.White);
            spriteBatch.End();
            game.gui_manager.Draw(gameTime);
        }

        private void CreateMenuControls(Screen mainScreen)
        {
            //Logout Button.
            ButtonControl backButton = GuiHelper.CreateButton("Back",
                UIConstants.BROWSE_BACK_BTN.X, UIConstants.BROWSE_BACK_BTN.Y,
                UIConstants.BROWSE_BACK_BTN.Width, UIConstants.BROWSE_BACK_BTN.Height);
            backButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMultiplayerMenu();
            };
            mainScreen.Desktop.Children.Add(backButton);
            prevPageButton = GuiHelper.CreateButton("Prev",
                UIConstants.BROWSE_PREV_BTN.X, UIConstants.BROWSE_PREV_BTN.Y,
                UIConstants.BROWSE_PREV_BTN.Width, UIConstants.BROWSE_PREV_BTN.Height);
            prevPageButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                LobbyBrowserTable.PrevPage();
            };
           
            nextPageButton = GuiHelper.CreateButton("Next",
                UIConstants.BROWSE_NEXT_BTN.X, UIConstants.BROWSE_NEXT_BTN.Y,
                UIConstants.BROWSE_NEXT_BTN.Width, UIConstants.BROWSE_NEXT_BTN.Height);
            nextPageButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                LobbyBrowserTable.NextPage();
            };

            mainScreen.Desktop.Children.Add(nextPageButton);
            mainScreen.Desktop.Children.Add(prevPageButton);
            
            pagesLabel = GuiHelper.CreateLabel(LobbyBrowserTable.currentPage + "/" + LobbyBrowserTable.maxPage,
                UIConstants.BROWSE_PAGE_LABEL.X, UIConstants.BROWSE_PAGE_LABEL.Y,
                UIConstants.BROWSE_PAGE_LABEL.Width, UIConstants.BROWSE_PAGE_LABEL.Height);
            
            mainScreen.Desktop.Children.Add(pagesLabel);
        }
    }
}