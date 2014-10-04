using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using SpaceUnion.Tools;

namespace SpaceUnion
{
    /// <summary>
    /// Hitboxes will contain an array of rectangles
    /// </summary>
    public class HitBox
    {
        private int width;
        private int height;
        private float xPos; //X coordinate of hitbox
        private float yPos; //Y coordinate of hitbox
        private Rectangle rectHitBox;

        public HitBox(float x, float y, int w, int h) 
        {
            xPos = x;
            yPos = y;
            width = w;
            height = h;
        }

        public Rectangle getArray(){
            return rectHitBox = new Rectangle((int)xPos, (int)yPos, width, height);
        }

        public void updatePosition(float newX,float newY){
            xPos = newX;
            yPos = newY;
        }

    }
}
