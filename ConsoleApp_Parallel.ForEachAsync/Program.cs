using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ConsoleApp_Parallel.ForEachAsync {
    internal class Program {
        //https://blog.csdn.net/sD7O95O/article/details/117914853
        static async Task Main(string[] args) {
            var watch = Stopwatch.StartNew();
            await Task.WhenAll(Enumerable.Range(1, 100).Select(_ => Task.Delay(1000)));
            watch.Stop();
            Console.WriteLine("WhenAll " + watch.ElapsedMilliseconds);

            watch.Restart();
            using var semaphore = new SemaphoreSlim(10, 10);
            await Task.WhenAll(Enumerable.Range(1, 100).Select(async _ => {
                try {
                    await semaphore.WaitAsync();
                    await Task.Delay(1000);
                } finally {
                    semaphore.Release();
                }
            }));
            watch.Stop();
            Console.WriteLine("SemaphoreSlim " + watch.ElapsedMilliseconds);


            Console.WriteLine($"{nameof(Environment.ProcessorCount)}: {Environment.ProcessorCount}");
            watch.Restart();
            await Parallel.ForEachAsync(Enumerable.Range(1, 100), async (_, _) => await Task.Delay(1000));
            watch.Stop();
            Console.WriteLine("Parallel.ForEachAsync " + watch.ElapsedMilliseconds);

            watch.Restart();
            await Parallel.ForEachAsync(Enumerable.Range(1, 100), new ParallelOptions() {
                MaxDegreeOfParallelism = 10
            }, async (_, _) => await Task.Delay(1000));
            watch.Stop();
            Console.WriteLine("Parallel.ForEachAsync ParallelOptions 10 " + watch.ElapsedMilliseconds);

            watch.Restart();
            await Parallel.ForEachAsync(Enumerable.Range(1, 100), new ParallelOptions() {
                MaxDegreeOfParallelism = 100
            }, async (_, _) => await Task.Delay(1000));
            watch.Stop();
            Console.WriteLine("Parallel.ForEachAsync ParallelOptions 100 " + watch.ElapsedMilliseconds);

            //默认情况下，Parallel.ForEachAsync 的最大并行度是当前机器的 CPU 数量，也就是 Environment.ProcessorCount，如果要不限制可以指定最大并行度为 int.MaxValue
            watch.Restart();
            await Parallel.ForEachAsync(Enumerable.Range(1, 100), new ParallelOptions() {
                MaxDegreeOfParallelism = int.MaxValue
            }, async (_, _) => await Task.Delay(1000));
            watch.Stop();
            Console.WriteLine("Parallel.ForEachAsync ParallelOptions MaxValue " + watch.ElapsedMilliseconds);
        }

        //WhenAll 1038
        //SemaphoreSlim 10127
        //ProcessorCount: 8
        //Parallel.ForEachAsync 13178
        //Parallel.ForEachAsync ParallelOptions 10 10115
        //Parallel.ForEachAsync ParallelOptions 100 1043
        //Parallel.ForEachAsync ParallelOptions MaxValue 1075
    }
}