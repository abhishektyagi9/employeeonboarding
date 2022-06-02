using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employeeonboarding.Model
{
    internal class employeeformdata
    {
        public string id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string alias { get; set; }
        public DateTime startdate { get; set; }
        public string socialsecurity { get; set; }
        public string drivinglicense { get; set; }
        public string manageremail { get; set; }
        public string manageralias { get; set; }
        public string department { get; set; }
        public string password { get; set; }
        public documents docs { get; set; }

    }
    
    public class documents
    {
        public byte[] DLimagefile { get; set; }
        public byte[] socialimagefile { get; set; }
    }
}
