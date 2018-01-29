#include "main.h"

EXPORT(int*) GetCPUInfo(unsigned i) {
	int regs[4] = { 0 };
#ifdef _WIN32
	__cpuid(regs, i);
#else
	asm volatile ("cpuid" : "=a" (regs[0]), "=b" (regs[1]), "=c" (regs[2]), "=d" (regs[3]) : "a" (i), "c" (0));
#endif
	return regs;
}