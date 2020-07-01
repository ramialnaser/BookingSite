using PSApi.SQL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSApi.SQL
{
    // this class will handle the communications between the database and the controllers using the model classes as objects.
    public class SqlDbHelper
    {
        private readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=lab3frsdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // to fetch a list of customers from the database
        public async Task<List<Customer>> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM dbo.Customers;";

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            customers.Add(new Customer()
                            {
                                CardNumber = reader.GetInt32(0),
                                HolderName = reader.GetString(1),
                                ExpiryDate = reader.GetString(2),
                                Balance = reader.GetInt32(3)
                            });
                        }
                    }
                }
            }

            return customers;
        }
        // to add a new customer to the database
        public async Task AddCustomer(Customer customer)
        {
            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO dbo.Customers(CardNumber, HolderName, ExpiryDate, Balance) VALUES(@CardNumber, @HolderName, @ExpiryDate, @Balance);";
                    cmd.Parameters.AddWithValue("@CardNumber", customer.CardNumber);
                    cmd.Parameters.AddWithValue("@HolderName", customer.HolderName);
                    cmd.Parameters.AddWithValue("@ExpiryDate", customer.ExpiryDate);
                    cmd.Parameters.AddWithValue("@Balance", customer.Balance);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        // to update the customer's balance
        public async Task UpdateCustomer(Customer customer)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // open the connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE dbo.Customers SET Balance = @Balance WHERE CardNumber = @CardNumber;";
                    cmd.Parameters.AddWithValue("@Balance", customer.Balance);
                    cmd.Parameters.AddWithValue("@CardNumber", customer.CardNumber);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        // to fetch all the transations from the database
        public async Task<List<Transaction>> GetTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();

            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM dbo.Transactions;";

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            transactions.Add(new Transaction()
                            {
                                TransactionDate = reader.GetDateTime(1),
                                Amount = reader.GetInt32(2),
                                CardNumber = reader.GetInt32(3),
                            });
                        }
                    }
                }
            }

            return transactions;
        }

        public async Task<List<Transaction>> GetSpecificTransactions(int cardNumber)
        {
            List<Transaction> transactions = new List<Transaction>();

            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM dbo.Transactions WHERE dbo.Transactions.CardNumber = @CardNumber;";

                    cmd.Parameters.AddWithValue("@CardNumber", cardNumber);


                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            transactions.Add(new Transaction()
                            {
                                TransactionDate = reader.GetDateTime(1),
                                Amount = reader.GetInt32(2),
                                CardNumber = reader.GetInt32(3),
                            });
                        }
                    }
                }
            }

            return transactions;
        }

        // to add a new transation to the database
        public async Task AddTransaction(Transaction transaction)
        {
            // using with resources to establish the connection and disposes it after it is done with the task
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // opens connection
                await connection.OpenAsync();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO dbo.Transactions(CardNumber, TransactionDate, Amount) VALUES(@CardNumber, @TransactionDate, @Amount);";
                    cmd.Parameters.AddWithValue("@CardNumber", transaction.CardNumber);
                    cmd.Parameters.AddWithValue("@TransactionDate", transaction.TransactionDate);
                    cmd.Parameters.AddWithValue("@Amount", transaction.Amount);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

    }

}
