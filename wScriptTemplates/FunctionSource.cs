using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wScriptTemplates
{
    class FunctionTemplateCall
    {
        public FunctionTemplateCall(string name, 
            List<string> arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public string Name { get; set; }
        public List<string> Arguments { get; set; }
    }

    class FunctionSource : IFunction
    {
        public FunctionSource(string name, 
            List<string> parameterNames,
            List<FunctionTemplateCall> calls,
            string body)
        {
            Name = name;
            ParameterNames = parameterNames;
            Body = body;
            TemplateCalls = calls;
        }

        public string Name { get; set; }
        public List<string> ParameterNames { get; set; }
        public string Body { get; set; }

        public List<FunctionTemplateCall> TemplateCalls { get; set; } = new List<FunctionTemplateCall>();
    }
}
