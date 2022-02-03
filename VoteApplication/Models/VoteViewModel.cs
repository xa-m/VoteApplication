

using System;
using VoteApplicaiton.Database.Models;

namespace VoteApplication.UI.Models
{
    public class VoteViewModel
    {
        public bool isFound { get; set; }
        public VoteModel Vote { get; set; }

        public int DaysLeft { get; set; }
        public int HoursLeft { get; set; }
        public int MinutesLeft { get; set; }
        public bool isTimeLeft { get; set; }
    }
}
