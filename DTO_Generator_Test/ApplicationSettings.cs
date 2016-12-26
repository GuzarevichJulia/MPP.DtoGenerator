using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DTO_Generator_Test
{
    class ApplicationSettings : IApplicationSettings
    {
        private const string classNamespaceKey = "namespace";
        private const string maxTaskCountKey = "maxTaskCount";

        private string classNamespace;
        private int maxTaskCount;

        public string ClassNamespace { get { return classNamespace; } }
        public int MaxTaskCount { get { return maxTaskCount; } }

        public void Set()
        {
            classNamespace = ConfigurationManager.AppSettings[classNamespaceKey];
            if (classNamespace != null)
             maxTaskCount = Int32.Parse(ConfigurationManager.AppSettings[maxTaskCountKey]);
        }
    }
}
