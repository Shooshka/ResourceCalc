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
            numericBasesGB.Value = 1;
            numericBasesGB.Minimum = 1;
            numericMaxBaseGB.Value = 1;
            numericMaxBaseGB.Minimum = 1;
            numericUsers.Value = 1;
            numericUsers.Minimum = 1;

            resultBox.Text = $"Калькулятор предназначен для расчета под клиента с параметрами:{System.Environment.NewLine}" +
                $"Пользователи: 1-60(RDP+Web), 1-80(Web){System.Environment.NewLine}" +
                $"Максимальный размер 1 базы: 40(RDP+Web), 20(Web){System.Environment.NewLine}" +
                $"Максимальный суммарный размер баз: 40(RDP+Web), 20(Web){System.Environment.NewLine}" +
                $"За всем остальным обращайтесь на vo.";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Считаем RDP+Web
            if (comboBox1.SelectedIndex == 0)
            {
                //Считаем юзеров от 1 до 5
                if (numericUsers.Value > 0 && numericUsers.Value <= 5)
                {
                    //Считаем макс.базу <= 4Gb, суммарно <= 40Gb
                    if (numericMaxBaseGB.Value <= 4 && numericBasesGB.Value <= 40)
                    {
                        resultBox.Text = $"Сажаем клиента на простой терминальный сервер с файловыми базами." +
                        $"{System.Environment.NewLine}{System.Environment.NewLine}" +
                        $"CPU: 2{System.Environment.NewLine}" +
                        $"RAM: {2 + numericUsers.Value}{System.Environment.NewLine}" +
                        $"Disk: 50Gb система + {Convert.ToDouble(numericBasesGB.Value) * 1.5}Gb под базы.";
                    }
                    //Считаем макс.базу > 4Gb < 10, суммарно <= 40Gb
                    else if (numericMaxBaseGB.Value > 4 && numericMaxBaseGB.Value <= 10 && numericBasesGB.Value <= 40)
                    {
                        var cpu4users = (Convert.ToInt32(numericUsers.Value) % 7 == 0) ? Convert.ToInt32(numericUsers.Value) / 7 : (Convert.ToInt32(numericUsers.Value) / 7) + 1;
                        var cpu4system = 1;
                        var cpu41c = 1;
                        var cpu4sql = 1 + cpu41c;
                        var cpuTotal = cpu4users + cpu41c + cpu4sql + cpu4system;

                        var ram4users = (Convert.ToInt32(numericUsers.Value) % 2 == 0) ? Convert.ToInt32(numericUsers.Value) / 2 : (Convert.ToInt32(numericUsers.Value) / 2) + 1;
                        var ram4system = 2;
                        var ram41c = 4;
                        var ram4sql = Convert.ToInt32(numericBasesGB.Value) / 2;
                        var ramTotal = ram4users + ram4system + ram41c + Convert.ToInt32(ram4sql);

                        var systemdisk = 60 + numericUsers.Value;
                        var basesdisk = Convert.ToDouble(numericBasesGB.Value) * 1.5;

                        resultBox.Text = $"ВНИМАНИЕ!{System.Environment.NewLine}Если клиент очень хочет с такой большой базой в файл," +
                            $" то сажаем клиента на простой терминальный сервер с файловыми базами:" +
                        $"{System.Environment.NewLine}{System.Environment.NewLine}" +
                        $"CPU: 2{System.Environment.NewLine}" +
                        $"RAM: {2 + numericUsers.Value}{System.Environment.NewLine}" +
                        $"Disk: 50Gb система + {Convert.ToDouble(numericBasesGB.Value) * 1.5}Gb под базы." +
                        $"{System.Environment.NewLine}{System.Environment.NewLine}" +
                        $"Лучше предложить 3в1:{System.Environment.NewLine}{System.Environment.NewLine}" +
                        $"CPU: {cpuTotal}{System.Environment.NewLine}" +
                        $"RAM: {ramTotal}{System.Environment.NewLine}" +
                        $"Disk: {systemdisk}Gb система + {basesdisk}Gb под базы.";
                    }
                    //Считаем макс.базу >10, суммарно <= 40Gb
                    else if (numericMaxBaseGB.Value > 10 && numericBasesGB.Value <= 40)
                    {
                        var cpu4users = (Convert.ToInt32(numericUsers.Value) % 7 == 0) ? Convert.ToInt32(numericUsers.Value) / 7 : (Convert.ToInt32(numericUsers.Value) / 7) + 1;
                        var cpu4system = 1;
                        var cpu41c = 1;
                        var cpu4sql = 1 + cpu41c;
                        var cpuTotal = cpu4users + cpu41c + cpu4sql + cpu4system;

                        var ram4users = (Convert.ToInt32(numericUsers.Value) % 2 == 0) ? Convert.ToInt32(numericUsers.Value) / 2 : (Convert.ToInt32(numericUsers.Value) / 2) + 1;
                        var ram4system = 2;
                        var ram41c = 4;
                        var ram4sql = Convert.ToInt32(numericBasesGB.Value) / 2;
                        var ramTotal = ram4users + ram4system + ram41c + Convert.ToInt32(ram4sql);

                        var systemdisk = 60 + numericUsers.Value;
                        var basesdisk = Convert.ToDouble(numericBasesGB.Value) * 1.5;

                        resultBox.Text = $"Сажаем клиента на 3-в-1.{System.Environment.NewLine}{System.Environment.NewLine}" +
                        $"CPU: {cpuTotal}{System.Environment.NewLine}" +
                        $"RAM: {ramTotal}{System.Environment.NewLine}" +
                        $"Disk: {systemdisk}Gb система + {basesdisk}Gb под базы.";
                    }
                    //Этого не должно быть, но вдруг магия.
                    else
                    {
                        resultBox.Text = "Скорее всего автоматически такое посчитать не получится, обратитесь на vo за расчетом.";
                    }
                }
                //Считаем юзеров от 5 до 15
                else if (numericUsers.Value > 5 && numericUsers.Value <= 15)
                {
                    //Считаем макс.базу <= 35Gb, суммарно <= 35Gb
                    if (numericMaxBaseGB.Value <= 35 && numericBasesGB.Value <= 35)
                    {
                        var cpu4users = (Convert.ToInt32(numericUsers.Value) % 7 == 0) ? Convert.ToInt32(numericUsers.Value) / 7 : (Convert.ToInt32(numericUsers.Value) / 7) + 1;
                        var cpu4system = 1;
                        var cpu41c = 1;
                        var cpu4sql = 1 + cpu41c;
                        var cpuTotal = cpu4users + cpu41c + cpu4sql + cpu4system;

                        var ram4users = (Convert.ToInt32(numericUsers.Value) % 2 == 0) ? Convert.ToInt32(numericUsers.Value) / 2 : (Convert.ToInt32(numericUsers.Value) / 2) + 1;
                        var ram4system = 2;
                        var ram41c = 4;
                        var ram4sql = Convert.ToInt32(numericBasesGB.Value) / 2;
                        var ramTotal = ram4users + ram4system + ram41c + Convert.ToInt32(ram4sql);

                        var systemdisk = 60 + numericUsers.Value;
                        var basesdisk = Convert.ToDouble(numericBasesGB.Value) * 1.5;

                        resultBox.Text = $"Сажаем клиента на 3-в-1.{System.Environment.NewLine}{System.Environment.NewLine}" +
                        $"CPU: {cpuTotal}{System.Environment.NewLine}" +
                        $"RAM: {ramTotal}{System.Environment.NewLine}" +
                        $"Disk: {systemdisk}Gb система + {basesdisk}Gb под базы.";
                    }
                    //Этого не должно быть, но вдруг магия.
                    else
                    {
                        resultBox.Text = "Скорее всего автоматически такое посчитать не получится, обратитесь на vo за расчетом.";
                    }
                }
                //Считаем юзеров от 15 до 60
                else if (numericUsers.Value > 15 && numericUsers.Value <= 60)
                {
                    //Считаем макс.базу <= 35Gb, суммарно <= 35Gb
                    if (numericMaxBaseGB.Value <= 35 && numericBasesGB.Value <= 35)
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
                        var ram4sql = Convert.ToInt32(numericBasesGB.Value) / 2;

                        var ramTotalTerm = ram4users + ram4system;
                        var ramTotal1c = ram4system + ram41c + Convert.ToInt32(ram4sql);

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
                    //Этого не должно быть, но вдруг магия.
                    else
                    {
                        resultBox.Text = "Скорее всего автоматически такое посчитать не получится, обратитесь на vo за расчетом.";
                    }

                }
                //Этого не должно быть, но вдруг магия.
                else
                {
                    resultBox.Text = "Скорее всего автоматически такое посчитать не получится, обратитесь на vo за расчетом.";
                }
            }
            //Считаем Web
            else if (comboBox1.SelectedIndex == 1)
            {
                //Считаем юзеров от 1 до 80
                if (numericUsers.Value <= 80)
                {
                    //Считаем макс.базу <= 20, суммарно <= 20
                    if (numericBasesGB.Value < 20 && numericMaxBaseGB.Value < 20)
                    {
                        var cpu4users = 1;
                        var cpu4system = 1;
                        var cpu41c = (Convert.ToInt32(numericUsers.Value) % 20 == 0) ? Convert.ToInt32(numericUsers.Value) / 20 : (Convert.ToInt32(numericUsers.Value) / 20) + 1;
                        var cpu4sql = 1 + cpu41c;
                        var cpuTotal = cpu4users + cpu41c + cpu4sql + cpu4system;

                        var ram4system = 2;
                        var ram41c = cpu41c * 4;
                        var ram4sql = Convert.ToInt32(numericBasesGB.Value) / 2;
                        var ramTotal = ram4system + ram41c + Convert.ToInt32(ram4sql);

                        var systemdisk = 60;
                        var basesdisk = Convert.ToDouble(numericBasesGB.Value) * 1.5;

                        resultBox.Text = $"Сажаем клиента на 3-в-1.{System.Environment.NewLine}{System.Environment.NewLine}" +
                            $"CPU: {cpuTotal}{System.Environment.NewLine}" +
                            $"RAM: {ramTotal}{System.Environment.NewLine}" +
                            $"Disk: {systemdisk}Gb система + {basesdisk}Gb под базы.";
                    }
                    //Этого не должно быть, но вдруг магия.
                    else
                    {
                        resultBox.Text = "Скорее всего автоматически такое посчитать не получится, обратитесь на vo за расчетом.";
                    }
                }
                //Этого не должно быть, но вдруг магия.
                else
                {
                    resultBox.Text = "Скорее всего автоматически такое посчитать не получится, обратитесь на vo за расчетом.";
                }
            }
            else
            {
                //Этого не должно быть, но вдруг магия.
                resultBox.Text = "Скорее всего автоматически такое посчитать не получится, обратитесь на vo за расчетом.";
            }
        }

        private void numericUsers_Enter(object sender, EventArgs e)
        {
            numericUsers.Select(0, numericUsers.Text.Length);
        }

        private void numericBasesGB_Enter(object sender, EventArgs e)
        {
            numericBasesGB.Select(0, numericBasesGB.Text.Length);
        }

        private void numericMaxBaseGB_Enter(object sender, EventArgs e)
        {
            numericMaxBaseGB.Select(0, numericMaxBaseGB.Text.Length);
        }
    }
}
