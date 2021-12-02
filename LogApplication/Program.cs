using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace LogApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log/");
            LogServiceHelper.Instance.Start(dir);
            LogWriteTest();
        }

        private static void LogWriteTest()
        {
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 5; i++)
            {
                var _thread = new Thread(Start);
                _thread.IsBackground = true;
                _thread.Start(i);
            }

            while (LogServiceHelper.Instance.GetQueueCount() > 0)
            {
            }

            sw.Stop();
            LogServiceHelper.Instance.Stop();
            var s = sw.ElapsedMilliseconds / 1000;
            Console.WriteLine("共耗时：" + s + "秒");
            Console.ReadLine();
        }

        private static void Start(object tag)
        {
            for (var i = 0; i < 200000; i++) LogServiceHelper.Instance.Write("测试" + tag, i + "测试测试测试测试测试", "sa");
        }
    }
}