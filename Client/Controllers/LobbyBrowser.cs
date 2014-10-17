using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnion.Ships;
using SpaceUnion.Tools;
///Created by Matthew Baldock
namespace SpaceUnion.Controllers
{
    class LobbyBrowser
    {
        BaseButton nextlayerButton;
        CustomGUI banner;
        BaseButton lastlayerButton;
        Game1 game;
        /// <summary>
        /// Constructor for Lobby Browser
        /// Creates Buttons to other GameStates
        /// </summary>
        ///  
        /// Entire file created by Matthew Baldock
        /// <param name="game"></param>
        public LobbyBrowser(Game1 game)
        {
            this.game = game;
            nextlayerButton = new BaseButton(Game1.Assets.gamelobby) { height = 100, width = 300 };
            banner = new CustomGUI(game, "Lobby browser");
            lastlayerButton = new BaseButton(Game1.Assets.lobbyoptions) { height = 100, width = 300 };
            lastlayerButton.setPosition(new Vector2(0, game.getScreenHeight() - 300));
            nextlayerButton.setPosition(new Vector2(0, game.getScreenHeight() - 100));
          
        }
        /// <summary>
        /// Update method, checks what button is clicked and performs proper action
        /// </summary>
        public void update()
        {
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
            nextlayerButton.draw(spriteBatch);
            banner.draw(spriteBatch);
            lastlayerButton.draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
