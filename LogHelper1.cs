using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using blank.Tools;

namespace blank.Log
{
    public class LogHelper
    {
        private static object _state = new object();
        private static string _Path = AppDomain.CurrentDomain.BaseDirectory.ToString();
        private static string _sm = "模块名:{0} 命名空间:{1} 类名:{2} 方法名:{3}";

        #region write log
        public static void WriteLog(string m)
        {
            WriteLog("", m);
        }

        public static void WriteLog(string s, string m)
        {
            WriteLog(s, m, null);
        }

        public static void WriteLog(string s, string m, object[] param)
        { 
           string u = CookiesHelper.GetCookieValue("LoginUserCode");

           WriteLog(s, m, u, param);
        }

        public static void WriteLog(string s, Exception e, object[] p)
        {
            string m = string.Concat(e.Message,e.InnerException);

            WriteLog(s, m, p);
        }

        public static void WriteLog(Exception e)
        {
            WriteLog("", e, null);
        }

        public static void WriteLog(string s, string m, string u, object[] param)
        {
            string p = (param==null) ? "" : GetParams(param) ;
                
            string _m = string.Format("用户{0}服务{1}参数{2}消息{3}",u,s,p, m);

            Write(_m);
        }

        public static void Write(string m)
        {
            string path = _Path + @"load/log/" + string.Concat("log", DateTime.Now.ToString("yyyyMMdd"), ".txt");

            _Write(m, path);
        }
        #endregion

        #region 私有
        private static string GetParams(object[] param)
        {
            string _params = "";
            foreach (object o in param)
            {
                if (o != null)
                {
                    _params += Convert.ToString(o);
                }
            }
            return _params;
        }

        private static void _Write(string m, string path)
        {
            lock (_state)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(string.Concat(m, " 时间:", DateTime.Now, ".", DateTime.Now.Millisecond));
                }
            }
        }
        #endregion

        #region ____方法
        private static string GetMethodInfo()
        {
            MethodBase mb = MethodBase.GetCurrentMethod();
            //string sm = Environment.NewLine;
            //sm += "模块名:" + mb.Module.ToString() + Environment.NewLine;
            //sm += "命名空间名:" + mb.ReflectedType.Namespace + Environment.NewLine;
            //完全限定名，包括命名空间
            //sm += "类名:" + mb.ReflectedType.FullName + Environment.NewLine;
            //sm += "方法名:" + mb.Name;

            return string.Format(_sm, mb.Module, mb.ReflectedType.Namespace, mb.ReflectedType.FullName,mb.Name);
        }
        #endregion
    }
}
