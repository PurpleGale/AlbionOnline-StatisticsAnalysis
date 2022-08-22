﻿using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Threading;
using StatisticsAnalysisTool.Avalonia.Enumerations;
using StatisticsAnalysisTool.Avalonia.Models;

namespace StatisticsAnalysisTool.Avalonia.Common
{
    public static class ConsoleManager
    {
        private const int MaxEntries = 1000;
        public static ObservableCollectionEx<ConsoleFragment> Console = new();

        private static bool _isConsoleActive;

        public static void Start()
        {
            _isConsoleActive = true;
        }

        public static void Stop()
        {
            _isConsoleActive = false;
        }

        public static void Reset()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                lock (Console)
                {
                    Console.Clear();
                }
            });
        }

        public static void WriteLine(ConsoleFragment consoleFragment)
        {
            if (!_isConsoleActive)
            {
                return;
            }

            Dispatcher.UIThread.InvokeAsync(() =>
            {
                lock (Console)
                {
                    Console.Add(consoleFragment);

                    if (Console.Count <= MaxEntries)
                    {
                        return;
                    }

                    var firstItem = Console.FirstOrDefault();
                    if (firstItem != null)
                    {
                        Console.Remove(firstItem);
                    }
                }
            });
        }

        public static void WriteLineForNetworkHandler(string name, Dictionary<byte, object> parameters)
        {
            WriteLine(new ConsoleFragment(name, parameters, ConsoleColorType.EventColor));
        }

        public static void WriteLineForWarning(Type declaringType, Exception e)
        {
            WriteLine(new ConsoleFragment(declaringType.ToString(), e.Message, ConsoleColorType.WarnColor));
        }

        public static void WriteLineForError(Type? declaringType, Exception e)
        {
            WriteLine(new ConsoleFragment(declaringType?.ToString() ?? string.Empty, e.Message, ConsoleColorType.ErrorColor));
            WriteLine(new ConsoleFragment(string.Empty, e.StackTrace ?? string.Empty, ConsoleColorType.ErrorColor));
        }

        public static void WriteLineForMessage(Type declaringType, string message, ConsoleColorType consoleColorType = ConsoleColorType.Default)
        {
            WriteLine(new ConsoleFragment(declaringType.ToString(), message, consoleColorType));
        }

        public static void WriteLineForMessage(string name, Dictionary<byte, object> parameters, ConsoleColorType consoleColorType = ConsoleColorType.Default)
        {
            WriteLine(new ConsoleFragment(name, parameters, consoleColorType));
        }
    }
}