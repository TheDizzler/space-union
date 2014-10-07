using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_Comm_Module;

namespace Comm_Module_Tester
{
    /// <summary>
    /// TEMPORARY TEST LAUNCHER FOR CLIENT COMM MODULE.
    /// DO NOT TOUCH.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ClientCommHandler cch = new ClientCommHandler();
            Console.ReadLine();

            cch.sendGameMessage(null);
            cch.
        }
    }
}
