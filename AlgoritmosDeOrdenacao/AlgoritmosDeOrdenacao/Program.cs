using System;

namespace AlgoritmosDeOrdenacao
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            int[] array = new int[10];

            for (int i = 0; i < 10; i++)
            {
                array[i] = rand.Next(0, 10);
            }

            Console.WriteLine("Array sem alteração");
            printArray(array);
            //bubbleSort(array);
            quickSort(ref array, 0, array.Length - 1);
            Console.WriteLine("Resultado da ordenação");
            printArray(array);
            Console.ReadKey();
        }
        static void printArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }

        // bubble sort
        static void bubbleSort(int[] array)
        {
            int i, j;
            for (i = 0; i < array.Length - 1; i++)
            {
                for (j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("Resultado da ordenação");
            printArray(array);
        }

        // quick sort
        static int partition(int[] array, int minor, int max)
        {
            int pivô = array[max];

            int i = (minor - 1);
            for (int j = minor; j < max; j++)
            {
                if (array[j] < pivô)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            int aux = array[i + 1];
            array[i + 1] = array[max];
            array[max] = aux;

            return i + 1;
        }

        static void quickSort(ref int[] array, int minor, int max)
        {
            if (minor < max)
            {
                int pi = partition(array, minor, max);

                quickSort(ref array, minor, pi - 1);
                quickSort(ref array, pi + 1, max);
            }

        }
    }
}