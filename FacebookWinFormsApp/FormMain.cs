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

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
        FBLogic fBLogic = new FBLogic();
        public FormMain()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 100;
        }
       
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (fBLogic.Login())
            {
                SetLogEvent("Login Success");
            }
            else
            {
                SetLogEvent("Login Faild");
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


    }
}
