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
using SpaceMenus;
//NETWORKING
using Data_Structures;

namespace SpaceUnionXNA.Controllers
{
    public class LobbyBrowserMenu
    {
        private Game1 game;
        //NETWORKING
        private RoomList roomList;
        LabelControl lobbyListLabel;
        Screen mainScreen;

        public LobbyBrowserMenu(Game1 game)
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

        private void CreateMenuControls(Screen screen)
        {
            mainScreen = screen;
            //Logout Button.
            ButtonControl backButton = GuiHelper.CreateButton("Back", -75, -400, 70, 32);
            backButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMultiplayerMenu();
            };
            mainScreen.Desktop.Children.Add(backButton);

            //Refresh Button.
            ButtonControl refreshButton = GuiHelper.CreateButton("Refresh", -75, -300, 70, 32);
            refreshButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                refreshLobbyList();
            };
            mainScreen.Desktop.Children.Add(refreshButton);

            //Menu Title Label
            LabelControl menuTitleLabel = new LabelControl();
            menuTitleLabel.Text = "Lobby Browser";
            menuTitleLabel.Bounds = GuiHelper.MENU_TITLE_LABEL;
            mainScreen.Desktop.Children.Add(menuTitleLabel);


            //Lobby List Label
            lobbyListLabel = new LabelControl();
            lobbyListLabel.Text = "Info";
            lobbyListLabel.Bounds = new UniRectangle(200.0f, 150.0f, 110.0f, 24.0f);
            mainScreen.Desktop.Children.Add(lobbyListLabel);
        }

        private void refreshLobbyList() 
        {
            //NETWORKING
            RoomList temproom = (RoomList)game.Communication.sendRoomListRequest(game.Player);
            if (temproom != null)
                roomList = temproom;
            createLobbyList();
        }

        private void createLobbyList() {
            lobbyListLabel.Text = "";
            
            int i = 0;
            int spacing = 50;
            foreach(RoomInfo roomInfo in roomList.RoomInfoList)
            {
                i++;
                ButtonControl joinLobbyButton = GuiHelper.CreateButton("Join "+ i, -75, -300+(spacing*i), 70, 32);
                joinLobbyButton.Pressed += delegate(object sender, EventArgs arguments)
                {
                    Data requestRoomData = game.Communication.sendRoomJoinRequest(game.Player, roomInfo.RoomNumber);
                    if (requestRoomData != null) {
                        if (requestRoomData.Type == 10)
                        {
                            game.roomInfo = (RoomInfo)requestRoomData;
                            Console.WriteLine(game.roomInfo.RoomNumber);
                            game.EnterLobbyMenu();
                        }
                        else
                        {
                            game.EnterLobbyBrowserMenu();
                        }
                    }
                    
                };
                mainScreen.Desktop.Children.Add(joinLobbyButton);
                lobbyListLabel.Text += ("LOBBY # " + roomInfo.RoomNumber + " HOST: " + roomInfo.Host.Username + "NAME" + roomInfo.RoomName); 
            }
             
        }
    }
}