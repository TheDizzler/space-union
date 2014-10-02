using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;

namespace SpaceUnion
{
    class Projectile : Sprite
    {
        // Image representing the Projectile
        public Texture2D Texture;

        // State of the Projectile
        public bool Active;

        // The amount of damage the projectile can inflict to an enemy
        public int Damage;

        // Represents the viewable boundary of the game
        Viewport viewport;

        // Get the width of the projectile ship
        public int Width
        {
            get { return Texture.Width; }
        }

        // Get the height of the projectile ship
        public int Height
        {
            get { return Texture.Height; }
        }

        // Determines how fast the projectile moves
        float projectileMoveSpeed = 20f;

        private float projectileVelocityDirectionX;
        private float projectileVelocityDirectionY;


         public Projectile(Texture2D texture, Vector2 position, Ship ship)
			: base(texture, position) {
                rotation = (float)ship.getRotation();
                projectileVelocityDirectionX = (float)Math.Sin(rotation) * projectileMoveSpeed;
                projectileVelocityDirectionY = (float)Math.Cos(rotation) * projectileMoveSpeed;
		}

        public void Initialize(Viewport viewport, Texture2D texture)
        {
            Texture = texture;
            this.viewport = viewport;

            Active = true;

            Damage = 2;

        }

        public void Update()
        {
            // Projectiles always move to the right
            position.X += projectileVelocityDirectionX;
            position.Y -= projectileVelocityDirectionY;

        }

        public override void draw(SpriteBatch sBatch)
        {
            sBatch.Draw(texture, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
        }

    }
}
