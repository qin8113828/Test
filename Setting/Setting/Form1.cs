using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace Setting
{
    public partial class Form1 : Form
    {
        private string language;
        private string imageType;
        string path = System.Environment.CurrentDirectory + "/Config.ini";

        public string Language
        {
            get => language;
            set
            {
                if (value == "0")
                {
                    language = "0";

                    radioButton1.Checked = true;
                }
                else
                {
                    language = "1";
                    radioButton2.Checked = true;
                }
            }
        }

        public string ImageType { get => imageType;
            set
            {
                    if (value == "0")
                    {
                        imageType = "0";
                        radioButton4.Checked = true;
                    }
                    else
                    {
                        imageType = "1";
                        radioButton3.Checked = true;
                    }
            }
        }

        //类的构造函数，传递
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
            //验证InI是否存在
            if (!File.Exists(path))
            {
                MessageBox.Show("Config.ini不存在");
                Application.Exit();
            }
            Language = INIParse.GetValue("Config", "Language", path);
            ImageType = INIParse.GetValue("Config", "ImageType", path);
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            Language = "0";
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            Language = "1";
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            ImageType = "0";
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            ImageType = "1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!INIParse.WriteValue("Config", "Language", Language, path))
            {
                MessageBox.Show("写入INI错误！");
            }
            if (!INIParse.WriteValue("Config", "ImageType",ImageType, path))
            {
                MessageBox.Show("写入INI错误！");
            }
            Application.Exit();
        }
    }
    public static class INIParse
    {
        [DllImport("kernel32")]
        private static extern bool WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, byte[] retVal, int size, string filePath);
        public static bool WriteValue (string section, string key, string val, string filePath)
        {
            return WritePrivateProfileString(section, key, val, filePath);
        }
        public static string GetValue(string section, string key,string path)
        {
            byte[] data = new byte[65535];
            int length= GetPrivateProfileString(section, key, "", data, data.Length, path);
            return Encoding.Default.GetString(data, 0, length);
        }
    }
}
