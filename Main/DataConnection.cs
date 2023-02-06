using System;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Main
{
    class DataConnection
    {
        string ConnectionString;

        public DataConnection()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
        }

        public bool AddCategoryDB(string CategoryName, string CategoryDescription)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlTransaction sqlTran = connection.BeginTransaction();
                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;

                MemoryStream ms = new MemoryStream();

                ms.Close();

                try
                {

                    command.Parameters.AddWithValue("@CategoryName", CategoryName);
                    command.Parameters.AddWithValue("@CategoryDescription", CategoryDescription);

                    command.CommandText =
                       "Insert into Categories (CategoryName, CategoryDescription) values (@CategoryName, @CategoryDescription)";
                    command.ExecuteNonQuery();

                    sqlTran.Commit();

                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    connection.Close();
                    return false;
                }
            }
        }

        public ArrayList RetreiveAllCategoriesFromDatabase()
        {
            ArrayList CategoriesList = new ArrayList();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ID, CategoryName FROM Categories;", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int CategoryID = reader.GetInt32(0);
                        string CategoryName = reader.GetString(1);

                        CategoriesList.Add(new Data() { ID = CategoryID, Name = CategoryName });
                    }
                }
                reader.Close();

                return CategoriesList;
            }
        }

        public bool AddProductDB(string ProductName, decimal ProductPrice, int ProductCategoryID, string ProductDescription, byte[] ProductPicture)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlTransaction sqlTran = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;

                try
                {

                    command.Parameters.AddWithValue("@ProductName", ProductName);
                    command.Parameters.AddWithValue("@ProductPrice", ProductPrice);
                    command.Parameters.AddWithValue("@ProductCategoryID", ProductCategoryID);
                    command.Parameters.AddWithValue("@ProductDescription", ProductDescription);
                    command.Parameters.AddWithValue("@ProductImage", ProductPicture);

                    command.CommandText =
                       "Insert into Products (ProductName, ProductPrice, ProductCategoryID, ProductDescription, ProductImage) values (@ProductName, @ProductPrice, @ProductCategoryID, @ProductDescription, @ProductImage)";
                    command.ExecuteNonQuery();

                    sqlTran.Commit();

                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    connection.Close();
                    return false;
                }
            }
        }

        public ArrayList RetreiveAllProducts()
        {
            ArrayList ProductsList = new ArrayList();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ID, ProductName, ProductPrice, ProductCategoryID, ProductDescription, ProductImage FROM Products;", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int ProductID = reader.GetInt32(0);
                        string ProductName = reader.GetString(1);
                        decimal ProductPrice = reader.GetDecimal(2);
                        int ProductCategoryID = reader.GetInt32(3);
                        string ProductCategoryName = ReturnCategoryName(ProductCategoryID);
                        string ProductDescription = reader.GetString(4);
                        byte[] ProductPicture = (byte[])reader[5];

                        ProductsList.Add(new Data() { Name = ProductName, Price = ProductPrice, Category = ProductCategoryName, Description = ProductDescription, Picture = ProductPicture, ID = ProductID });
                    }
                }
                reader.Close();

                return ProductsList;
            }
        }

        public ArrayList RetreiveProductsFromCategory(int CategoryID)
        {
            ArrayList ProductsList = new ArrayList();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ID, ProductName, ProductPrice, ProductDescription, ProductImage FROM Products where ProductCategoryID = '" + CategoryID + "';", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int ProductID = reader.GetInt32(0);
                        string ProductName = reader.GetString(1);
                        decimal ProductPrice = reader.GetDecimal(2);
                        string ProductDescription = reader.GetString(3);
                        byte[] ProductPicture = (byte[])reader[4];

                        ProductsList.Add(new Data() { ID = ProductID, Name = ProductName, Price = ProductPrice, Picture = ProductPicture, Description = ProductDescription });
                    }
                }
                reader.Close();

                return ProductsList;
            }
        }

        public int ReturnCategoryID(string CategoryName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ID FROM Categories where CategoryName = '" + CategoryName + "';", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                int CategoryID = 0;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CategoryID = reader.GetInt32(0);
                    }
                }
                reader.Close();

                return CategoryID;
            }
        }

        public string ReturnCategoryName(int CategoryID)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT CategoryName FROM Categories where ID = '" + CategoryID + "';", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                string CategoryName = string.Empty;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CategoryName = reader.GetString(0);
                    }
                }
                reader.Close();

                return CategoryName;
            }
        }

        public Data RetreiveProductDetails(int ProductID)   
        {
            Data ProductDetails = new Data();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ProductName, ProductPrice, ProductCategoryID, ProductDescription, ProductImage FROM Products where ID = '" + ProductID + "';", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductDetails.Name = reader.GetString(0);
                        ProductDetails.Price = reader.GetDecimal(1);
                        int ProductCategoryID = reader.GetInt32(2);
                        ProductDetails.Category = ReturnCategoryName(ProductCategoryID);
                        ProductDetails.Description = reader.GetString(3);
                        ProductDetails.Picture = (byte[])reader[4];
                    }
                }
                reader.Close();

                return ProductDetails;
            }
        }

      

        public bool AddSaleDB(DateTime SaleTime, decimal CashGiven, decimal TotalBill, decimal CashReturn)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                SqlTransaction sqlTran = connection.BeginTransaction();

                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;

                try
                {
                    command.Parameters.AddWithValue("@SaleTime", SaleTime);
                    command.Parameters.AddWithValue("@CashGiven", CashGiven);
                    command.Parameters.AddWithValue("@TotalBill", TotalBill);
                    command.Parameters.AddWithValue("@CashReturn", CashReturn);

                    command.CommandText =
                       "Insert into Sales (SaleTime, CashGiven, TotalBill, CashReturn) values (@SaleTime, @CashGiven, @TotalBill, @CashReturn)";
                    command.ExecuteNonQuery();

                    sqlTran.Commit();

                    connection.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    connection.Close();
                    return false;
                }
            }
        }

        public ArrayList RetreiveAllSales()
        {
            ArrayList SalesList = new ArrayList();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT ID, SaleTime, CashGiven, CashReturn, TotalBill FROM Sales;", connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int SaleID = reader.GetInt32(0);
                        DateTime SaleTime = reader.GetDateTime(1);
                        decimal CashGiven = reader.GetDecimal(2);
                        decimal CashReturn = reader.GetDecimal(3);
                        decimal TotalBill = reader.GetDecimal(4);

                        SalesList.Add(new Data() { SaleID=SaleID, SaleTime = SaleTime, Given=CashGiven, Retrun=CashReturn, Total = TotalBill });
                    }
                }
                reader.Close();

                return SalesList;
            }
        }
    }
}
