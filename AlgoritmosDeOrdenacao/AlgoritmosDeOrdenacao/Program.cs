using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Collections;

namespace AlgoritmosDeOrdenacao
{
    class Program
    {
        static int numOfLines = 500;

        static void Main(string[] args)
        {

            // Lê arquivo e mapeia colunas para a lista de objetos
            List<DataSetEl> dataSetArray = new List<DataSetEl>();
            readFileContent(ref dataSetArray);

            // Inicia o algoritmo de bubble sort
            Stopwatch stopWatch = new Stopwatch();
            String flag = "bubble";
            DateTime localDate = DateTime.Now;

            if (flag == "bubble")
            {

                stopWatch.Start();
                bubbleSort(dataSetArray);
                stopWatch.Stop();
                printArray(dataSetArray, stopWatch.ElapsedMilliseconds, "BubbleSort - " + numOfLines + " lines" + " - " + localDate.Millisecond);
                stopWatch.Reset();

            }
            else if (flag == "quick")
            {

                stopWatch.Start();
                quickSort(ref dataSetArray, 0, dataSetArray.Count - 1);
                stopWatch.Stop();
                TimeSpan ts2 = stopWatch.Elapsed;
                printArray(dataSetArray, stopWatch.ElapsedMilliseconds, "QuickSort - " + numOfLines + " lines" + " - " + localDate.Millisecond);

            }
            else if (flag == "selection")
            {
                stopWatch.Start();
                selectionSort(dataSetArray);
                stopWatch.Stop();
                TimeSpan ts2 = stopWatch.Elapsed;
                printArray(dataSetArray, stopWatch.ElapsedMilliseconds, "SelectionSort - " + numOfLines + " lines" + " - " + localDate.Millisecond);
            }
            else if (flag == "my")
            {
                stopWatch.Start();
                mySort(dataSetArray);
                stopWatch.Stop();
                TimeSpan ts2 = stopWatch.Elapsed;
                printArray(dataSetArray, stopWatch.ElapsedMilliseconds, "MySort - " + numOfLines + " lines" + " - " + localDate.Millisecond);
            }
            else if (flag == "merge")
            {
                stopWatch.Start();
                mergeSort(dataSetArray);
                stopWatch.Stop();
                TimeSpan ts2 = stopWatch.Elapsed;
                printArray(dataSetArray, stopWatch.ElapsedMilliseconds, "MergeSort - " + numOfLines + " lines" + " - " + localDate.Millisecond);
            }
            else if (flag == "insertion")
            {
                stopWatch.Start();
                insertionSort(dataSetArray);
                stopWatch.Stop();
                printArray(dataSetArray, stopWatch.ElapsedMilliseconds, "InsertionSort - " + numOfLines + " lines" + " - " + localDate.Millisecond);
                stopWatch.Reset();
            }

            Console.WriteLine("Fim das ordenações, pressione qualquer tecla para encerrar");
            Console.ReadKey();
        }

