using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public struct API
{
    [DllImport("user32.dll")]
    public static extern bool GetAsyncKeyState(short vKey);

    [DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(IntPtr handle, IntPtr Address, IntPtr buffer, int Size, out IntPtr lpNumberOfBytesReaded);

    [DllImport("kernel32.dll")]
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size, out IntPtr lpNumberOfBytesWritten);
}
