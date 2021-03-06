﻿using System;
using System.Globalization;
using System.IO;
using System.Linq;
using TrustorLib;
using TrustorLib.Interfaces;
using TrustorLib.Models;

namespace Trustor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Yellow;

            var fileName = "";
            if (args.Length > 0 && File.Exists(args[0])) // Skall användas vid live release
            {
                fileName = args[0];
            }
            else
            {
                Console.WriteLine("File not found");
                Console.ReadLine();
                Environment.Exit(-1);
            }

            //fileName = "bankdata.txt"; // TODO Ta bort vid live-release. Ska ej vara hårdkodad.
            //var path = Path.Combine(Environment.CurrentDirectory, @"Database\", fileName);
            var path = fileName; // Live kod.

            var trustorDb = new TrustorDb(path);

            var system = new TrustorBankSystem(new CustomerManager(trustorDb), new AccountManager(trustorDb)); //TODO: Skall ta in AccountManager och CustomerManager när klasserna är klara

            var input = ConsoleKey.A;

            while (input != 0)
            {

                var currentItem = 0;
                ConsoleKeyInfo key;

                var menuItems = new[]
                {
                    " Avsluta och spara ",
                    " Sök kund ",
                    " Visa kundbild ",
                    " Skapa kund ",
                    " Ta bort kund ",
                    " Skapa konto ",
                    " Ta bort konto ",
                    " Insättning ",
                    " Uttag ",
                    " Överföring"
                };

                do
                {
                    Console.Clear();
                    var customerCount = trustorDb.Customers.Count.ToString();
                    var accountCount = trustorDb.Accounts.Count.ToString();
                    var totalBalance = trustorDb.Accounts.Sum(x => x.Balance).ToString();
                    Console.WriteLine(Menu.WelcomeText);
                    Console.WriteLine(Menu.Logo);
                    Console.WriteLine(Menu.BankStatistics(customerCount, accountCount, totalBalance));

                    for (var i = 0; i < menuItems.Length; i++)
                    {
                        if (currentItem == i)
                        {
                            Console.Write(">>");
                            Console.WriteLine(menuItems[i]);
                        }
                        else
                        {
                            Console.WriteLine("  " + menuItems[i]);
                        }
                    }

                    key = Console.ReadKey(true);

                    if (key.Key.ToString() == "DownArrow")
                    {
                        currentItem++;
                        if (currentItem > menuItems.Length - 1)
                            currentItem = 0;
                    }
                    else if (key.Key.ToString() == "UpArrow")
                    {
                        currentItem--;
                        if (currentItem < 0)
                            currentItem = menuItems.Length - 1;
                    }

                } while (key.KeyChar != 13);

                switch (currentItem)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Sparar och avslutar...");
                        Console.ReadKey();
                        trustorDb.SaveChanges();
                        Environment.Exit(0);
                        break;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("\n Sök efter kund:");
                        var search = Console.ReadLine();
                        var answer = system.SearchCustomer(search);
                        Console.Clear();
                        Console.WriteLine(answer);
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.Clear();
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
                    case 3:
                        Console.Clear();
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
                    case 4:
                        Console.Clear();
                        Console.WriteLine("\n Mata in kundnummer: ");
                        int customerNumber;
                        var result = int.TryParse(Console.ReadLine(), out customerNumber);
                        if (!result || customerNumber.ToString().Length > 4)
                        {
                            Console.Clear();
                            Console.WriteLine(
                                "**** Du har ej angett ett korrekt kundnummer! Tryck [Enter] för att fortsätta ****");
                            Console.ReadLine();
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine(system.DeleteCustomer(customerNumber));
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("\nSkriv in kundnummer för att skapa nytt konto");
                        var customerInput = int.TryParse(Console.ReadLine(), out customerNumber);
                        if (!customerInput || customerNumber.ToString().Length > 4)
                        {
                            Console.WriteLine("**** Du har ej angett ett korrekt kundnummer! Tryck [Enter] för att fortsätta ****");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine(system.CreateAccount(customerNumber));
                            Console.ReadKey();
                        }
                        break;
                    case 6:
                        Console.Clear();
                        Console.WriteLine("\n Mata in kontonummer: ");

                        int removeAccountNumber;
                        var removeAccountNumberResult = int.TryParse(Console.ReadLine(), out removeAccountNumber);

                        if (!removeAccountNumberResult || removeAccountNumber.ToString().Length < 5)
                        {
                            Console.Clear();
                            Console.WriteLine("**** Du har ej angett ett korrekt kontonummer! Tryck [Enter] för att fortsätta ****");
                            Console.ReadLine();
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine(system.DeleteAccount(removeAccountNumber));
                        Console.ReadKey();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("\nMata in kontonummer för insättning: ");
                        var newDepositAccountResult = int.TryParse(Console.ReadLine(), out var newDepositAccount);

                        if (!newDepositAccountResult || newDepositAccount.ToString().Length < 5)
                        {
                            Console.Clear();
                            Console.WriteLine("**** Du har ej angett ett korrekt kontonummer! Tryck [Enter] för att fortsätta ****");
                            Console.ReadLine();
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine("Mata in hur mycket du vill sätta in:");
                        decimal.TryParse(Console.ReadLine().Replace(",","."),NumberStyles.AllowDecimalPoint,CultureInfo.InvariantCulture, out var newDepositAmount);
                        Console.WriteLine(system.NewDeposit(newDepositAccount, newDepositAmount));
                        Console.ReadKey();
                        break;
                    case 8:
                        Console.Clear();
                        int withdrawalAccountNumber;
                        decimal withdrawalAmount;

                        Console.WriteLine("\n Mata in kontonummer att dra pengar ifrån: ");
                        var withdrawalResult = int.TryParse(Console.ReadLine(), out withdrawalAccountNumber);
                        if (!withdrawalResult || withdrawalAccountNumber.ToString().Length < 5)
                        {
                            Console.Clear();
                            Console.WriteLine("**** Du har ej angett ett korrekt kontonummer! Tryck [Enter] för att fortsätta ****");
                            Console.ReadLine();
                            break;
                        }
                        Console.WriteLine("\n Mata in summa: ");
                        var withdrawalIsDecimal = decimal.TryParse(Console.ReadLine().Replace(",","."),NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out withdrawalAmount);
                        if (!withdrawalIsDecimal)
                        {
                            Console.Clear();
                            Console.WriteLine(
                                "**** Du har ej angett en korrekt summa! Tryck [Enter] för att fortsätta ****");
                            Console.ReadLine();
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine(system.NewWithdrawal(withdrawalAccountNumber, withdrawalAmount));
                        Console.ReadLine();

                        break;
                    case 9:
                        Console.Clear();
                        int fromAccountNumber;
                        int toAccountNumber;
                        decimal amount;

                        Console.WriteLine("\n Mata in konto att dra pengar ifrån: ");
                        var validFromNumber = int.TryParse(Console.ReadLine(), out fromAccountNumber);
                        if (!validFromNumber || fromAccountNumber.ToString().Length != 5)
                        {
                            Console.Clear();
                            Console.WriteLine("**** Du har ej angett ett korrekt kontonummer! Tryck [Enter] för att fortsätta ****");
                            Console.ReadLine();
                            break;
                        }

                        Console.WriteLine("\n Mata in konto pengarna skall sättas in på: ");
                        var validToNumber = int.TryParse(Console.ReadLine(), out toAccountNumber);
                        if (!validToNumber || toAccountNumber.ToString().Length != 5)
                        {
                            Console.Clear();
                            Console.WriteLine(
                                "**** Du har ej angett ett korrekt kontonummer! Tryck [Enter] för att fortsätta ****");
                            Console.ReadLine();
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine("\n Mata in summa: ");
                        var isDecimal = decimal.TryParse(Console.ReadLine().Replace(",","."),NumberStyles.AllowDecimalPoint,CultureInfo.InvariantCulture, out amount);
                        if (!isDecimal)
                        {
                            Console.Clear();
                            Console.WriteLine(
                                "**** Du har ej angett en korrekt summa! Tryck [Enter] för att fortsätta ****");
                            Console.ReadLine();
                            break;
                        }
                        Console.Clear();
                        Console.WriteLine(system.NewTransfer(fromAccountNumber, toAccountNumber, amount));
                        Console.ReadLine();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\n**** Ogiltigt kommando. Tryck [Enter] för att fortsätta ****");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
