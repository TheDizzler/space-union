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
namespace SpaceUnion.Controllers
{
    class Options
    {
        Game1 game;
        BaseButton confirmButton;
        public Options(Game1 game)
        {
            this.game = game;
            confirmButton = new BaseButton(Game1.Assets.confirm) { height = 100, width = 200 };
            confirmButton.setPosition(new Vector2((int)(100),
                                                  (int)(100)));
            
        }
        public void update()
        {
            MouseState mouseState = Mouse.GetState();
            confirmButton.update(mouseState);

            if (confirmButton.isClicked == true)
            {
                game.GoToMain();
                confirmButton.isClicked = false;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            confirmButton.draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
