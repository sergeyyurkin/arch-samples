using System;

namespace Ordering.Migrator
{
    public class Log
    {
        public void Write(string text)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " | " + text);
        }
    }
}
