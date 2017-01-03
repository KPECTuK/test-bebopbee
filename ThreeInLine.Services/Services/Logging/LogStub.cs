using System;

namespace ThreeInLine.Services.Logging
{
	public class LogStub : ILog
	{
		public void Log(Type source, Level level, string message) { }

		public void Log(Type source, Level level, Exception exception) { }
	}
}
