using System;
using System.Runtime.InteropServices;
using System.Security;

namespace DevIL
{
	// Token: 0x02000037 RID: 55
	public static class IlInterop
	{
		// Token: 0x060001FD RID: 509
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilActiveImage(int Number);

		// Token: 0x060001FE RID: 510
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilActiveLayer(int Number);

		// Token: 0x060001FF RID: 511
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilActiveMipmap(int Number);

		// Token: 0x06000200 RID: 512
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilApplyPal(string FileName);

		// Token: 0x06000201 RID: 513
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilApplyProfile(string InProfile, string OutProfile);

		// Token: 0x06000202 RID: 514
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilBindImage(int Image);

		// Token: 0x06000203 RID: 515
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilBlit(int Source, int DestX, int DestY, int DestZ, int SrcX, int SrcY, int SrcZ, int Width, int Height, int Depth);

		// Token: 0x06000204 RID: 516
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilClearColour(float Red, float Green, float Blue, float Alpha);

		// Token: 0x06000205 RID: 517
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilClearImage();

		// Token: 0x06000206 RID: 518
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern int ilCloneCurImage();

		// Token: 0x06000207 RID: 519
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilCompressFunc(int Mode);

		// Token: 0x06000208 RID: 520
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilConvertImage(int DestFormat, int DestType);

		// Token: 0x06000209 RID: 521
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilConvertPal(int DestFormat);

		// Token: 0x0600020A RID: 522
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilCopyImage(int Src);

		// Token: 0x0600020B RID: 523
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern int ilCopyPixels(int XOff, int YOff, int ZOff, int Width, int Height, int Depth, int Format, int Type, IntPtr Data);

		// Token: 0x0600020C RID: 524
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern int ilCreateSubImage(int Type, int Num);

		// Token: 0x0600020D RID: 525
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilDefaultImage();

		// Token: 0x0600020E RID: 526
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilDeleteImage(int Num);

		// Token: 0x0600020F RID: 527
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilDeleteImages(int Num, ref int Image);

		// Token: 0x06000210 RID: 528
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilDeleteImages(int Num, int[] Images);

		// Token: 0x06000211 RID: 529
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilDisable(int Mode);

		// Token: 0x06000212 RID: 530
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilEnable(int Mode);

		// Token: 0x06000213 RID: 531
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilFormatFunc(int Mode);

		// Token: 0x06000214 RID: 532
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilGenImages(int Num, out int Images);

		// Token: 0x06000215 RID: 533
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilGenImages(int Num, [Out] int[] Images);

		// Token: 0x06000216 RID: 534
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern int ilGenImage();

		// Token: 0x06000217 RID: 535
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern IntPtr ilGetAlpha(int Type);

		// Token: 0x06000218 RID: 536
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilGetBoolean(int Mode);

		// Token: 0x06000219 RID: 537
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilGetBooleanv(int Mode, out bool Param);

		// Token: 0x0600021A RID: 538
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern IntPtr ilGetData();

		// Token: 0x0600021B RID: 539
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern int ilGetDXTCData(IntPtr Buffer, int BufferSize, int DXTCFormat);

		// Token: 0x0600021C RID: 540
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern int ilGetError();

		// Token: 0x0600021D RID: 541
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern int ilGetInteger(int Mode);

		// Token: 0x0600021E RID: 542
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilGetIntegerv(int Mode, out int Param);

		// Token: 0x0600021F RID: 543
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilGetIntegerv(int Mode, [Out] int[] Param);

		// Token: 0x06000220 RID: 544
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern int ilGetLumpPos();

		// Token: 0x06000221 RID: 545
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern IntPtr ilGetPalette();

		// Token: 0x06000222 RID: 546
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern string ilGetString(int StringName);

		// Token: 0x06000223 RID: 547
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilHint(int Target, int Mode);

		// Token: 0x06000224 RID: 548
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilInit();

		// Token: 0x06000225 RID: 549
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilIsDisabled(int Mode);

		// Token: 0x06000226 RID: 550
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilIsEnabled(int Mode);

		// Token: 0x06000227 RID: 551
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilIsImage(int Image);

		// Token: 0x06000228 RID: 552
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilIsValid(int Type, string FileName);

