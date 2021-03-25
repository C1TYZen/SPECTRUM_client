using System; // Console
using System.Runtime.InteropServices; // DllImport

class App
{
	//Функции
	const int CMD_MB = 0x626d; // начать измерение
	const int CMD_DZ = 0x7a64; // двигатель на ноль
	const int CMD_CC = 0x6363; // проверить соединение
	const int CMD_CS = 0x7363; // установка значения переменной
	const int CMD_TP = 0x7074; // тестирование пинов
	const int CMD_TF = 0x6674; // тестовая функция

	//Сообщения
	const int CMD_MS = 0x736d; // остановить измерение
	const int CMD_MI = 0x696d; // прервать измерение

	//Переменные
	const int CVAR_MA = 0x616d; // начало измерения
	const int CVAR_MZ = 0x7a6d; // конец измерения
	const int CVAR_MC = 0x636d; // количество измерений
	const int CVAR_DS = 0x7364; // скорость двигателя
	const int CVAR_FN = 0x6e66; // номер фильтра
	const int CVAR_FS = 0x7366; // шаг установки фильтра

	static int end = -1;
	static bool Receive = false;
	static int imsg = 0;
	static byte[] bmsg = new byte[2];

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

	static void dubious_activity()
	{
		Console.WriteLine("Do something");
		int some_var = 10;
		int another_var = 3;
		int result = some_var - another_var * some_var/another_var;
	}

	static void snd()
	{
		int imsg = 0;
		byte[] bmsg = new byte[2];
		while(true)
		{
			com_read(bmsg, 2);
			imsg = bmsg[0] + (bmsg[1] << 8);
			Console.WriteLine($"{imsg}");
			if(imsg == 29549)
				return;
		}
	}

	static int receiver()
	{
		com_read(bmsg, 2);
		imsg = bmsg[0] + (bmsg[1] << 8);
		Console.WriteLine($"{imsg}");
		if (imsg != CMD_MS)
		{
			end += 1;
			dubious_activity();
		}
		else
		{
			Console.WriteLine("");
			get_ready();
			return 0;
		}
		return -1;

	}
	
	static void get_ready()
	{
		Receive	= false;
		Console.WriteLine("Готов");
		Console.WriteLine("*****");

		Console.WriteLine($"шаг: {end}");
	}

	static int TALKER_connect()
	{
		Console.WriteLine("Соединение");

		//попытка открыть порт
		if (com_open_port() == -1)
		{
			Console.WriteLine("**ОШИБКА** Подключите прибор!");
			return -1;
		}

		Console.WriteLine("************");
		Console.WriteLine("ПОДКЛЮЧЕНО");
		Console.WriteLine("ПОРТ:");
		Console.WriteLine("BAUDRATE:");
		Console.WriteLine("************");

		return 0;
	}

	static int TALKER_flush_read_buf()
	{
		com_flush();

		return 0;
	}

	static void TALKER_send(int buf)
	{
		com_write((short)buf);
	}

	static void TALKER_set(int var, int val)
	{
		//TALKER_send(CMD_CS);
		TALKER_send(var);
		TALKER_send(val);
		TALKER_read_line();
	}

	static int TALKER_read_line()
	{
		com_read_line();

		return 0;
	}

	static void mesure_setup()
	{
		TALKER_flush_read_buf();
		int x0 = 0;
		int x1 = 100;
		int mps = 1;
		int speed = 700;

		TALKER_set(CVAR_MA, x0);
		TALKER_set(CVAR_MZ, x1);
		TALKER_set(CVAR_MC, mps);
		TALKER_set(CVAR_DS, speed);

		Console.WriteLine($"Scale:");
		Console.WriteLine($"Height Scale:");
		end = -1;

		Console.WriteLine($"ИЗМЕРЕНИЕ!");
		Receive = true;
		if(Receive)
			dubious_activity();
		com_write((short)CMD_MB);
		
		while(true)
		{
			com_read(bmsg, 2);
			imsg = bmsg[0] + (bmsg[1] << 8);
			Console.WriteLine($"{imsg}");
			if (imsg != CMD_MS)
			{
				end += 1;
				dubious_activity();
			}
			else
			{
				Console.WriteLine("");
				get_ready();
				break;
			}
		}
	}

	//
	//MAIN
	//
	static void Main()
	{
		com_open_port();
		while(true)
		{
			mesure_setup();

			
			Console.ReadLine();
		}
	}
}