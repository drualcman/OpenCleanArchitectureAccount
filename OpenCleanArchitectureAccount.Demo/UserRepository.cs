using OpenCleanArchitectureAccount.Abstraction.Interfaces;
using OpenCleanArchitectureAccount.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.Demo
{
    public class UserRepository : IUserRepository
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

        public Task<IUserLogin> FindBySubjectId(string subjectId)
        {
            IUserLogin user = _users.FirstOrDefault(x => x.SubjectId == subjectId);
            return Task.FromResult(user);
        }

        public Task<IUserLogin> FindByUsername(string username)
        {
            IUserLogin user = _users.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(user);
        }

        public Task<IUserLogin> ValidateCredentials(ILogin login)
        {
            IUserLogin user = _users.FirstOrDefault(x => x.UserName == login.UserName && x.Password == login.Password);
            return Task.FromResult(user);
        }

    }

}