		// Token: 0x06000229 RID: 553
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilIsValidF(int Type, IntPtr File);

		// Token: 0x0600022A RID: 554
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilIsValidL(int Type, IntPtr Lump, int Size);

		// Token: 0x0600022B RID: 555
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilIsValidL(int Type, byte[] Lump, int Size);

		// Token: 0x0600022C RID: 556
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilKeyColour(float Red, float Green, float Blue, float Alpha);

		// Token: 0x0600022D RID: 557
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilLoad(int Type, string FileName);

		// Token: 0x0600022E RID: 558
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilLoadF(int Type, IntPtr File);

		// Token: 0x0600022F RID: 559
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilLoadImage(string FileName);

		// Token: 0x06000230 RID: 560
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilLoadL(int Type, IntPtr Lump, int Size);

		// Token: 0x06000231 RID: 561
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilLoadL(int Type, byte[] Lump, int Size);

		// Token: 0x06000232 RID: 562
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilLoadPal(string FileName);

		// Token: 0x06000233 RID: 563
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilModAlpha(double AlphaValue);

		// Token: 0x06000234 RID: 564
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern byte ilOriginFunc(int Mode);

		// Token: 0x06000235 RID: 565
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilOverlayImage(int Source, int XCoord, int YCoord, int ZCoord);

		// Token: 0x06000236 RID: 566
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilPopAttrib();

		// Token: 0x06000237 RID: 567
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilPushAttrib(int Bits);

		// Token: 0x06000238 RID: 568
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilRegisterFormat(int Format);

		// Token: 0x06000239 RID: 569
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilRegisterLoad(string Ext, IlInterop.IL_LOADPROC Load);

		// Token: 0x0600023A RID: 570
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilRegisterMipNum(int Num);

		// Token: 0x0600023B RID: 571
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilRegisterNumImages(int Num);

		// Token: 0x0600023C RID: 572
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilRegisterOrigin(int Origin);

		// Token: 0x0600023D RID: 573
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilRegisterPal(IntPtr Pal, int Size, int Type);

		// Token: 0x0600023E RID: 574
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilRegisterSave(string Ext, IlInterop.IL_SAVEPROC Save);

		// Token: 0x0600023F RID: 575
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilRegisterType(int Type);

		// Token: 0x06000240 RID: 576
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilRemoveLoad(string Ext);

		// Token: 0x06000241 RID: 577
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilRemoveSave(string Ext);

		// Token: 0x06000242 RID: 578
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilResetMemory();

		// Token: 0x06000243 RID: 579
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilResetRead();

		// Token: 0x06000244 RID: 580
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilResetWrite();

		// Token: 0x06000245 RID: 581
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilSave(int Type, string FileName);

		// Token: 0x06000246 RID: 582
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern int ilSaveF(int Type, IntPtr File);

		// Token: 0x06000247 RID: 583
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilSaveImage(string FileName);

		// Token: 0x06000248 RID: 584
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern int ilSaveL(int Type, IntPtr Lump, int Size);

		// Token: 0x06000249 RID: 585
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern int ilSaveL(int Type, byte[] Lump, int Size);

		// Token: 0x0600024A RID: 586
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilSavePal(string FileName);

		// Token: 0x0600024B RID: 587
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilSetAlpha(double AlphaValue);

		// Token: 0x0600024C RID: 588
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilSetData(IntPtr Data);

		// Token: 0x0600024D RID: 589
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilSetDuration(int Duration);

		// Token: 0x0600024E RID: 590
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilSetInteger(int Mode, int Param);

		// Token: 0x0600024F RID: 591
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilSetMemory(IlInterop.mAlloc AllocFunc, IlInterop.mFree FreeFunc);

		// Token: 0x06000250 RID: 592
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilSetPixels(int XOff, int YOff, int ZOff, int Width, int Height, int Depth, int Format, int Type, IntPtr Data);

		// Token: 0x06000251 RID: 593
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilSetRead(IlInterop.fOpenRProc Open, IlInterop.fCloseRProc Close, IlInterop.fEofProc Eof, IlInterop.fGetcProc Getc, IlInterop.fReadProc Read, IlInterop.fSeekRProc Seek, IlInterop.fTellRProc Tell);

