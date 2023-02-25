/*
 *
 *  学习Yield Return 语法
 *  使用两个方法，显示1 - 100之间的全部偶数
 * https://blog.csdn.net/qq_33060405/article/details/78484825
 * 
 */

namespace YieldReturn {
    class Program {
        static private List<int> _numArray; //用来保存1-100 这100个整数

        Program() //构造函数。我们可以通过这个构造函数往待测试集合中存入1-100这100个测试数据
        {
            _numArray = new List<int>(); //给集合变量开始在堆内存上开内存，并且把内存首地址交给这个_numArray变量

            for (int i = 1; i <= 10; i++) {
                _numArray.Add(i);  //把1到100保存在集合当中方便操作
            }
        }

        static void Main(string[] args) {
            // yield return
            new Program();
            TestMethod();

            // Enumerable.Range
            //EnumerableTest();
        }

        private static void EnumerableTest() {
            foreach (var i in Enumerable.Range(0, 10)) {
                Console.WriteLine(i);
            }
        }

        //测试求1到100之间的全部偶数
        static public void TestMethod() {
            foreach (var item in GetAllEvenNumber()) {
                Console.WriteLine(item); //输出偶数测试
            }
        }

        //测试我们正常情况下拿到全部偶数的方法
        static IEnumerable<int> GetAllEvenNumber() {
            List<int> result = new List<int>(); //开集合内存存偶数用

            foreach (int num in _numArray) {
                if (num % 2 == 0) //判断是不是偶数
                {
                    Thread.Sleep(1000);
                    yield return num;
                    //result.Add(num); //存入集合
                }
            }

            //返回偶数集合变量   可能有人会觉得奇怪返回类型不是List<int>这样可以吗
            //这个就要回到我们的里氏替换原则了，子类是可以替换父类的，也就是当父类用
            //比如我这个方法是想得到IEnumerable<int> 类型变量，但是我给了List<int>类型变量
            //注意List<int> 是继承 IEnumerable<int> 的，什么意思当我们把子类当父类使用，
            //那么大才小用，因为子类很多都是继承父亲，你自身增加很多字段或者方法，这样就不能用了。
            //return result;
            yield break;
        }


    }

    /*我们发现这个Yield Return是可以让当前函数的进程状态切换到阻塞状态，然后去选择了把cpu交给当前的出进程，这样就转而执行调用方函数。 （补充个小知识点其实我们写的程序加入到内存中，并不定就是一个进程，我们会根据情况分成几个子进程去干活，方便操作系统去管理以及多道程序运行在内存，提高计算机资源的利用率）
这样有个好处，我们假如有1000000个数据，我们需要得到里面的耦合，当我们通过这个方法得到一个耦合会立马显示在控制台上。而不是等很久也就把全部偶数都查找到存入集合当中，然后在一一遍历输出。
这个好处是很大的。比如我们用户可能就看数据开始肯定不是全部值需要部分就可以，看完这些在看后面的，这样数据会让觉得显示没有延迟。*/

}
