﻿using KukacCom.Google.Drive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GDriveCmd
{
    class Program
    {
        static void Main( string[] args )
        {
            if ( args.Length == 1 )
            {
                Drive drive = new Drive();
                drive.Init();

                UploadTest( drive, args[0] );
            }
        }

        private static void UploadTest( Drive drive, string sourcePath )
        {
            Folder folder = new Folder()
            {
                Drive = drive
            };

            Uploader uploader = new Uploader()
            {
                Drive = drive,
                SourcePath = sourcePath,
                Description = "Teszt",
                ParentId = folder.GetFolderId( "Munka" )
            };
            uploader.Upload( );
        }

        private static void FolderTest( Drive drive )
        {
            Folder folder = new Folder()
            {
                Drive = drive
            };
            folder.GetFolderId( "Munka" );
        }
    }
}
