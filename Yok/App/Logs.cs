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

using System.IO;
using yok.Util;

namespace yok.App
{
    /// <summary>
    ///     Helper class to help with log files
    /// </summary>
    public class Logs
    {
        public static string LogKey { set; get; }
        public static string LogKeyBackup { set; get; }
        public static string LogProc { set; get; }
        public static string LogProcBackup { set; get; }

        /// <summary>
        ///     Initialize all get/setters and check if we already
        ///     have backup files, so we can delete
        /// </summary>
        public static void Initialize()
        {
            CheckLogs(Globals.YokFileLog);
            CheckLogs(Globals.YokFileKey);

            LogKey = Utils.GetBasePath() + "\\" + Globals.YokFileKey;
            LogKeyBackup = Utils.GetBasePath() + "\\" + "_" + Globals.YokFileKey;
            LogProc = Utils.GetBasePath() + "\\" + Globals.YokFileLog;
            LogProcBackup = Utils.GetBasePath() + "\\" + "_" + Globals.YokFileLog;
        }

        /// <summary>
        ///     Check logs, so we can delete the backups
        /// </summary>
        /// <param name="file">Filename</param>
        private static void CheckLogs(string file)
        {
            var fileLog = new FileInfo(Utils.GetBasePath() + "\\" + "_" + file);

            if (fileLog.Exists)
            {
                File.Delete(Utils.GetBasePath() + "\\" + "_" + file);

                var fileLogNormal = new FileInfo(Utils.GetBasePath() + "\\" + file);
                if (fileLogNormal.Exists)
                {
                    File.Delete(Utils.GetBasePath() + "\\" + file);
                }
            }
        }
    }
}