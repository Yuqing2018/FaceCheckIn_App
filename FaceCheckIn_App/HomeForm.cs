using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace FaceCheckIn_App
{
    public partial class HomeForm : Form
    {
        private string[] allowsExts = { ".jpg", ".png", ".bmp", ".jpeg", ".gif" };
        // 图像文件数据
        private List<string> FaceList = new List<string>();
        public List<FaceSearch> Userinfolist { get; set; }

        //设置摄像头获取配置
        FilterInfoCollection videoDevices;
        VideoCaptureDevice videoSource;
        public int selectedDeviceIndex = 0;

        public HomeForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (Userinfolist == null)
            {
                Userinfolist = new List<FaceSearch>();
            }

            users_dataGridView.DataSource = Userinfolist;
        }

        //创建一个委托，是为访问DataGridView控件服务的。
        public delegate void UpdateDataGrid();
        //定义一个委托变量
        public UpdateDataGrid UpdateGrid;

        //修改DataGridView值的方法。
        public void UpdateDataGridMethod()
        {
            var Userinfolist = FaceDectectHelper.GetAllUserList();
            users_dataGridView.DataSource = Userinfolist;
        }
        //此为在非创建线程中的调用方法，其实是使用Invoke方法。
        public void ThreadMethoddataGridView()
        {
            this.BeginInvoke(UpdateGrid);
        }

        //创建一个委托，是为访问DataGridView控件服务的。
        public delegate void UserCheckInDelegate();
        //定义一个委托变量
        public UserCheckInDelegate UserCheck;
        public void UserCheckInMethod()
        {
            Bitmap bitmap = videoSourcePlayer_UserCheckIn.GetCurrentVideoFrame();

            using (var m = new MemoryStream())
            {
                bitmap.Save(m, ImageFormat.Jpeg);
                var flag = UserCheckIn(m.ToArray());
                if (!flag)
                {
                    var img = Image.FromStream(m);
                    string fileName = String.Format("tempPict_{0}.jpg", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    img.Save(fileName);
                }

            }

            bitmap.Dispose();
        }
        public void ThreadMethodUserCheckIn()
        {
            this.BeginInvoke(UserCheck);
        }
        private void loadImage_btn_Click(object sender, EventArgs e)
        {
            ofdOpenImageFile.Filter = "Image Files (Image)|*.jpg;*.png;*.bmp;*.jpeg;*.gif;";
            ofdOpenImageFile.Multiselect = true;
            ofdOpenImageFile.AutoUpgradeEnabled = true;

            // 打开图像文件
            if (ofdOpenImageFile.ShowDialog() == DialogResult.Cancel)
                return;
            ofdOpenImageFile.FileNames.ToList().ForEach(item =>
            {
                var msg = AddFace(item);
            });

            UpdateImageListUI();
        }
        private string AddFace(string filePath)
        {
            if (FaceList.Contains(filePath))
                return "该人脸已注册";

            //检测图像质量
            var imageBytes = File.ReadAllBytes(filePath);
            var image = Convert.ToBase64String(imageBytes);
            var result = FaceDectectHelper.DetectDemo(image);

            if (String.IsNullOrEmpty(result))
            {
                FaceList.Add(filePath);
                return "";
            }
            return result;
        }
        private void UpdateImageListUI()
        {
            imageLists.Images.Clear();
            FacelistView.Items.Clear();

            int length = FaceList.Count;

            for (int i = 0; i < length; i++)
            {
                var item = FaceList[i];
                //添加到FaceList
                var imageName = Path.GetFileNameWithoutExtension(item);
                imageLists.Images.Add(Image.FromFile(item));
                FacelistView.Items.Add(imageName);
                var index = FacelistView.Items.Count - 1;
                FacelistView.Items[index].ImageIndex = index;
                FacelistView.Items[index].Name = imageName;
            }

        }
        private void signIn_btn_Click(object sender, EventArgs e)
        {
            string groupId = groupId_tb.Text;
            string userId = userId_tb.Text;
            string userName = UserName_tb.Text;
            if (String.IsNullOrEmpty(groupId))
            {
                groupId_tb.Focus();
                return;
            }
            if (String.IsNullOrEmpty(userId))
            {
                userId_tb.Focus();
                return;
            }
            if (String.IsNullOrEmpty(userName))
            {
                UserName_tb.Focus();
                return;
            }

            // 如果有可选参数
            var options = new Dictionary<string, object>{
        {"user_info", userName},
        {"quality_control", "NORMAL"},
        {"liveness_control", "NORMAL"}
        };
            var count = imageLists.Images.Count;
            for (int i = 0; i < count; i++)
            {
                var imageBytes = File.ReadAllBytes(FaceList[i]);

                var image = Convert.ToBase64String(imageBytes);
                var jresult = FaceDectectHelper.UserAddDemo(image, groupId, userId, options);
            }
            MessageBox.Show("注册成功！");
        }
        private bool UserCheckIn(byte[] imageBytes)
        {
            bool flag = false;
            var image = Convert.ToBase64String(imageBytes);
            FaceSearch result = FaceDectectHelper.SearchDemo(image);
            // 可选参数
            var option = new Dictionary<string, object>()
                {
                    {"spd", 5}, // 语速
                    {"vol", 7}, // 音量
                    {"per", 4}  // 发音人，4：情感度丫丫童声
                };

            if (result != null && result.score > 50)
            {
                flag = true;
                CheckResult_rtb.AppendText(String.Format("{0}\t 签到时间：{1}\n", result.user_info, DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")));
                SpeechHelper.Tts(String.Format("签到成功，欢迎{0}", result.user_info), option);
            }
            else
            {
                SpeechHelper.Tts(String.Format("没有该用户的信息，请先注册该用户"), option);
            }

            return flag;
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            FaceList.Clear();
            imageLists.Images.Clear();
            FacelistView.Items.Clear();
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            while (FacelistView.SelectedIndices.Count > 0)
            {
                var index = FacelistView.SelectedIndices[0];
                FaceList.RemoveAt(index);
                imageLists.Images.RemoveAt(index);
                FacelistView.Items.RemoveAt(index);
            }

            UpdateImageListUI();
        }

        private void folderBrowser_btn_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                if (!String.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                    ImageFolderAddress_tb.Text = folderBrowserDialog.SelectedPath.Trim();
            }
        }

        private void uploadFiles_btn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ImageFolderAddress_tb.Text))
                return;

            imageLists.Images.Clear();
            FacelistView.Items.Clear();
            //刷新Listview
            BindListView();
        }

        private void BindListView()
        {
            var path = @ImageFolderAddress_tb.Text.Trim();
            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo d in dir.GetFiles())
            {
                var filePath = Path.Combine(path, d.Name);

                if (allowsExts.Contains(d.Extension))
                {
                    AddFace(filePath);
                }
            }
            //更新iamgeList
            UpdateImageListUI();
        }

        //打开摄像头
        private void activeCamara_btn_Click(object sender, EventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                videoSource = null;
            }
            videoSource = new VideoCaptureDevice(videoDevices[selectedDeviceIndex].MonikerString);//连接摄像头。
            videoSource.VideoResolution = videoSource.VideoCapabilities[selectedDeviceIndex];
            switch (UserFace_Page.SelectedIndex)
            {
                case 0:
                    videoSourcePlayer_UserSignIn.VideoSource = videoSource;
                    videoSourcePlayer_UserSignIn.Start();
                    break;
                case 2:
                    videoSourcePlayer_UserCheckIn.VideoSource = videoSource;
                    videoSourcePlayer_UserCheckIn.Start();
                    break;
            }
        }

        //用户签到
        private void button2_Click(object sender, EventArgs e)
        {
            if (videoSource == null)
                return;
            if (!videoSourcePlayer_UserCheckIn.IsRunning)
                return;
            Thread objThread = new Thread(new ThreadStart(delegate
            {
                ThreadMethodUserCheckIn();
                //Bitmap bitmap = videoSourcePlayer_UserCheckIn.GetCurrentVideoFrame();

                //using (var m = new MemoryStream())
                //{
                //    bitmap.Save(m, ImageFormat.Jpeg);
                //    var flag = UserCheckIn(m.ToArray());
                //    if (!flag)
                //    {
                //        var img = Image.FromStream(m);
                //        string fileName = String.Format("tempPict_{0}.jpg", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                //        img.Save(fileName);
                //    }

                //}

                //bitmap.Dispose();
            }));

            objThread.Start();

        }
        private void UserFace_Page_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (TabPage tab in UserFace_Page.TabPages)
            {
                if (UserFace_Page.SelectedIndex != tab.TabIndex)
                {
                    tab.Hide();
                }
                else
                {
                    tab.Show();
                }
            }

            switch (UserFace_Page.SelectedIndex)
            {
                case 1:
                    Userinfolist = FaceDectectHelper.GetAllUserList();
                    users_dataGridView.DataSource = Userinfolist;
                    break;
                case 2:
                    activeCamara_btn_Click(sender, e);
                    break;
            }

        }

        private void CatchPicture_btn_Click(object sender, EventArgs e)
        {
            if (videoSource == null)
                return;

            Bitmap bitmap = null;
            switch (UserFace_Page.SelectedIndex)
            {
                case 0:
                    if (videoSourcePlayer_UserSignIn.IsRunning)
                    {
                        bitmap = videoSourcePlayer_UserSignIn.GetCurrentVideoFrame();
                    }
                    break;
                case 2:
                    if (videoSourcePlayer_UserCheckIn.IsRunning)
                    {
                        bitmap = videoSourcePlayer_UserCheckIn.GetCurrentVideoFrame();
                    }
                    break;
            }
            string fileName = String.Format("tempPict_{0}.jpg", DateTime.Now.ToString("yyyyMMddHHmmssfff"));

            using (var m = new MemoryStream())
            {
                bitmap.Save(m, ImageFormat.Jpeg);

                var img = Image.FromStream(m);

                //TEST
                img.Save(fileName);
                var msg = AddFace(fileName);

                if (!String.IsNullOrEmpty(msg))
                {
                    //SpeechHelper.Tts(msg, null);
                    MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    UpdateImageListUI();
            }

            bitmap.Dispose();
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            var flag = false;
            foreach (DataGridViewRow row in users_dataGridView.SelectedRows)
            {
                var user = row.DataBoundItem as FaceSearch;
                var jresult = FaceDectectHelper.DeleteUser(user.group_id, user.user_id);
                if (jresult["error_code"].ToString() == "0")
                    flag = true;
            }

            if (flag)
            {
                Userinfolist = FaceDectectHelper.GetAllUserList();
                users_dataGridView.DataSource = Userinfolist;
            }
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            //实例化委托
            //UpdateGrid = new UpdateDataGrid(UpdateDataGridMethod);
            UserCheck = new UserCheckInDelegate(UserCheckInMethod);
        }
    }
}
