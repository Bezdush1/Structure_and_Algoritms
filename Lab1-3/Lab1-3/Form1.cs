using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_3
{
    public partial class Form1 : Form
    {
        const int n = 100000000;
        int[] array = new int[n + 1];
        int[] array_sort = new int[n + 1];


        public Form1()
        {
            InitializeComponent();
        }
        //генерация массива при загрузке
        private void Form1_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            for(int i = 0; i < n; i++)
            {
                array[i] = rnd.Next(0, n);
            }

            //для 3 лабораторной
            array_sort[0] = rnd.Next(0, 100);
            for (int i=1;i<n;i++)
            {
                array_sort[i] = array_sort[i - 1] + rnd.Next(0, 5);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
        //поиск ключа в массиве, а также отслеживание времени поиска
        private void button1_Click(object sender, EventArgs e)
        {
            int key = (int)numericUpDown1.Value;

            string index = "Поиск неудачен";

            int StartTime = Environment.TickCount;

            for(int i = 0; i < n; i++)
            {
                if (array[i] == key)
                {
                    index = i.ToString();
                    break;
                }
            }
            int ResultTime = Environment.TickCount - StartTime;

            textBox1.Text = ResultTime.ToString();
            textBox2.Text = index;

            //оптимальный поиск
            string index1 = "Поиск неудачен";
            array[n] = key;
            int j = 0;
            int StartTime1 = Environment.TickCount;
            {
                while (array[j] != key)
                    j++;
            }
            int ResultTime1 = Environment.TickCount - StartTime1;
            if (j != n)
                index1 = j.ToString();
            textBox5.Text = ResultTime1.ToString();
            textBox6.Text = index1;
        }
        //генерация рандомного ключа
        private void numericUpDown1_DoubleClick(object sender, EventArgs e)
        {
            Random rnd = new Random();
            numericUpDown1.Value = rnd.Next(0, 100000000);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //как в неупорядоченном
            int key1 = (int)numericUpDown2.Value;
            string index2 = "Поиск неудачен";
            array_sort[n] = key1;
            int j = 0;
            int StartTime2 = Environment.TickCount;
            {
                while (array_sort[j] != key1)
                    j++;
            }
            int ResultTime2 = Environment.TickCount - StartTime2;
            if (j != n)
                index2 = j.ToString();
            textBox3.Text = ResultTime2.ToString();
            textBox4.Text = index2;

            //как в упорядоченном
            array_sort[n] = key1+5;
            string index3 = "Поиск неудачен";
            int k = 0;
            int StartTime3 = Environment.TickCount;
            {
                while (key1 > array_sort[k])
                {
                    k++;
                }
            }
            int ResultTime3 = Environment.TickCount - StartTime3;
            if (array_sort[k] == key1)
                index3 = k.ToString();
            textBox7.Text = ResultTime3.ToString();
            textBox8.Text = index3;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void numericUpDown2_DoubleClick(object sender, EventArgs e)
        {
            Random rnd1 = new Random();
            numericUpDown2.Value = rnd1.Next(0, 100000000);
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
