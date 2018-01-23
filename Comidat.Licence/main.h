#ifndef MAIN_HPP
#define MAIN_HPP
extern "C"
{
	__declspec(dllexport) int __stdcall add(int a, int b) {
		return a + b;
	}
}
#endif