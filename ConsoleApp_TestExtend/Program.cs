using System;
using System.Collections.Generic;

namespace ConsoleApp_TestExtend {


    internal class Program {
        static void Main(string[] args) {
            var parents = new List<Parent>();
            //Test1(parents);
            //Test2(parents);
            //Test3(parents);
            var children = new List<Child>();
            Child Child = new Child();
            Child.Id = 1;
            Child.Name = "a";
            children.Add(Child);
            Test1(children);
            //Test2(children);
            Test3(children);
        }

        private static void Test1<T>(List<T> persons) where T : Parent {
            foreach (var person in persons) {
                Console.WriteLine(person.Id);

            }
        }
        private static void Test2(List<Parent> persons) {
        }
        private static void Test3(IEnumerable<Parent> persons) {
            foreach (var person in persons) {
                Console.WriteLine(person.Id);

            }

        }
    }
    public class Parent {
        public int Id { get; set; }
    }

    public class Child : Parent {


        public string Name { get; set; }

    }


}
