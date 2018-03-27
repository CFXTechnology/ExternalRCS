using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public struct Memory
{
    private static Process GameProcess { get; set; }
    private static long BytesWritten { get; set; }
    private static bool UseUnsafeReadWrite { get; set; }

    public static void Initialize(string ProcessName)
    {
        if (Process.GetProcessesByName(ProcessName).Length > 0)
            GameProcess = Process.GetProcessesByName(ProcessName)[0];
        else
            Environment.Exit(1);
    }

    public static IntPtr GetModuleBaseAddress(string ModuleName)
    {
        foreach (ProcessModule p in GameProcess.Modules)
        {
            if (p.ModuleName.ToLower() == ModuleName.ToLower())
            {
                return p.BaseAddress;
            }
        }
        return IntPtr.Zero;
    }

    public static T Read<T>(IntPtr ptr, T defVal = default(T)) where T : struct
    {
        T val = defVal;
        IntPtr bytes;

        var size = Marshal.SizeOf(typeof(T));
        var buf = Marshal.AllocHGlobal(size);
        if (API.ReadProcessMemory(GameProcess.Handle, ptr, buf, size, out bytes))
            val = (T)Marshal.PtrToStructure(buf, typeof(T));

        Marshal.FreeHGlobal(buf);

        return val;
    }

    private static void Write(IntPtr address, byte[] data)
    {
        IntPtr numBytes = IntPtr.Zero;
        bool result = API.WriteProcessMemory(GameProcess.Handle, address, data, data.Length, out numBytes);
        BytesWritten += numBytes.ToInt32();
        if (!result)
            throw new Win32Exception(Marshal.GetLastWin32Error());
    }

    private static void Write(IntPtr address, byte[] data, int offset, int length)
    {
        byte[] writeData = new byte[length];
        Array.Copy(data, offset, writeData, 0, writeData.Length);
        Write((IntPtr)(address.ToInt32() + offset), writeData);
    }

    public static void Write<T>(IntPtr address, T value) where T : struct
    {
        Write(address, TToBytes<T>(value));
    }

    public static void Write<T>(IntPtr address, T value, int offset, int length) where T : struct
    {
        byte[] data = TToBytes<T>(value);
        Write(address, data, offset, length);
    }

    private static unsafe byte[] TToBytes<T>(T value) where T : struct
    {
        int size = Marshal.SizeOf(typeof(T));
        byte[] data = new byte[size];

        if (UseUnsafeReadWrite)
        {
            fixed (byte* b = data)
                Marshal.StructureToPtr(value, (IntPtr)b, true);
        }
        else
        {
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(value, ptr, true);
            Marshal.Copy(ptr, data, 0, size);
            Marshal.FreeHGlobal(ptr);
        }

        return data;
    }
}
