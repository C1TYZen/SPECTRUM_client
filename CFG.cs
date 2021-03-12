using System;
using System.IO;

namespace graph1
{
	partial class Graph
	{
		struct cvar
		{
			public string name;
			public int value;

			public cvar(string str, int val)
			{
				name = str;
				value = val;
			}
		}

		string line;
		string[] comps;

		void CFG_get_config(cvar[] vars)
		{
			for (int i = 0; i < 64; i++)
			{
				using(StreamReader reader = new StreamReader("out.txt"))
				{
					line = reader.ReadLine();
					if (line == null)
						break;
				}
				
				comps = line.Split(' ');
				if (comps.Length != 2)
					continue;

				for (int ctr = 0; ctr < vars.Length; ctr++)
				{
					if (comps[0].CompareTo(vars[ctr].name) == 0)
					{
						vars[ctr].value = Int32.Parse(comps[1]);
					}
				}
			}
		}

		void CFG_set_value(cvar[] vars, string name, ref int var)
		{
			for (int ctr = 0; ctr < vars.Length; ctr++)
			{
				if (vars[ctr].name.CompareTo(name) == 0)
				{
					var = vars[ctr].value;
				}
			}
		}
	}
}