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
using System.Globalization;
using log4net;
using yok.Util;

namespace yok.App.Engine
{
    /// <summary>
    ///     Scheduler engine
    /// </summary>
    internal class ScheduleEngine
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (ScheduleEngine));

        /// <summary>
        ///     Check the lastupdate parameter
        /// </summary>
        /// <returns></returns>
        public static bool Check()
        {
            Logger.Info("Checking schedule");

            try
            {
                //02/06/2010 14:44:51
                var lastUpdate = Config.GetStringValue(Globals.YokXmlLastupdate);

                if (lastUpdate.Length <= 0)
                {
                    Logger.Info("First time run");

                    Config.SetStringValue(Globals.YokXmlLastupdate, DateTime.Now.ToString(CultureInfo.InvariantCulture));

                    Logger.Info("Schedule OK at " + DateTime.Now);

                    return true;
                }

                var toUpdate = Config.GetIntValue(Globals.YokXmlTimetoupdate);
                Logger.Info("Schedule File: " + lastUpdate);
                Logger.Info("Schedule Now:  " + DateTime.Now);

                var datetime = Convert.ToDateTime(lastUpdate);

                datetime = datetime.AddHours(toUpdate);

                var begin = false;

                if (toUpdate < 24)
                {
                    if (datetime.Hour > DateTime.Now.Hour)
                    {
                        begin = true;
                    }
                }
                else if (toUpdate >= 24)
                {
                    if (datetime.Day > DateTime.Now.Day)
                    {
                        begin = true;
                    }
                }

                if (begin)
                {
                    Config.SetStringValue(Globals.YokXmlLastupdate, datetime.ToString(CultureInfo.InvariantCulture));

                    Logger.Info("Schedule OK at " + DateTime.Now);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            Logger.Info("End schedule");

            return false;
        }
    }
}