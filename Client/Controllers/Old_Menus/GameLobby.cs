﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.Tools;
///Created by Matthew Baldock
namespace SpaceUnionXNA.Controllers
{
    class GameLobby
    {
        BaseButton nextlayerButton;
        BaseButton nextlayerButton2;
        BaseButton lastlayerButton;
        CustomGUI banner;
        Game1 game;
        /// <summary>
        /// Constructor for Game Lobby
        /// Creates Buttons to other GameStates
        /// </summary>
        ///  
        /// Entire file created by Matthew Baldock
        /// <param name="game"></param>
        public GameLobby(Game1 game)
        {
            this.game = game;
            banner = new CustomGUI(game, "Game Lobby");
            nextlayerButton = new BaseButton(Game1.Assets.lobbybrowser) { height = 100, width = 300 };
            nextlayerButton2 = new BaseButton(Game1.Assets.gameroom) { height = 100, width = 300 };
            lastlayerButton = new BaseButton(Game1.Assets.createlobby) { height = 100, width = 300 };
            lastlayerButton.setPosition(new Vector2(0, game.getScreenHeight() - 300));
            nextlayerButton.setPosition(new Vector2(0, game.getScreenHeight() - 100));
            nextlayerButton2.setPosition(new Vector2(0, game.getScreenHeight() - 500));
        }
        /// <summary>
        /// Update method, checks what button is clicked and performs proper action
        /// </summary>
        public void update()
        {
            MouseState mouseState = Mouse.GetState();
            nextlayerButton.update(mouseState);
            nextlayerButton2.update(mouseState);
            lastlayerButton.update(mouseState);
            if (nextlayerButton.isClicked == true)
            {
                game.GoToLobbyBrowser();
                nextlayerButton.isClicked = false;
            }
            if (nextlayerButton2.isClicked == true)
            {
                game.GoToGameRoom();
                nextlayerButton2.isClicked = false;
            }
            if (lastlayerButton.isClicked == true)
            {
                game.GoToCreateLobby();
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
            nextlayerButton.draw(spriteBatch);
            nextlayerButton2.draw(spriteBatch);
            lastlayerButton.draw(spriteBatch);
            banner.draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
