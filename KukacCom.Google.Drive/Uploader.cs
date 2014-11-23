using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KukacCom.Google.Drive
{
    public class Uploader
    {
        private const string MimeType = "";

        public string Description { get; set; }
        public string SourcePath { get; set; }
        
        public string FileName 
        {
            get
            {
                return System.IO.Path.GetFileName( SourcePath );
            }
        }

        public void Upload( DriveService service )
        {
            File body = new File();
            body.Title = FileName;
            body.Description = Description;
            body.MimeType = MimeType;

            byte[] byteArray = System.IO.File.ReadAllBytes( SourcePath );
            System.IO.MemoryStream stream = new System.IO.MemoryStream( byteArray );

            FilesResource.InsertMediaUpload request = service.Files.Insert( body, stream, MimeType );
            request.Upload();
        }
        /*    // Set the parent folder.
    if (!String.IsNullOrEmpty(parentId)) {
      body.Parents = new List<ParentReference>()
          {new ParentReference() {Id = parentId}};
    }
*/
    }
}
