using System.Diagnostics;
using Task_5.Models;

namespace Task_5.Helpers
{
    public static class PerformanceHelper
    {
        private static readonly PerformanceCounter CpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private static readonly PerformanceCounter RamCounter = new PerformanceCounter("Process", "Working Set", "_Total");

        public static PerformanceMould GetMould(double coef = 1.0)
        {
            return new PerformanceMould
            {
                Cpu = CpuCounter.NextValue() * coef,
                Ram = RamCounter.NextValue() * coef
            };
        }
    }
}
