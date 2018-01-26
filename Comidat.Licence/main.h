#ifdef _WIN32
#define _CRT_SECURE_NO_DEPRECATE
#endif
#define EXPORT(TYPE) __declspec(dllexport) TYPE __stdcall
#define DELEGATE(RET,NAME,IN) typedef RET(__stdcall *NAME)IN;

#ifdef __cplusplus
extern "C" {
#endif
	DELEGATE(void, ProgressCallback, (int));
	DELEGATE(char*, GetFilePathCallback, (char* filter));

	EXPORT(void) DoWork(ProgressCallback progressCallback);
	EXPORT(void) ProcessFile(GetFilePathCallback getPath);
#ifdef __cplusplus
}
#endif