//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Runtime.CompilerServices;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Box.V2.Auth;
//using Box.V2.Config;
//using Box.V2.Converter;
//using Box.V2.Exceptions;
//using Box.V2.Extensions;
//using Box.V2.Models;
//using Box.V2.Models.Request;
//using Box.V2.Services;
//using Box.V2.Utility;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json.Linq;
//using RestSharp;

//namespace Box.V2.Managers
//{
//    public class Program
//    {
//        static void Main(string[] args)
//        {
//            try {
//                Upload();
//                //Upload_Doc("166402473801", "n0KB7X76zjnWeRCrGX13jSj6Jyi2XJOx");
//            }
//            catch (Exception ex)
//            {
//                //insert to system error log
//            }
//        }
//        public static async Task Upload()
//        {
//            using (FileStream fileStream = new FileStream(@"D:\ORS_Data\ORS_RCM_New\Capital_SKS\Export_CSV\Category.csv", FileMode.Open))
//            {
//                BoxFileRequest requestParams = new BoxFileRequest()
//                {
//                    Name = "Category.csv",
//                    Parent = new BoxRequestEntity() { Id = "0" }
//                };

//                BoxFile file = await client.FilesManager.UploadAsync(requestParams, fileStream);
//            }
//            //BoxFileRequest req = new BoxFileRequest()
//            //{
//            //    Name = "NewFile",
//            //    Parent = new BoxRequestEntity() { Id = "0" }
//            //};

//            //using (var stream = System.IO.File.Open(@"D:\ORS_Data\ORS_RCM_New\Capital_SKS\Export_CSV\Category.csv", FileMode.Open))
//            //{
//            //    BoxFile f = await client.FilesManager.UploadAsync(req, stream);
//            //}
//        }
//        public static void Upload_Doc(string folder_id, string accessToken)
//        {
//            var client = new RestClient("https://upload.box.com/api/2.0");
//            var request = new RestRequest("files/content", Method.Post);
//            request.AddParameter("parent_id", folder_id);

//            request.AddHeader("Authorization", "Bearer " + accessToken);

//            string path = @"D:\ORS_Data\ORS_RCM_New\Capital_SKS\Item_Image\testcode2-1.jpg";
//            byte[] byteArray = System.IO.File.ReadAllBytes(path);

//            request.AddFile("filename", byteArray, "testcode2-1.jpg");

//            var responses = client.Execute(request);
//            var content = responses.Content;
//        }
//        //public async Task Upload(string authorization, string filename, string parentID)
//        //{
//        //    HttpRequestMessage message = new HttpRequestMessage();
//        //    message.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("BoxAuth", authorization);


//        //    MultipartFormDataContent content = new MultipartFormDataContent();
//        //    StreamContent streamContent = null;

//        //    streamContent = new StreamContent(new FileStream(localURI, FileMode.Open));

//        //    streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
//        //    {
//        //        FileName = "\"" + filename + "\"",
//        //        Name = "\"filename\""
//        //    };
//        //    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
//        //    content.Add(streamContent);

//        //    ByteArrayContent byteContent = new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(parentID));
//        //    byteContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
//        //    {
//        //        Name = "\"folder_id\""
//        //    };

//        //    content.Add(byteContent);
//        //    message.Method = HttpMethod.Post;
//        //    message.Content = content;

//        //    message.RequestUri = new Uri("https://api.box.com/2.0/files/content");
//        //    CancellationTokenSource tokenSource = new CancellationTokenSource();
//        //    HttpResponseMessage response = null;
//        //    Task<HttpResponseMessage> t = HttpClient.SendAsync(message);
//        //    response = await t;

//        //    if (t.IsCompleted)
//        //    {
//        //        if (!response.IsSuccessStatusCode)
//        //        {
//        //            if (response.Content != null)
//        //                Console.WriteLine(await response.Content.ReadAsStringAsync(), "Box Upload");
//        //            else
//        //                Console.WriteLine("Error", "Box Upload");
//        //        }
//        //    }
//        //}
//    }
//}


////OAuth 2.0
//using Box.V2;
//using Box.V2.Auth;
//using Box.V2.Config;
//using Box.V2.Models;
//using Box.V2.Utility;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace BoxBatch
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            mainAsync().Wait();
//        }

