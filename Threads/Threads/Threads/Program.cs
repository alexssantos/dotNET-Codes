using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
	class Program
	{
		static void Main(string[] args)
		{
			var work1 = new Work1();
			var work2 = new Work2();

			var thread1 = new Thread(() => work1.DoWork());
			thread1.Start();

			var thread2 = new Thread(() => work2.DoWork());
			thread2.Start();

			Console.ReadKey();
		}
	}
}
