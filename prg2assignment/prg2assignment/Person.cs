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
    abstract class Person
    {
        public string Name { get; set; }
        public List<SafeEntry> SafeEntryList = new List<SafeEntry>();
        public List<TravelEntry> TravelEntryList = new List<TravelEntry>();

        public Person() { }

        public Person(string name)
        {
            Name = name;
        }

        public void AddTravelEntry(TravelEntry x) // Add travel entry to travelentry list 
        {
            TravelEntryList.Add(x);
        }

        public void AddSafeEntry(SafeEntry x)
        {
            SafeEntryList.Add(x);
        }

        public abstract double CalculateSHNCharges();

        public override string ToString()
        {
            return Name;
        }
    }
}
