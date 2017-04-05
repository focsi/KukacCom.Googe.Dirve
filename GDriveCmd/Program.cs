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
                    if( options.UploadMode )
                        Upload( options );
                    else if( options.DownloadMode )
                        Download( options );
                    else
                        Console.WriteLine( options.GetUsage() );
                }
                else
                    Console.WriteLine( options.GetUsage() );
            }
        }

        private static void Upload( CommandLineOptions options )
        {
            Drive drive = new Drive();
            drive.Init();

            Folder folder = new Folder( drive, options.DestinationFolder );

            Uploader uploader = new Uploader( drive, folder )
            {
                SourcePath = options.SourceFile,
                Description = options.DescriptionInfo
            };
            //if ( !uploader.IsExists() )
            uploader.Upload( options.OverWrite );
        }

        private static void Download( CommandLineOptions options )
        {
            Drive drive = new Drive();
            drive.Init();

            Folder folder = new Folder( drive, System.IO.Path.GetDirectoryName( options.SourceFile ) );
            Downloader downloader = new Downloader( drive, folder )
            {
                DriveFileName = System.IO.Path.GetFileName( options.SourceFile )
            };
            downloader.Download( options.OverWrite );
        }
    }
}
