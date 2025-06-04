using UniversalFileExtractor;
using System.Windows.Forms;

namespace 万能二进制提取器
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // 这里假设主窗体类名为Form1，需根据实际情况调整
            Application.Run(new FileExtractor());
        }
    }
}