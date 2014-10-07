﻿using System;
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

		public Texture2D guiRectangle;
		public Texture2D playButton;
		// bgs and doodads
		public Texture2D background;
		public Texture2D starfield1; // stackable background layer
		public Texture2D starfield2; // stackable background layer
		public Texture2D starfield3; // stackable background layer

		public Texture2D explosions;
        public Texture2D explosionsBig;

		// space objects
		public Texture2D earth;
        public Texture2D winflag1;
		public Texture2D waterPlanet;
        public Texture2D asteroid;

		// ships and platforms
		public Texture2D shuttle;
        public Texture2D spaceShipTest;

		public Texture2D ufo;
		public Texture2D wedge;
		public Texture2D wrench;

        // projectiles
        public Texture2D laser;



		public AssetManager(ContentManager cntnt) {

			Content = cntnt;
		}




		internal void loadContent(GraphicsDevice graphicsDevice) {


			background = Content.Load<Texture2D>("Backgrounds/background");
			font = Content.Load<SpriteFont>("SpriteFonts/SpriteFont1");
			shuttle = Content.Load<Texture2D>("Spaceships/shuttle");

			playButton = Content.Load<Texture2D>("Buttons/playbutton");

			guiRectangle = new Texture2D(graphicsDevice, 1, 1);
			guiRectangle.SetData(new[] { Color.White });


			starfield1 = Content.Load<Texture2D>("Backgrounds/starfield (800x600)");
			starfield2 = Content.Load<Texture2D>("Backgrounds/galaxy");
			starfield3 = Content.Load<Texture2D>("Backgrounds/beautifulbg");

			explosions = Content.Load<Texture2D>("Animations/fireballs");
			explosionsBig = Content.Load<Texture2D>("Animations/big fireballs with blur");


			waterPlanet = Content.Load<Texture2D>("StellarObjects/waterplanet (256x256)");

            spaceShipTest = Content.Load<Texture2D>("Spaceships/spaceshiptest");
			ufo = Content.Load<Texture2D>("Spaceships/circleship (128x128)");
			wedge = Content.Load<Texture2D>("Spaceships/triangleship (128x128)");
			wrench = Content.Load<Texture2D>("Spaceships/wrenchship");

            winflag1 = Content.Load<Texture2D>("WinFlags/WinFlag1");

            laser = Content.Load<Texture2D>("Projectiles/laser");
            asteroid = Content.Load<Texture2D>("StellarObjects/asteroid(56x56)");
		}
	}
}