using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VoteApplicaiton.Database;
using VoteApplicaiton.Database.Models;
using VoteApplication.UI.Application;
using VoteApplication.UI.Models;

namespace VoteApplication.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public HomeController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        Generetors generetors = new Generetors();

        [Route("")]
        public IActionResult Index()
        {
            // puts the clients old created vote slugs to variable
            IndexViewModel viewModel = new IndexViewModel();
            List<string> ownedByClient = null;
            List<VoteModel> votes = new List<VoteModel>();
            if (HttpContext.Request.Cookies["owned"] != null)
            {
                ownedByClient = Request.Cookies["owned"].Split('\u002C').ToList();

                ownedByClient.ForEach(slug =>
                {
                    VoteModel vote = _ctx.Votes.FirstOrDefault(v => v.Slug == slug);
                    if (vote != null)
                    {
                        votes.Add(vote);
                        viewModel.Votes = votes;
                    }
                });
            }
            return View(viewModel);
        }


        [Route("startvote")]
        public async Task<IActionResult> StartVote(string VoteName, string VoteSelect1, string VoteSelect2, int timeToEnd)
        {
            if (timeToEnd > 168) { timeToEnd = 168; }
            DateTime endingTime = DateTime.Now.AddHours(timeToEnd);

            string slug = generetors.GenereteRandomSlug(5);

            VoteModel vote = new VoteModel
            {
                VoteTitle = VoteName,
                VoteSelect1 = VoteSelect1,
                VoteSelect2 = VoteSelect2,
                EndDate = endingTime,
                StartDate = DateTime.Now,
                Slug = slug,
            };

            await _ctx.Votes.AddAsync(vote);
            await _ctx.SaveChangesAsync();

            List<string> ownedByClient = null;
            if (HttpContext.Request.Cookies["owned"] != null)
            {
                ownedByClient = Request.Cookies["owned"].Split('\u002C').ToList();
                ownedByClient.Add(slug);


                string stringOwned = "";
                ownedByClient.ForEach(slug =>
                {
                    stringOwned += slug + ",";
                });
                stringOwned = stringOwned.Remove(stringOwned.Length - 1); // deletes last character which is extra ","

                HttpContext.Response.Cookies.Append("owned", stringOwned);
            }
            else
            {
                HttpContext.Response.Cookies.Append("owned", slug);
            }

            return RedirectToAction("Vote", new { slug = vote.Slug });
        }

        [Route("vote/{slug}")]
        public IActionResult Vote(string slug)
        {
            VoteModel vote = _ctx.Votes.FirstOrDefault(v => v.Slug == slug);
            VoteViewModel viewModel = new VoteViewModel();
            if (vote == null)
            {
                viewModel.isFound = false;
            }
            else
            {
                TimeSpan daysLeft = vote.EndDate - DateTime.Now;
                viewModel.isFound = true;
                viewModel.Vote = vote;
                viewModel.DaysLeft = daysLeft.Days;
                viewModel.HoursLeft = daysLeft.Hours;
                viewModel.MinutesLeft = daysLeft.Minutes;
                //viewModel.isTimeLeft = daysLeft.TotalMinutes > 0 ? true : false;
            }
            return View(viewModel);
        }
    }
}
