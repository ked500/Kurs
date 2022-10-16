using OpenTK.Graphics.OpenGL;
using OpenTK;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;
using System.Runtime.InteropServices;


class Program
{
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [STAThread]

    static void Main(string[] args)
    {
        var handle = GetConsoleWindow();
        ShowWindow(handle, 1);

        using (Window game = new Window(1280, 768, "Курсовая"))
        {
            game.Run();
        }
    }
    
}
