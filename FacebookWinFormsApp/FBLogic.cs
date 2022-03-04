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

namespace BasicFacebookFeatures
{
    public class FBLogic
    {
        readonly string m_UsersFilePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\FBUsers.xml";
        private List<FBUser> listAllUsersFriends = new List<FBUser>();

        //FacebookWrapper.FacebookService.s_CollectionLimit = 100;

        User m_LoggedInUser;
        LoginResult m_LoginResult;
        private FacebookWrapper.LoginResult loginResult;
        public bool Login()
        {
            Clipboard.SetText("design.patterns20cc"); /// the current password for Desig Patter

            try
            {
                   loginResult = FacebookService.Login(
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
                /// add any relevant permissions
                /// 

                if (loginResult!=null && !string.IsNullOrEmpty(loginResult.AccessToken))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
           
                   
            //buttonLogin.Text = $"Logged in as {loginResult.LoggedInUser.Name}";

        }
        public bool Logout()
        {

            try
            {
                FacebookService.LogoutWithUI();

                if (loginResult == null || string.IsNullOrEmpty(loginResult.AccessToken))
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


        public void LoadAllFriendsUser()
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
                    listAllUsersFriends.Add(new FBUser() { UserName= userName, Gender=gender,Location=location, Age = age } );
                }
            }
            catch(Exception mes)
            { 

                ;
            
            }
        } 

    }

    public class FBUser
    {
        public string UserName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }

    }
}
