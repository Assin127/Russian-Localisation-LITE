using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;


namespace EU4_Russian_Localisation_LITE_Check_for_Updates
{
    public partial class Update : Form
    {
        WebClient client = new WebClient();
        public Update()
        {
            InitializeComponent();
        }

        private void StartGame()
        {
            try
            {
                System.Diagnostics.Process.Start(@"dowser.exe");
            }
            catch
            {
                MessageBox.Show("Лаунчер игры не найден\nПроверьте, находится ли программа в одной папке с лаунчером, либо переустановите русификатор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Application.Exit();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string localversion = "", webversion = "";
            try
            {
                localversion = client.DownloadString(@"version.txt");
            }
            catch
            {
                MessageBox.Show("Информация о текущей версии русификатора не найдена\nПереустановите русификатор", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StartGame();
            }
            finally
            {
                try
                {
                    webversion = client.DownloadString("https://pastebin.com/smD98BqZ");
                }
                catch
                {
                    MessageBox.Show("Нет доступа к серверу для проверки версии русификатора\nПроверьте подключение к интернету", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StartGame();
                }
                finally
                {
                    if (webversion.Contains(localversion))
                    {
                        StartGame();
                    }
                    else
                    {
                        MessageBox.Show("Доступна новая версия GEKS EU4 Russian Localisation LITE!\n\nСкачать обновление можно по ссылке: vk.com/geks_org", "Доступно обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        StartGame();
                    }
                }
            }
        }
    }
}
