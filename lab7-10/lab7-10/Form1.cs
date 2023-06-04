using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab7_10
{
    public partial class Form1 : Form
    {
        const int key = 45678;
        int divider = 997;
        int adress = 999;
        int maxPrime;
        const int n = 1000;
        const int n1 = 10000;
        int[] arr = new int[n];
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private int HashDiv(int key, int divider) //метод деления
        {
            return key % divider;
        }
        private int HashMiddleSquare(int key, int adress) //метод середины квадрата
        {
            ulong Key2 = ((ulong)Math.Pow(key, 2)); //возвел в квадрат
            int sizeFull = (int)Math.Log10(Key2 + 1); //полная длина числа
            int sizeFullAdreslen = (int)Math.Log10(adress + 1);
            if (sizeFull % 2 == 1) // если нечётная длина
            {
                return (int)(Key2 % (Math.Pow(10, sizeFull - sizeFullAdreslen)) / Math.Pow(10, sizeFullAdreslen)); // слева отсекаем больше цифр

            }
            else //если чётная длина
            {
                return (int)(Key2 % (Math.Pow(10, sizeFull - sizeFullAdreslen)) / Math.Pow(10, sizeFullAdreslen + 1));
            }
        }

        private int Hashfolding(int key, int adress)     ///метод свертывания
        {
            int Index = 0;
            int adressDigits = (int)Math.Pow(10, Math.Ceiling(Math.Log10(adress)));
            while (key > 0)
            {
                Index += key % adressDigits;
                key /= adressDigits;
            }
            return Index % adressDigits;
        }
        private static int Hashmultiplication(int key, int adress)    //метод умножения
        {
            double A = (Math.Sqrt(5) - 1) / 2;
            int buf = (int)(key * A);
            return (int)(adress * (key * A - buf));
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void button3_Click_1(object sender, EventArgs e)
        {
            //textBox1.Text = HashDiv(key, divider).ToString();
            /////
            //textBox2.Text = HashMiddleSquare(key, adress).ToString();
            ////
            //textBox4.Text = Hashfolding(key, adress).ToString();
            ////
            //textBox3.Text = Hashmultiplication(key, adress).ToString();
            int iterationCount = (int)numericUpDown1.Value;

            int t1 = 0, t2 = 0, t3 = 0, t4 = 0;
           

            for (int j = 0; j < iterationCount; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    arr[i] = rnd.Next(0, 100000);
                }
                

                List<int>[] masDivision = new List<int>[n];
                List<int>[] masSquare = new List<int>[n];
                List<int>[] masfolding = new List<int>[n];
                List<int>[] masMultiplication = new List<int>[n];

                int k1 = 0, k2 = 0, k3 = 0, k4 = 0;

                for (int i = 0; i < n; i++)
                {
                    int index = HashDiv(arr[i],divider);
                    if (masDivision[index] != null)
                    {
                        k1++;
                    }
                    else
                    {
                        masDivision[index] = new List<int>();

                    }
                    masDivision[index].Add(arr[i]);
                }

                for (int i = 0; i < n; i++)
                {
                    int index = HashMiddleSquare(arr[i], n);
                    if (masSquare[index] != null)
                    {
                        k2++;
                    }
                    else
                    {
                        masSquare[index] = new List<int>();
                    }
                    masSquare[index].Add(arr[i]);
                }

                for (int i = 0; i < n; i++)
                {
                    int index = Hashfolding(arr[i], n);
                    if (masfolding[index] != null)
                    {
                        k3++;
                    }
                    else
                    {
                        masfolding[index] = new List<int>();
                    }
                    masfolding[index].Add(arr[i]);
                }

                for (int i = 0; i < n; i++)
                {
                    int index = Hashmultiplication(arr[i], n);
                    if (masMultiplication[index] != null)
                    {
                        k4++;
                    }
                    else
                    {
                        masMultiplication[index] = new List<int>();
                    }
                    masMultiplication[index].Add(arr[i]);
                }

                if (k1 < k2 && k1 < k3 && k1 < k4)
                {
                    t1++;
                }
                else if (k2 < k3 && k2 < k4)
                {
                    t2++;
                }
                else if (k3 < k4)
                {
                    t3++;
                }
                else
                {
                    t4++;
                }
            }

            textBox1.Text = t1.ToString();
            textBox2.Text = t2.ToString();
            textBox4.Text = t3.ToString();
            textBox3.Text = t4.ToString();

            
        }

        private static void ChainMethodFill(int[] arrToHashFrom, List<int>[] arrToHashTo)
        {
            for (int i = 0; i < n1; i++)
            {
                int index = Hashmultiplication(arrToHashFrom[i], n1);
                if (arrToHashTo[index] == null)
                {
                    arrToHashTo[index] = new List<int>();
                    arrToHashTo[index].Add(arrToHashFrom[i]);
                }
                else
                {
                    arrToHashTo[index].Add(arrToHashFrom[i]);
                }
            }
        }

        private void ChainMethodSearch(int[] arrToSearchFrom, List<int>[] arrToSearchIn)
        {
            int founded = 0;
            int startTime = Environment.TickCount;
            int sum = 0;
            int count = 0;
            for (int i = 0; i < n1; i++)
            {
                founded += ChainMethodFind(arrToSearchFrom[i], arrToSearchIn, ref sum);
                count++;
            }
            int endTime = Environment.TickCount;

            textBox10.Text = (endTime - startTime).ToString();
            textBox9.Text = ((double)sum / count).ToString();
            textBox8.Text = founded.ToString();
        }

        private static int ChainMethodFind(int key, List<int>[] arrToSearchIn, ref int comparisonSum)
        {
            int index = Hashmultiplication(key, n1);
            int count = 1;
            if (arrToSearchIn[index] == null)
            {
                comparisonSum += count;
                return 0;
            }
            for (int i = 0; i < arrToSearchIn[index].Count; i++)
            {
                count++;
                if (arrToSearchIn[index][i] == key)
                {
                    comparisonSum += count;
                    return 1;
                }
            }
            comparisonSum += count;
            return 0;
        }

        private void OpenAddressMethodSearch(int[] arrToSearchFrom, int[] arrToSearchIn)
        {
            int founded = 0;
            int startTime = Environment.TickCount;
            int sum = 0;
            int count = 0;
            for (int i = 0; i < n1; i++)
            {
                founded += OpenAddressMethodFind(arrToSearchFrom[i], arrToSearchIn, ref sum);
                count++;
            }
            int endTime = Environment.TickCount;

            textBox5.Text = (endTime - startTime).ToString();
            textBox6.Text = (sum / count).ToString();
            textBox7.Text = founded.ToString();
        }

        private static int OpenAddressMethodFind(int key, int[] arrToSearchIn, ref int comparisonSum)
        {
            int index = Hashmultiplication(key, n1);
            int count = 1;
            if (arrToSearchIn[index] == key)
            {
                return 1;
            }
            else
            {
                for (int j = index; j < n1; j++)
                {
                    count++;
                    if (arrToSearchIn[j] == key)
                    {
                        comparisonSum += count;
                        return 1;
                    }
                }
                for (int j = 0; j < index; j++)
                {
                    count++;
                    if (arrToSearchIn[j] == key)
                    {
                        comparisonSum += count;
                        return 1;
                    }
                }
                comparisonSum += count;
                return 0;
            }
        }

        private static void OpenAddressMethodFill(int[] arrToHashFrom, int[] arrToHashTo)
        {
            for (int i = 0; i < n1; i++)
            {
                if (OpenAddressMethodAdd(arrToHashFrom[i], arrToHashTo) == 0)
                {
                    return;
                }
            }
        }

        private static int OpenAddressMethodAdd(int key, int[] arrToHashTo)
        {
            int index = Hashmultiplication(key, n1);
            if (arrToHashTo[index] < 0)
            {
                arrToHashTo[index] = key;
                return 1;
            }
            else
            {
                for (int j = index; j < n1; j++)
                {
                    if (arrToHashTo[j] < 0)
                    {
                        arrToHashTo[j] = key;
                        return 1;
                    }
                }
                for (int j = 0; j < index; j++)
                {
                    if (arrToHashTo[j] < 0)
                    {
                        arrToHashTo[j] = key;
                        return 1;
                    }
                }
                return 0;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] M1 = new int[n1];
            for (int i = 0; i < n1; i++)
            {
                M1[i] = rnd.Next(0, 10000);
            }

            int[] MOA = new int[n1];
            for (int i = 0; i < n1; i++)
            {
                MOA[i] = -1;
            }

            List<int>[] MC = new List<int>[n1];

            ChainMethodFill(M1, MC);
            OpenAddressMethodFill(M1, MOA);

            int[] M2 = new int[n1];
            for (int i = 0; i < n1; i++)
            {
                M2[i] = rnd.Next(0, 20000);
            }

            OpenAddressMethodSearch(M2, MOA);
            ChainMethodSearch(M2, MC);
        }
    }
}
        
