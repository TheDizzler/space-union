using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System.Text;
using SpaceUnion.Tools;

namespace SpaceUnion
{
    class WinFlag : Sprite
    {

        public WinFlag(Texture2D tex, Vector2 pos)
			: base(tex, pos) {
		}

        public override void draw(SpriteBatch sBatch)
        {


            //Vector2 location = new Vector2(position.X, position.Y);
            //Rectangle sourceRectangle = new Rectangle(0, 0, shipTexture.Width, shipTexture.Height);
            //Vector2 origin = new Vector2(shipTexture.Width / 2, shipTexture.Height / 2);
            //sBatch.Draw(shipTexture, location, null, Color.White, angle, origin, 0.1f, SpriteEffects.None, 0);


            base.draw(sBatch);
        }
    }
}
