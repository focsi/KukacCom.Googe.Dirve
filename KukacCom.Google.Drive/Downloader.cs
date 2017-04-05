using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KukacCom.Google.Drive
{
    public class Downloader : FileOperation
    {
        public string DriveFileName { get; set; }

        public Downloader( Drive drive, Folder parentFolder ) : base( drive, parentFolder )
        {

        }

        public void Download( bool overwrite )
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();

            string id = GetFileId( DriveFileName );

            var request = Drive.Service.Files.Get( id );
            request.Download( stream );
            System.IO.File.WriteAllBytes( DriveFileName, stream.GetBuffer() );

        }
    }
}
