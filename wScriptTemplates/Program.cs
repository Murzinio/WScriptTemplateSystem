using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace wScriptTemplates
{
    class Program
    {
        private static string m_templateFolder = Directory.GetCurrentDirectory() + "\\templates";
        private static string m_templateFormat = "*.wst";

        private static string[] m_templateFilePaths;
        public static string[] TemplateFilePaths { get => m_templateFilePaths; }

        private static string m_sourceFolder = Directory.GetCurrentDirectory() + "\\source";
        private static string m_sourceFormat = "*.ws";

        private static string[] m_sourceFilePaths;
        public static string[] SourceFilePaths { get => m_sourceFilePaths; }

        private static Parser m_parser = new Parser();

        static void Main(string[] args)
        {
            if (!parseTemplates() || !parseSource())
            {
                return;
            }


        }

        /// <summary>
        /// Parses files in templates folder. 
        /// </summary>
        /// <returns>True on success, false if encounters any exceptions.</returns>
        static bool parseTemplates()
        {
            if (!Directory.Exists(m_templateFolder))
            {
                Console.WriteLine("'" + m_templateFolder + "'" + " folder not found!");
                return false;
            }

            m_templateFilePaths = Directory.GetFiles(m_templateFolder, m_templateFormat, SearchOption.AllDirectories).Select(x => Path.GetFullPath(x)).ToArray();

            if (m_templateFilePaths.Length == 0)
            {
                Console.WriteLine("No template files found!");
                Console.WriteLine("Expected file format: '" + m_templateFormat + "'");

                return false;
            }

            try
            {
                m_parser.parseTemplates(m_templateFilePaths);
            }
            catch (InvalidDataException e)
            {
                Console.WriteLine("Invalid template!");
                Console.WriteLine(e.Message);

                return false;
            }

            return true;
        }

        /// <summary>
        /// Parses files in source folder. 
        /// </summary>
        /// <returns>True on success, false if encounters any exceptions.</returns>
        static bool parseSource()
        {
            if (!Directory.Exists(m_sourceFolder))
            {
                Console.WriteLine("'" + m_sourceFolder + "'" + " folder not found!");
                return false;
            }

            m_sourceFilePaths = Directory.GetFiles(m_sourceFolder, m_sourceFormat, SearchOption.AllDirectories).Select(x => Path.GetFullPath(x)).ToArray();

            if (m_sourceFilePaths.Length == 0)
            {
                Console.WriteLine("No source files found!");
                Console.WriteLine("Expected file format: '" + m_sourceFormat + "'");

                return false;
            }

            try
            {
                m_parser.parseSource(m_sourceFilePaths);
            }
            catch (InvalidDataException e)
            {
                Console.WriteLine("Invalid script!");
                Console.WriteLine(e.Message);

                return false;
            }

            return true;
        }
    }
}
