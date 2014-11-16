using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SpaceUnionXNA
{
    public class PasswordInputControl : Nuclex.UserInterface.Controls.Desktop.InputControl
    {
        private String enteredText = "";
        public event EventHandler Activated;
        public PasswordInputControl()
        {

        }
        /// <summary>
        /// Gets the string from the textbox
        /// </summary>
        /// <returns></returns>
        public String GetText()
        {
            return enteredText;
        }


        protected override void OnCharacterEntered(char character)
        {
            if (character == '\n' || character == '\r')
            {
                OnActivate();
                return;
            }
            if (this.HasFocus &&
               (!Char.IsWhiteSpace(character) || !Char.IsPunctuation(character) || !Char.IsSeparator(character))
                && character != '\b')
            {
                enteredText += character;
                this.Text = String.Empty;
                for (int i = 0; i < enteredText.Length; i++)
                    this.Text += "*";
                this.CaretPosition = this.Text.Length;
            }
            else if (character == '\b')
            {
                if (enteredText.Length >= 1)
                {
                    enteredText = RemoveIndex(enteredText.ToCharArray(), this.CaretPosition);
                    this.Text = string.Empty;
                    for (int i = 0; i < enteredText.Length; i++)
                    {
                        this.Text += "*";
                    }
                    this.CaretPosition = this.Text.Length;
                }
            }
        }
        /*
        protected override void OnKeyReleased(Microsoft.Xna.Framework.Input.Keys key)
        {
            if (!HasFocus) return;
            if (!String.IsNullOrEmpty(enteredText)
                && (Keys.Back == key || Keys.Delete == key))
            {
                int oldIndex = this.CaretPosition;
                enteredText = RemoveIndex(enteredText.ToCharArray(), this.CaretPosition);
                this.Text = string.Empty;
                for (int i = 0; i < enteredText.Length; i++)
                    this.Text += "*";
                this.CaretPosition = oldIndex;
                return;
            }
        }*/

        private String RemoveIndex(Char[] IndicesArray, int RemoveAt)
        {
            Char[] newIndicesArray = new char[IndicesArray.Length - 1];

            int i = 0;
            int j = 0;
            while (i < IndicesArray.Length)
            {
                if (i != RemoveAt)
                {
                    newIndicesArray[j] = IndicesArray[i];
                    j++;
                }
                i++;
            }

            return new string(newIndicesArray);
        }


        /// <summary>
        /// Call our delegate and clear text
        /// </summary>
        protected void OnActivate()
        {
            if (Activated != null)
            {
                Activated(this, EventArgs.Empty);
                Text = string.Empty;

            }
        }
    }
}