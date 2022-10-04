﻿using System;
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
using SysKurs.Common;

public class Window : GameWindow
{
    //Drawing
    private VertexBuffer vertexBuffer;
    private ShaderProgramm shaderProgram;
    private VertexArray vertexArray;
    private IndexBuffer indexBuffer;
  
    //Camera
    private Camera _camera;
    private bool _firstMove = true;
    private Vector2 _lastPos;

    //Rotation
    private double _time;


    public Window(int width, int height, string title) : base(GameWindowSettings.Default,
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
        if (!IsFocused)
            return;
          
        if (input.IsKeyDown(Keys.Escape))
            Close();


        const float cameraSpeed = 3.0f;
        const float sensitivity = 0.2f;

        if (input.IsKeyDown(Keys.D)) // Right
            _camera.Position += _camera.Right * cameraSpeed * (float)e.Time;
        if (input.IsKeyDown(Keys.A)) //Left
            _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time;
        if (input.IsKeyDown(Keys.Space)) // Up
            _camera.Position += _camera.Up * cameraSpeed * (float)e.Time;
        if (input.IsKeyDown(Keys.LeftShift)) // Down
            _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time;
        if (input.IsKeyDown(Keys.S)) //Back
            _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time;
        if (input.IsKeyDown(Keys.W)) //Forward
            _camera.Position += _camera.Front * cameraSpeed * (float)e.Time;

        var mouse = MouseState;

        if(_firstMove)
        {
            _lastPos = new Vector2(mouse.X, mouse.Y);
            _firstMove = false;
        }
        else
        {
            var deltaX = mouse.X - _lastPos.X;
            var deltaY = mouse.Y - _lastPos.Y;

            _lastPos = new Vector2(mouse.X, mouse.Y);

            _camera.Yaw += deltaX * sensitivity;
            _camera.Pitch -= deltaY * sensitivity;
        }

        //Console.WriteLine(_camera.Position);
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        IsVisible = true;
        GL.ClearColor(Color.LightGray);    
        GL.Enable(EnableCap.DepthTest);
    
        Cube.CountIndexes();

        vertexBuffer = new VertexBuffer(VertexPositionColor.VertexInfo, Cube.vertexes.Length);
        vertexBuffer.SetData(Cube.vertexes, Cube.vertexes.Length);

        indexBuffer = new IndexBuffer(Cube.indexes.Length);
        indexBuffer.SetData(Cube.indexes, Cube.indexes.Length);

        vertexArray = new VertexArray(vertexBuffer);

        string vertexShaderCode =
            @"
            #version 330 core
                
            uniform mat4 model;
            uniform mat4 view;
            uniform mat4 projection;

            layout (location = 0) in vec3 aPosition;
            layout (location = 1) in vec4 aColor;
            
            
            out vec4 vColor;

            void main(void)
            {
                 vColor = aColor;         
                 gl_Position = vec4(aPosition,1.0f) * model * view * projection;                   
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

        //_view = Matrix4.CreateTranslation(0.0f,0.0f,-3.0f);
        _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

        CursorState = CursorState.Grabbed;    
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
        _camera.AspectRatio = Size.X / (float)Size.Y;
        base.OnResize(e);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        _time += 4.0 * args.Time;

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        GL.UseProgram(shaderProgram.ShaderProgrammHandle);
        GL.BindVertexArray(vertexArray.VertexArrayHandle);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, indexBuffer.IndexBufferHandle);

        var _model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time));

        shaderProgram.SetMatrix4("model",_model);
        shaderProgram.SetMatrix4("view", _camera.GetViewMatrix());
        shaderProgram.SetMatrix4("projection", _camera.GetProjectionMatrix());

        GL.DrawElements(PrimitiveType.Triangles, Cube.indexCount, DrawElementsType.UnsignedInt, 0);

        Context.SwapBuffers();
    }

    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
        base.OnMouseWheel(e);

        _camera.Fov -= e.OffsetY;
    }

}