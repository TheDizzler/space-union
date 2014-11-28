using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Tools;


namespace SpaceUnionXNA.Weapons {
	public class Shield : Sprite {

		protected enum AnimationRow {
			row0,
			row1,
			row2,
			row3,
			row4,
			row5
		}

		AnimationRow row = AnimationRow.row0;

		public bool on = false;

		public Shield(Texture2D tex, Vector2 pos)
			: base(tex, pos) {

			// the size of each tile
			setSize(128, 128); // must be explicitly set for tile sheets

			AnimationClass anima = new AnimationClass();
			//anima.scale = 1.0f;
			addAnimation("row 0", 0, 10, anima.copy());
			addAnimation("row 1", 1, 10, anima.copy());
			addAnimation("row 2", 2, 10, anima.copy());
			addAnimation("row 3", 3, 10, anima.copy());
			addAnimation("row 4", 4, 10, anima.copy());
			addAnimation("row 5", 5, 10, anima.copy());

			frameLength = .02f;
			animation = "row 0";
			frameIndex = 0;
		}


		public void update(GameTime gameTime, Vector2 pos) {

			position = pos;

			frameTimeElapsed += (float) gameTime.ElapsedGameTime.TotalSeconds;

			if (frameTimeElapsed >= frameLength) {
				frameTimeElapsed = 0;
				if (frameIndex < animations[animation].frameCount - 1)
					++frameIndex;
				else {

					switch (row) {
						// change the animation to the next row and set the row to the current 
						case AnimationRow.row0:
							animation = "row 1";
							row = AnimationRow.row1;
							break;
						case AnimationRow.row1:
							animation = "row 2";
							row = AnimationRow.row2;
							break;
						case AnimationRow.row2:
							animation = "row 3";
							row = AnimationRow.row3;
							break;
						case AnimationRow.row3:
							animation = "row 4";
							row = AnimationRow.row4;
							break;
						case AnimationRow.row4:
							animation = "row 5";
							row = AnimationRow.row5;
							break;
						case AnimationRow.row5:
							animation = "row 0";
							row = AnimationRow.row0;
							break;
					}

					frameIndex = 0;
				}
			}
		}
	}
}
