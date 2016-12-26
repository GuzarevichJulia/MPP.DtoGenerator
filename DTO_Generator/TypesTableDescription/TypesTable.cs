using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_Generator.TypesTableDescription
{
    public class TypesTable
    {
        private List<SupportedType> types;

        public TypesTable()
        {
            this.types = new List<SupportedType>();
            AddType("integer", "int32", "System.Int32");
            AddType("integer", "int64", "System.Int64");
            AddType("number", "float", "System.Single");
            AddType("number", "double", "System.Double");
            AddType("string", "byte", "System.Byte");
            AddType("string", "date", "System.DateTime");
            AddType("string", "string", "System.String");
            AddType("boolean", "", "System.Boolean");
        }

        private void AddType(string type, string format, string netType)
        {
            SupportedType supportedType = new SupportedType(type,format,netType);
            types.Add(supportedType);
        }

        public string GetNetType(string type, string format)
        {
            foreach (SupportedType typeItem in types)
            {
                if((typeItem.Format == format)&& (typeItem.Type == type))
                {
                    return typeItem.NetType;
                }
            }
            return "";
        }
    }
}
