namespace FaceCheckIn_App
{
    partial class HomeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeForm));
            this.UserFace_Page = new System.Windows.Forms.TabControl();
            this.Tab_User = new System.Windows.Forms.TabPage();
            this.uploadFiles_btn = new System.Windows.Forms.Button();
            this.folderBrowser_btn = new System.Windows.Forms.Button();
            this.ImageFolderAddress_tb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.RemoveBtn = new System.Windows.Forms.Button();
            this.FacelistView = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Cancel_btn = new System.Windows.Forms.Button();
            this.signIn_btn = new System.Windows.Forms.Button();
            this.activeCamara_btn = new System.Windows.Forms.Button();
            this.loadImage_btn = new System.Windows.Forms.Button();
            this.userId_tb = new System.Windows.Forms.TextBox();
            this.UserIdLabel = new System.Windows.Forms.Label();
            this.pboxImage = new System.Windows.Forms.PictureBox();
            this.groupId_tb = new System.Windows.Forms.TextBox();
            this.groupIdLabel = new System.Windows.Forms.Label();
            this.UserName_tb = new System.Windows.Forms.TextBox();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.Tab_SignIn = new System.Windows.Forms.TabPage();
            this.Tab_CheckIn = new System.Windows.Forms.TabPage();
            this.ofdOpenImageFile = new System.Windows.Forms.OpenFileDialog();
            this.imageLists = new System.Windows.Forms.ImageList(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.UserFace_Page.SuspendLayout();
            this.Tab_User.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // UserFace_Page
            // 
            this.UserFace_Page.Controls.Add(this.Tab_User);
            this.UserFace_Page.Controls.Add(this.Tab_SignIn);
            this.UserFace_Page.Controls.Add(this.Tab_CheckIn);
            this.UserFace_Page.Location = new System.Drawing.Point(-1, 1);
            this.UserFace_Page.Name = "UserFace_Page";
            this.UserFace_Page.SelectedIndex = 0;
            this.UserFace_Page.Size = new System.Drawing.Size(2100, 1380);
            this.UserFace_Page.TabIndex = 0;
            // 
            // Tab_User
            // 
            this.Tab_User.AllowDrop = true;
            this.Tab_User.Controls.Add(this.uploadFiles_btn);
            this.Tab_User.Controls.Add(this.folderBrowser_btn);
            this.Tab_User.Controls.Add(this.ImageFolderAddress_tb);
            this.Tab_User.Controls.Add(this.label4);
            this.Tab_User.Controls.Add(this.RemoveBtn);
            this.Tab_User.Controls.Add(this.FacelistView);
            this.Tab_User.Controls.Add(this.label3);
            this.Tab_User.Controls.Add(this.label2);
            this.Tab_User.Controls.Add(this.label1);
            this.Tab_User.Controls.Add(this.Cancel_btn);
            this.Tab_User.Controls.Add(this.signIn_btn);
            this.Tab_User.Controls.Add(this.activeCamara_btn);
            this.Tab_User.Controls.Add(this.loadImage_btn);
            this.Tab_User.Controls.Add(this.userId_tb);
            this.Tab_User.Controls.Add(this.UserIdLabel);
            this.Tab_User.Controls.Add(this.pboxImage);
            this.Tab_User.Controls.Add(this.groupId_tb);
            this.Tab_User.Controls.Add(this.groupIdLabel);
            this.Tab_User.Controls.Add(this.UserName_tb);
            this.Tab_User.Controls.Add(this.UserNameLabel);
            this.Tab_User.Location = new System.Drawing.Point(10, 48);
            this.Tab_User.Name = "Tab_User";
            this.Tab_User.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_User.Size = new System.Drawing.Size(2080, 1322);
            this.Tab_User.TabIndex = 0;
            this.Tab_User.Text = "用户注册";
            this.Tab_User.UseVisualStyleBackColor = true;
            // 
            // uploadFiles_btn
            // 
            this.uploadFiles_btn.Location = new System.Drawing.Point(1178, 326);
            this.uploadFiles_btn.Name = "uploadFiles_btn";
            this.uploadFiles_btn.Size = new System.Drawing.Size(110, 51);
            this.uploadFiles_btn.TabIndex = 37;
            this.uploadFiles_btn.Text = "上传";
            this.uploadFiles_btn.UseVisualStyleBackColor = true;
            this.uploadFiles_btn.Click += new System.EventHandler(this.uploadFiles_btn_Click);
            // 
            // folderBrowser_btn
            // 
            this.folderBrowser_btn.Location = new System.Drawing.Point(1016, 327);
            this.folderBrowser_btn.Name = "folderBrowser_btn";
            this.folderBrowser_btn.Size = new System.Drawing.Size(123, 50);
            this.folderBrowser_btn.TabIndex = 36;
            this.folderBrowser_btn.Text = "浏览";
            this.folderBrowser_btn.UseVisualStyleBackColor = true;
            this.folderBrowser_btn.Click += new System.EventHandler(this.folderBrowser_btn_Click);
            // 
            // ImageFolderAddress_tb
            // 
            this.ImageFolderAddress_tb.Location = new System.Drawing.Point(355, 327);
            this.ImageFolderAddress_tb.Name = "ImageFolderAddress_tb";
            this.ImageFolderAddress_tb.Size = new System.Drawing.Size(655, 38);
            this.ImageFolderAddress_tb.TabIndex = 35;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 334);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 32);
            this.label4.TabIndex = 34;
            this.label4.Text = "文件夹路径：";
            // 
            // RemoveBtn
            // 
            this.RemoveBtn.Location = new System.Drawing.Point(375, 396);
            this.RemoveBtn.Name = "RemoveBtn";
            this.RemoveBtn.Size = new System.Drawing.Size(172, 60);
            this.RemoveBtn.TabIndex = 33;
            this.RemoveBtn.Text = "删除选中";
            this.RemoveBtn.UseVisualStyleBackColor = true;
            this.RemoveBtn.Click += new System.EventHandler(this.RemoveBtn_Click);
            // 
            // FacelistView
            // 
            this.FacelistView.LargeImageList = this.imageLists;
            this.FacelistView.Location = new System.Drawing.Point(155, 462);
            this.FacelistView.Margin = new System.Windows.Forms.Padding(0);
            this.FacelistView.Name = "FacelistView";
            this.FacelistView.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FacelistView.Size = new System.Drawing.Size(1343, 762);
            this.FacelistView.TabIndex = 32;
            this.FacelistView.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(820, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 32);
            this.label3.TabIndex = 30;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(816, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 32);
            this.label2.TabIndex = 29;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(816, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 32);
            this.label1.TabIndex = 31;
            this.label1.Text = "*";
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.Location = new System.Drawing.Point(567, 394);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(177, 60);
            this.Cancel_btn.TabIndex = 28;
            this.Cancel_btn.Text = "清空";
            this.Cancel_btn.UseVisualStyleBackColor = true;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // signIn_btn
            // 
            this.signIn_btn.Location = new System.Drawing.Point(144, 1253);
            this.signIn_btn.Name = "signIn_btn";
            this.signIn_btn.Size = new System.Drawing.Size(177, 55);
            this.signIn_btn.TabIndex = 27;
            this.signIn_btn.Text = "注册";
            this.signIn_btn.UseVisualStyleBackColor = true;
            this.signIn_btn.Click += new System.EventHandler(this.signIn_btn_Click);
            // 
            // activeCamara_btn
            // 
            this.activeCamara_btn.Location = new System.Drawing.Point(794, 393);
            this.activeCamara_btn.Name = "activeCamara_btn";
            this.activeCamara_btn.Size = new System.Drawing.Size(176, 61);
            this.activeCamara_btn.TabIndex = 26;
            this.activeCamara_btn.Text = "实时采集";
            this.activeCamara_btn.UseVisualStyleBackColor = true;
            this.activeCamara_btn.Click += new System.EventHandler(this.activeCamara_btn_Click);
            // 
            // loadImage_btn
            // 
            this.loadImage_btn.Location = new System.Drawing.Point(165, 395);
            this.loadImage_btn.Name = "loadImage_btn";
            this.loadImage_btn.Size = new System.Drawing.Size(177, 61);
            this.loadImage_btn.TabIndex = 25;
            this.loadImage_btn.Text = "添加图片";
            this.loadImage_btn.UseVisualStyleBackColor = true;
            this.loadImage_btn.Click += new System.EventHandler(this.loadImage_btn_Click);
            // 
            // userId_tb
            // 
            this.userId_tb.Location = new System.Drawing.Point(350, 146);
            this.userId_tb.Name = "userId_tb";
            this.userId_tb.Size = new System.Drawing.Size(456, 38);
            this.userId_tb.TabIndex = 24;
            // 
            // UserIdLabel
            // 
            this.UserIdLabel.AutoSize = true;
            this.UserIdLabel.Location = new System.Drawing.Point(159, 146);
            this.UserIdLabel.Name = "UserIdLabel";
            this.UserIdLabel.Size = new System.Drawing.Size(126, 32);
            this.UserIdLabel.TabIndex = 23;
            this.UserIdLabel.Text = "用户ID：";
            // 
            // pboxImage
            // 
            this.pboxImage.Location = new System.Drawing.Point(165, 466);
            this.pboxImage.Name = "pboxImage";
            this.pboxImage.Size = new System.Drawing.Size(455, 211);
            this.pboxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pboxImage.TabIndex = 22;
            this.pboxImage.TabStop = false;
            // 
            // groupId_tb
            // 
            this.groupId_tb.Location = new System.Drawing.Point(350, 51);
            this.groupId_tb.Name = "groupId_tb";
            this.groupId_tb.Size = new System.Drawing.Size(456, 38);
            this.groupId_tb.TabIndex = 21;
            // 
            // groupIdLabel
            // 
            this.groupIdLabel.AutoSize = true;
            this.groupIdLabel.Location = new System.Drawing.Point(159, 57);
            this.groupIdLabel.Name = "groupIdLabel";
            this.groupIdLabel.Size = new System.Drawing.Size(134, 32);
            this.groupIdLabel.TabIndex = 20;
            this.groupIdLabel.Text = "用户组ID:";
            // 
            // UserName_tb
            // 
            this.UserName_tb.Location = new System.Drawing.Point(355, 243);
            this.UserName_tb.Name = "UserName_tb";
            this.UserName_tb.Size = new System.Drawing.Size(456, 38);
            this.UserName_tb.TabIndex = 19;
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Location = new System.Drawing.Point(158, 243);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(127, 32);
            this.UserNameLabel.TabIndex = 18;
            this.UserNameLabel.Text = "用户名：";
            // 
            // Tab_SignIn
            // 
            this.Tab_SignIn.Location = new System.Drawing.Point(10, 48);
            this.Tab_SignIn.Name = "Tab_SignIn";
            this.Tab_SignIn.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_SignIn.Size = new System.Drawing.Size(2080, 1322);
            this.Tab_SignIn.TabIndex = 1;
            this.Tab_SignIn.Text = "人脸注册";
            this.Tab_SignIn.UseVisualStyleBackColor = true;
            // 
            // Tab_CheckIn
            // 
            this.Tab_CheckIn.Location = new System.Drawing.Point(10, 48);
            this.Tab_CheckIn.Name = "Tab_CheckIn";
            this.Tab_CheckIn.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_CheckIn.Size = new System.Drawing.Size(2080, 1322);
            this.Tab_CheckIn.TabIndex = 2;
            this.Tab_CheckIn.Text = "用户签到";
            this.Tab_CheckIn.UseVisualStyleBackColor = true;
            // 
            // ofdOpenImageFile
            // 
            this.ofdOpenImageFile.FileName = "ofdOpenImageFile";
            // 
            // imageLists
            // 
            this.imageLists.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageLists.ImageStream")));
            this.imageLists.TransparentColor = System.Drawing.Color.Transparent;
            this.imageLists.Images.SetKeyName(0, "");
            this.imageLists.Images.SetKeyName(1, "");
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(2111, 1464);
            this.Controls.Add(this.UserFace_Page);
            this.Name = "HomeForm";
            this.Text = "HomeForm";
            this.UserFace_Page.ResumeLayout(false);
            this.Tab_User.ResumeLayout(false);
            this.Tab_User.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl UserFace_Page;
        private System.Windows.Forms.TabPage Tab_User;
        private System.Windows.Forms.TabPage Tab_SignIn;
        private System.Windows.Forms.TabPage Tab_CheckIn;
        private System.Windows.Forms.Button uploadFiles_btn;
        private System.Windows.Forms.Button folderBrowser_btn;
        private System.Windows.Forms.TextBox ImageFolderAddress_tb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button RemoveBtn;
        private System.Windows.Forms.ListView FacelistView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancel_btn;
        private System.Windows.Forms.Button signIn_btn;
        private System.Windows.Forms.Button activeCamara_btn;
        private System.Windows.Forms.Button loadImage_btn;
        private System.Windows.Forms.TextBox userId_tb;
        private System.Windows.Forms.Label UserIdLabel;
        private System.Windows.Forms.PictureBox pboxImage;
        private System.Windows.Forms.TextBox groupId_tb;
        private System.Windows.Forms.Label groupIdLabel;
        private System.Windows.Forms.TextBox UserName_tb;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.OpenFileDialog ofdOpenImageFile;
        private System.Windows.Forms.ImageList imageLists;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}