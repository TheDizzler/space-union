﻿using System;
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
        
        public Texture2D Texture; // Image representing the Projectile
        private HitBox projectileHitBox;
        private bool Active; // State of the Projectile
        private int damage = 2;

        Viewport viewport; // Represents the viewable boundary of the game

        public HitBox getProjectileHitBox() {
            return projectileHitBox;
        }

        // Get the width of the projectile
        public int Width
        {
            get { return Texture.Width; }
        }

        // Get the height of the projectile
        public int Height
        {
            get { return Texture.Height; }
        }

        // Determines how fast the projectile moves
        float projectileMoveSpeed = 20f;

        private float projectileVelocityDirectionX;
        private float projectileVelocityDirectionY;
        private int projectileTTL = 50;  //The projectile will disappear after these many updates
        //private int projectileDamage; // The amount of damage the projectile can inflict to an enemy
        public int Damage 
        {
            get { return damage; } 
        }
        

         public Projectile(Texture2D texture, Vector2 position, Ship ship)
			: base(texture, position) {
                rotation = (float)ship.getRotation();
                projectileHitBox = new HitBox(position.X, position.Y, texture.Width, texture.Height);
                projectileVelocityDirectionX = (float)Math.Sin(rotation) * projectileMoveSpeed;
                projectileVelocityDirectionY = (float)Math.Cos(rotation) * projectileMoveSpeed;
		}

        public void Initialize(Viewport viewport, Texture2D texture)
        {
            Texture = texture;
            this.viewport = viewport;

            Active = true;
        }

        public void Update()
        {
            if (projectileTTL > 0)
            {
                position.X += projectileVelocityDirectionX;
                position.Y -= projectileVelocityDirectionY;
                projectileHitBox.updatePosition(position.X, position.Y); //updating hitbox
                projectileTTL--;
                
            }
            else 
            {
                Active = false;
            }
        }

        public override void draw(SpriteBatch sBatch)
        {
            sBatch.Draw(texture, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
        }

        public bool getActive()
        {
            return Active;
        }
        public void setActive(bool b)
        {
            this.Active = b;
        }
    }
}