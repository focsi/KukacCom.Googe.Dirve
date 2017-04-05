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


        public Downloader( Drive drive, Folder parentFolder ) : base( drive, parentFolder )
        {

        }

        public void Download( bool overwrite )
        {
            string localPath = Path.Combine( LocalFolder, FileName );
            if( File.Exists( localPath ) && !overwrite )
                throw new FileExistsButNoOverwriteExeption();

            System.IO.MemoryStream stream = new System.IO.MemoryStream();

            var request = Drive.Service.Files.Get( ParentFolder.GetFileId( FileName ) );
            request.Download( stream );

            System.IO.File.WriteAllBytes( localPath, stream.GetBuffer() );
        }
    }
}
