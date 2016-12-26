using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_Generator.TypesTableDescription
{
    public class SupportedType
    {
        public string Type { get; set; }
        public string Format { get; set; }
        public string NetType { get; set; }

        public SupportedType(string type, string format, string netType)
        {
            this.Type = type;
            this.Format = format;
            this.NetType = netType;
        }
    }
}
