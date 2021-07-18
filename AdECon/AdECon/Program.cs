using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;

namespace AdECon
{
    static class Program
    {
        #region Telegram
        private static readonly TelegramBotClient bot = new("1703124081:AAEml8NCLK0YRCKVUien6k8sFLBnhHr_wBk");
        #endregion


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // bot.SendTextMessageAsync(bot.BotId, "Vem pegar seu Sedex");
            
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}