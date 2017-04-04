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
        [ValueOption( 0 )]
        public string SourceFile { get; set; }

        [ValueOption(1 )]
        public string DestinationFolder { get; set; }

        [Option( 'd', "download", HelpText = "Download mode", DefaultValue = false )]
        public bool DownloadMode { get; set; }

        [Option( 'u', "upload", HelpText = "Upload mode", DefaultValue = true )]
        public bool UploadMode { get; set; }

        [Option( 'i', "info", HelpText = "Description info of uploaded file" )]
        public string DescriptionInfo { get; set; }

        [Option( 'o', "overwrite", HelpText = "Overwrite destination if file exists" )]
        public bool OverWrite { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = HelpText.AutoBuild( this,
              ( HelpText current ) => HelpText.DefaultParsingErrorsHandler( this, current ) );
            help.AddPreOptionsLine( "" );
            help.AddPreOptionsLine( "Usage: GDriveCmd.exe source_file destination_folder [switches]" );
            return help;
        }
    }
}
