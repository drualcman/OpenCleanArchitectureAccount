using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCleanArchitectureAccount.Abstraction.Interfaces
{
    public interface ILoginInputPort
    {
        ValueTask Handle<TLogin>(TLogin login);
    }
}
