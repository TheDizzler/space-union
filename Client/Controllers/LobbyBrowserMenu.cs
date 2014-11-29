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

using Data_Structures;
using SpaceUnionXNA.Tools;


using SpaceUnionXNA.Gui;
using System.Threading;
using SpaceUnionXNA.Animations;

namespace SpaceUnionXNA.Controllers
{
    public class LobbyBrowserMenu
    {
        private Game1 game;

        //NETWORKING
        private RoomList roomList;

        static int rowRectSizeY = 15;
        static int rowsBeforeScroll = 19;
        static int columns = 6;
        LabelControl pagesLabel;
        static int columnWidth = 100;
        int totalWidth = columns * columnWidth;
        double totalHeight = (rowsBeforeScroll + 1) * rowRectSizeY;
        Table LobbyBrowserTable;
        private ScrollingBackground scroll;
        private Rectangle WhiteBackground;
        ButtonControl prevPageButton;
        ButtonControl nextPageButton;
        public LobbyBrowserMenu(Game1 game)
        {
            this.game = game;
            int rowRectOriginY = (int)(game.mainScreen.Height/2 - totalHeight/1.25);
            int rowRectOriginX = (int)game.mainScreen.Width/2 - totalWidth/2;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            scroll = new ScrollingBackground(Game1.Assets.background) { height = game.getScreenHeight(), width = game.getScreenWidth() };
            scroll.setPosition(new Vector2((int)0, (int)0));
            LobbyBrowserTable = new Table(columns, new string[] { "Lobby Name", "Game Type", "Host Name", "Players", "Max. Players", "Ping" }, rowRectOriginX, rowRectOriginY, rowRectSizeY, rowsBeforeScroll, columnWidth, true, game.mainScreen);
            CreateMenuControls(game.mainScreen);
            RefreshLobbyList();
        }

        /// <summary>
        /// Refreshes the lobby browser
        /// </summary>
        private void RefreshLobbyList()
        {
            //NETWORKING
            RoomList temproom = (RoomList)game.Communication.sendRoomListRequest(game.Player);
            if (temproom != null)
                roomList = temproom;
            CreateLobbyList();
        }

        public void CreateLobbyList()
        {
            int i = 0;
            LobbyBrowserTable.Clear();
            foreach (RoomInfo roomInfo in roomList.RoomInfoList)
            {
                i++;
                LobbyBrowserTable.CreateNewRow(new string[] { roomInfo.RoomName, "Team Battle", roomInfo.Host, roomInfo.Players.Length.ToString(), "6", "Ping..." },roomInfo.RoomNumber,game);
            }
        }

        public void Update(GameTime gameTime)
        {
            scroll.update();
            var newState = Keyboard.GetState();

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
            scroll.draw(spriteBatch);
            LobbyBrowserTable.draw(spriteBatch);
            
            spriteBatch.End();
            game.gui_manager.Draw(gameTime);
        }

        private void CreateMenuControls(Screen mainScreen)
        {
            //Back Button.
            ButtonControl backButton = GuiHelper.CreateButton("Back", -25, 175, 70, 32);
            backButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMultiplayerMenu();
            };
            mainScreen.Desktop.Children.Add(backButton);

            //Refresh Button.
            ButtonControl refreshButton = GuiHelper.CreateButton("Refresh", 50, 175, 70, 32);
            refreshButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                RefreshLobbyList();
            };
            mainScreen.Desktop.Children.Add(refreshButton);



            prevPageButton = GuiHelper.CreateButton("Prev", -375, -100, 70, 32);
            prevPageButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                LobbyBrowserTable.PrevPage();

               
            };
           
            nextPageButton = GuiHelper.CreateButton("Next", 375, -100, 70, 32);
            nextPageButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                LobbyBrowserTable.NextPage();
            };

            

            mainScreen.Desktop.Children.Add(nextPageButton);
            mainScreen.Desktop.Children.Add(prevPageButton);
            
            
            //Menu Title Label
            LabelControl menuTitleLabel = new LabelControl();
            menuTitleLabel.Text = "Lobby Browser";
            menuTitleLabel.Bounds = GuiHelper.MENU_TITLE_LABEL;
            mainScreen.Desktop.Children.Add(menuTitleLabel);

            
            pagesLabel = GuiHelper.CreateLabel(LobbyBrowserTable.currentPage + "/" + LobbyBrowserTable.maxPage, -25, 100, 30, 30);
            
            mainScreen.Desktop.Children.Add(pagesLabel);
        }
    }
}