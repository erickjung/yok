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
using System.Reflection;
using System.Windows.Forms;
using log4net;
using yok.App.Forms;

namespace yok.App.Engine
{
    /// <summary>
    ///     The main application execution
    /// </summary>
    internal class ApplicationEngine
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (ApplicationEngine));

        /// <summary>
        ///     The main form
        /// </summary>
        private static MainForm _mainForm;

        /// <summary>
        ///     Start the main form as hided
        /// </summary>
        private static void StartMainForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _mainForm = new MainForm();
            _mainForm.Hide();

            Application.Run();
        }

        /// <summary>
        ///     Initialize the application
        /// </summary>
        public static void Initialize()
        {
            Logger.Info(Globals.YokAppName);
            Logger.Info("Version: " + Assembly.GetExecutingAssembly().GetName().Version);
            Logger.Info(Globals.YokAppCopyright);
            Logger.Info("Starting at: " + DateTime.Now);

            AutoStartEngine.Process();

            StartMainForm();
        }
    }
}