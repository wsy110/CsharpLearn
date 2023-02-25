namespace ConsoleApp_Task.Delay {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello, World!");
            btnDelayHaveAwait_Click();
            Console.ReadLine();
        }

        //https://www.cnblogs.com/dfcq/p/12685872.html
        private static void btnDelayNoAwait_Click() {
            Task task = Task.Factory.StartNew(() => {
                Console.WriteLine($" Task.Delay开始：{DateTime.Now.ToString(" HH:mm:ss.fff")}");
                for (int i = 0; i < 10; i++) {
                    Console.WriteLine($" {DateTime.Now.ToString("HH:mm:ss.fff")} 正在进行：{i}");
                    Task.Delay(2000);
                }
                Console.WriteLine($" Task.Delay结束：{DateTime.Now.ToString("HH:mm:ss.fff")}");
                Console.ReadKey();
            });

        }

        private static void btnDelayHaveAwait_Click() {
            Task task = Task.Factory.StartNew(async () => {
                Console.WriteLine($" Task.Delay 有await 开始：{DateTime.Now.ToString("HH:mm:ss.fff")}");
                for (int i = 0; i < 10; i++) {
                    Console.WriteLine($" {DateTime.Now.ToString("HH:mm:ss.fff")} 有await正在进行：{i} {Thread.CurrentThread.}");
                    await Task.Delay(2000);
                }
                Console.WriteLine($" Task.Delay 有await 结束：{DateTime.Now.ToString("HH:mm:ss.fff")}");
                Console.ReadKey();
            });
        }

        private static void btnThreadSleep_Click() {
            Task task = Task.Factory.StartNew(() => {
                Console.WriteLine($" Task.Delay 有await 开始：{DateTime.Now.ToString("HH:mm:ss.fff")}");
                for (int i = 0; i < 10; i++) {
                    Console.WriteLine($" {DateTime.Now.ToString("HH:mm:ss.fff")} 有await正在进行：{i}");
                    Thread.Sleep(2000);
                }
                Console.WriteLine($" Task.Delay 有await 结束：{DateTime.Now.ToString("HH:mm:ss.fff")}");
                Console.ReadKey();
            });
        }
    }
}

