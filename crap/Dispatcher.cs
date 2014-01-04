using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace crap
{

	public class Dispatcher<TMessage, TResult> where TResult:class
	{
		private readonly Dictionary<Type, Func<TMessage, TResult>> _dictionary = new Dictionary<Type, Func<TMessage, TResult>>();

		public void Register<T>(Func<T, TResult> func) where T : TMessage
		{
			_dictionary.Add(typeof(T), x => func((T)x));
		}

		public void Register<T>(Action<T> action) where T : TMessage
		{
			Action<TMessage> casted = x => action((T) x);
			_dictionary.Add(typeof(T), q => { 
				casted(q);
				return Nothing.Value as TResult;
			});
		}

		public TResult Dispatch(TMessage m)
		{
			Func<TMessage, TResult> handler;
			if (!_dictionary.TryGetValue(m.GetType(), out handler))
			{
				throw new Exception("cannot map " + m.GetType());
			}
			return handler(m);
		}
	}
}
