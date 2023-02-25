using System;
using System.Diagnostics;
using System.Reflection;

namespace ConsoleApp {
    internal class Program {
        static async Task Main(string[] args) {
            Console.WriteLine("Hello, World!");
            //Parallel.For demo
            /*Parallel.For(1, 10, i => Console.WriteLine(i));
            Console.WriteLine();*/

            //Parallel.ForEach demo
            /* var all = new[] { 1, 2, 3, 4, 5 };
             Parallel.ForEach(all, i => Console.WriteLine(i));*/

            //Parallel.For耗时 demo
            /* Stopwatch sw = Stopwatch.StartNew();
             sw.Start();
             Parallel.For(1, 3, cal);//1,2
             sw.Stop();
             Console.WriteLine(sw.ElapsedMilliseconds);*/

            //Parallel.ForEach 不安全 demo
            /*var sum = SumFac(4, 5, 6, 7);
            Console.WriteLine(sum);*/
            //Parallel.ForEach 安全 demo
            /* var sum = SumFac1(4, 5, 6, 7);
             Console.WriteLine(sum);*/




            //Semaphore demo
            //SemaphoreTest();

            //Parallel ForEachAsync demo https://blog.csdn.net/sD7O95O/article/details/117914853
            //await NewMethod();
            //await NewMethod1();
        }
        /*
         * 来看一个实际的示例吧，多个任务并行执行，通常我们会使用 Task.WhenAll 来并行多个 Task 的执行，但是 Task.WhenAll 不能限制并发度，通常我们是会在异步 task 上封装一层，使用信号量来限制并发，示例如下：
         */
        private static async Task NewMethod() {
            var semaphore = new SemaphoreSlim(10);
            await Task.WhenAll(Enumerable.Range(1, 100).Select(async index => {
                try {
                    await semaphore.WaitAsync();
                    await Task.Delay(1000);
                    Console.WriteLine($"第{index}个人正在过桥。");
                } finally {
                    Console.WriteLine($"第{index}个人已经过桥。");
                    semaphore.Release();
                }
                Console.WriteLine($"--------");
            }));
        }

        private static async Task NewMethod1() {
            await Parallel.ForEachAsync(Enumerable.Range(1, 100), new ParallelOptions() {
                MaxDegreeOfParallelism = 10
            }, async (index_, index) => {
                Console.WriteLine($"第{index_}个人正在过桥。");
                await Task.Delay(1000);
                Console.WriteLine($"第{index_}个人已经过桥。");
            });
        }

        static void cal(int n) {
            Thread.Sleep(n * 1000);
        }

        static int calfac(int n) {
            return n < 2 ? n : n * calfac(n - 1);
        }
        //Parallel.ForEach 不安全
        static int SumFac(params int[] data) {
            int sum = 0;
            Parallel.ForEach(data, n => {
                sum += calfac(n);
            });
            return sum;
        }

        static int SumFac1(params int[] data) {
            int sum = 0;
            Parallel.ForEach(data, n => {
                Interlocked.Add(ref sum, calfac(n));
            });
            return sum;
        }



        public static void SemaphoreTest() {
            var semaphore = new SemaphoreSlim(5);
            for (int i = 1; i <= 10; i++) {
                Thread.Sleep(100); // 排队上桥
                var index = i; // 定义index 避免出现闭包的问题
                Task.Run(() => {
                    semaphore.Wait();
                    try {
                        Console.WriteLine($"第{index}个人正在过桥。");
                        Thread.Sleep(5000); // 模拟过桥需要花费的时间
                    } finally {
                        Console.WriteLine($"第{index}个人已经过桥。");
                        semaphore.Release();
                    }
                });
            }
        }
    }
}