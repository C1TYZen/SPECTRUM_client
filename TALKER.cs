using System;
using System.IO.Ports;
using System.Threading;

// Библиотека для общения с сервером по последовательному порту.

namespace graph1
{
	partial class Graph
	{
		SerialPort _serialPort = new SerialPort();
		int _baudrate;
		string _portname;

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
			_serialPort.ReadTimeout = 1000;
			_serialPort.WriteTimeout = 1000;

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

			while (attempt <= 5)
			{
				//прочитать строку проверки связи
				attempt++;
				Thread.Sleep(1000);
				TALKER_send2bytes(CMD_CC);
				if (TALKER_read_line() == 0)
				{
					LOG_Debug($"Попыток подключения: {attempt}");
					break;
				}
			}

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
		void TALKER_send(byte[] msg, int offset, int count)
		{
			try { _serialPort.Write(msg, offset, count); }
			catch(Exception ex)
			{
				LOG_Debug($"**ERROR** in <<TALKER_send()>>\n**{ex}**");
			}
		}

		/// <summary>
		/// Отправляет в последовательный порт 2 байта из
		/// переменной buf.
		/// </summary>
		/// <param name="buf"></param>

		/// <remarks>
		/// 2 байта последовательно отправляются на ардуино,
		/// где первый записываеся в переменную, а второй сдвигается
		/// на 8 бит и тоже записывается.
		/// </remarks>
		void TALKER_send2bytes(int buf)
		{
			bmsg[0] = (byte)buf;
			bmsg[1] = (byte)(buf >> 8);
			TALKER_send(bmsg, 0, 2);
		}

		/// <summary>
		/// Отправляет в последовательный порт 3 байта из
		/// переменной buf.
		/// </summary>
		/// <param name="buf"></param>
		 
		/// <remarks>
		/// В отличие от функции отправки 2х байт, здесь байты отправляются в обратном порядке.
		/// Ардуино принимает байты в 8-битный регистр последовательного
		/// порта, после чего эти данные можно записать в переменную.
		/// При приеме 2х байт первый байт записывается в переменную,
		/// а второй сперва сдвигается в регистре приема и только потом записывается.
		/// При попытке сдвига третьего байта - байт отбрасывается.
		/// Непонятно почему так происходит, так как регистр 8-битный.
		/// То есть в регистре при сдвиге должен отбрасываться и второй байт тоже.
		/// </remarks>
		void TALKER_send3bytes(int buf)
		{
			bmsg[0] = (byte)(buf >> 16);
			bmsg[1] = (byte)(buf >> 8);
			bmsg[2] = (byte)buf;
			TALKER_send(bmsg, 0, 3);
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
			catch (Exception ex)
			{
				LOG_Debug($"**ERROR** in <<TALKER_read()>>\n{ex}");
			}
		}

		int TALKER_read2bytes(byte[] msg)
		{
			TALKER_read(msg, 0, 2);
			return msg[0] + (msg[1] << 8);
		}

		/// <summary>
		/// Читает строку из последовательного порта и
		/// выводит в консоль.
		/// </summary>
		/// <returns></returns>
		int TALKER_read_line()
		{
			try { LOG_Debug(_serialPort.ReadLine()); }
			catch (Exception ex)
			{
				LOG_Debug($"**ERROR** in <<TALKER_read_line()>>\n **{ex}**");
				return -1;
			}

			return 0;
		}

		/// <summary>
		/// Очистка входного порта.
		/// </summary>
		void TALKER_FlushReadBuf()
		{
			_serialPort.DiscardInBuffer();
			string dummy = _serialPort.ReadExisting();
			LOG_Debug($"Мусор: {dummy}");
		}
	}
}
