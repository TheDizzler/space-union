using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnion.Tools;

namespace SpaceUnion {

	public class Projectile : Sprite {


		private HitBox projectileHitBox;
		private bool Active; // State of the Projectile
		/// <summary>
		/// The firerer of the projectile
		/// </summary>
		private Ship owner;
		//Viewport viewport; // Represents the viewable boundary of the game

		public HitBox getProjectileHitBox() {
			return projectileHitBox;
		}

		// Determines how fast the projectile moves
		protected float projectileMoveSpeed = 2000f;

		private Vector2 velocity;
		//private float projectileVelocityDirectionX;
		//private float projectileVelocityDirectionY;
		private int projectileTTL = 50;  //The projectile will disappear after these many updates
		

		//private int projectileDamage; // The amount of damage the projectile can inflict to an enemy


		public Projectile(Texture2D texture, Vector2 position, Ship ship)
			: base(texture, position) {

			owner = ship;
			rotation = (float) ship.getRotation();
			projectileHitBox = new HitBox(position.X, position.Y, texture.Width, texture.Height);

			velocity = new Vector2((float) Math.Sin(rotation) * projectileMoveSpeed, (float) Math.Cos(rotation) * projectileMoveSpeed);
			Active = true;
		}

		public void Initialize(Viewport vwprt) {

			//this.viewport = vwprt;

			Active = true;
			//projectileDamage = 2;
		}

		public void update(GameTime gameTime) {
			if (projectileTTL > 0) {

				//position.X += projectileVelocityDirectionX;
				//position.Y -= projectileVelocityDirectionY;
				position.X += velocity.X * (float) gameTime.ElapsedGameTime.TotalSeconds;
				position.Y -= velocity.Y * (float) gameTime.ElapsedGameTime.TotalSeconds;

				projectileHitBox.updatePosition(position.X, position.Y); //updating hitbox
				projectileTTL--;

			} else {
				Active = false;
			}
		}

		//public override void draw(SpriteBatch sBatch) {
		//	sBatch.Draw(texture, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
		//}

		public bool getActive() {
			return Active;
		}
	}
}
