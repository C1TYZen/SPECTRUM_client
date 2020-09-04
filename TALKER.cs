using System;
using System.IO.Ports;
using System.Threading;

// Библиотека для общения с ардуино по последовательному порту.

namespace graph1
{
	partial class Graph
	{
		SerialPort _serialPort = new SerialPort();
		bool Receive;
		int _baudrate;
		string _portname;
		byte[] bmsg = new byte[3];
		int imsg;

		/// <summary>
		/// Открывает порт с указанным именем и соростью.
		/// Устанавливает дефолтные таймауты и очищает буферы.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="speed"></param>
		/// <returns></returns>
		int TALKER_open()
		{
			if (_portname != null)
				_serialPort.PortName = _portname;
			else
			{
				LOG_Debug("Choose name of port");
				return -1;
			}
			_serialPort.BaudRate = _baudrate;
			_serialPort.ReadTimeout = 1000;
			_serialPort.WriteTimeout = 1000;

			try { _serialPort.Open(); }
			catch(Exception ex)
			{
				LOG_Debug($"ERROR in <<open()>> {ex}");
				return -1;
			}

			_serialPort.DiscardInBuffer();
			_serialPort.DiscardOutBuffer();

			return 0;
		}

		/// <summary>
		/// Функция осуществляет подключение к серверу ардуино, по порту _portname.
		/// </summary>
		void TALKER_connect()
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
				LOG("**ERROR** Plug in and restart!");
				return;
			}

			while (attempt <= 5)
			{
				//прочитать строку проверки связи
				attempt++;
				Thread.Sleep(1000);
				TALKER_send2bytes(25443);   //cc
				if (TALKER_read_line() == 0)
				{
					LOG_Debug($"Connected with {attempt} attempts");
					break;
				}
			}

			if (attempt > 3)
			{
				LOG("**ERROR** Can't connect");
				return;
			}

			LOG_Debug("************");
			LOG_Debug("CONNECTED");
			LOG_Debug($"PORT: {_portname}");
			LOG_Debug($"SPEED: {_baudrate}");
			LOG_Debug("************");

			get_ready();
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
				LOG_Debug($"ERROR in <<send()>> function\n**{ex}**");
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
				LOG_Debug($"Error in <<read()>> function\n{ex}");
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
			catch (Exception ex)
			{
				LOG_Debug($"ERROR in <<read_line()>> function\n **{ex}**");
				return -1;
			}

			return 0;
		}

		/// <summary>
		/// Читает по 2 байта из
		/// буфера и записывает в контейнер.
		/// При получении команды стоп - опускается флаг.
		/// </summary>

		/// <remarks>
		/// Цикл приема данных:
		/// 1. Прочитать 2 байта из буфера;
		///	2. Соеденить 2 байта и сохранить в переменную;
		///	3. Если данные равны значению выхода - закончить прием измерений;
		///	4. Вывод статуса измерения в строку в интерфейсе;
		///	5. Добавить значение в контейнер.
		/// </remarks>
		void TALKER_receiver()
		{
			if (_serialPort.BytesToRead >= 2)
			{
				TALKER_read(bmsg, 0, 2);
				imsg = bmsg[0] + (bmsg[1] << 8);
				if (imsg != 28019)
				{
					LOG_Status(
						String.Format($"Значение: {imsg} Шаг: {CONTAINER_cur+1}"));
					CONTAINER_Add(imsg);
				}
				else
				{
					LOG_Debug("");
					get_ready();
				}
			}
		}

		/// <summary>
		/// Очистка входного порта.
		/// </summary>
		void TALKER_FlushReadBuf()
		{
			_serialPort.DiscardInBuffer();
			string dummy = _serialPort.ReadExisting();
			LOG_Debug($"Trash: {dummy}");
		}
	}
}
