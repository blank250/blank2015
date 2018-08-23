using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace blank.Log
{
    public class LogHelper
    {
        private static object _state = new object();
        private static string _Path = AppDomain.CurrentDomain.BaseDirectory;

        #region write log
        public static void WriteLog(Exception e)
        {
            WriteLog(e.Message);
        }

        public static void WriteLog(string m)
        {
            string path = _Path + @"load/log/" + string.Concat("log", DateTime.Now.ToString("yyyyMMdd"), ".txt");

            WriteLog(m, path);
        }

        public static void WriteLog(string m,string path)
        {
            _Write(m, path);
        }
        #endregion

        #region 私有
        private static void Write(string m)
        {
            string path = _Path + @"load\log\" + string.Concat("log", DateTime.Now.ToString("yyyyMMdd"), ".txt");

            _Write(m, path);
        }
        
        private static void _Write(string m, string path)
        {
            lock (_state)
            {
                if (!File.Exists(path))
                {
                   File.Create(path).Close();
                }

                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(string.Concat(m, " 时间:", DateTime.Now, ".", DateTime.Now.Millisecond));
                }
            }
        }
        #endregion
    }
}
