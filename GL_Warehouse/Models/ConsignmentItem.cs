using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ConsignmentItem
    {
        public Consignment Consignment   { get; set; }
        public Product     Product       { get; set; }
        public int         ProductAmount { get; set; }
    }
}
