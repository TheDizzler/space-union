using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using SpaceUnion.Tools;
namespace SpaceUnion.Animations
{
    class ScrollingBackground :Sprite
    {
        /// <summary>
        /// Create 2 textures and 2 rectangles to hold image
        /// </summary>
        public Texture2D background1;
        public Texture2D background2;
        public Rectangle imageRectangle;
        public Rectangle imageRectangle2;
        /// <summary>
        /// Constructor for ScrollingBackground
        /// </summary>
        /// <param name="texture"></param>
        public ScrollingBackground(Texture2D texture)
            : base(texture, Vector2.Zero)
        {
            background1 = texture;
            background2 = texture;
           
        }
        /// <summary>
        /// Draw method for ScrollingBackground
        /// 
        /// overrides sprite draw
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void draw(SpriteBatch spriteBatch)
        {
           imageRectangle2.X += imageRectangle.Width; 
           spriteBatch.Draw(background1, imageRectangle, Color.White);
           spriteBatch.Draw(background2, imageRectangle2, Color.White);
           
        }
        /// <summary>
        /// Set the position for the image rectangles
        /// </summary>
        /// <param name="newPosition"></param>
        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
            imageRectangle = new Rectangle((int)position.X, (int)position.Y, (int)width, (int)height);
            imageRectangle2 = new Rectangle((int)position.X, (int)position.Y, (int)width, (int)height);
            
        }
        /// <summary>
        /// Upadate the image rectangle positions
        /// 
        /// when their position is less than their width past x = 0
        /// move to the other side of the screen
        /// </summary>
        public  void update()
        {
                imageRectangle.X -= 1;
                imageRectangle2.X -= 1;
                this.setPosition(new Vector2(imageRectangle.X,imageRectangle.Y));
                this.setPosition(new Vector2(imageRectangle2.X, imageRectangle2.Y));
                if (imageRectangle.X < (0-imageRectangle.Width))
                {
                    imageRectangle.X += imageRectangle2.Width;
                }
                if (imageRectangle2.X < (0-imageRectangle2.Width))
                {
                    imageRectangle2.X += imageRectangle.Width;
                }
        }
    }
}
