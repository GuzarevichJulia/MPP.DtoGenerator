using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Generator.ClassDescription;
using Newtonsoft.Json;
using DTO_Generator;

namespace DTO_Generator_Test.Converter
{
    class JsonConverter : IConverter
    {
        public ClassList Convert(string data)
        {
            try
            {
                ClassList classList = JsonConvert.DeserializeObject<ClassList>(data);
                return classList;
            }
            catch(JsonReaderException)
            {
                throw new InvalidFileException("Check the json file");
            }
        }
    }
}
