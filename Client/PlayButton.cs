using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceUnion
{
    class PlayButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle buttonRectangle;

        public Vector2 size;

        public PlayButton(Texture2D newTexture, GraphicsDevice graphics) {
            texture = newTexture;
            size = new Vector2(300, 150);
        }
        public bool isClicked = false;

        public void Update(MouseState mouse){
            buttonRectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            if (mouseRectangle.Intersects(buttonRectangle))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    isClicked = true;
                }
                else
                {
                    isClicked = false;
                }
            }
            

        }

        public void setPosition(Vector2 newPosition) 
        {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(texture, buttonRectangle, Color.Blue);
        }
    }
}
