using System;
using System.Diagnostics;
using ThreeInLine.Services;
using ThreeInLine.Services.Container;
using ThreeInLine.Services.Logging;
using UnityEngine;
using UnityEngine.Internal;

// ReSharper disable once CheckNamespace
public static class Debug
{
	private class LogHandler : ILog
	{
		public void Log(Type source, Level level, string message)
		{
			if(level == Level.Debug)
				Debug.Log(message);
			if(level == Level.Notice)
				Debug.Log(message);
			if(level == Level.Info)
				Debug.Log(message);
			if(level == Level.Warn)
				LogWarning(message);
			if(level == Level.Error)
				LogError(message);
		}

		public void Log(Type source, Level level, Exception exception)
		{
			if (level == Level.Debug)
				Debug.Log(exception.ToText());
			if (level == Level.Notice)
				Debug.Log(exception.ToText());
			if (level == Level.Info)
				Debug.Log(exception.ToText());
			if (level == Level.Warn)
				LogWarning(exception.ToText());
			if (level == Level.Error)
				LogError(exception.ToText());
		}
	}

	public static void RegisterLogger(IContainer container)
	{
		container.RegisterInstance<ILog>(new LogHandler());
	}

	public static bool DeveloperConsoleVisible { get { return UnityEngine.Debug.developerConsoleVisible; } set { UnityEngine.Debug.developerConsoleVisible = value; } }
	public static bool IsDebugBuild { get { return UnityEngine.Debug.isDebugBuild; } }
	public static ILogger Logger { get { return UnityEngine.Debug.logger; } }

	[Conditional("UNITY_ASSERTIONS")]
	[Conditional("DEBUG")]
	public static void Assert(bool condition)
	{
		UnityEngine.Debug.Assert(condition);
	}

	[Conditional("UNITY_ASSERTIONS")]
	[Conditional("DEBUG")]
	public static void Assert(bool condition, string message)
	{
		UnityEngine.Debug.Assert(condition, message);
	}

	[Conditional("UNITY_ASSERTIONS")]
	[Conditional("DEBUG")]
	public static void Assert(bool condition, object message)
	{
		UnityEngine.Debug.Assert(condition, message);
	}

	[Conditional("UNITY_ASSERTIONS")]
	[Conditional("DEBUG")]
	public static void Assert(bool condition, UnityEngine.Object context)
	{
		UnityEngine.Debug.Assert(condition, context);
	}

	[Conditional("UNITY_ASSERTIONS")]
	[Conditional("DEBUG")]
	public static void Assert(bool condition, string message, UnityEngine.Object context)
	{
		UnityEngine.Debug.Assert(condition, message, context);
	}

	[Conditional("UNITY_ASSERTIONS")]
	[Conditional("DEBUG")]
	public static void Assert(bool condition, object message, UnityEngine.Object context)
	{
		UnityEngine.Debug.Assert(condition, message, context);
	}

	[Conditional("UNITY_ASSERTIONS")]
	[Conditional("DEBUG")]
	public static void AssertFormat(bool condition, string format, params object[] args)
	{
		UnityEngine.Debug.AssertFormat(condition, format, args);
	}

	[Conditional("UNITY_ASSERTIONS")]
	[Conditional("DEBUG")]
	public static void AssertFormat(bool condition, UnityEngine.Object context, string format, params object[] args)
	{
		UnityEngine.Debug.AssertFormat(condition, context, format, args);
	}

	[Conditional("UNITY_EDITOR")]
	[Conditional("DEBUG")]
	public static void Break()
	{
		UnityEngine.Debug.Break();
	}

	[Conditional("DEBUG")]
	public static void ClearDeveloperConsole()
	{
		UnityEngine.Debug.ClearDeveloperConsole();
	}

	[Conditional("DEBUG")]
	public static void DebugBreak()
	{
		UnityEngine.Debug.DebugBreak();
	}

	[ExcludeFromDocs]
	[Conditional("DEBUG")]
	public static void DrawLine(Vector3 start, Vector3 end)
	{
		UnityEngine.Debug.DrawLine(start, end);
	}

	[ExcludeFromDocs]
	[Conditional("DEBUG")]
	public static void DrawLine(Vector3 start, Vector3 end, Color color)
	{
		UnityEngine.Debug.DrawLine(start, end, color);
	}

