using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoteApplicaiton.Database.Models
{
    public class VoteModel
    {
        public int Id { get; set; }

        public int TotalVote { get; set; }
        public string VoteTitle { get; set; }
        public string Slug { get; set; }

        public string VoteSelect1 { get; set; }
        public int VoteSelect1Count { get; set; }

        public string VoteSelect2 { get; set; }
        public int VoteSelect2Count { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
    }
}
