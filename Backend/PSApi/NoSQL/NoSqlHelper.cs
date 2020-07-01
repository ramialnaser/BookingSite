using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using PSApi.NoSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSApi.NoSQL
{
    // This class will handle communications between the cloudstorage and the Api
    // responsible for table creation and operations such as insert, update and get data.
    public class NoSqlHelper
    {
        private CloudStorageAccount cloudStorageAccount;
        private CloudTableClient cloudTableClient;
        private CloudTable table;
        public NoSqlHelper()
        {
            // establish communication with the cloud storage and creates a table in case it doesn't exit
            cloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            table = cloudTableClient.GetTableReference("lab3nosqlps");
            table.CreateIfNotExists();
        }

        // adds an instance of the customer object to the table with a partionkey customers and rowkey as the customer's card number
        public void AddCustomer(Customer customer)
        {
            Customer c = new Customer()
            {
                CardNumber = customer.CardNumber,
                HolderName = customer.HolderName,
                ExpiryDate = customer.ExpiryDate,
                Balance = customer.Balance
            };
            c.RowKey = customer.CardNumber.ToString();


            TableOperation insert = TableOperation.Insert(c);
            table.Execute(insert);
        }

        //to update a specific customer, it takes an object of a customer and then it checks the table if contains that customer
        // if yes the new version of the customer will replace the old one.
        public void UpdateCustomer(Customer customer)
        {
            Customer c = new Customer()
            {
                CardNumber = customer.CardNumber,
                HolderName = customer.HolderName,
                ExpiryDate = customer.ExpiryDate,
                Balance = customer.Balance
            };
            c.RowKey = customer.CardNumber.ToString();
            TableOperation update = TableOperation.InsertOrReplace(c);
            table.ExecuteAsync(update);
        }

        // gets a list of customers that exists in the table, under partitionkey customers.
        public List<Customer> GetCustomers()
        {
            var entities = table.ExecuteQuery(new TableQuery<Customer>()).ToList();
            List<Customer> customers = new List<Customer>();
            foreach (Customer entity in entities)
            {
                if (entity.PartitionKey == "Customers")
                {
                    customers.Add(entity);
                }
            }

            return customers;
        }

        // adds an instance of the transaction object to the table with a partionkey transactions and an incremental rowkey(by getting the size of all transactions and add one to that)
        public void AddTransaction(Transaction transaction)
        {
            Transaction t = new Transaction()
            {
                CardNumber = transaction.CardNumber,
                TransactionDate = transaction.TransactionDate,
                Amount = transaction.Amount
            };
            int id = GetTransactions().Count() + 1;
            t.RowKey = (id).ToString();

            TableOperation insert = TableOperation.Insert(t);
            table.Execute(insert);
        }

        // gets a list of transactions that exists in the table, under partitionkey transactions.
        public List<Transaction> GetTransactions()
        {
            var entities = table.ExecuteQuery(new TableQuery<Transaction>()).ToList();
            List<Transaction> transactions = new List<Transaction>();

            foreach (Transaction entity in entities)
            {
                if (entity.PartitionKey == "Transactions")
                {
                    transactions.Add(entity);
                }
            }

            return transactions;
        }

        // to get a list transactions of a specific customer
        // it takes the cusomter's card number and fetches all the transactions with the matching card number 
        public List<Transaction> GetTransactionsOfSpecCustomer(int cardNumber)
        {
            var entities = table.ExecuteQuery(new TableQuery<Transaction>()).ToList();
            List<Transaction> transactions = new List<Transaction>();

            foreach (Transaction entity in entities)
            {
                if (entity.PartitionKey == "Transactions")
                {
                    if (entity.CardNumber == cardNumber)
                    {
                        transactions.Add(entity);
                    }
                }
            }

            return transactions;
        }
    }
}
