//============================================================
// Student Number : S10204620, S10206093
// Student Name : Nicholas Chng, Nicolas Teo
// Module Group : P06
//============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prg2assignment
{
    class SafeEntry
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public BusinessLocation Location { get; set; }

        public SafeEntry() { }
        
        public SafeEntry(DateTime ci, BusinessLocation l)
        {
            CheckIn = ci;
            Location = l;
        }

        public void PerformCheckOut()
        {
            CheckOut = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Location.BusinessName,-7}|{CheckIn}";
        }

    }
}
