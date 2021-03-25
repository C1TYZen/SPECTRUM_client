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

		//Конфиг
		cvar[] cfg =
		{
			new cvar("mesure_start", 0),
			new cvar("mesure_end", 100),
			new cvar("mesure_count", 1),

			new cvar("driver_speed", 700),

			new cvar("filter_num", 1),
			new cvar("filter_step", 0),

			new cvar("amp", 1),

			new cvar("port_baudrate", 38400),
			new cvar("port_read_timeout", 5000),
			new cvar("port_write_timeout", 5000),
			new cvar("port_parity", 0),
			new cvar("port_data_bits", 8),
			new cvar("port_stop_bits", 1),
			new cvar("port_handshake", 0),

			new cvar("spectrum_points", 524288),
			new cvar("spectrum_count", 64),

			new cvar("draw_resolution", 1),

			new cvar("timer_tick_interval", 1)
		};

		void CFG_get_config(cvar[] vars)
		{
			string line;
			string[] comps;

			string cfg_path = "spectrum.cfg";
			FileInfo cfg_file = new FileInfo(cfg_path);
			if (!cfg_file.Exists)
			{
				LOG("Отсутствует конфигурационный файл!");
				return;
			}

			using (StreamReader reader = new StreamReader(cfg_path))
			{
				for (int i = 0; i < 64; i++)
				{
					line = reader.ReadLine();
					if (line == null)
						break;

					comps = line.Split(' ');
					if (comps.Length != 2)
						continue;

					for (int ctr = 0; ctr < vars.Length; ctr++)
					{
						if (comps[0].CompareTo(vars[ctr].name) == 0)
						{
							LOG_Debug($"{vars[ctr].name} = {vars[ctr].value}");
							vars[ctr].value = Int32.Parse(comps[1]);
							break;
						}
					}
				}
			}
			LOG_Debug("");
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

		int CFG_get_value(cvar[] vars, string name)
		{
			for (int ctr = 0; ctr < vars.Length; ctr++)
			{
				if (vars[ctr].name.CompareTo(name) == 0)
				{
					return vars[ctr].value;
				}
			}
			return -1;
		}
	}
}