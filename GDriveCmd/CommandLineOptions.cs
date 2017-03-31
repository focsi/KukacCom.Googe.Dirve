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
        [Option( 'u', "upload", HelpText = "File to upload." )]
        public string UploadFile { get; set; }

        [Option( 'd', "download", HelpText = "File to download." )]
        public string DownloadFile { get; set; }

        [Option( 'f', "destfolder", HelpText = "Destination folder." )]
        public string DestinationFolder { get; set; }

        [Option( 'i', "info", HelpText = "Description info of uploaded file" )]
        public string DescriptionInfo { get; set; }

        [Option( 'o', "info", HelpText = "Overwrite destination if file exists" )]
        public bool OverWrite { get; set; }

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
