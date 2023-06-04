using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab21_25
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            dataGridView1.RowCount = 5;        // число строк
            dataGridView1.ColumnCount = 6;  // число столбцов
            dataGridView1.Rows[0].Cells[1].Value = "Простое 2ф";
            dataGridView1.Rows[1].Cells[1].Value = "Простое 1ф";
            dataGridView1.Rows[2].Cells[1].Value = "Естественное 2ф";
            dataGridView1.Rows[3].Cells[1].Value = "Естественное 1ф";
            dataGridView1.Rows[4].Cells[1].Value = "Поглощение";

            dataGridView1.Rows[0].Cells[0].Value = false;
            dataGridView1.Rows[1].Cells[0].Value = false;
            dataGridView1.Rows[2].Cells[0].Value = false;
            dataGridView1.Rows[3].Cells[0].Value = false;
            dataGridView1.Rows[4].Cells[0].Value = true;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            int arrayLength = (int)lengthArray.Value;
            int[] arrayBasic = new int[arrayLength];
            int[] arraySupport = new int[arrayLength];
            Random rnd = new Random();
            for (int i = 0; i < arrayLength; i++)
                arrayBasic[i] = rnd.Next(1, arrayLength);
            int comparisons = 0;//сравнения
            int permutation = 0;//перестановки
            int series = 1;//серия
            if ((bool)dataGridView1.Rows[0].Cells[0].Value)
            {
                arrayBasic.CopyTo(arraySupport, 0);
                int StartTime = Environment.TickCount;
                int[] arrayB = new int[arrayLength];
                int[] arrayC = new int[arrayLength];
                while (series < arrayLength)
                {
                    int indexB = 0, indexC = 0, indexA = 0;

                    while (indexA < arrayLength)//разделяем наш массив на 2 массива В и С
                    {
                        int i = 0, j = 0;
                        while (i < series && indexA < arrayLength)//пока размер не достиг серии
                        {
                            arrayB[indexB++] = arraySupport[indexA++];
                            permutation++;
                            i++;
                        }
                        while (j < series && indexA < arrayLength)//пока размер не достиг серии
                        {
                            arrayC[indexC++] = arraySupport[indexA++];
                            permutation++;
                            j++;
                        }
                    }

                    indexA = 0;
                    indexB = 0;
                    indexC = 0;
                    while (indexA < arrayLength)
                    {
                        int i = 0, j = 0;
                        while (i < series && j < series && arrayB[indexB] != 0 && arrayC[indexC] != 0)
                        {
                            if (arrayB[indexB] < arrayC[indexC])
                            {
                                arraySupport[indexA++] = arrayB[indexB++];
                                permutation++;
                                i++;
                            }
                            else
                            {
                                arraySupport[indexA++] = arrayC[indexC++];
                                permutation++;
                                j++;
                            }
                            comparisons++;
                        }
                        while (i < series && arrayB[indexB] != 0)
                        {
                            arraySupport[indexA++] = arrayB[indexB++];
                            permutation++;
                            i++;
                        }
                        while (j < series && arrayC[indexC] != 0)
                        {
                            arraySupport[indexA++] = arrayC[indexC++];
                            permutation++;
                            j++;
                        }
                    }
                    series *= 2;
                    Array.Clear(arrayB, 0, arrayLength);
                    Array.Clear(arrayC, 0, arrayLength);
                }

                int ResultTime = Environment.TickCount - StartTime;
                dataGridView1.Rows[0].Cells[4].Value = ResultTime.ToString();
                dataGridView1.Rows[0].Cells[3].Value = permutation;
                dataGridView1.Rows[0].Cells[2].Value = comparisons;
                if (IsSorted(arraySupport)) dataGridView1.Rows[0].Cells[5].Value = true;
                else dataGridView1.Rows[0].Cells[5].Value = false;
                Array.Clear(arraySupport, 0, arrayLength);


            }
            else
            {
                dataGridView1.Rows[0].Cells[5].Value = "";
                dataGridView1.Rows[0].Cells[4].Value = "";
                dataGridView1.Rows[0].Cells[3].Value = "";
                dataGridView1.Rows[0].Cells[2].Value = "";
            }

            //22
            if ((bool)dataGridView1.Rows[1].Cells[0].Value)
                SimpleOneFSort(arrayBasic);
            else
            {
                dataGridView1.Rows[1].Cells[5].Value = "";
                dataGridView1.Rows[1].Cells[4].Value = "";
                dataGridView1.Rows[1].Cells[3].Value = "";
                dataGridView1.Rows[1].Cells[2].Value = "";
            }

            //23
            if ((bool)dataGridView1.Rows[2].Cells[0].Value)
            {
                int size = arrayBasic.Length;
                arrayBasic.CopyTo(arraySupport, 0);
                int comparison = 0;   //сравнения
                int assigmnment = 0;  //присвоения
                int StartTime3 = Environment.TickCount;

                int[] b = new int[size];
                int[] c = new int[size];

                bool sorted = false;
                while (!sorted)
                {
                    int indexA = 0, indexB = 0, indexC = 0;
                    bool massivSwitch = true;
                    while (indexA < size)
                    {
                        if (massivSwitch)
                        {
                            do
                            {
                                b[indexB++] = arraySupport[indexA++];
                                comparison++;
                                assigmnment++;
                            }
                            while (indexA < size && arraySupport[indexA - 1] <= arraySupport[indexA]);
                            massivSwitch = !massivSwitch;
                        }
                        else
                        {
                            do
                            {
                                c[indexC++] = arraySupport[indexA++];
                                comparison++;
                                assigmnment++;
                            }
                            while (indexA < size && arraySupport[indexA - 1] <= arraySupport[indexA]);
                            massivSwitch = !massivSwitch;
                        }
                    }
                    if (b[0] == 0 || c[0] == 0) break;

                    indexA = 0; indexB = 0; indexC = 0;
                    bool b_sequence, с_sequence;//переменные для отслеживания отсортированности на текущем шаге
                    while (b[indexB] != 0 && c[indexC] != 0 && indexA < size)//слияние
                    {
                        b_sequence = с_sequence = false;
                        while (!b_sequence && !с_sequence && indexA < size)
                        {
                            if (b[indexB] <= c[indexC])//выбераем меньший или равный из В и С
                            {
                                arraySupport[indexA++] = b[indexB];
                                b_sequence = b[indexB] > b[indexB + 1];//проверка упорядоченной послед-ти
                                b[indexB++] = 0;
                            }
                            else
                            {
                                arraySupport[indexA++] = c[indexC];
                                с_sequence = c[indexC] > c[indexC + 1];
                                c[indexC++] = 0;
                            }
                            assigmnment += 2;
                            comparison++;
                        }
                        while (!b_sequence && indexA < size)
                        {
                            arraySupport[indexA++] = b[indexB];
                            b_sequence = b[indexB] > b[indexB + 1];
                            b[indexB++] = 0;
                            comparison++;
                            assigmnment++;
                        }
                        while (!с_sequence && indexA < size)
                        {
                            arraySupport[indexA++] = c[indexC];
                            с_sequence = c[indexC] > c[indexC + 1];
                            c[indexC++] = 0;
                            comparison++;
                            assigmnment++;
                        }
                    }
                }

                int ResultTime3 = Environment.TickCount - StartTime3;
                dataGridView1.Rows[2].Cells[4].Value = ResultTime3.ToString();
                dataGridView1.Rows[2].Cells[3].Value = comparison;
                dataGridView1.Rows[2].Cells[2].Value = assigmnment;
                if (IsSorted(arraySupport)) dataGridView1.Rows[2].Cells[5].Value = true;
                else dataGridView1.Rows[2].Cells[5].Value = false;
                Array.Clear(arraySupport, 0, size);

            }
            else
            {
                dataGridView1.Rows[2].Cells[5].Value = "";
                dataGridView1.Rows[2].Cells[4].Value = "";
                dataGridView1.Rows[2].Cells[3].Value = "";
                dataGridView1.Rows[2].Cells[2].Value = "";
            }


            //24
            if ((bool)dataGridView1.Rows[3].Cells[0].Value)
            {

                arrayBasic.CopyTo(arraySupport, 0);
                int size = arrayBasic.Length;
                int comparison24 = 0;//перестановки
                int assigments24 = 0;//cравнения
                int StartTime4 = Environment.TickCount;
                int[] b = new int[size];
                int[] c = new int[size];
                int[] D = new int[size];
                int[] E = new int[size];

                int[] read1; int[] read2;//массивы чтения
                int[] write1; int[] write2;//массивы записи

                bool masivsSwitch = true;

                int indexB = 0, indexC = 0, indexA = 0;
                bool massivSwitch = true;// переключатель - чтобы записывать правильную упоряд.последоват - ть
                while (indexA < size)//пока не конец массива А
                {
                    if (massivSwitch)
                    {
                        do
                        {
                            b[indexB++] = arraySupport[indexA++];
                            comparison24++;
                            assigments24++;
                        }
                        while (indexA < size && arraySupport[indexA - 1] <= arraySupport[indexA]);//пока не конец А и
                                                                                //предыдущий элемент из А
                                                                                //меньше или = текущему
                        massivSwitch = !massivSwitch;
                    }
                    else
                    {
                        do
                        {
                            c[indexC++] = arraySupport[indexA++];
                            comparison24++;
                            assigments24++;
                        }
                        while (indexA < size && arraySupport[indexA - 1] <= arraySupport[indexA]);
                        massivSwitch = !massivSwitch;
                    }
                }

                while (true)
                {
                    int indexRead1 = 0, indexRead2 = 0, indexWrite1 = 0, indexWrite2 = 0;

                    if (masivsSwitch)//переключатель для чтения из массивов В С или D E
                    {
                        read1 = b;
                        read2 = c;
                        write1 = D;
                        write2 = E;
                    }
                    else
                    {
                        read1 = D;
                        read2 = E;
                        write1 = b;
                        write2 = c;
                    }

                    bool read1_sequence, read2_sequence;//переменные для отслеживания отсортированности
                                                        //на текущем шаге
                    massivSwitch = true;//переключатель
                    int fillCount = 0;// количество элементов, которые были записаны в выходные массивы
                    while (read1[indexRead1] != 0 && read2[indexRead2] != 0 && fillCount < size)
                    {
                        read1_sequence = read2_sequence = false;
                        while (!read1_sequence && !read2_sequence && fillCount < size)
                        {
                            if (read1[indexRead1] <= read2[indexRead2])//выбераем меньший или равный из В и С
                                                                       //или D и E
                            {
                                if (massivSwitch)
                                {
                                    if (write1[0] != 0 && write1[indexWrite1 - 1] > read1[indexRead1])
                                        //если элемент не 0 и предыдущий больше следующего(неупоряд)
                                        massivSwitch = false;
                                }
                                else
                                {
                                    if (write2[0] != 0 && write2[indexWrite2 - 1] > read1[indexRead1])
                                        //если элемент не 0 и предыдущий больше следующего(неупоряд)
                                        massivSwitch = true;
                                }
                                if (massivSwitch)
                                {
                                    write1[indexWrite1++] = read1[indexRead1];
                                }
                                else
                                {
                                    write2[indexWrite2++] = read1[indexRead1];
                                }
                                comparison24++;
                                assigments24 += 2;
                                read1_sequence = read1[indexRead1] > read1[indexRead1 + 1];//проверка упорядо-ти
                                read1[indexRead1++] = 0;
                            }
                            else
                            {
                                if (massivSwitch)
                                {
                                    //если значение последнего записанного элемента больше текущего, то порядок
                                    //нарушен
                                    if (write1[0] != 0 && write1[indexWrite1 - 1] > read2[indexRead2])
                                        massivSwitch = false;
                                }
                                else
                                {
                                    if (write2[0] != 0 && write2[indexWrite2 - 1] > read2[indexRead2])
                                        massivSwitch = true;
                                }
                                if (massivSwitch)
                                {
                                    write1[indexWrite1++] = read2[indexRead2];
                                }
                                else
                                {
                                    write2[indexWrite2++] = read2[indexRead2];
                                }
                                comparison24++;
                                assigments24 += 2;
                                read2_sequence = read2[indexRead2] > read2[indexRead2 + 1];
                                read2[indexRead2++] = 0;
                            }
                            fillCount++;
                        }
                        while (!read1_sequence && fillCount < size)//оставшиеся из 1 массива
                        {
                            if (massivSwitch)
                            {
                                if (write1[0] != 0 && write1[indexWrite1 - 1] > read1[indexRead1])
                                    massivSwitch = false;
                            }
                            else
                            {
                                if (write2[0] != 0 && write2[indexWrite2 - 1] > read1[indexRead1])
                                    massivSwitch = true;
                            }
                            if (massivSwitch)
                            {
                                write1[indexWrite1++] = read1[indexRead1];
                            }
                            else
                            {
                                write2[indexWrite2++] = read1[indexRead1];
                            }
                            comparison24++;
                            assigments24 += 2;
                            read1_sequence = read1[indexRead1] > read1[indexRead1 + 1];
                            read1[indexRead1++] = 0;
                            fillCount++;
                        }
                        while (!read2_sequence && fillCount < size)//оставшиеся из 2 массива
                        {
                            if (massivSwitch)
                            {
                                if (write1[0] != 0 && write1[indexWrite1 - 1] > read2[indexRead2])
                                    massivSwitch = false;
                            }
                            else
                            {
                                if (write2[0] != 0 && write2[indexWrite2 - 1] > read2[indexRead2])
                                    massivSwitch = true;
                            }
                            if (massivSwitch)
                            {
                                write1[indexWrite1++] = read2[indexRead2];
                            }
                            else
                            {
                                write2[indexWrite2++] = read2[indexRead2];
                            }
                            comparison24++;
                            assigments24 += 2;
                            read2_sequence = read2[indexRead2] > read2[indexRead2 + 1];
                            read2[indexRead2++] = 0;
                            fillCount++;
                        }
                    }
                    //докидывание оставшихся элементов после основного цикла сливания
                    while (indexRead1 < size && read1[indexRead1] != 0 && fillCount < size)
                    {
                        if (massivSwitch)
                        {
                            if (write1[0] != 0 && write1[indexWrite1 - 1] > read1[indexRead1])
                                massivSwitch = false;
                        }
                        else
                        {
                            if (write2[0] != 0 && write2[indexWrite2 - 1] > read1[indexRead1])
                                massivSwitch = true;
                        }
                        if (massivSwitch)
                        {
                            write1[indexWrite1++] = read1[indexRead1];
                        }
                        else
                        {
                            write2[indexWrite2++] = read1[indexRead1];
                        }
                        comparison24++;
                        assigments24++;
                        read1[indexRead1++] = 0;
                        fillCount++;
                    }
                    while (indexRead2 < size && read2[indexRead2] != 0 && fillCount < size)
                    {
                        if (massivSwitch)
                        {
                            if (write1[0] != 0 && write1[indexWrite1 - 1] > read2[indexRead2])
                                massivSwitch = false;
                        }
                        else
                        {
                            if (write2[0] != 0 && write2[indexWrite2 - 1] > read2[indexRead2])
                                massivSwitch = true;
                        }
                        if (massivSwitch)
                        {
                            write1[indexWrite1++] = read2[indexRead2];
                        }
                        else
                        {
                            write2[indexWrite2++] = read2[indexRead2];
                        }
                        comparison24++;
                        assigments24++;
                        read2[indexRead2++] = 0;
                        fillCount++;
                    }

                    if (write1[0] == 0)//записываем из 2 массива в исходный
                    {
                        for (int i = 0; i < size; i++)
                            arraySupport[i] = write2[i];
                        break;
                    }
                    else if (write2[0] == 0)//записываем из 1 массива в исходный
                    {
                        for (int i = 0; i < size; i++)
                            arraySupport[i] = write1[i];
                        break;
                    }
                    masivsSwitch = !masivsSwitch;
                }



                int ResultTime4 = Environment.TickCount - StartTime4;
                dataGridView1.Rows[3].Cells[4].Value = ResultTime4.ToString();
                dataGridView1.Rows[3].Cells[3].Value = comparison24;
                dataGridView1.Rows[3].Cells[2].Value = assigments24;
                if (IsSorted(arraySupport)) dataGridView1.Rows[3].Cells[5].Value = true;
                else dataGridView1.Rows[3].Cells[5].Value = false;
                Array.Clear(arraySupport, 0, size);

            }
            else
            {
                dataGridView1.Rows[3].Cells[5].Value = "";
                dataGridView1.Rows[3].Cells[4].Value = "";
                dataGridView1.Rows[3].Cells[3].Value = "";
                dataGridView1.Rows[3].Cells[2].Value = "";
            }
            //25
            if ((bool)dataGridView1.Rows[4].Cells[0].Value)
            {
                int ramPercent = (int)numericUpDown2.Value;
                arrayBasic.CopyTo(arraySupport, 0);
                int comparisons5 = 0;//перестановки
                int assigments5 = 0;//cравнения
                int length = arraySupport.Length;
                int StartTime5 = Environment.TickCount;
                int inMemorySize = (int)(length * ramPercent * 0.01);//кол-во элементов для временного хранения

                int[] inMemoryArray = new int[inMemorySize];//массив для временного хранения элементов в памяти
                int offset = 0;//текущая позиция в исходном массиве А

                while (offset < length)//пока не конец А
                {
                    int counter = 0;//счётчик скопированных элементов из исходного массива во временный
                    while (counter < inMemorySize && offset < length)//пока не всё скопировали и не конец А 
                                                                     //Копируем из А с конца во временный массив
                    {
                        inMemoryArray[counter++] = arraySupport[length - 1 - offset];
                        offset++;
                        comparisons5++;
                    }
                    inMemoryArray = inMemoryArray.Where(x => x != 0).ToArray();//если элемент 0 - то в массив не записываем
                    Array.Sort(inMemoryArray);//сортируем

                    int[] sortedSequence = new int[offset - counter];//массив скопированных элементов равный кол-ву скопированных
                    int sortedQuantity = offset - counter;//кол-во скопированных
                    counter = 0;
                    int increment = 0;//для смещения индексов при копировании(для правильной после-ти)
                    while (counter < sortedQuantity)//пока не больше кол-ва скопированных
                    {
                        sortedSequence[counter++] = arraySupport[length - sortedQuantity + increment];
                        increment++;
                        comparisons5++;
                    }

                    int indexInMemoryArray = 0, indexSortedArray = 0;//индексы временного массива и скопированных чисел
                    int mergeOffset = offset;//текущая позиция в массиве А(туда помещается элемент при слиянии)
                    while (indexInMemoryArray < inMemoryArray.Length && indexSortedArray < sortedSequence.Length)//объединяем 2 массива в исходный
                    {
                        if (inMemoryArray[indexInMemoryArray] < sortedSequence[indexSortedArray])//записываем сначала меньший
                        {
                            arraySupport[length - mergeOffset] = inMemoryArray[indexInMemoryArray];
                            inMemoryArray[indexInMemoryArray++] = 0;
                        }
                        else
                        {
                            arraySupport[length - mergeOffset] = sortedSequence[indexSortedArray];
                            sortedSequence[indexSortedArray++] = 0;
                        }
                        comparisons5++;
                        assigments5++;
                        mergeOffset--;
                    }
                    while (indexInMemoryArray < inMemoryArray.Length)//докидывание оставшихся из массива временного хранения
                    {
                        arraySupport[length - mergeOffset] = inMemoryArray[indexInMemoryArray];
                        inMemoryArray[indexInMemoryArray++] = 0;
                        mergeOffset--;
                        comparisons5++;
                    }
                    while (indexSortedArray < sortedSequence.Length)//докидывание оставшихся из массива скопированных элементов
                    {
                        arraySupport[length - mergeOffset] = sortedSequence[indexSortedArray];
                        sortedSequence[indexSortedArray++] = 0;
                        mergeOffset--;
                        comparisons5++;
                    }
                }
                int ResultTime5 = Environment.TickCount - StartTime5;
                dataGridView1.Rows[4].Cells[4].Value = ResultTime5.ToString();
                dataGridView1.Rows[4].Cells[3].Value = comparisons5;
                dataGridView1.Rows[4].Cells[2].Value = assigments5;
                if (IsSorted(arraySupport)) dataGridView1.Rows[4].Cells[5].Value = true;
                else dataGridView1.Rows[4].Cells[5].Value = false;
                Array.Clear(arraySupport, 0, arrayLength);

            }
            else
            {
                dataGridView1.Rows[4].Cells[5].Value = "";
                dataGridView1.Rows[4].Cells[4].Value = "";
                dataGridView1.Rows[4].Cells[3].Value = "";
                dataGridView1.Rows[4].Cells[2].Value = "";
            }
    }

        private bool IsSorted(int[] array)
        {
            if (array.Length < 2) return true;
            for (int i = 0; i < array.Length-1; i++)
            {
                if (array[i] > array[i+1]) return false;
            }
            return true;
        }

        private void SimpleOneFSort(int[] array)
        {
            int[] arrayForSorting = new int[array.Length];
            Array.Copy(array, arrayForSorting, array.Length);
            int length = arrayForSorting.Length;

            long assignments = 0, comparisons = 0;
            long startTime = Environment.TickCount;

            int[] arrB = new int[length];
            int[] arrC = new int[length];
            int[] arrD = new int[length];
            int[] arrE = new int[length];

            int[] read1; int[] read2;
            int[] write1; int[] write2;

            int k = 1;
            bool filesSwitch = true;

            int indexB = 0, indexC = 0;
            for (int i = 0; i < length; i++)
            {
                if (i % 2 == 0)
                    arrB[indexB++] = arrayForSorting[i];
                else
                    arrC[indexC++] = arrayForSorting[i];
                assignments++;
            }

            while (k < length)
            {
                int indexRead1 = 0, indexRead2 = 0, indexWrite1 = 0, indexWrite2 = 0;

                if (filesSwitch)
                {
                    read1 = arrB;
                    read2 = arrC;
                    write1 = arrD;
                    write2 = arrE;
                }
                else
                {
                    read1 = arrD;
                    read2 = arrE;
                    write1 = arrB;
                    write2 = arrC;
                }

                int mergeCount = k * 2; //требуемое количество элементов для слияния
                bool fillSwitch = true;
                int fillCount = 0; //количество записанных элементов
                while (indexRead1 + indexRead2 < length)
                {
                    int i = 0, j = 0;
                    while (i < k && j < k && read1[indexRead1] != 0 && read2[indexRead2] != 0)
                    {
                        if (read1[indexRead1] < read2[indexRead2])
                        {
                            if (fillCount == mergeCount)
                            {
                                fillSwitch = !fillSwitch;
                                fillCount = 0;
                            }
                            if (fillSwitch)
                            {
                                write1[indexWrite1++] = read1[indexRead1];
                            }
                            else
                            {
                                write2[indexWrite2++] = read1[indexRead1];
                            }
                            read1[indexRead1++] = 0;
                            fillCount++;
                            i++;
                        }
                        else
                        {
                            if (fillCount == mergeCount)
                            {
                                fillSwitch = !fillSwitch;
                                fillCount = 0;
                            }
                            if (fillSwitch)
                            {
                                write1[indexWrite1++] = read2[indexRead2];
                            }
                            else
                            {
                                write2[indexWrite2++] = read2[indexRead2];
                            }
                            read2[indexRead2++] = 0;
                            fillCount++;
                            j++;
                        }
                        assignments++;
                        comparisons++;
                    }
                    while (i < k && read1[indexRead1] != 0)
                    {
                        if (fillCount == mergeCount)
                        {
                            fillSwitch = !fillSwitch;
                            fillCount = 0;
                        }
                        if (fillSwitch)
                        {
                            write1[indexWrite1++] = read1[indexRead1];
                        }
                        else
                        {
                            write2[indexWrite2++] = read1[indexRead1];
                        }
                        read1[indexRead1++] = 0;
                        fillCount++;
                        i++;
                        assignments++;
                    }
                    while (j < k && read2[indexRead2] != 0)
                    {
                        if (fillCount == mergeCount)
                        {
                            fillSwitch = !fillSwitch;
                            fillCount = 0;
                        }
                        if (fillSwitch)
                        {
                            write1[indexWrite1++] = read2[indexRead2];
                        }
                        else
                        {
                            write2[indexWrite2++] = read2[indexRead2];
                        }
                        read2[indexRead2++] = 0;
                        fillCount++;
                        j++;
                        assignments++;
                    }
                }
                k *= 2;
                filesSwitch = !filesSwitch;
            }

            int[] data;
            if (arrB[0] != 0)
                data = arrB;
            else if (arrC[0] != 0)
                data = arrC;
            else if (arrD[0] != 0)
                data = arrD;
            else
                data = arrE;
            for (int i = 0; i < length; i++)
                arrayForSorting[i] = data[i];

            dataGridView1.Rows[1].Cells[2].Value = comparisons.ToString();
            dataGridView1.Rows[1].Cells[3].Value = assignments.ToString();
            dataGridView1.Rows[1].Cells[4].Value = (Environment.TickCount - startTime).ToString();
            dataGridView1.Rows[1].Cells[5].Value = IsSorted(arrayForSorting);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
