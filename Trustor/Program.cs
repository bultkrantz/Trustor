using System;
using TrustorLib;
using TrustorLib.Interfaces;
using TrustorLib.Models;

namespace Trustor
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = new TrustorBankSystem(new CustomerManager()); //TODO: Skall ta in AccountManager och CustomerManager när klasserna är klara
            var input = ConsoleKey.A;

            while (input != 0)
            {
                Console.Clear();
                Console.WriteLine(Menu.WelcomeText + Menu.MenuText);

                input = Console.ReadKey().Key;

                switch (input)
                {
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        Environment.Exit(0);
                        break;
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine("\n Sök efter kund:");
                        var search = Console.ReadLine();
                        var answer = system.SearchCustomer(search);
                        Console.Clear();
                        Console.WriteLine(answer);
                        Console.ReadKey();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine("\n Visa info från kund:");

                        var customerNumberInput = Console.ReadLine();

                        if (int.TryParse(customerNumberInput, out var parsedResult))
                        {
                            var customerInfo = system.ShowCustomerInfo(parsedResult);
                            Console.Clear();
                            Console.WriteLine(customerInfo);
                            Console.ReadKey();
                        }

                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        var newCustomer = new Customer();
                        Console.WriteLine("\n Skriv in företagsnamn (Obligatoriskt) tryck sedan [Enter] ");
                        newCustomer.CompanyName = Console.ReadLine();
                        Console.WriteLine("\n Skriv in organisationnummer (Obligatoriskt) tryck sedan [Enter] ");
                        newCustomer.OrgNumber = Console.ReadLine();
                        Console.WriteLine("\n Skriv in adress (Obligatoriskt) tryck sedan [Enter] ");
                        newCustomer.Address = Console.ReadLine();
                        Console.WriteLine("\n Skriv in stad tryck sedan [Enter] ");
                        newCustomer.City = Console.ReadLine();
                        Console.WriteLine("\n Skriv in region (Obligatoriskt) tryck sedan [Enter] ");
                        newCustomer.Region = Console.ReadLine();
                        Console.WriteLine("\n Skriv in postnummer (Obligatoriskt) tryck sedan [Enter] ");
                        newCustomer.PostalCode = Console.ReadLine();
                        Console.WriteLine("\n Skriv in land tryck sedan [Enter] ");
                        newCustomer.Country = Console.ReadLine();
                        Console.WriteLine("\n Skriv in telefonnummer (Obligatoriskt) tryck sedan [Enter] ");
                        newCustomer.Phone = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine(system.CreateCustomer(newCustomer));
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.WriteLine("\n Mata in kundnummer: ");
                        int customerNumber;
                        var result = int.TryParse(Console.ReadLine(), out customerNumber);
                        if (!result || customerNumber.ToString().Length > 4)
                        {
                            Console.Clear();
                            Console.WriteLine("**** Du har ej angett ett korrekt kundnummer! Tryck [Enter] för att fortsätta ****");
                            Console.ReadLine();
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine(system.DeleteCustomer(customerNumber));
                        Console.ReadLine();
                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        Console.WriteLine("\n Skapa konto skall köras");
                        break;
                    case ConsoleKey.D6:
                    case ConsoleKey.NumPad6:
                        Console.WriteLine("\n Ta bort konto skall köras");
                        break;
                    case ConsoleKey.D7:
                    case ConsoleKey.NumPad7:
                        Console.WriteLine("\n Insättning skall köras");
                        break;
                    case ConsoleKey.D8:
                    case ConsoleKey.NumPad8:
                        Console.WriteLine("\n Uttag skall köras");
                        break;
                    case ConsoleKey.D9:
                    case ConsoleKey.NumPad9:
                        Console.WriteLine("\n Överföring skall köras");
                        break;
                    default:
                        Console.WriteLine("\n**** Ogiltigt kommando. Tryck [Enter] för att fortsätta ****");
                        Console.ReadLine();
                        break;
                }

            }
        }
    }
}
