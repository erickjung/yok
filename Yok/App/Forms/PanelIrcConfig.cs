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
    public partial class PanelIrcConfig : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (PanelIrcConfig));

        public PanelIrcConfig()
        {
            InitializeComponent();
        }

        private void PanelIrcConfigLoad(object sender, EventArgs e)
        {
            try
            {
                textBoxServer.Text = Config.GetStringValue(Globals.YokXmlIrcserver);
                textBoxPort.Text = Config.GetStringValue(Globals.YokXmlIrcport);
                textBoxChannel.Text = Config.GetStringValue(Globals.YokXmlIrcchannel);
                textBoxNickname.Text = Config.GetStringValue(Globals.YokXmlIrcnick);
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
                if (!textBoxServer.Text.Equals(Crypt.Decrypt(Config.GetStringValue(Globals.YokXmlIrcserver))))
                {
                    Config.SetStringValue(Globals.YokXmlIrcserver, textBoxServer.Text);
                }

                if (!textBoxPort.Text.Equals(Crypt.Decrypt(Config.GetStringValue(Globals.YokXmlIrcport))))
                {
                    Config.SetStringValue(Globals.YokXmlIrcport, textBoxPort.Text);
                }

                if (!textBoxChannel.Text.Equals(Crypt.Decrypt(Config.GetStringValue(Globals.YokXmlIrcchannel))))
                {
                    Config.SetStringValue(Globals.YokXmlIrcchannel, textBoxChannel.Text);
                }

                if (!textBoxNickname.Text.Equals(Crypt.Decrypt(Config.GetStringValue(Globals.YokXmlIrcnick))))
                {
                    Config.SetStringValue(Globals.YokXmlIrcnick, textBoxNickname.Text);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}