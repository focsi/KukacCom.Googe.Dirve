using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KukacCom.Google.Drive
{
    public class Uploader : OperationBase
    {
        private const string MimeType = "";
        private File m_Body = new File();

        public Uploader( Drive drive, Folder parentFolder ) : base(drive)
        {
            if( parentFolder == null )
                throw new ArgumentNullException( "parentFolder" );

            ParentFolder = parentFolder;
        }

        public string Description 
        { 
            set
            {
                m_Body.Description = value;
            }
        }
        public string SourcePath { get; set; }
        public Folder ParentFolder { get; private set; }

        public void Upload( bool overwrite )
        {
            m_Body.Name = System.IO.Path.GetFileName( SourcePath );
            
            m_Body.MimeType = MimeType;

            SetParentId( m_Body );

            byte[] byteArray = System.IO.File.ReadAllBytes( SourcePath );
            System.IO.MemoryStream stream = new System.IO.MemoryStream( byteArray );

            string id = GetFileId( System.IO.Path.GetFileName( SourcePath ) );
            if( IsValidId( id ) && overwrite )
            {
                var deletRequest = Drive.Service.Files.Delete( id );
                deletRequest.Execute();
            }
            var request = Drive.Service.Files.Create( m_Body, stream, MimeType );
            request.Upload();
        }

        private void SetParentId( File body )
        {
            if ( !String.IsNullOrEmpty( ParentFolder.FolderId ) )
            {
                body.Parents = new List<string>() { ParentFolder.FolderId };
            }
        }

        private bool IsValidId( string id )
        {
            return !String.IsNullOrWhiteSpace( id );
        }

        public bool IsExists()
        {
            return IsValidId( GetFileId( System.IO.Path.GetFileName( SourcePath ) ) );
        }

        public string GetFileId( string title )
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


    }
}
