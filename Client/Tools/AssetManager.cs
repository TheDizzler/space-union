using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
///Edited by Matthew Baldock
namespace SpaceUnionXNA.Tools {

	/// <summary>
	/// A class to hold textures, animations, audio, etc..
	/// </summary>
	public class AssetManager {

		public   ContentManager Content;

		// misc graphics
		public SpriteFont font;

		public Texture2D guiRectangle;
		public Texture2D playButton;
		/// <summary>
		/// Ship selection button texture
		/// 
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D shipselection;
		/// <summary>
		/// Confirm button texture
		/// 
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D confirm;
		/// <summary>
		/// Lobby Options button texture
		/// 
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D lobbyoptions;
		/// <summary>
		/// Lobby Browser button texture
		/// 
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D lobbybrowser;
		/// <summary>
		/// Create Lobby button texture
		/// 
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D createlobby;
		/// <summary>
		/// Game Lobby button texture
		/// 
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D gamelobby;
		/// <summary>
		/// Game Room button texture
		/// 
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D gameroom;
		public Texture2D options;

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
		public Texture2D moon;
		public Texture2D asteroid;

		// ships and platforms
		public Texture2D shuttle;
		public Texture2D spaceShipTest;

		public Texture2D ufo;
		public Texture2D stunt;
		/// <summary>
		/// Zoid ship texture
		/// 
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D zoid;

		/// <summary>
		/// Galactus Ship Texture
		/// 
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D galactusship;
		public Texture2D bug;



		// projectiles
		public Texture2D laser;
		public Texture2D missile;
		public Texture2D shield;
		public Texture2D moltenBullet;
		public GraphicsDevice graphicsDevice;

		// radar icons
		public Texture2D shipMapIcon;
		public  Texture2D pixel;

        public Texture2D alpha_ship_texture;
        public Texture2D theta_ship_texture;
        public Texture2D omega_ship_texture;


		public AssetManager(ContentManager cntnt) {

			Content = cntnt;
		}



		/// <summary>
		/// Loading Graphics for all texture assets from
		/// the folders they reside in.
		/// </summary>
		/// <param name="graphicsDevice"></param>
		internal void loadContent(GraphicsDevice graphicsDevice) {

			this.graphicsDevice = graphicsDevice;
			background = Content.Load<Texture2D>("Backgrounds/background");
			font = Content.Load<SpriteFont>("SpriteFonts/SpriteFont1");
			shuttle = Content.Load<Texture2D>("Spaceships/shuttle");

			playButton = Content.Load<Texture2D>("Buttons/playbutton");

			///Added by Matthew Baldock
			confirm = Content.Load<Texture2D>("Buttons/confirm");
			shipselection = Content.Load<Texture2D>("Buttons/shipselection");

			lobbyoptions = Content.Load<Texture2D>("Buttons/lobbyoptions");
			lobbybrowser = Content.Load<Texture2D>("Buttons/lobbybrowser");
			createlobby = Content.Load<Texture2D>("Buttons/createlobby");
			gamelobby = Content.Load<Texture2D>("Buttons/gamelobby");
			gameroom = Content.Load<Texture2D>("Buttons/gameroom");
			options = Content.Load<Texture2D>("Buttons/optionsbutton");
			///end added by Matthew Baldock

			guiRectangle = new Texture2D(graphicsDevice, 1, 1);
			guiRectangle.SetData(new[] { Color.White });


			starfield1 = Content.Load<Texture2D>("Backgrounds/starfield (800x600)");
			starfield2 = Content.Load<Texture2D>("Backgrounds/galaxy");
			starfield3 = Content.Load<Texture2D>("Backgrounds/beautifulbg");

			explosions = Content.Load<Texture2D>("Animations/fireballs");
			explosionsBig = Content.Load<Texture2D>("Animations/big fireballs with blur");


			//waterPlanet = Content.Load<Texture2D>("StellarObjects/waterplanet (256x256)");
			//moon = Content.Load<Texture2D>("StellarObjects/moon (115x117)");
			//asteroid = Content.Load<Texture2D>("StellarObjects/asteroid(56x56)");

			
			alpha_ship_texture = Content.Load<Texture2D>("Spaceships/alpha_ship");
            theta_ship_texture = Content.Load<Texture2D>("Spaceships/theta_ship");
            omega_ship_texture = Content.Load<Texture2D>("Spaceships/omega_ship");
			///Added by Matthew Baldock
			//zoid = Content.Load<Texture2D>("Spaceships/zoidship");
			//bug = Content.Load<Texture2D>("Spaceships/bug2 (16x16)");
			///Added by Matthew Baldock
			//galactusship = Content.Load<Texture2D>("Spaceships/galactuship.png");


			//winflag1 = Content.Load<Texture2D>("WinFlags/WinFlag1");

			laser = Content.Load<Texture2D>("Projectiles/laser");
			missile = Content.Load<Texture2D>("Projectiles/short missile (16x16)");
			moltenBullet = Content.Load<Texture2D>("Projectiles/molten bullet (6x8)");
			shield = Content.Load<Texture2D>("Animations/bubble shield sheet");


			shipMapIcon = Content.Load<Texture2D>("MapIcons/reticle (16x16)");


			pixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });
		}
	}
}
