using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Drawing;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SysKurs;
using SysKurs.Buffers;
using System.Reflection;

public class Game : GameWindow
{
    private VertexBuffer vertexBuffer;
    private ShaderProgramm shaderProgram;
    private VertexArray vertexArray;
    private IndexBuffer indexBuffer;
    private static Matrix4 _projection;
    private static Matrix4 _world;
    private static Matrix4 _view;
    private double _time;


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
        CursorState = CursorState.Grabbed;

        if (!IsFocused)
            return;
        KeyboardState input = KeyboardState;
        var a = Vector3.Zero;

        if (input.IsKeyDown(Keys.Escape))
            Close();

        if (input.IsKeyDown(Keys.D)) // Right
            a.X -= 1;
        if (input.IsKeyDown(Keys.A)) //Left
            a.X += 1;
        if (input.IsKeyDown(Keys.Space)) // Up
            a.Y -= 1;
        if (input.IsKeyDown(Keys.LeftShift)) // Down
            a.Y += 1;
        if (input.IsKeyDown(Keys.S)) //Back
            a.Z -= 1;
        if (input.IsKeyDown(Keys.W)) //Forward
            a.Z += 1;
      
        if (a.LengthSquared > 0.0001f )
          _world *= Matrix4.CreateTranslation(a.Normalized()*0.05f);

        Console.WriteLine(a);
    }

    protected override void OnLoad()
    {
        IsVisible = true;
        GL.ClearColor(Color.LightGray);    
        GL.Enable(EnableCap.DepthTest);

        _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size.X / (float)Size.Y, 0.1f, 100.0f);
        _world = Matrix4.Identity;


        Cube.CountIndexes();

        vertexBuffer = new VertexBuffer(VertexPositionColor.VertexInfo, Cube.vertexes.Length);
        vertexBuffer.SetData(Cube.vertexes, Cube.vertexes.Length);

        indexBuffer = new IndexBuffer(Cube.indexes.Length);
        indexBuffer.SetData(Cube.indexes, Cube.indexes.Length);

        vertexArray = new VertexArray(vertexBuffer);

        string vertexShaderCode =
            @"
            #version 330 core
                
            uniform mat4 uMVP;
            uniform mat4 model;
            uniform mat4 view;

            layout (location = 0) in vec3 aPosition;
            layout (location = 1) in vec4 aColor;
            
            
            out vec4 vColor;

            void main(void)
            {
                 vColor = aColor;         
                 gl_Position = vec4(aPosition,1.0f) * model * view * uMVP;                   
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

        shaderProgram = new ShaderProgramm(vertexShaderCode, fragmentShaderCode);

        _view = Matrix4.CreateTranslation(0.0f,0.0f,-3.0f);

        base.OnLoad();
    }

    protected override void OnUnload()
    {
        vertexArray?.Dispose();
        indexBuffer?.Dispose();
        vertexBuffer?.Dispose();
        shaderProgram?.Dispose();
       
        base.OnUnload();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, e.Width, e.Height);
        base.OnResize(e);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        _time += 10.0 * args.Time;

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        GL.UseProgram(shaderProgram.ShaderProgrammHandle);
        GL.BindVertexArray(vertexArray.VertexArrayHandle);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer.IndexBufferHandle);

        var  _model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time));
        var mvp = _world * _projection;
        // GL.UniformMatrix4(0, false, ref mvp);

        int location = GL.GetUniformLocation(shaderProgram.ShaderProgrammHandle,"model");
        GL.UniformMatrix4(location,false, ref _model);
        location = GL.GetUniformLocation(shaderProgram.ShaderProgrammHandle, "view");
        GL.UniformMatrix4(location, false, ref _view);
        location = GL.GetUniformLocation(shaderProgram.ShaderProgrammHandle, "uMVP");
        GL.UniformMatrix4(location, false, ref mvp);


        GL.DrawElements(PrimitiveType.Triangles, Cube.indexCount, DrawElementsType.UnsignedInt, 0);

        Context.SwapBuffers();
       
    }

}