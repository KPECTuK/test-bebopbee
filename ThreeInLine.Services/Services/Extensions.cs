using System;
using System.Diagnostics;
using System.Text;
using ThreeInLine.Services.Logging;
using UnityEngine;

namespace ThreeInLine.Services
{
	public static class Extensions
	{
		public static Vector3 ProjectXZ(this Vector3 vector)
		{
			return new Vector3(vector.x, 0f, vector.z);
		}

		public static string ToText(this Exception source)
		{
			if(source == null)
				return "exception is null";
			var exception = source;
			var builder = new StringBuilder();
			var counter = 0;
			while(exception != null)
			{
				builder.AppendLine("-- ." + ++counter);
				builder.Append("Exception: ");
				builder.AppendLine(exception.GetType().Name);
				builder.Append("Message: ");
				builder.AppendLine(exception.Message.TrimEnd('\n'));
				builder.AppendLine("Trace: ");
				builder.AppendLine(exception.StackTrace);
				exception = exception.InnerException;
			}
			builder.Append("-- .end exceptions trace");
			return builder.ToString();
		}
		
		[Conditional("DEBUG")]
		public static void Log(this object @object, Level level, string message)
		{
			Container.Container.CompositionRoot.Resolve<ILog>().Log(@object.GetType(), level, message);
		}

		[Conditional("DEBUG")]
		public static void Log(this object @object, Level level, Exception exception)
		{
			Container.Container.CompositionRoot.Resolve<ILog>().Log(@object.GetType(), level, exception);
		}
	}
}
