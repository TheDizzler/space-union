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
		TimeSpan teamBattleTime = new TimeSpan(0, 2, 1);
        Game1 game;
		public TeamBattle(Game1 game, SpriteBatch batch, Ship selectedship)
			: base(game, batch, selectedship) {

                this.game = game;
			gui = new TeamBattleGUI(game, selectedship, teamBattleTime, ships, inactiveShips);

		}

		public override void Update(GameTime gameTime) {
            if (((TeamBattleGUI)gui).getRedTeamKills() == 15 || ((TeamBattleGUI)gui).getBlueTeamKills() == 15)
            {
                game.EnterMainMenu();
            }
			if (((TeamBattleGUI) gui).timeOver == true) {
                game.EnterLobbyMenu();
			} else {
				base.Update(gameTime);
			}

		}
	}
}
