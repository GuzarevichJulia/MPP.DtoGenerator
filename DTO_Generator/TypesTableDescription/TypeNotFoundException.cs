using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_Generator.TypesTableDescription
{
    public class TypeNotFoundException : Exception
    {
        public TypeNotFoundException(string message) : base(message)
        { }
    }
}
