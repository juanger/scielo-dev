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
using System.Collections;

namespace Scielo.Utils {

public enum Level { DEBUG, INFO, WARNING, ERROR };

public interface ILogger
{
	void ClearList ();
	void Log (Level lvl, string msg, params object[] args);
}

class FileLogger : ILogger
{
	StreamWriter log;
	ArrayList log_list;
	public int debug = 0, warning = 0, info = 0, error = 0;
	
	public FileLogger ()
	{
		try {
			log_list = new ArrayList (5);
			log = File.CreateText (Path.Combine (
				Environment.GetEnvironmentVariable ("HOME"), 
				".pdf2scielo.log"));
			log.Flush ();
		} catch (IOException) {
			// FIXME: Use temp file
		}
	}
	
	~FileLogger ()
	{
		if (log != null)
			log.Flush ();
	}
	
	public void Log (Level lvl, string msg, params object[] args)
	{
		if (log != null) {
			// FILE
			string file_msg = string.Format ("{0} [{1}]: {2}",
						DateTime.Now.ToString(),
						Enum.GetName (typeof (Level), lvl),
						msg);
			log.WriteLine (file_msg, args);
			log.Flush();
			
			// CONSOLE
			string console_msg = string.Format ("[{0}]: {1}",
						Enum.GetName (typeof (Level), lvl),
						msg);
			Console.WriteLine (console_msg, args);
			// LIST
			string list_msg = string.Format (msg, args);
			LogEntry entry = new LogEntry (lvl, list_msg);
			log_list.Add (entry);			
		}
	}
	
	public void ClearList ()
	{
		error = 0;
		warning = 0;
		info = 0;
		debug = 0;
		log_list.Clear ();
	}
	
	public ArrayList List {
		get {
			return log_list;
		}
	}
}


public class LogEntry {
	Level lvl;
	string msg;
	
	public LogEntry (Level lvl, string msg) 
	{
		this.lvl = lvl;
		this.msg = msg;
	}
	
	public Level Level {
		get {
			return lvl;
		}
	}
	
	public string Message {
		get {
			return msg;
		}
	}

}
// This class provides a generic logging facility. By default all
// information is written to standard out and a log file, but other 
// loggers are pluggable.
public static class Logger
{
	private static Level log_level = Level.INFO;
	
	static FileLogger log_dev = new FileLogger ();
	
	static bool muted = false;
	
	public static Level LogLevel
	{
		get { return log_level; }
		set { log_level = value; }
	}
	
	public static void Debug (string msg, params object[] args)
	{
		log_dev.debug++;
		Log (Level.DEBUG, msg, args);
	}
	
	public static void Warning (string msg, params object[] args)
	{
		log_dev.warning++;
		Log (Level.WARNING, msg, args);
	}
	
	public static void Error (string msg, params object[] args)
	{
		log_dev.error++;
		Log (Level.ERROR, msg, args);
	}
	
	public static void Info (string msg, params object[] args)
	{
		log_dev.info++;
		Log (Level.INFO, msg, args);
	}
	
	private static void Log (Level lvl, string msg, params object[] args)
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
	
	public static void ClearList ()
	{
		log_dev.ClearList ();
	}
	
	public static ArrayList List {
		get {
			return log_dev.List;
		}
	}
	
	public static int NumMessages {
		get{
			return log_dev.info;
		}
	}
	
	public static int NumWarns {
		get{
			return log_dev.warning;
		}
	}
	
	public static int NumErrors {
		get{
			return log_dev.error;
		}
	}
}
}
