using System;

namespace ThreeInLine.Services.Logging
{
	public interface ILog
	{
		void Log(Type source, Level level, string message);
		void Log(Type source, Level level, Exception exception);
	}
}