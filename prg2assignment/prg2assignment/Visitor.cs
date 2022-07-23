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
    class Visitor : Person
    {
        public string PassportNo { get; set; }
        public string Nationality { get; set; }
        public Visitor(string name, string passno, string national) : base(name)
        {
            PassportNo = passno;
            Nationality = national;
        }
        public override double CalculateSHNCharges() // Calculates SHN Charges if visitor stayed in SHN facility. 
        {
            return (2000 + 200) * 1.07;
        }
        public override string ToString()
        {
            return base.ToString() + " PassportNo: " + PassportNo + " Nationality: " + Nationality;

        }


    }
}
