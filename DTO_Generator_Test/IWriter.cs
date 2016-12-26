using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Generator;

namespace DTO_Generator_Test
{
    interface IWriter
    {
        void Write(string directoryPath, string[] filesNames, List<GeneratedClass> generatedClassList);
    }
}
