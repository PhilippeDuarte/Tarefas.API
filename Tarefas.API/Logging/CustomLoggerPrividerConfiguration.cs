namespace Tarefas.API.Logging
{
	public class CustomLoggerPrividerConfiguration
	{
		public LogLevel LogLevel { get; set; } = LogLevel.Warning;
		public int EventId { get; set; } = 0;
	}
}
