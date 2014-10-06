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
        Ship selectedship;
        GeneralButton lastbutton;

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
            selectShip1.height = 128;
            selectShip1.width = 128;
            selectShip2.height = 128;
            selectShip2.width = 128;
            selectShip3.height = 128;
            selectShip3.width = 128;
            selectShip4.height = 128;
            selectShip4.width = 128;
            confirmButton.setPosition(new Vector2((game.getScreenWidth()  - confirmButton.width)/2, (game.getScreenHeight() - confirmButton.height)));
            selectShip1.setPosition(new Vector2((game.getScreenWidth() - (selectShip1.width*4))/2,(selectShip1.height)));
            selectShip2.setPosition(new Vector2((game.getScreenWidth() - (selectShip2.width*2)) / 2, (selectShip2.height)));
            selectShip3.setPosition(new Vector2((game.getScreenWidth() + (selectShip3.width/2)) / 2, (selectShip3.height)));
            selectShip4.setPosition(new Vector2((game.getScreenWidth() + (selectShip4.width *3)) / 2, (selectShip4.height)));
            selectedship = new Ship(Game1.Assets.ufo, new Vector2(200, 200));
            selectShip1.selected = true;
            lastbutton = selectShip1;
            
        }
        public Ship getship()
        {
            return selectedship;
        }
        public void Update()
        {
            

            MouseState mouseState = Mouse.GetState();
            confirmButton.Update(mouseState);
            selectShip1.Update(mouseState);
            selectShip2.Update(mouseState);
            selectShip3.Update(mouseState);
            selectShip4.Update(mouseState);
            
            
            if (selectShip1.selected && selectShip2.selected)
            {
                if (lastbutton == selectShip2)
                {

                    selectedship = new Ship(Game1.Assets.ufo, new Vector2(200, 200));
                    selectShip2.selected = false;
                    selectShip3.selected = false;
                    selectShip4.selected = false;
                    lastbutton = selectShip1;
                }
                else
                {
                    selectedship = new Ship(Game1.Assets.wedge, new Vector2(200, 200));
                    selectShip1.selected = false;
                    selectShip3.selected = false;
                    selectShip4.selected = false;
                    lastbutton = selectShip2;
                }
                

            }
            if (selectShip1.selected && selectShip3.selected)
            {
                if (lastbutton == selectShip3)
                {

                    selectedship = new Ship(Game1.Assets.ufo, new Vector2(200, 200));
                    selectShip2.selected = false;
                    selectShip3.selected = false;
                    selectShip4.selected = false;
                    lastbutton = selectShip1;
                }
                else
                {
                    selectedship = new Ship(Game1.Assets.wrench, new Vector2(200, 200));
                    selectShip1.selected = false;
                    selectShip2.selected = false;
                    selectShip4.selected = false;
                    lastbutton = selectShip3;
                }


            }
            if (selectShip1.selected && selectShip4.selected)
            {
                if (lastbutton == selectShip4)
                {

                    selectedship = new Ship(Game1.Assets.ufo, new Vector2(200, 200));
                    selectShip2.selected = false;
                    selectShip3.selected = false;
                    selectShip4.selected = false;
                    lastbutton = selectShip1;
                }
                else
                {
                    selectedship = new Ship(Game1.Assets.shuttle, new Vector2(200, 200));
                    selectShip1.selected = false;
                    selectShip3.selected = false;
                    selectShip2.selected = false;
                    lastbutton = selectShip4;
                }


            }
            if (selectShip2.selected && selectShip3.selected)
            {
                if (lastbutton == selectShip3)
                {
                    selectedship = new Ship(Game1.Assets.wedge, new Vector2(200, 200));
                    selectShip1.selected = false;
                    selectShip3.selected = false;
                    selectShip4.selected = false;
                    lastbutton = selectShip2;
                }
                else
                {
                    selectedship = new Ship(Game1.Assets.wrench, new Vector2(200, 200));
                    selectShip1.selected = false;
                    selectShip2.selected = false;
                    selectShip4.selected = false;
                    lastbutton = selectShip3;
                }
            }
            if (selectShip2.selected && selectShip4.selected)
            {
                if (lastbutton == selectShip4)
                {
                    selectedship = new Ship(Game1.Assets.wedge, new Vector2(200, 200));
                    selectShip1.selected = false;
                    selectShip3.selected = false;
                    selectShip4.selected = false;
                    lastbutton = selectShip2;
                }
                else
                {
                    selectedship = new Ship(Game1.Assets.shuttle, new Vector2(200, 200));
                    selectShip1.selected = false;
                    selectShip2.selected = false;
                    selectShip3.selected = false;
                    lastbutton = selectShip4;
                }
            }

            if (selectShip3.selected && selectShip4.selected)
            {
                if (lastbutton == selectShip4)
                {
                    selectedship = new Ship(Game1.Assets.wrench, new Vector2(200, 200));
                    selectShip1.selected = false;
                    selectShip2.selected = false;
                    selectShip4.selected = false;
                    lastbutton = selectShip3;
                }
                else
                {
                    selectedship = new Ship(Game1.Assets.shuttle, new Vector2(200, 200));

                    selectShip1.selected = false;
                    selectShip3.selected = false;
                    selectShip2.selected = false;
                    lastbutton = selectShip4;
                }
            }
            
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
            selectShip1.draw(spriteBatch);
            selectShip2.draw(spriteBatch);
            selectShip3.draw(spriteBatch);
            selectShip4.draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
