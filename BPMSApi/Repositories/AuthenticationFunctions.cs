using BPMSApi.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMSApi.Repositories
{
    public class AuthenticationFunctions : BaseRepository
    {
        public AuthenticationFunctions(BPMSApiInstance instance) : base(instance)
        {
        }

        public async Task<Boolean> Register(User user)
        {
            var users = await Get<List<User>>("Users");

            if(users.Any(u => u.Username == user.Username))
            {
                return false;
            }

            users.Add(user);
            await Post<List<User>>("Users", users);
            return true;
        }

        public async Task<String> Login(User user)
        {
            var users = await Get<List<User>>("Users");
            return users.Where(u => u.Username == user.Username && u.Password == user.Password)?.FirstOrDefault()?.Name;
        }
    }
}
