namespace GoogleDriveExample {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnAuthorize = new System.Windows.Forms.Button();
            this.listFiles = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDownload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.imgLargeList = new System.Windows.Forms.ImageList(this.components);
            this.imgSmallList = new System.Windows.Forms.ImageList(this.components);
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMimeType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateBegin = new System.Windows.Forms.DateTimePicker();
            this.dateEnd = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pbarStatus = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnPrev = new System.Windows.Forms.Button();
            this.cbAllFolderActive = new System.Windows.Forms.CheckBox();
            this.cbDateActive = new System.Windows.Forms.CheckBox();
            this.cmbViewMode = new System.Windows.Forms.ComboBox();
            this.txtNavigation = new System.Windows.Forms.TextBox();
            this.lblFileCount = new System.Windows.Forms.Label();
            this.lblSelectedCount = new System.Windows.Forms.Label();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAuthorize
            // 
            this.btnAuthorize.Location = new System.Drawing.Point(27, 12);
            this.btnAuthorize.Name = "btnAuthorize";
            this.btnAuthorize.Size = new System.Drawing.Size(97, 23);
            this.btnAuthorize.TabIndex = 0;
            this.btnAuthorize.Text = "Authorization";
            this.btnAuthorize.UseVisualStyleBackColor = true;
            this.btnAuthorize.Click += new System.EventHandler(this.btnAuthorize_Click);
            // 
            // listFiles
            // 
            this.listFiles.AllowDrop = true;
            this.listFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader2,
            this.columnHeader1});
            this.listFiles.ContextMenuStrip = this.contextMenu;
            this.listFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listFiles.FullRowSelect = true;
            this.listFiles.GridLines = true;
            this.listFiles.HideSelection = false;
            this.listFiles.LabelEdit = true;
            this.listFiles.LargeImageList = this.imgLargeList;
            this.listFiles.Location = new System.Drawing.Point(27, 133);
            this.listFiles.Name = "listFiles";
            this.listFiles.Size = new System.Drawing.Size(968, 354);
            this.listFiles.SmallImageList = this.imgSmallList;
            this.listFiles.TabIndex = 1;
            this.listFiles.UseCompatibleStateImageBehavior = false;
            this.listFiles.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listFiles_AfterLabelEdit);
            this.listFiles.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listFiles_ItemSelectionChanged);
            this.listFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.listFiles_DragDrop);
            this.listFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.listFiles_DragEnter);
            this.listFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listFiles_MouseDoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Dosya Adı";
            this.columnHeader3.Width = 250;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Boyut (KB)";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Boyut (bytes)";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Mime Type";
            this.columnHeader6.Width = 220;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Oluşturulma Zamanı";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File ID";
            this.columnHeader1.Width = 160;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmNewFolder,
            this.tsmUpload,
            this.tsmCut,
            this.tsmCopy,
            this.tsmPaste,
            this.tsmDownload,
            this.tsmDelete});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(139, 158);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // tsmNewFolder
            // 
            this.tsmNewFolder.Name = "tsmNewFolder";
            this.tsmNewFolder.Size = new System.Drawing.Size(138, 22);
            this.tsmNewFolder.Text = "Yeni Klasör";
            this.tsmNewFolder.Click += new System.EventHandler(this.tsmNewFolder_Click);
            // 
            // tsmUpload
            // 
            this.tsmUpload.Name = "tsmUpload";
            this.tsmUpload.Size = new System.Drawing.Size(138, 22);
            this.tsmUpload.Text = "Dosya Yükle";
            this.tsmUpload.Click += new System.EventHandler(this.tsmUpload_Click);
            // 
            // tsmCut
            // 
            this.tsmCut.Name = "tsmCut";
            this.tsmCut.Size = new System.Drawing.Size(138, 22);
            this.tsmCut.Text = "Kes";
            this.tsmCut.Click += new System.EventHandler(this.tsmCut_Click);
            // 
            // tsmCopy
            // 
            this.tsmCopy.Name = "tsmCopy";
            this.tsmCopy.Size = new System.Drawing.Size(138, 22);
            this.tsmCopy.Text = "Kopyala";
            this.tsmCopy.Click += new System.EventHandler(this.tsmCopy_Click);
            // 
            // tsmPaste
            // 
            this.tsmPaste.Name = "tsmPaste";
            this.tsmPaste.Size = new System.Drawing.Size(138, 22);
            this.tsmPaste.Text = "Yapıştır";
            this.tsmPaste.Click += new System.EventHandler(this.tsmPaste_Click);
            // 
            // tsmDownload
            // 
            this.tsmDownload.Name = "tsmDownload";
            this.tsmDownload.Size = new System.Drawing.Size(138, 22);
            this.tsmDownload.Text = "İndir";
            this.tsmDownload.Click += new System.EventHandler(this.tsmDownload_Click);
            // 
            // tsmDelete
            // 
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.Size = new System.Drawing.Size(138, 22);
            this.tsmDelete.Text = "Sil";
            this.tsmDelete.Click += new System.EventHandler(this.tsmDelete_Click);
            // 
            // imgLargeList
            // 
            this.imgLargeList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLargeList.ImageStream")));
            this.imgLargeList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLargeList.Images.SetKeyName(0, "folder.png");
            this.imgLargeList.Images.SetKeyName(1, "empty-file.png");
            // 
            // imgSmallList
            // 
            this.imgSmallList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgSmallList.ImageStream")));
            this.imgSmallList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgSmallList.Images.SetKeyName(0, "folder-16.png");
            this.imgSmallList.Images.SetKeyName(1, "empty-file-16.png");
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(68, 85);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(235, 20);
            this.txtFileName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Dosya Adı (contains)";
            // 
            // txtMimeType
            // 
            this.txtMimeType.Location = new System.Drawing.Point(308, 85);
            this.txtMimeType.Name = "txtMimeType";
            this.txtMimeType.Size = new System.Drawing.Size(109, 20);
            this.txtMimeType.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Mime Type (contains)";
            // 
            // dateBegin
            // 
            this.dateBegin.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateBegin.Enabled = false;
            this.dateBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateBegin.Location = new System.Drawing.Point(444, 85);
            this.dateBegin.Name = "dateBegin";
            this.dateBegin.Size = new System.Drawing.Size(130, 20);
            this.dateBegin.TabIndex = 7;
            // 
            // dateEnd
            // 
            this.dateEnd.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateEnd.Enabled = false;
            this.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateEnd.Location = new System.Drawing.Point(580, 85);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(130, 20);
            this.dateEnd.TabIndex = 8;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(838, 83);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(44, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Ara";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(441, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Tarih Ara. (Baş.)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(577, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Tarih Ara. (Bit.)";
            // 
            // pbarStatus
            // 
            this.pbarStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbarStatus.Location = new System.Drawing.Point(27, 513);
            this.pbarStatus.Name = "pbarStatus";
            this.pbarStatus.Size = new System.Drawing.Size(968, 23);
            this.pbarStatus.TabIndex = 16;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(24, 539);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(21, 13);
            this.lblStatus.TabIndex = 17;
            this.lblStatus.Text = "%0";
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(27, 85);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(35, 20);
            this.btnPrev.TabIndex = 19;
            this.btnPrev.Text = "<";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // cbAllFolderActive
            // 
            this.cbAllFolderActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAllFolderActive.AutoSize = true;
            this.cbAllFolderActive.Location = new System.Drawing.Point(716, 87);
            this.cbAllFolderActive.Name = "cbAllFolderActive";
            this.cbAllFolderActive.Size = new System.Drawing.Size(119, 17);
            this.cbAllFolderActive.TabIndex = 20;
            this.cbAllFolderActive.Text = "Tüm klasörlerde ara";
            this.cbAllFolderActive.UseVisualStyleBackColor = true;
            // 
            // cbDateActive
            // 
            this.cbDateActive.AutoSize = true;
            this.cbDateActive.Location = new System.Drawing.Point(423, 88);
            this.cbDateActive.Name = "cbDateActive";
            this.cbDateActive.Size = new System.Drawing.Size(15, 14);
            this.cbDateActive.TabIndex = 21;
            this.cbDateActive.UseVisualStyleBackColor = true;
            this.cbDateActive.CheckedChanged += new System.EventHandler(this.cbDateActive_CheckedChanged);
            // 
            // cmbViewMode
            // 
            this.cmbViewMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbViewMode.FormattingEnabled = true;
            this.cmbViewMode.Items.AddRange(new object[] {
            "Büyük Simgeler",
            "Küçük Simgeler",
            "Detay"});
            this.cmbViewMode.Location = new System.Drawing.Point(888, 84);
            this.cmbViewMode.Name = "cmbViewMode";
            this.cmbViewMode.Size = new System.Drawing.Size(107, 21);
            this.cmbViewMode.TabIndex = 24;
            this.cmbViewMode.SelectedIndexChanged += new System.EventHandler(this.cmbViewMode_SelectedIndexChanged);
            // 
            // txtNavigation
            // 
            this.txtNavigation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNavigation.BackColor = System.Drawing.Color.White;
            this.txtNavigation.Location = new System.Drawing.Point(27, 111);
            this.txtNavigation.Name = "txtNavigation";
            this.txtNavigation.ReadOnly = true;
            this.txtNavigation.Size = new System.Drawing.Size(968, 20);
            this.txtNavigation.TabIndex = 25;
            this.txtNavigation.Text = "Root";
            // 
            // lblFileCount
            // 
            this.lblFileCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFileCount.AutoSize = true;
            this.lblFileCount.Location = new System.Drawing.Point(24, 490);
            this.lblFileCount.Name = "lblFileCount";
            this.lblFileCount.Size = new System.Drawing.Size(0, 13);
            this.lblFileCount.TabIndex = 26;
            // 
            // lblSelectedCount
            // 
            this.lblSelectedCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSelectedCount.AutoSize = true;
            this.lblSelectedCount.Location = new System.Drawing.Point(915, 490);
            this.lblSelectedCount.Name = "lblSelectedCount";
            this.lblSelectedCount.Size = new System.Drawing.Size(0, 13);
            this.lblSelectedCount.TabIndex = 27;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 559);
            this.Controls.Add(this.lblSelectedCount);
            this.Controls.Add(this.lblFileCount);
            this.Controls.Add(this.txtNavigation);
            this.Controls.Add(this.cmbViewMode);
            this.Controls.Add(this.cbDateActive);
            this.Controls.Add(this.cbAllFolderActive);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pbarStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dateEnd);
            this.Controls.Add(this.dateBegin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMimeType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.listFiles);
            this.Controls.Add(this.btnAuthorize);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Google Drive API Using";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAuthorize;
        private System.Windows.Forms.ListView listFiles;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMimeType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateBegin;
        private System.Windows.Forms.DateTimePicker dateEnd;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar pbarStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.CheckBox cbAllFolderActive;
        private System.Windows.Forms.CheckBox cbDateActive;
        private System.Windows.Forms.ImageList imgLargeList;
        private System.Windows.Forms.ImageList imgSmallList;
        private System.Windows.Forms.ComboBox cmbViewMode;
        private System.Windows.Forms.TextBox txtNavigation;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmNewFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmUpload;
        private System.Windows.Forms.ToolStripMenuItem tsmCut;
        private System.Windows.Forms.ToolStripMenuItem tsmCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmDownload;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.Label lblSelectedCount;
    }
}

