using System;
using System.Windows.Forms;

namespace graph1
{
	/// <summary>
	/// Класс Log для записи логов в консоль системы,
	/// консоль приложения и файлы
	/// </summary>
	public static class SP_Log
	{
		public static string external;
		public static string mesure_status;
		public static string status;
		public static TextBox console;

		public static void Log(string str)
		{
			console.AppendText(str + "\r\n");
			Console.Write(str + "\n");
		}

		public static void Debug(string str)
		{
			Console.Write(str + "\n");
		}

		public static void Status(string str)
		{
			status = str;
			Console.Write(str + "\r");
		}

		public static void External_message(string str)
		{
			external = str;
			SP_Flags.external_message_flag = true;
		}
	}
}
