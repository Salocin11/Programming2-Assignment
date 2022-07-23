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
    class BusinessLocation
    {
        public string BusinessName { get; set; }
        public string BranchCode { get; set; }
        public int MaximumCapacity { get; set; }
        public int VisitorsNow { get; set; }

        public BusinessLocation() { }

        public BusinessLocation(string bn, string bc, int c)
        {
            BusinessName = bn;
            BranchCode = bc;
            MaximumCapacity = c;
            VisitorsNow = 0;
        }

        public bool IsFull()
        {
            if (VisitorsNow >= MaximumCapacity)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return $"{BusinessName, -7}|{BranchCode, -7}|{MaximumCapacity}";
        }
    }
}
