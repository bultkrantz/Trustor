using System;
using System.Collections.Generic;
using System.Text;
using TrustorLib.Models;

namespace TrustorLib.Interfaces
{
    public interface ICustomerManager
    {
        List<Customer> SearchCustomer(string search);
        Customer ShowCustomerInfo(int customerNumber);
        Customer CreateCustomer(Customer customer);
        void DeleteCustomer(int customerNumber);
        int CreateNewCustomerNumber();
    }
}
