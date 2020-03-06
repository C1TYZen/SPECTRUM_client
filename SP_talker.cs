using System;
using System.IO.Ports;

namespace graph1
{
	/// <summary>
	/// Класс для общения по последовательному порту.
	/// </summary>
	public class SP_talker : IDisposable
	{
		public SerialPort _serialPort = new SerialPort();

		//буферы для 2х байтогого приема
		byte[] bmsg = new byte[2];
		int imsg;
		public bool Receive = false;

		int _baudrate = 76800;
		int count = 1;

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
		/// массива buf.
		/// </summary>
		/// <param name="buf"></param>
		public void send2bytes(int buf)
		{
			bmsg[0] = (byte)buf;
			bmsg[1] = (byte)(buf >> 8);
			send(bmsg, 0, 2);
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
				SP_Log.Debug($"Error in <<on_receive()>> function\n{ex}");
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
		/// Функция запускающаяся в отдельном потоке.
		/// В бесконечном цикле следит за флагом Receive.
		/// При поднятии флага начинает читать по 2 байта из
		/// буфера и записывать в контейнер.
		/// Флаг опускается при получении команды остановки от сервера.
		/// </summary>
		public void go_online()
		{
			while(true)
			{
				while (Receive)
				{
					if (_serialPort.BytesToRead >= 2)
					{
						read(bmsg, 0, 2);
						imsg = bmsg[0] + (bmsg[1] << 8);
						if (imsg == 28019)
						{
							Console.WriteLine();
							SP_Flags.buttons_enable_flag = true;
							count = 1;
							break;
						}
						SP_Log.Status(
							String.Format($"Step: {count} Value: {imsg} Bytes to read: {_serialPort.BytesToRead}"));
						SP_contaner.Add(imsg);
						//Console.WriteLine(imsg);
						count++;
					}
				}
			}
		}

		public void FlushReadBuf()
		{
			_serialPort.DiscardInBuffer();
			string dummy = _serialPort.ReadExisting();
			SP_Log.Debug($"Trash: {dummy}");
		}

		public string[] GetPortNames()
		{
			return SerialPort.GetPortNames();
		}

		public int GetBaudRate()
		{
			return _baudrate;
		}

		public void Dispose()
		{
			_serialPort.Close();
		}
	}
}
