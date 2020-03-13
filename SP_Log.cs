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
		/// <summary>
		/// Элемент типа Control для вывода сообщений
		/// </summary>
		public static TextBox console;

		/// <summary>
		/// Вывод сообщений в системную консоль и консоль приложения
		/// </summary>
		/// <param name="str"></param>
		public static void Log(string str)
		{
			console.AppendText(str + "\r\n");
			Console.Write(str + "\n");
		}

		/// <summary>
		/// Вывод сообщений только в системную консоль
		/// </summary>
		/// <param name="str"></param>
		public static void Debug(string str)
		{
			Console.Write(str + "\n");
		}

		/// <summary>
		/// Вывод статуса измерения
		/// </summary>
		/// <param name="str"></param>
		public static void Status(string str)
		{
			status = str;
			Console.Write(str + "\r");
		}

		/// <summary>
		/// Вывод сообщения из паралельного потока
		/// </summary>
		/// <param name="str"></param>
		public static void External_message(string str)
		{
			external = str;
			SP_Flags.external_message_flag = true;
		}
	}
}