	[ExcludeFromDocs]
	[Conditional("DEBUG")]
	public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
	{
		UnityEngine.Debug.DrawLine(start, end, color, duration);
	}

	[Conditional("DEBUG")]
	public static void DrawLine(Vector3 start, Vector3 end, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
	{
		UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);
	}

	[ExcludeFromDocs]
	[Conditional("DEBUG")]
	public static void DrawRay(Vector3 start, Vector3 dir)
	{
		UnityEngine.Debug.DrawRay(start, dir);
	}

	[ExcludeFromDocs]
	[Conditional("DEBUG")]
	public static void DrawRay(Vector3 start, Vector3 dir, Color color)
	{
		UnityEngine.Debug.DrawRay(start, dir, color);
	}

	[ExcludeFromDocs]
	[Conditional("DEBUG")]
	public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
	{
		UnityEngine.Debug.DrawRay(start, dir, color, duration);
	}

	[Conditional("DEBUG")]
	public static void DrawRay(Vector3 start, Vector3 dir, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
	{
		UnityEngine.Debug.DrawRay(start, dir, color, duration, depthTest);
	}

	[Conditional("DEBUG")]
	public static void Log(object message)
	{
		UnityEngine.Debug.Log(message);
	}

	[Conditional("DEBUG")]
	public static void Log(object message, UnityEngine.Object context)
	{
		UnityEngine.Debug.Log(message, context);
	}

	[Conditional("UNITY_ASSERTIONS")]
	[Conditional("DEBUG")]
	public static void LogAssertion(object message)
	{
		UnityEngine.Debug.LogAssertion(message);
	}

	[Conditional("UNITY_ASSERTIONS")]
	[Conditional("DEBUG")]
	public static void LogAssertion(object message, UnityEngine.Object context)
	{
		UnityEngine.Debug.LogAssertion(message, context);
	}

	[Conditional("UNITY_ASSERTIONS")]
	[Conditional("DEBUG")]
	public static void LogAssertionFormat(string format, params object[] args)
	{
		UnityEngine.Debug.LogAssertionFormat(format, args);
	}

	[Conditional("UNITY_ASSERTIONS")]
	[Conditional("DEBUG")]
	public static void LogAssertionFormat(UnityEngine.Object context, string format, params object[] args)
	{
		UnityEngine.Debug.LogAssertionFormat(context, format, args);
	}

	[Conditional("DEBUG")]
	public static void LogError(object message)
	{
		UnityEngine.Debug.LogError(message);
	}

	[Conditional("DEBUG")]
	public static void LogError(object message, UnityEngine.Object context)
	{
		UnityEngine.Debug.LogError(message, context);
	}

	[Conditional("DEBUG")]
	public static void LogErrorFormat(string format, params object[] args)
	{
		UnityEngine.Debug.LogErrorFormat(format, args);
	}

	[Conditional("DEBUG")]
	public static void LogErrorFormat(UnityEngine.Object context, string format, params object[] args)
	{
		UnityEngine.Debug.LogErrorFormat(context, format, args);
	}

	[Conditional("DEBUG")]
	public static void LogException(Exception exception)
	{
		UnityEngine.Debug.LogException(exception);
	}

	[Conditional("DEBUG")]
	public static void LogException(Exception exception, UnityEngine.Object context)
	{
		UnityEngine.Debug.LogException(exception, context);
	}

	[Conditional("DEBUG")]
	public static void LogFormat(string format, params object[] args)
	{
		UnityEngine.Debug.LogFormat(format, args);
	}

	[Conditional("DEBUG")]
	public static void LogFormat(UnityEngine.Object context, string format, params object[] args)
	{
		UnityEngine.Debug.LogFormat(context, format, args);
	}

	[Conditional("DEBUG")]
	public static void LogWarning(object message)
	{
		UnityEngine.Debug.LogWarning(message);
	}

	[Conditional("DEBUG")]
	public static void LogWarning(object message, UnityEngine.Object context)
	{
		UnityEngine.Debug.LogWarning(message, context);
	}

	[Conditional("DEBUG")]
	public static void LogWarningFormat(string format, params object[] args)
	{
		UnityEngine.Debug.LogWarningFormat(format, args);
	}

	[Conditional("DEBUG")]
	public static void LogWarningFormat(UnityEngine.Object context, string format, params object[] args)
	{
		UnityEngine.Debug.LogWarningFormat(context, format, args);
	}
}
