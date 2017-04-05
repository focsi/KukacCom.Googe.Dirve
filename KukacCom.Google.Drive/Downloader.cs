using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KukacCom.Google.Drive
{
    public class Downloader : FileOperation
    {
        public string DriveFileName { get; set; }

        public string LocalFolder { get; set; }

        public Downloader( Drive drive, Folder parentFolder ) : base( drive, parentFolder )
        {

        }

        public void Download( bool overwrite )
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();

            var request = Drive.Service.Files.Get( ParentFolder.GetFileId( DriveFileName ) );
            request.Download( stream );

            string localPath = Path.Combine( LocalFolder, DriveFileName );
            System.IO.File.WriteAllBytes( localPath, stream.GetBuffer() );
        }
    }
}
