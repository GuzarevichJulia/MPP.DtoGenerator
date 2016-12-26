using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_Generator.ClassDescription;
using System.Threading;


namespace DTO_Generator
{
    public class Task 
    {
        public string ClassNamespace { get;  set; }
        public Class Class { get; set; }
        public ManualResetEvent Event { get; set; }
        public GeneratedClass GeneratedClass { get; set; }

        public Task(string classNamespace, Class Class)
        {
            this.ClassNamespace = classNamespace;
            this.Class = Class;
            this.GeneratedClass = new GeneratedClass(Class.ClassName);
        }
    }
}
