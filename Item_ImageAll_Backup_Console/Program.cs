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
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Dynamic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Runtime.InteropServices;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using Box.V2;
//using Box.V2.Auth;
//using Box.V2.Config;
//using Box.V2.Converter;
//using Box.V2.Exceptions;
//using Box.V2.JWTAuth;
//using Box.V2.Models;
//using Box.V2.Utility;
//using Newtonsoft.Json;

//namespace BoxPlayground
//{
//    public class Program
//    {
//        static void Main(string[] args)
//        {
//            ExecuteMainAsync().Wait();
//        }
//        const long CHUNKED_UPLOAD_MINIMUM = 20000000;
//        private static async Task ExecuteMainAsync()
//        {
//            var directoryName = @"D:\ORS_Data\ORS_RCM_New\Item_ImageAll_Backup_Console\bin\Debug\dotnetUploadFolder\";
//            var parentFolderId = "0";
//            //var files = Directory.EnumerateFiles(directoryName);
//            var files = Directory.GetFileSystemEntries(directoryName, "*", SearchOption.AllDirectories);
//            //var files = Directory.GetFiles(directoryName, "*", SearchOption.AllDirectories);
//            System.Console.WriteLine(files.Count());
//            //var reader = new StreamReader(@"D:\ORS_Data\ORS_RCM_New\Item_ImageAll_Backup_Console/907629304_cpqmqz6i_config.json");
//            //var json = reader.ReadToEnd();
//            //var config = BoxConfig.CreateFromJsonString(json);

//            //var sdk = new BoxJWTAuth(config);
//            //var token = sdk.AdminTokenAsync();
//            //BoxClient client = sdk.AdminClient(token.ToString());


//            using (FileStream fs = new FileStream(@"D:\ORS_Data\ORS_RCM_New\Item_ImageAll_Backup_Console\819341516_3y6kxr9l_config.json", FileMode.Open))
//            {
//                var session = new BoxJWTAuth(BoxConfig.CreateFromJsonFile(fs));
//                var client = session.AdminClient(session.AdminTokenAsync().ToString());
//                var folderId = "";
//                //try
//                //{
//                //    var createdFolder = await client.FoldersManager.CreateAsync(
//                //      new BoxFolderRequest
//                //      {
//                //          Parent = new BoxRequestEntity
//                //          {
//                //              Id = parentFolderId
//                //          },
//                //          Name = "BCP_Backup"
//                //      });
//                //    folderId = createdFolder.Id;
//                //}
//                //catch (BoxConflictException<BoxFolder> e)
//                //{
//                //    folderId = e.ConflictingItems.FirstOrDefault().Id;
//                //    System.Console.WriteLine($"Found existing folder: {folderId}");
//                //}
//                BoxFolder folder1 = await client.FoldersManager.GetInformationAsync("166757454656");
//                if (folder1 == null)
//                    throw new InvalidOperationException(string.Format("Folder does not exist"));
//                folderId = folder1.Id;

//                var fileUploadTasks = new List<Task<BoxFile>>();
//                foreach (var file in files)
//                {
//                    if (!File.Exists(file))
//                    {                        
//                    }
//                    else
//                    {
//                        fileUploadTasks.Add(Task.Run(
//                      async () =>
//                      {
//                          System.Console.WriteLine(file);
//                          // var fileName = "Category5.csv";

//                          //var fileName = file.Split(Path.DirectorySeparatorChar)
//                          //.Where((item) => { return item != directoryName; }).ToString();
//                          //DirectoryInfo di = new DirectoryInfo(directoryName);

//                          //var fileName = di.GetFiles().OrderBy(fi => fi.Name).Select(fi => fi.Name).FirstOrDefault();

//                          var fileName = Path.GetFileName(file);

//                          System.Console.WriteLine(fileName);
//                          var fileInfo = new FileInfo(file);
//                          var preflightRequest = new BoxPreflightCheckRequest
//                          {
//                              Name = fileName,
//                              Size = fileInfo.Length,
//                              Parent = new BoxRequestEntity
//                              {
//                                  Id = folderId
//                              }
//                          };
//                          using (FileStream toUpload = new FileStream(file, FileMode.Open))
//                          {
//                              try
//                              {
//                                  var preflightCheck = await client.FilesManager.PreflightCheck(preflightRequest);

