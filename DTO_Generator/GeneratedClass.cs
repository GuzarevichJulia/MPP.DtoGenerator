using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace DTO_Generator
{
    public class GeneratedClass
    {
        public string Name { get; private set; }
        public SyntaxTree SyntaxTree  { get; set; }

        public GeneratedClass(string name)
        {
            this.Name = name;
        }
    }
}