//        static private async Task mainAsync()
//        {
//            var client_id = ConfigurationManager.AppSettings["client_id"];
//            var client_secret = ConfigurationManager.AppSettings["client_secret"];
//            var redirect_uri = ConfigurationManager.AppSettings["redirect_uri"];
//            var tokenFile = ConfigurationManager.AppSettings["tokenFile"];
//            var accessToken = "xF2hWkApijEx7UbY7WGO0VFnruGW5P07";
//            var auth = new OAuthSession(accessToken, "YOUR_REFRESH_TOKEN", 3600, "bearer");

//            var config = new BoxConfig(client_id, client_secret, new Uri(redirect_uri));
//            var client = new BoxClient(config, auth);
//            BoxFolder folder = await client.FoldersManager.GetInformationAsync("166571415062");
//            if (folder == null)
//                throw new InvalidOperationException(string.Format("Folder does not exist"));

//            client.Auth.SessionAuthenticated += (s, e) => saveSession(e.Session, tokenFile);

//            //var req = new BoxFileRequest()
//            //{
//            //    Name = "Category.csv",
//            //    Parent = new BoxRequestEntity() { Id = folder.Id }
//            //};

//            try
//            {
//                //using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("ORS_DB.bak")))
//                //{
//                //    await client.FilesManager.UploadAsync(req, stream);
//                //}
//                //using (FileStream fileStream = new FileStream(tokenFile, FileMode.Open))
//                //{
//                //    BoxFileRequest requestParams = new BoxFileRequest()
//                //    {
//                //        Name = "ORS_DB.zip",
//                //        Parent = new BoxRequestEntity() { Id = folder.Id }
//                //    };

//                //    BoxFile file = await client.FilesManager.UploadAsync(requestParams, fileStream);
//                //}
//                var progress = new Progress<BoxProgress>(val =>
//                {
//                    Console.WriteLine("Uploaded {0}%", val.progress);
//                });
//                using (FileStream fileStream = new FileStream(tokenFile, FileMode.Open))
//                {
//                    string parentFolderId = folder.Id;
//                    var bFile = await client.FilesManager.UploadUsingSessionAsync(fileStream, "ORS_DB.zip", parentFolderId, null, progress);
//                    Console.WriteLine("{0} uploaded to folder: {1} as file: {2}", tokenFile, parentFolderId, bFile.Id);
//                }
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//            }
//        }

//        static private void saveSession(OAuthSession session, string filename)
//        {
//            var json = JsonConvert.SerializeObject(session, Formatting.Indented);

//            using (var tw = File.CreateText(filename))
//            {
//                tw.WriteLine(json);
//            }
//        }

//        static OAuthSession loadSession(string filename)
//        {
//            string json;

//            using (var sr = new StreamReader(filename))
//            {
//                json = sr.ReadToEnd();
//            }

//            var session = JsonConvert.DeserializeObject<OAuthSession>(json);

//            // always pass invalid access_token to force refresh
//            return new OAuthSession("invalid token", session.RefreshToken, session.ExpiresIn, session.TokenType);
//        }
//    }
//}


//jwt
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Exceptions;
using Box.V2.JWTAuth;
using Box.V2.Models;
using Box.V2.Utility;
using Newtonsoft.Json;

