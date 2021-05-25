using System.IO.Ports;
using System.Runtime.InteropServices; // DllImport

// Библиотека для общения с сервером по последовательному порту.

namespace graph1
{
	partial class Graph
	{
		[DllImport("libserial.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static int com_open_port();

		[DllImport("libserial.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static void com_flush();

		[DllImport("libserial.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static void com_write(short data);

		[DllImport("libserial.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static void com_read(byte[] msg, int len);

		[DllImport("libserial.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static void com_read_line();

		[DllImport("libserial.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static void com_var_set(short var, int data, short cmd);


		SerialPort _serialport = new SerialPort();
		string _portname;

		/// <summary>
		/// Функция осуществляет подключение к серверу ардуино, по порту _portname.
		/// </summary>
		int TALKER_connect()
		{
			foreach (string s in SerialPort.GetPortNames())
			{
				LOG_Debug($"    {s}");
				_portname = s;
			}

			LOG("Соединение");

			//попытка открыть порт
			if (com_open_port() == -1)
			{
				LOG("**ОШИБКА** Подключите прибор!");
				return -1;
			}

			LOG_Debug("************");
			LOG_Debug("ПОДКЛЮЧЕНО");
			LOG_Debug($"ПОРТ: {_portname}");
			LOG_Debug($"BAUDRATE: {_serialport.BaudRate}");
			LOG_Debug("************");

			return 0;
		}

		/// <summary>
		/// Очистка входного порта.
		/// </summary>
		int TALKER_flush_read_buf()
		{
			com_flush();

			return 0;
		}

		/***************
		 * SEND
		 ***************/
		/// <summary>
		/// Отправка на сервер
		/// </summary>
		/// <param name="buf"></param>
		void TALKER_send(int buf)
		{
			com_write((short)buf);
		}

		void TALKER_sendln(byte[] buf)
		{
			for (int i = buf.Length; i > 0; i--)
			{
				com_write(buf[i]);
			}
		}

		/// <summary>
		/// Отправка команды на сервер
		/// </summary>
		/// <param name="var"></param>
		/// <param name="val"></param>
		void TALKER_set(int var, int val)
		{
			com_write((short)var);
			com_write((short)val);
			TALKER_read_line();
		}

		/***************
		 * READ
		 ***************/
		int TALKER_get()
		{
			byte[] buf = new byte[2];
			com_read(buf, 2);

			return buf[0] + (buf[1] << 8);
		}

		/// <summary>
		/// Читает строку из последовательного порта и
		/// выводит в консоль.
		/// </summary>
		/// <returns></returns>
		int TALKER_read_line()
		{
			com_read_line();

			return 0;
		}
	}
}
