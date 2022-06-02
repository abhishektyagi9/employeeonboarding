using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employeeonboarding.Model
{
    internal class Event
    {
        public string Id { get; set; }
        public DateTime eventTime { get; set; }
        public string eventType { get; set; }
        public string subject { get; set; }
        public string employeedata { get; set; }
    }
}
