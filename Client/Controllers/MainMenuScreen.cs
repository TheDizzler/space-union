using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceUnion
{
    class MainMenuScreen
    {
        private Texture2D texture;
        private Game1 game;
        PlayButton btnPlay;
        
        public MainMenuScreen(Game1 game)
        {
            this.game = game;
            texture = game.Content.Load<Texture2D>("Buttons/playbutton");
            btnPlay = new PlayButton(game.Content.Load<Texture2D>("Buttons/playbutton"),game.GraphicsDevice);
            btnPlay.setPosition(new Vector2(game.getScreenWidth()/2, game.getScreenHeight()/2));
        }

        public void Update()
        {
            MouseState mouseState = Mouse.GetState();
            btnPlay.Update(mouseState);
            
            if(btnPlay.isClicked == true)
            {
              game.StartGame();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            btnPlay.Draw(spriteBatch);
        }

    }
}
