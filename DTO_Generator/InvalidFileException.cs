using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_Generator
{
    public class InvalidFileException : Exception
    {
        public InvalidFileException(string message) : base(message)
        { }
    }
}
