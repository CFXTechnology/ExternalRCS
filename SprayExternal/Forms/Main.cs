using SprayExternal.Math;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SprayExternal
{
 
   public partial class Main : Form
    {
        int ShotsFired = 0;
        Vector3 Angle;
        Vector3 AimPunch;
        Vector3 OldAngle;
        IntPtr ClientDll, EngineDll;
        IntPtr LocalPlayer, ClientState;

        public Main()
        {
            InitializeComponent();
            Memory.Initialize("csgo");
            new Thread(() => RCS()).Start();
        }

        public static Vector3 ClampAngle(Vector3 Angle)
        {
            if (Angle[0] > 89.0f)
                Angle[0] = 89.0f;

            if (Angle[0] < -89.0f)
                Angle[0] = -89.0f;

            while (Angle[1] > 180)
                Angle[1] -= 360;

            while (Angle[1] < -180)
                Angle[1] += 360;

            Angle.Z = 0;

            return Angle;
        }

        public void RCS()
        {
            while (true)
            {
                if (ClientDll == IntPtr.Zero) ClientDll = Memory.GetModuleBaseAddress("client.dll");
                if (EngineDll == IntPtr.Zero) EngineDll = Memory.GetModuleBaseAddress("engine.dll");
                if (LocalPlayer == IntPtr.Zero) LocalPlayer = Memory.Read<IntPtr>(ClientDll + Offsets.BasePlayer);
                if (ClientState == IntPtr.Zero) ClientState = Memory.Read<IntPtr>(EngineDll + Offsets.ClientState);

                if (API.GetAsyncKeyState(0x01)) ControlSpray();
                Thread.Sleep(10);
            }
        }

        public void ControlSpray()
        {
            ShotsFired = Memory.Read<int>(LocalPlayer + Offsets.m_shotsFired);
            if (ShotsFired > 1)
            {
                Angle = Memory.Read<Vector3>(LocalPlayer + Offsets.m_aimPunchAngle);                
                AimPunch = OldAngle - Angle * 2f;
                ClampAngle(AimPunch);
                Memory.Write<Vector3>((IntPtr)ClientState + Offsets.m_viewPunchAngle, AimPunch);
            }
            else
            {
                OldAngle = Memory.Read<Vector3>(ClientState + Offsets.m_viewPunchAngle);
            }
        }
    }
}
