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
using System.IO;
using System.Windows.Forms;
using log4net;

namespace yok.App.Forms
{
    public partial class PanelStatus : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (PanelStatus));

        public PanelStatus()
        {
            InitializeComponent();

            try
            {
                var strAttributes = "Logs Info:\n";

                var logFile = new FileInfo(Logs.LogProc);
                if (logFile.Exists)
                {
                    strAttributes += Globals.YokFileLog + " size: " + logFile.Length + " bytes\n";
                }

                var keyFile = new FileInfo(Logs.LogKey);
                if (keyFile.Exists)
                {
                    strAttributes += Globals.YokFileKey + " size: " + keyFile.Length + " bytes";
                }

                textBox.Text = strAttributes;
            }
            catch (Exception ex)
            {
                textBox.Text = "Error while reading the log files!";

                Logger.Error(ex);
            }
        }
    }
}