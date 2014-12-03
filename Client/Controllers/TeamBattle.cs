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
		TimeSpan teamBattleTime = new TimeSpan(0, 0, 5);
        Game1 game;
        private bool first = true;

		public TeamBattle(Game1 game, SpriteBatch batch, Ship selectedship)
			: base(game, batch, selectedship) {

            this.game = game;
            gui = new TeamBattleGUI(game, selectedship, teamBattleTime, ships, inactiveShips);

		}

		public override void Update(GameTime gameTime) {
            if (first)
            {
                base.Update(gameTime);
                first = false;
                return;
            }
            if (!((TeamBattleGUI)gui).countedDown)
            {
                gui.update(gameTime, quadTree);
                return;
            }
            if (((TeamBattleGUI)gui).getRedTeamKills() == 15 || ((TeamBattleGUI)gui).getBlueTeamKills() == 15)
            {
                game.EndMatch();
                game.EnterMainMenu();
            }
			if (((TeamBattleGUI) gui).timeOver == true) {
                game.EndMatch();
                game.EnterMainMenu();
			} else {
				base.Update(gameTime);
			}

		}
	}
}
