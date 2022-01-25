using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Off_Windows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int intHours;
        public int intMinutes;
        int Time;

        public static string Hours;
        public static string Minutes;

        public void Act(string a)
        {
            Hours = textBox4.Text.Trim();
            Minutes = textBox5.Text.Trim();

            if (Hours == "") Hours = "0";
            if (Minutes == "") Minutes = "0";

            if (!int.TryParse(Hours, out intHours) || !int.TryParse(Minutes, out intMinutes))
            {
                MessageBox.Show("Неправильно введенное время.\nНеобходимо ввести число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (intHours < 0 || intMinutes < 0)
            {
                MessageBox.Show("Неправильно введенное время.\nНеобходимо ввести положительные числа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (radioButton3.Checked)
                {
                    Time = intHours * 3600 + intMinutes * 60;
                    if (Time != 0) Process.Start("shutdown", "/" + a + " /t " + Time);
                    else
                    {
                        var res = MessageBox.Show("Вы точно хотите выключить компьютер прямо сейчас?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (res == DialogResult.Yes) Process.Start("shutdown", "/" + a + " /t " + Time);
                    }
                }
                else if (radioButton4.Checked)
                {
                    int HH = int.Parse(DateTime.Now.ToString("HH"));
                    int mm = int.Parse(DateTime.Now.ToString("mm"));
                    int ss = int.Parse(DateTime.Now.ToString("ss"));
                    int DT = HH * 3600 + mm * 60;
                    if (intHours > 23 || intMinutes > 59)
                    {
                        MessageBox.Show("Неправильно введенное время.\nТаймер можно поставить не больше суток (не больше 23:59).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        int TimeView = intHours * 3600 + intMinutes * 60;

                        if (TimeView < DT)
                        {
                            Time = 86400 - (DT - TimeView) - ss;
                        }
                        else if (TimeView > DT)
                        {
                            Time = TimeView - DT - ss;
                        }
                        else Time = 86400 - ss;

                        Process.Start("shutdown", "/" + a + " /t " + Time);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Act("s");
            }

            else if (radioButton2.Checked)
            {
                Act("r");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("shutdown", "/a");
        }
    }
}
