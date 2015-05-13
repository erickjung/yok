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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using log4net;

namespace yok.Util
{
    /// <summary>
    ///     Helper class to key hooker
    /// </summary>
    internal class Hook
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (Hook));
        public static int ModAlt = 0x1;
        public static int ModControl = 0x2;
        public static int ModShift = 0x4;
        public static int ModWin = 0x8;
        public static int WmHotkey = 0x312;
        private static int _keyId;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        ///     Registry hook key
        /// </summary>
        /// <param name="f"></param>
        /// <param name="key"></param>
        public static void RegisterHotKey(Form f, Keys key)
        {
            var modifiers = 0;

            if ((key & Keys.Alt) == Keys.Alt)
            {
                modifiers = modifiers | ModAlt;
            }

            if ((key & Keys.Control) == Keys.Control)
            {
                modifiers = modifiers | ModControl;
            }

            if ((key & Keys.Shift) == Keys.Shift)
            {
                modifiers = modifiers | ModShift;
            }

            var k = key & ~Keys.Control & ~Keys.Shift & ~Keys.Alt;

            _keyId = f.GetHashCode();
            RegisterHotKey(f.Handle, _keyId, modifiers, (int) k);
        }

        /// <summary>
        ///     Unregister hook key
        /// </summary>
        /// <param name="f"></param>
        public static void UnregisterHotKey(Form f)
        {
            try
            {
                UnregisterHotKey(f.Handle, _keyId);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}