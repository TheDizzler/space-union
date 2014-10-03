using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceUnion
{
    /// <summary>
    /// Timer class implements a game timer that counts down to zero.
    /// </summary>
    class Timer
    {
        private float countdownTime;
        private float elapsedTime;
        private Boolean isCountdownOver = false;

        public Timer(float cTime) 
        {
            countdownTime = cTime;
        }

        public void Update(GameTime gameTime){
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds; //Timer  
            countdownTime -= elapsedTime;
            if (countdownTime <= 0)
            {
                isCountdownOver = true;
            }
        }

        public Boolean getCountdownOver() 
        {
            return isCountdownOver;
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.DrawString(Game1.Assets.font, "Time: " + (countdownTime), new Vector2(300, 400), Color.Red);
        }
    }
}
