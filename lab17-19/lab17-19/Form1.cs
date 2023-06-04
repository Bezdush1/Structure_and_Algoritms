using System;
using System.Windows.Forms;

namespace lab17_19
{
    public partial class Form1 : Form
    {
        static int size = 15;
        int[] array = new int[size + 1];
        bool CheckQueue = false;

        private int Count = 1;
        private int CountElemInRowSample = 0;
        public Form1()
        {
            InitializeComponent();

            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = 15;
            for (int i = 0; i < 15; i++)
                dataGridView1.Columns[i].Width = 40;

            dataGridView2.ColumnHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.RowCount = 4;
            dataGridView2.ColumnCount = 15;
            for (int i = 0; i < 15; i++)
                dataGridView2.Columns[i].Width = 40;

            dataGridView3.ColumnHeadersVisible = false;
            dataGridView3.RowHeadersVisible = false;
            dataGridView3.RowCount = 1;
            dataGridView3.ColumnCount = 15;
            for (int i = 0; i < 15; i++)
                dataGridView3.Columns[i].Width = 40;
            for (int i = 0; i < size; i++)
            {
                dataGridView3.Rows[0].Cells[i].Value = "";
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CreateQueue_Click(object sender, EventArgs e)
        {
            if (!CheckQueue)
            {
                Random random = new Random();
                for (int i = 1; i < array.Length; i++)
                {
                    array[i] = random.Next(10, 99);
                    fixup(array, i);
                    Count++;
                }
                PrintArray1(array);
                PrintArray2(array);
                CheckQueue = true;
            }
            else
            {
                MessageBox.Show("Вы уже создали очередь!");
            }

        }

        private bool Clear_Tab()
        {
            dataGridView1.Rows.Clear();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                    dataGridView2.Rows[i].Cells[j].Value = "";
            }
            dataGridView3.Rows.Clear();
            for (int i = 0; i < size; i++)
                array[i] = 0;
            Count = 1;
            CountElemInRowSample = 0;
            return true;
        }

        private void ClearQueue_Click(object sender, EventArgs e)
        {
            if (Count != 0)
            {
                CheckQueue = Clear_Tab();
            }
            if (CheckQueue)
            {
                CheckQueue = Clear_Tab();
                CheckQueue = false;
            }
            else
            {
                MessageBox.Show("Очередь уже очищена!");
            }
        }

        private bool PrintArray1(int[] array)
        {
            for (int k = 0, l = 1; l < array.Length; k++, l++)
            {
                if (array[l] != 0)
                {
                    dataGridView1.Rows[0].Cells[k].Value = array[l];
                }
                else
                {
                    dataGridView1.Rows[0].Cells[k].Value = "";
                }
            }
            return true;
        }

        private void PrintArray2(int[] array)
        {
            int position = size / 2;
            int i = 1, Rows = 0;
            int maxElemInRows = 1;
            while (Rows < dataGridView2.RowCount)
            {
                int j = 0;
                int firstElemInRows = position;
                while (maxElemInRows > j && i < array.Length)
                {
                    if (array[i] == 0)
                        dataGridView2.Rows[Rows].Cells[firstElemInRows].Value = "";
                    else
                        dataGridView2.Rows[Rows].Cells[firstElemInRows].Value = array[i++];
                    firstElemInRows += position * 2 + 2;
                    j++;
                }
                position /= 2;
                maxElemInRows *= 2;
                Rows++;
            }
        }

        //private void PrintArray3(int value)
        //{
        //    if (dataGridView3 != null && dataGridView3.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < array.Length; i++)
        //        {
        //            if (dataGridView3.Rows[0].Cells[i].Value == null || dataGridView3.Rows[0].Cells[i].Value.ToString() == "")
        //            {
        //                dataGridView3.Rows[0].Cells[i].Value = value;
        //                break;
        //            }
        //        }
        //    }
        //    varible++;
        //}

        private void TakeMax_Click(object sender, EventArgs e)
        {
            if (Count == 1)
            {
                MessageBox.Show("Массив пустой. Операция отклонена");
                return;
            }
            if (CountElemInRowSample == 15)
            {
                MessageBox.Show("Очередь выборки заполнена. Операция отклонена");
                return;
            }
            dataGridView3.Rows[0].Cells[CountElemInRowSample].Value = array[1];
            CountElemInRowSample++;
            array[1] = array[Count - 1];
            array[Count - 1] = 0;
            Count--;



            fixDown(array, 1, size);
            PrintArray1(array);
            PrintArray2(array);
        }

        private void InsertNew_Click(object sender, EventArgs e)
        {
            int i;

            int value = (int)numericUpDown1.Value;
            for (i = 1; i < array.Length; i++)
            {
                if (array[i] == 0)
                {
                    array[i] = value;
                    fixup(array, i);
                    PrintArray1(array);
                    PrintArray2(array);
                    Count++;
                    return;
                }
            }
            CheckQueue = true;
            if (i == 16)
            {
                MessageBox.Show("Вы заполнинил всю очередь!");
                return;
            }

        }

        private void ChangeImpotant_Click(object sender, EventArgs e)
        {
            int val = (int)numericUpDown2.Value;
            int valueTo = (int)numericUpDown3.Value;
            int index = -1;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] == val)
                {
                    index = i;
                    array[i] = valueTo;
                    break;
                }
            }
            if (index < 0)
            {
                MessageBox.Show("Значение не найдено");
                return;
            }
            if (val > valueTo)
            {
                fixDown(array, index, size);
            }
            else
            {
                fixup(array, index);
            }
            PrintArray1(array);
            PrintArray2(array);
        }

        public static void fixup(int[] array, int index)
        {
            while (index > 1 && array[index / 2] < array[index])
            {
                int temp = array[index / 2];
                array[index / 2] = array[index];
                array[index] = temp;
                index /= 2;
            }
        }

        private void fixDown(int[] array, int index, int size)
        {
            while (2 * index <= size)
            {
                int j = 2 * index;
                if (j < size && array[j] < array[j + 1])
                {
                    j++;
                }
                if (!(array[index] < array[j]))
                                           
                {
                    break;
                }
                int temp = array[index];
                array[index] = array[j];
                array[j] = temp;
                index = j;
            }
        }
    }
}
