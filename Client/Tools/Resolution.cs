using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SpaceUnionXNA.Tools
{
    class Resolution
    {
        /// <summary>
        /// Source: http://stackoverflow.com/questions/10039339/get-the-supported-screen-resolutions-in-xna
        /// Used to find all supported resolutions by the corresponding computer's graphic driver
        /// Used over the built in XNA for easier filtering of resolutions
        /// XNA will display all resolutions including for each refresh rate supported spooling out duplicate resolutions
        /// @Added by Steven
        /// </summary>
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
                    if (devMode.dmPelsWidth >= 800 && devMode.dmPelsHeight >= 600)
                    {
                        resolutions.Add(string.Format("{0}x{1}", devMode.dmPelsWidth, devMode.dmPelsHeight));
                    }
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
    }
}
