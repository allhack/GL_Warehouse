using System;
using System.Data;

namespace Models
{
    public class Product
    {
        public int         Id          { get; set; }
        public string      Name        { get; set; }
        public MeasureUnit MeasureUnit { get; set; }
        public decimal     Price       { get; set; }

        public static Product FromReader(IDataReader reader)
        {
            return new Product
            {
                Id          = (int)reader.GetValue(0),
                Name        = (string)reader.GetValue(1),
                MeasureUnit = new MeasureUnit { Id = (int)reader.GetValue(2)},
                Price       = (decimal)reader.GetValue(3)
            };
        }
    }
}
