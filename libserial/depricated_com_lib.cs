using System;
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
		extern static short com_read();

		[DllImport("libserial.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static void com_read_line();

		[DllImport("libserial.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static void com_var_set(short var, int data, short cmd);


		SerialPort _serialport = new SerialPort();
		string _portname;

		/// <summary>
		/// Открывает порт с указанным именем и соростью.
		/// Устанавливает дефолтные таймауты и очищает буферы.
		/// </summary>
		int TALKER_open()
		{
			if (_portname != null)
				_serialport.PortName = _portname;
			else
			{
				LOG_Debug("**ОШИБКА** Выберите имя порта");
				return -1;
			}
			//Значения возможных скоростей указаны в документации.
			//Если указать другое значение - порт перестает работать.
			//Но не всегда...
			_serialport.BaudRate		= 38400;
			_serialport.ReadTimeout		= 1000;
			_serialport.WriteTimeout	= 1000;
			_serialport.Parity			= Parity.None;
			_serialport.DataBits		= 8;
			_serialport.StopBits		= StopBits.One;
			_serialport.Handshake		= Handshake.XOnXOff;

			//_serialport.ReadBufferSize	= 8192;
			//_serialport.WriteBufferSize	= 128;

			//https://stackoverflow.com/questions/21562055/serial-port-not-receiving-any-data
			//Параметр должен иметь значение true
			//_serialport.RtsEnable		= true;

			try { _serialport.Open(); }
			catch(Exception ex)
			{
				LOG_Debug($"**ERROR** in <<open()>> {ex}");
				return -1;
			}

			_serialport.DiscardInBuffer();
			_serialport.DiscardOutBuffer();

			return 0;
		}

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

			int attempt = 0;
			LOG("Соединение");

			//попытка открыть порт
			if (TALKER_open() == -1)
			{
				LOG("**ОШИБКА** Подключите прибор!");
				return -1;
			}

			if (attempt > 3)
			{
				LOG("**ОШИБКА** Не могу подключиться");
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
			try { _serialport.DiscardInBuffer(); }
			catch (Exception ex)
			{
				LOG("**ОШИБКА** Прибор отключен. Подключите прибор и перезагрузите программу.");
				LOG_Debug($"**ОШИБКА** в функции <<flush()>> {ex}");
				return -1;
			}
			string dummy = _serialport.ReadExisting();
			LOG_Debug($"Мусор: {dummy}");
			return 0;
		}

		/***************
		 * SEND
		 ***************/
		/// <summary>
		/// Отправляет в последовательный порт количество байт count
		/// из массива msg с отступом offset.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		void TALKER_write(byte[] msg, int offset, int count)
		{
			try { _serialport.Write(msg, offset, count); }
			catch(Exception ex)
			{
				LOG_Debug($"**ERROR** in <<TALKER_send()>>\n**{ex}**");
			}
		}

		/// <summary>
		/// Отправка на сервер
		/// </summary>
		/// <param name="buf"></param>
		void TALKER_send(int buf)
		{
			for (int i = 0; i < 2; i++)
			{
				bmsg[i] = (byte)(buf>>(8 * i));
			}
			TALKER_write(bmsg, 0, 2);
		}

		/// <summary>
		/// Отправка команды на сервер
		/// </summary>
		/// <param name="var"></param>
		/// <param name="val"></param>
		void TALKER_set(int var, int val)
		{
			TALKER_send(CMD_CS);
			TALKER_send(var);
			TALKER_send(val);
			TALKER_read_line();
		}

		/***************
		 * READ
		 ***************/
		/// <summary>
		/// Читает из последовательного порта количество байт count
		/// в массив msg с отступом offset.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		void TALKER_read(byte[] msg, int offset, int count)
		{
			try { _serialport.Read(msg, offset, count); }
			catch
			{
				LOG_Debug($"**ERROR** in <<TALKER_read()>>");
			}
		}

		/// <summary>
		/// Читает строку из последовательного порта и
		/// выводит в консоль.
		/// </summary>
		/// <returns></returns>
		int TALKER_read_line()
		{
			try { LOG_Debug(_serialport.ReadLine()); }
			catch
			{
				LOG_Debug($"**ERROR** in <<TALKER_read_line()>>");
				return -1;
			}

			return 0;
		}
	}
}
