using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wScriptTemplates
{
    class FunctionTemplate : IFunction
    {
        public FunctionTemplate(string name, 
            List<string> parameterNames,
            string body)
        {
            Name = name;
            ParameterNames = parameterNames;
            Body = body;
        }

        public string Name { get; set; }
        public List<string> ParameterNames { get; set; }
        public string Body { get; set; }

        public bool Void { get; set; }
        public string requiredType { get; set; }
    }
}
