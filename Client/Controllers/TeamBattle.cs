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

namespace SpaceUnionXNA.Controllers {
	class TeamBattle : GameplayScreen {
		TimeSpan teamBattleTime = new TimeSpan(0, 2, 5);

		public TeamBattle(Game1 game, SpriteBatch batch, Ship selectedship)
			: base(game, batch, selectedship) {


			gui = new TeamBattleGUI(game, selectedship, teamBattleTime);

		}

		public override void Update(GameTime gameTime) {

			if (((TeamBattleGUI) gui).timeOver == true) {
				//do nothing
			} else {
				base.Update(gameTime);
			}

		}
	}
}