		// Token: 0x06000252 RID: 594
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilSetString(int Mode, string str);

		// Token: 0x06000253 RID: 595
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilSetWrite(IlInterop.fOpenWProc Open, IlInterop.fCloseWProc Close, IlInterop.fPutcProc Putc, IlInterop.fSeekWProc Seek, IlInterop.fTellWProc Tell, IlInterop.fWriteProc Write);

		// Token: 0x06000254 RID: 596
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern void ilShutDown();

		// Token: 0x06000255 RID: 597
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilTexImage(int Width, int Height, int Depth, byte numChannels, int Format, int Type, IntPtr Data);

		// Token: 0x06000256 RID: 598
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern int ilTypeFromExt(string FileName);

		// Token: 0x06000257 RID: 599
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilTypeFunc(int Mode);

		// Token: 0x06000258 RID: 600
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilLoadData(string FileName, int Width, int Height, int Depth, byte Bpp);

		// Token: 0x06000259 RID: 601
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilLoadDataF(IntPtr File, int Width, int Height, int Depth, byte Bpp);

		// Token: 0x0600025A RID: 602
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilLoadDataL(IntPtr Lump, int Size, int Width, int Height, int Depth, byte Bpp);

		// Token: 0x0600025B RID: 603
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilLoadDataL(byte[] Lump, int Size, int Width, int Height, int Depth, byte Bpp);

		// Token: 0x0600025C RID: 604
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilSaveData(string FileName);

		// Token: 0x0600025D RID: 605
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilLoadFromJpegStruct(IntPtr JpegDecompressorPtr);

		// Token: 0x0600025E RID: 606
		[SuppressUnmanagedCodeSecurity]
		[DllImport("devil64")]
		public static extern bool ilSaveFromJpegStruct(IntPtr JpegCompressorPtr);

		// Token: 0x04000135 RID: 309
		private const CallingConvention CALLING_CONVENTION = CallingConvention.Winapi;

		// Token: 0x04000136 RID: 310
		public const string DllPath = "devil64";

		// Token: 0x04000137 RID: 311
		public const int IL_FALSE = 0;

		// Token: 0x04000138 RID: 312
		public const int IL_TRUE = 1;

		// Token: 0x04000139 RID: 313
		public const int IL_COLOUR_INDEX = 6400;

		// Token: 0x0400013A RID: 314
		public const int IL_COLOR_INDEX = 6400;

		// Token: 0x0400013B RID: 315
		public const int IL_RGB = 6407;

		// Token: 0x0400013C RID: 316
		public const int IL_RGBA = 6408;

		// Token: 0x0400013D RID: 317
		public const int IL_BGR = 32992;

		// Token: 0x0400013E RID: 318
		public const int IL_BGRA = 32993;

		// Token: 0x0400013F RID: 319
		public const int IL_LUMINANCE = 6409;

		// Token: 0x04000140 RID: 320
		public const int IL_LUMINANCE_ALPHA = 6410;

		// Token: 0x04000141 RID: 321
		public const int IL_BYTE = 5120;

		// Token: 0x04000142 RID: 322
		public const int IL_UNSIGNED_BYTE = 5121;

		// Token: 0x04000143 RID: 323
		public const int IL_SHORT = 5122;

		// Token: 0x04000144 RID: 324
		public const int IL_UNSIGNED_SHORT = 5123;

		// Token: 0x04000145 RID: 325
		public const int IL_INT = 5124;

		// Token: 0x04000146 RID: 326
		public const int IL_UNSIGNED_INT = 5125;

		// Token: 0x04000147 RID: 327
		public const int IL_FLOAT = 5126;

		// Token: 0x04000148 RID: 328
		public const int IL_DOUBLE = 5130;

		// Token: 0x04000149 RID: 329
		public const int IL_VENDOR = 7936;

		// Token: 0x0400014A RID: 330
		public const int IL_LOAD_EXT = 7937;

		// Token: 0x0400014B RID: 331
		public const int IL_SAVE_EXT = 7938;

		// Token: 0x0400014C RID: 332
		public const int IL_VERSION_1_6_8 = 1;

		// Token: 0x0400014D RID: 333
		public const int IL_VERSION = 168;

		// Token: 0x0400014E RID: 334
		public const int IL_ORIGIN_BIT = 1;

