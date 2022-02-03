
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoteApplicaiton.Database;
using VoteApplicaiton.Database.Models;

namespace VoteApplication.UI.Hubs
{
    public class VoteHub : Hub
    {
        public static List<Client> ClientList { get; } = new List<Client>();
        private readonly ApplicationDbContext _ctx;

        public VoteHub(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task VoteToSelect(string slug, int VoteSelect)
        {
            VoteModel vote = _ctx.Votes.FirstOrDefault(v => v.Slug == slug);
            Client client = ClientList.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);
            TimeSpan timeUntilEnd = vote.EndDate - DateTime.Now;
            if (!client.isVotedBefore && timeUntilEnd.TotalMinutes > 0)
            {
                if (VoteSelect == 1)
                {
                    vote.VoteSelect1Count++;
                    vote.TotalVote++;

                    await _ctx.SaveChangesAsync();
                    await ValuesUpdated(vote.Slug);
                    await Clients.Caller.SendAsync("voteResponse", VoteSelect);
                }
                else if (VoteSelect == 2)
                {
                    vote.VoteSelect2Count++;
                    vote.TotalVote++;

                    await _ctx.SaveChangesAsync();
                    await ValuesUpdated(vote.Slug);
                    await Clients.Caller.SendAsync("voteResponse", VoteSelect);
                }
                else { return; }

                client.isVotedBefore = true;
            }
        }

        public async Task ReportSlug(string slug, string oldVote)
        {
            int oldVoteInt = Convert.ToInt16(oldVote);
            Client oldClient = ClientList.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);
            VoteModel vote = _ctx.Votes.FirstOrDefault(v => v.Slug == slug);
            if (vote == null || oldClient != null) { return; }
            TimeSpan timeUntilEnd = vote.EndDate - DateTime.Now;
            bool isTimeLeft = timeUntilEnd.TotalMinutes > 0;
            bool votedBefore = false;
            if (oldVoteInt == 1 || oldVoteInt == 2)
            {
                votedBefore = true;
                await Clients.Caller.SendAsync("voted", true, oldVoteInt, isTimeLeft);
            }
            else
            {
                await Clients.Caller.SendAsync("voted", false, 0, isTimeLeft);
            }

            Client client = new Client { ConnectionId = Context.ConnectionId, LookingVoteId = vote.Id, isVotedBefore = votedBefore };
            ClientList.Add(client);
        }

        public async Task ValuesUpdated(string slug)
        {
            VoteModel vote = _ctx.Votes.FirstOrDefault(v => v.Slug == slug);
            IEnumerable<string> clients = ClientList.FindAll(c => c.LookingVoteId == vote.Id).Select(c => c.ConnectionId);

            int allVotes = vote.TotalVote;
            int section1Votes = vote.VoteSelect1Count;
            int section2Votes = vote.VoteSelect2Count;

            await Clients.Clients(clients).SendAsync("updateValue", allVotes, section1Votes, section2Votes);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("clientConnected");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Client disconnectedClient = ClientList.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);
            if (disconnectedClient == null) { return; }

            ClientList.Remove(disconnectedClient);
        }

    }
}
