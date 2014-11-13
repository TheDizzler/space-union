using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceMenus
{
    class GuiHelper
    {
        public static ButtonControl CreateButton(String text,int offsetX, int offsetY, int width, int height) 
        {
            ButtonControl button = new ButtonControl();
            button.Text = text;
            button.Bounds = new UniRectangle(
                        new UniScalar(1.0f, (float)(offsetX)), new UniScalar(1.0f, (float)(offsetY)), width, height
            );
            return button;
        }

        public static UniRectangle MENU_TITLE_LABEL = new UniRectangle(50.0f, 0.0f, 110.0f, 24.0f);
    }
}
