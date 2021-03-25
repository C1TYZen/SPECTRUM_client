#include "serial.h"

int end = -1;
int Receive = 0;
int imsg = 0;
char bmsg[2] = {0};

int res()
{
	while(1)
	{
		char bmsg[2] = {0};
		int imsg = 0;
		com_read(bmsg, 2);
		imsg = bmsg[0] + (bmsg[1] << 8);
		printf("%d\n", imsg);
		if(imsg == 29549)
		{
			return 0;
		}
	}
}

void get_ready()
{
	Receive	= 0;
	printf("ready | step: %d", end);
	imsg = 0;
	bmsg[0] = 0;
	bmsg[1] = 0;
}

void TALKER_set(int var, int val)
{
	com_write(var);
	com_write(val);
	com_read_line();
}

void mesure_setup()
{
	com_flush();
	// int x0 = 0;
	// int x1 = 100;
	// int mps = 1;
	// int speed = 700;

	// TALKER_set(CVAR_MA, x0);
	// TALKER_set(CVAR_MZ, x1);
	// TALKER_set(CVAR_MC, mps);
	// TALKER_set(CVAR_DS, speed);

	// Console.WriteLine($"Scale:");
	// Console.WriteLine($"Height Scale:");
	end = -1;

	printf("mesure!");
	Receive = 1;
	com_write((short)CMD_MB);
	
	while(1)
	{
		com_read(bmsg, 2);
		imsg = bmsg[0] + (bmsg[1] << 8);
		printf("%d\n", imsg);
		if (imsg == CMD_MS)
		{
			printf("\n");
			get_ready();
			break;
		}
		end += 1;
	}
}

int main()
{
	char str[32];

	com_open_port();

	while(1)
	{
		com_flush();
		int x0 = 0;
		int x1 = 100;
		int mps = 1;
		int speed = 700;

		TALKER_set(CVAR_MA, x0);
		TALKER_set(CVAR_MZ, x1);
		TALKER_set(CVAR_MC, mps);
		TALKER_set(CVAR_DS, speed);

		end = -1;

		printf("mesure!\n");
		Receive = 1;
		com_write(CMD_MB);
		
		while(1)
		{
			com_read(bmsg, 2);
			imsg = bmsg[0] + (bmsg[1] << 8);
			printf("%d\r", imsg);
			if (imsg == CMD_MS)
			{
				printf("\n");
				get_ready();
				break;
			}
			end += 1;
		}
		gets(str);
	}
}