﻿using System;

namespace graph1
{
	partial class Graph
	{
		/// <summary>
		/// Вывод сообщений в системную консоль и консоль приложения
		/// </summary>
		/// <param name="str"></param>
		void LOG(string str)
		{
			Text_console.AppendText(str + "\r\n");
			Console.Write(str + "\n");
		}

		/// <summary>
		/// Вывод сообщений только в системную консоль
		/// </summary>
		/// <param name="str"></param>
		void LOG_Debug(string str)
		{
			Console.Write(str + "\n");
		}

		/// <summary>
		/// Вывод статуса измерения
		/// </summary>
		/// <param name="str"></param>
		void LOG_Status(string str)
		{
			Mesure_stat_label.Text = str;
			Console.Write(str + "\r");
		}
	}
}