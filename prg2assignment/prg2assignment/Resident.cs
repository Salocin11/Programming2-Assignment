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
	class Resident : Person
	{
		public string Address { get; set; }
		public DateTime LastLeftCountry { get; set; }
		public TraceTogetherToken Token { get; set; }
		public Resident(string name, string address, DateTime lastleft) : base(name) // Constructor 
		{
			Address = address;
			LastLeftCountry = lastleft;

		}



		public override double CalculateSHNCharges() // Method to calculate SHN Charges if person stayed at Shn Facility 
		{
			double x = (200 + 1000 + 20) * 1.07 ;
			return x;
		}
		public override string ToString() // ToString 
		{
			return base.ToString() + " Address: " + Address + " Lastleftcountry" + LastLeftCountry + " Token: " + Token;

		}

	}
}
