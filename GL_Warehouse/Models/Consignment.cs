using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Models
{
    public enum ConsignmentType
    {
        IN,
        OUT
    }

    public class Consignment
    {
        public int                   Id    { get; set; }
        public ConsignmentType       Type  { get; set; }
        public DateTime              Date  { get; set; }
        public List<ConsignmentItem> Items { get; set; }

        public static Consignment FromReader(IDataReader reader)
        {
            return new Consignment
            {
                Id   = (int)reader.GetValue(0),
                Type = (ConsignmentType)Enum.Parse(typeof(ConsignmentType), (string)reader.GetValue(1)),
                Date = (DateTime)reader.GetValue(2)
            };
        }
    }
}
