using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.WebUI.Models;
using Newtonsoft.Json;


namespace WebApp.WebUI.Store
{
    public class VoteStore
    {
        public Dictionary<User, int> Votes { get; } = new Dictionary<User, int>();

        private Dictionary<string, bool> VotedUsers { get; set;  } = new Dictionary<string, bool>();

        public VoteStore(List<User> bs)
        {
            foreach (User u in bs)
            {
                Votes.Add(u, 0);
            }
        }

        public void AddCandidate(User user)
        {
            Votes.Add(user, 0);
        }

        public bool AddVote(string userGuid, string blankGuid)
        {
            if (VotedUsers.ContainsKey(userGuid))
            {
                return false;
            }
            VotedUsers.Add(userGuid, true);
            var item = Votes.Where(i => i.Key.Guid.Equals(blankGuid)).ElementAt(0);
            Votes[item.Key] += 1;
            return true;
        }

        public Dictionary<string, int> GetVotes()
        {
            Dictionary<string, int> tmp = new Dictionary<string, int>();

            foreach (var item in Votes)
            {
                tmp.Add(item.Key.Name, item.Value);
            }

            return tmp;
        }

    }
}
