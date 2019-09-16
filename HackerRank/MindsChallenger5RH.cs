using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleAppCore2_2_teste
{
	class Program
	{
		public static string FileFullPath = @"D:\workspaces\home-alex\MindsHR5CaseTesteIn2.txt";
		public static int IDs_COUNT;
		public static int QUERY_MAX_COUNT;

		public static List<P> pList = new List<P>();    //index = id - 1

		public class P
		{
			public P lead { get; set; }
			public int id { get; set; }
			public int count { get; set; }

		}

		public static string[] ReadFileTeste()
		{
			string[] lines = File.ReadAllLines(FileFullPath);
			return lines;
		}
		public static string[] FirstRead()
		{
			return Console.ReadLine().Split(" ");

		}

		static void Main(string[] args)
		{
			//string[] lines = FirstRead();	// INPUT
			string[] lines = ReadFileTeste();   // FILE

			IDs_COUNT = int.Parse(lines[0].Split(" ")[0]);
			QUERY_MAX_COUNT = int.Parse(lines[0].Split(" ")[1]);

			for (int i = 0; i < IDs_COUNT; i++)
			{
				P pessoa = new P();
				pessoa.id = i + 1;
				pessoa.lead = new P();
				pessoa.lead.id = pessoa.id;
				pessoa.lead.count = 1;
				pList.Add(pessoa);
			}

			for (int i = 0; i < QUERY_MAX_COUNT; i++)
			{
				//string[] query = Console.ReadLine().Split(" ");	// INPUT
				string[] query = lines[i + 1].Split(" ");       // FILE

				string cmd = query[0];
				int idA = int.Parse(query[1]);

				if (cmd.Equals("M"))
				{
					int idB = int.Parse(query[2]);
					MergeIds(idA, idB);
				}
				if (cmd.Equals("Q"))
				{
					P pessoa = pList[idA - 1];
					Console.WriteLine(pessoa.lead.count);
				}

			}

		}

		public static void MergeIds(int IDA, int IDB)
		{
			int count = 0;
			for (int i = 0; i < pList.Count; i++)
			{
				if (pList[i].lead.id == pList[IDB - 1].lead.id)
				{
					pList[i].lead = pList[IDA - 1].lead;
					count += 1;
				}
			}
			pList[IDA - 1].lead.count += count;
		}
	}
}
