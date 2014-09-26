using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SpaceUnion.Tools {

	/// <summary>
	/// A class to hold textures, animations, audio, etc..
	/// </summary>
	public class AssetManager {

		private   ContentManager Content;

		// misc graphics
		public SpriteFont font;


		// bgs and doodads
		public Texture2D background;


		// space objects
		public Texture2D earth;



		// ships and platforms
		public Texture2D shuttle;

		public Texture2D ufo;
		public Texture2D wedge;
		public Texture2D wrench;
		


		public AssetManager(ContentManager Content) {
			
			this.Content = Content;
		}




		internal void loadContent() {


			background = Content.Load<Texture2D>("Backgrounds/background");
			font = Content.Load<SpriteFont>("SpriteFonts/SpriteFont1"); // Use the name of your sprite font file here instead of 'Score'.


			shuttle = Content.Load<Texture2D>("Spaceships/shuttle");

			ufo = Content.Load<Texture2D>("Spaceships/circleship (128x128)");
			wedge = Content.Load<Texture2D>("Spaceships/triangleship (128x128)");
			wrench = Content.Load<Texture2D>("Spaceships/wrenchship");
		}
	}
}
