using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employeerequestform.Model
{
    public class employeeformdata
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string alias { get; set; }
        public DateTime startdate { get; set; }
        public Byte[] socialsecurity { get; set; }
        public Byte[] drivinglicense { get; set; }
        public string manageremail { get; set; }
        public string manageralias { get; set; }
        public string department { get; set; }

    }
}
