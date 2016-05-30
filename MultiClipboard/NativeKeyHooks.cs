using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MultiClipboard
{
    public enum HookType : int
    {
        WH_JOURNALRECORD = 0,
        WH_JOURNALPLAYBACK = 1,
        WH_KEYBOARD = 2,
        WH_GETMESSAGE = 3,
        WH_CALLWNDPROC = 4,
        WH_CBT = 5,
        WH_SYSMSGFILTER = 6,
        WH_MOUSE = 7,
        WH_HARDWARE = 8,
        WH_DEBUG = 9,
        WH_SHELL = 10,
        WH_FOREGROUNDIDLE = 11,
        WH_CALLWNDPROCRET = 12,
        WH_KEYBOARD_LL = 13,
        WH_MOUSE_LL = 14
    }
    public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

    enum KeyPressType
    {
        Pressed = 0,
        Unpressed = 3
    }
    internal class KeyPressedEventArgs : EventArgs
    {
        public Keys Key { get; private set; }
        public KeyPressType KeyPressType { get; private set; }
        public KeyPressedEventArgs(Keys key, KeyPressType type)
        {
            Key = key;
            KeyPressType = type;
        }
    }
    internal class NativeKeyHooks
    {
        private HookProc myCallbackDelegate = null;

        public NativeKeyHooks()
        {
            // initialize our delegate
            this.myCallbackDelegate = new HookProc(this.MyCallbackFunction);

            // setup a keyboard hook
            SetWindowsHookEx(HookType.WH_KEYBOARD, this.myCallbackDelegate, IntPtr.Zero, AppDomain.GetCurrentThreadId());
        }

        [DllImport("user32.dll")]
        protected static extern IntPtr SetWindowsHookEx(HookType code, HookProc func, IntPtr hInstance, int threadID);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr MyCallbackFunction(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0)
            {
                return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            }
            Keys keyPressed = (Keys)wParam.ToInt32();
            Debug.WriteLine("key: "+keyPressed+" code: "+code+" l: "+lParam.ToInt32());
            InvokeKeyPressed(keyPressed, (KeyPressType)code);
            return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        public event EventHandler<KeyPressedEventArgs> KeyPressed = ((sender, args) => { });

        private void InvokeKeyPressed(Keys key, KeyPressType type)
        {
            KeyPressed(this,new KeyPressedEventArgs(key, type));
        }
    }
}
