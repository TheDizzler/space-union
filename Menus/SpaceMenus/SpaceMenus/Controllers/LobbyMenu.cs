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

namespace SpaceMenus
{
    public class LobbyMenu
    {
        private Game1 game;
        public String lobbyTitle;

        public LobbyMenu(Game1 game, String title)
        {
            this.game = game;
            game.mainScreen.Desktop.Children.Clear(); //Clear the gui
            lobbyTitle = title;
            CreateMenuControls(game.mainScreen);
            
        }

        public void Update(GameTime gameTime)
        {

        }

        public void DrawMenu(GameTime gameTime)
        {
            game.gui_manager.Draw(gameTime);
        }

        private void CreateMenuControls(Screen mainScreen)
        {
            //Menu Name Label
            LabelControl menuNameLabel = new LabelControl();
            menuNameLabel.Text = "Lobby: " + lobbyTitle;
            menuNameLabel.Bounds = GuiHelper.MENU_TITLE_LABEL;
            mainScreen.Desktop.Children.Add(menuNameLabel);

            //Choose Chip Label
            LabelControl chooseShipLabel = new LabelControl();
            chooseShipLabel.Text = "Choose Your Ship";
            chooseShipLabel.Bounds = new UniRectangle(10.0f, 100.0f, 120.0f, 16.0f);
            mainScreen.Desktop.Children.Add(chooseShipLabel);

            //Choosing Ship Options
            ChoiceControl shipChoice_1 = new ChoiceControl();
            shipChoice_1.Bounds = new UniRectangle(10.0f, 125.0f, 120.0f, 16.0f);
            shipChoice_1.Text = "Alpha Class";
            mainScreen.Desktop.Children.Add(shipChoice_1);

            ChoiceControl shipChoice_2 = new ChoiceControl();
            shipChoice_2.Bounds = new UniRectangle(10.0f, 150.0f, 120.0f, 16.0f);
            shipChoice_2.Text = "Theta Class";
            mainScreen.Desktop.Children.Add(shipChoice_2);

            ChoiceControl shipChoice_3 = new ChoiceControl();
            shipChoice_3.Bounds = new UniRectangle(10.0f, 175.0f, 120.0f, 16.0f);
            shipChoice_3.Text = "Omega Class";
            mainScreen.Desktop.Children.Add(shipChoice_3);

            //Ready up Label
            LabelControl readyUpLabel = new LabelControl();
            readyUpLabel.Text = "I'm Ready!";
            readyUpLabel.Bounds = new UniRectangle(115.0f, 300.0f, 110.0f, 24.0f);
            mainScreen.Desktop.Children.Add(readyUpLabel);

            //Ready Up Button.
            OptionControl readyUpButton = new OptionControl();
            readyUpButton.Bounds = new UniRectangle(
                        new UniScalar(1.0f, -600.0f), new UniScalar(1.0f, -85.0f), 50, 50
            );
            mainScreen.Desktop.Children.Add(readyUpButton);

            //Start Game Button
            ButtonControl startGameButton = GuiHelper.CreateButton("Start Game", -400, -75, 100, 60);
            startGameButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterMainMenu();
            };
            mainScreen.Desktop.Children.Add(startGameButton);

            //Cancel Button
            ButtonControl cancelGameButton = GuiHelper.CreateButton("Cancel", -200, -75, 100, 60);
            cancelGameButton.Pressed += delegate(object sender, EventArgs arguments)
            {
                game.EnterLobbyBrowserMenu();
            };
            mainScreen.Desktop.Children.Add(cancelGameButton);

        }
    }
}