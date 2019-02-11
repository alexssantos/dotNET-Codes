using System;

namespace Threads
{
	public class Work1
	{
		public void DoWork()
		{
			for (int i = 1; i < 1001; i++)
				Console.WriteLine($"Thread 1 -> Iteração {i}");
		}
	}
}