		// Token: 0x0400014F RID: 335
		public const int IL_FILE_BIT = 2;

		// Token: 0x04000150 RID: 336
		public const int IL_PAL_BIT = 4;

		// Token: 0x04000151 RID: 337
		public const int IL_FORMAT_BIT = 8;

		// Token: 0x04000152 RID: 338
		public const int IL_TYPE_BIT = 16;

		// Token: 0x04000153 RID: 339
		public const int IL_COMPRESS_BIT = 32;

		// Token: 0x04000154 RID: 340
		public const int IL_LOADFAIL_BIT = 64;

		// Token: 0x04000155 RID: 341
		public const int IL_FORMAT_SPECIFIC_BIT = 128;

		// Token: 0x04000156 RID: 342
		public const int IL_ALL_ATTRIB_BITS = 1048575;

		// Token: 0x04000157 RID: 343
		public const int IL_PAL_NONE = 1024;

		// Token: 0x04000158 RID: 344
		public const int IL_PAL_RGB24 = 1025;

		// Token: 0x04000159 RID: 345
		public const int IL_PAL_RGB32 = 1026;

		// Token: 0x0400015A RID: 346
		public const int IL_PAL_RGBA32 = 1027;

		// Token: 0x0400015B RID: 347
		public const int IL_PAL_BGR24 = 1028;

		// Token: 0x0400015C RID: 348
		public const int IL_PAL_BGR32 = 1029;

		// Token: 0x0400015D RID: 349
		public const int IL_PAL_BGRA32 = 1030;

		// Token: 0x0400015E RID: 350
		public const int IL_TYPE_UNKNOWN = 0;

		// Token: 0x0400015F RID: 351
		public const int IL_BMP = 1056;

		// Token: 0x04000160 RID: 352
		public const int IL_CUT = 1057;

		// Token: 0x04000161 RID: 353
		public const int IL_DOOM = 1058;

		// Token: 0x04000162 RID: 354
		public const int IL_DOOM_FLAT = 1059;

		// Token: 0x04000163 RID: 355
		public const int IL_ICO = 1060;

		// Token: 0x04000164 RID: 356
		public const int IL_JPG = 1061;

		// Token: 0x04000165 RID: 357
		public const int IL_JFIF = 1061;

		// Token: 0x04000166 RID: 358
		public const int IL_LBM = 1062;

		// Token: 0x04000167 RID: 359
		public const int IL_PCD = 1063;

		// Token: 0x04000168 RID: 360
		public const int IL_PCX = 1064;

		// Token: 0x04000169 RID: 361
		public const int IL_PIC = 1065;

		// Token: 0x0400016A RID: 362
		public const int IL_PNG = 1066;

		// Token: 0x0400016B RID: 363
		public const int IL_PNM = 1067;

		// Token: 0x0400016C RID: 364
		public const int IL_SGI = 1068;

		// Token: 0x0400016D RID: 365
		public const int IL_TGA = 1069;

		// Token: 0x0400016E RID: 366
		public const int IL_TIF = 1070;

		// Token: 0x0400016F RID: 367
		public const int IL_CHEAD = 1071;

		// Token: 0x04000170 RID: 368
		public const int IL_RAW = 1072;

		// Token: 0x04000171 RID: 369
		public const int IL_MDL = 1073;

		// Token: 0x04000172 RID: 370
		public const int IL_WAL = 1074;

		// Token: 0x04000173 RID: 371
		public const int IL_LIF = 1076;

		// Token: 0x04000174 RID: 372
		public const int IL_MNG = 1077;

		// Token: 0x04000175 RID: 373
		public const int IL_JNG = 1077;

		// Token: 0x04000176 RID: 374
		public const int IL_GIF = 1078;

		// Token: 0x04000177 RID: 375
		public const int IL_DDS = 1079;

		// Token: 0x04000178 RID: 376
		public const int IL_DCX = 1080;

		// Token: 0x04000179 RID: 377
		public const int IL_PSD = 1081;

		// Token: 0x0400017A RID: 378
		public const int IL_EXIF = 1082;

		// Token: 0x0400017B RID: 379
		public const int IL_PSP = 1083;

		// Token: 0x0400017C RID: 380
		public const int IL_PIX = 1084;

		// Token: 0x0400017D RID: 381
		public const int IL_PXR = 1085;

