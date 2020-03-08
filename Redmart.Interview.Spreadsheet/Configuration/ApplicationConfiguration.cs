using System;
using System.Collections.Generic;
using System.Text;

namespace Redmart.Interview.Spreadsheet.Configuration
{
    public class ApplicationConfiguration
    {
        private static ApplicationConfiguration instance;

        private ApplicationConfiguration() 
        {
            AllowNegatives = true; //Set this to false to treat negatives as positive numbers
        }
        
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
