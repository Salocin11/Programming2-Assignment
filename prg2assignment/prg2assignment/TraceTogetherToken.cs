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
	class TraceTogetherToken
	{
		public string SerialNo { get; set; }
		public string CollectionLocation { get; set; }
		public DateTime ExpiryDate { get; set; }
		public TraceTogetherToken() { }
		public TraceTogetherToken(string sNo, string CollectLoc, DateTime expiredate)
		{
			SerialNo = sNo;
			CollectionLocation = CollectLoc;
			ExpiryDate = expiredate;
		}
		public bool IsEligibleForReplacement()
		{
			DateTime today = DateTime.Today;
			if (DateTime.Compare(ExpiryDate, today) < 0 ) // Not Correct 
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		public void ReplaceToken(string sNo, string colLoc)
		{
			SerialNo = sNo;
			CollectionLocation = colLoc;

		}

        public override string ToString()
        {
            return SerialNo;
        }

    }
}
