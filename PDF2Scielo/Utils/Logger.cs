//
// Foo.cs: A class that implements
//
// Author:
//   Hector E. Gomez Morales (hectoregm@gmail.com)
//   Anaid V. Velazquez Rivera (anaidv@gmail.com)
//   Virginia Teodosio Procopio (ainigriv_t@hotmail.com)
//   Juan Germán Castañeda Echevarría (juanger@gmail.com)
//
// Copyright (C) 2007 UNAM DGB
//

using System;
using System.IO;

namespace Scielo.Utils {

public enum Level { DEBUG, INFO, WARNING, ERROR };

public interface ILogger
{
	void Log (Level lvl, string msg, params object[] args);
}

class NullLogger : ILogger
{
	public void Log (Level lvl, string msg, params object[] args)
	{
	}
}

class ConsoleLogger : ILogger
{
	public void Log (Level lvl, string msg, params object[] args)
	{
		msg = string.Format ("[{0}]: {1}", Enum.GetName (typeof (Level), lvl), msg);
		Console.WriteLine (msg, args);
	}
}

class FileLogger : ILogger
{
	StreamWriter log;
	ConsoleLogger console;
	
	public FileLogger ()
	{
		try {
			log = File.CreateText (Path.Combine (
				Environment.GetEnvironmentVariable ("HOME"), 
				".pdf2scielo.log"));
			log.Flush ();
		} catch (IOException) {
			// FIXME: Use temp file
		}
		
		console = new ConsoleLogger ();
	}
	
	~FileLogger ()
	{
		if (log != null)
			log.Flush ();
	}
	
	public void Log (Level lvl, string msg, params object[] args)
	{
		console.Log (lvl, msg, args);
		
		if (log != null) {
			msg = string.Format ("{0} [{1}]: {2}",
						DateTime.Now.ToString(),
						Enum.GetName (typeof (Level), lvl),
						msg);
			log.WriteLine (msg, args);
			log.Flush();
		}
	}
}

// This class provides a generic logging facility. By default all
// information is written to standard out and a log file, but other 
// loggers are pluggable.
public static class Logger
{
	private static Level log_level = Level.INFO;
	
	static ILogger log_dev = new FileLogger ();
	
	static bool muted = false;
	
	public static Level LogLevel
	{
		get { return log_level; }
		set { log_level = value; }
	}
	
	public static ILogger LogDevice
	{
		get { return log_dev; }
		set { log_dev = value; }
	}
	
	public static void Debug (string msg, params object[] args)
	{
		Log (Level.DEBUG, msg, args);
	}
	
	public static void Warning (string msg, params object[] args)
	{
		Log (Level.WARNING, msg, args);
	}
	
	public static void Error (string msg, params object[] args)
	{
		Log (Level.ERROR, msg, args);
	}
	
	public static void INFO (string msg, params object[] args)
	{
		Log (Level.INFO, msg, args);
	}
	
	public static void Log (Level lvl, string msg, params object[] args)
	{
		if (!muted && lvl >= log_level)
			log_dev.Log (lvl, msg, args);
	}
	
	public static void Mute ()
	{
		muted = true;
	}
	
	public static void ActivateDebug ()
	{
		log_level = Level.DEBUG;
	}
	
	public static void Unmute ()
	{
		muted = false;
	}
}
}
