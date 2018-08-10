using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    class WarehouseRepository : BaseRepository
    {
        public void AddProduct(Product product)
        {
            string commandText = "INSERT INTO [Product] VALUES (@Name, @MeasureUnitId, @Price)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@MeasureUnitId", product.MeasureUnit.Id);
                    command.Parameters.AddWithValue("@Price", product.Price);

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddConsignmentWithProducts(Consignment consignment)
        {
            string addConsignmentText = "INSERT INTO [Consignment] VALUES (@Type, @Date)";
            string addConsignmentItemText = "INSERT INTO [ConsignmentItem] VALUES (@ConsignmentId, @ProductId, @ProductAmount)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(addConsignmentText, connection))
                {
                    command.Parameters.AddWithValue("@Type", consignment.Type.ToString());
                    command.Parameters.AddWithValue("@Date", consignment.Date);

                    command.ExecuteNonQuery();
                }

                foreach (var item in consignment.Items)
                    using (SqlCommand command = new SqlCommand(addConsignmentItemText, connection))
                    {
                        command.Parameters.AddWithValue("@ConsignmentId", item.Consignment.Id);
                        command.Parameters.AddWithValue("@ProductId", item.Product.Id);
                        command.Parameters.AddWithValue("@ProductAmount", item.ProductAmount);

                        command.ExecuteNonQuery();
                    }
            }
        }

        public Dictionary<Product, double> GetSummary()
        {
            string commandText = @"SELECT 
                                	[p].[Id],
                                	[p].[Name],
                                    [p].[MeasureUnitId],
                                    [p].[Price],
                                	SUM(CASE [c].[Type] WHEN 'IN' THEN 1 WHEN 'OUT' THEN -1 ELSE 0 END * COALESCE([i].[ProductAmount], 0)) AS [Summary]
                                  FROM 
                                	[Product] [p] LEFT JOIN [ConsignmentItem] [i] ON [p].[Id] = [i].[ProductId] LEFT JOIN [Consignment] [c] ON [i].[ConsignmentId] = [c].[Id]
                                  GROUP BY
                                	[p].[Id], [p].[Name]";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        Dictionary<Product, double> result = new Dictionary<Product, double>();

                        while(reader.Read())
                        {
                            result.Add(Product.FromReader(reader), reader.GetDouble(4));
                        }

                        return result;
                    }
                }
            }
        }
    }
}
