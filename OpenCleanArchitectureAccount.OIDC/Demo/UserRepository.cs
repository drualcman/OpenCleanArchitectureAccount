using OpenCleanArchitectureAccount.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.OIDC.Demo
{
    public class UserRepository : IUserRepository<CustomUser>
    {        
        // some dummy data. Replce this with your user persistence. 
        private readonly List<CustomUser> _users = new List<CustomUser>
        {
            new CustomUser{
                SubjectId = "123",
                UserName = "user1",
                Password = "password1",
                Email = "damienbod@email.com"
            },
            new CustomUser{
                SubjectId = "321",
                UserName = "user2",
                Password = "password2",
                Email = "raphael@email.com"
            },
        };

        public Task<CustomUser> ValidateCredentials(string username, string password)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.UserName == username && x.Password == password));
        }

        public Task<CustomUser> FindBySubjectId(string subjectId)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.SubjectId == subjectId));
        }

        public Task<CustomUser> FindByUsername(string username)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)));
        }

    }

}
