using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceUnionXNA.Ships;
using SpaceUnionXNA.Weapons;
using SpaceUnionXNA.Tools;
using SpaceUnionXNA.Animations;
using System.Runtime.InteropServices;

namespace SpaceUnionXNA.Controllers
{

	[StructLayout(LayoutKind.Sequential)]
	internal struct DEVMODE
	{
		private const int CCHDEVICENAME = 0x20;
		private const int CCHFORMNAME = 0x20;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
		public string dmDeviceName;
		public short dmSpecVersion;
		public short dmDriverVersion;
		public short dmSize;
		public short dmDriverExtra;
		public int dmFields;
		public int dmPositionX;
		public int dmPositionY;
		public int dmDisplayOrientation;
		public int dmDisplayFixedOutput;
		public short dmColor;
		public short dmDuplex;
		public short dmYResolution;
		public short dmTTOption;
		public short dmCollate;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
		public string dmFormName;

		public short dmLogPixels;
		public int dmBitsPerPel;
		public int dmPelsWidth;
		public int dmPelsHeight;
		public int dmDisplayFlags;
		public int dmDisplayFrequency;
		public int dmICMMethod;
		public int dmICMIntent;
		public int dmMediaType;
		public int dmDitherType;
		public int dmReserved1;
		public int dmReserved2;
		public int dmPanningWidth;
		public int dmPanningHeight;
	}

	class Options
	{
		Game1 game;
		BaseButton confirmButton;
		BaseButton changeGameReso;
		BaseButton dropDownList;

		ScrollingBackground scroll;
		GameTime gametime;
		/// <summary>
		/// Constructor for Options
		/// </summary>
		/// <param name="game"></param>
		public Options(Game1 game)
		{
			this.game = game;
			gametime = new GameTime();
			confirmButton = new BaseButton(Game1.Assets.confirm) { height = 100, width = 200 };
			confirmButton.setPosition(new Vector2((int)(100),
												  (int)(100)));
			scroll = new ScrollingBackground(Game1.Assets.background) { height = game.getScreenHeight(), width = game.getScreenWidth() };
			scroll.setPosition(new Vector2((int)0, (int)0));
			changeGameReso = new BaseButton(Game1.Assets.confirm) { height = 100, width = 200 };
			changeGameReso.setPosition(new Vector2((int)(100),
												  (int)(500)));
		}
		/// <summary>
		/// Update Options, buttons and background
		/// </summary>
		public void update()
		{
			MouseState mouseState = Mouse.GetState();
			confirmButton.update(mouseState);
			changeGameReso.update(mouseState);
			
			if (confirmButton.isClicked == true)
			{
				game.GoToMain();
				confirmButton.isClicked = false;
			}
			scroll.update();

			if (changeGameReso.isClicked == true)
			{
				List<string> resoList = GetScreenResolutions();
				
				string fullScreen = resoList.ElementAt(resoList.Count() - 1);

				string width = "";
				string height = "";
				bool xFound = false;

				for (int i = 0; i < fullScreen.Length; i++)
				{
					if (Char.IsDigit(fullScreen[i]))
					{
						if (xFound)
						{
							height += fullScreen[i];
						}
						else
						{
							width += fullScreen[i];
						}
					}
					else
					{
						xFound = true;
					}
				}
				//game.setScreenSize(int.Parse(width), int.Parse(height), true);
				changeGameReso.isClicked = false;
			}
		}
		
		[DllImport("user32.dll")]
		private static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

		[DllImport("user32.dll")]
		private static extern int GetSystemMetrics(int nIndex);

		public static List<string> GetScreenResolutions()
		{
			var resolutions = new List<string>();

			try
			{
				var devMode = new DEVMODE();
				int i = 0;

				while (EnumDisplaySettings(null, i, ref devMode))
				{
					resolutions.Add(string.Format("{0}x{1}", devMode.dmPelsWidth, devMode.dmPelsHeight));
					i++;
				}

				resolutions = resolutions.Distinct(StringComparer.InvariantCulture).ToList();
			}
			catch (Exception ex)
			{
				Console.WriteLine("Could not get screen resolutions.");
			}

			return resolutions;
		}

		public static string GetCurrentScreenResolution()
		{
			int width = GetSystemMetrics(0x00);
			int height = GetSystemMetrics(0x01);

			return string.Format("{0}x{1}", width, height);
		}

		/// <summary>
		/// Draw images on to Options view
		/// </summary>
		/// <param name="spriteBatch"></param>
		public void draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			scroll.draw(spriteBatch);
			confirmButton.draw(spriteBatch);
			
			changeGameReso.draw(spriteBatch);
			spriteBatch.End();
		}
	}
}