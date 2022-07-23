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
	class SHNFacility
	{
		public string FacilityName { get; set; }
		public int FacilityCapacity { get; set; }
		public int FacilityVacancy { get; set; }
		public double distFromAirCheckpoint { get; set; }
		public double distFromSeaCheckpoint { get; set; }
		public double distFromLandCheckpoint { get; set; }

		public SHNFacility() { }

		public SHNFacility(string n, int fc, double a, double s, double l) // Constructor 
		{
			FacilityName = n;
			FacilityCapacity = fc;
			distFromAirCheckpoint = a;
			distFromSeaCheckpoint = s;
			distFromLandCheckpoint = l;
		}

		public double CalculateTravelCost(string eMode, DateTime eDate) // Calculates travel. 
		{
			if (eDate.Hour >=0 && eDate.Hour <6) // Checks time
            {
				if(eMode == "Sea") // Checks mode of transporation 
                {

					return 1.605 *(50 + (distFromSeaCheckpoint * 0.22));
				}
				else if(eMode == "Air")
                {
					return 1.605 * (50 + (distFromAirCheckpoint* 0.22));
				}
				else if(eMode == "Land")
                {
					return 1.605 * (50 + (distFromLandCheckpoint* 0.22)); //
				}
				
			}
            else if(eDate.Hour >= 6 && eDate.Hour < 9 || eDate.Hour >= 18 && eDate.Hour < 24)
            {
				if (eMode == "Sea")
				{
					return 1.3375 * (50 + (distFromSeaCheckpoint * 0.22));
				}
				else if (eMode == "Air")
				{
					return 1.3375 *  (50 + (distFromAirCheckpoint * 0.22));
				}
				else if (eMode == "Land")
				{
					return 1.3375 * (50 + (distFromLandCheckpoint * 0.22));
				}
			}
            else
            {
				if (eMode == "Sea")
				{
					return 1.07 * (50 + (distFromSeaCheckpoint * 0.22));
				}
				else if (eMode == "Air")
				{
					return 1.07 * (50 + (distFromAirCheckpoint * 0.22));
				}
				else if (eMode == "Land")
				{
					return 1.07 * (50 + (distFromLandCheckpoint * 0.22));
				}
			}
			return 0.0;
		}

		public bool IsAvailable() // checks if facility is avaliable 
		{
			if (FacilityVacancy <= 0)
				return false;
			else
				return true;
		}

		public override string ToString() // ToString 
		{
			return $"{FacilityName,-7}|{FacilityCapacity,-7}|{FacilityVacancy,-7}|{distFromAirCheckpoint,-7}|{distFromSeaCheckpoint,-7}|{distFromLandCheckpoint,-7}";
		}
	}
}
