using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wScriptTemplates
{
    class FunctionTemplateCall
    {
        public FunctionTemplateCall(string name, List<string> arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public string Name { get; set; }
        public List<string> Arguments { get; set; }
    }

    class FunctionSource : IFunction
    {
        public string Name { get; set; }
        public List<string> ParameterNames { get; set; }

        public List<FunctionTemplateCall> TemplateCalls { get; set; } = new List<FunctionTemplateCall>();
    }
}