		// Token: 0x0400017E RID: 382
		public const int IL_XPM = 1086;

		// Token: 0x0400017F RID: 383
		public const int IL_HDR = 1087;

		// Token: 0x04000180 RID: 384
		public const int IL_JASC_PAL = 1141;

		// Token: 0x04000181 RID: 385
		public const int IL_NO_ERROR = 0;

		// Token: 0x04000182 RID: 386
		public const int IL_INVALID_ENUM = 1281;

		// Token: 0x04000183 RID: 387
		public const int IL_OUT_OF_MEMORY = 1282;

		// Token: 0x04000184 RID: 388
		public const int IL_FORMAT_NOT_SUPPORTED = 1283;

		// Token: 0x04000185 RID: 389
		public const int IL_INTERNAL_ERROR = 1284;

		// Token: 0x04000186 RID: 390
		public const int IL_INVALID_VALUE = 1285;

		// Token: 0x04000187 RID: 391
		public const int IL_ILLEGAL_OPERATION = 1286;

		// Token: 0x04000188 RID: 392
		public const int IL_ILLEGAL_FILE_VALUE = 1287;

		// Token: 0x04000189 RID: 393
		public const int IL_INVALID_FILE_HEADER = 1288;

		// Token: 0x0400018A RID: 394
		public const int IL_INVALID_PARAM = 1289;

		// Token: 0x0400018B RID: 395
		public const int IL_COULD_NOT_OPEN_FILE = 1290;

		// Token: 0x0400018C RID: 396
		public const int IL_INVALID_EXTENSION = 1291;

		// Token: 0x0400018D RID: 397
		public const int IL_FILE_ALREADY_EXISTS = 1292;

		// Token: 0x0400018E RID: 398
		public const int IL_OUT_FORMAT_SAME = 1293;

		// Token: 0x0400018F RID: 399
		public const int IL_STACK_OVERFLOW = 1294;

		// Token: 0x04000190 RID: 400
		public const int IL_STACK_UNDERFLOW = 1295;

		// Token: 0x04000191 RID: 401
		public const int IL_INVALID_CONVERSION = 1296;

		// Token: 0x04000192 RID: 402
		public const int IL_BAD_DIMENSIONS = 1297;

		// Token: 0x04000193 RID: 403
		public const int IL_FILE_READ_ERROR = 1298;

		// Token: 0x04000194 RID: 404
		public const int IL_FILE_WRITE_ERROR = 1298;

		// Token: 0x04000195 RID: 405
		public const int IL_LIB_GIF_ERROR = 1505;

		// Token: 0x04000196 RID: 406
		public const int IL_LIB_JPEG_ERROR = 1506;

		// Token: 0x04000197 RID: 407
		public const int IL_LIB_PNG_ERROR = 1507;

		// Token: 0x04000198 RID: 408
		public const int IL_LIB_TIFF_ERROR = 1508;

		// Token: 0x04000199 RID: 409
		public const int IL_LIB_MNG_ERROR = 1509;

		// Token: 0x0400019A RID: 410
		public const int IL_UNKNOWN_ERROR = 1535;

		// Token: 0x0400019B RID: 411
		public const int IL_ORIGIN_SET = 1536;

		// Token: 0x0400019C RID: 412
		public const int IL_ORIGIN_LOWER_LEFT = 1537;

		// Token: 0x0400019D RID: 413
		public const int IL_ORIGIN_UPPER_LEFT = 1538;

		// Token: 0x0400019E RID: 414
		public const int IL_ORIGIN_MODE = 1539;

		// Token: 0x0400019F RID: 415
		public const int IL_FORMAT_SET = 1552;

		// Token: 0x040001A0 RID: 416
		public const int IL_FORMAT_MODE = 1553;

		// Token: 0x040001A1 RID: 417
		public const int IL_TYPE_SET = 1554;

		// Token: 0x040001A2 RID: 418
		public const int IL_TYPE_MODE = 1555;

		// Token: 0x040001A3 RID: 419
		public const int IL_FILE_OVERWRITE = 1568;

		// Token: 0x040001A4 RID: 420
		public const int IL_FILE_MODE = 1569;

		// Token: 0x040001A5 RID: 421
		public const int IL_CONV_PAL = 1584;

		// Token: 0x040001A6 RID: 422
		public const int IL_DEFAULT_ON_FAIL = 1586;

