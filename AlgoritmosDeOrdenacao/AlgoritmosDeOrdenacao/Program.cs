using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
namespace AlgoritmosDeOrdenacao
{

    // TODO fazer os algoritmos de ordenação em ordem decrescente (pronto)
    // TODO fazer os casos de teste com valores estáticos (pronto)
    // TODO criar um documento de output com os valores ordenados (pronto)
    // TODO fazer os gráficos
    class Program
    {
        static Random rand = new Random();
        static int numOfLines = 2000;
        static void Main(string[] args)
        {
            // Lê arquivo e mapeia colunas para a lista de objetos
            List<DataSetEl> dataSetArray = new List<DataSetEl>();
            readFileContent(ref dataSetArray);

            // Inicia o algoritmo de bubble sort
            Stopwatch stopWatch = new Stopwatch();
            Boolean flag = true;
            DateTime localDate = DateTime.Now;

            if (flag)
            {
                stopWatch.Start();
                bubbleSort(dataSetArray);
                stopWatch.Stop();
                printArray(dataSetArray, stopWatch.ElapsedMilliseconds, "BubbleSort - " + numOfLines + " lines" + " - " + localDate.Millisecond);
                stopWatch.Reset();
            }
            else
            {
                stopWatch.Start();
                quickSort(ref dataSetArray, 0, dataSetArray.Count - 1);
                stopWatch.Stop();
                TimeSpan ts2 = stopWatch.Elapsed;
                printArray(dataSetArray, stopWatch.ElapsedMilliseconds, "QuickSort - " + numOfLines + " lines" + " - " + localDate.Millisecond);
            }

            Console.WriteLine("Fim das ordenações, pressione qualquer tecla para encerrar");
        }

        static List<DataSetEl> insertionSort(List<DataSetEl> dataset)
        {
            int i, j;
            long aux;
            for (i = 1; i < dataset.Count; i++)
            {
                aux = dataset[i].RoomId;
                j = i - 1;
                while (j >= 0 && dataset[j].RoomId > aux)
                {
                    dataset[j + 1].RoomId = dataset[j].RoomId;
                    j--;
                }
                dataset[j + 1].RoomId = aux;
            }
            return dataset;
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
                    if(counter > 0)
                    {
                        string[] lineSplited = line.Split("\t");
                        DataSetEl aux = new DataSetEl();
                        aux.RoomId = long.Parse(lineSplited[0]);
                        aux.HostId = long.Parse(lineSplited[1]);
                        aux.RoomType = lineSplited[2];
                        aux.Country = lineSplited[3];
                        aux.City = lineSplited[4];
                        aux.Neighborhood = lineSplited[5];
                        aux.Reviews = lineSplited[6];
                        aux.OverallSatisfaction = lineSplited[7];
                        aux.Accommodates = lineSplited[8];
                        aux.Bedrooms = lineSplited[9];
                        aux.Price = lineSplited[10];
                        aux.PropertyType = lineSplited[11];

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

        static void generateRandomArray(ref int[] array)
        {
            for (int i = 0; i < 10; i++)
            {
                array[i] = rand.Next(0, 10);
            }
        }
        static void printArray(List<DataSetEl> el, long time, String fileName)
        {
            TextWriter tw = new StreamWriter(fileName + ".txt");
            int count = 0;
            tw.WriteLine("Tempo gasto: " + time + " milisegundos para ordenar " + numOfLines + " elementos\n\n");
            while (count < numOfLines - 1)
            {
                tw.WriteLine(el[count].RoomId);
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
                    if (dataset[j].RoomId > dataset[j + 1].RoomId)
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
            long pivô = dataset[max].RoomId;

            int i = (minor - 1);
            for (int j = minor; j < max; j++)
            {
                if (dataset[j].RoomId < pivô)
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
    }

    class DataSetEl
    {
        public long RoomId { get; set; }
        public long HostId { get; set; }
        public String RoomType { get; set; }
        public String Country { get; set; }
        public String City { get; set; }
        public String Neighborhood { get; set; }
        public String Reviews { get; set; }
        public String OverallSatisfaction { get; set; }
        public String Accommodates { get; set; }
        public String Bedrooms { get; set; }
        public String Price { get; set; }
        public String PropertyType { get; set; }
    }
}