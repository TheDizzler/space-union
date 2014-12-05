using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.StellarObjects;
using SpaceUnionXNA.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceUnionXNA.Tools {
	class TeamBattleGUI : GUI {
		//private Game1 game;
		private TimeSpan teamBattleTime;
		//private Texture2D gameOver;
		private Rectangle rect;
		//private Ship playerShip;
		private BaseButton button = new BaseButton(Game1.Assets.playButton);
		private List<Ship> ships;
		//private List<Ship> inactiveships;
		private int redTeamKills = 0;
		private int blueTeamKills = 0;
		public bool timeOver = false;
		public bool countedDown = false;
		public TimeSpan countDown = new TimeSpan(0, 0, 2);

		public int getRedTeamKills() {
			return redTeamKills;
		}

		public int getBlueTeamKills() {
			return blueTeamKills;
		}

		public TeamBattleGUI(Game1 game, Ship ship, TimeSpan time, List<Ship> ships)
			: base(game, ship) {
			//this.game = game;
			teamBattleTime = time;
			//gameOver = Game1.Assets.guiRectangle;
			rect = new Rectangle(50, 50, game.getScreenWidth() - 100, game.getScreenHeight() - 100);
			//playerShip = ship;
			playerShip.redTeam = true;
			this.ships = ships;
			//this.inactiveships = inactiveships;
		}


		public override void update(GameTime gameTime, QuadTree quadTree) {
			redTeamKills = 0;
			blueTeamKills = 0;
			if (!countedDown) {
				countDown -= gameTime.ElapsedGameTime;
				if (countDown.Seconds <= 0) {
					countedDown = true;
				}
				return;
			}
			if (teamBattleTime <= TimeSpan.Zero) {

				timeOver = true;
				MouseState mouseState = Mouse.GetState();
				button.update(mouseState);

				if (button.isClicked == true) {
					button.isClicked = false;
					game.EnterMainMenu();
					button.isClicked = false;
				}
			} else {

				base.update(gameTime, quadTree);
				teamBattleTime -= gameTime.ElapsedGameTime;
				foreach (Ship ship in ships) {
					if (ship.redTeam == true) {
						redTeamKills += ship.kills;
					}
					if (ship.blueTeam == true) {
						blueTeamKills += ship.kills;
					}
				}
				//foreach (Ship ship in inactiveships)
				//{
				//	if (ship.redTeam == true)
				//	{
				//		redTeamKills += ship.kills;
				//	}
				//	if (ship.blueTeam == true)
				//	{
				//		blueTeamKills += ship.kills;
				//	}
				//}

			}
		}


		public override void draw(SpriteBatch spriteBatch) {

			spriteBatch.DrawString(font, "Red Team:  " + redTeamKills,
				new Vector2(10, 10), Color.Red, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
			spriteBatch.DrawString(font, "Blue Team: " + blueTeamKills,
				new Vector2(10, 30), Color.Blue, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
			spriteBatch.DrawString(font, "Time: " + teamBattleTime.ToString(@"hh\:mm\:ss"),
				new Vector2(game.getScreenWidth() - 200, 10), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
			if (!countedDown) {
				spriteBatch.DrawString(font, countDown.Seconds.ToString(),
						new Vector2(game.getScreenWidth() / 2, game.getScreenHeight() / 2), Color.SpringGreen, 0.0f, Vector2.Zero, 5f, SpriteEffects.None, 0.5f);
			}
			if (timeOver == true) {

				spriteBatch.Draw(guiRectangle, rect, Color.DarkSlateBlue);
				spriteBatch.DrawString(font, "Game Over",
					new Vector2(200, 200), Color.DarkGoldenrod, 0.0f, Vector2.Zero, 5f, SpriteEffects.None, 0.5f);
				spriteBatch.DrawString(font, "Your Kills: " + playerShip.kills,
					new Vector2(210, 300), Color.Red, 0.0f, Vector2.Zero, 3.5f, SpriteEffects.None, 0.5f);
				spriteBatch.DrawString(font, "Your Deaths: " + playerShip.deaths,
					new Vector2(210, 400), Color.Blue, 0.0f, Vector2.Zero, 3.5f, SpriteEffects.None, 0.5f);
				button = new BaseButton(Game1.Assets.playButton);

				button.setPosition(new Vector2(210, 500));
				button.draw(spriteBatch);
			} else {
				base.draw(spriteBatch);
			}
		}
	}
}
