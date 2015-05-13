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
using log4net;
using Microsoft.Win32;

namespace yok.App.Engine
{
    /// <summary>
    ///     Helper class to autostart function
    /// </summary>
    internal class AutoStartEngine
    {
        /// <summary>
        ///     The main windows run location
        /// </summary>
        private const string RunLocation = @"Software\Microsoft\Windows\CurrentVersion\Run";

        private static readonly ILog Logger = LogManager.GetLogger(typeof (AutoStartEngine));

        /// <summary>
        ///     Set the registry entry
        /// </summary>
        /// <param name="keyName">Registry key</param>
        /// <param name="assemblyLocation">Location</param>
        private static void Set(string keyName, string assemblyLocation)
        {
            var key = Registry.CurrentUser.CreateSubKey(RunLocation);

            if (key != null)
            {
                key.SetValue(keyName, assemblyLocation);
            }
        }

        /// <summary>
        ///     Unset the registry key
        /// </summary>
        /// <param name="keyName">Registry key</param>
        private static void Unset(string keyName)
        {
            var key = Registry.CurrentUser.CreateSubKey(RunLocation);

            if (key != null)
            {
                key.DeleteValue(keyName);
            }
        }

        /// <summary>
        ///     Check if the key is already enabled
        /// </summary>
        /// <param name="keyName">Registry key</param>
        /// <param name="assemblyLocation">Location</param>
        /// <returns></returns>
        private static bool IsEnabled(string keyName, string assemblyLocation)
        {
            var key = Registry.CurrentUser.OpenSubKey(RunLocation);

            if (key == null)
            {
                return false;
            }

            var value = (string) key.GetValue(keyName);

            if (value == null)
            {
                return false;
            }

            return (value == assemblyLocation);
        }

        /// <summary>
        ///     The main process engine
        /// </summary>
        public static void Process()
        {
            // set autostart
            Logger.Info("Init autostart");

            try
            {
                if (!IsEnabled("yok", Assembly.GetExecutingAssembly().Location))
                {
                    Set("yok", Assembly.GetExecutingAssembly().Location);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}