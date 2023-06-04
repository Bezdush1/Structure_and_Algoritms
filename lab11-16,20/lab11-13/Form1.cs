using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab11_13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.RowCount = 8;     // число строк
            dataGridView1.ColumnCount = 6;  // число столбцов
            dataGridView1.Rows[0].Cells[1].Value = "Обмен";
            dataGridView1.Rows[1].Cells[1].Value = "Выбор";
            dataGridView1.Rows[2].Cells[1].Value = "Включение";
            dataGridView1.Rows[3].Cells[1].Value = "быстрая";
            dataGridView1.Rows[4].Cells[1].Value = "Шелл";
            dataGridView1.Rows[5].Cells[1].Value = "Линейная";
            dataGridView1.Rows[6].Cells[1].Value = "Встроенная";
            dataGridView1.Rows[7].Cells[1].Value = "Пирамидальная";

            dataGridView1.Rows[0].Cells[0].Value = false;
            dataGridView1.Rows[1].Cells[0].Value = false;
            dataGridView1.Rows[2].Cells[0].Value = false;
            dataGridView1.Rows[3].Cells[0].Value = false;
            dataGridView1.Rows[4].Cells[0].Value = false;
            dataGridView1.Rows[5].Cells[0].Value = false;
            dataGridView1.Rows[6].Cells[0].Value = false;
            dataGridView1.Rows[7].Cells[0].Value = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int size = (int)numericUpDown1.Value;
            int[] basic = new int[size];
            int[] arr = new int[size];
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
                basic[i] = rnd.Next(1, size);
            int r = 0;//сравнения
            int k = 0;//перестановки
            //обмен
            if ((bool)dataGridView1.Rows[0].Cells[0].Value)
            {
                basic.CopyTo(arr, 0);
                int temp;
                int StartTime = Environment.TickCount;
                for (int i = 0; i < size; i++)
                {
                    for (int j = i + 1; j < size; j++)
                    {
                        if (arr[i] > arr[j])
                        {
                            temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                            k++;
                        }
                        r++;
                    }

                }
                int ResultTime = Environment.TickCount - StartTime;
                dataGridView1.Rows[0].Cells[4].Value = ResultTime.ToString();
                dataGridView1.Rows[0].Cells[3].Value = k;
                dataGridView1.Rows[0].Cells[2].Value = r;
                if (IsSorted(arr, size)) dataGridView1.Rows[0].Cells[5].Value = true;
                else dataGridView1.Rows[0].Cells[5].Value = false;
                Array.Clear(arr, 0, size);


            }
            //выбор
            if ((bool)dataGridView1.Rows[1].Cells[0].Value)
            {
                basic.CopyTo(arr, 0);
                k = 0;
                r = 0;
                int StartTime2 = Environment.TickCount;
                for (int i = 0; i < size - 1; i++)
                {
                    //поиск минимального числа
                    int min = i;
                    for (int j = i + 1; j < size; j++)
                    {
                        if (arr[j] < arr[min])
                        {
                            min = j;
                        }
                        r++;
                    }
                    //обмен элементов
                    int tmp = arr[min];
                    arr[min] = arr[i];
                    arr[i] = tmp;
                    k++;
                }
                int ResultTime2 = Environment.TickCount - StartTime2;
                dataGridView1.Rows[1].Cells[4].Value = ResultTime2.ToString();
                dataGridView1.Rows[1].Cells[3].Value = k;
                dataGridView1.Rows[1].Cells[2].Value = r;
                if (IsSorted(arr, size)) dataGridView1.Rows[1].Cells[5].Value = true;
                else dataGridView1.Rows[1].Cells[5].Value = false;
                Array.Clear(arr, 0, size);
            }
            //включение
            if ((bool)dataGridView1.Rows[2].Cells[0].Value)
            {
                basic.CopyTo(arr, 0);
                k = 0;
                r = 0;
                int StartTime3 = Environment.TickCount;
                for (int i = 1; i < size; i++)
                {
                    int l = arr[i];
                    int j = i - 1;

                    while (j >= 0 && arr[j] > l)
                    {
                        arr[j + 1] = arr[j];
                        arr[j] = l;
                        k++;
                        r++;
                        j--;
                    }
                    r++;
                }
                int ResultTime3 = Environment.TickCount - StartTime3;
                dataGridView1.Rows[2].Cells[4].Value = ResultTime3.ToString();
                dataGridView1.Rows[2].Cells[3].Value = k;
                dataGridView1.Rows[2].Cells[2].Value = r;
                if (IsSorted(arr, size)) dataGridView1.Rows[2].Cells[5].Value = true;
                else dataGridView1.Rows[2].Cells[5].Value = false;
                Array.Clear(arr, 0, size);
            }

            //быстрая сортировка

            if ((bool)dataGridView1.Rows[3].Cells[0].Value)
            {
                basic.CopyTo(arr, 0);
                k = 0;//перестановки
                r = 0;//сравнения
                int StartTime5 = Environment.TickCount;
                QuickSort(arr, ref k, ref r);
                int ResultTime5 = Environment.TickCount - StartTime5;
                dataGridView1.Rows[3].Cells[4].Value = ResultTime5.ToString();
                dataGridView1.Rows[3].Cells[3].Value = k;
                dataGridView1.Rows[3].Cells[2].Value = r;
                if (IsSorted(arr, size)) dataGridView1.Rows[3].Cells[5].Value = true;
                else dataGridView1.Rows[3].Cells[5].Value = false;
                Array.Clear(arr, 0, size);
            }

            //Сортировка методом Шелла
            if ((bool)dataGridView1.Rows[4].Cells[0].Value)
            {
                basic.CopyTo(arr, 0);
                int StartTime6 = Environment.TickCount;
                int j, step = 0;
                int h = (int)(Math.Log(size, 2)) - 1;//формула Вирта (вычисляет кол-во шагов)
                for (int i = 0; i < h; i++)
                {
                    step = 2 * step + 1;//длина шага
                }
                for (; step > 0; step /= 2)
                {
                    for (int i = step; i < size; i++)
                    {
                        int tmp = arr[i];
                        for (j = i; j >= step; j -= step)//освободить место для переменной
                        {
                            r++;
                            if (tmp < arr[j - step])
                            {
                                k++;
                                arr[j] = arr[j - step];
                            }
                            else
                            {
                                break;
                            }
                        }
                        k++;
                        arr[j] = tmp;//ставит переменную на своё место
                    }
                }

                int ResultTime6 = Environment.TickCount - StartTime6;
                dataGridView1.Rows[4].Cells[4].Value = ResultTime6.ToString();
                dataGridView1.Rows[4].Cells[3].Value = k;
                dataGridView1.Rows[4].Cells[2].Value = r;
                if (IsSorted(arr, size)) dataGridView1.Rows[4].Cells[5].Value = true;
                else dataGridView1.Rows[4].Cells[5].Value = false;
                Array.Clear(arr, 0, size);
            }
            else
            {
                dataGridView1.Rows[4].Cells[5].Value = "";
                dataGridView1.Rows[4].Cells[4].Value = "";
                dataGridView1.Rows[4].Cells[3].Value = "";
                dataGridView1.Rows[4].Cells[2].Value = "";
            }

            //Линейная сортировка
            if ((bool)dataGridView1.Rows[5].Cells[0].Value)
            {
                basic.CopyTo(arr, 0);
                k = 0;
                r = 0;
                int StartTime7 = Environment.TickCount;
                // Определение максимального значения в массиве
                int max = arr[0];
                for (int i = 1; i < size; i++)
                {
                    if (arr[i] > max)
                    {
                        max = arr[i];
                    }
                    r++;
                }
                // Создание массива для подсчета количества элементов
                int[] support_arr = new int[max + 1];

                // Подсчет количества элементов в массиве
                for (int i = 0; i < size; i++)
                {
                    support_arr[arr[i]]++;
                }
                //Расставляем в массиве arr элементы поочерёдно, до тех пор,
                //пока в массиве support сумма элементов ячейки не станет =0
                //т.е. если в очередной ячейке лежит что-то больше нуля, то мы
                //8в исходный массив столько же раз отправляем номер этой ячейки.
                for (int i = 0, j = 0; i <= max; i++)
                {
                    while (support_arr[i] > 0)
                    {
                        arr[j] = i;
                        j++;
                        support_arr[i]--;//уменьшение кол-ва элементов(суммы)
                    }
                    k++;
                }
                int ResultTime7 = Environment.TickCount - StartTime7;
                dataGridView1.Rows[5].Cells[4].Value = ResultTime7.ToString();
                dataGridView1.Rows[5].Cells[3].Value = k;
                dataGridView1.Rows[5].Cells[2].Value = r;
                if (IsSorted(arr, size)) dataGridView1.Rows[5].Cells[5].Value = true;
                else dataGridView1.Rows[5].Cells[5].Value = false;
                Array.Clear(arr, 0, size);
            }
            else
            {
                dataGridView1.Rows[5].Cells[5].Value = "";
                dataGridView1.Rows[5].Cells[4].Value = "";
                dataGridView1.Rows[5].Cells[3].Value = "";
                dataGridView1.Rows[5].Cells[2].Value = "";
            }



            if ((bool)dataGridView1.Rows[6].Cells[0].Value)
            {
                basic.CopyTo(arr, 0);
                int StartTime8 = Environment.TickCount;
                Array.Sort(arr);
                int ResultTime8 = Environment.TickCount - StartTime8;
                dataGridView1.Rows[6].Cells[4].Value = ResultTime8.ToString();
                if (IsSorted(arr, size)) dataGridView1.Rows[6].Cells[5].Value = true;
                else dataGridView1.Rows[6].Cells[5].Value = false;
                Array.Clear(arr, 0, size);
            }
            else
            {
                dataGridView1.Rows[6].Cells[5].Value = "-";
                dataGridView1.Rows[6].Cells[4].Value = "-";
                dataGridView1.Rows[6].Cells[3].Value = "-";
                dataGridView1.Rows[6].Cells[2].Value = "-";
            }

            //Пирамидальная сортировка
            if ((bool)dataGridView1.Rows[7].Cells[0].Value)
            {
                k = 0;//перестановки
                r = 0;//сравнения
                int root = 0;
                basic.CopyTo(arr, 0);
                int StartTime9 = Environment.TickCount;
                int n = arr.Length;

                // Построение кучи (с середины до 0)
                for (int i = n / 2; i >= 0; i--)
                    FixDown(arr, i, n - 1, ref r, ref k);//восстанавливаем свойства кучи для поддерева с корнем в "i".

                // Один за другим извлекаем элементы из кучи
                for (int i = n - 1; i >= 1; i--)
                {
                    // Перемещаем текущий корень в конец
                    int temp = arr[0];
                    arr[0] = arr[i];
                    arr[i] = temp;
                    k++;

                    // восстанавливаем свойства кучи для поддерева с корнем в 0.
                    FixDown(arr, 0, i - 1, ref r, ref k);
                }
                int ResultTime9 = Environment.TickCount - StartTime9;
                dataGridView1.Rows[7].Cells[4].Value = ResultTime9.ToString();
                dataGridView1.Rows[7].Cells[3].Value = k;
                dataGridView1.Rows[7].Cells[2].Value = r;
                if (IsSorted(arr, size)) dataGridView1.Rows[7].Cells[5].Value = true;
                else dataGridView1.Rows[7].Cells[5].Value = false;
                Array.Clear(arr, 0, size);
            }
            else
            {
                dataGridView1.Rows[7].Cells[5].Value = "";
                dataGridView1.Rows[7].Cells[4].Value = "";
                dataGridView1.Rows[7].Cells[3].Value = "";
                dataGridView1.Rows[7].Cells[2].Value = "";
            }

            static void FixDown(int[] arr, int root, int n, ref int k, ref int r)
            {
                int bigElement;//самый большой элемент
                while (root * 2 <= n)//пока корень имеет хотя бы 1 потомка
                {
                    if ((root * 2 == n) || (arr[root * 2] > arr[root * 2 + 1]))//поиска наибольшего из потомков корня(левый или правый)
                        bigElement = root * 2;
                    else
                        bigElement = root * 2 + 1;
                    r += 2;
                    if (!(arr[root] < arr[bigElement]))// Если значение корня меньше наибольшего из потомков,
                        break;                         // то они меняются местами
                    int temp = arr[root];
                    arr[root] = arr[bigElement];
                    arr[bigElement] = temp;
                    root = bigElement;
                    k++;

                }

            }
        }

        //метод возвращающий индекс опорного элемента
        private int FindPivot(int[] arr, int minIndex, int maxIndex, ref int k, ref int r)
        {
            var pivot = minIndex - 1;
            int temp2 = 0;
            for (int i = minIndex; i < maxIndex; i++)
            {
                r++;
                if (arr[i] < arr[maxIndex])
                {
                    pivot++;
                    temp2 = arr[pivot];
                    arr[pivot] = arr[i];
                    arr[i] = temp2;
                    k++;
                }
            }

            pivot++;
            temp2 = arr[pivot];
            arr[pivot] = arr[maxIndex];
            arr[maxIndex] = temp2;
            k++;
            return pivot;
        }

        private int[] QuickSort(int[] arr, int minIndex, int maxIndex, ref int r, ref int k)
        {
            r++;
            if (minIndex >= maxIndex)
            {
                return arr;
            }

            var pivotIndex = FindPivot(arr, minIndex, maxIndex, ref r, ref k);
            QuickSort(arr, minIndex, pivotIndex - 1, ref r, ref k);
            QuickSort(arr, pivotIndex + 1, maxIndex, ref r, ref k);

            return arr;
        }

        private int[] QuickSort(int[] array, ref int r, ref int k)
        {
            return QuickSort(array, 0, array.Length - 1, ref r, ref k);
        }

        //проверка на отсортированность
        private bool IsSorted(int[] arr, int size)
        {
            if (arr.Length < 2) return true;
            int prev = arr[0];
            for (int i = 1; i < size; i++)
            {
                if (arr[i] < prev) return false;
                prev = arr[i];
            }
            return true;
        }

        



        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

