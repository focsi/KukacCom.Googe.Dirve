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
                // Values are available here
                if( !string.IsNullOrEmpty( options.UploadFile ) && !string.IsNullOrEmpty( options.DestinationFolder ) )
                {
                    Drive drive = new Drive();
                    drive.Init();

                    Upload( drive, options );
                }
            }
        }

        private static void Upload( Drive drive, CommandLineOptions options )
        {
            Folder folder = new Folder()
            {
                Drive = drive
            };

            Uploader uploader = new Uploader()
            {
                Drive = drive,
                SourcePath = options.UploadFile,
                Description = options.DescriptionInfo,
                ParentId = folder.GetFolderId( options.DestinationFolder )
            };
            uploader.Upload( );
        }
    }
}
