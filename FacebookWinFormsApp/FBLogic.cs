using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using Xamarin.Forms.Maps;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using System.Drawing;
using MapElement = Windows.UI.Xaml.Controls.Maps.MapElement;
using Windows.Services.Maps;
using static Uno.UI.FeatureConfiguration;

namespace BasicFacebookFeatures
{
    public class FBLogic
    {
        readonly string m_UsersFilePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\FBUsers.xml";
        private List<FBUser> listAllUsersFriends = new List<FBUser>();
        public PictureBox PictureBoxProfile { get; private set; }
        public FBUser user = new FBUser();
        public string AccessToken { get; set; }

        private FacebookWrapper.LoginResult m_LoginResult;
        private FacebookWrapper.ObjectModel.User m_LoggedInUser;
        public string Login()
        {
            Clipboard.SetText("design.patterns20cc"); /// the current password for Desig Patter

            try
            {
                m_LoginResult = FacebookService.Login(
                /// (This is Desig Patter's App ID. replace it with your own)
                //"1450160541956417",
                "289790089925279",
                   /// requested permissions:
                   "email",
                "public_profile",
                "user_age_range",
                "user_birthday",
                "user_events",
                "user_friends",
                "user_gender",
                "user_hometown",
                "user_likes",
                "user_link",
                "user_location",
                "user_photos",
                "user_posts",
                "user_videos");


                

                //user.PictureBoxProfile.ImageLocation = m_LoggedInUser.PictureNormalURL;

                /// add any relevant permissions
                /// 

                if (m_LoginResult != null && !string.IsNullOrEmpty(m_LoginResult.AccessToken))
                {
                    m_LoggedInUser = m_LoginResult.LoggedInUser;
                    fetchUserInfo();
                    AccessToken = m_LoginResult.AccessToken;
                    //user.PictureBoxProfile.ImageLocation = m_LoggedInUser.PictureNormalURL;
                    return m_LoginResult.AccessToken;
                }
            }
            catch
            {
                return null;
            }
            return null;

            //buttonLogin.Text = $"Logged in as {loginResult.LoggedInUser.Name}";

        }

        public void Connect(string accessToken)
        {
            try
            {
                m_LoginResult = FacebookService.Connect(accessToken);
            }
            catch
            {
                throw new NotImplementedException();
            }

        }

        
        public bool Logout()
        {

            try
            {
                FacebookService.LogoutWithUI();

                if (m_LoginResult == null || string.IsNullOrEmpty(m_LoginResult.AccessToken))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private void fetchUserInfo()
        {
            PictureBox pictureBoxProfile = new PictureBox();
            pictureBoxProfile.LoadAsync(m_LoggedInUser.PictureNormalURL);
            this.user.PictureBoxProfile = pictureBoxProfile;
            if (m_LoggedInUser.Posts.Count > 0)
            {
                //textBoxStatus.Text = m_LoggedInUser.Posts[0].Message;
            }
        }

        public bool LoadAllFriendsUser()
        {
            try
            {
                listAllUsersFriends.Clear();
                string folder = Application.StartupPath;
                if (folder.ToLower().EndsWith(@"\debug"))
                    folder = folder.Substring(0, folder.Length - @"\Debug".Length);
                if (folder.ToLower().EndsWith(@"\x64"))
                    folder = folder.Substring(0, folder.Length - @"\bin".Length);
                if (folder.ToLower().EndsWith(@"\bin"))
                    folder = folder.Substring(0, folder.Length - @"\bin".Length);
                string filepath = Path.Combine(folder, "FBFriends.xml");
                var users = XElement.Load(filepath);
                foreach (var item in users.Elements("user"))
                {

                    string userName = item.Element("name").Value;
                    int age = Convert.ToInt32(item.Element("age").Value);
                    string gender = item.Element("gender").Value;
                    string location = item.Element("location").Value;
                    listAllUsersFriends.Add(new FBUser() { UserName = userName, Gender = gender, Location = location, Age = age });
                }
                return true;
            }
            catch (Exception mes)
            {
                return false;
            }

        }

        public void Test()
        {

            try
            {
                var f = m_LoggedInUser.FriendLists;
                var f1 = m_LoggedInUser.Friends;
                //m_LoginResult = FacebookService.Get
                //var f= m_LoginResult.
            }
            catch
            {

            }
        }
        //public void AddSpaceNeedleIcon()
        //{
        //    var MyLandmarks = new List<MapElement>();

        //    BasicGeoposition snPosition = new BasicGeoposition { Latitude = 47.620, Longitude = -122.349 };
        //    Geopoint snPoint = new Geopoint(snPosition);
        //    Point point = new Point(1, 1);
        //    var spaceNeedleIcon = new MapIcon
        //    {
        //        Location = snPoint,
        //        //NormalizedAnchorPoint = point;

        //    NormalizedAnchorPoint = new Point(),
        //        ZIndex = 0,
        //        Title = "Space Needle"
        //    };

        //    MyLandmarks.Add(spaceNeedleIcon);

        //    var LandmarksLayer = new MapElementsLayer
        //    {
        //        ZIndex = 1,
        //        MapElements = MyLandmarks
        //    };

        //    myMap.Layers.Add(LandmarksLayer);

        //    myMap.Center = snPoint;
        //    myMap.ZoomLevel = 14;

        //}
    }

    public class FBUser
    {
        public PictureBox PictureBoxProfile { get; set; }

        public string UserName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }

    }
}
