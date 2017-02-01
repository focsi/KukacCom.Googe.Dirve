using Google.Apis.Drive.v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace KukacCom.Google.Drive
{
    public class Folder : OperationBase
    {
        public string GetFolderId( string path )
        {
            var folders = path.Split( '/' );
            string parentId = "root";
            foreach ( var folder in folders )
            {
                parentId = GetFolderId( parentId, folder );
            }
            return parentId;
        }

        public string GetFolderId( string parentId, string title )
        {

            string pageToken = String.Empty;
            do
            {
                var request = Drive.Service.Files.List();
                //request.Q = "\"root\" in parents and trashed = false and mimeType = \"application/vnd.google-apps.folder\"";
                request.Q = "\"" + parentId + "\" in parents and trashed = false and mimeType = \"application/vnd.google-apps.folder\"";
                //request.Q = "\"0B-bnPfjeo_w8V2hLd2xWTHpjM0E\" in parents and trashed = false";
                //request.Q = " trashed = false";
                request.MaxResults = 100;
                request.PageToken = pageToken;
                var response = request.Execute();

                var folderId = response.Items.Where( f => f.Title == title ).Select( f => f.Id ).FirstOrDefault();
                if ( !String.IsNullOrWhiteSpace( folderId ) )
                    return folderId;

                pageToken = response.NextPageToken;
            } while ( !String.IsNullOrWhiteSpace( pageToken ) );

            return null;    
        }

    }

//       function listRootFolders( DriveService Drive  ) {
//  var query = '"root" in parents and trashed = false and ' +
//      'mimeType = "application/vnd.google-apps.folder"';
//  var folders, pageToken;
//  do {
//    folders = Drive.Files.List({
//      q: query,
//      maxResults: 100,
//      pageToken: pageToken
//    });
//    if (folders.items && folders.items.length > 0) {
//      for (var i = 0; i < folders.items.length; i++) {
//        var folder = folders.items[i];
//        Logger.log('%s (ID: %s)', folder.title, folder.id);
//      }
//    } else {
//      Logger.log('No folders found.');
//    }
//    pageToken = folders.nextPageToken;
//  } while (pageToken);
//}
}
