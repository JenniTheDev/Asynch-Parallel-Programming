using System;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronous {
    internal class Program {

        private static void Main(string[] args) {
            Console.WriteLine($"Sync");
            CalculateSync();

            Console.WriteLine($"Async");
            CalculateAsync();
            // Console.Read(); // put this here to wait for one, if not exits

            Console.WriteLine($"Async more complicated");
            CalcWithMoreAsync();
        }

        private static void CalculateSync() { // runs synchronous in order
            CalculateOne();
            CalculateTwo();
            CalculateThree();
        }

        private static void CalculateAsync() {
            Task.Run(() => {
                CalculateOne();
            });
            Task.Run(() => {
                CalculateTwo();
            });
            Task.Run(() => {
                CalculateThree();
            });
        }

        private static void CalcWithMoreAsync() {
            var taskOne = Task.Run(() => { return CalculateOne(); });
            var taskTwo = Task.Run(() => { return CalculateTwo(); });
            Task.WaitAll(taskOne, taskTwo);

            var awaiterOne = taskOne.GetAwaiter();
            var awaiterTwo = taskTwo.GetAwaiter();
            var resultOne = awaiterOne.GetResult();
            var resultTwo = awaiterTwo.GetResult();
            var result = CalculateFour(resultOne, resultTwo);
        }

        private static int CalculateFour(int result1, int result2) {
            Console.WriteLine($"Calculating Result of one and two");
            return result1 + result2;
        }

        private static int CalculateOne() {
            Thread.Sleep(3000);
            Console.WriteLine($"Calculating result one");
            return 100;
        }

        private static int CalculateTwo() {
            Console.WriteLine($"Calculating result two");
            return 200;
        }

        private static int CalculateThree() {
            Console.WriteLine($"Calculating result three");
            return 300;
        }
    }
}