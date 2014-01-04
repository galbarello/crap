using System;
using System.Data;
using System.Data.SqlClient;

namespace crap
{
	public class PartialAppPlayground
	{
		private static Dispatcher<Command, Nothing> _dispatcher;

		public static void Initialize()
		{
			_dispatcher = new Dispatcher<Command, Nothing>();
			_dispatcher.Register<DoBar>(message => CommandHandlers.Bar(() => new SqlConnection(), message));
			_dispatcher.Register<DoFoo>(message => CommandHandlers.Log<DoFoo>(new ConsoleLogger(),message,
				logged =>CommandHandlers.Foo(new SqlConnection(), logged)));
		}

		public static void Main()
		{
			Initialize();
			_dispatcher.Dispatch(new DoBar());
			_dispatcher.Dispatch(new DoFoo());
		}
	}




}
