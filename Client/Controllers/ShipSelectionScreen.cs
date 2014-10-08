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
        Game1 game;
        GeneralButton[] shipSelectionArray;
        GeneralButton confirmButton;
        Ship selectedShip;
        GeneralButton lastButton;
        /* Default size of the ships */
        const int WIDTH       = 128;
        const int HEIGHT      = 128;
        const float SHIPCOUNT = 1;   // Change value according to how many different ships are available
        int shipsPerRow = 0;

        public ShipSelectionScreen(Game1 game)
        {
            shipsPerRow = (game.getScreenWidth() - (WIDTH * 2)) / WIDTH;
            float currentShipsPerRow = shipsPerRow;
            int shipsPerLastRow = (int)(SHIPCOUNT % shipsPerRow);
            float shipsPerColumn = (float)Math.Ceiling(SHIPCOUNT / shipsPerRow);
            this.game = game;
            shipSelectionArray = new GeneralButton[(int)SHIPCOUNT];
            /* Actual ships used; commented out to test other functions */
            //shipSelectionArray[0] = new GeneralButton(Game1.Assets.ufo, game.GraphicsDevice);
            //shipSelectionArray[1] = new GeneralButton(Game1.Assets.wedge, game.GraphicsDevice);
            //shipSelectionArray[2] = new GeneralButton(Game1.Assets.wrench, game.GraphicsDevice);
            //shipSelectionArray[3] = new GeneralButton(Game1.Assets.shuttle, game.GraphicsDevice);
            
            /* For Testing X amount of ships; Remove */
            for (int i = 0; i < SHIPCOUNT; i++)
            {
                shipSelectionArray[i] = new GeneralButton(Game1.Assets.ufo, game.GraphicsDevice);
            }

            confirmButton = new GeneralButton(Game1.Assets.confirm, game.GraphicsDevice);
            confirmButton.height = 100;
            confirmButton.width = 300;

            /* Sets the ship's icon size and then its position on the screen based on how many ships there are */
            for (int i = 0; i < SHIPCOUNT; i++)
            {
                if (i == SHIPCOUNT - shipsPerLastRow)
                {
                    currentShipsPerRow = shipsPerLastRow;
                }
                shipSelectionArray[i].height = HEIGHT;
                shipSelectionArray[i].width = WIDTH;
                shipSelectionArray[i].setPosition(
                    new Vector2(/* X Coordinate */
                               ((i % shipsPerRow * WIDTH)            // Sets each ship side by side from left to right starting from 0
                              + (game.getScreenWidth() / 2)          // Moves all ships towards the center
                              - (WIDTH * (currentShipsPerRow / 2))), // Moves all ships back by half the ships total width
                                /* Y Coordinate */
                                (i / shipsPerRow * HEIGHT)           // Sets each row from top to bottem starting from 0
                              + ((game.getScreenHeight() / 2) - 50)  // Moves the rows towards the center
                              - (HEIGHT * (shipsPerColumn / 2))));   // Moves the rows back half the rows total height
            }
            
            confirmButton.setPosition(new Vector2((game.getScreenWidth() - confirmButton.width) / 2, 
                                                  (game.getScreenHeight() - confirmButton.height)));
            
            /* Sets the default selected ship */
            selectedShip = new Ship(Game1.Assets.ufo, new Vector2(200, 200));
            shipSelectionArray[0].selected = true;
            lastButton = shipSelectionArray[0];
            
        }
        public Ship getship()
        {
            return selectedShip;
        }
        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            confirmButton.Update(mouseState);

            for (int i = 0; i < shipSelectionArray.Length; i++)
            {
                shipSelectionArray[i].Update(mouseState);
            }

            /* Allows for feedback of selecting a ship and sets the selected ship */
            for (int i = 0; i < SHIPCOUNT; i++)
            {
                for (int j = 0; j < SHIPCOUNT; j++)
                {
                    /* Due to how GeneralButton works, at most 2 ships will be selected
                     * which results in this logic to compensate for that fact 
                     */
                    if (shipSelectionArray[i].selected && shipSelectionArray[j].selected && i != j)
                    {
                        if (lastButton == shipSelectionArray[j])
                        {
                            selectedShip = new Ship(shipSelectionArray[i].getTexture(), new Vector2(200,200));
                            shipSelectionArray[j].selected = false;
                            lastButton = shipSelectionArray[i];
                        }
                        else
                        {
                            selectedShip = new Ship(shipSelectionArray[j].getTexture(), new Vector2(200, 200));
                            shipSelectionArray[i].selected = false;
                            lastButton = shipSelectionArray[j];
                        }
                    }
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

            for (int i = 0; i < SHIPCOUNT; i++)
            {
                shipSelectionArray[i].draw(spriteBatch);
            }

            spriteBatch.End();
        }
    }
}