        static void readFileContent(ref List<DataSetEl> dataSetArray)
        {
            try
            {

                int counter = 0;
                string line;

                StreamReader file = new StreamReader("dataset_ordered_desc.txt");

                while ((line = file.ReadLine()) != null && counter < numOfLines)
                {
                    if (counter > 0)
                    {
                        string[] lineSplited = line.Split('\t');
                        DataSetEl aux = new DataSetEl();
                        aux.room_id = long.Parse(lineSplited[0]);
                        aux.host_id = long.Parse(lineSplited[1]);
                        aux.room_type = lineSplited[2];
                        aux.country = lineSplited[3];
                        aux.city = lineSplited[4];
                        aux.neighborhood = lineSplited[5];
                        aux.reviews = lineSplited[6];
                        aux.overall_satisfaction = lineSplited[7];
                        aux.accommodates = lineSplited[8];
                        aux.bedrooms = lineSplited[9];
                        aux.price = lineSplited[10];
                        aux.property_type = lineSplited[11];

                        dataSetArray.Add(aux);
                    }
                    counter++;
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
        static void printArray(List<DataSetEl> el, long time, String fileName)
        {
            TextWriter tw = new StreamWriter(fileName + ".txt");
            int count = 0;
            tw.WriteLine("Tempo gasto: " + time + " milisegundos para ordenar " + numOfLines + " elementos\n\n");
            while (count < numOfLines - 1)
            {
                tw.WriteLine(el[count].room_id);
                count++;
            }
            tw.Close();
        }

        // bubble sort
        static List<DataSetEl> bubbleSort(List<DataSetEl> dataset)
        {
            int i, j;
            for (i = 0; i < dataset.Count - 1; i++)
            {
                for (j = 0; j < dataset.Count - i - 1; j++)
                {
                    if (dataset[j].room_id > dataset[j + 1].room_id)
                    {
                        DataSetEl temp = dataset[j];
                        dataset[j] = dataset[j + 1];
                        dataset[j + 1] = temp;
                    }
                }
            }

            return dataset;
        }

        // quick sort
        static int partition(List<DataSetEl> dataset, int minor, int max)
        {
            long pivô = dataset[max].room_id;

            int i = (minor - 1);
            for (int j = minor; j < max; j++)
            {
                if (dataset[j].room_id < pivô)
                {
                    i++;
                    DataSetEl temp = dataset[i];
                    dataset[i] = dataset[j];
                    dataset[j] = temp;
                }
            }

            DataSetEl aux = dataset[i + 1];
            dataset[i + 1] = dataset[max];
            dataset[max] = aux;

            return i + 1;
        }

        static void quickSort(ref List<DataSetEl> dataset, int minor, int max)
        {
            if (minor < max)
            {
                int pi = partition(dataset, minor, max);

                quickSort(ref dataset, minor, pi - 1);
                quickSort(ref dataset, pi + 1, max);
            }
        }

        //Selection Sort
        static List<DataSetEl> selectionSort(List<DataSetEl> dataset)
        {
            int min;
            long aux;

            for (int i = 0; i < dataset.Count - 1; i++)
            {
                min = i;

                for (int j = i + 1; j < dataset.Count; j++)
                {
                    if (dataset[j].room_id < dataset[min].room_id)
                    {
                        min = j;
                    }
                }

                if (min != i)
                {
                    aux = dataset[min].room_id;
                    dataset[min].room_id = dataset[i].room_id;
                    dataset[i].room_id = aux;
                }
            }

            return dataset;
        }

        static List<DataSetEl> mySort(List<DataSetEl> dataset)
        {
            int partition = numOfLines / 2;
            int min;
            long aux;
            int i, j = 0;


            //partition A
            for (i = 0; i < partition - 1; i++)
            {
                min = i;

                for (j = i + 1; j < dataset.Count; j++)
                {
                    if (dataset[j].room_id < dataset[min].room_id)
                    {
                        min = j;
                    }
                }

                if (min != i)
                {
                    aux = dataset[min].room_id;
                    dataset[min].room_id = dataset[i].room_id;
                    dataset[i].room_id = aux;
                }
            }

            //partition B
            for (i = partition; i < partition - 1; i++)
            {
                min = i;

                for (j = i + 1; j < dataset.Count; j++)
                {
                    if (dataset[j].room_id < dataset[min].room_id)
                    {
                        min = j;
                    }
                }

                if (min != i)
                {
                    aux = dataset[min].room_id;
                    dataset[min].room_id = dataset[i].room_id;
                    dataset[i].room_id = aux;
                }
            }

            i = 0;
            j = partition;
            int pos = 0;
            while (i < partition && j < dataset.Count)
            {
                if (dataset[j].room_id < dataset[i].room_id)
                {
                    dataset[pos].room_id = dataset[j].room_id;
                    j++;
                }
                else
                {
                    dataset[pos].room_id = dataset[i].room_id;
                    i++;
                }
                pos++;
            }

            return dataset;
        }
        //MergeSort
        static List<DataSetEl> mergeSort(List<DataSetEl> dataset)
        {

            if (dataset.Count <= 1)
                return dataset;

            List<DataSetEl> left = new List<DataSetEl>();
            List<DataSetEl> right = new List<DataSetEl>();

            int middle = dataset.Count / 2;
            for (int i = 0; i < middle; i++)
            {
                left.Add(dataset[i]);
            }
            for (int i = middle; i < dataset.Count; i++)
            {
                right.Add(dataset[i]);
            }

            left = mergeSort(left);
            right = mergeSort(right);
            return Merge(left, right);
        }
        static List<DataSetEl> Merge(List<DataSetEl> left, List<DataSetEl> right)
        {
            List<DataSetEl> result = new List<DataSetEl>();

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left[0].room_id <= right[0].room_id)
                    {
                        result.Add(left[0]);
                        left.Remove(left[0]);
                    }
                    else
                    {
                        result.Add(right[0]);
                        right.Remove(right[0]);
                    }
                }
                else if (left.Count > 0)
                {
                    result.Add(left[0]);
                    left.Remove(left[0]);
                }
                else if (right.Count > 0)
                {
                    result.Add(right[0]);

                    right.Remove(right[0]);
                }
            }
            return result;
        }

        // insertion sort
        static List<DataSetEl> insertionSort(List<DataSetEl> dataset)
        {
            int i, j;
            long aux;
            for (i = 1; i < dataset.Count; i++)
            {
                aux = dataset[i].room_id;
                j = i - 1;
                while (j >= 0 && dataset[j].room_id > aux)
                {
                    dataset[j + 1].room_id = dataset[j].room_id;
                    j--;
                }
                dataset[j + 1].room_id = aux;
            }
            return dataset;
        }

    }

    class DataSetEl
    {
        public long room_id { get; set; }
        public long host_id { get; set; }
        public String room_type { get; set; }
        public String country { get; set; }
        public String city { get; set; }
        public String neighborhood { get; set; }
        public String reviews { get; set; }
        public String overall_satisfaction { get; set; }
        public String accommodates { get; set; }
        public String bedrooms { get; set; }
        public String price { get; set; }
        public String property_type { get; set; }
    }
}