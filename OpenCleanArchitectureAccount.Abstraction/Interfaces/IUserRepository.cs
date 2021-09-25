using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.Interfaces
{
    public interface IUserRepository<TUser>
    {
        Task<TUser> ValidateCredentials(string username, string password);

        Task<TUser> FindBySubjectId(string subjectId);

        Task<TUser> FindByUsername(string username);
    }
}
