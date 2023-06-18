using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OOP30
{
    public partial class Form1 : Form
    {
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();
        public Form1()
        {
            InitializeComponent();
            label1.Text = "Хост";
            label2.Text = "Ім'я користувача";
            label3.Text = "Пароль";
            label4.Text = "Файл";
            label5.Text = "Каталог";
            label6.Text = "Каталог";
            label7.Text = "файл";
            label8.Text = "Каталог";
            label9.Text = "Шлях";
            button1.Text= "Підключитися та отримати список файлів та каталогів";
            button2.Text = "Розмір файлу";
            button3.Text = "Створити каталог";
            button4.Text = "Видалити каталог";
            button5.Text = "Видалити файл";
            button6.Text = "Завантаження на FTP";
            IPAddress[] localIP = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress address in localIP)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    tbHost.Text = "ftp://" + address.ToString();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string host = tbHost.Text.Trim();

            if (string.IsNullOrEmpty(host))
            {
                MessageBox.Show("Будь ласка, введіть дійсну адресу FTP-хоста.");
                return;
            }
            FadList.Items.Clear();

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(tbHost.Text);
            request.Credentials = new NetworkCredential(tbUser.Text, tbPass.Text);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);


            while (!reader.EndOfStream)
            {
                FadList.Items.Add(reader.ReadLine());
            }
            MessageBox.Show(response.WelcomeMessage);
            reader.Close();
            response.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(tbHost.Text +
tbNewDir.Text);
            request.Credentials = new NetworkCredential(tbUser.Text, tbPass.Text);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            MessageBox.Show("Каталог " + tbNewDir.Text + "створено");

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Всі файли|*.*|png files|*.png|jpg files|*.jpg|gif files|*.gif|bmp files|*.bmp|exe files|*.exe|rar files|*.rar|zip files|*.zip|txt files|*.txt";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                tbUpload.Text = openFileDialog1.FileName;
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(tbHost.Text + tbUpload.Text + openFileDialog1.SafeFileName);
                request.Credentials = new NetworkCredential(tbUser.Text, tbPass.Text);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                byte[] file = System.IO.File.ReadAllBytes(tbUpload.Text);
                Stream strz = request.GetRequestStream();
                strz.Write(file, 0, file.Length);
                strz.Close();
                strz.Dispose();

                MessageBox.Show(openFileDialog1.SafeFileName + " завантажено");
            }
            else
            {
                MessageBox.Show(openFileDialog1.SafeFileName + " не завантажено");
            }

        }
    }
}
