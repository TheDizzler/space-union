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
        private ScrollingBackground scroll;
        private Rectangle WhiteBackground;
        private Texture2D Background;

        public CreditsMenu(Game1 game)
        {
            this.game = game;
            scroll = new ScrollingBackground(Game1.Assets.background) { height = game.getScreenHeight(), width = game.getScreenWidth() };
            scroll.setPosition(new Vector2((int)0, (int)0));
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
            ProjectNameLabel = GuiHelper.CreateLabel("SPACE UNION - Set 4L Production", -200, -300, 30, 30);
            game.mainScreen.Desktop.Children.Add(ProjectNameLabel);

            ProjectManagersLabel = GuiHelper.CreateLabel("Project Managers", -300, -225, 30, 30);
            game.mainScreen.Desktop.Children.Add(ProjectManagersLabel);
            RobertPurdeyLabel2 = GuiHelper.CreateLabel("Robert Purdey (Lead Manager)", -100, -225, 30, 30);
            game.mainScreen.Desktop.Children.Add(RobertPurdeyLabel2);
            TristanGillonLabel2 = GuiHelper.CreateLabel("Tristan Gillon (Technical Manager)", -100, -200, 30, 30);
            game.mainScreen.Desktop.Children.Add(TristanGillonLabel2);

            GameLogicAssetsLabel = GuiHelper.CreateLabel("Game Logic And Assets", -300, -150, 30, 30);
            game.mainScreen.Desktop.Children.Add(GameLogicAssetsLabel);
            TroyCarefootLabel = GuiHelper.CreateLabel("Troy Carefoot (Leader)", -100, -150, 30, 30);
            game.mainScreen.Desktop.Children.Add(TroyCarefootLabel);
            TristanGillonLabel = GuiHelper.CreateLabel("Tristan Gillon (Leader)", -100, -125, 30, 30);
            game.mainScreen.Desktop.Children.Add(TristanGillonLabel);
            KonstantinBoyarinovLabel= GuiHelper.CreateLabel("Konstantin Boyarinov", -100, -100, 30, 30);
            game.mainScreen.Desktop.Children.Add(KonstantinBoyarinovLabel);
            KyleWiltonLabel = GuiHelper.CreateLabel("Kyle Wilton", -100, -75, 30, 30);
            game.mainScreen.Desktop.Children.Add(KyleWiltonLabel);
            MatthewBaldockLabel = GuiHelper.CreateLabel("Matthew Baldock", -100, -50, 30, 30);
            game.mainScreen.Desktop.Children.Add(MatthewBaldockLabel);
            StevenChenLabel = GuiHelper.CreateLabel("Steven Chen", -100, -25, 30, 30);
            game.mainScreen.Desktop.Children.Add(StevenChenLabel);
            

            DatabaseLabel = GuiHelper.CreateLabel("Database", -300, 25, 30, 30);
            game.mainScreen.Desktop.Children.Add(DatabaseLabel);
            MichaelGordonLabel = GuiHelper.CreateLabel("Michael Gordon (Leader)", -100, 25, 30, 30);
            game.mainScreen.Desktop.Children.Add(MichaelGordonLabel);
            RobertPurdeyLabel = GuiHelper.CreateLabel("Robert Purdey", -100, 50, 30, 30);
            game.mainScreen.Desktop.Children.Add(RobertPurdeyLabel);
            

            ClientServerLabel = GuiHelper.CreateLabel("Client/Server", -300, 100, 30, 30);
            game.mainScreen.Desktop.Children.Add(ClientServerLabel);
            MikhailDimchukLabel = GuiHelper.CreateLabel("Mikhail Dimchuk", -100, 100, 30, 30);
            game.mainScreen.Desktop.Children.Add(MikhailDimchukLabel);
            AndrewKimLabel = GuiHelper.CreateLabel("Andrew Kim", -100, 125, 30, 30);
            game.mainScreen.Desktop.Children.Add(AndrewKimLabel);

            WebDevLabel = GuiHelper.CreateLabel("Web Development", -300, 175, 30, 30);
            game.mainScreen.Desktop.Children.Add(WebDevLabel);
            KevinMcKeenLabel = GuiHelper.CreateLabel("Kevin McKeen (Leader)", -100, 175, 30, 30);
            game.mainScreen.Desktop.Children.Add(KevinMcKeenLabel);
            MiguelGatmaytanLabel = GuiHelper.CreateLabel("Miguel Gatmaytan", -100, 200, 30, 30);
            game.mainScreen.Desktop.Children.Add(MiguelGatmaytanLabel);    
            
            CreateMenuControls(game.mainScreen);
        }

        public void Update(GameTime gameTime)
        {
            scroll.update();
        }

        public void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - 150, (int)game.mainScreen.Height / 2 - 150, 300, 225);

            WhiteBackground = new Rectangle((int)game.mainScreen.Width / 2 - 325, (int)game.mainScreen.Height / 2 - 325, 650, 650);

            scroll.draw(spriteBatch);
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
            ButtonControl logoutButton = GuiHelper.CreateButton("Back", 0, 275, 70, 32);
            logoutButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMainMenu();
            };
            mainScreen.Desktop.Children.Add(logoutButton);
        }
    }
}