		// Token: 0x040001A7 RID: 423
		public const int IL_USE_KEY_COLOUR = 1589;

		// Token: 0x040001A8 RID: 424
		public const int IL_USE_KEY_COLOR = 1589;

		// Token: 0x040001A9 RID: 425
		public const int IL_SAVE_INTERLACED = 1593;

		// Token: 0x040001AA RID: 426
		public const int IL_INTERLACE_MODE = 1594;

		// Token: 0x040001AB RID: 427
		public const int IL_QUANTIZATION_MODE = 1600;

		// Token: 0x040001AC RID: 428
		public const int IL_WU_QUANT = 1601;

		// Token: 0x040001AD RID: 429
		public const int IL_NEU_QUANT = 1602;

		// Token: 0x040001AE RID: 430
		public const int IL_NEU_QUANT_SAMPLE = 1603;

		// Token: 0x040001AF RID: 431
		public const int IL_MAX_QUANT_INDEXS = 1604;

		// Token: 0x040001B0 RID: 432
		public const int IL_FASTEST = 1632;

		// Token: 0x040001B1 RID: 433
		public const int IL_LESS_MEM = 1633;

		// Token: 0x040001B2 RID: 434
		public const int IL_DONT_CARE = 1634;

		// Token: 0x040001B3 RID: 435
		public const int IL_MEM_SPEED_HINT = 1637;

		// Token: 0x040001B4 RID: 436
		public const int IL_USE_COMPRESSION = 1638;

		// Token: 0x040001B5 RID: 437
		public const int IL_NO_COMPRESSION = 1639;

		// Token: 0x040001B6 RID: 438
		public const int IL_COMPRESSION_HINT = 1640;

		// Token: 0x040001B7 RID: 439
		public const int IL_SUB_NEXT = 1664;

		// Token: 0x040001B8 RID: 440
		public const int IL_SUB_MIPMAP = 1665;

		// Token: 0x040001B9 RID: 441
		public const int IL_SUB_LAYER = 1666;

		// Token: 0x040001BA RID: 442
		public const int IL_COMPRESS_MODE = 1792;

		// Token: 0x040001BB RID: 443
		public const int IL_COMPRESS_NONE = 1793;

		// Token: 0x040001BC RID: 444
		public const int IL_COMPRESS_RLE = 1794;

		// Token: 0x040001BD RID: 445
		public const int IL_COMPRESS_LZO = 1795;

		// Token: 0x040001BE RID: 446
		public const int IL_COMPRESS_ZLIB = 1796;

		// Token: 0x040001BF RID: 447
		public const int IL_TGA_CREATE_STAMP = 1808;

		// Token: 0x040001C0 RID: 448
		public const int IL_JPG_QUALITY = 1809;

		// Token: 0x040001C1 RID: 449
		public const int IL_PNG_INTERLACE = 1810;

		// Token: 0x040001C2 RID: 450
		public const int IL_TGA_RLE = 1811;

		// Token: 0x040001C3 RID: 451
		public const int IL_BMP_RLE = 1812;

		// Token: 0x040001C4 RID: 452
		public const int IL_SGI_RLE = 1813;

		// Token: 0x040001C5 RID: 453
		public const int IL_TGA_ID_STRING = 1815;

		// Token: 0x040001C6 RID: 454
		public const int IL_TGA_AUTHNAME_STRING = 1816;

		// Token: 0x040001C7 RID: 455
		public const int IL_TGA_AUTHCOMMENT_STRING = 1817;

		// Token: 0x040001C8 RID: 456
		public const int IL_PNG_AUTHNAME_STRING = 1818;

		// Token: 0x040001C9 RID: 457
		public const int IL_PNG_TITLE_STRING = 1819;

		// Token: 0x040001CA RID: 458
		public const int IL_PNG_DESCRIPTION_STRING = 1820;

		// Token: 0x040001CB RID: 459
		public const int IL_TIF_DESCRIPTION_STRING = 1821;

		// Token: 0x040001CC RID: 460
		public const int IL_TIF_HOSTCOMPUTER_STRING = 1822;

		// Token: 0x040001CD RID: 461
		public const int IL_TIF_DOCUMENTNAME_STRING = 1823;

		// Token: 0x040001CE RID: 462
		public const int IL_TIF_AUTHNAME_STRING = 1824;