namespace BoxPlayground
{
    public class Program
    {
        static void Main(string[] args)
        {
            ExecuteMainAsync().Wait();
        }
        const long CHUNKED_UPLOAD_MINIMUM = 20000000;
        private static async Task ExecuteMainAsync()
        {
            var directoryName = @"D:\ORS_Data\ORS_RCM_New\Item_ImageAll_Backup_Console\bin\Debug\dotnetUploadFolder\";
            var parentFolderId = "0";
            //var files = Directory.EnumerateFiles(directoryName);
            var files = Directory.GetFileSystemEntries(directoryName, "*", SearchOption.AllDirectories);
            //var files = Directory.GetFiles(directoryName, "*", SearchOption.AllDirectories);
            System.Console.WriteLine(files.Count());
            //var reader = new StreamReader(@"D:\ORS_Data\ORS_RCM_New\Item_ImageAll_Backup_Console/907629304_cpqmqz6i_config.json");
            //var json = reader.ReadToEnd();
            //var config = BoxConfig.CreateFromJsonString(json);

            //var sdk = new BoxJWTAuth(config);
            //var token = sdk.AdminTokenAsync();
            //BoxClient client = sdk.AdminClient(token.ToString());


            using (FileStream fs = new FileStream(@"D:\ORS_Data\ORS_RCM_New\Item_ImageAll_Backup_Console\819341516_3y6kxr9l_config.json", FileMode.Open))
            {
                var session = new BoxJWTAuth(BoxConfig.CreateFromJsonFile(fs));
                var client = session.AdminClient(session.AdminTokenAsync().ToString());
                var folderId = "";
                //try
                //{
                //    var createdFolder = await client.FoldersManager.CreateAsync(
                //      new BoxFolderRequest
                //      {
                //          Parent = new BoxRequestEntity
                //          {
                //              Id = parentFolderId
                //          },
                //          Name = "BCP_Backup"
                //      });
                //    folderId = createdFolder.Id;
                //}
                //catch (BoxConflictException<BoxFolder> e)
                //{
                //    folderId = e.ConflictingItems.FirstOrDefault().Id;
                //    System.Console.WriteLine($"Found existing folder: {folderId}");
                //}
                BoxFolder folder1 = await client.FoldersManager.GetInformationAsync("166757454656");
                if (folder1 == null)
                    throw new InvalidOperationException(string.Format("Folder does not exist"));
                folderId = folder1.Id;

                var fileUploadTasks = new List<Task<BoxFile>>();
                foreach (var file in files)
                {
                    if (!File.Exists(file))
                    {                        
                    }
                    else
                    {
                        fileUploadTasks.Add(Task.Run(
                      async () =>
                      {
                          System.Console.WriteLine(file);
                          // var fileName = "Category5.csv";

                          //var fileName = file.Split(Path.DirectorySeparatorChar)
                          //.Where((item) => { return item != directoryName; }).ToString();
                          //DirectoryInfo di = new DirectoryInfo(directoryName);

                          //var fileName = di.GetFiles().OrderBy(fi => fi.Name).Select(fi => fi.Name).FirstOrDefault();

                          var fileName = Path.GetFileName(file);

                          System.Console.WriteLine(fileName);
                          var fileInfo = new FileInfo(file);
                          var preflightRequest = new BoxPreflightCheckRequest
                          {
                              Name = fileName,
                              Size = fileInfo.Length,
                              Parent = new BoxRequestEntity
                              {
                                  Id = folderId
                              }
                          };
                          using (FileStream toUpload = new FileStream(file, FileMode.Open))
                          {
                              try
                              {
                                  var preflightCheck = await client.FilesManager.PreflightCheck(preflightRequest);

                                  if (toUpload.Length < CHUNKED_UPLOAD_MINIMUM)
                                  {
                                      System.Console.WriteLine(toUpload.Length);
                                      using (SHA1 sha1 = SHA1.Create())
                                      {
                                          var fileUploadRequest = new BoxFileRequest
                                          {
                                              Name = fileName,
                                              Parent = new BoxRequestEntity
                                              {
                                                  Id = folderId
                                              }
                                          };
                                          var fileSHA = sha1.ComputeHash(toUpload);
                                          System.Console.WriteLine(fileSHA);
                                          return await client.FilesManager.UploadAsync(fileRequest: fileUploadRequest, stream: toUpload, contentMD5: fileSHA);
                                      }
                                  }
                                  else
                                  {
                                      //var progress = new Progress<BoxProgress>(val =>
                                      //{
                                      //    Console.WriteLine("Uploaded {0}%", val.progress);
                                      //});
                                   
                                      //Console.WriteLine("{0} uploaded to folder: {1} as file: {2}", toUpload, folderId);
                                      //return await client.FilesManager.UploadUsingSessionAsync(toUpload, fileName, folderId, null, progress);

                                      return await client.FilesManager.UploadUsingSessionAsync(stream: toUpload, fileName: fileName, folderId: folderId);
                                  }
                              }
                              catch (BoxPreflightCheckConflictException<BoxFile> e)
                              {
                                  if (toUpload.Length < CHUNKED_UPLOAD_MINIMUM)
                                  {
                                      System.Console.WriteLine(toUpload.Length);
                                      using (SHA1 sha1 = SHA1.Create())
                                      {
                                          var fileSHA = sha1.ComputeHash(toUpload);
                                          return await client.FilesManager.UploadNewVersionAsync(fileName: e.ConflictingItem.Name, fileId: e.ConflictingItem.Id, stream: toUpload, contentMD5: fileSHA);
                                      }
                                  }
                                  else
                                  {
                                      await client.FilesManager.UploadFileVersionUsingSessionAsync(fileId: e.ConflictingItem.Id, stream: toUpload);
                                      return await client.FilesManager.GetInformationAsync(e.ConflictingItem.Id);
                                  }
                              }
                          }

                      }));
                    }
                }

                var uploaded = await Task.WhenAll(fileUploadTasks);
                foreach (var file in uploaded)
                {
                    System.Console.WriteLine(file.Id);
                }
            }

        }
    }
}