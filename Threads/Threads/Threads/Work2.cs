using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
	public class Work2
	{
		public void DoWork()
		{
			for (int i = 1; i < 1001; i++)
				Console.WriteLine($"Thread 2 -> Iteração {i}");
		}
	}
}
