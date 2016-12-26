using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Generator.ClassDescription;

namespace DTO_Generator_Test.Converter
{
    interface IConverter
    {
        ClassList Convert(string data);
    }
}
