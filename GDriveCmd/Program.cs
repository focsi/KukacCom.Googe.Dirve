using KukacCom.Google.Drive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GDriveCmd
{
    class Program
    {
        static void Main( string[] args )
        {
            var options = new CommandLineOptions();
            if( CommandLine.Parser.Default.ParseArguments( args, options ) )
            {
                if( !string.IsNullOrEmpty( options.SourceFile ) && !string.IsNullOrEmpty( options.DestinationFolder ) )
                {
                    Drive drive = new Drive();
                    drive.Init();
                    if( options.UploadMode )
                        Upload( drive, options );
                }
            }
        }

        private static void Upload( Drive drive, CommandLineOptions options )
        {
            Folder folder = new Folder( drive, options.DestinationFolder );

            Uploader uploader = new Uploader( drive, folder )
            {
                SourcePath = options.SourceFile,
                Description = options.DescriptionInfo
            };
            //if ( !uploader.IsExists() )
            uploader.Upload( options.OverWrite );
        }
    }
}
