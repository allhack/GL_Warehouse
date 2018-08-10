using System.Data;

namespace Models
{
    public class MeasureUnit
    {
        public int    Id   { get; set; }
        public string Name { get; set; }

        public static MeasureUnit FromReader(IDataReader reader)
        {
            return new MeasureUnit
            {
                Id   = (int)reader.GetValue(0),
                Name = (string)reader.GetValue(1)
            };
        }
    }
}