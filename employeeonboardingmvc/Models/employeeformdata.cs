using NuGet.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employeeonboardingmvc.Models
{
    public class employeeformdata
    {
        public string id { get; set; }
        [DisplayName("First Name")]
        public string firstname { get; set; }
        [DisplayName("Last Name")]
        public string lastname { get; set; }
        [DisplayName("Email")]
        public string email { get; set; }
        [DisplayName("Alias")]
        public string alias { get; set; }
        [DisplayName("Start Date")]
        public DateTime startdate { get; set; }
        [DisplayName("Social Security Card")]
        public string socialsecurity { get; set; }
        [DisplayName("Driving License")]
        public string drivinglicense { get; set; }
        [DisplayName("Manager Email")]
        public string manageremail { get; set; }
        [DisplayName("Manager Alias")]
        public string manageralias { get; set; }
        [DisplayName("Department")]
        public string department { get; set; }
        public documents docs { get; set; }
    }
    public class documents
    {
        public byte[] DLimagefile { get; set; }
        public byte[] socialimagefile { get; set; }
    }
}
