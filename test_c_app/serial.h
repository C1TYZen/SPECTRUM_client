#include <stdio.h>
#include <windows.h>

//Функции
#define CMD_MB	0x626d // начать измерение
#define CMD_DZ  0x7a64 // двигатель на ноль
#define CMD_CC	0x6363 // проверить соединение
#define CMD_CS  0x7363 // установка значения переменной

#define CMD_FA 0x6166
#define CMD_FB 0x6266
#define CMD_FC 0x6366
#define CMD_FD 0x6466
#define CMD_FE 0x6566
#define CMD_FF 0x6666
#define CMD_FZ 0x7a66

#define CMD_TP	0x7074 // тестирование пинов
#define CMD_TF  0x6674 // тестовая функция

//Сообщения
#define CMD_MS	0x736d // остановить измерение
#define CMD_MI  0x696d // прервать измерение

//Переменные
#define CVAR_MA  0x616d // начало измерения
#define CVAR_MZ  0x7a6d // конец измерения
#define CVAR_MC  0x636d // количество измерений
#define CVAR_DS  0x7364 // скорость двигателя
#define CVAR_FN  0x6e66 // номер фильтра
#define CVAR_FS  0x7366 // шаг установки фильтра

HANDLE g_com_handler; //Handle variable
DWORD g_rwlen; // read/write length

/***************
 * SYS
 ***************/
void port_read(LPVOID buffer, DWORD len)
{
	ReadFile(g_com_handler, buffer, len, &g_rwlen, 0);
}

void port_write(LPVOID data, DWORD len)
{
	WriteFile(g_com_handler, data, len, &g_rwlen, 0);
}

int com_open_port()
{
	int port = 11;
	char port_name[20];

	sprintf(port_name, "\\\\.\\COM%d", port);

	g_com_handler = CreateFile(port_name, 
		GENERIC_READ|GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);

	if(g_com_handler == INVALID_HANDLE_VALUE)
	{
		printf("\terror: COM%d is not available.\n", port);
		return -1;
	}

	DCB dcbSerialParams = {0};

	dcbSerialParams.DCBlength=sizeof(dcbSerialParams);

	if (!GetCommState(g_com_handler, &dcbSerialParams))
	{
		printf("Unable to get the state of serial port");
		return -1;
	}

	dcbSerialParams.BaudRate=CBR_38400;
	dcbSerialParams.ByteSize=8;
	dcbSerialParams.StopBits=ONESTOPBIT;
	dcbSerialParams.Parity=NOPARITY;

	if(!SetCommState(g_com_handler, &dcbSerialParams))
	{
		printf("Unable to set serial port settings\n");
		return -1;
	}

	COMMTIMEOUTS timeouts={0};

	timeouts.ReadIntervalTimeout = 50;
	timeouts.ReadTotalTimeoutConstant = 50;
	timeouts.ReadTotalTimeoutMultiplier = 10;
	timeouts.WriteTotalTimeoutConstant = 50;
	timeouts.WriteTotalTimeoutMultiplier = 10;

	if(!SetCommTimeouts(g_com_handler, &timeouts))
	{
		printf("Error setting Serial Port timeouts property\n");
		return -1;
	}

	printf("COM%d opened successfully\n",port);

	return 0;
}

void com_flush()
{
	char dumm[1] = {0};

	printf("trash: ");
	while(1)
	{
		strset(dumm, 0);
		port_read(dumm, sizeof(dumm));
		printf("%d", dumm[0]);
		if(dumm[0] == 0)
		{
			break;
		}
	}
	printf("\n");
}

/***************
 * WRITE
 ***************/
void com_write(short data)
{
	char send[32];
	sprintf(send, "%c%c", data, data>>8);
	Sleep(80);
	port_write(send, 2);
}

/***************
 * READ
 ***************/
void com_read(char *msg, int len)
{
	//Sleep(80);
	//printf("size: %d\n", GetFileSize(g_com_handler, NULL));
	port_read(msg, len);
}

void com_read_line()
{
	//DWORD dwFileSize = GetFileSize(g_com_handler,  NULL);
	//printf("size %d\n", dwFileSize);
	char receive[32] = {0};
	strset(receive, 0);

	port_read(receive, sizeof(receive));
	printf("%s", receive);
}

/***************
 * ADVANCE
 ***************/
void com_var_set(short var, int val, short cmd)
{
	com_write(cmd);
	com_write(var);
	com_write(val);
	com_read_line();
}