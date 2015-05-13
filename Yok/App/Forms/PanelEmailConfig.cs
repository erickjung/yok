// Yok 
// https://github.com/erickjung/yok
//
// Copyright (c) 2012-2015 Erick Jung
//
// This code is distributed under the terms and conditions of the MIT license.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Windows.Forms;
using log4net;
using yok.Util;

namespace yok.App.Forms
{
    public partial class PanelEmailConfig : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (PanelEmailConfig));

        public PanelEmailConfig()
        {
            InitializeComponent();
        }

        private void PanelEmailConfigLoad(object sender, EventArgs e)
        {
            try
            {
                textBoxLogin.Text = Config.GetStringValue(Globals.YokXmlEmaillogin);
                textBoxPassword.Text = Config.GetStringValue(Globals.YokXmlEmailpwd);
                textBoxSmtp.Text = Config.GetStringValue(Globals.YokXmlEmailsmtp);
                textBoxSmtpPort.Text = Config.GetStringValue(Globals.YokXmlEmailsmtpport);
                textBoxTo.Text = Config.GetStringValue(Globals.YokXmlEmailto);
                textBoxSubject.Text = Config.GetStringValue(Globals.YokXmlEmailsubject);
                textBoxMessage.Text = Config.GetStringValue(Globals.YokXmlEmailmsg);
                checkBoxSSL.Checked = Config.GetBoolValue(Globals.YokXmlEmailssl);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            try
            {
                if (!textBoxLogin.Text.Equals(Config.GetStringValue(Globals.YokXmlEmaillogin)))
                {
                    Config.SetStringValue(Globals.YokXmlEmaillogin, textBoxLogin.Text);
                }

                if (!textBoxPassword.Text.Equals(Config.GetStringValue(Globals.YokXmlEmailpwd)))
                {
                    Config.SetStringValue(Globals.YokXmlEmailpwd, textBoxPassword.Text);
                }

                if (!textBoxSmtp.Text.Equals(Config.GetStringValue(Globals.YokXmlEmailsmtp)))
                {
                    Config.SetStringValue(Globals.YokXmlEmailsmtp, textBoxSmtp.Text);
                }

                if (!textBoxSmtpPort.Text.Equals(Config.GetStringValue(Globals.YokXmlEmailsmtpport)))
                {
                    Config.SetStringValue(Globals.YokXmlEmailsmtpport, textBoxSmtpPort.Text);
                }

                if (!textBoxTo.Text.Equals(Config.GetStringValue(Globals.YokXmlEmailto)))
                {
                    Config.SetStringValue(Globals.YokXmlEmailto, textBoxTo.Text);
                }

                if (!textBoxSubject.Text.Equals(Config.GetStringValue(Globals.YokXmlEmailsubject)))
                {
                    Config.SetStringValue(Globals.YokXmlEmailsubject, textBoxSubject.Text);
                }

                if (!textBoxMessage.Text.Equals(Config.GetStringValue(Globals.YokXmlEmailmsg)))
                {
                    Config.SetStringValue(Globals.YokXmlEmailmsg, textBoxMessage.Text);
                }

                Config.SetBoolValue(Globals.YokXmlEmailssl, checkBoxSSL.Checked);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}