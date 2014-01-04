using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace crap
{

	public static class CommandHandlers
	{
		public static void Bar(Func<IDbConnection> connection, DoBar command)
		{
			Console.WriteLine("bar");
		}

		public static Nothing Foo(IDbConnection connection, DoFoo command)
		{
			Console.WriteLine("foo");
			return Nothing.Value;
		}

		public static Nothing Log<T>(ILogger logger,T command,Func<T,Nothing> next) where T:Command
		{
			logger.Log (command);
			next (command);
			return Nothing.Value;
		}
	}

	public interface ILogger
	{
		void Log(Message message);
	}

	public class ConsoleLogger:ILogger
	{
		public void Log(Message message)
		{
			Console.WriteLine (string.Format("starting {0}",message.ToString ()));
		}
	}
}
