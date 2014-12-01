using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Nuclex.UserInterface.Controls.Desktop;
using Nuclex.Input;
using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls;
using SpaceUnionXNA;
using SpaceUnionXNA.Animations;

namespace SpaceUnionXNA.Controllers
{
	public class CreditsMenu
	{
		private Game1 game;
		
		private Rectangle WhiteBackground;
		private Texture2D Background;
		private Texture2D TexBanner;
		private Rectangle Banner;

		public CreditsMenu(Game1 game)
		{
			this.game = game;
			
			

			TexBanner = Game1.Assets.suCredits;
			Banner = new Rectangle((int)game.mainScreen.Width / 2 - UIConstants.SU_BANNER.X, 
				(int)game.mainScreen.Height / 2 - UIConstants.SU_BANNER.Y, 
				UIConstants.SU_BANNER.Width, UIConstants.SU_BANNER.Height);

			game.mainScreen.Desktop.Children.Clear(); //Clear the gui
			LabelControl KonstantinBoyarinovLabel;
			LabelControl MikhailDimchukLabel;
			LabelControl KyleWiltonLabel;
			LabelControl MiguelGatmaytanLabel;
			LabelControl RobertPurdeyLabel;
			LabelControl RobertPurdeyLabel2;
			LabelControl MichaelGordonLabel;
			LabelControl AndrewKimLabel;
			LabelControl TroyCarefootLabel;
			LabelControl KevinMcKeenLabel;
			LabelControl TristanGillonLabel;
			LabelControl TristanGillonLabel2;
			LabelControl MatthewBaldockLabel;
			LabelControl StevenChenLabel;
			Background = Game1.Assets.guiRectangle;
			LabelControl GameLogicAssetsLabel;
			LabelControl DatabaseLabel;
			LabelControl ClientServerLabel;
			LabelControl WebDevLabel;
			LabelControl ProjectManagersLabel;
			LabelControl ProjectNameLabel;
			ProjectNameLabel = GuiHelper.CreateLabel("SPACE UNION - Set 4L Production", -200, -230, 30, 30);
			game.mainScreen.Desktop.Children.Add(ProjectNameLabel);

			ProjectManagersLabel = GuiHelper.CreateLabel("Project Managers", -300, -200, 30, 30);
			game.mainScreen.Desktop.Children.Add(ProjectManagersLabel);
			RobertPurdeyLabel2 = GuiHelper.CreateLabel("Robert Purdey (Lead Manager)", -100, -200, 30, 30);
			game.mainScreen.Desktop.Children.Add(RobertPurdeyLabel2);
			TristanGillonLabel2 = GuiHelper.CreateLabel("Tristan Gillon (Technical Manager)", -100, -175, 30, 30);
			game.mainScreen.Desktop.Children.Add(TristanGillonLabel2);

			GameLogicAssetsLabel = GuiHelper.CreateLabel("Game Logic And Assets", -300, -125, 30, 30);
			game.mainScreen.Desktop.Children.Add(GameLogicAssetsLabel);
			TroyCarefootLabel = GuiHelper.CreateLabel("Troy Carefoot (Leader)", -100, -125, 30, 30);
			game.mainScreen.Desktop.Children.Add(TroyCarefootLabel);
			TristanGillonLabel = GuiHelper.CreateLabel("Tristan Gillon (Leader)", -100, -100, 30, 30);
			game.mainScreen.Desktop.Children.Add(TristanGillonLabel);
			KonstantinBoyarinovLabel= GuiHelper.CreateLabel("Konstantin Boyarinov", -100, -75, 30, 30);
			game.mainScreen.Desktop.Children.Add(KonstantinBoyarinovLabel);
			KyleWiltonLabel = GuiHelper.CreateLabel("Kyle Wilton", -100, -50, 30, 30);
			game.mainScreen.Desktop.Children.Add(KyleWiltonLabel);
			MatthewBaldockLabel = GuiHelper.CreateLabel("Matthew Baldock", -100, -25, 30, 30);
			game.mainScreen.Desktop.Children.Add(MatthewBaldockLabel);
			StevenChenLabel = GuiHelper.CreateLabel("Steven Chen", -100, 0, 30, 30);
			game.mainScreen.Desktop.Children.Add(StevenChenLabel);
			

			DatabaseLabel = GuiHelper.CreateLabel("Database", -300, 50, 30, 30);
			game.mainScreen.Desktop.Children.Add(DatabaseLabel);
			MichaelGordonLabel = GuiHelper.CreateLabel("Michael Gordon (Leader)", -100, 50, 30, 30);
			game.mainScreen.Desktop.Children.Add(MichaelGordonLabel);
			RobertPurdeyLabel = GuiHelper.CreateLabel("Robert Purdey", -100, 75, 30, 30);
			game.mainScreen.Desktop.Children.Add(RobertPurdeyLabel);
			

			ClientServerLabel = GuiHelper.CreateLabel("Client/Server", -300, 125, 30, 30);
			game.mainScreen.Desktop.Children.Add(ClientServerLabel);
			MikhailDimchukLabel = GuiHelper.CreateLabel("Mikhail Dimchuk", -100, 125, 30, 30);
			game.mainScreen.Desktop.Children.Add(MikhailDimchukLabel);
			AndrewKimLabel = GuiHelper.CreateLabel("Andrew Kim", -100, 150, 30, 30);
			game.mainScreen.Desktop.Children.Add(AndrewKimLabel);

			WebDevLabel = GuiHelper.CreateLabel("Web Development", -300, 200, 30, 30);
			game.mainScreen.Desktop.Children.Add(WebDevLabel);
			KevinMcKeenLabel = GuiHelper.CreateLabel("Kevin McKeen (Leader)", -100, 200, 30, 30);
			game.mainScreen.Desktop.Children.Add(KevinMcKeenLabel);
			MiguelGatmaytanLabel = GuiHelper.CreateLabel("Miguel Gatmaytan", -100, 225, 30, 30);
			game.mainScreen.Desktop.Children.Add(MiguelGatmaytanLabel);    
			
			CreateMenuControls(game.mainScreen);
		}

