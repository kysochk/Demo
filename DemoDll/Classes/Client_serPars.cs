using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoDll;

namespace DemoDll
{
    partial class service_client
    {
        private string fullNameSevice;
        public string FullNameService
        {
            get
            {
                return fullNameSevice;
            }
            set { fullNameSevice = value; }
        }

        private string fullNameClient;
        public string FullNameClient
        {
            get
            {
                return fullNameClient;
            }
            set { fullNameClient = value; }
        }


        private string phoneEmail;
        public string PhoneEmail
        {
            get
            {
                return phoneEmail;
            }
            set { phoneEmail = value; }
        }

        private string timeStart;
        public string TimeStart
        {
            get
            {
                return timeStart;
            }
            set { timeStart = value; }
        }

        public string ColorTime;
        public string colortime
        {
            get
            {
                return ColorTime;
            }
            set
            {
                ColorTime = value;
            }

        }

    }
}
