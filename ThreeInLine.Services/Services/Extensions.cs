using System;
using System.Text;
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
	}
}
