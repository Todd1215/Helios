﻿using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Threading;

namespace GadrocsWorkshop.Helios.Windows
{
    public class ExceptionViewer
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static void DisplayUnhandledException(DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Error(e.Exception, $"Unhandled exception occurred. {Assembly.GetExecutingAssembly()?.GetName()?.Name ?? "Application"} will exit.");
            DisplayException(e.Exception);
            
            // prepare for exit
            HeliosInit.OnShutdown();
        }

        public static void DisplayException(Exception ex)
        {
            string message = ex.Message;
            Regex buildPathExpression = new Regex("[A-Z]:.*\\\\Helios(Dev)?\\\\");
            string trace = ex.StackTrace;
            Match buildPathMatch = buildPathExpression.Match(trace);
            if (buildPathMatch.Success)
            {
                message += "\n" + trace.Replace(buildPathMatch.Groups[0].Value, "");
            }
            else
            {
                message += "\n" + trace;
            }

            // XXX try to use a custom dialog that supports selecting text and maybe share dialog

            System.Windows.MessageBox.Show(
                $"Unhandled exception occurred.  Please file a bug:\n{message}",
                $"Unhandled Error in {ex.Source}");
        }
    }
}