using Baidu.Aip;
using Baidu.Aip.Face;
using FaceCheckIn_App;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceCheckIn_App
{
    public class FaceDectectHelper
    {
        // 设置APPID/AK/SK
        private const string APP_ID = "14670997";
        private const string API_KEY = "MA0G2N35rvrZ5tjsekTpaZ6w";
        private const string SECRET_KEY = "1P2DWvlodV7YY0myMqH8xhxzGg0ZT1GY";

        private static Face _faceClient = new Face(API_KEY, SECRET_KEY) { Timeout = 6000 };

        /// <summary>
        /// 人脸检测 
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static String DetectDemo(string image)
        {
            String Result = String.Empty;
            try
            {
                var imageType = "BASE64";
                //var result = _faceClient.Detect(image, imageType);
                // 如果有可选参数
                var options = new Dictionary<string, object>
                {
                    {"face_field",  "beauty,age,expression,face_shape,gender,race,quality"},
                    {"max_face_num", 1},
                };

                var jresult = _faceClient.Detect(image, imageType, options);

                if (jresult["error_code"].ToString() != "0" && !String.IsNullOrEmpty(jresult["error_msg"].ToString()))
                    return jresult["error_msg"].ToString();

                FaceDectectResult faceResult = JsonConvert.DeserializeObject<FaceDectectResult>(jresult["result"].ToString());
                var quality = faceResult.face_list.FirstOrDefault()?.quality;

                if (quality != null)
                {
                    var s = CommonUtility.GetProperties<Occlusion>(quality.occlusion);
                    string pattern = @"[^\d.\d]"; // 正则表达式剔除非数字字符（不包含小数点.）
                    var values = Regex.Split(s, pattern).Where(m => !String.IsNullOrEmpty(m)).Select(m => double.Parse(m)).ToList();
                    if (values.Count(x => x > 0.7) > 0)
                    {
                        return "图像中遮挡范围太大，请重新上传！";
                    }

                    if (0.7 < quality.blur)
                    {
                        return "图像太模糊，请重新上传！";
                    }
                    if (40 > quality.illumination)
                    {
                        return "图像中光线太暗，请重新上传！";
                    }
                    if (0.7 > quality.completeness)
                    {
                        return "人脸不完整，请重新上传！";
                    }
                }
            }
            catch (AipException exp)
            {
                MessageBox.Show(exp.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WebException exp)
            {
                MessageBox.Show(exp.Message, "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Result;
        }

        /// <summary>
        /// 人脸注册
        /// </summary>
        /// <param name="image">图片转为string格式</param>
        /// <param name="groupId">用户组Id</param>
        /// <param name="userId">用户Id</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static FaceItem UserAddDemo(string image, string groupId, string userId, Dictionary<string, object> options)
        {
            FaceItem newUser = null;
            try
            {
                var imageType = "BASE64";
                // 带参数调用人脸注册
                var jresult = _faceClient.UserAdd(image, imageType, groupId, userId, options);

                var addUserResult = jresult["result"].ToString();

                newUser = JsonConvert.DeserializeObject<FaceItem>(addUserResult);
            }
            catch (AipException exp)
            {
                MessageBox.Show(exp.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WebException exp)
            {
                MessageBox.Show(exp.Message, "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return newUser;
        }

        /// <summary>
        /// 获取某一用户图像列表
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        public static List<FaceListItem> FaceGetlistDemo(String groupId, string userId)
        {
            List<FaceListItem> userFaces = null;

            try
            {
                var jresult = _faceClient.FaceGetlist(userId, groupId);

                if (jresult["error_code"].ToString() != "0" && !String.IsNullOrEmpty(jresult["error_msg"].ToString()))
                {
                    MessageBox.Show(jresult["error_msg"].ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

                var faceList = jresult["result"]["face_list"].ToString();

                userFaces = JsonConvert.DeserializeObject<List<FaceListItem>>(faceList);
            }
            catch (AipException exp)
            {
                MessageBox.Show(exp.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WebException exp)
            {
                MessageBox.Show(exp.Message, "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return userFaces;
        }

        public static List<FaceSearch> GetAllUserList()
        {
            List<FaceSearch> userinfolist = new List<FaceSearch>();

            bool flag = true;
            try
            {
                var jresult = FaceDectectHelper.GetGroupList();

                if (flag && jresult["error_code"].ToString() == "0")
                {
                    var groupList = JsonConvert.DeserializeObject<List<string>>(jresult["result"]["group_id_list"].ToString());

                    foreach (var groupId in groupList)
                    {
                        var groupResult = GroupGetusersDemo(groupId.ToString());

                        if (flag && groupResult["error_code"].ToString() == "0")
                        {
                            var users = groupResult["result"]["user_id_list"];

                            var userList = JsonConvert.DeserializeObject<List<string>>(users.ToString());

                            foreach (var userId in userList)
                            {
                                Thread.Sleep(1000);
                                var userResult = GetUser(groupId.ToString(), userId.ToString());

                                if (flag && userResult["error_code"].ToString() == "0")
                                {
                                    var userInfos = JsonConvert.DeserializeObject<List<FaceSearch>>(userResult["result"]["user_list"].ToString());

                                    userInfos.ForEach(user =>
                                    {
                                        user.user_id = userId;
                                    });

                                    userinfolist.AddRange(userInfos);
                                }
                                else
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            flag = false;
                            break;
                        }
                    }
                }
            }
            catch (AipException exp)
            {
                MessageBox.Show(exp.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WebException exp)
            {
                MessageBox.Show(exp.Message, "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return userinfolist;
        }

        public static JObject GetGroupList()
        {
            // 组列表查询
            return _faceClient.GroupGetlist(null);
        }
        public static JObject GetUser(string groupId, String userId)
        {
            return _faceClient.UserGet(userId, groupId);

        }
        public static JObject DeleteUser(string groupId, string userId)
        {
            try
            {
                return _faceClient.UserDelete(groupId, userId);
            }
            catch (AipException exp)
            {
                MessageBox.Show(exp.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WebException exp)
            {
                MessageBox.Show(exp.Message, "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;

        }
        /// <summary>
        /// 人脸搜索
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static FaceSearch SearchDemo(string image)
        {
            FaceSearch Result = null;

            try
            {
                //  var image = "取决于image_type参数，传入BASE64字符串或URL字符串或FACE_TOKEN字符串";

                var imageType = "BASE64";
                var groupList = JsonConvert.DeserializeObject<List<string>>(GetGroupList()["result"]["group_id_list"].ToString());

                var groupIdList = String.Join(",", groupList);

                var options = new Dictionary<string, object>{
        {"quality_control", "NORMAL"},
        {"liveness_control", "LOW"},
    };
                // 带参数调用人脸搜索
                var jresult = _faceClient.Search(image, imageType, groupIdList, options);

                if (jresult["error_code"].ToString() != "0" && !String.IsNullOrEmpty(jresult["error_msg"].ToString()))
                {
                    SpeechHelper.Tts(jresult["error_msg"].ToString(), null);
                    return null;
                }

                var searchResult = jresult["result"]["user_list"].ToString();
                Result = JsonConvert.DeserializeObject<List<FaceSearch>>(searchResult).FirstOrDefault();
            }
            catch (AipException exp)
            {
                MessageBox.Show(exp.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WebException exp)
            {
                MessageBox.Show(exp.Message, "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Result;
        }
        /// <summary>
        /// 根据用户组获取用户列表
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static JObject GroupGetusersDemo(string groupId, int? start = 0, int? length = 50)
        {
            try
            {
                //var result = _faceClient.GroupGetusers(groupId);
                // 如果有可选参数
                var options = new Dictionary<string, object>
                {
                    {"start", start},
                    {"length", length}
                };
                // 带参数调用获取用户列表
                return _faceClient.GroupGetusers(groupId, options);
            }
            catch (AipException exp)
            {
                MessageBox.Show(exp.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WebException exp)
            {
                MessageBox.Show(exp.Message, "网络错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

    }
    public class LocationInfo
    {
        public float left { get; set; }
        public float top { get; set; }
        public float width { get; set; }
        public float height { get; set; }
        public float rotation { get; set; }
    }
    public class ItemType
    {
        public string type { get; set; }
        public double probability { get; set; }

    }
    public class FaceItem
    {
        public string face_token { get; set; }
        public LocationInfo location { get; set; }
        public double face_probability { get; set; }
        public double beauty { get; set; }
        public ItemType expression { get; set; }
        public ItemType glasses { get; set; }
        public ItemType race { get; set; }
        public ItemType face_shape { get; set; }
        public ItemType gender { get; set; }
        public int age { get; set; }
        public ImageQuality quality { get; set; }
    }

    /// <summary>
    /// 用户图像查询返回类型
    /// </summary>
    public class FaceListItem
    {
        /// <summary>
        /// 人脸图片的唯一标识
        /// </summary>
        public string face_token { get; set; }
        /// <summary>
        /// 人脸创建时间
        /// </summary>
        public string ctime { get; set; }
    }

    public class FaceSearch
    {
        public FaceSearch() { }
        public FaceSearch(FaceSearch face)
        {
            group_id = face.group_id;
            user_id = face.user_id;
            user_info = face.user_info;
        }
        public string group_id { get; set; }
        public string user_id { get; set; }
        public string user_info { get; set; }
        public double score { get; set; }
    }
    public class ImageQuality
    {
        /// <summary>
        /// 遮挡范围
        /// </summary>
        public Occlusion occlusion { get; set; }
        /// <summary>
        /// 人脸模糊程度，范围[0~1]，0表示清晰，1表示模糊
        /// </summary>
        public double blur { get; set; }
        /// <summary>
        /// 取值范围在[0~255], 表示脸部区域的光照程度 越大表示光照越好
        /// </summary>
        public double illumination { get; set; }
        /// <summary>
        /// 人脸完整度，0或1, 0为人脸溢出图像边界，1为人脸都在图像边界内
        /// </summary>
        public Int64 completeness { get; set; }
    }

    /// <summary>
    /// 人脸各部分遮挡的概率，范围[0~1]，0表示完整，1表示不完整
    /// </summary>
    public class Occlusion
    {
        /// <summary>
        /// 左眼遮挡比例
        /// </summary>
        public double left_eye { get; set; }
        /// <summary>
        /// 右眼遮挡比例
        /// </summary>
        public double right_eye { get; set; }
        /// <summary>
        /// 鼻子遮挡比例
        /// </summary>
        public double nose { get; set; }
        /// <summary>
        /// 嘴巴遮挡比例
        /// </summary>
        public double mouth { get; set; }
        /// <summary>
        /// 左脸颊遮挡比例
        /// </summary>
        public double left_cheek { get; set; }
        /// <summary>
        /// 右脸颊遮挡比例
        /// </summary>
        public double right_cheek { get; set; }
        /// <summary>
        /// 下巴遮挡比例
        /// </summary>
        public double chin { get; set; }

    }

    public class FaceDectectResult
    {
        public int face_num { get; set; }
        public List<FaceItem> face_list { get; set; }
    }

}



