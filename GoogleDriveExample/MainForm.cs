using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoogleDriveExample {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// Aktif klasör bilgileri
        /// </summary>
        private string ActiveParentID;
        private string ActiveFolderName;

        /// <summary>
        /// Navbar, klasör gezintisi için, her girilen klasör listeye eklenir, geriye döndükçe listeden silinir
        /// </summary>
        private readonly List<string> Navigation = new List<string>();

        private GoogleDriveAPI DriveApi;

        /// <summary>
        /// Kes, kopyala işlemleri için dosya ID ve ismi
        /// </summary>
        private string CopyFileID, CutFileID, SelectedFileName;

        /// <summary>
        /// Son execute edilen query 
        /// </summary>
        private string LastQuery;

        /// <summary>
        /// drive'a login olur ve login işlemi başarılıysa root'taki dosyaları çeker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAuthorize_Click(object sender, EventArgs e) {
            DriveApi = GoogleDriveAPI.GetInstance();

            try {
                DriveApi.Authorize();
                btnAuthorize.Enabled = false;
                btnAuthorize.Text = "Authorized";

                GetRootFiles();

                cmbViewMode.SelectedIndex = 0;
                btnPrev.Enabled = false;

                pbarStatus.Visible = false;
                lblStatus.Visible = false;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// aktif listeyi yenile
        /// </summary>
        private void RefreshList() {
            GetList(LastQuery);
        }

        /// <summary>
        /// Root'taki dosyaları listeler
        /// </summary>
        private void GetRootFiles() {
            string rootId = DriveApi.GetRootID();
            ActiveParentID = rootId;
            Navigation.Add(rootId);

            string query = $"('{rootId}' in parents)";
            GetList(query);
        }

        /// <summary>
        /// verilen query'e göre dosya listeler
        /// </summary>
        /// <param name="query">query</param>
        private void GetList(string query) {
            LastQuery = query;

            List<Google.Apis.Drive.v3.Data.File> files = DriveApi.GetFiles(query);
            FillListView(files);
        }

        /// <summary>
        /// Listview e dosyaları doldurur
        /// </summary>
        /// <param name="files">dosyalar</param>
        private void FillListView(List<Google.Apis.Drive.v3.Data.File> files) {
            listFiles.Items.Clear();

            int i, n = files.Count;
            for (i = 0; i < n; i++) {
                long fileSize = files[i].Size ?? 0;
                string[] row = { files[i].Name, (fileSize / 1024f).ToString("n0") + " KB", fileSize.ToString(), files[i].MimeType, files[i].CreatedTime.Value.ToString("G"), files[i].Id };

                ListViewItem lvi = new ListViewItem(row);

                if (files[i].MimeType.Contains("folder")) {
                    lvi.ImageIndex = 0;
                }
                else {
                    lvi.ImageIndex = 1;
                }

                listFiles.Items.Add(lvi);
            }
        }

        /// <summary>
        /// drive'da arama yapar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e) {
            string query = "";

            // tüm klasörlerde arama aktif değilse mevcut klasörde arar
            if (!cbAllFolderActive.Checked) {
                query = $"('{ActiveParentID}' in parents) and";
            }

            // tarih seçimi aktif ise oluşturulma tarihine göre tarih aralığı ile arar
            if (cbDateActive.Checked) {
                query += $" createdTime >= \"{dateBegin.Value:s}\" and"; // or modifiedTime
                query += $" createdTime <= \"{dateEnd.Value:s}\" and"; // or modifiedTime
            }

            // dosya adına göre içinde geçenleri arar
            if (!string.IsNullOrEmpty(txtFileName.Text)) {
                query += $" name contains \"{txtFileName.Text}\" and"; // or fullText contains
            }

            // dosya tipine göre arar
            if (!string.IsNullOrEmpty(txtMimeType.Text)) {
                query += $" mimeType contains \"{txtMimeType.Text}\" and"; // or !=, =
            }

            if (query.Length == 0) {
                return;
            }

            query = query.Substring(0, query.Length - 3);

            // dosya yüklerken özel key ve value değerler girilebilir, bunların içinde de arama yapabiliriz
            // search with appProperties values
            // query += $" and appProperties has {{key=\"customKey\" and value=\"customValue\"}} ";
            GetList(query);
        }

        /// <summary>
        /// progress barı kaydırmak için kullanılır
        /// </summary>
        /// <param name="value">yeni değeri</param>
        /// <param name="text">işlem metni</param>
        private void SetProgressValue(int value, string text) {
            pbarStatus.Value = value;
            lblStatus.Text = $"%{value} {text}";
        }

        private void MainForm_Load(object sender, EventArgs e) {
            btnAuthorize.PerformClick();
        }

        /// <summary>
        /// bir önceki klasöre döner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e) {
            // aktif klasörü navigasyondan siler
            Navigation.Remove(ActiveParentID);

            // aktif klasör silindiği için aktif klasörün üstündeki klasöre last ile ulaşabiliriz
            string query = $"('{Navigation.Last()}' in parents)";

            // yeni aktif klasörümüz geri döndüğümüz klasör olur
            ActiveParentID = Navigation.Last();
            txtNavigation.Text = txtNavigation.Text.Replace("/" + ActiveFolderName, "");
            ActiveFolderName = txtNavigation.Text.Split('/').Last();

            GetList(query);

            if (Navigation.Count < 2) {
                btnPrev.Enabled = false;
            }
        }

        // klasöre çift tıklayınca içini açmak için
        private void listFiles_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (listFiles.SelectedItems.Count == 0) {
                return;
            }

            ListViewItem lvi = listFiles.SelectedItems[0];
            string mimeType = lvi.SubItems[3].Text;

            // tıklanan item klasör değilse işlem yapmaz
            if (!mimeType.Contains("folder")) {
                return;
            }

            string folderId = lvi.SubItems[5].Text;
            string folderName = lvi.SubItems[0].Text;
            string query = $"('{folderId}' in parents)";

            GetList(query);

            // açılan klasörü navigasyona ekler ve aktif klasör olarak belirtir
            Navigation.Add(folderId);
            ActiveParentID = folderId;

            txtNavigation.Text += $"/{folderName}";
            ActiveFolderName = $"{folderName}";

            if (Navigation.Count > 1) {
                btnPrev.Enabled = true;
            }

        }

        /// <summary>
        /// tarih kullanımını aktif/inaktif et
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbDateActive_CheckedChanged(object sender, EventArgs e) {
            dateBegin.Enabled = cbDateActive.Checked;
            dateEnd.Enabled = cbDateActive.Checked;
        }

        /// <summary>
        /// görünümü değiştir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbViewMode_SelectedIndexChanged(object sender, EventArgs e) {
            if (cmbViewMode.SelectedIndex == 0) {
                listFiles.View = View.LargeIcon;
            }
            else if (cmbViewMode.SelectedIndex == 1) {
                listFiles.View = View.SmallIcon;
            }
            else if (cmbViewMode.SelectedIndex == 2) {
                listFiles.View = View.Details;
            }
        }

        /// <summary>
        /// dosya/klasör yeniden isimlendirmek için veya yeni klasör oluşturmak için
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFiles_AfterLabelEdit(object sender, LabelEditEventArgs e) {
            ListViewItem lvi = listFiles.Items[e.Item];
            string fileId = lvi.SubItems[5].Text;

            // eğer fileId null ise bu yeni oluşacak bir klasördür, ama ismi verilmemişse işlemi geri alır
            if (string.IsNullOrEmpty(fileId) && string.IsNullOrEmpty(e.Label)) {
                lvi.Selected = false;
                lvi.Focused = false;
                listFiles.Items.Remove(lvi);
            }


            string newName = e.Label;
            if (string.IsNullOrEmpty(newName)) {
                return;
            }

            string oldName = lvi.SubItems[0].Text;

            // eğer fileId null ise yeni klasör oluşacaktır, e.label girilmişse klasöre isim verilmiş demektir
            if (string.IsNullOrEmpty(fileId)) {
                // klasörü oluşturur ve aktif seçili olan item'ı günceller, ardından unselect ve unfocus olur
                Google.Apis.Drive.v3.Data.File file = DriveApi.CreateFolder(newName, ActiveParentID);
                long fileSize = file.Size ?? 0;

                lvi.SubItems[1].Text = (fileSize / 1024f).ToString("n0") + " KB";
                lvi.SubItems[2].Text = fileSize.ToString();
                lvi.SubItems[3].Text = file.MimeType;
                lvi.SubItems[4].Text = file.CreatedTime?.ToString("G");
                lvi.SubItems[5].Text = file.Id;

                lvi.Selected = false;
                lvi.Focused = false;
                return;
            }

            // yeni isim ile eski isim aynı ise işlem yapmaya gerek yok
            if (oldName == newName) {
                return;
            }

            // dosya/klasör ismini değiştirir
            DriveApi.Rename(fileId, newName);

        }

        // sağ click ile menü açılır, duruma göre gösterilecek alt menüler belirlenir
        private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            // seçili bir dosya yok ise kes, kopyala, indir, sil gibi dosya üzerinde yapılacak işlemler gizlenir
            // yeni klasör ve yeni dosya yükleme butonları gösterilir. kesilen veya kopyalanan dosya varsa yapıştır butonu da görünür
            if (listFiles.SelectedItems.Count == 0) {
                tsmCopy.Visible = false;
                tsmCut.Visible = false;
                tsmDownload.Visible = false;
                tsmDelete.Visible = false;

                tsmNewFolder.Visible = true;
                tsmUpload.Visible = true;
                tsmPaste.Visible = !string.IsNullOrEmpty(CutFileID) || !string.IsNullOrEmpty(CopyFileID);
            }
            else {
                // seçilen bir dosya varsa yeni klasör, yapıştır ve yükle butonları gizlenir
                // kes, kopyala, indir, sil butonları gösterilir. seçilen dosya bir klasör ise indir butonu da gizlenir
                tsmNewFolder.Visible = false;
                tsmUpload.Visible = false;
                tsmPaste.Visible = false;

                tsmCopy.Visible = true;
                tsmCut.Visible = true;
                tsmDownload.Visible = true;
                tsmDelete.Visible = true;
                tsmDownload.Visible = !listFiles.SelectedItems[0].SubItems[3].Text.Contains("folder");
            }
        }

        /// <summary>
        ///  yeni klasör oluştur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmNewFolder_Click(object sender, EventArgs e) {
            string[] row = { "Yeni Klasör", 0.ToString("n0") + " KB", 0.ToString(), "", "", "" };

            // listview'e bir tane item ekler, seçer ve focus olur, ardından ismini düzenlemek için edit moda geçer
            ListViewItem lvi = new ListViewItem(row) {
                ImageIndex = 0
            };
            listFiles.Items.Add(lvi);
            lvi.Selected = true;
            lvi.Focused = true;
            lvi.BeginEdit();

        }

        /// <summary>
        /// yeni dosya yüklemek için openfileDialog ile dosya seçer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tsmUpload_Click(object sender, EventArgs e) {
            OpenFileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() != DialogResult.OK) {
                return;
            }

            string file = fileDialog.FileName;

            if (string.IsNullOrEmpty(file)) {
                MessageBox.Show("Bir dosya seçiniz");
                return;
            }

            if (!File.Exists(file)) {
                MessageBox.Show("Dosya bulunamadı");
                return;
            }

            await UploadFile(file);

        }

        /// <summary>
        /// dosya yükler
        /// </summary>
        /// <param name="file">yüklenecek dosya</param>
        /// <returns></returns>
        private async Task<bool> UploadFile(string file) {
            tsmUpload.Enabled = false;
            tsmDownload.Enabled = false;
            pbarStatus.Visible = true;
            lblStatus.Visible = true;

            DriveApi.SetProgressValue += SetProgressValue;
            Google.Apis.Drive.v3.Data.File uploadedFile = await DriveApi.UploadFile(file, ActiveParentID);

            long fileSize = uploadedFile.Size ?? 0;
            string[] row = { uploadedFile.Name, (fileSize / 1024f).ToString("n0") + " KB", fileSize.ToString(), uploadedFile.MimeType, uploadedFile.CreatedTime?.ToString("G"), uploadedFile.Id };

            ListViewItem lvi = new ListViewItem(row) {
                ImageIndex = 1
            };
            listFiles.Items.Add(lvi);

            DriveApi.SetProgressValue -= SetProgressValue;

            tsmUpload.Enabled = true;
            tsmDownload.Enabled = true;

            pbarStatus.Value = 0;
            lblStatus.Text = "";

            pbarStatus.Visible = false;
            lblStatus.Visible = false;

            return true;
        }

        /// <summary>
        /// dosya kes(taşı) işlemi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmCut_Click(object sender, EventArgs e) {
            if (listFiles.SelectedItems.Count == 0) {
                return;
            }

            CutFileID = listFiles.SelectedItems[0].SubItems[5].Text;
            SelectedFileName = listFiles.SelectedItems[0].SubItems[0].Text;
            CopyFileID = null;
        }

        /// <summary>
        /// dosya kopyalama işlemi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmCopy_Click(object sender, EventArgs e) {
            if (listFiles.SelectedItems.Count == 0) {
                return;
            }

            CopyFileID = listFiles.SelectedItems[0].SubItems[5].Text;
            SelectedFileName = listFiles.SelectedItems[0].SubItems[0].Text;
            CutFileID = null;
        }

        /// <summary>
        /// kes ya da kopyala yapılan dosya varsa aktif klasöre yapıştırır
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tsmPaste_Click(object sender, EventArgs e) {
            bool b = !string.IsNullOrEmpty(CutFileID) || !string.IsNullOrEmpty(CopyFileID);

            if (!b) {
                return;
            }
            Google.Apis.Drive.v3.Data.File file;
            if (!string.IsNullOrEmpty(CopyFileID)) {
                file = await DriveApi.Copy(CopyFileID, SelectedFileName, ActiveParentID);
            }
            else {
                file = await DriveApi.Move(CutFileID, ActiveParentID);
            }

            long fileSize = file.Size ?? 0;
            string[] row = { file.Name, (fileSize / 1024f).ToString("n0") + " KB", fileSize.ToString(), file.MimeType, file.CreatedTime?.ToString("G"), file.Id };

            ListViewItem lvi = new ListViewItem(row) {
                ImageIndex = 1
            };
            listFiles.Items.Add(lvi);

            CopyFileID = null;
            CutFileID = null;
        }

        /// <summary>
        /// seçilen dosyayı indirir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tsmDownload_Click(object sender, EventArgs e) {
            if (listFiles.SelectedItems.Count == 0) {
                return;
            }

            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() != DialogResult.OK) {
                return;
            }

            string folderPath = folderDialog.SelectedPath;

            ListViewItem lvi = listFiles.SelectedItems[0];

            string mimeType = lvi.SubItems[3].Text;
            string fileId = lvi.SubItems[5].Text;
            string fileName = lvi.SubItems[0].Text;
            long fileSize = long.Parse(lvi.SubItems[2].Text);

            if (fileSize == 0) {
                return;
            }

            pbarStatus.Visible = true;
            lblStatus.Visible = true;

            tsmDownload.Enabled = false;
            tsmUpload.Enabled = false;
            DriveApi.SetProgressValue += SetProgressValue;

            using (var stream = await DriveApi.DownloadFile(fileId, fileSize)) {
                stream.Position = 0;
                stream.Seek(0, SeekOrigin.Begin);

                using (FileStream fs = new FileStream(folderPath + "\\" + fileName, FileMode.Create, FileAccess.Write)) {
                    stream.CopyTo(fs);
                }
            }

            DriveApi.SetProgressValue -= SetProgressValue;

            MessageBox.Show("Dosya indirildi");
            tsmDownload.Enabled = true;
            tsmUpload.Enabled = true;

            pbarStatus.Value = 0;
            lblStatus.Text = "";


            pbarStatus.Visible = false;
            lblStatus.Visible = false;
        }

        /// <summary>
        /// seçilen dosyayı siler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tsmDelete_Click(object sender, EventArgs e) {
            if (listFiles.SelectedItems.Count == 0) {
                return;
            }

            if (MessageBox.Show("Silmek istiyor musun?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                return;
            }

            ListViewItem lvi = listFiles.SelectedItems[0];
            string fileId = lvi.SubItems[5].Text;

            tsmDelete.Enabled = false;
            await DriveApi.DeleteFile(fileId);

            listFiles.Items.Remove(lvi);

            MessageBox.Show("Dosya silindi");
            tsmDelete.Enabled = true;
        }

        /// <summary>
        /// sürükle bırakla dosya yükleme, sürüklenen dosyaları yükler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void listFiles_DragDrop(object sender, DragEventArgs e) {
            // sürüklenen tüm dosya yollarını diziye alır
            string[] handles = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            // diziyi döner ve tek tek dosyaları yükler
            foreach (string file in handles) {
                if (File.Exists(file)) {
                    await UploadFile(file);
                }
            }
        }

        // bir dosya sürüklenmişse efect'i copy olarak gösterir
        private void listFiles_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
        }
    }
}
