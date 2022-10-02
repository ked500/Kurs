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
using SysKurs;
using SysKurs.Buffers;

public class Game : GameWindow
{
    private VertexBuffer vertexBuffer;
    private ShaderProgramm shaderProgram;
    private VertexArray vertexArray;
    private IndexBuffer indexBuffer;
    private static Matrix4 _projection;
    private static Matrix4 _world;

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
        var a = Vector3.Zero;

        if (input.IsKeyDown(Keys.Escape))
            Close();

        if (input.IsKeyDown(Keys.D))
            a.X -= 1;
        if (input.IsKeyDown(Keys.A))
            a.X += 1;
        if (input.IsKeyDown(Keys.LeftShift))
            a.Y -= 1;
        if (input.IsKeyDown(Keys.C))
            a.Y += 1;
        if (input.IsKeyDown(Keys.S))
            a.Z -= 1;
        if (input.IsKeyDown(Keys.W))
            a.Z += 1;

        if(a.LengthSquared > 0.0001f )
          _world *= Matrix4.CreateTranslation(a.Normalized()*0.05f);
    }

    protected override void OnLoad()
    {
        IsVisible = true;
        GL.ClearColor(Color.LightGray);
        _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2,16f/9,0.01f,100f);
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
            layout (location = 0) in vec3 aPosition;
            layout (location = 1) in vec4 aColor;
            
            
            out vec4 vColor;

            void main(void)
            {
                 vColor = aColor;         
                 gl_Position = uMVP * vec4(aPosition,1.0f);                   
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

        //int[] viewport = new int[4];
        //GL.GetInteger(GetPName.Viewport, viewport);

        //shaderProgram.SetUniform("ViewportSize", (float)viewport[2], (float)viewport[3]);
 
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
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        GL.UseProgram(shaderProgram.ShaderProgrammHandle);
        GL.BindVertexArray(vertexArray.VertexArrayHandle);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer.IndexBufferHandle);
        var mvp = _world * _projection;
        GL.UniformMatrix4(0,false,ref mvp);
        GL.DrawElements(PrimitiveType.Triangles, Cube.indexCount, DrawElementsType.UnsignedInt, 0);

        Context.SwapBuffers();
        base.OnRenderFrame(args);
    }

}