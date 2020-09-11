using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleDriveExample {
    public class GoogleDriveAPI {

        private static GoogleDriveAPI Instance = null;

        private GoogleDriveAPI() {

        }

        public static GoogleDriveAPI GetInstance() {
            if (Instance == null) {
                Instance = new GoogleDriveAPI();
            }

            return Instance;
        }

        private DriveService service;

        public bool Running = false;

        public event Action<int, string> SetProgressValue = delegate { };

        private readonly string[] Scopes = new string[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile, DriveService.Scope.DriveReadonly };

        /// <summary>
        /// Authorization işlemi yapılır, 
        /// öncesinde google dev. console üzerinden proje açılıp 
        /// api kullanımı için gerekli işlemler yapılmalı ve client_id.json dosyası alınmalı
        /// client_id.json dosyası programın çalışacağı dizine atılmalı
        /// </summary>
        public void Authorize() {
            UserCredential credential;

            if (!System.IO.File.Exists(Application.StartupPath + "/client_id.json")) {
                throw new Exception("client_id.json dosyası bulunamadı");
            }

            using (var stream = new FileStream(Application.StartupPath + "/client_id.json", FileMode.Open, FileAccess.Read)) {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    Environment.UserName,
                    CancellationToken.None,
                    new FileDataStore(Application.StartupPath + "/google-tokens", true)
                ).Result;
            }

            service = new DriveService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleDriveExample",

            });

            service.HttpClient.Timeout = TimeSpan.FromMinutes(100);
        }

        /// <summary>
        /// dosya yüklerken dosyanın türünü belirlemek için kullanılır, drive tarafından MimeType bilgisi istenir
        /// </summary>
        /// <param name="file">yüklenecek dosya</param>
        /// <returns></returns>
        private string GetMimeType(string file) {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(file).ToLower();

            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);

            if (regKey != null && regKey.GetValue("Content Type") != null) {
                mimeType = regKey.GetValue("Content Type").ToString();
            }

            return mimeType;
        }

        /// <summary>
        /// Drive root klasör id bilgisini döner
        /// </summary>
        /// <returns></returns>
        public string GetRootID() {
            Google.Apis.Drive.v3.Data.File file = service.Files.Get("root").Execute();
            return file.Id;
        }

        /// <summary>
        /// drive'dan dosya çekme
        /// </summary>
        /// <param name="query">query varsa kullanır, yoksa klasörler hariç her dosyayı getirir</param>
        /// <returns></returns>
        public List<Google.Apis.Drive.v3.Data.File> GetFiles(string query = null) {
            List<Google.Apis.Drive.v3.Data.File> fileList = new List<Google.Apis.Drive.v3.Data.File>();
            FilesResource.ListRequest request = service.Files.List();
            request.PageSize = 1;
            request.Q = query ?? "mimeType != \"application/vnd.google-apps.folder\"";

            // hangi alanların gelmesini istiyorsak burada belirtiyoruz
            request.Fields = "nextPageToken, files(id, name, createdTime, modifiedTime, mimeType, description, size)";

            //dosyalar parça parça geliyor, her parçada nextPageToken dönüyor, nextPageToken null gelene kadar bu döngü devam eder.
            // null dönerse tüm dosyalar çekilmiştir
            do {
                FileList files = request.Execute();

                // her partta gelen dosyaları fileList listesine ekliyoruz
                fileList.AddRange(files.Files);
                request.PageToken = files.NextPageToken;

            } while (!string.IsNullOrEmpty(request.PageToken));

            return fileList;
        }

        /// <summary>
        /// drive'dan dosya çekme
        /// </summary>
        /// <param name="query">query varsa kullanır, yoksa klasörler hariç her dosyayı getirir</param>
        /// <returns></returns>
        public async IAsyncEnumerable<Google.Apis.Drive.v3.Data.File> GetFilesAsync(string query = null) {
            Running = true;

            FilesResource.ListRequest request = service.Files.List();
            request.PageSize = 200;
            request.Q = query ?? "mimeType != \"application/vnd.google-apps.folder\"";

            // hangi alanların gelmesini istiyorsak burada belirtiyoruz
            request.Fields = "nextPageToken, files(id, name, createdTime, modifiedTime, mimeType, description, size)";

            //dosyalar parça parça geliyor, her parçada nextPageToken dönüyor, nextPageToken null gelene kadar bu döngü devam eder.
            // null dönerse tüm dosyalar çekilmiştir
            do {
                FileList files = await request.ExecuteAsync();
                foreach (var item in files.Files) {
                    yield return item;
                }

                request.PageToken = files.NextPageToken;


            } while (!string.IsNullOrEmpty(request.PageToken));

            Running = false;

            yield break;
        }

        /// <summary>
        /// klasör oluşturur ve id döner, eğer belirtilen isimde klasör varsa oluşturmaz ve mevcut klasör id bilgisini döner
        /// </summary>
        /// <param name="folderName">klasör adı</param>
        /// <param name="parentId">hangi klasörün altına açılmak istenirse o klasörün id bilgisi verilir, boş geçilirse root'a açar</param>
        /// <returns></returns>
        public string CreateFolderAndGetID(string folderName, string parentId = null) {
            string query = $"mimeType = \"application/vnd.google-apps.folder\" and name = \"{folderName}\"";
            List<Google.Apis.Drive.v3.Data.File> result = GetFiles(query);
            Google.Apis.Drive.v3.Data.File file = result.FirstOrDefault();

            if (file != null) {
                return file.Id;
            }
            else {
                file = new Google.Apis.Drive.v3.Data.File {
                    Name = folderName,
                    MimeType = "application/vnd.google-apps.folder"
                };

                if (parentId != null) {
                    file.Parents = new List<string> { parentId };
                }

                var request = service.Files.Create(file);
                request.Fields = "id";
                var response = request.Execute();
                return response.Id;
            }
        }

        /// <summary>
        /// klasör oluşturur ve oluşturulan klasörü istenen detaylarla döner
        /// </summary>
        /// <param name="folderName">klasör adı</param>
        /// <param name="parentId">hangi klasörün altına açılmak istenirse o klasörün id bilgisi verilir, boş geçilirse root'a açar</param>
        /// <returns></returns>
        public Google.Apis.Drive.v3.Data.File CreateFolder(string folderName, string parentId = null) {
            Google.Apis.Drive.v3.Data.File file = new Google.Apis.Drive.v3.Data.File {
                Name = folderName,
                MimeType = "application/vnd.google-apps.folder"
            };

            if (parentId != null) {
                file.Parents = new List<string> { parentId };
            }

            var request = service.Files.Create(file);
            request.Fields = "id, name, createdTime, modifiedTime, mimeType, description, size";
            var response = request.Execute();
            return response;
        }

        /// <summary>
        /// dosya yükler
        /// </summary>
        /// <param name="file">yüklenecek dosya</param>
        /// <param name="parentId">hangi klasöre yüklenecek, boş geçilirse DriveApiExample klasörü oluşturup oraya yükler</param>
        /// <returns></returns>
        public async Task<Google.Apis.Drive.v3.Data.File> UploadFile(string file, string parentId = null) {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);

            if (System.IO.File.Exists(file)) {
                Google.Apis.Drive.v3.Data.File body = new Google.Apis.Drive.v3.Data.File {
                    Name = System.IO.Path.GetFileName(file),
                    Description = "",
                    AppProperties = new Dictionary<string, string> { { "customKey", "customValue" } },
                    MimeType = GetMimeType(file)
                };

                if (!string.IsNullOrEmpty(parentId)) {
                    body.Parents = new List<string> { parentId };
                }
                else {
                    string folderId = CreateFolderAndGetID("DriveApiExample");
                    body.Parents = new List<string> { folderId };
                }

                byte[] byteArray = System.IO.File.ReadAllBytes(file);

                using (var stream = new MemoryStream(byteArray)) {
                    try {
                        FilesResource.CreateMediaUpload request = service.Files.Create(body, stream, GetMimeType(file));
                        request.SupportsTeamDrives = true;
                        request.Fields = "id, name, createdTime, modifiedTime, mimeType, description, size";

                        request.ProgressChanged += (e) => {
                            if (e.BytesSent > 0) {
                                int progress = (int)Math.Floor((decimal)((e.BytesSent * 100) / byteArray.Length));
                                SetProgressValue(progress, "yükleniyor...");

                            }
                        };

                        request.ResponseReceived += (e) => {
                            SetProgressValue(100, "yüklendi");
                        };

                        SetProgressValue(0, "yükleniyor...");

                        await request.UploadAsync();

                        return request.ResponseBody;
                    }
                    catch (Exception ex) {
                        throw new Exception(ex.Message);
                    }
                }
            }
            else {
                throw new Exception("Can not found");
            }
        }

        /// <summary>
        /// dosya siler
        /// </summary>
        /// <param name="fileId">silinecek dosya id</param>
        /// <returns></returns>
        public async Task<string> DeleteFile(string fileId) {
            return await service.Files.Delete(fileId).ExecuteAsync();
        }

        /// <summary>
        /// dosya kopyalar
        /// </summary>
        /// <param name="fileId">kopyalanacak dosya id</param>
        /// <param name="fileName">dosya adı</param>
        /// <param name="destinationParentID">hangi klasöre kopyalanacaksa o klasörün id değeri</param>
        /// <returns></returns>
        public async Task<Google.Apis.Drive.v3.Data.File> Copy(string fileId, string fileName, string destinationParentID) {
            Google.Apis.Drive.v3.Data.File file = new Google.Apis.Drive.v3.Data.File {
                Name = fileName,
                Parents = new List<string> { destinationParentID }
            };

            var request = service.Files.Copy(file, fileId);
            request.Fields = "id, name, createdTime, modifiedTime, mimeType, description, size";

            return await request.ExecuteAsync();
        }

        /// <summary>
        /// dosya taşır
        /// </summary>
        /// <param name="fileId">taşınacak dosya id</param>
        /// <param name="destinationParentID">taşınacağı klasör id</param>
        /// <returns></returns>
        public async Task<Google.Apis.Drive.v3.Data.File> Move(string fileId, string destinationParentID) {
            var fileRequest = service.Files.Get(fileId);
            fileRequest.Fields = "*";
            Google.Apis.Drive.v3.Data.File file = await fileRequest.ExecuteAsync();

            var request = service.Files.Update(new Google.Apis.Drive.v3.Data.File(), fileId);
            request.AddParents = destinationParentID;
            request.RemoveParents = file.Parents.Last();
            request.Fields = "id, name, createdTime, modifiedTime, mimeType, description, size";

            return await request.ExecuteAsync();
        }

        /// <summary>
        /// dosyayı tekrar isimlendirir
        /// </summary>
        /// <param name="fileId">dosya id</param>
        /// <param name="name">yeni adı</param>
        public void Rename(string fileId, string name) {
            Google.Apis.Drive.v3.Data.File file = service.Files.Get(fileId).Execute();
            file.Id = null;
            file.Name = name;
            service.Files.Update(file, fileId).Execute();
        }

        /// <summary>
        /// dosya indirir
        /// </summary>
        /// <param name="fileId">dosya id</param>
        /// <param name="fileSize">dosyanın boyutu (progress hesaplaması için kullanılır)</param>
        /// <returns></returns>
        public async Task<MemoryStream> DownloadFile(string fileId, long fileSize) {
            MemoryStream stream = new MemoryStream();
            var request = service.Files.Get(fileId);

            request.MediaDownloader.ProgressChanged += (e) => {
                if (e.BytesDownloaded > 0) {
                    int progress = (int)Math.Floor((decimal)((e.BytesDownloaded * 100) / fileSize));
                    SetProgressValue(progress, "indiriliyor...");

                    if (e.Status == Google.Apis.Download.DownloadStatus.Completed) {
                        SetProgressValue(100, "indirildi");
                    }
                    else if (e.Status == Google.Apis.Download.DownloadStatus.Failed) {
                        SetProgressValue(100, "hata oluştu: " + e.Exception.Message);
                    }
                }
            };

            SetProgressValue(0, "indiriliyor ...");

            await request.DownloadAsync(stream);

            return stream;
        }

    }
}
