#include "serial.h"

int main()
{
	com_open_port();
	
	com_write(CVAR_MA);
	com_read_line();
	com_write(1000);
	com_read_line();

	// com_write(CMD_CC);
	// com_read_line();
}