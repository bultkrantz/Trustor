using System;
using TrustorLib;
using TrustorLib.Models;

namespace Trustor
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = ConsoleKey.A;

            while (input != 0)
            {
                Console.WriteLine(Menu.WelcomeText + Menu.MenuText);

                input = Console.ReadKey().Key;

                switch (input)
                {
                    case ConsoleKey.D0: case ConsoleKey.NumPad0:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.D1: case ConsoleKey.NumPad1:
                        Console.WriteLine("\n Sök kund skall köras");
                        break;
                    case ConsoleKey.D2: case ConsoleKey.NumPad2:
                        Console.WriteLine("\n Visa kundbild skall köras");
                        break;
                    case ConsoleKey.D3: case ConsoleKey.NumPad3:
                        Console.WriteLine("\n Skapa kund skall köras");
                        break;
                    case ConsoleKey.D4: case ConsoleKey.NumPad4:
                        Console.WriteLine("\n Ta bort kund skall köras");
                        break;
                    case ConsoleKey.D5: case ConsoleKey.NumPad5:
                        Console.WriteLine("\n Skapa konto skall köras");
                        break;
                    case ConsoleKey.D6: case ConsoleKey.NumPad6: 
                        Console.WriteLine("\n Ta bort konto skall köras");
                        break;
                    case ConsoleKey.D7: case ConsoleKey.NumPad7:
                        Console.WriteLine("\n Insättning skall köras");
                        break;
                    case ConsoleKey.D8: case ConsoleKey.NumPad8:
                        Console.WriteLine("\n Uttag skall köras");
                        break;
                    case ConsoleKey.D9: case ConsoleKey.NumPad9:
                        Console.WriteLine("\n Överföring skall köras");
                        break;
                    default:
                        Console.WriteLine("\n Ogiltigt kommando");
                        break;
                }
      
            }
        }
    }
}
