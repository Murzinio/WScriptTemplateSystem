using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wScriptTemplates
{
    interface IFunction
    {
        string Name { get; set; }
        List<string> ParameterNames { get; set; }
        string Body { get; set; }
    }
}
