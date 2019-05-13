using System;
using System.Runtime.InteropServices;

namespace KOR.SystemInfo.Helpers
{
    public static class CommonHelpers
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public enum SizeUnits
        {
            Byte, KB, MB, GB, TB, PB, EB, ZB, YB
        }

        public static bool IsDllLoaded(string path)
        {
            return GetModuleHandle(path) != IntPtr.Zero;
        }

        public static string ToSize(this long value, SizeUnits unit)
        {
            return (value / Math.Pow(1024, (long)unit)).ToString("0.00");
        }
    }
}
