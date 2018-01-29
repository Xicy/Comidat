#ifndef CPUID_H
#define CPUID_H
#define EXPORT(TYPE) __declspec(dllexport) TYPE __stdcall
#define DELEGATE(RET,NAME,IN) typedef RET(__stdcall *NAME)IN;
#ifdef _WIN32
#include <limits.h>
#include <intrin.h>
typedef unsigned __int32  uint32_t;
#else
#include <stdint.h>
#endif
#ifdef __cplusplus
extern "C" {
#endif
	//Header start here
	EXPORT(int*) GetCPUInfo(unsigned);
	//Header end here
#ifdef __cplusplus
}
#endif
#endif // CPUID_H