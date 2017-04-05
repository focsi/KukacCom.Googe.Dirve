using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KukacCom.Google.Drive
{
    public class Uploader : FileOperation
    {
        private const string MimeType = "";
        private File m_Body = new File();

        public Uploader( Drive drive, Folder parentFolder ) : base( drive, parentFolder )
        {

        }

        public string Description 
        { 
            set
            {
                m_Body.Description = value;
            }
        }
        public string SourcePath { get; set; }


        public void Upload( bool overwrite )
        {
            m_Body.Name = System.IO.Path.GetFileName( SourcePath );
            
            m_Body.MimeType = MimeType;

            SetParentId( m_Body );

            byte[] byteArray = System.IO.File.ReadAllBytes( SourcePath );
            System.IO.MemoryStream stream = new System.IO.MemoryStream( byteArray );

            string id = ParentFolder.GetFileId( System.IO.Path.GetFileName( SourcePath ) );
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
            if( !String.IsNullOrEmpty( ParentFolder.FolderId ) )
            {
                body.Parents = new List<string>() { ParentFolder.FolderId };
            }
        }

        private bool IsValidId( string id )
        {
            return !String.IsNullOrWhiteSpace( id );
        }
    }
}
