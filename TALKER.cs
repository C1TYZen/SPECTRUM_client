using System;
using System.IO.Ports;
//using System.Threading;

// Библиотека для общения с сервером по последовательному порту.

namespace graph1
{
	partial class Graph
	{
		SerialPort _serialPort = new SerialPort();
		int _baudrate;
		string _portname;

		// Переменные для хранения сообщений
		byte[] bmsg = new byte[4];
		int imsg;

		/// <summary>
		/// Открывает порт с указанным именем и соростью.
		/// Устанавливает дефолтные таймауты и очищает буферы.
		/// </summary>
		int TALKER_open()
		{
			if (_portname != null)
				_serialPort.PortName = _portname;
			else
			{
				LOG_Debug("**ОШИБКА** Выберите имя порта");
				return -1;
			}
			_serialPort.BaudRate = _baudrate;
			_serialPort.ReadTimeout = 5000;
			_serialPort.WriteTimeout = 5000;
			_serialPort.Parity = Parity.None;
			_serialPort.DataBits = 8;
			_serialPort.StopBits = StopBits.One;
			_serialPort.Handshake = Handshake.None;

			//https://stackoverflow.com/questions/21562055/serial-port-not-receiving-any-data
			//Параметр должен иметь значение true
			_serialPort.RtsEnable = true;

			try { _serialPort.Open(); }
			catch(Exception ex)
			{
				LOG_Debug($"**ERROR** in <<open()>> {ex}");
				return -1;
			}

			_serialPort.DiscardInBuffer();
			_serialPort.DiscardOutBuffer();

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

			/*while (attempt <= 5)
			{
				//прочитать строку проверки связи
				attempt++;
				TALKER_send(CMD_CC);
				if (TALKER_read_line() == 0)
				{
					LOG_Debug($"Попыток подключения: {attempt}");
					break;
				}
			}*/

			if (attempt > 3)
			{
				LOG("**ОШИБКА** Не могу подключиться");
				return -1;
			}

			LOG_Debug("************");
			LOG_Debug("ПОДКЛЮЧЕНО");
			LOG_Debug($"ПОРТ: {_portname}");
			LOG_Debug($"BAUDRATE: {_baudrate}");
			LOG_Debug("************");

			return 0;
		}

		/// <summary>
		/// Очистка входного порта.
		/// </summary>
		int TALKER_FlushReadBuf()
		{
			try { _serialPort.DiscardInBuffer(); }
			catch (Exception ex)
			{
				LOG("**ОШИБКА** Прибор отключен. Подключите прибор и перезагрузите программу.");
				LOG_Debug($"**ОШИБКА** в функции <<flush()>> {ex}");
				return -1;
			}
			string dummy = _serialPort.ReadExisting();
			LOG_Debug($"Мусор: {dummy}");
			return 0;
		}

		/************************
		 * SEND
		 ************************/

		/// <summary>
		/// Отправляет в последовательный порт количество байт count
		/// из массива msg с отступом offset.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		void TALKER_write(byte[] msg, int offset, int count)
		{
			try { _serialPort.Write(msg, offset, count); }
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
		/// <param name="cmd"></param>
		/// <param name="val"></param>
		void TALKER_set(int cmd, int val)
		{
			TALKER_send(cmd);
			TALKER_send(val);
			TALKER_read_line();
		}

		/************************
		 * READ
		 ************************/

		/// <summary>
		/// Читает из последовательного порта количество байт count
		/// в массив msg с отступом offset.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		void TALKER_read(byte[] msg, int offset, int count)
		{
			try { _serialPort.Read(msg, offset, count); }
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
			try { LOG_Debug(_serialPort.ReadLine()); }
			catch
			{
				LOG_Debug($"**ERROR** in <<TALKER_read_line()>>");
				return -1;
			}

			return 0;
		}
	}
}
