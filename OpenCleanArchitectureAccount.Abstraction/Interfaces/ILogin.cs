using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.Abstraction.Interfaces
{
    public interface ILogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
