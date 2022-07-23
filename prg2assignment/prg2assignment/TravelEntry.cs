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
    class TravelEntry
    {
        public string LastCountryOfEmbarkation { get; set; }
        public string EntryMode { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ShnEndDate { get; set; }
        public SHNFacility ShnStay { get; set; }
        public bool IsPaid { get; set; }
        public TravelEntry() { }
        public TravelEntry(string LastCont, string EMode, DateTime EDate) // Constructor 
        {
            LastCountryOfEmbarkation = LastCont;
            EntryMode = EMode;
            EntryDate = EDate;
        }
        public void AssignSHNFacility(SHNFacility shnfacil) // assigns SHN facility 
        {
            ShnStay = shnfacil;
        }
        public void CalculateSHNDuration() // calculates SHN Duration and makes changes to SHNEndDate if there is a need for SHN regardless of facility.
        {
            int days = 0;
            
            if (LastCountryOfEmbarkation == "New Zealand" || LastCountryOfEmbarkation == "Vietnam")
            {
                return ;
            }
            else if (LastCountryOfEmbarkation == "Marcao SAR")
            {
                days = 7;
            }
            else {
                days = 14;
                
            }
            ShnEndDate = EntryDate.AddDays(days);
            
        }
        public override string ToString()
        {
            return "Last Country of Embarkation: " + LastCountryOfEmbarkation + " Entry Mode: " + EntryMode + " Entry Date: " + EntryDate + " sHn End Date: " + ShnEndDate + " ShnStay: " + ShnStay;
        }
    }
}
