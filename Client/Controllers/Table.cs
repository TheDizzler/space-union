using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnion.Ships;
using SpaceUnion.Tools;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using SpaceUnion.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceUnion.Controllers
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
        int RowRectSizeX = 500;
        Rectangle browserRectangle;
        int Column1TextX, Column1TextY;
        string[] ColumnNames;
        string[,] RowArray = new string[MAX_AMOUNT, MAX_AMOUNT];
        SpriteFont font;
        int ColumnWidth;
        
        public Table(int column, string[] columnNames, int rowRectOriginX, int rowRectOriginY, int rowRectSizeY, int RowsBeforeScroll, int columnWidth)
        {
            font = Game1.Assets.font;
            NumberOfColumns = column;
            ColumnNames = new string[column];
            ColumnNames = columnNames;
            
            columnLine = new Rectangle[MAX_AMOUNT, NumberOfColumns+1];
            RowRectOriginY = rowRectOriginY;
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
            ColumnWidth =columnWidth;
            rows = 1;
            
        }

        public void drawTable(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rowRectangleTexture, browserRectangle, Color.White);
            for (int j = 0; j < rows; j++)
            {
                
                rowLine[j] = new Rectangle(RowRectOriginX, RowRectOriginY + (j * RowRectSizeY), RowRectSizeX, 1);
                rowLine[j + 1] = new Rectangle(RowRectOriginX, RowRectOriginY + ((j + 1) * RowRectSizeY), RowRectSizeX, 1);
                for (int k = 100, c = 0;  c < NumberOfColumns; c++, k += ColumnWidth)
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
                if (j == 0)
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

                        spriteBatch.DrawString(font, RowArray[j, i],
                            new Vector2(Column1TextX + k, Column1TextY + j * RowRectSizeY), Color.Black, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.5f);
                    }
                }

            }

        }

        public void CreateNewRow(string[] rowArray)
        {
                
                for (int j = 0; j < NumberOfColumns; j++)
                {
                    RowArray[rows,j] = rowArray[j]; 

                }


            
            rows++;



        }

        public void RemoveLastRow()
        {
            if (rows != 1)
            {
                rows--;

            }

        }
    }
}
