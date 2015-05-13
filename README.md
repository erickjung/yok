
Did you imagined what is going on your computer in your absence?

YOK is a tool which allows you to find out what other users do on your computer when you are out. 
It is primaly designed to be hidden and monitoring the computer activity.

Also, YOK gives you the opportunity to control your computer by distance and ANONYMOUS, using IRC (Internet Relay Chat) as the main communication platform. You just need to use any client out there (for desktop, web, mobile, etc), log in a #channel and talk with your computer through a irc bot.

And to give you more power, YOK extends and gives you a way to do even more than just watch. 
You can extend functionalities using LUA script language, running all the scripts by distance.

## Functionalities

This is a very high level feature list:

```
- Log every key pressed
- Send results through email
- Remote computer control by IRC
- Execute scripts in LUA
```

## Usage

### Starting

```
1 - After starting YOK he will be working in background
2 - To open press `CTRL` + `ALT` + `K`
3 - He will open a password popup with a fake msg (by default is an adobe msg)
4 - By default the password is `12345` - Change in config later
5 - The control panel will be showed
```

### Config

On control panel you can configure several things:

#### [MAIN CONFIG]

```
- password  -> the control panel password
- update    -> timer to send data
- input dlg -> fake text to be shown on password popup
- services  -> enable/disable services
```

#### [EMAIL CONFIG]

```
- config  -> email account details to send
- message -> email message config
```

#### [IRC CONFIG]

```
- config -> irc server details (including channel and bot name)
```

Obs. At bottom left there is a button to send you to the YOK's folder.

### IRC Commands

```
- targetname   -> the computer's name
- sendemail    -> send the logs through email now
- loginfo      -> show details about the logging 
- disconnect   -> turn off yok
- ch_email     -> change the email config
- ch_irc       -> change the irc config
- lua_files    -> print all the lua files inside the computer
- lua_download -> send a new lua script 
- lua_exec     -> execute a specific lua script
- lua_execstr  -> execute a specific lua string directly through irc
- lua_del      -> remove a specific lua script
```

## License
[MIT](https://github.com/erickjung/yok/blob/master/LICENSE)

YOk uses projects bellow:

- SmartIRC4Net (LGPL)
