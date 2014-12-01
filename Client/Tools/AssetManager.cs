using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

///Edited by Matthew Baldock
namespace SpaceUnionXNA.Tools {

	/// <summary>
	/// A class to hold textures, animations, audio, etc..
	/// </summary>
	public class AssetManager {

		public ContentManager Content;
		public GraphicsDevice graphicsDevice;

		// misc graphics
		public SpriteFont font;
		public Texture2D winflag1;

		public Texture2D guiRectangle;
		public Texture2D playButton;
		/// <summary>
		/// Ship selection button texture
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D shipselection;
		/// <summary>
		/// Confirm button texture
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D confirm;
		/// <summary>
		/// Lobby Options button texture
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D lobbyoptions;
		/// <summary>
		/// Lobby Browser button texture
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D lobbybrowser;
		/// <summary>
		/// Create Lobby button texture
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D createlobby;
		/// <summary>
		/// Game Lobby button texture
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D gamelobby;
		/// <summary>
		/// Game Room button texture
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D gameroom;
		public Texture2D options;
		public Texture2D createLobby2;
		public Texture2D browseLobby;
		public Texture2D spaceUnion;
        public Texture2D suCredits;
        public Texture2D suMultiBrowse;
        public Texture2D suMultiCreate;
        public Texture2D suMultiplayer;
        public Texture2D suOption;
        public Texture2D suOptionKeys;
        public Texture2D suBanner;
        public Texture2D suMultiLobby;


		// bgs and doodads
		public Texture2D background;
		public Texture2D starfield1; // stackable background layer
		public Texture2D starfield2; // stackable background layer
		public Texture2D starfield3; // stackable background layer

		public Texture2D explosions;
		public Texture2D explosionsBig;

		// space objects
		/// <summary>
		/// Planetoids created by Tristan
		/// </summary>
		public Texture2D earth;
		public Texture2D waterPlanet;
		public Texture2D moon;
		public Texture2D asteroid;

		// ships and platforms
		public Texture2D shuttle;
		public Texture2D spaceShipTest;

		/// <summary>
		/// Ufo, scout and Lobstar created by Tristan
		/// </summary>
		public Texture2D ufo;
		public Texture2D scout;
		public Texture2D lobstar;
		/// <summary>
		/// Zoid ship texture
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D zoid;

		/// <summary>
		/// Galactus Ship Texture
		/// Created by Matthew Baldock
		/// </summary>
		public Texture2D galactusship;
		public Texture2D bug;



		// weapons systems
		public Texture2D laser;
		public Texture2D missile;
		public Texture2D homingMissile;
		public Texture2D shield;
		public Texture2D moltenBullet;
		public Texture2D redLaser;

		// radar icons
		public Texture2D shipMapIcon;
		public  Texture2D pixel;

		/// <summary>
		/// Created by Tristan
		/// </summary>
		public SoundEffect laserbolt0, laserbolt1, laserbolt2, laserbolt3;
		public SoundEffect klaxxon;
		public SoundEffect torpedo;
		public  SoundEffect plasmaburst;
		public List<SoundEffect> explosionsSFX;
		

		public Song titleSong;
		/// <summary>
		/// Written by Matthew, arranged by Tristan
		/// </summary>
		public Song battleSong;
		

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
            createLobby2 = Content.Load<Texture2D>("Buttons/Create_Lobby");
            browseLobby = Content.Load<Texture2D>("Buttons/Browse_Lobby");
            suBanner = Content.Load<Texture2D>("Buttons/Space-Union_Banner");
            spaceUnion = Content.Load<Texture2D>("Buttons/Space-Union");
            suCredits = Content.Load<Texture2D>("Buttons/SU_Credits");
            suMultiBrowse = Content.Load<Texture2D>("Buttons/SU_M_BrowseLobby");
            suMultiCreate = Content.Load<Texture2D>("Buttons/SU_M_CreateLobby");
            suMultiplayer = Content.Load<Texture2D>("Buttons/SU_Multiplayer");
            suOption = Content.Load<Texture2D>("Buttons/SU_Options");
            suOptionKeys = Content.Load<Texture2D>("Buttons/SU_O_KeyBindings");
            suMultiLobby = Content.Load<Texture2D>("Buttons/SU_M_Lobby");

