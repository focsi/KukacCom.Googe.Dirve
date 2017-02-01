using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
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

        public string Description 
        { 
            set
            {
                m_Body.Description = value;
            }
        }
        public string SourcePath { get; set; }
        public string ParentId { get; set; }

        public void Upload()
        {
            m_Body.Title = System.IO.Path.GetFileName( SourcePath );
            
            m_Body.MimeType = MimeType;

            SetParentId( m_Body );

            byte[] byteArray = System.IO.File.ReadAllBytes( SourcePath );
            System.IO.MemoryStream stream = new System.IO.MemoryStream( byteArray );

            FilesResource.InsertMediaUpload request = Drive.Service.Files.Insert( m_Body, stream, MimeType );
            request.Upload();
        }

        private void SetParentId( File body )
        {
            if ( !String.IsNullOrEmpty( ParentId ) )
            {
                body.Parents = new List<ParentReference>() { new ParentReference() { Id = ParentId } };
            }
        }


 
    }
}
