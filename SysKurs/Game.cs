using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Drawing;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class Game : GameWindow
{
    private int vertexBufferHandle; //Хранение на видеокарте
    private int shaderProgramHandle;
    private int vertexArrayHandle;

    public Game(int width, int height, string title) : base(GameWindowSettings.Default,
    new NativeWindowSettings()
    {
        Size = (width, height),
        Title = title,
        WindowBorder = WindowBorder.Fixed,
        StartVisible = false,
        API = ContextAPI.OpenGL,
        Profile = ContextProfile.Core,
        APIVersion = new Version(3, 3)
    })
    {
        this.CenterWindow();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        KeyboardState input = KeyboardState;

        if (input.IsKeyDown(Keys.Escape))
            Close();

    }

    protected override void OnLoad()
    {
        IsVisible = true;
        GL.ClearColor(Color.Black);

        float[] vertexes = new float[]
        {
            0.0f,  0.5f,  0f, 1f, 0f, 0f, 1f,
            0.5f, -0.5f, 0f, 0f, 1f, 0f, 1f,
           -0.5f, -0.5f, 0f, 0f, 0f, 1f, 1f
        };

        vertexBufferHandle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferHandle);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexes.Length * sizeof(float), vertexes, BufferUsageHint.StaticDraw);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

        vertexArrayHandle = GL.GenVertexArray();
        GL.BindVertexArray(vertexArrayHandle);

        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferHandle);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0);
        GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 7 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(0);
        GL.EnableVertexAttribArray(1);

        GL.BindVertexArray(0);

        string vertexShaderCode =
            @"
            #version 330 core
                
            layout (location = 0) in vec3 aPosition;
            layout (location = 1) in vec4 aColor;
            
            out vec4 vColor;

            void main(void)
            {
                 vColor = aColor;
                 gl_Position = vec4(aPosition, 1.0f);
            }
            ";

        string fragmentShaderCode =
            @"
            #version 330 core
                
            in vec4 vColor;
            out vec4 pixelColor;

            void main()
            {
                pixelColor = vColor;
            }
            ";

        int vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShaderHandle, vertexShaderCode);
        GL.CompileShader(vertexShaderHandle);

        int fragmentShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShaderHandle, fragmentShaderCode);
        GL.CompileShader(fragmentShaderHandle);

        shaderProgramHandle = GL.CreateProgram();
        GL.AttachShader(shaderProgramHandle, vertexShaderHandle);
        GL.AttachShader(shaderProgramHandle, fragmentShaderHandle);

        GL.LinkProgram(shaderProgramHandle);

        GL.DetachShader(shaderProgramHandle, vertexShaderHandle);
        GL.DetachShader(shaderProgramHandle, fragmentShaderHandle);

        GL.DeleteShader(vertexShaderHandle);
        GL.DeleteShader(fragmentShaderHandle);


        base.OnLoad();
    }

    protected override void OnUnload()
    {
        GL.BindVertexArray(0);
        GL.DeleteVertexArray(vertexArrayHandle);

        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.DeleteBuffer(vertexBufferHandle);

        GL.UseProgram(0);
        GL.DeleteProgram(shaderProgramHandle);
        base.OnUnload();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, e.Width, e.Height);
        base.OnResize(e);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);

        GL.UseProgram(shaderProgramHandle);
        GL.BindVertexArray(vertexArrayHandle);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

        Context.SwapBuffers();
        base.OnRenderFrame(args);
    }

}