using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnion.Ships;
using SpaceUnion.Tools;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using SpaceUnion.Controllers;
using System.Threading;
///Created by Matthew Baldock
namespace SpaceUnion.Controllers
{
    class LobbyBrowser
    {
        static int MAX_AMOUNT_SERVERS = 1024;
        BaseButton nextlayerButton;
        CustomGUI banner;
        BaseButton lastlayerButton;
        Game1 game;

        int rowRectOriginY = 125;
        int rowRectOriginX = 100;
        int rowRectSizeY = 15;
        int rowRectSizeX = 500;

        Table LobbyBrowserTable;
        /// <summary>
        /// Creates Buttons to other GameStates
        /// </summary>
        /// <param name="game"></param>
        public LobbyBrowser(Game1 game)
        {

            this.game = game;
            nextlayerButton = new BaseButton(Game1.Assets.gamelobby) { height = 100, width = 300 };
            banner = new CustomGUI(game, "Lobby browser");
            lastlayerButton = new BaseButton(Game1.Assets.lobbyoptions) { height = 100, width = 300 };
            lastlayerButton.setPosition(new Vector2(0, game.getScreenHeight() - 300));
            nextlayerButton.setPosition(new Vector2(0, game.getScreenHeight() - 100));

            LobbyBrowserTable = new Table(6, new string[6] { "Lobby Name", "Game Type", "Host Name", "Players", "Max. Players", "Ping" }, rowRectOriginX, rowRectOriginY, rowRectSizeY, 17, 100);
            
          
        }
        /// <summary>
        /// Update method, checks what button is clicked and performs proper action
        /// </summary>
        public void update()
        {
            

            var newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.D1))
            {
                LobbyBrowserTable.CreateNewRow(new string[6] {"CST", "Team Battle", "Konstantin", "3", "6", "100"});
                Thread.Sleep(100);
            }
             

            if (newState.IsKeyDown(Keys.D0))
            {
                LobbyBrowserTable.RemoveLastRow();
                Thread.Sleep(100);
            }
            
            MouseState mouseState = Mouse.GetState();
            nextlayerButton.update(mouseState);
            lastlayerButton.update(mouseState);
            if (nextlayerButton.isClicked == true)
            {
                game.GoToLayer4();
                nextlayerButton.isClicked = false;
            }
            
            if (lastlayerButton.isClicked == true)
            {
                game.GoToLayer1();
                lastlayerButton.isClicked = false;
            }

        }
        /// <summary>
        /// Draws the button on the viewport
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            LobbyBrowserTable.drawTable(spriteBatch);
            nextlayerButton.draw(spriteBatch);
            banner.draw(spriteBatch);
            lastlayerButton.draw(spriteBatch);
            spriteBatch.End();
        }

       
    }
}
