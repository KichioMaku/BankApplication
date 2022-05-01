using BankApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Reader
{
    class CsvUserReader : IReader
    {
        public List<string> GetCsvLines(string file)
        {
            return System.IO.File.ReadAllLines(file).ToList();
        }
        public List<object> ReadFile(string file)
        {
            List<object> list = new List<object>();
            var lines = GetCsvLines(file);
            var headerLine = lines.First();
            var rows = lines.Skip(1);
            throw new NotImplementedException();
        }
    }
}