		// Token: 0x040001CF RID: 463
		public const int IL_JPG_SAVE_FORMAT = 1825;

		// Token: 0x040001D0 RID: 464
		public const int IL_CHEAD_HEADER_STRING = 1826;

		// Token: 0x040001D1 RID: 465
		public const int IL_PCD_PICNUM = 1827;

		// Token: 0x040001D2 RID: 466
		public const int IL_PNG_ALPHA_INDEX = 1828;

		// Token: 0x040001D3 RID: 467
		public const int IL_DXTC_FORMAT = 1797;

		// Token: 0x040001D4 RID: 468
		public const int IL_DXT1 = 1798;

		// Token: 0x040001D5 RID: 469
		public const int IL_DXT2 = 1799;

		// Token: 0x040001D6 RID: 470
		public const int IL_DXT3 = 1800;

		// Token: 0x040001D7 RID: 471
		public const int IL_DXT4 = 1801;

		// Token: 0x040001D8 RID: 472
		public const int IL_DXT5 = 1802;

		// Token: 0x040001D9 RID: 473
		public const int IL_DXT_NO_COMP = 1803;

		// Token: 0x040001DA RID: 474
		public const int IL_KEEP_DXTC_DATA = 1804;

		// Token: 0x040001DB RID: 475
		public const int IL_DXTC_DATA_FORMAT = 1805;

		// Token: 0x040001DC RID: 476
		public const int IL_3DC = 1806;

		// Token: 0x040001DD RID: 477
		public const int IL_RXGB = 1807;

		// Token: 0x040001DE RID: 478
		public const int IL_ATI1N = 1808;

		// Token: 0x040001DF RID: 479
		public const int IL_CUBEMAP_POSITIVEX = 1024;

		// Token: 0x040001E0 RID: 480
		public const int IL_CUBEMAP_NEGATIVEX = 2048;

		// Token: 0x040001E1 RID: 481
		public const int IL_CUBEMAP_POSITIVEY = 4096;

		// Token: 0x040001E2 RID: 482
		public const int IL_CUBEMAP_NEGATIVEY = 8192;

		// Token: 0x040001E3 RID: 483
		public const int IL_CUBEMAP_POSITIVEZ = 16384;

		// Token: 0x040001E4 RID: 484
		public const int IL_CUBEMAP_NEGATIVEZ = 32768;

		// Token: 0x040001E5 RID: 485
		public const int IL_VERSION_NUM = 3554;

		// Token: 0x040001E6 RID: 486
		public const int IL_IMAGE_WIDTH = 3556;

		// Token: 0x040001E7 RID: 487
		public const int IL_IMAGE_HEIGHT = 3557;

		// Token: 0x040001E8 RID: 488
		public const int IL_IMAGE_DEPTH = 3558;

		// Token: 0x040001E9 RID: 489
		public const int IL_IMAGE_SIZE_OF_DATA = 3559;

		// Token: 0x040001EA RID: 490
		public const int IL_IMAGE_BPP = 3560;

		// Token: 0x040001EB RID: 491
		public const int IL_IMAGE_BYTES_PER_PIXEL = 3560;

		// Token: 0x040001EC RID: 492
		public const int IL_IMAGE_BITS_PER_PIXEL = 3561;

		// Token: 0x040001ED RID: 493
		public const int IL_IMAGE_FORMAT = 3562;

		// Token: 0x040001EE RID: 494
		public const int IL_IMAGE_TYPE = 3563;

		// Token: 0x040001EF RID: 495
		public const int IL_PALETTE_TYPE = 3564;

		// Token: 0x040001F0 RID: 496
		public const int IL_PALETTE_SIZE = 3565;

		// Token: 0x040001F1 RID: 497
		public const int IL_PALETTE_BPP = 3566;

		// Token: 0x040001F2 RID: 498
		public const int IL_PALETTE_NUM_COLS = 3567;

		// Token: 0x040001F3 RID: 499
		public const int IL_PALETTE_BASE_TYPE = 3568;

		// Token: 0x040001F4 RID: 500
		public const int IL_NUM_IMAGES = 3569;

		// Token: 0x040001F5 RID: 501
		public const int IL_NUM_MIPMAPS = 3570;

		// Token: 0x040001F6 RID: 502
		public const int IL_NUM_LAYERS = 3571;

