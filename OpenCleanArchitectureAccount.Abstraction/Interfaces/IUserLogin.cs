using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.Abstraction.Interfaces
{
    public interface IUserLogin
    {
        public string SubjectId { get; }
        public string Email { get; }
        public string UserName { get; }
    }
}
