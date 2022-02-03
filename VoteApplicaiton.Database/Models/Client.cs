using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApplicaiton.Database.Models
{
    public class Client
    {
        public string ConnectionId { get; set; }

        public int LookingVoteId { get; set; }
        public bool isVotedBefore { get; set; }
    }
}
