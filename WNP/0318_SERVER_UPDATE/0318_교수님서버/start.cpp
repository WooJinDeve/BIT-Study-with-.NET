//start.cpp

#include "std.h"

int main()
{
	if (con_Init() == 1)
	{
		con_Run();
	}

	con_Exit();

	return 0;
}