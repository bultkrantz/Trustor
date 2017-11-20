using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using TrustorLib.Interfaces;

namespace TrustorLib.Models
{
    public class TrustorDb
    {
        private readonly string _filePath;

        public List<Customer> Customers;
        public List<Account> Accounts;

        public TrustorDb(string filePath)
        {
            _filePath = filePath;
            LoadFile();
        }

        private void LoadFile()
        {
            var text = File.ReadAllLines(_filePath);
            var customers = text.Where(x => x.Count(k => k == ';') == 8)
                .Select(x=> x.Split(';'))
                .Select(x=> new Customer
                {
                    CustomerNumber = int.Parse(x[0]),
                    OrgNumber = x[1],
                    CompanyName = x[2],
                    Address = x[3],
                    City = x[4],
                    Region = x[5],
                    PostalCode = x[6],
                    Country = x[7],
                    Phone = x[8]
                }).ToList();

            var accounts = text.Where(x => x.Count(k => k == ';') == 2)
                .Select(x=> x.Split(';'))
                .Select(x=> new Account
                {
                    AccountNumber = int.Parse(x[0]),
                    CustomerNumber = int.Parse(x[1]),
                    Balance = decimal.Parse(x[2],CultureInfo.InvariantCulture)
                }).ToList();

            Customers = customers;
            Accounts = accounts;
        }

        public void SaveChanges()
        {
            var fileName = Path.GetDirectoryName(_filePath) + @"\\" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".txt";
            var fileContent = "";

            var customersScsv = Customers.Select(x => x.ToString()).ToArray();
            var accountsScsv = Accounts.Select(x => x.ToString()).ToArray();

            var customersBlob = string.Join($"{Environment.NewLine}",customersScsv);
            var accountsBlob = string.Join($"{Environment.NewLine}", accountsScsv);

            fileContent = $"{Customers.Count}{Environment.NewLine}{customersBlob}{Environment.NewLine}{Accounts.Count}{Environment.NewLine}{accountsBlob}";

            File.WriteAllText(fileName,fileContent);
        }
    }
}