using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace deployViaFtp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 5)
            {
                Console.WriteLine("You need 5 arguments: ");
                Console.WriteLine(
                    "1 - The source folder for the front end"+
                    "2 - The ftp username" +
                    "3 - The ftp password"+
                    "4 - The destination ftpfolder above wwwroot"+
                    "5 - Additional root level files source folder (for web.config, favicon.ico etc)"
                    +"6 - The source folder where the publised api is found"
                    +"7 - The database connection string for the api"
                    );
                return;
            }
            var sourceFolder = args[0];
            var userName = args[1];
            var password = args[2];
            var ftpRootFolder = args[3];
            var additionalRootFolderItemsFolder = args[4];
            var apiSourceFolder = args[5];
            var apiConnectionString = args[6];

            var apiWebConfigFile = File.ReadAllText(apiSourceFolder + "\\web.config");
            apiWebConfigFile = apiWebConfigFile.Replace("%CONNECTION_STRING%", apiConnectionString);
            File.WriteAllText(apiSourceFolder + "\\web.config",apiWebConfigFile);

            var credentials = new NetworkCredential(userName, password);
            
            //DeleteFolder(ftpRootFolder, credentials, "nextwwwroot");
            UploadFolder(ftpRootFolder, credentials, "nextwwwroot", sourceFolder,true);
            UploadFolder(ftpRootFolder, credentials, "nextwwwroot", additionalRootFolderItemsFolder,false);
         //   DeleteFile(ftpRootFolder, credentials, "nextwwwroot" + "/assets/clientConfig.js");
           // UploadFile(ftpRootFolder + "/nextwwwroot/assets", credentials, additionalRootFolderItemsFolder, "clientConfig.js");
            //DeleteFolder(ftpRootFolder, credentials, "nextapi");
            UploadFolder(ftpRootFolder, credentials, "nextapi", apiSourceFolder,true);
            RenameFolder(ftpRootFolder, credentials, "wwwroot", "wwwrootBackup" + DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
            RenameFolder(ftpRootFolder, credentials, "api", "apiBackup" + DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
            RenameFolder(ftpRootFolder, credentials, "nextwwwroot", "wwwroot");
            RenameFolder(ftpRootFolder, credentials, "nextapi", "api");

        }

        static bool CreateFolder(
            string ftpRootFolder,
            NetworkCredential credentials,
            string folderName)
        {
            Console.WriteLine("making remote folder "+ ftpRootFolder + "/" + folderName);
            var request = (FtpWebRequest)WebRequest.Create(new Uri(ftpRootFolder + "/"+folderName));
            request.Credentials = credentials;
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            var result = response.StatusCode;
            response.Close();
            return result == FtpStatusCode.PathnameCreated;
        }
        static bool DeleteFile(
           string ftpRootFolder,
           NetworkCredential credentials,
           string filePath)
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(new Uri(ftpRootFolder +"/"+ filePath));
                request.Credentials = credentials;
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                var result = response.StatusCode;
                response.Close();
                return result == FtpStatusCode.PathnameCreated;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //static bool DeleteFolder(
        //   string ftpRootFolder,
        //   NetworkCredential credentials,
        //   string folderName)
        //{
        //    try
        //    {
        //        var request = (FtpWebRequest) WebRequest.Create(new Uri(ftpRootFolder + "/" + folderName));
        //        request.Credentials = credentials;
        //        request.Method = WebRequestMethods.Ftp.RemoveDirectory;
        //        FtpWebResponse response = (FtpWebResponse) request.GetResponse();
        //        var result = response.StatusCode;
        //        response.Close();
        //        return result == FtpStatusCode.PathnameCreated;
        //    }
        //    catch(Exception)
        //    {
        //        return false;
        //    }
        //}
        static bool RenameFolder(
           string ftpRootFolder,
           NetworkCredential credentials,
           string folderName,
           string newName)
        {
            var request = (FtpWebRequest)WebRequest.Create(new Uri(ftpRootFolder + "/" + folderName));
            request.Credentials = credentials;
            request.Method = WebRequestMethods.Ftp.Rename;
            request.RenameTo = newName;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            var result = response.StatusCode;
            response.Close();
            return result == FtpStatusCode.PathnameCreated;
        }
        static bool UploadFolder(
           string ftpRootFolder,
           NetworkCredential credentials,
           string folderName,
           string sourceFolder,bool create)
        {
            if (create)
            {
                CreateFolder(ftpRootFolder, credentials, folderName);
            }
            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine("cannot find folder "+sourceFolder);
                return false;
            }
            var directories = Directory.GetDirectories(sourceFolder);
            var files = Directory.GetFiles(sourceFolder);
            foreach (var file in files)
            {
                UploadFile(ftpRootFolder, credentials, folderName, file);
            }
            foreach (var directory in directories)
            {
                UploadFolder(ftpRootFolder + "/" + folderName
                    , credentials, directory.Substring(directory.LastIndexOf("\\", StringComparison.Ordinal)+1)
                    , directory,create);
            }
            return true;
        }
        static bool UploadFile(
          string ftpRootFolder,
          NetworkCredential credentials,
          string folderName,
          string fileName)
        {
            Console.Write(fileName);

            var destination = ftpRootFolder + "/" + folderName + "/"
                                + fileName.Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal)+1);
            using (WebClient client = new WebClient())
            {
                client.Credentials =credentials;
                client.UploadFile(destination, "STOR", fileName);
            }
            Console.Write(" - Uploaded");
            Console.WriteLine();
            return true;
        }
    }
}
