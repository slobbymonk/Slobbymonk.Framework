using UnityEditor;
using UnityEngine;
using System.IO;

namespace Framework.DebugMessages
{
    public static class Log
    {
        public enum LogLevel
        {
            Verbose,
            Info,
            Warning,
            Error,
            Critical
        }

        private static readonly string LoggingEnabledKey = "DebugMessagesManager.LoggingEnabled";
        private static readonly string LoggingLevelKey = "DebugMessagesManager.LoggingLevel";

        public static bool LoggingEnabled
        {
            get
            {
#if UNITY_EDITOR
                return EditorPrefs.GetBool(LoggingEnabledKey, true);
#endif
                return true; // Default to true if not in editor
            }
            set
            {
#if UNITY_EDITOR
                EditorPrefs.SetBool(LoggingEnabledKey, value);
#endif
            }
        }

        public static LogLevel CurrentLogLevel
        {
            get
            {
#if UNITY_EDITOR
                return (LogLevel)EditorPrefs.GetInt(LoggingLevelKey, (int)LogLevel.Info); // Default to Info
#endif
                return LogLevel.Verbose; // Default to Info if not in editor
            }
            set
            {
#if UNITY_EDITOR
                EditorPrefs.SetInt(LoggingLevelKey, (int)value);
#endif
            }
        }
#if UNITY_EDITOR

        #region ToolMenuItems
        [MenuItem("Tools/Debugger/Toggle Debug Logging")]
        public static void ToggleEnabled()
        {
            LoggingEnabled = !LoggingEnabled;
            Debug.Log("Debug logging toggled to: " + LoggingEnabled);
        }

        [MenuItem("Tools/Debugger/Test Log Messages")]
        public static void TestLogMessages()
        {
            LogVerbose("This is a verbose log message.");
            LogInfo("This is an info log message.");
            LogWarning("This is a warning log message.");
            LogError("This is an error log message.");
            LogCritical("This is a critical log message.");
        }

        [MenuItem("Tools/Debugger/Set Log Level/Verbose", priority = 0)]
        public static void SetLogLevelVerbose()
        {
            CurrentLogLevel = LogLevel.Verbose;
            Debug.Log("Log Level set to: Verbose");
        }

        [MenuItem("Tools/Debugger/Set Log Level/Info", priority = 1)]
        public static void SetLogLevelInfo()
        {
            CurrentLogLevel = LogLevel.Info;
            Debug.Log("Log Level set to: Info");
        }

        [MenuItem("Tools/Debugger/Set Log Level/Warning", priority = 2)]
        public static void SetLogLevelWarning()
        {
            CurrentLogLevel = LogLevel.Warning;
            Debug.Log("Log Level set to: Warning");
        }

        [MenuItem("Tools/Debugger/Set Log Level/Error", priority = 3)]
        public static void SetLogLevelError()
        {
            CurrentLogLevel = LogLevel.Error;
            Debug.Log("Log Level set to: Error");
        }

        [MenuItem("Tools/Debugger/Set Log Level/Critical", priority = 4)]
        public static void SetLogLevelCritical()
        {
            CurrentLogLevel = LogLevel.Critical;
            Debug.Log("Log Level set to: Critical");
        }
        #endregion
#endif
        private static bool ShouldLog(LogLevel level)
        {
            return LoggingEnabled && (int)level >= (int)CurrentLogLevel;
        }

        // How to access logs
        // C:\Users\[YourUsername]\AppData\LocalLow\Racoon Inc\Randy the Racoon\Logs

#if UNITY_STANDALONE || UNITY_ANDROID || UNITY_IOS
        public static void SaveLogToFile(string message)
        {
            if (Application.isEditor) return;

            try
            {
                string logDirectory = Path.Combine(Application.persistentDataPath, "Logs");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                string logFilePath = Path.Combine(logDirectory, "debug_log.txt");
                File.AppendAllText(logFilePath, message + "\n");
            }
            catch (IOException ex)
            {
                // Optionally log to a fallback system or ignore
                Debug.LogError("Failed to write log to file: " + ex.Message);
            }
        }
#endif
        private static string GetLogColor(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Verbose:
                    return "d8e3e3";
                case LogLevel.Info:
                    return "00f7ff";
                case LogLevel.Warning:
                    return "deed11";
                case LogLevel.Error:
                    return "FF0000";
                case LogLevel.Critical:
                    return "000000";
                default:
                    return "d3d4cb";
            }
        }

        #region LogMethods
        public static void LogMessage(MonoBehaviour callingBehaviour, string message, LogLevel level = LogLevel.Info, Color? textColour = null)
        {
            if (ShouldLog(level))
            {
                string colorHex = ColorUtility.ToHtmlStringRGB(textColour ?? Color.gray);

                // Combine the log message with the caller method
                string logMessage = $"<color=#{colorHex}>[{callingBehaviour.gameObject.name}] {message}</color>";

                Debug.Log(logMessage);

                SaveLogToFile($"{System.DateTime.Now}: [{callingBehaviour.gameObject.name}] {message}");
            }
        }
        public static void LogWarning(string message)
        {
            if (ShouldLog(LogLevel.Warning))
            {
                string color = GetLogColor(LogLevel.Warning);
                Debug.LogWarning($"<color=#{color}>[WARNING] {message}</color>");
                SaveLogToFile($"{System.DateTime.Now}: [WARNING] {message}");
            }
        }
        public static void LogError(string message)
        {
            if (ShouldLog(LogLevel.Error))
            {
                string color = GetLogColor(LogLevel.Error);
                Debug.LogError($"<color=#{color}>[ERROR] {message}</color>");
                SaveLogToFile($"{System.DateTime.Now}: [ERROR] {message}");
            }
        }
        public static void LogVerbose(string message)
        {
            if (ShouldLog(LogLevel.Verbose))
            {
                string color = GetLogColor(LogLevel.Verbose);
                Debug.Log($"<color=#{color}>[VERBOSE] {message}</color>");
                SaveLogToFile($"{System.DateTime.Now}: [VERBOSE] {message}");
            }
        }
        public static void LogInfo(string message)
        {
            if (ShouldLog(LogLevel.Info))
            {
                string color = GetLogColor(LogLevel.Info);
                Debug.Log($"<color=#{color}>[INFO] {message}</color>");
                SaveLogToFile($"{System.DateTime.Now}: [INFO] {message}");
            }
        }
        public static void LogCritical(string message)
        {
            if (ShouldLog(LogLevel.Critical))
            {
                string color = GetLogColor(LogLevel.Critical);
                Debug.LogError($"<color=#{color}>[CRITICAL] {message}</color>");
                SaveLogToFile($"{System.DateTime.Now}: [CRITICAL] {message}");
            }
        }
        #endregion
    }
}