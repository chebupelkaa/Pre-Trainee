using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            var syncStopwatch = Stopwatch.StartNew();

            string result1 = ProcessData("Файл 1");
            Console.WriteLine(result1);

            string result2 = ProcessData("Файл 2");
            Console.WriteLine(result2);

            string result3 = ProcessData("Файл 3");
            Console.WriteLine(result3);

            syncStopwatch.Stop();
            Console.WriteLine($"Общее время синхронной обработки: {syncStopwatch.Elapsed.TotalSeconds:F2} секунд\n");

            var asyncStopwatch = Stopwatch.StartNew();

            var firstDataSet = new List<string> { "Файл 1", "Файл 2", "Файл 3" };
            RunAsyncProcessing(firstDataSet).Wait();

            asyncStopwatch.Stop();
            Console.WriteLine($"Общее время асинхронной обработки: {asyncStopwatch.Elapsed.TotalSeconds:F2} секунд");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
        static string ProcessData(string dataName)
        {
            Console.WriteLine($"Начало обработки {dataName}...");
            Thread.Sleep(3000);
            return $"Обработка '{dataName}' завершена за 3 секунды";
        }

        static async Task<string> ProcessDataAsync(string dataName)
        {
            Console.WriteLine($"Начало асинхронной обработки {dataName}...");
            await Task.Delay(3000); 
            return $"Обработка '{dataName}' завершена за 3 секунды ";
        }

        static async Task RunAsyncProcessing(IEnumerable<string> dataSet)
        {
            var tasks = new List<Task<string>>();

            foreach (var dataName in dataSet)
            {
                tasks.Add(ProcessDataAsync(dataName));
            }

            Console.WriteLine("Ожидание завершения по мере готовности:\n");

            var remainingTasks = new List<Task<string>>(tasks);

            while (remainingTasks.Count > 0)
            {
                Task<string> completedTask = await Task.WhenAny(remainingTasks);
                remainingTasks.Remove(completedTask);
                string result = await completedTask;
                Console.WriteLine($"{result}");
            }
        }
    }
}