		public void Update(GameTime gameTime)
		{
			game.scroll.update();
		}

		public void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			//WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - 150, (int)game.mainScreen.Height / 2 - 150, 300, 225);

			WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - UIConstants.CREDIT_WHITE_BG.X,
				(int)game.mainScreen.Height / 2 - UIConstants.CREDIT_WHITE_BG.Y,
				UIConstants.CREDIT_WHITE_BG.Width, UIConstants.CREDIT_WHITE_BG.Height);

			game.scroll.draw(spriteBatch);
			spriteBatch.Draw(TexBanner, Banner, Color.White);
			spriteBatch.Draw(Background, WhiteBackground, Color.White * 0.75f);
			//spriteBatch.Draw(Background, WhiteBackground, Color.White * 0.75f);
			spriteBatch.End();
			game.gui_manager.Draw(gameTime);
		}

		private void CreateMenuControls(Screen mainScreen)
		{
			//Menu Title Label
			//LabelControl menuTitleLabel = GuiHelper.CreateLabel("Credits", -325, -325, 30, 30);
			//mainScreen.Desktop.Children.Add(menuTitleLabel);
			
			//Logout Button.
			ButtonControl logoutButton = GuiHelper.CreateButton("Back",
				UIConstants.CREDIT_BACK_BTN.X, UIConstants.CREDIT_BACK_BTN.Y,
				UIConstants.CREDIT_BACK_BTN.Width, UIConstants.CREDIT_BACK_BTN.Height);
			logoutButton.Pressed += delegate(object sender, EventArgs arguments)
			{
				game.EnterMainMenu();
			};
			mainScreen.Desktop.Children.Add(logoutButton);
		}
	}
}