		// Token: 0x040001F7 RID: 503
		public const int IL_ACTIVE_IMAGE = 3572;

		// Token: 0x040001F8 RID: 504
		public const int IL_ACTIVE_MIPMAP = 3573;

		// Token: 0x040001F9 RID: 505
		public const int IL_ACTIVE_LAYER = 3574;

		// Token: 0x040001FA RID: 506
		public const int IL_CUR_IMAGE = 3575;

		// Token: 0x040001FB RID: 507
		public const int IL_IMAGE_DURATION = 3576;

		// Token: 0x040001FC RID: 508
		public const int IL_IMAGE_PLANESIZE = 3577;

		// Token: 0x040001FD RID: 509
		public const int IL_IMAGE_BPC = 3578;

		// Token: 0x040001FE RID: 510
		public const int IL_IMAGE_OFFX = 3579;

		// Token: 0x040001FF RID: 511
		public const int IL_IMAGE_OFFY = 3580;

		// Token: 0x04000200 RID: 512
		public const int IL_IMAGE_CUBEFLAGS = 3581;

		// Token: 0x04000201 RID: 513
		public const int IL_IMAGE_ORIGIN = 3582;

		// Token: 0x04000202 RID: 514
		public const int IL_IMAGE_CHANNELS = 3583;

		// Token: 0x04000203 RID: 515
		public const int IL_SEEK_SET = 0;

		// Token: 0x04000204 RID: 516
		public const int IL_SEEK_CUR = 1;

		// Token: 0x04000205 RID: 517
		public const int IL_SEEK_END = 2;

		// Token: 0x04000206 RID: 518
		public const int IL_EOF = -1;

		// Token: 0x02000038 RID: 56
		// (Invoke) Token: 0x06000260 RID: 608
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void fCloseRProc(IntPtr handle);

		// Token: 0x02000039 RID: 57
		// (Invoke) Token: 0x06000264 RID: 612
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate bool fEofProc(IntPtr handle);

		// Token: 0x0200003A RID: 58
		// (Invoke) Token: 0x06000268 RID: 616
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int fGetcProc(IntPtr handle);

		// Token: 0x0200003B RID: 59
		// (Invoke) Token: 0x0600026C RID: 620
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr fOpenRProc(string str);

		// Token: 0x0200003C RID: 60
		// (Invoke) Token: 0x06000270 RID: 624
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int fReadProc(IntPtr ptr, int a, int b, IntPtr handle);

		// Token: 0x0200003D RID: 61
		// (Invoke) Token: 0x06000274 RID: 628
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int fSeekRProc(IntPtr handle, int a, int b);

		// Token: 0x0200003E RID: 62
		// (Invoke) Token: 0x06000278 RID: 632
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int fTellRProc(IntPtr handle);

		// Token: 0x0200003F RID: 63
		// (Invoke) Token: 0x0600027C RID: 636
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void fCloseWProc(IntPtr handle);

		// Token: 0x02000040 RID: 64
		// (Invoke) Token: 0x06000280 RID: 640
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate IntPtr fOpenWProc(string str);

		// Token: 0x02000041 RID: 65
		// (Invoke) Token: 0x06000284 RID: 644
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int fPutcProc(byte byt, IntPtr handle);

		// Token: 0x02000042 RID: 66
		// (Invoke) Token: 0x06000288 RID: 648
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int fSeekWProc(IntPtr handle, int a, int b);

		// Token: 0x02000043 RID: 67
		// (Invoke) Token: 0x0600028C RID: 652
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int fTellWProc(IntPtr handle);

		// Token: 0x02000044 RID: 68
		// (Invoke) Token: 0x06000290 RID: 656
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int fWriteProc(IntPtr ptr, int a, int b, IntPtr handle);

		// Token: 0x02000045 RID: 69
		// (Invoke) Token: 0x06000294 RID: 660
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void mAlloc(int a);

		// Token: 0x02000046 RID: 70
		// (Invoke) Token: 0x06000298 RID: 664
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate void mFree(IntPtr ptr);

		// Token: 0x02000047 RID: 71
		// (Invoke) Token: 0x0600029C RID: 668
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int IL_LOADPROC(string str);

		// Token: 0x02000048 RID: 72
		// (Invoke) Token: 0x060002A0 RID: 672
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public delegate int IL_SAVEPROC(string str);
	}
}
