using System.Collections.Concurrent;

namespace Tarefas.API.Logging
{
	public class CustomLoggerProvider 
	{
		readonly CustomLoggerPrividerConfiguration loggerConfig;
		readonly ConcurrentDictionary<string, CustumerLogger> logger = new ConcurrentDictionary<string, CustumerLogger> ();
		//public ILogger CreateLogger(string categoryName)
		//{
		//	//return logger.GetOrAdd(categoryName, name => CustumerLogger(name, loggerConfig));
		//}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