//                                  if (toUpload.Length < CHUNKED_UPLOAD_MINIMUM)
//                                  {
//                                      System.Console.WriteLine(toUpload.Length);
//                                      using (SHA1 sha1 = SHA1.Create())
//                                      {
//                                          var fileUploadRequest = new BoxFileRequest
//                                          {
//                                              Name = fileName,
//                                              Parent = new BoxRequestEntity
//                                              {
//                                                  Id = folderId
//                                              }
//                                          };
//                                          var fileSHA = sha1.ComputeHash(toUpload);
//                                          System.Console.WriteLine(fileSHA);
//                                          return await client.FilesManager.UploadAsync(fileRequest: fileUploadRequest, stream: toUpload, contentMD5: fileSHA);
//                                      }
//                                  }
//                                  else
//                                  {
//                                      //var progress = new Progress<BoxProgress>(val =>
//                                      //{
//                                      //    Console.WriteLine("Uploaded {0}%", val.progress);
//                                      //});

//                                      //Console.WriteLine("{0} uploaded to folder: {1} as file: {2}", toUpload, folderId);
//                                      //return await client.FilesManager.UploadUsingSessionAsync(toUpload, fileName, folderId, null, progress);

//                                      return await client.FilesManager.UploadUsingSessionAsync(stream: toUpload, fileName: fileName, folderId: folderId);
//                                  }
//                              }
//                              catch (BoxPreflightCheckConflictException<BoxFile> e)
//                              {
//                                  if (toUpload.Length < CHUNKED_UPLOAD_MINIMUM)
//                                  {
//                                      System.Console.WriteLine(toUpload.Length);
//                                      using (SHA1 sha1 = SHA1.Create())
//                                      {
//                                          var fileSHA = sha1.ComputeHash(toUpload);
//                                          return await client.FilesManager.UploadNewVersionAsync(fileName: e.ConflictingItem.Name, fileId: e.ConflictingItem.Id, stream: toUpload, contentMD5: fileSHA);
//                                      }
//                                  }
//                                  else
//                                  {
//                                      await client.FilesManager.UploadFileVersionUsingSessionAsync(fileId: e.ConflictingItem.Id, stream: toUpload);
//                                      return await client.FilesManager.GetInformationAsync(e.ConflictingItem.Id);
//                                  }
//                              }
//                          }

//                      }));
//                    }
//                }

//                var uploaded = await Task.WhenAll(fileUploadTasks);
//                foreach (var file in uploaded)
//                {
//                    System.Console.WriteLine(file.Id);
//                }
//            }

//        }
//    }
//}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Exceptions;
using Box.V2.Extensions;
using Box.V2.JWTAuth;
using Box.V2.Managers;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Services;
using Box.V2.Utility;
using Item_ImageAll_Backup_Console;
using Newtonsoft.Json.Linq;

