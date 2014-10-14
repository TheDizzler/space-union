using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using SpaceUnion.StellarObjects;
using SpaceUnion.Tools;
///Created by Matthew Baldock
namespace SpaceUnion.Tools
{
    class CustomGUI
    {
        String bannerMessage;
        public const int guiHeight = 80;
        SpriteFont font;
        Vector2 line1Pos;
        /// <summary>
        /// Constructor for CustomGUI, gets game and string to print
        /// </summary>
        /// <param name="game"></param>
        /// <param name="customBanner"></param>
        public CustomGUI(Game1 game, String customBanner)
        {
            font = Game1.Assets.font;
            line1Pos = new Vector2(100, 80);
            bannerMessage = customBanner;
        }
        /// <summary>
        /// Draw method for CustomGUI, uses spritebatch to draw text on screen
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void draw(SpriteBatch spriteBatch)
        {

            

            spriteBatch.DrawString(font, bannerMessage,
                line1Pos, Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);

        }

    }
}
