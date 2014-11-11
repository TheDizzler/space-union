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
//NETWORKING
//using Data_Structures;
using SpaceUnionXNA.Tools;


using SpaceUnionXNA.Gui;
using System.Threading;

namespace SpaceUnionXNA.Controllers
{
    public class LobbyBrowserMenu
    {
        private Game1 game;
        int rowRectOriginY = 125;
        int rowRectOriginX = 100;
        int rowRectSizeY = 15;
        Table LobbyBrowserTable;

        public LobbyBrowserMenu(Game1 game)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            CreateMenuControls(game.mainScreen);
            LobbyBrowserTable = new Table(6, new string[] { "Lobby Name", "Game Type", "Host Name", "Players", "Max. Players", "Ping" }, rowRectOriginX, rowRectOriginY, rowRectSizeY, 19, 100, true);
        }



        public void Update(GameTime gameTime)
        {
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
        }

        public void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            LobbyBrowserTable.draw(spriteBatch, game.mainScreen);
            spriteBatch.End();
            game.gui_manager.Draw(gameTime);
        }

        private void CreateMenuControls(Screen mainScreen)
        {
            //Logout Button.
            ButtonControl backButton = GuiHelper.CreateButton("Back", -75, -400, 70, 32);
            backButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMultiplayerMenu();
            };
            mainScreen.Desktop.Children.Add(backButton);

            ButtonControl nextPageButton = GuiHelper.CreateButton("Next", 0, (int)(-(mainScreen.Height) / 2), 70, 32);
            nextPageButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                LobbyBrowserTable.currentPage += 1;
            };

            ButtonControl prevPageButton = GuiHelper.CreateButton("Prev", (-850), (int)(-(mainScreen.Height) / 2), 70, 32);
            prevPageButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                LobbyBrowserTable.currentPage -= 1;
            };
            mainScreen.Desktop.Children.Add(nextPageButton);
            mainScreen.Desktop.Children.Add(prevPageButton);
            //Menu Title Label
            LabelControl menuTitleLabel = new LabelControl();
            menuTitleLabel.Text = "Lobby Browser";
            menuTitleLabel.Bounds = GuiHelper.MENU_TITLE_LABEL;
            mainScreen.Desktop.Children.Add(menuTitleLabel);
        }
    }
}