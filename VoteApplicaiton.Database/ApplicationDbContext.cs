using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoteApplicaiton.Database.Models;

namespace VoteApplicaiton.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        // database model sets

        public DbSet<VoteModel> Votes { get; set; }
        public DbSet<SingleVote> SingleVotes { get; set; }
    }
}
