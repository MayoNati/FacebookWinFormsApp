using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.MapProviders;
using GoogleMaps.LocationServices;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
       
        public FormMain()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 100;

            //m_AppSettings = new AppSettings();

            m_AppSettings = AppSettings.LoadFromFile();

            fBLogic = new FBLogic();
            this.StartPosition = FormStartPosition.Manual;
            this.Size = m_AppSettings.LastWindowSize;
            this.Location = m_AppSettings.LastWindowLocation;
            this.checkBoxRememberUser.Checked = m_AppSettings.RemmeberUser;
            if (m_AppSettings.RemmeberUser 
                && !string.IsNullOrEmpty(m_AppSettings.LastAccessToken))
            {
                fBLogic.Connect(m_AppSettings.LastAccessToken);
            }

        }
        AppSettings m_AppSettings;
        FBLogic fBLogic;
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if(m_AppSettings!=null && m_AppSettings.LastAccessToken==null|| m_AppSettings.LastAccessToken==string.Empty)
            {

                string accessToken = fBLogic.Login();
                if (accessToken != null)
                {
                    m_AppSettings.LastAccessToken = accessToken;
                    try
                    {
                        pictureBoxProfile = fBLogic.user.PictureBoxProfile;

                    }
                    catch
                    {
                        ;
                    }
                }
            }
           




            //fBLogic.Login();
            //       Clipboard.SetText("design.patterns20cc"); /// the current password for Desig Patter

            //       FacebookWrapper.LoginResult loginResult = FacebookService.Login(
            //               /// (This is Desig Patter's App ID. replace it with your own)
            //               "1450160541956417", 
            //               /// requested permissions:
            //"email",
            //               "public_profile"
            //               /// add any relevant permissions
            //               );

            //       buttonLogin.Text = $"Logged in as {loginResult.LoggedInUser.Name}";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            m_AppSettings.LastWindowLocation = this.Location;
            m_AppSettings.LastWindowSize = this.Size;
            m_AppSettings.RemmeberUser = this.checkBoxRememberUser.Checked;
            if (m_AppSettings.RemmeberUser)
            {
                m_AppSettings.LastAccessToken = fBLogic.AccessToken;
                m_AppSettings.SaveToFile();
            }
            else
            {
                fBLogic.AccessToken = null;
                m_AppSettings.LastAccessToken = null;
            }
        }
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            if (fBLogic.Logout())
            {
                SetLogEvent("Logout Success");
            }
            else
            {
                SetLogEvent("Logout Faild");
            }
            //         FacebookService.LogoutWithUI();
            //buttonLogin.Text = "Login";
        }

        private void richTextLogEvent_TextChanged(object sender, EventArgs e)
        {
            richTextLogEvent.SelectionStart = richTextLogEvent.Text.Length;
            richTextLogEvent.ScrollToCaret();
        }



        public void SetLogEvent(String textLog)
        {
            string templog = "";
            if (this.InvokeRequired)
            {

                this.Invoke((MethodInvoker)delegate ()
                {
                    templog = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff") + "  -  " + textLog;
                    AppendText(this.richTextLogEvent, Color.White, templog + Environment.NewLine);
                    //LogProgress.AppendLine(templog);

                });
            }
            else
            {
                templog = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff") + "  -  " + textLog;
                AppendText(this.richTextLogEvent, Color.White, templog + Environment.NewLine);
                //LogProgress.AppendLine(templog);
            }

            //logFileResult2(logFileName, LogProgress);
        }

        public void SetLogEventError(String textLog)
        {
            string templog = "";
            if (this.InvokeRequired)
            {

                this.Invoke((MethodInvoker)delegate ()
                {
                    templog = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff") + "  -  " + "Error: " + textLog;
                    AppendText(this.richTextLogEvent, Color.Red, templog + Environment.NewLine);
                });
            }
            else
            {
                templog = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss:fff") + "  -  " + "Error: " + textLog;
                AppendText(this.richTextLogEvent, Color.Red, templog + Environment.NewLine);
            }
            //LogErrorProgress.AppendLine(templog);

            //logEventError.AppendText(templog);
            //logFileResult2(logFileName, LogErrorProgress);
        }

        void AppendText(RichTextBox box, Color color, string text)
        {
            int start = box.TextLength;
            box.AppendText(text);
            int end = box.TextLength;

            // Textbox may transform chars, so (end-start) != text.Length
            box.Select(start, end - start);
            {
                box.SelectionColor = color;
                // could set box.SelectionBackColor, box.SelectionFont too.
            }
            box.SelectionLength = 0; // clear
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //fBLogic.LoadAllFriendsUser();
            fBLogic.Test();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //apikey
            //AIzaSyD7h0K97lmDqUoikBl-sKJzYN7rSMld308

            //var address = "75 Ninth Avenue 2nd and 4th Floors New York, NY 10011";
            //var address = "75 Ninth Avenue 2nd and 4th Floors New York, NY 10011";

            var locationService = new GoogleLocationService();
            var point1 = locationService.GetLatLongFromAddress("Israel");
            var latitude = point1.Latitude;
            var longitude = point1.Longitude;

            //gMapControl1
            gMapControl1.DragButton = MouseButtons.Left;       
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            double lat = 31.5574811;
            double longt = 34.8331272;
            gMapControl1.Position = new GMap.NET.PointLatLng(lat, longt);
            gMapControl1.SetPositionByKeywords("Israel, Tel Aviv");
            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 10;
            PointLatLng point = new PointLatLng(lat, longt);
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.red_pushpin);
            GMapOverlay markers = new GMapOverlay("markers");
            markers.Markers.Add(marker);
            gMapControl1.Overlays.Add(markers);






        }
    }
}
