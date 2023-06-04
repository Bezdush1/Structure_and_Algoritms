using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const int n = 50000000;
        int[] arr = new int[n];
        const int m = 1000000;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int k = (int)numericUpDown1.Value;

            int i = 0;

            int StartTime = Environment.TickCount;
            {
                for (int j = 0; j < m; j++)
                {
                    int L = 0;
                    int R = n-1;
                    while (R >= L)
                    {
                        i = (L + R) / 2;
                        if (k == arr[i])
                        {
                            break;
                        }
                        if (k < arr[i])
                        {
                            R = i - 1;
                        }
                        else
                        {
                            L = i + 1;
                        }
                    }

                }
            }
            int ResultTime = Environment.TickCount - StartTime;
            textBox1.Text = ResultTime.ToString();
            if (arr[i] == k)
            {
                textBox2.Text = i.ToString();
            }
            else
            {
                textBox2.Text = "Не найдено";
            }
            //Оптимальный бинарный поиск
            int i2 = 0;
            int R1 = 0;
            int StartTime_out = Environment.TickCount;
            {
                for (int p = 0; p < m; p++)
                {
                    R1 = n-1;
                    int L1 = 0;

                    while (R1 > L1)
                    {
                        i2 = (L1 + R1) / 2;
                        if (k <= arr[i2])
                        {
                            R1 = i2;
                        }
                        else
                        {
                            L1 = i2 + 1;
                        }

                    }
                }

            }
            int ResultTime_out = Environment.TickCount - StartTime_out;
            textBox9.Text = ResultTime_out.ToString();
            if (arr[R1] == k)
            {
                textBox10.Text = R1.ToString();
            }
            else
            {
                textBox10.Text = "Не найдено";
            }

            //Интерполяционный поиск
            long i3 = 0;
            long R2 = 0;
            long L2 = 0;
            int StartTime_out_p = Environment.TickCount;
            {
                for (int f = 0; f < m; f++)
                {
                    R2 = n-1 ;
                    L2 = 0;
                    i3 = 0;
                    while ((arr[L2] < k) && (k < arr[R2]))
                    {
                        i3 = L2 + ((k - arr[L2]) * (R2 - L2)) / (arr[R2] - arr[L2]);
                        if (k == arr[i3])
                        {
                            break;
                        }
                        if (k < arr[i3])
                        {
                            R2 = i3 - 1;
                        }
                        else
                        {
                            L2 = i3 + 1;
                        }

                    }
                    if (k == arr[L2])
                    {
                        i3 = L2;
                    }
                    else
                    {
                        if (k == arr[R2])
                        {
                            i3 = R2;
                        }
                    }
                }

            }
            int ResultTime_out_p = Environment.TickCount - StartTime_out_p;
            textBox4.Text = ResultTime_out_p.ToString();
            if (arr[i3] == k)
            {
                textBox3.Text = i3.ToString();
            }
            else
            {
                textBox3.Text = "Не найдено";
            }


            // последовательный поиск в упоряд массиве
            int nm = arr[n - 1];
            arr[n - 1] = k + 1;
            int mm = 0;
            int StartTime_opt_sys = Environment.TickCount;
            {
                for (int i1 = 0; i1 < 100; i1++)
                {
                    mm = 0;
                    while (k > arr[mm])
                    {
                        mm++;
                    }
                }
            }
            int ResultTime_opt_sys = (Environment.TickCount - StartTime_opt_sys) * 1000;
            textBox8.Text = ResultTime_opt_sys.ToString();
            if (k != arr[mm])
            {
                textBox7.Text = "Не найдено";
            }
            else
            {
                textBox7.Text = mm.ToString();
            }
            arr[n - 1] = nm;
            // Последовательный бинарный поиск
            int P = 0;
            int B = 0;
            int StartTime_out_3 = Environment.TickCount;
            {
                for (int h = 0; h < m; h++)
                {
                   // P = 0;
                    B = n / 2;
                    while (B > 0)
                    {
                        while ((P + B < n) && (arr[P + B] <= k))
                        {
                            P = P + B;
                        }
                        B = B / 2;
                    }
                }
            }
            int ResultTime_out_3 = Environment.TickCount - StartTime_out_3;
            textBox6.Text = ResultTime_out_3.ToString();
            if (arr[P] == k)
            {
                textBox5.Text = P.ToString();
            }
            else
            {
                textBox5.Text = "Не найдено";
            }
        }

    



    private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            Random rnd = new Random();
            arr[0] = 105;
            for (int i = 1; i < n; i++)
            {
                arr[i] = arr[i - 1] + rnd.Next(1, 5);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

