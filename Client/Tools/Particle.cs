using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceUnionXNA.Tools {
	public class Particle {

		public Texture2D Texture { get; set; }      // The texture that will be drawn to represent the particle
		public Vector2 Position { get; set; }       // The current position of the particle        
		public Vector2 Velocity { get; set; }       // The speed of the particle at the current instance
		public float Angle { get; set; }            // The current angle of rotation of the particle
		public float AngularVelocity { get; set; }  // The speed that the angle is changing
		public Color color { get; set; }            // The color of the particle
		public float Size { get; set; }             // The size of the particle
		public float TTL { get; set; }                // The 'time to live' of the particle
		private  float timeLapsed;
		public  bool isActive;
		private  float red, green, blue, alpha;

		public Particle(Texture2D texture, Vector2 position, Vector2 velocity,
			float angle, float angularVelocity, Color clr, float size, float ttl) {
			Texture = texture;
			Position = position;
			Velocity = velocity;
			Angle = angle;
			AngularVelocity = angularVelocity;
			color = clr;
			Size = size;
			TTL = ttl;
			red = color.R / 255;
			green = color.G / 255;
			blue = color.B / 255;
			alpha = color.A / 255;
			isActive = true;
		}

		public void Update(GameTime gameTime) {
			timeLapsed += (float) gameTime.ElapsedGameTime.TotalSeconds;
			if (timeLapsed >= TTL)
				isActive = false;

			Position += Velocity * (float) gameTime.ElapsedGameTime.TotalSeconds;
			Angle += AngularVelocity;

		}

		public void Draw(SpriteBatch spriteBatch) {
			Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
			Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

			color = new Color(red - (timeLapsed / TTL), green - timeLapsed * 4f / TTL, blue - (timeLapsed * 4f / TTL));
			spriteBatch.Draw(Texture, Position, sourceRectangle, color,
				Angle, origin, Size, SpriteEffects.None, 0f);
		}
	}
}
