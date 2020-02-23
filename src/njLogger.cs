/***********************************************************/
/* NJAGE Engine - Debugging Helpers                        */
/*                                                         */
/* Copyright 2020 Marcel Bulla. All rights reserved.       */
/* Licensed under the MIT License. See LICENSE in the      */
/* project root for license information.                   */
/***********************************************************/

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace De.Markellus.Njage.Debugging
{
    /// <summary>
    ///The class njLogger provides a global, thread-safe way to log events.
    /// </summary>
    public static class njLogger
    {
        private static njLogSeverity _severity;
        private static Mutex _mutex;
        
        static njLogger()
        {
            _severity = njLogSeverity.Info;
            _mutex = new Mutex();
            yfLog($"-----------------------------------------------------------------------------------------------------", njLogSeverity.System);
            yfLog($"---- NJAGE ENGINE - (C) 2020 Marcel Bulla ", njLogSeverity.System);
            yfLog($"----         Build: {Assembly.GetExecutingAssembly().FullName}", njLogSeverity.System);
            yfLog($"---- Graphics APIs: OpenGlL3+ | Direct3D11 | Vulkan", njLogSeverity.System);
            yfLog($"----   Shader APIs: SPIR-V / GLSL", njLogSeverity.System);
            yfLog($"-----------------------------------------------------------------------------------------------------", njLogSeverity.System);
        }

        public static void SetSeverity(njLogSeverity severity)
        {
            _severity = severity;
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void yfLog(string strMessage, njLogSeverity severity = njLogSeverity.Error)
        {
            if (_severity < severity)
            {
                return;
            }

            _mutex.WaitOne();

            Console.ForegroundColor = severity switch
            {
                njLogSeverity.Error => ConsoleColor.Red,
                njLogSeverity.Warning => ConsoleColor.DarkYellow,
                njLogSeverity.Info => ConsoleColor.White,
                njLogSeverity.System => ConsoleColor.Blue,
                _ => throw new ArgumentOutOfRangeException(nameof(severity), severity, null)
            };

            Console.WriteLine("{0} [{1, 7}]: {2}", DateTime.Now, severity, strMessage);

            _mutex.ReleaseMutex();
        }

        public static void njInfo(string strMessage)
        {
            yfLog(strMessage, njLogSeverity.Info);
        }

        public static void njWarn(string strMessage)
        {
            yfLog(strMessage, njLogSeverity.Warning);
        }

        public static void njError(string strMessage)
        {
            yfLog(strMessage, njLogSeverity.Error);
        }
    }
}