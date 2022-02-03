using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApplicaiton.Database.Models
{
    public class SingleVote
    {
        public int Id { get; set; }

        public int VoteId { get; set; }
        public int VoteSide { get; set; }
        public string VoterNote { get; set; }
    }
}
