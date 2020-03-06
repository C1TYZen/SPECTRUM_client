using System;
using System.IO.Ports;

namespace graph1
{
	public class SP_talker : IDisposable
	{
		/// <summary>
		/// Класс для общения по последовательному порту.
		/// </summary>
		bool _continue;
		SerialPort _serialPort = new SerialPort();

		string message;
		StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;

		//For rescive handler
		byte[] bmsg = new byte[2];
		int imsg;

		public SP_talker()
		{
			Console.WriteLine("*****************");
			_serialPort.PortName = "COM5"; //SetPortName(_serialPort.PortName);
			_serialPort.BaudRate = 9600; //SetPortBaudRate(_serialPort.BaudRate);
			_serialPort.DataReceived += serialPort_DataReceived;
			Console.WriteLine("PortName: COM5");
			Console.WriteLine("BaudRate: 9600");
			Console.WriteLine("Type QUIT to exit");
			Console.WriteLine("*****************");

			// Set the read/write timeouts
			_serialPort.ReadTimeout = 500;
			_serialPort.WriteTimeout = 500;
		}

		//Opens port
		public void open()
		{
			try { _serialPort.Open(); }
			catch(Exception ex)
			{
				Console.WriteLine("ERROR in <<open()>> {0}", ex);
				return;
			}

			_serialPort.DiscardInBuffer();
			_serialPort.DiscardOutBuffer();

			_continue = true;
		}

		//Send bytes
		public void send(byte[] msg, int offset, int count)
		{
			try { _serialPort.Write(msg, offset, count); }
			catch(Exception ex)
			{
				Console.WriteLine("ERROR in <<send()>> function\n**{0}**", ex);
				_continue = false;
			}
		}

		//rescive handler
		void serialPort_DataReceived(object s, SerialDataReceivedEventArgs e)
		{
			try
			{
				//Console.WriteLine("//{0}\\\\", _serialPort.BytesToRead);
				_serialPort.Read(bmsg, 0, 2);
				imsg = bmsg[0] + ((bmsg[1] & ~12)<<8); //убираем лишнее, сцепляем 2 байта
				SP_contaner.add(imsg);
				Console.WriteLine(imsg);
			}
			catch(Exception ex)
			{
				Console.WriteLine("Error in <<serialPort_DataReceived>> function\n{0}", ex);
				_continue = false;
			}
		}

		public void go_online()
		{
			open();

			while (_continue)
			{
				message = Console.ReadLine();

				if (stringComparer.Equals("quit", message))
				{
					_continue = false;
				}
				else
				{
					try
					{
						_serialPort.Write(message);
					}
					catch
					{
						Console.WriteLine("Error in <<go_online>> function, with sending");
						_continue = false;
					}
				}
			}
		}

		// Display Port values and prompt user to enter a port.
		string SetPortName(string defaultPortName)
		{
			string portName;

			Console.WriteLine("Available Ports:");
			foreach (string s in SerialPort.GetPortNames())
			{
				Console.WriteLine("    {0}", s);
			}

			Console.Write("Enter COM port value (Default: {0}): ", defaultPortName);
			portName = Console.ReadLine();

			if (portName == "" || !(portName.ToLower()).StartsWith("com"))
			{
				portName = defaultPortName;
			}
			return portName;
		}

		// Display BaudRate values and prompt user to enter a value.
		int SetPortBaudRate(int defaultPortBaudRate)
		{
			string baudRate;

			Console.Write("Baud Rate(default:{0}): ", defaultPortBaudRate);
			baudRate = Console.ReadLine();

			if (baudRate == "")
			{
				baudRate = defaultPortBaudRate.ToString();
			}

			return int.Parse(baudRate);
		}

		public void Dispose()
		{
			_serialPort.Close();
		}
	}
}
