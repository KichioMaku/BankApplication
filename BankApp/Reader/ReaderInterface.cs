using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Reader
{
    public interface IReader
    {
        List<Object> ReadFile(string file);
    }
}
