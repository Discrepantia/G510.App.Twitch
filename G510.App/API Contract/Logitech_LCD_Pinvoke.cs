#define Win86

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace G510.App.API_Contract
{
    public class LedPinvoke
    {
        public const int LOGITECH_LED_MOUSE = 0x0001;
        public const int LOGITECH_LED_KEYBOARD = 0x0002;

        public const int LOGITECH_LED_ALL = LOGITECH_LED_MOUSE | LOGITECH_LED_KEYBOARD;

#if Win64 
        public const string DLLPath = @"Lib\x64\LogitechLed.dll";
#else
#if Win86
        public const string DLLPath = @"Lib\x86\LogitechLed.dll";
#endif
#endif

        [DllImport(DLLPath)]
        public static extern Boolean LogiLedInit();

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern Boolean LogiLedSaveCurrentLighting(Int32 lcdType);

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern Boolean LogiLedSetLighting(Int32 lcdType, Int32 redPercentage, Int32 greenPercentage, Int32 bluePercentage);

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern Boolean LogiLedRestoreLighting(Int32 lcdType);

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern void LogiLedShutdown();
    }

    public class LcdPinvoke
    {

#if Win64 
        public const string DLLPath = @"Lib\x64\LogitechLcd.dll";
#else 
#if Win86
        public const string DLLPath = @"Lib\x86\LogitechLcd.dll";
        #endif
#endif

        public const Int32 LCD_TYPE_MONO = 1;
        public const Int32 LCD_TYPE_COLOR = 2;

        public const Int32 MONO_BUTTON_0 = 1;
        public const Int32 MONO_BUTTON_1 = 2;
        public const Int32 MONO_BUTTON_2 = 4;
        public const Int32 MONO_BUTTON_3 = 8;

        public const Byte MONO_Color_Light = 255;
        public const Byte MONO_Color_Dark = 0;

        public const Int32 COLOR_BUTTON_LEFT = 256;
        public const Int32 COLOR_BUTTON_RIGHT = 512;
        public const Int32 COLOR_BUTTON_OK = 1024;
        public const Int32 COLOR_BUTTON_CANCEL = 2048;
        public const Int32 COLOR_BUTTON_UP = 4096;
        public const Int32 COLOR_BUTTON_DOWN = 8192;
        public const Int32 COLOR_BUTTON_MENU = 16384;

        public const Int32 MONO_WIDTH = 160;
        public const Int32 MONO_HEIGHT = 43;
        public const Int32 MONO_PIXEL_COUNT = MONO_WIDTH * MONO_HEIGHT;

        public const Int32 COLOR_WIDTH = 320;
        public const Int32 COLOR_HEIGHT = 240;
        public const Int32 COLOR_PIXEL_COUNT = COLOR_WIDTH * COLOR_HEIGHT;

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern Boolean LogiLcdInit([MarshalAs(UnmanagedType.LPWStr)] string friendlyName, Int32 lcdType);

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern Boolean LogiLcdIsConnected(Int32 lcdType);

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern Boolean LogiLcdIsButtonPressed(Int32 button);

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern void LogiLcdUpdate();

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean LogiLcdShutdown();

        #region Mono LCD functions
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern Boolean LogiLcdMonoSetBackground([MarshalAs(UnmanagedType.LPArray)] Byte[] monoBitmap);

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern Boolean LogiLcdMonoSetText(Int32 lineNumber, [MarshalAs(UnmanagedType.LPWStr)] string text);

        #region UnrealEngine
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern Boolean LogiLcdMonoResetBackgroundUDK();

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern Boolean LogiLcdMonoSetBackgroundUDK([MarshalAs(UnmanagedType.LPArray)] Byte[] monoBitmap, int arraySize);
        #endregion
        #endregion


        #region Color LCD functions
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool LogiLcdColorSetBackground([MarshalAs(UnmanagedType.LPArray)] Byte[] colorBitmap);

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern Boolean LogiLcdColorSetTitle([MarshalAs(UnmanagedType.LPWStr)] string text, Int32 red, Int32 green, Int32 blue);

        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern Boolean LogiLcdColorSetText(Int32 lineNumber, [MarshalAs(UnmanagedType.LPWStr)] string text, Int32 red, Int32 green, Int32 blue);
        #region UnrealEngine
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern Boolean LogiLcdColorSetBackgroundUDK();
        [DllImport(DLLPath, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern Boolean LogiLcdColorResetBackgroundUDK([MarshalAs(UnmanagedType.LPArray)] Byte[] monoBitmap, int arraySize);


        #endregion
        #endregion
    }

    public class LogiLcdException : Exception
    {
        public LogiLcdException() : base() { }
        public LogiLcdException(string msg) : base(msg) { }
    }

    public class LogiLcd : IDisposable
    {
        public static byte[] bgByte { get { return new Byte[LogiLcd.MonoWidth * LogiLcd.MonoHeight]; } }

        [Flags]
        public enum LcdType
        {
            Mono = LcdPinvoke.LCD_TYPE_MONO,
            Color = LcdPinvoke.LCD_TYPE_COLOR
        }

        [Flags]
        public enum LcdButton
        {
            Mono0 = LcdPinvoke.MONO_BUTTON_0,
            Mono1 = LcdPinvoke.MONO_BUTTON_1,
            Mono2 = LcdPinvoke.MONO_BUTTON_2,
            Mono3 = LcdPinvoke.MONO_BUTTON_3,
            ColorLeft = LcdPinvoke.COLOR_BUTTON_LEFT,
            ColorRight = LcdPinvoke.COLOR_BUTTON_RIGHT,
            ColorOk = LcdPinvoke.COLOR_BUTTON_OK,
            ColorCancel = LcdPinvoke.COLOR_BUTTON_CANCEL,
            ColorUp = LcdPinvoke.COLOR_BUTTON_UP,
            ColorDown = LcdPinvoke.COLOR_BUTTON_DOWN,
            ColorMenu = LcdPinvoke.COLOR_BUTTON_MENU
        }

        public enum LcdMonoColr
        {
            Light = LcdPinvoke.MONO_Color_Light,
            Dark = LcdPinvoke.MONO_Color_Dark
        }

        public const int MonoWidth = LcdPinvoke.MONO_WIDTH;
        public const int MonoHeight = LcdPinvoke.MONO_HEIGHT;
        public const int ColorWidth = LcdPinvoke.COLOR_WIDTH;
        public const int ColorHeight = LcdPinvoke.COLOR_HEIGHT;

        private static bool hasInstance = false;
        private bool disposed = false;

        public LogiLcd(string friendlyName, LcdType lcdtype)
        {
            if (hasInstance)
                throw new LogiLcdException("There can only be one instance of this class at a time!");

            if (!LcdPinvoke.LogiLcdInit(friendlyName, (int)lcdtype))
                throw new LogiLcdException("LogiLcdInit failed");

            if (!LedPinvoke.LogiLedInit())
                throw new LogiLcdException("LogiLedInit failed");

            hasInstance = true;
        }

        public LogiLcd(string friendlyName)
            : this(friendlyName, LcdType.Mono)
        {
        }

        public void Dispose()
        {
            if (disposed)
                return;

            LcdPinvoke.LogiLcdShutdown();
            LedPinvoke.LogiLedRestoreLighting(LedPinvoke.LOGITECH_LED_ALL);
            LedPinvoke.LogiLedShutdown();

            hasInstance = false;

            disposed = true;
        }

        public Boolean StoreColor()
        {
            return LedPinvoke.LogiLedSaveCurrentLighting(LedPinvoke.LOGITECH_LED_ALL);
        }

        public Boolean ChangeColor(Int32 _redPercentage, Int32 _greenPercentage, Int32 _bluePercentage)
        {
            return LedPinvoke.LogiLedSetLighting(LedPinvoke.LOGITECH_LED_ALL, _redPercentage,  _greenPercentage, _bluePercentage);
        }

        public Boolean RestoreColor()
        {
            return LedPinvoke.LogiLedRestoreLighting(LedPinvoke.LOGITECH_LED_ALL);
        }



        ~LogiLcd()
        {
            Dispose();
        }

        public bool IsConnected(LcdType lcdtype)
        {
            return LcdPinvoke.LogiLcdIsConnected((int)lcdtype);
        }

        public bool IsButtonPressed(LcdButton button)
        {
            return LcdPinvoke.LogiLcdIsButtonPressed((int)button);
        }

        public void Update()
        {
            LcdPinvoke.LogiLcdUpdate();
        }

        public bool MonoSetBackground(Byte[] monoBitmap)
        {
            if (monoBitmap.Length != MonoWidth * MonoHeight)
                throw new LogiLcdException("Bitmap size does not match expected size");

            return LcdPinvoke.LogiLcdMonoSetBackground(monoBitmap);
        }

        public Boolean MonoClear()
        {
            return LcdPinvoke.LogiLcdMonoResetBackgroundUDK();
        }

        public bool MonoSetBackground(Bitmap bitmap)
        {
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                throw new LogiLcdException("Bitmap PixelFormat has to be 32bppArgb");

            if (bitmap.Width != MonoWidth || bitmap.Height != MonoHeight)
                throw new LogiLcdException("Bitmap size does not match the mono LCD size");

            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int srcBytes = Math.Abs(data.Stride) * bitmap.Height;
            Byte[] rgbData = new Byte[srcBytes];
            Marshal.Copy(data.Scan0, rgbData, 0, srcBytes);

            Byte[] resultData = new Byte[bitmap.Width * bitmap.Height];

            for (int y = 0, ypos = 0; y < bitmap.Height; ++y, ypos += Math.Abs(data.Stride))
                for (int x = 0, pos = ypos; x < bitmap.Width; ++x, pos += 4)
                {
                    byte b = rgbData[pos + 0];
                    byte g = rgbData[pos + 1];
                    byte r = rgbData[pos + 2];

                    resultData[y * bitmap.Width + x] = (byte)((0.2125 * r) + (0.7154 * g) + (0.0721 * b));
                }

            bitmap.UnlockBits(data);

            return MonoSetBackground(resultData);
        }

        public bool MonoSetText(int lineNumber, string text)
        {
            return LcdPinvoke.LogiLcdMonoSetText(lineNumber, text);
        }

        public bool ColorSetBackground(Byte[] colorBitmap)
        {
            if (colorBitmap.Length != ColorWidth * ColorHeight)
                throw new LogiLcdException("Bitmap size does not match expected size");

            return LcdPinvoke.LogiLcdColorSetBackground(colorBitmap);
        }

        public bool ColorSetTitle(string text, byte red = 255, byte green = 255, byte blue = 255)
        {
            return LcdPinvoke.LogiLcdColorSetTitle(text, red, green, blue);
        }

        public bool ColorSetText(int lineNumber, string text, byte red = 255, byte green = 255, byte blue = 255)
        {
            return LcdPinvoke.LogiLcdColorSetText(lineNumber, text, red, green, blue);
        }

        void IDisposable.Dispose()
        {
            Dispose();
        }
    }
}
