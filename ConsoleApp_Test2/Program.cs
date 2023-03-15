namespace ConsoleApp_Test2 {
    internal class Program {
        static void Main(string[] args) {
            var empList = new List<Employee>
          {
            new Employee {ID = 1, FName = "John", Age = 23, Sex = 'M'},
            new Employee {ID = 2, FName = "Mary", Age = 25, Sex = 'F'},
            new Employee {ID = 3, FName = "Amber", Age = 23, Sex = 'M'},
            new Employee {ID = 4, FName = "Kathy", Age = 25, Sex = 'F'},
            new Employee {ID = 5, FName = "Lena", Age = 27, Sex = 'F'},
            new Employee {ID = 6, FName = "Bill", Age = 28, Sex = 'M'},
            new Employee {ID = 7, FName = "Celina", Age = 27, Sex = 'F'},
            new Employee {ID = 8, FName = "John", Age = 28, Sex = 'M'}
         };

            // query with lamda expression g.Key代表Age和Sex两字段，Count为新增字段<br>        // 最终返回的集合QueryWithLamda包含字段：Age、Sex、Count
            var QueryWithLamda = empList.GroupBy(x => new { x.Age, x.Sex })
                        .Select(g => new { g.Key, Count = g.Count() });

            //query with standard expression<br>        //返回结果同上
            var query = from el in empList
                        group el by new { el.Age, el.Sex } into g
                        select new { g.Key, Count = g.Count() };

            /*foreach (var employee in QueryWithLamda *//* Or  QueryWithLamda *//* ) {
                Console.WriteLine(employee.Count);
                Console.WriteLine(employee.Key);
            }*/
            var QueryWithLamda1 = empList.GroupBy(x => new { x.Age, x.Sex }).ToDictionary(x => x.Key, x => {
                return x.OrderByDescending(x => x.ID).Take(1).ToList();
            });
            foreach (var employee in QueryWithLamda1 /* Or  QueryWithLamda */ ) {
                Console.WriteLine(employee.Key);
                Console.WriteLine(employee.Value.First().ID);
            }

            //{ Age = 23, Sex = M }
            //            3
            //{ Age = 25, Sex = F }
            //            4
            //{ Age = 27, Sex = F }
            //            7
            //{ Age = 28, Sex = M }
            //            8
        }


    }

    public class Employee {
        public int ID { get; set; }
        public string FName { get; set; }
        public int Age { get; set; }
        public char Sex { get; set; }
    }
}