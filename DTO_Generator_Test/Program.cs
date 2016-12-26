using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Generator;
using DTO_Generator.ClassDescription;
using DTO_Generator_Test.Converter;
using DTO_Generator.TypesTableDescription;
using System.IO;
using System.Configuration;

namespace DTO_Generator_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath;
            string directoryPath;

            Console.WriteLine("Enter the path to json-file");
            filePath = Console.ReadLine();
            filePath = filePath.Trim();
            Console.WriteLine("Enter the path to directory");
            directoryPath = Console.ReadLine();
            directoryPath = directoryPath.Trim();

            if ((filePath != "") && (directoryPath != ""))
            {
               try
                {
                    Program tester = new Program();
                    ApplicationSettings applicationSettings = new ApplicationSettings();
                    applicationSettings.Set();
                    if (applicationSettings.ClassNamespace != null)
                    {
                        tester.Test(filePath, directoryPath, applicationSettings);
                    }
                    else
                    {
                        Console.WriteLine("App.config file must contain the namespace setting");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect value of the setting of max thread count");
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("App.config file must contain the setting of max thread count");
                }
                catch(ConfigurationErrorsException)
                {
                    Console.WriteLine("Check the App.config file ");
                }
            }
            else
            {
                Console.WriteLine("You need to enter a file name and directory name");
            }
            Console.ReadLine();
        }

        private void Test(string filePath, string directoryPath, ApplicationSettings applicationSettings)
        {
            try
            {
                FileReader fileReader = new FileReader();
                string readData = fileReader.Read(filePath);
                if (readData == null)
                {
                    Console.WriteLine("Check the input file path");
                    return;
                }

                IConverter jsonConverter = new JsonConverter();
                ClassList classList = jsonConverter.Convert(readData);

                DtoGenerator dtoGenerator = new DtoGenerator(applicationSettings.ClassNamespace, applicationSettings.MaxTaskCount);
                List<GeneratedClass> generatedClasses = dtoGenerator.Generate(classList);
                dtoGenerator.Dispose();

                FileWriter fileWriter = new FileWriter();
                string[] filesNames = classList.classDescriptions.Select(x => x.ClassName).ToArray();
                fileWriter.Write(directoryPath, filesNames, generatedClasses);
                Console.WriteLine("Code generation was completed");
            }
            catch(FileNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (InvalidFileException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch
            {
                Console.WriteLine("Code generation was failed");
            }
        }        
    }
}
