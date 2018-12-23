using Baidu.Aip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceCheckIn_App
{
    public partial class HomeForm : Form
    {
        private string[] allowsExts = { ".jpg", ".png", ".bmp", ".jpeg", ".gif" };
        // 图像文件数据
        private List<string> FaceList = new List<string>();

        public HomeForm()
        {
            InitializeComponent();
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
                if (!FaceList.Contains(item) && AddFace(item))
                    FaceList.Add(item);
            });

            UpdateImageListUI();
        }
        private bool AddFace(string filePath)
        {
            //检测图像质量
            var imageBytes = File.ReadAllBytes(filePath);
            var image = Convert.ToBase64String(imageBytes);
            var result = FaceDectectHelper.DetectDemo(image);

            if (!String.IsNullOrEmpty(result))
            {
                MessageBox.Show(result);
                return false;
            }
            return true;
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
                var imageBytes = File.ReadAllBytes(ofdOpenImageFile.FileName);

                var image = Convert.ToBase64String(imageBytes);
                var jresult = FaceDectectHelper.UserAddDemo(image, groupId, userId, options);
            }

        }        
        private void MenuItem_CheckIn_Click(object sender, EventArgs e)
        {
            var imageBytes = File.ReadAllBytes(FaceList[0]);
            var image = Convert.ToBase64String(imageBytes);
            FaceSearch result = FaceDectectHelper.SearchDemo(image);
            // 可选参数
            var option = new Dictionary<string, object>()
                {
                    {"spd", 5}, // 语速
                    {"vol", 7}, // 音量
                    {"per", 4}  // 发音人，4：情感度丫丫童声
                };
            if (result.score > 50)
            {
                var flag = SpeechHelper.Tts(String.Format("签到成功，欢迎{0}", result.user_info), option);
            }
            else
            {
                var flag = SpeechHelper.Tts(String.Format("没有该人的信息，请先注册该用户"), option);
            }

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

                if (allowsExts.Contains(d.Extension) && !FaceList.Contains(filePath) && AddFace(filePath))
                {
                    FaceList.Add(filePath);
                }
            }
            //更新iamgeList
            UpdateImageListUI();
        }

        private void activeCamara_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
