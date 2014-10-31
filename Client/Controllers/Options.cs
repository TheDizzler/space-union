using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnion.Ships;
using SpaceUnion.Weapons;
using SpaceUnion.Tools;
using SpaceUnion.Animations;
namespace SpaceUnion.Controllers
{
    class Options
    {
        Game1 game;
        BaseButton confirmButton;
        ScrollingBackground scroll;
        GameTime gametime;
        /// <summary>
        /// Constructor for Options
        /// </summary>
        /// <param name="game"></param>
        public Options(Game1 game)
        {
            this.game = game;
            gametime = new GameTime();
            confirmButton = new BaseButton(Game1.Assets.confirm) { height = 100, width = 200 };
            confirmButton.setPosition(new Vector2((int)(100),
                                                  (int)(100)));
            scroll = new ScrollingBackground(Game1.Assets.background) { height = game.getScreenHeight(), width = game.getScreenWidth() };
            scroll.setPosition(new Vector2((int)0, (int)0));
            
        }
        /// <summary>
        /// Update Options, buttons and background
        /// </summary>
        public void update()
        {
            MouseState mouseState = Mouse.GetState();
            confirmButton.update(mouseState);
            
            if (confirmButton.isClicked == true)
            {
                game.GoToMain();
                confirmButton.isClicked = false;
            }
            scroll.update();
        }

        /// <summary>
        /// Draw images on to Options view
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            scroll.draw(spriteBatch);
            confirmButton.draw(spriteBatch);
            
            spriteBatch.End();
        }
    }
}