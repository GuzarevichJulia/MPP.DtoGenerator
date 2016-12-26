using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DTO_Generator_Test
{
    class FileReader : IReader
    {
        private const string extension = ".json";
        
        public string Read(string filePath)
        {
            if (!File.Exists(filePath) || !Path.GetExtension(filePath).Equals(extension))
            {
                throw new FileNotFoundException("File "+filePath+" does not exist");
            };

            return File.ReadAllText(filePath);
        }
    }
}
