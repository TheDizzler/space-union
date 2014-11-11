using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.GamerServices;
using SpaceUnionXNA.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceUnionXNA;
using Nuclex.UserInterface.Controls.Desktop;
using Nuclex.UserInterface;

namespace SpaceUnionXNA.Gui
{
    class Table
    {
        static int MAX_AMOUNT = 1024;
        Texture2D columnLineTexture;
        Rectangle[] rowLine = new Rectangle[MAX_AMOUNT + 1];
        Rectangle[,] columnLine;
        int NumberOfColumns;
        Texture2D rowLineTexture;
        Texture2D rowRectangleTexture;
        Rectangle[] rowRectangle = new Rectangle[MAX_AMOUNT];
        int rows;
        int RowRectOriginY = 125;
        int RowRectOriginX = 100;
        int RowRectSizeY = 15;
        public int currentPage { get; set; }
        int RowRectSizeX = 500;
        Rectangle browserRectangle;
        int Column1TextX, Column1TextY;
        string[] ColumnNames;
        string[,] RowArray = new string[MAX_AMOUNT, MAX_AMOUNT];
        SpriteFont font;
        ButtonControl[] joinButton = new ButtonControl[MAX_AMOUNT];
        int ColumnWidth;
        int currentPageAmount;
        bool BarEnabled;
        public int RowsPerPage { get; set; }
        int maxpages = 0;
        int ButtonAmount;
        int[] pageFull = new int[MAX_AMOUNT];
        public Table(int column, string[] columnNames, int rowRectOriginX, int rowRectOriginY, int rowRectSizeY, int RowsBeforeScroll, int columnWidth, bool barEnabled)
        {
            font = Game1.Assets.font;
            NumberOfColumns = column;
            currentPageAmount = RowsBeforeScroll;
            ColumnNames = new string[column];
            ColumnNames = columnNames;
            RowsPerPage = RowsBeforeScroll;
            columnLine = new Rectangle[MAX_AMOUNT, NumberOfColumns + 1];
            RowRectOriginY = rowRectOriginY;
            currentPage = 1;
            RowRectOriginX = rowRectOriginX;
            RowRectSizeY = rowRectSizeY;
            Column1TextX = RowRectOriginX + 5;
            Column1TextY = RowRectOriginY + 3;
            RowRectSizeX = column * columnWidth;
            
            rowRectangleTexture = Game1.Assets.guiRectangle;
            columnLineTexture = Game1.Assets.guiRectangle;
            rowLineTexture = Game1.Assets.guiRectangle;
            rowRectangle[0] = new Rectangle(rowRectOriginX, rowRectOriginY, RowRectSizeX, rowRectSizeY);
            browserRectangle = new Rectangle(rowRectOriginX, rowRectOriginY, RowRectSizeX, rowRectSizeY * RowsBeforeScroll);
            rowLine[0] = new Rectangle(rowRectOriginX, rowRectOriginY, RowRectSizeX, 1);
            ColumnWidth = columnWidth;
            BarEnabled = barEnabled;
            if (barEnabled == true)
            {
                rows = 1;
            }

        }

        public void draw(SpriteBatch spriteBatch, Screen mainScreen)
        {
            spriteBatch.Draw(rowRectangleTexture, browserRectangle, Color.White);
            int maxPagePos = (rows % (RowsPerPage+1));


            if (maxPagePos == 1)
            {
                maxPagePos = 1;
                pageFull[currentPage] = 1;
                /*for (int i = currentPage * RowsPerPage; i < (currentPage * RowsPerPage + 1); i++)
                {
                    mainScreen.Desktop.Children.Remove(joinButton[i]);
                }*/
            }
            

            if (pageFull[currentPage] == 1)
            {
                maxPagePos = RowsPerPage;
            }
            for (int j = 0; j < maxPagePos; j++)
            {

                rowLine[j] = new Rectangle(RowRectOriginX, RowRectOriginY + (j * RowRectSizeY), RowRectSizeX, 1);
                rowLine[j + 1] = new Rectangle(RowRectOriginX, RowRectOriginY + ((j + 1) * RowRectSizeY), RowRectSizeX, 1);
                for (int k = ColumnWidth, c = 0; c < NumberOfColumns; c++, k += ColumnWidth)
                {
                    columnLine[j, c] = new Rectangle(k, RowRectOriginY + (j * RowRectSizeY), 1, RowRectSizeY);
                }
                spriteBatch.Draw(rowRectangleTexture, rowRectangle[j], Color.White);
                spriteBatch.Draw(rowLineTexture, rowLine[j], Color.Black);
                spriteBatch.Draw(rowLineTexture, rowLine[j + 1], Color.Black);
                for (int i = 0; i < NumberOfColumns; i++)
                {
                    spriteBatch.Draw(columnLineTexture, columnLine[j, i], Color.Black);
                }
                /*if (j == (RowsPerPage - 2))
                {
                    j = 0;
                }*/
                if (j == 0 && BarEnabled == true)
                {
                    for (int c = 0, k = 0; c < NumberOfColumns; c++, k += ColumnWidth)
                    {
                        spriteBatch.DrawString(font, ColumnNames[c],
                            new Vector2(Column1TextX + k, Column1TextY), Color.Black, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.5f);
                    }
                }
                else
                {
                    for (int i = 0, k = 0; i < NumberOfColumns; i++, k += ColumnWidth)
                    {

                        spriteBatch.DrawString(font, RowArray[((currentPage-1)*(RowsPerPage+1))+j, i],
                            new Vector2(Column1TextX + k, Column1TextY + j * RowRectSizeY), Color.Black, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.5f);

                        
                    }

                    /*for (int i = currentPage * RowsPerPage; i < maxPagePos - 1 + (currentPage * RowsPerPage); i++)
                    {
                        joinButton[i] = GuiHelper.CreateButton("Join", -(int)(Column1TextX + (ColumnWidth * NumberOfColumns) - mainScreen.Width / 2 - ColumnWidth / 2), -(int)(Column1TextY * 1.2 - j * RowRectSizeY + mainScreen.Height / 2), 30, 15);
                        joinButton[i].Pressed += delegate(object sender, EventArgs arguments)
                        {

                        };
                        mainScreen.Desktop.Children.Add(joinButton[i]);
                    }*/
                    
                }
                
                
               
                
            }

            

        }

        public void CreateNewRow(string[] rowArray)
        {

            for (int j = 0; j < NumberOfColumns; j++)
            {
                RowArray[rows, j] = rowArray[j];

            }
            
            rows++;
            if (rows == (currentPage * (RowsPerPage+1)))
            {
                rows++;
                currentPage++;
            }
            




        }

        public void RemoveLastRow()
        {
            if ((rows != 1 && BarEnabled == true) || (rows != 0 && BarEnabled == false))
            {
                rows--;

            }

        }
    }
}
