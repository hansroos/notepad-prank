using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace NotepadPrank
{
    internal static class Program
    {
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private static IntPtr notepadWindow;

        [STAThread]
        private static void Main()
        {
            Process notepadProcess = new Process();
            notepadProcess.StartInfo.FileName = "notepad.exe";
            notepadProcess.Start();
            notepadProcess.WaitForInputIdle();
            notepadWindow = notepadProcess.MainWindowHandle;

            WriteToNotepad("Hi,");
            WriteToNotepad(Environment.NewLine);
            WriteToNotepad("This is a simple prank with notepad.");

            WriteToNotepad(Environment.NewLine);
            WriteToNotepad("Hope you enjoy it!");
        }

        public static void WriteToNotepad(string str)
        {
            var r = new Random();
            foreach (var c in str.ToCharArray())
            {
                SetForegroundWindow(notepadWindow);
                SendKeys.SendWait(c.ToString());

                Thread.Sleep(r.Next(50, 200));
            }
        }
    }
}