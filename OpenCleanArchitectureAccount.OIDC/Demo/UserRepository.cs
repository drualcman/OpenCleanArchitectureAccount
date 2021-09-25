using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.OIDC.Demo
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

        public bool ValidateCredentials(string username, string password)
        {
            bool result = false;
            CustomUser user = FindByUsername(username);
            if (user != null)
            {
                result = user.Password.Equals(password);
            }
            return result;
        }

        public CustomUser FindBySubjectId(string subjectId)
        {
            return _users.FirstOrDefault(x => x.SubjectId == subjectId);
        }

        public CustomUser FindByUsername(string username)
        {
            return _users.FirstOrDefault(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
    }

}
