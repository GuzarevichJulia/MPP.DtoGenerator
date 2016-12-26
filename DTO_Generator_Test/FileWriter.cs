using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DTO_Generator.ClassDescription;
using DTO_Generator;
using Microsoft.CodeAnalysis;

namespace DTO_Generator_Test
{
    public class FileWriter : IWriter
    {
        public void Write(string directoryPath, string[] filesNames, List<GeneratedClass> generatedClassList)
        {
            Directory.CreateDirectory(directoryPath);
            string fileName;
            string filePath;
            for (int i = 0; i < generatedClassList.Count; i++)
            {
                fileName = filesNames[i] + ".cs";
                filePath = Path.Combine(directoryPath, fileName);
                if (generatedClassList[i].SyntaxTree != null)
                {
                    File.WriteAllText(filePath, generatedClassList[i].SyntaxTree.ToString());
                }
            }
        }
    }
}
