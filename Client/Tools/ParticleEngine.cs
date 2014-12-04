using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceUnionXNA.Tools {
	public class ParticleEngine {
		private Random random;
		public Vector2 EmitterLocation { get; set; }
		private List<Particle> particles;
		private List<Texture2D> textures;

		public ParticleEngine(List<Texture2D> textures, Vector2 location) {
			EmitterLocation = location;
			this.textures = textures;
			this.particles = new List<Particle>();
			random = new Random();
		}

		public void Update(GameTime gameTime) {
			//int total = 10;

			//for (int i = 0; i < total; i++) {
			//	particles.Add(GenerateNewParticle());
			//}

			for (int particle = 0; particle < particles.Count; particle++) {
				particles[particle].Update(gameTime);
				if (!particles[particle].isActive) {
					particles.RemoveAt(particle);
					particle--;
				}
			}
		}

		private Particle GenerateNewParticle() {

			Texture2D texture = textures[random.Next(textures.Count)];
			Vector2 position = EmitterLocation;
			Vector2 velocity = new Vector2(
									1f * (float) (random.NextDouble() * 2 - 1),
									1f * (float) (random.NextDouble() * 2 - 1));
			float angle = 0;
			float angularVelocity = 0.1f * (float) (random.NextDouble() * 2 - 1);
			Color color = new Color(
						(float) random.NextDouble(),
						(float) random.NextDouble(),
						(float) random.NextDouble());
			float size = (float) random.NextDouble();
			int ttl = 20 + random.Next(40);

			return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
		}

		public void Draw(SpriteBatch spriteBatch) {
			
			for (int index = 0; index < particles.Count; index++) {
				particles[index].Draw(spriteBatch);
			}
		}

		public void createThrustParticle(Vector2 location, Vector2 shipAccel, float timeToLive) {

			Texture2D texture = textures[random.Next(textures.Count)];
			Vector2 position = location;
			Vector2 velocity = -shipAccel;
			float angle = 0;
			float angularVelocity = 0.1f * (float) (random.NextDouble() * 2 - 1);
			Color color = new Color(1f, 1f, 0);
			float size = 2.5f;
			float ttl = timeToLive;

			particles.Add(new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl));
		}
	}
}