			///end added by Matthew Baldock

			guiRectangle = new Texture2D(graphicsDevice, 1, 1);
			guiRectangle.SetData(new[] { Color.White });


			starfield1 = Content.Load<Texture2D>("Backgrounds/starfield (800x600)");
			starfield2 = Content.Load<Texture2D>("Backgrounds/galaxy");
			starfield3 = Content.Load<Texture2D>("Backgrounds/beautifulbg");

			explosions = Content.Load<Texture2D>("Animations/fireballs");
			explosionsBig = Content.Load<Texture2D>("Animations/big fireballs with blur");


			waterPlanet = Content.Load<Texture2D>("StellarObjects/waterplanet (256x256)");
			moon = Content.Load<Texture2D>("StellarObjects/moon (115x117)");
			asteroid = Content.Load<Texture2D>("StellarObjects/rock (32x32)");

			spaceShipTest = Content.Load<Texture2D>("Spaceships/spaceshiptest");
			ufo = Content.Load<Texture2D>("Spaceships/UFO (39x39)");
			scout = Content.Load<Texture2D>("Spaceships/scoutship (64x64)");
			lobstar = Content.Load<Texture2D>("Spaceships/rocket (64x64)");
			///Added by Matthew Baldock
			zoid = Content.Load<Texture2D>("Spaceships/zoidship");
			bug = Content.Load<Texture2D>("Spaceships/bug (16x16)");
			///Added by Matthew Baldock
			galactusship = Content.Load<Texture2D>("Spaceships/galactuship");


			winflag1 = Content.Load<Texture2D>("WinFlags/WinFlag1");

			laser = Content.Load<Texture2D>("Projectiles/laser");
			missile = Content.Load<Texture2D>("Projectiles/short missile (16x16)");
			homingMissile = Content.Load<Texture2D>("Projectiles/homing missile smaller");
			moltenBullet = Content.Load<Texture2D>("Projectiles/molten bullet (6x8)");
			shield = Content.Load<Texture2D>("Animations/bubble shield sheet");
			redLaser = Content.Load<Texture2D>("Animations/redlaserbeam (25x11)");

			shipMapIcon = Content.Load<Texture2D>("MapIcons/reticle (16x16)");


			pixel = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
			pixel.SetData(new[] { Color.White });


			laserbolt0 = Content.Load<SoundEffect>("SFX/weapons/Laser High 0");
			laserbolt1 = Content.Load<SoundEffect>("SFX/weapons/Laser High 1");
			laserbolt2 = Content.Load<SoundEffect>("SFX/weapons/Laser High 2");
			laserbolt3 = Content.Load<SoundEffect>("SFX/weapons/Laser High 3");

			plasmaburst = Content.Load<SoundEffect>("SFX/weapons/plasma burst");

			torpedo = Content.Load<SoundEffect>("SFX/weapons/torpedo");

			klaxxon = Content.Load<SoundEffect>("SFX/miscl/klaxxon01");
			
			explosionsSFX = new List<SoundEffect>();
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion 11"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion 12"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion 13"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion 14"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion 15"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion deep 01"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion deep 02"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion double hit 01"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion double hit 02"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion high"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion low 01"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion low 02"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion quickdouble 01"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion quickdouble 02"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/explosion short"));
			explosionsSFX.Add(Content.Load<SoundEffect>("SFX/explosions/multi-explosion"));
			titleSong = Content.Load<Song>("Music/mystery");
			battleSong = Content.Load<Song>("Music/battle stations");
		}
	}
}
