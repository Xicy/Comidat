#include "main.h"
#include <stdio.h>

EXPORT(void) DoWork(ProgressCallback progressCallback)
{
	int counter = 0;
	if (progressCallback)
	{
		for (; counter <= 100; counter++)
		{
			progressCallback(counter);

		}
	}
}

EXPORT(void) ProcessFile(GetFilePathCallback getPath)
{
	if (getPath)
	{
		// get file path...
		char* path = getPath((char*)"Text Files|*.txt");
		// open the file for reading
		FILE *file = fopen(path, "r");
		// read buffer
		char line[1024];

		// print file info to the screen
		printf("File path: %s\n", path ? path : "N/A");
		printf("File content:\n");

		while (fgets(line, 1024, file) != NULL)
		{
			printf("%s", line);
		}

		// close the file
		fclose(file);
	}
}