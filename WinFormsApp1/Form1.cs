namespace WinFormsApp1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            Task task = Task.Factory.StartNew(() => {
                Console.WriteLine($" Task.Delay��ʼ��{DateTime.Now.ToString(" HH:mm:ss.fff")}");
                for (int i = 0; i < 10; i++) {
                    Console.WriteLine($" {DateTime.Now.ToString("HH:mm:ss.fff")} ���ڽ��У�{i}");
                    Task.Delay(2000);
                }
                Console.WriteLine($" Task.Delay������{DateTime.Now.ToString("HH:mm:ss.fff")}");
                //Console.ReadKey();
            });
        }

        private void button2_Click(object sender, EventArgs e) {
            Task task = Task.Factory.StartNew(async () => {
                Console.WriteLine($" Task.Delay ��await ��ʼ��{DateTime.Now.ToString("HH:mm:ss.fff")}");
                for (int i = 0; i < 10; i++) {
                    Console.WriteLine($" {DateTime.Now.ToString("HH:mm:ss.fff")} ��await���ڽ��У�{i}");
                    await Task.Delay(2000);
                }
                Console.WriteLine($" Task.Delay ��await ������{DateTime.Now.ToString("HH:mm:ss.fff")}");
                //Console.ReadKey();
            });
        }

        private void button3_Click(object sender, EventArgs e) {
            Task task = Task.Factory.StartNew(() => {
                Console.WriteLine($" Task.Delay ��await ��ʼ��{DateTime.Now.ToString("HH:mm:ss.fff")}");
                for (int i = 0; i < 10; i++) {
                    Console.WriteLine($" {DateTime.Now.ToString("HH:mm:ss.fff")} ��await���ڽ��У�{i}");
                    Thread.Sleep(2000);
                }
                Console.WriteLine($" Task.Delay ��await ������{DateTime.Now.ToString("HH:mm:ss.fff")}");
                //Console.ReadKey();
            });
        }
    }
}