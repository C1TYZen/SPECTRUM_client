﻿using System;
using System.IO.Ports;
using System.Threading;

namespace graph1
{
	/// <summary>
	/// Класс для общения с ардуино по последовательному порту.
	/// </summary>
	class SP_talker
	{
		public delegate void ready();
		public ready get_ready_func;
		public SerialPort _serialPort = new SerialPort();
		public bool Receive = false;

		public int _baudrate = 76800;
		public string _portname;

		byte[] bmsg = new byte[3];
		int imsg;

		/// <summary>
		/// Открывает порт с указанным именем и соростью.
		/// Устанавливает дефолтные таймауты и очищает буферы.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="speed"></param>
		/// <returns></returns>
		public int open(string name, int speed)
		{
			if (name != null)
				_serialPort.PortName = name;
			else
			{
				SP_Log.Debug("Choose name of port");
				return -1;
			}
			_serialPort.BaudRate = speed;

			_serialPort.ReadTimeout = 1000;
			_serialPort.WriteTimeout = 1000;

			try { _serialPort.Open(); }
			catch(Exception ex)
			{
				SP_Log.Debug($"ERROR in <<open()>> {ex}");
				return -1;
			}

			_serialPort.DiscardInBuffer();
			_serialPort.DiscardOutBuffer();

			return 0;
		}

		/// <summary>
		/// Отправляет в последовательный порт количество байт count
		/// из массива msg с отступом offset.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		public void send(byte[] msg, int offset, int count)
		{
			try { _serialPort.Write(msg, offset, count); }
			catch(Exception ex)
			{
				SP_Log.Debug($"ERROR in <<send()>> function\n**{ex}**");
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
		public void send2bytes(int buf)
		{
			bmsg[0] = (byte)buf;
			bmsg[1] = (byte)(buf >> 8);
			send(bmsg, 0, 2);
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
		/// Не понятно почему так происходит, так как регистр 8-битный.
		/// То есть в регистре при сдвиге должен отбрасываться и второй бит тоже.
		/// </remarks>
		public void send3bytes(int buf)
		{
			bmsg[0] = (byte)(buf >> 16);
			bmsg[1] = (byte)(buf >> 8);
			bmsg[2] = (byte)buf;
			Console.WriteLine($"BMSG 2:({bmsg[2]}) 1:({bmsg[1]}) 0:({bmsg[0]})");
			send(bmsg, 0, 3);
		}

		/// <summary>
		/// Читает из последовательного порта количество байт count
		/// в массив msg с отступом offset.
		/// </summary>
		/// <param name="msg"></param>
		/// <param name="offset"></param>
		/// <param name="count"></param>
		public void read(byte[] msg, int offset, int count)
		{
			try { _serialPort.Read(msg, offset, count); }
			catch (Exception ex)
			{
				SP_Log.Debug($"Error in <<read()>> function\n{ex}");
			}
		}

		/// <summary>
		/// Читает строку из последовательного порта и
		/// выводит в консоль.
		/// </summary>
		/// <returns></returns>
		public int read_line()
		{
			try { SP_Log.Debug(_serialPort.ReadLine()); }
			catch (Exception ex)
			{
				SP_Log.Debug($"ERROR in <<read_line()>> function\n **{ex}**");
				return -1;
			}

			return 0;
		}

		/// <summary>
		/// Функция осуществляет подключение к серверу ардуино, по порту _portname.
		/// </summary>
		public void connect()
		{
			int attempt = 1;
			SP_Log.Log("Соединение");

			//попытка открыть порт
			if (open(_portname, _baudrate) == -1)
			{
				SP_Log.Log("**ERROR** Plug in and restart!");
				return;
			}

			while (attempt <= 5)
			{
				//прочитать строку проверки связи
				Thread.Sleep(1000);
				send2bytes(25443);   //cc
				if (read_line() == 0)
				{
					Console.WriteLine("Connected with {0} attempts", attempt);
					break;
				}
				attempt++;
			}

			if (attempt > 3)
			{
				SP_Log.Log("**ERROR** Can't connect");
				return;
			}

			Console.WriteLine("************");
			Console.WriteLine("CONNECTED");
			Console.WriteLine($"PORT: {_portname}");
			Console.WriteLine($"SPEED: {_baudrate}");
			Console.WriteLine("************");

			if (get_ready_func != null)
				get_ready_func();
		}

		/// <summary>
		/// Читает по 2 байта из
		/// буфера и записывает в контейнер.
		/// При получении команды стоп - опускается поднимается флаг.
		/// </summary>

		/// <remarks>
		/// Основной цикл приема данных:
		/// 1. Прочитать 2 байта из буфера;
		///	2. Соеденить 2 байта и сохранить в переменную;
		///	3. Если данные равны значению выхода - закончить прием измерений;
		///	4. Вывод статуса измерения в строку в интерфейсе;
		///	5. Добавить значение в контейнер.
		/// </remarks>
		public void receiver()
		{
			if (_serialPort.BytesToRead >= 2)
			{
				read(bmsg, 0, 2);
				imsg = bmsg[0] + (bmsg[1] << 8);
				if (imsg != 28019)
				{
					SP_Log.Status(
						String.Format($"Значение: {imsg} Шаг: {SP_contaner.cur+1}"));
					SP_contaner.Add(imsg);
				}
				else
				{
					Console.WriteLine();
					get_ready_func?.Invoke();
				}
			}
		}

		/// <summary>
		/// Очистка входного порта.
		/// </summary>
		public void FlushReadBuf()
		{
			_serialPort.DiscardInBuffer();
			string dummy = _serialPort.ReadExisting();
			SP_Log.Debug($"Trash: {dummy}");
		}

		/// <summary>
		/// Получить имена портов.
		/// </summary>
		public string[] GetPortNames()
		{
			return SerialPort.GetPortNames();
		}

		public void Dispose()
		{
			_serialPort.Close();
		}
	}
}
