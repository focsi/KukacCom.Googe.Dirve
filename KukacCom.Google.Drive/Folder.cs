using Google.Apis.Drive.v3;
using System;
using System.Linq;


namespace KukacCom.Google.Drive
{
    public class Folder : OperationBase
    {
        public string FolderId { get; private set; }

        public Folder( Drive drive, string path ) : base( drive )
        {
            FolderId = GetFolderId( path );
        }

        private string GetFolderId( string path )
        {
            var folders = path.Split( '/' );
            string parentId = "root";
            foreach( var folder in folders )
            {
                parentId = GetFolderId( parentId, folder );
            }
            return parentId;
        }

        private string GetFolderId( string parentId, string title )
        {

            string pageToken = String.Empty;
            do
            {
                var request = Drive.Service.Files.List();
                //request.Q = "\"root\" in parents and trashed = false and mimeType = \"application/vnd.google-apps.folder\"";
                request.Q = "\"" + parentId + "\" in parents and trashed = false and mimeType = \"application/vnd.google-apps.folder\"";
                //request.Q = "\"0B-bnPfjeo_w8V2hLd2xWTHpjM0E\" in parents and trashed = false";
                //request.Q = " trashed = false";
                request.PageSize = 100;
                request.PageToken = pageToken;
                var response = request.Execute();

                var folderId = response.Files.Where( f => f.Name == title ).Select( f => f.Id ).FirstOrDefault();
                if( !String.IsNullOrWhiteSpace( folderId ) )
                    return folderId;

                pageToken = response.NextPageToken;
            } while( !String.IsNullOrWhiteSpace( pageToken ) );

            return null;
        }

        public string GetFileId( string title )
        {

            string pageToken = String.Empty;
            do
            {
                var request = Drive.Service.Files.List();
                request.Q = "\"" + FolderId + "\" in parents and trashed = false and mimeType != \"application/vnd.google-apps.folder\"";
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
