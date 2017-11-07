using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wScriptTemplates
{
    class Generator
    {
        private List<FunctionTemplate> m_functionTemplates = new List<FunctionTemplate>();
        private List<FunctionSource> m_sourceFunctions = new List<FunctionSource>();

        public void generate(List<FunctionTemplate> functionTemplates,
            List<FunctionSource> sourceFunctions)
        {
            m_functionTemplates = functionTemplates;
            m_sourceFunctions = sourceFunctions;
        }
    }
}
