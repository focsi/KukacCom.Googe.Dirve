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
        private Folder ParentFolder { get; set; }

        public FileOperation( Drive drive, Folder parentFolder ) : base(drive)
        {
            if( parentFolder == null )
                throw new ArgumentNullException( "parentFolder" );

            ParentFolder = parentFolder;
        }

        protected void SetParentId( File body )
        {
            if( !String.IsNullOrEmpty( ParentFolder.FolderId ) )
            {
                body.Parents = new List<string>() { ParentFolder.FolderId };
            }
        }

        protected string GetFileId( string title )
        {

            string pageToken = String.Empty;
            do
            {
                var request = Drive.Service.Files.List();
                request.Q = "\"" + ParentFolder.FolderId + "\" in parents and trashed = false and mimeType != \"application/vnd.google-apps.folder\"";
                request.PageSize = 100;
                request.PageToken = pageToken;
                var response = request.Execute();

                var id = response.Files.Where( f => f.Name == title ).Select( f => f.Id ).FirstOrDefault();
                if( !String.IsNullOrWhiteSpace( id ) )
                    return id;

                pageToken = response.NextPageToken;
            } while( !String.IsNullOrWhiteSpace( pageToken ) );

            return null;
        }

        protected bool IsValidId( string id )
        {
            return !String.IsNullOrWhiteSpace( id );
        }

    }
}
