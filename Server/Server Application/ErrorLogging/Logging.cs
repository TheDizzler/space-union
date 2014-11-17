using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLogging
{
    public class Logging
    {
        private static FileStream Log { get; set; }
        public static Logging()
        {
            string path = "";
            try
            {
                path = Directory.GetCurrentDirectory() + "\\Logs\\" + DateTime.Now.Date.ToString("d-M-yyyy") + "\\";
                Directory.CreateDirectory(path);
                Log = File.Create(path + DateTime.Now.ToString("HH-mm-ss") + ".txt");
            }
            catch (UnauthorizedAccessException e) { Console.WriteLine("Could not create the log file.\n" + e.ToString()); Log = null; return; }
            catch (ArgumentNullException e) { Console.WriteLine("Could not create the log file.\n" + e.ToString()); Log = null; return; }
            catch (PathTooLongException e) { Console.WriteLine("Could not create the log file.\n" + e.ToString()); Log = null; return; }
            catch (DirectoryNotFoundException e) { Console.WriteLine("Could not create the log file.\n" + e.ToString()); Log = null; return; }
            catch (NotSupportedException e) { Console.WriteLine("Could not create the log file.\n" + e.ToString()); Log = null; return; }
            catch (IOException e) { Console.WriteLine("Could not create the log file.\n" + e.ToString()); Log = null; return; }
            catch (ArgumentException e) { Console.WriteLine("Could not create the log file.\n" + e.ToString()); Log = null; return; }

        }

        public static void Write(string input)
        {
            Console.WriteLine(input);
            if (Log == null)
                return;
            try
            {
                byte[] data = new UTF8Encoding(true).GetBytes(input);
                Log.Write(data, 0, data.Length);
            }
            catch (ArgumentNullException e) { Console.WriteLine("Failed to write to log file.\n" + e.ToString()); return; }
            catch (EncoderFallbackException e) { Console.WriteLine("Failed to write to log file.\n" + e.ToString()); return; }
            catch (ArgumentOutOfRangeException e) { Console.WriteLine("Failed to write to log file.\n" + e.ToString()); return; }
            catch (ObjectDisposedException e) { Console.WriteLine("Failed to write to log file.\n" + e.ToString()); return; }
            catch (NotSupportedException e) { Console.WriteLine("Failed to write to log file.\n" + e.ToString()); return; }
            catch (IOException e) { Console.WriteLine("Failed to write to log file.\n" + e.ToString()); return; }
            catch (ArgumentException e) { Console.WriteLine("Failed to write to log file.\n" + e.ToString()); return; }
        }
    }
}
