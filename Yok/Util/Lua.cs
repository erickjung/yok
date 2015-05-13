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

namespace yok.Util
{
    /// <summary>
    ///     Interface to use lua dll
    /// </summary>
    internal class Lua
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PrintCallback(string text);

        private const string Luadllname = "lua514.dll";
        //some Lua defines
        /* option for multiple returns in `lua_pcall' and `lua_call' */
        public const int LuaMultret = (-1);
        //the first of any calls to Lua.
        //get a valid Lua state to operate on
        [DllImport(Luadllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr luaL_newstate();

        //open all Lua libraries
        [DllImport(Luadllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern void luaL_openlibs(IntPtr luaState);

        //close Lua
        [DllImport(Luadllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_close(IntPtr luaState);

        //load a Lua script string into the Lua state
        [DllImport(Luadllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaL_loadstring(IntPtr luaState, string s);

        //load a Lua script file into the Lua state
        [DllImport(Luadllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaL_loadfile(IntPtr luaState, string filename);

        //call a lua function, a function can be a Lua script loaded into the Lua state
        [DllImport(Luadllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_pcall(IntPtr luaState, int nargs, int nresults, int errfunc);

        [DllImport(Luadllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern string lua_tolstring(IntPtr luaState, int index, out int strLen);

        [DllImport(Luadllname, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_setPrintCallback(MulticastDelegate callback);
    }
}