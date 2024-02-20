using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    internal class History
    {
        public string Equation;
        public string Result;
        public History() { }
        public History(string Equation, string Result)
        {
            this.Equation = Equation;
            this.Result = Result;
        }
            
    }
}
