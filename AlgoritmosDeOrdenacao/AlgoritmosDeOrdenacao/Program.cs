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
        static int numOfLines = 128000;
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