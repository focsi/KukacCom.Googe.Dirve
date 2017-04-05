using Google.Apis.Drive.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KukacCom.Google.Drive
{
    public class FileOperation : OperationBase
    {
        protected Folder ParentFolder { get; set; }

        public string FileName { get; set; }

        public string LocalFolder { get; set; }

        public FileOperation( Drive drive, Folder parentFolder ) : base(drive)
        {
            if( parentFolder == null )
                throw new ArgumentNullException( "parentFolder" );

            ParentFolder = parentFolder;
        }
    }
}
