using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KukacCom.Google.Drive
{
    public class Drive
    {
        public DriveService Service { get; private set; }
    

        public void Init()
        {
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = "325522869377-g7i7e3nvhkhvnbtop1fgagimte79equc.apps.googleusercontent.com",
                    ClientSecret = "hIyVhPWht_mMq6dEBQB7DuyD",
                },
                new[] { DriveService.Scope.Drive },
                "user",
                CancellationToken.None 
            ).Result;

            // Create the service.
            Service = new DriveService( new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GDrive client",
            } );
        }
    }
}
