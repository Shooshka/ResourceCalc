using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResourceCalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Плюёмся еррорами на RDP+Web
            if (comboBox1.SelectedIndex == 0)
            {
                if (numericUsers.Value > 60 || numericBasesGB.Value > 40 || numericMaxBaseGB.Value > 40)
                {
                    resultBox.Text = "Скорее всего автоматически такое посчитать не получится, обратитесь на vo за расчетом.";
                }
            }
            //Плюёмся еррорами на Web
            else
            {
                if (numericUsers.Value > 80 || numericBasesGB.Value > 20)
                {
                    resultBox.Text = "Скорее всего автоматически такое посчитать не получится, обратитесь на vo за расчетом.";
                }
            }
                  
            //Обычный файловый серв, RDP+Web, ДО 5 юзеров включительно И база ДО 4Гб включительно И суммарно баз ДО 40Гб включительно.
            if (comboBox1.SelectedIndex == 0 && numericUsers.Value < 6 && numericBasesGB.Value < 41 && numericMaxBaseGB.Value < 5)
            {
                resultBox.Text = $"Сажаем клиента на простой терминальный сервер с файловыми базами." +
                    $"{System.Environment.NewLine}{System.Environment.NewLine}" +
                    $"CPU: 2{System.Environment.NewLine}" +
                    $"RAM: {2 + numericUsers.Value}{System.Environment.NewLine}" +
                    $"Disk: 50Gb система + {Convert.ToDouble(numericBasesGB.Value) * 1.5}Gb под базы.";
            }
            //3-в-1, RDP+Web, ОТ 5 юзеров и ДО 15 включительно И база ДО 35Гб включительно И суммарно баз ДО 35Гб включительно.
            else if (comboBox1.SelectedIndex == 0 && 5 < numericUsers.Value && numericUsers.Value < 16 && numericBasesGB.Value < 36 && numericMaxBaseGB.Value < 36)
            {
                var cpu4users = (Convert.ToInt32(numericUsers.Value) % 7 == 0) ? Convert.ToInt32(numericUsers.Value) / 7 : (Convert.ToInt32(numericUsers.Value) / 7) + 1;
                var cpu4system = 1;
                var cpu41c = 1;
                var cpu4sql = 1 + cpu41c;
                var cpuTotal = cpu4users + cpu41c + cpu4sql + cpu4system;

                var ram4users = (Convert.ToInt32(numericUsers.Value) % 2 == 0) ? Convert.ToInt32(numericUsers.Value) / 2 : (Convert.ToInt32(numericUsers.Value) / 2) + 1;
                var ram4system = 2;
                var ram41c = 4;
                var ram4sql = numericBasesGB.Value / 2;
                var ramTotal = ram4users + ram4system + ram41c + ram4sql;

                var systemdisk = 60 + numericUsers.Value;
                var basesdisk = Convert.ToDouble(numericBasesGB.Value) * 1.5;

                resultBox.Text = $"Сажаем клиента на 3-в-1.{System.Environment.NewLine}{System.Environment.NewLine}" +
                    $"CPU: {cpuTotal}{System.Environment.NewLine}" +
                    $"RAM: {ramTotal}{System.Environment.NewLine}" +
                    $"Disk: {systemdisk}Gb система + {basesdisk}Gb под базы.";
            }
            //Структура Терм и 1с+скуль RDP+Web, ОТ 16 юзеров ДО 60 юзеров.
            else if (comboBox1.SelectedIndex == 0 && numericUsers.Value > 15)
            {
                if (numericBasesGB.Value > 35)
                {
                    resultBox.Text = "Скорее всего автоматически такое посчитать не получится, обратитесь на vo за расчетом.";
                }
                else
                {
                    var cpu4users = (Convert.ToInt32(numericUsers.Value) % 7 == 0) ? Convert.ToInt32(numericUsers.Value) / 7 : (Convert.ToInt32(numericUsers.Value) / 7) + 1;
                    var cpu4system = 1;

                    var cpu41c = (Convert.ToInt32(numericUsers.Value) % 20 == 0) ? Convert.ToInt32(numericUsers.Value) / 20 : (Convert.ToInt32(numericUsers.Value) / 20) + 1;
                    var cpu4sql = 1 + cpu41c;

                    var cpuTotalTerm = cpu4users + cpu4system;
                    var cpuTotal1c = cpu4system + cpu41c + cpu4sql;

                    var ram4users = (Convert.ToInt32(numericUsers.Value) % 2 == 0) ? Convert.ToInt32(numericUsers.Value) / 2 : (Convert.ToInt32(numericUsers.Value) / 2) + 1;
                    var ram4system = 2;

                    var ram41c = cpu41c * 4;
                    var ram4sql = numericBasesGB.Value / 2;

                    var ramTotalTerm = ram4users + ram4system;
                    var ramTotal1c = ram4system + ram41c + ram4sql;

                    var systemdiskTerm = 50 + numericUsers.Value;

                    var systemdisk1c = 60;
                    var basesdisk1c = Convert.ToDouble(numericBasesGB.Value) * 1.5;

                    resultBox.Text = $"Сажаем клиента на Терм и 1с+скуль.{System.Environment.NewLine}{System.Environment.NewLine}" +
                        $"Term CPU: {cpuTotalTerm}{System.Environment.NewLine}" +
                        $"Term RAM: {ramTotalTerm}{System.Environment.NewLine}" +
                        $"Term Disk: {systemdiskTerm}Gb система.{System.Environment.NewLine}{System.Environment.NewLine}" +
                        $"1c+sql CPU: {cpuTotal1c}{System.Environment.NewLine}" +
                        $"1c+sql RAM: {ramTotal1c}{System.Environment.NewLine}" +
                        $"1c+sql Disk: {systemdisk1c}Gb система + {basesdisk1c}Gb под базы.";
                }
                
            }
            //3-в-1, Web, ДО 80 юзеров И база ДО 20Гб включительно И суммарно баз ДО 20Гб включительно.
            else if (comboBox1.SelectedIndex == 1 && numericUsers.Value < 80 && numericBasesGB.Value < 25 && numericMaxBaseGB.Value < 25)
            {
                var cpu4users = 1;
                var cpu4system = 1;
                var cpu41c = (Convert.ToInt32(numericUsers.Value) % 20 == 0) ? Convert.ToInt32(numericUsers.Value) / 20 : (Convert.ToInt32(numericUsers.Value) / 20) + 1;
                var cpu4sql = 1 + cpu41c;
                var cpuTotal = cpu4users + cpu41c + cpu4sql + cpu4system;

                var ram4system = 2;
                var ram41c = cpu41c * 4;
                var ram4sql = numericBasesGB.Value / 2;
                var ramTotal = ram4system + ram41c + ram4sql;

                var systemdisk = 60;
                var basesdisk = Convert.ToDouble(numericBasesGB.Value) * 1.5;

                resultBox.Text = $"Сажаем клиента на 3-в-1.{System.Environment.NewLine}{System.Environment.NewLine}" +
                    $"CPU: {cpuTotal}{System.Environment.NewLine}" +
                    $"RAM: {ramTotal}{System.Environment.NewLine}" +
                    $"Disk: {systemdisk}Gb система + {basesdisk}Gb под базы.";
            }
        }
    }
}
