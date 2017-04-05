using KukacCom.Google.Drive;
using System;
using System.Collections.Generic;
using System.IO;
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
                        Console.WriteLine( "You have to use --download or --upload switch" );
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
                FileName = System.IO.Path.GetFileName( options.SourceFile ),
                LocalFolder = Path.GetDirectoryName( options.SourceFile ),
                Description = options.DescriptionInfo
            };

            try
            {
                uploader.Upload( options.OverWrite );
                Console.WriteLine( "File is uploaded." );

            }
            catch( Exception ex )
            {
                Console.WriteLine( $"Error: {ex.Message}" );
            }
        }

        private static void Download( CommandLineOptions options )
        {
            Drive drive = new Drive();
            drive.Init();

            Folder folder = new Folder( drive, System.IO.Path.GetDirectoryName( options.SourceFile ) );
            Downloader downloader = new Downloader( drive, folder )
            {
                FileName = System.IO.Path.GetFileName( options.SourceFile ),
                LocalFolder = options.DestinationFolder
            };
            try
            {
                downloader.Download( options.OverWrite );
                Console.WriteLine( "File is downloaded." );
            }
            catch( FileExistsButNoOverwriteExeption )
            {
                Console.WriteLine( "File is exists in destination folder. If you want overwrite it use --overwrite switch!" );
            }
            catch( Exception ex )
            {
                Console.WriteLine( $"Error: {ex.Message}" );
            }
        }
    }
}
