using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Configuration
{
    public class ApplicationConfiguration
    {
        private static ApplicationConfiguration instance;

        private ApplicationConfiguration() { }
        
        public bool AllowNegatives { get; set; }

        public static ApplicationConfiguration Instance
        {
            get
            {
                if (instance == null)                
                    instance = new ApplicationConfiguration();

                return instance;
            }
        }
    }
}
