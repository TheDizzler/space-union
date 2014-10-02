using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnion.Tools;


namespace SpaceUnion.Controllers
{
    class ShipSelectionScreen
    {
        //private Texture2D texture;
        Game1 game;
        public GeneralButton selectShip1;
        GeneralButton selectShip2;
        GeneralButton selectShip3;
        GeneralButton selectShip4;
        GeneralButton confirmButton;

        public ShipSelectionScreen(Game1 game)
        {
            this.game = game;
            selectShip1 = new GeneralButton(Game1.Assets.ufo, game.GraphicsDevice);
            selectShip2 = new GeneralButton(Game1.Assets.wedge, game.GraphicsDevice);
            selectShip3 = new GeneralButton(Game1.Assets.wrench, game.GraphicsDevice);
            selectShip4 = new GeneralButton(Game1.Assets.shuttle, game.GraphicsDevice);
            confirmButton = new GeneralButton(Game1.Assets.confirm, game.GraphicsDevice);
            confirmButton.height = 100;
            confirmButton.width = 300;
            confirmButton.setPosition(new Vector2((game.getScreenWidth()  - confirmButton.width)/2, (game.getScreenHeight() - confirmButton.height)));
        }
        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            confirmButton.Update(mouseState);

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
