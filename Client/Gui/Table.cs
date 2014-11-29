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
using Data_Structures;

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
        Rectangle[] columnMenuLine;
        public int RowsPerPage { get; set; }
        int RowRectOriginDataY;
        Rectangle TopRowLine;
        Rectangle SecondRowLine;
        Screen screen;
        int buttonsPerPage = 0;
        int Column1TextDataY;
        public int maxPage { get; set; }
        int[] pageFull = new int[MAX_AMOUNT];
        public Table(int column, string[] columnNames, int rowRectOriginX, int rowRectOriginY, int rowRectSizeY, int RowsBeforeScroll, int columnWidth, bool barEnabled, Screen mainScreen)
        {
            font = Game1.Assets.font;
            NumberOfColumns = column;
            currentPageAmount = RowsBeforeScroll;
            ColumnNames = new string[column];
            ColumnNames = columnNames;
            screen = mainScreen;
            RowsPerPage = RowsBeforeScroll;
            columnLine = new Rectangle[MAX_AMOUNT, NumberOfColumns+1];
            columnMenuLine = new Rectangle[NumberOfColumns+1];
            RowRectOriginY = rowRectOriginY;
            currentPage = 1;
            RowRectOriginX = rowRectOriginX;
            RowRectSizeY = rowRectSizeY;
            RowRectOriginDataY = RowRectSizeY + RowRectOriginY;
            Column1TextX = RowRectOriginX + 5;
            Column1TextY = RowRectOriginY + 3;
            Column1TextDataY = RowRectOriginDataY + 3;
            RowRectSizeX = column * columnWidth;
            rowRectangleTexture = Game1.Assets.guiRectangle;
            columnLineTexture = Game1.Assets.guiRectangle;
            rowLineTexture = Game1.Assets.guiRectangle;
            rowRectangle[0] = new Rectangle(rowRectOriginX, rowRectOriginY, RowRectSizeX, rowRectSizeY);
            browserRectangle = new Rectangle(rowRectOriginX, rowRectOriginY, RowRectSizeX, rowRectSizeY * (RowsBeforeScroll+1));
            rowLine[0] = new Rectangle(rowRectOriginX, rowRectOriginY, RowRectSizeX, 1);
            ColumnWidth = columnWidth;
            BarEnabled = barEnabled;
            maxPage = currentPage;
            
            rows = 0;
           

        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rowRectangleTexture, browserRectangle, Color.White);
            int maxPagePos = rows - ((currentPage-1) * RowsPerPage);
            
            if (BarEnabled == true)
            {
               
               TopRowLine = new Rectangle(RowRectOriginX, RowRectOriginY, RowRectSizeX, 1);
               SecondRowLine = new Rectangle(RowRectOriginX, RowRectOriginY + RowRectSizeY, RowRectSizeX, 1);
               for (int k = RowRectOriginX, c = 0; c < NumberOfColumns; c++, k += ColumnWidth)
               {
                columnMenuLine[c] = new Rectangle(k, RowRectOriginY, 1, RowRectSizeY);
                    
               }

               spriteBatch.Draw(rowLineTexture, TopRowLine, Color.Black);
               spriteBatch.Draw(rowLineTexture, SecondRowLine, Color.Black);

               
               for (int i = 0; i < NumberOfColumns; i++)
               {
                   spriteBatch.Draw(columnLineTexture, columnMenuLine[i], Color.Black);
               }
               for (int c = 0, k = 0; c < NumberOfColumns; c++, k += ColumnWidth)
               {
                   spriteBatch.DrawString(font, ColumnNames[c],
                       new Vector2(Column1TextX + k, Column1TextY), Color.Black, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.5f);
               }               
            }
            if (pageFull[currentPage] == 1)
            {
                maxPagePos = RowsPerPage;
            }
            for (int j = 0; j < maxPagePos; j++)
            {

                rowLine[j] = new Rectangle(RowRectOriginX, RowRectOriginDataY + (j * RowRectSizeY), RowRectSizeX, 1);
                rowLine[j + 1] = new Rectangle(RowRectOriginX, RowRectOriginDataY + ((j + 1) * RowRectSizeY), RowRectSizeX, 1);
                for (int k = RowRectOriginX, c = 0; c < NumberOfColumns; c++, k += ColumnWidth)
                {
                    columnLine[j, c] = new Rectangle(k, RowRectOriginDataY + (j * RowRectSizeY), 1, RowRectSizeY);
                }
                spriteBatch.Draw(rowLineTexture, rowLine[j], Color.Black);
                spriteBatch.Draw(rowLineTexture, rowLine[j + 1], Color.Black);
                for (int i = 0; i < NumberOfColumns; i++)
                {
                    spriteBatch.Draw(columnLineTexture, columnLine[j, i], Color.Black);
                }
                
               
                for (int i = 0, k = 0; i < NumberOfColumns; i++, k += ColumnWidth)
                {

                    spriteBatch.DrawString(font, RowArray[((currentPage-1)*(RowsPerPage))+j, i],
                        new Vector2(Column1TextX + k, Column1TextDataY + j * RowRectSizeY), Color.Black, 0.0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.5f);    
                }

                for (int i = (currentPage - 1) * RowsPerPage; i < maxPagePos + ((currentPage - 1) * RowsPerPage); i++)
                {
                        
                    
                }
                    
            }
                
                
               
                
      }





        public void CreateNewRow(string[] rowArray, int roomNumber, Game1 game)
        {
            if (rows == ((currentPage) * (RowsPerPage)) && rows != 0)
            {

                pageFull[currentPage] = 1;
                currentPage++;
                maxPage++;
                for (int i = 0; i < rows; i++)
                {
                    screen.Desktop.Children.Remove(joinButton[i]);
                }
                buttonsPerPage = 0;
            }
            for (int j = 0; j < NumberOfColumns; j++)
            {
                RowArray[rows, j] = rowArray[j];
            }
            joinButton[rows] = GuiHelper.CreateButton("Join", 275, buttonsPerPage * RowRectSizeY - 217, 30, 15);
            joinButton[rows].Pressed += delegate(object sender, EventArgs arguments)
            {
                Data requestRoomData = game.Communication.sendRoomJoinRequest(game.Player, roomNumber);
                if (requestRoomData != null)
                {
                    if (requestRoomData.Type == 10)
                    {
                        game.roomInfo = (RoomInfo)requestRoomData;
                        Console.WriteLine(game.roomInfo.RoomNumber);
                        game.EnterLobbyMenu();
                    }
                    else
                    {
                        game.EnterLobbyBrowserMenu();
                    }
                }
            };
            screen.Desktop.Children.Add(joinButton[rows]);
            buttonsPerPage++;
            rows++;
        }

        public void RemoveLastRow()
        {
            if (rows != 0)
            {
                rows--;
                buttonsPerPage--;
                screen.Desktop.Children.Remove(joinButton[rows]);

            }
            if (rows == ((currentPage-1) * (RowsPerPage)) && currentPage !=1)
            {
                
                currentPage--;
                maxPage--;
                pageFull[currentPage] = 0;
                buttonsPerPage = RowsPerPage;
                for (int i = rows - RowsPerPage; i < rows - RowsPerPage + buttonsPerPage; i++)
                {
                    screen.Desktop.Children.Add(joinButton[i]);
                }

            }

        }

        public void Clear()
        {
            for (int i = 0; i < maxPage; i++)
            {
                pageFull[i] = 0;
            }
            for (int i = 0; i < rows; i++)
            {
                screen.Desktop.Children.Remove(joinButton[i]);
            }
            currentPage = 1;
            maxPage = currentPage;
            rows = 0;
            buttonsPerPage = 0;
            

        }

        public void NextPage()
        {
            for (int i = 0; i < currentPage * RowsPerPage; i++)
            {
                screen.Desktop.Children.Remove(joinButton[i]);
            }
            currentPage++;
            for (int i = (currentPage - 1) * RowsPerPage; i < (currentPage) * RowsPerPage && joinButton[i] != null && i < rows; i++)
            {
                screen.Desktop.Children.Add(joinButton[i]);
            }

        }

        public void PrevPage()
        {
            for (int i = 0; i < currentPage * RowsPerPage; i++)
            {
                screen.Desktop.Children.Remove(joinButton[i]);
            }
            
            currentPage--;
            for (int i = (currentPage - 1) * RowsPerPage; i < (currentPage) * RowsPerPage && joinButton[i] != null && i < rows; i++)
            {
                screen.Desktop.Children.Add(joinButton[i]);
            }
        }
    }
}
