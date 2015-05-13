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
using System.Configuration;
using log4net;

namespace yok.Util
{
    /// <summary>
    ///     Helper class to work with exe config
    /// </summary>
    public class Config
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (Config));

        /// <summary>
        ///     Set and save string value
        /// </summary>
        /// <param name="param">The config parameter</param>
        /// <param name="value">The config value</param>
        public static void SetStringValue(string param, string value)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings[param].Value = value;

                config.Save(ConfigurationSaveMode.Modified);

                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        ///     Set and save bool value
        /// </summary>
        /// <param name="param">The config parameter</param>
        /// <param name="value">The config value</param>
        public static void SetBoolValue(string param, bool value)
        {
            SetStringValue(param, value.ToString());
        }

        /// <summary>
        ///     Set and save int value
        /// </summary>
        /// <param name="param">The config parameter</param>
        /// <param name="value">The config value</param>
        public static void SetIntValue(string param, int value)
        {
            SetStringValue(param, value.ToString());
        }

        /// <summary>
        ///     Get config value
        /// </summary>
        /// <param name="param">The config parameter</param>
        /// <returns></returns>
        public static string GetStringValue(string param)
        {
            return ConfigurationManager.AppSettings[param];
        }

        /// <summary>
        ///     Get config value
        /// </summary>
        /// <param name="param">The config parameter</param>
        /// <returns></returns>
        public static bool GetBoolValue(string param)
        {
            return Convert.ToBoolean(GetStringValue(param));
        }

        /// <summary>
        ///     Get config value
        /// </summary>
        /// <param name="param">The config parameter</param>
        /// <returns></returns>
        public static int GetIntValue(string param)
        {
            return Convert.ToInt32(GetStringValue(param));
        }
    }
}