using System;
using System.Windows.Forms;

namespace graph1
{
	/// <summary>
	/// Класс Log для записи логов в консоль системы,
	/// консоль приложения и файлы
	/// </summary>
	static class SP_Log
	{
		public static string external;
		public static string status;
		// Элемент типа Control для вывода сообщений
		public static TextBox console;

		//вывод сообщений в системную консоль и консоль приложения
		public static void Log(string str)
		{
			console.AppendText(str + "\r\n");
			Console.Write(str + "\n");
		}

		//вывод сообщений только в системную консоль
		public static void Debug(string str)
		{
			Console.Write(str + "\n");
		}

		//вывод статуса измерения
		public static void Status(string str)
		{
			status = str;
			Console.Write(str + "\r");
		}

		//вывод сообщения из паралельного потока
		public static void External_message(string str)
		{
			external = str;
			SP_Flags.external_message_flag = true;
		}
	}
}
