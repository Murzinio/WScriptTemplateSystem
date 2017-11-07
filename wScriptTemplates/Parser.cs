using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace wScriptTemplates
{
    class Parser
    {
        // function templates must match this exact syntax for simplicity (at least for now)
        // group1: function template name
        // group2: parameters
        // group3: return type (optional)

        private static string m_functionTemplatePrefix 
            = "_g_";

        private static string m_functionTemplatePattern = 
            @"function template " + m_functionTemplatePrefix + @"(\w+)(\((?:T \w+,* *)*\))(?: : (T))*";

        // wScript function
        // group1: function template name
        private static string m_functionSourcePattern = 
            @"function\s([a-zA-Z\d]+)\(";

        private static string[] m_acceptedTypes = {
            "int", "float", "name", "string"
        };

        private List<FunctionTemplate> m_functionTemplates = new List<FunctionTemplate>();
        internal List<FunctionTemplate> FunctionTemplates { get => m_functionTemplates;}

        private List<FunctionSource> m_sourceFunctions = new List<FunctionSource>();
        internal List<FunctionSource> SourceFunctions { get => m_sourceFunctions; }

        // function template call, like _g_hudMessage("string");
        // group1: function template name
        // group2: arguments
        private static string m_functionTemplateCallPattern = 
            m_functionTemplatePrefix + @"([a-zA-Z0-9]+)\((.+)\)";

        public void parseTemplates(string[] filePaths)
        {
            string currentFile;

            foreach(var filePath in filePaths)
            {
                currentFile = File.ReadAllText(filePath);

                if (currentFile.Length == 0)
                {
                    throw new 
                        InvalidDataException("Empty file: '" + filePath + "'");
                }

                extractFunctionTemplates(currentFile, filePath);
            }
        }

        public void parseSource(string[] filePaths)
        {
            string currentFile;

            foreach (var filePath in filePaths)
            {
                currentFile = File.ReadAllText(filePath);

                if (currentFile.Length == 0)
                {
                    throw new
                        InvalidDataException("Empty file: '" + filePath + "'");
                }

                extractSourceFunctions(currentFile, filePath);
            }
        }

        private void extractFunctionTemplates(string file, string filePath)
        {
            var regex = new Regex(m_functionTemplatePattern);

            MatchCollection matches = Regex.Matches(file, m_functionTemplatePattern);

            if (matches.Count == 0)
            {
                throw new 
                    InvalidDataException("No valid function templates found in file: '" + filePath);
            }

            foreach(Match match in matches)
            {
                var parameters = match.Groups[2].Value.Split(',').ToList();

                for (int i = 0; i < parameters.Count; ++i)
                {
                    parameters[i] = parameters[i].Split(' ').Last();
                    parameters[i] = parameters[i].Remove(parameters[i].Length- 1);
                }

                    int currentIndex = match.Index;
                    char currentChar = file[currentIndex];

                while (currentChar != '{')
                {
                    ++currentIndex;
                    currentChar = file[currentIndex];
                }

                // skip opening brace
                ++currentIndex;
                currentChar = file[currentIndex];

                string rawFunction = "";

                while (currentChar != '}')
                {
                    rawFunction += currentChar;

                    ++currentIndex;
                    currentChar = file[currentIndex];
                }

                    m_functionTemplates.Add(
                new FunctionTemplate(match.Groups[1].Value,
                parameters,
                rawFunction));
            }
        }

        private void extractSourceFunctions(string file, string filePath)
        {
            var regex = new Regex(m_functionSourcePattern);
            
            MatchCollection matches = Regex.Matches(file, m_functionSourcePattern);

            if (matches.Count == 0)
            {
                throw new
                    InvalidDataException("No valid functions found in file: '" + filePath);
            }

            foreach (Match match in matches)
            {
                int currentIndex = match.Index;
                char currentChar = file[currentIndex];

                while (currentChar != '{')
                {
                    ++currentIndex;
                    currentChar = file[currentIndex];
                }

                // skip opening brace
                ++currentIndex;
                currentChar = file[currentIndex];

                string rawFunction = ""; 

                while (currentChar != '}')
                {
                    rawFunction += currentChar;

                    ++currentIndex;
                    currentChar = file[currentIndex];
                }

                // get template calls
                

                var callRegex = new Regex(m_functionTemplateCallPattern);
                MatchCollection callMatches = Regex.Matches(file, m_functionTemplateCallPattern);

                var calls = new List<FunctionTemplateCall>();

                foreach (Match callMatch in callMatches)
                {
                    var templateName = callMatch.Groups[1].Value;

                    bool templateExists = false;
                    foreach(var template in m_functionTemplates)
                    {
                        if (template.Name == templateName)
                        {
                            templateExists = true;
                            break;
                        }
                    }

                    if (!templateExists)
                    {
                        throw
                            new InvalidDataException("Template doesn't exist: " + templateName);
                    }

                    
                   
                    calls.Add(
                        new FunctionTemplateCall(templateName,
                        callMatch.Groups[2].Value.Split(',').ToList())
                    );
                }
                var function = 
                    new FunctionSource(match.Groups[1].Value,
                        new List<String>(),
                        calls,
                        rawFunction
                    );

                m_sourceFunctions.Add(function);
            }
        }
    }
}
