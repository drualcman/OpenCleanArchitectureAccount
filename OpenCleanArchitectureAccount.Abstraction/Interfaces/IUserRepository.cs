using OpenCleanArchitectureAccount.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.Interfaces
{
    public interface IUserRepository
    {
        Task<IUserLogin> ValidateCredentials(ILogin login);

        Task<IUserLogin> FindBySubjectId(string subjectId);

        Task<IUserLogin> FindByUsername(string username);
    }
}