namespace BoxPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ExecuteMainAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task ExecuteMainAsync()
        {
            // var serviceAccount = BoxService.GetBoxServiceAccountClient();
            var session = new BoxJWTAuth(ConfigureBoxApi());
            var adminToken = session.AdminTokenAsync();
            var serviceAccount = session.AdminClient(adminToken.ToString());
            var me = await serviceAccount.UsersManager.GetCurrentUserInformationAsync();
            System.Console.WriteLine(me.Name);
            System.Console.WriteLine(me.Id);
            await ChunkedUpload(serviceAccount);
            System.Console.WriteLine("Finished.");
        }
        private static IBoxConfig ConfigureBoxApi()
        {

            IBoxConfig config = null;
            // Change this to the config file for your Box App
            using (FileStream fs = new FileStream(@"D:\ORS_Data\ORS_RCM_New\Item_ImageAll_Backup_Console\819341516_3y6kxr9l_config.json", FileMode.Open))
            {
                config = BoxConfig.CreateFromJsonFile(fs);
            }

            return config;
        }

        public static int GetUploadPartsCount(long totalSize, long partSize)
        {
            if (partSize == 0)
                throw new Exception("Part Size cannot be 0");
            int numberOfParts = 1;
            if (partSize != totalSize)
            {
                numberOfParts = Convert.ToInt32(totalSize / partSize);
                numberOfParts += 1;
            }
            return numberOfParts;
        }

        public static Stream GetFilePart(Stream stream, long partSize, long partOffset)
        {
            // Default the buffer size to 4K.
            const int bufferSize = 4096;

            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            stream.Position = partOffset;
            var partStream = new MemoryStream();
            do
            {
                bytesRead = stream.Read(buffer, 0, 4096);
                if (bytesRead > 0)
                {
                    long bytesToWrite = bytesRead;
                    bool shouldBreak = false;
                    if (partStream.Length + bytesRead >= partSize)
                    {
                        bytesToWrite = partSize - partStream.Length;
                        shouldBreak = true;
                    }

                    partStream.Write(buffer, 0, Convert.ToInt32(bytesToWrite));

                    if (shouldBreak)
                    {
                        break;
                    }
                }
            } while (bytesRead > 0);

            return partStream;
        }
        const long CHUNKED_UPLOAD_MINIMUM = 200000;
        private static async Task ChunkedUpload(BoxClient client)
        {
            long fileSize;
            System.Console.WriteLine("Retrieving randomized file...");

            var directoryName = @"D:\ors_bak\";
            var files = Directory.EnumerateFiles(directoryName);

            //MemoryStream fileInMemoryStream = GetBigFileInMemoryStream(fileSize);
            //System.Console.WriteLine("File in memory.");

            //string remoteFileName = "UploadedUsingSession-" + DateTime.Now.TimeOfDay;
            //System.Console.WriteLine($"File name: {remoteFileName}");
            string folderId ;
            BoxFolder folder1 = await client.FoldersManager.GetInformationAsync("167411991885");
            if (folder1 == null)
                throw new InvalidOperationException(string.Format("Folder does not exist"));
            folderId = folder1.Id;

     
            foreach (var file in files)
            {
                using (FileStream toUpload = new FileStream(file, FileMode.Open))
                {
                    var fileName = Path.GetFileName(file);
                    fileSize = toUpload.Length;
                    var boxFileUploadSessionRequest = new BoxFileUploadSessionRequest()
                    {
                        FolderId = folderId,
                        FileName = fileName,
                        FileSize = fileSize
                    };
                    try
                    {
                        if (toUpload.Length < CHUNKED_UPLOAD_MINIMUM)
                        {
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
                                await client.FilesManager.UploadAsync(fileRequest: fileUploadRequest, stream: toUpload, contentMD5: fileSHA);
                            }
                        }
                        else
                        {
                            try
                            {
                                // await client.FilesManager.UploadUsingSessionAsync(stream: toUpload, fileName: fileName, folderId: folderId);
                                var boxFileUploadSession = await client.FilesManager.CreateUploadSessionAsync(boxFileUploadSessionRequest);
                                System.Console.WriteLine("Requested for an Upload Session...");
                                System.Console.WriteLine($"ID: {boxFileUploadSession.Id}");
                                System.Console.WriteLine($"Parts Processed: {boxFileUploadSession.NumPartsProcessed}");
                                System.Console.WriteLine($"Part Size: {boxFileUploadSession.PartSize}");
                                System.Console.WriteLine($"Abort: {boxFileUploadSession.SessionEndpoints.Abort}");
                                System.Console.WriteLine($"Commit: {boxFileUploadSession.SessionEndpoints.Commit}");
                                System.Console.WriteLine($"List Parts: {boxFileUploadSession.SessionEndpoints.ListParts}");
                                System.Console.WriteLine($"Log Event: {boxFileUploadSession.SessionEndpoints.LogEvent}");
                                System.Console.WriteLine($"Status: {boxFileUploadSession.SessionEndpoints.Status}");
                                System.Console.WriteLine($"Upload Part: {boxFileUploadSession.SessionEndpoints.UploadPart}");
                                System.Console.WriteLine($"Type: {boxFileUploadSession.Type}");
                                System.Console.WriteLine($"Total Parts: {boxFileUploadSession.TotalParts}");
                                System.Console.WriteLine($"Expires: {boxFileUploadSession.SessionExpiresAt}");
                                var boxSessionEndpoint = boxFileUploadSession.SessionEndpoints;
                                var uploadPartUri = new Uri(boxSessionEndpoint.UploadPart);
                                var commitUri = new Uri(boxSessionEndpoint.Commit);
                                System.Console.WriteLine($"uri: {uploadPartUri}");
                                var partSize = boxFileUploadSession.PartSize;
                                long partSizeLong;
                                if (long.TryParse(partSize, out partSizeLong) == false)
                                {
                                    throw new BoxException("File part size is wrong!");
                                }

                                long.TryParse(partSize, out partSizeLong);
                                var numberOfParts = GetUploadPartsCount(fileSize, partSizeLong);

                                // Full file sha1 for final commit
                                var fullFileSha1 = await Task.Run(() => {
                                    return Helper.GetSha1Hash(toUpload);
                                });

                                for (var i = 0; i < numberOfParts; i++)
                                {
                                    var partOffset = partSizeLong * i;
                                    Stream partFileStream = GetFilePart(toUpload, partSizeLong, partOffset);
                                    var sha = Helper.GetSha1Hash(partFileStream);
                                    partFileStream.Position = 0;
                                    await client.FilesManager.UploadPartAsync(uploadPartUri, sha, partOffset, fileSize, partFileStream);
                                }
                                //var boxSessionParts = await UploadPartsInSessionAsync(uploadPartUri, numberOfParts, partSizeLong, toUpload, fileSize, client);
                                //var allSessionParts = new List<BoxSessionPartInfo>();

                                //foreach (var sessionPart in boxSessionParts)
                                //{
                                //    System.Console.WriteLine($"Retrieved Session Part: {sessionPart.Part.PartId}");
                                //    allSessionParts.Add(sessionPart.Part);
                                //}

                                //BoxSessionParts sessionPartsForCommit = new BoxSessionParts() { Parts = allSessionParts };

                                // Upload parts in session
                                var progress = new Progress<BoxProgress>(val =>
                                               {
                                                   Console.WriteLine("Uploaded {0}%", val.progress);
                                               });
                                BoxFilesManager1 boxFiles = new BoxFilesManager1(null, null, null, null, null, null);
                                var allSessionParts = await boxFiles.UploadPartsInSessionAsync(uploadPartUri,
                                                                numberOfParts, partSizeLong, toUpload,
                                                                fileSize, TimeSpan.FromSeconds(360), progress).ConfigureAwait(false);

                                var allSessionPartsList = allSessionParts.ToList();

                                var sessionPartsForCommit = new BoxSessionParts
                                {
                                    Parts = allSessionPartsList
                                };

                                // Commit
                                //await client.FilesManager.CommitSessionAsync(commitUri, Box.V2.Utility.Helper.GetSha1Hash(toUpload), sessionPartsForCommit);

                                // Commit, Retry 5 times with interval related to the total part number
                                // Having debugged this -- retries do consistenly happen so we up the retries
                                const int retryCount = 5;
                                var retryInterval = allSessionPartsList.Count * 100;

                                var response =
                                    await Retry.ExecuteAsync(
                                        async () =>
                                            await client.FilesManager.CommitSessionAsync(commitUri, Box.V2.Utility.Helper.GetSha1Hash(toUpload), sessionPartsForCommit),
                                        TimeSpan.FromMilliseconds(retryInterval), retryCount);

                                //return response;
                                System.Console.WriteLine(response.ToString());
                                // Delete file
                                //string fileId = await GetFileId(folderId, file, client);
                                //if (!string.IsNullOrWhiteSpace(fileId))
                                //{
                                //    await client.FilesManager.DeleteAsync(fileId);
                                //    System.Console.WriteLine("Deleted");
                                //}
                            }


                            catch (Exception ex)
                            {
                                System.Console.WriteLine(ex.ToString());
                            }
                        } 
                    }
                    catch (BoxPreflightCheckConflictException<BoxFile> e)
                    {
                              if (toUpload.Length < CHUNKED_UPLOAD_MINIMUM)
                              {
                                  using (SHA1 sha1 = SHA1.Create())
                                  {
                                      var fileSHA = sha1.ComputeHash(toUpload);
                                      await client.FilesManager.UploadNewVersionAsync(fileName: e.ConflictingItem.Name, fileId: e.ConflictingItem.Id, stream: toUpload, contentMD5: fileSHA);
                                  }
                              }
                              else
                              {
                                  await client.FilesManager.UploadFileVersionUsingSessionAsync(fileId: e.ConflictingItem.Id, stream: toUpload);
                                  await client.FilesManager.GetInformationAsync(e.ConflictingItem.Id);
                              }
                     }
                }
            }
           
        }
      private static MemoryStream GetBigFileInMemoryStream(long fileSize)
        {
            // Create random data to write to the file.
            byte[] dataArray = new byte[fileSize];
            new Random().NextBytes(dataArray);
            MemoryStream memoryStream = new MemoryStream(dataArray);
            return memoryStream;
        }

        //private static async Task<IEnumerable<BoxSessionPartInfo>> UploadPartsInSessionAsync(Uri uploadPartsUri, int numberOfParts, long partSize, Stream stream,
        //    long fileSize, TimeSpan? timeout = null, IProgress<BoxProgress> progress = null)
        //{
        //    BoxFilesManager1 boxFiles = new BoxFilesManager1(null, null, null, null, null,null);
        //    var maxTaskNum = Environment.ProcessorCount + 1;

        //    // Retry 5 times for 10 seconds
        //    const int RetryMaxCount = 5;
        //    const int RetryMaxInterval = 10;

        //    var ret = new List<BoxSessionPartInfo>();

        //    using (var concurrencySemaphore = new SemaphoreSlim(maxTaskNum))
        //    {
        //        var postTaskTasks = new List<Task>();
        //        var taskCompleted = 0;

        //        var tasks = new List<Task<BoxUploadPartResponse>>();
        //        for (var i = 0; i < numberOfParts; i++)
        //        {
        //            await concurrencySemaphore.WaitAsync().ConfigureAwait(false);

        //            // Split file as per part size
        //            var partOffset = partSize * i;

        //            // Retry
        //            var uploadPartWithRetryTask = Retry.ExecuteAsync(async () =>
        //            {
        //                // Release the memory when done
        //                using (var partFileStream = UploadUsingSessionInternal.GetFilePart(stream, partSize,
        //                            partOffset))
        //                {
        //                    var sha = Helper.GetSha1Hash(partFileStream);
        //                    partFileStream.Position = 0;
        //                    var uploadPartResponse = await boxFiles.UploadPartAsync(
        //                        uploadPartsUri, sha, partOffset, fileSize, partFileStream,
        //                        timeout).ConfigureAwait(false);

        //                    return uploadPartResponse;
        //                }
        //            }, TimeSpan.FromSeconds(RetryMaxInterval), RetryMaxCount);

        //            // Have each task notify the Semaphore when it completes so that it decrements the number of tasks currently running.
        //            postTaskTasks.Add(uploadPartWithRetryTask.ContinueWith(tsk =>
        //            {
        //                concurrencySemaphore.Release();
        //                ++taskCompleted;
        //                if (progress != null)
        //                {
        //                    var boxProgress = new BoxProgress()
        //                    {
        //                        progress = taskCompleted * 100 / numberOfParts
        //                    };

        //                    progress.Report(boxProgress);
        //                }
        //            }
        //            ));

        //            tasks.Add(uploadPartWithRetryTask);
        //        }

        //        var results = await Task.WhenAll(tasks).ConfigureAwait(false);
        //        ret.AddRange(results.Select(elem => elem.Part));
        //    }

        //    return ret;
        //}
        
        private static async Task<string> GetFileId(string folderId, string fileName, BoxClient client)
        {
            BoxCollection<BoxItem> boxCollection = await client.FoldersManager.GetFolderItemsAsync(folderId, 1000);
            return boxCollection.Entries.FirstOrDefault(item => item.Name == fileName)?.Id;
        }
    }
    //internal static class UploadUsingSessionInternal
    //{
    //    public static int GetNumberOfParts(long totalSize, long partSize)
    //    {
    //        if (partSize == 0)
    //        {
    //            throw new BoxCodingException("Part Size cannot be 0");
    //        }

    //        var numberOfParts = Convert.ToInt32(totalSize / partSize);
    //        if (totalSize % partSize != 0)
    //        {
    //            numberOfParts++;
    //        }
    //        return numberOfParts;
    //    }

    //    public static Stream GetFilePart(Stream stream, long partSize, long partOffset)
    //    {
    //        // Default the buffer size to 4K.
    //        const int BufferSize = 4096;

    //        var buffer = new byte[BufferSize];
    //        stream.Position = partOffset;
    //        var partStream = new MemoryStream();
    //        int bytesRead;
    //        do
    //        {
    //            bytesRead = stream.Read(buffer, 0, 4096);
    //            if (bytesRead > 0)
    //            {
    //                long bytesToWrite = bytesRead;
    //                var shouldBreak = false;
    //                if (partStream.Length + bytesRead >= partSize)
    //                {
    //                    bytesToWrite = partSize - partStream.Length;
    //                    shouldBreak = true;
    //                }

    //                partStream.Write(buffer, 0, Convert.ToInt32(bytesToWrite));

    //                if (shouldBreak)
    //                {
    //                    break;
    //                }
    //            }
    //        } while (bytesRead > 0);

    //        return partStream;
    //    }
    //}
}