﻿using System;
using System.Collections.Generic;
using System.Text;
using TrustorLib.Interfaces;
using TrustorLib.Models;

namespace TrustorLib
{
    class CustomerManager : ICustomerManager
    {
        public Customer CreateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public void DeleteCustomer(int customerNumber)
        {
            throw new NotImplementedException();
        }

        public List<Customer> SearchCustomer(string search)
        {
            throw new NotImplementedException();
        }

        public Customer ShowCustomerInfo(int customerNumber)
        {
            throw new NotImplementedException();
        }
    }
}
