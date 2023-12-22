using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;

namespace Paralell_Programming_Lesson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //------------Example 1-------------------
            var timer1 = new Stopwatch();
            timer1.Start();
            HeavyComputation("A");
            HeavyComputation("B");
            HeavyComputation("C");
            HeavyComputation("D");
            HeavyComputation("E");
            timer1.Stop();
            Console.WriteLine("All: " + timer1.ElapsedMilliseconds);
            Console.WriteLine("-----------------------------------");
            var timer2 = new Stopwatch();
            timer2.Start();
            Parallel.Invoke(
                () => HeavyComputation("A"),
                () => HeavyComputation("B"),
                () => HeavyComputation("C"),
                () => HeavyComputation("D"),
                () => HeavyComputation("E")
            );
            timer2.Stop();
            Console.WriteLine("All: " + timer2.ElapsedMilliseconds);
            //----------------------Example 1 Ended--------------------
            //-----------------------Example 2-------------------------
            List<int> collection = Enumerable.Range(0, 10).ToList();
            Console.WriteLine("Standard foreach loop");

            Stopwatch _watch = new Stopwatch();
            _watch.Start();

            foreach (var item in collection)
            {
                Console.WriteLine($" the value is {item} and Thread {Thread.CurrentThread.ManagedThreadId}");
                Thread.Sleep(10);
            }
            _watch.Stop();
            Console.WriteLine("All: " + _watch.ElapsedMilliseconds);
            Console.WriteLine("-------------------------");

            Console.WriteLine("Parallel foreach loop");
            Stopwatch _watch2 = new Stopwatch();
            _watch2.Start();

            Parallel.ForEach(collection, i =>
            {
                Console.WriteLine($" the value is {i} and Thread: {Thread.CurrentThread.ManagedThreadId}"); Thread.Sleep(10);
            });
            _watch2.Stop();

            Console.WriteLine($"Main Thread {Thread.CurrentThread.ManagedThreadId} Completed");
            Console.WriteLine("All: " + _watch2.ElapsedMilliseconds);
            Console.ReadLine();
            //------------------------Example 2 Ended---------------------------
        }
        public static int HeavyComputation(string name)
		{
			Console.WriteLine("Start: " + name);
			var timer = new Stopwatch();
			timer.Start();
			var result = 0;
			for (var i = 0; i < 10_000_000; i++)
			{
				var a = ((i + 1_500) / (i + 30)) * (i + 10);
				result += (a % 10) - 120;
			}
			timer.Stop();
			Console.WriteLine("End: " + name + ' ' + timer.ElapsedMilliseconds);
			return result;
		}

	}
}

