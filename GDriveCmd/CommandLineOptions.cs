using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDriveCmd
{
    class CommandLineOptions
    {
        [Option( 'u', "upload", HelpText = "Feltöltendő fájl." )]
        public string UploadFile { get; set; }

        [Option( 'd', "download", HelpText = "Leltöltendő fájl." )]
        public string DownloadFile { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild( this,
              ( HelpText current ) => HelpText.DefaultParsingErrorsHandler( this, current ) );
        }
    }
}
