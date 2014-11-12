﻿using Nuclex.UserInterface;
using Nuclex.UserInterface.Controls.Desktop;
using Nuclex.Input;
using Nuclex.UserInterface.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA.Tools;

namespace SpaceMenus
{
    class GuiHelper
    {
        /// <summary>
        /// Takes the center of the button and places it at the center of the client, 
        /// makes the center of the client coordinate (0, 0)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="offSetX"></param>
        /// <param name="offSetY"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="center"></param>
        /// <returns></returns>
        public static ButtonControl CreateButton(String text, int offSetX, int offSetY, int width, int height, bool center = true, float XPos = 0.0f, float YPos = 0.0f) 
        {
            if (center)
            {
                XPos = 0.5f;
                YPos = 0.5f;
                offSetX += -1 * width / 2;
                offSetY += -1 * height / 2;
            }
            ButtonControl button = new ButtonControl();
            button.Text = text;
            button.Bounds = new UniRectangle(
                new UniScalar(XPos, offSetX), new UniScalar(YPos, offSetY), width, height);
            return button;
        }

        public static LabelControl CreateLabel(String text, int offSetX, int offSetY, int width, int height, bool center = true, float XPos = 0.0f, float YPos = 0.0f)
        {
            if (center)
            {
                XPos = 0.5f;
                YPos = 0.5f;
                offSetX = offSetX + (-1 * width / 2);
                offSetY = offSetY + (-1 * height / 2);
            }
            LabelControl label = new LabelControl();
            label.Text = text;
            label.Bounds = new UniRectangle(
                new UniScalar(XPos, offSetX), new UniScalar(YPos, offSetY), width, height);
            return label;
        }

        public static InputControl CreateInput(String text, int offSetX, int offSetY, int width, int height, bool center = true, float XPos = 0.0f, float YPos = 0.0f)
        {
            if (center)
            {
                XPos = 0.5f;
                YPos = 0.5f;
                offSetX = offSetX + (-1 * width / 2);
                offSetY = offSetY + (-1 * height / 2);
            }
            InputControl input = new InputControl();
            input.Text = text;
            input.Bounds = new UniRectangle(
                new UniScalar(XPos, offSetX), new UniScalar(YPos, offSetY), width, height);
            return input;
        }

        public static PasswordInputControl CreatePasswordInput(int offSetX, int offSetY, int width, int height, bool center = true, float XPos = 0.0f, float YPos = 0.0f)
        {
            if (center)
            {
                XPos = 0.5f;
                YPos = 0.5f;
                offSetX = offSetX + (-1 * width / 2);
                offSetY = offSetY + (-1 * height / 2);
            }
            PasswordInputControl input = new PasswordInputControl();
            input.Bounds = new UniRectangle(
                new UniScalar(XPos, offSetX), new UniScalar(YPos, offSetY), width, height);
            return input;
        }

        public static UniRectangle CenterBound(int offSetX, int offSetY, int width, int height, bool center = true, float XPos = 0.0f, float YPos = 0.0f)
        {
            if (center)
            {
                XPos = 0.5f;
                YPos = 0.5f;
                offSetX = offSetX + (-1 * width / 2);
                offSetY = offSetY + (-1 * height / 2);
            }
            UniRectangle bounds = new UniRectangle(
                new UniScalar(XPos, offSetX), new UniScalar(YPos, offSetY), width, height);
            return bounds;
        }

        public static OptionControl CreateOption(String text, int offSetX, int offSetY, int width, int height, bool center = true, float XPos = 0.0f, float YPos = 0.0f)
        {
            if (center)
            {
                XPos = 0.5f;
                YPos = 0.5f;
                offSetX = offSetX + (-1 * width / 2);
                offSetY = offSetY + (-1 * height / 2);
            }
            OptionControl option = new OptionControl();
            option.Text = text;
            option.Bounds = new UniRectangle(
                new UniScalar(XPos, offSetX), new UniScalar(YPos, offSetY), width, height);
            return option;
        }
        

        public static UniRectangle MENU_TITLE_LABEL = new UniRectangle(50.0f, 0.0f, 110.0f, 24.0f);

    }
}
