using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Kalman.Command
{
    /// <summary>
    /// �����а�����
    /// </summary>
    public class CmdHelper
    {
        /// <summary>
        /// ����cmd.exeִ��һ������
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static string Execute(string cmdText)
        {
            return Execute(new string[] { cmdText });
        }
        
        /// <summary>
        /// ����cmd.exeִ�ж�������
        /// </summary>
        /// <param name="cmdTexts"></param>
        /// <returns></returns>
        public static string Execute(string[] cmdTexts)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            string output = null;
            try
            {
                p.Start();

                foreach (string item in cmdTexts)
                {
                    p.StandardInput.WriteLine(item);
                }

                p.StandardInput.WriteLine("exit");
                output = p.StandardOutput.ReadToEnd();
                //strOutput = Encoding.UTF8.GetString(Encoding.Default.GetBytes(strOutput));
                p.WaitForExit();
                p.Close();
            }
            catch (Exception e)
            {
                output = e.Message;
            }

            return output;
        }

        /// <summary>
        /// �����ⲿWindowsӦ�ó������س������
        /// </summary>
        /// <param name="appName">Ӧ�ó�����</param>
        /// <returns></returns>
        public static bool RunApp(string appName)
        {
            return RunApp(appName, ProcessWindowStyle.Hidden);
        }
        
        /// <summary>
        /// �����ⲿӦ�ó���
        /// </summary>
        /// <param name="appName">Ӧ�ó�����</param>
        /// <param name="style">Ӧ�ó�������ʱ������ʾ��ʽ</param>
        /// <returns></returns>
        public static bool RunApp(string appName, ProcessWindowStyle style)
        {
            return RunApp(appName, null, style);
        }

        /// <summary>
        /// �����ⲿӦ�ó������س������
        /// </summary>
        /// <param name="appName">Ӧ�ó�����</param>
        /// <param name="args">����</param>
        /// <returns></returns>
        public static bool RunApp(string appName, string args)
        {
            return RunApp(appName, args, ProcessWindowStyle.Hidden);
        }
        
        /// <summary>
        /// �����ⲿӦ�ó���
        /// </summary>
        /// <param name="appName">Ӧ�ó�����</param>
        /// <param name="args">����</param>
        /// <param name="style">Ӧ�ó�������ʱ������ʾ��ʽ</param>
        /// <returns></returns>
        public static bool RunApp(string appName, string args, ProcessWindowStyle style)
        {
            bool flag = false;

            Process p = new Process();
            p.StartInfo.FileName = appName;//exe,bat and so on
            p.StartInfo.WindowStyle = style;
            p.StartInfo.Arguments = args;
            try
            {
                p.Start();
                p.WaitForExit();
                p.Close();
                flag = true;
            }
            catch
            {
            }
            return flag;
        }
    }
}
