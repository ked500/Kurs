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
using SysKurs.Common;
using SysKurs.Model;

public class Window : GameWindow
{
    //Cube
    private VertexBuffer CubeVertexBuffer;
    private ShaderProgramm CubeShaderProgramm;
    private VertexArray CubeVertexArray;
    private IndexBuffer CubeIndexBuffer;

    //Lamp
    private VertexBuffer LampVertexBuffer;
    private ShaderProgramm LampShaderProgramm;
    private VertexArray LampVertexArray;
    private IndexBuffer LampIndexBuffer;

    //Textures
    private Texture _texture; //Without Lighting
    private Texture _diffuseMap;
    private Texture _specularMap;

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
            _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time;
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

    }

    protected override void OnLoad()
    {
        base.OnLoad();

        IsVisible = true;
        GL.ClearColor(Color.Black);   
        GL.Enable(EnableCap.DepthTest);

        //RGB Cube
        //Cube.CreateRGBCube(out CubeVertexBuffer, out CubeIndexBuffer, out CubeVertexArray, out CubeShaderProgramm);

        //Textured Cube
        //Cube.CreateTexturedCube(out CubeVertexBuffer, out CubeIndexBuffer, out CubeVertexArray, out CubeShaderProgramm, out _texture);

        //Static Cube
        Cube.CreateCubeForLighting(out CubeVertexBuffer, out CubeIndexBuffer, out CubeVertexArray, out CubeShaderProgramm, out _diffuseMap, out _specularMap);

        //Lamp 
        Lamp.CreateLamp(out LampVertexBuffer, out LampIndexBuffer, out LampVertexArray, out LampShaderProgramm);

        //Camera
        _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y);

        CursorState = CursorState.Grabbed;
    }

    protected override void OnUnload()
    {
        CubeVertexArray?.Dispose();
        CubeIndexBuffer?.Dispose();
        CubeVertexBuffer?.Dispose();
        CubeShaderProgramm?.Dispose();
       
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
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        //SpinCube(args);
        DrawCube(args);
        DrawLamp(args);

      
        Context.SwapBuffers();
    }

    protected override void OnMouseWheel(MouseWheelEventArgs e)
    {
        base.OnMouseWheel(e);

        _camera.Fov -= e.OffsetY;
    }

    private void SpinCube(FrameEventArgs args)
    {
        _time += 4.0 * args.Time;

        GL.UseProgram(CubeShaderProgramm.ShaderProgrammHandle);
        GL.BindVertexArray(CubeVertexArray.VertexArrayHandle);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, CubeIndexBuffer.IndexBufferHandle);


        var _model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time));

        CubeShaderProgramm.SetMatrix4("model", _model);
        CubeShaderProgramm.SetMatrix4("view", _camera.GetViewMatrix());
        CubeShaderProgramm.SetMatrix4("projection", _camera.GetProjectionMatrix());

        GL.DrawElements(PrimitiveType.Triangles, Cube.indexCount, DrawElementsType.UnsignedInt, 0);
    }

    private void DrawCube(FrameEventArgs args)
    {
        _time += 10.0 * args.Time;

        GL.UseProgram(CubeShaderProgramm.ShaderProgrammHandle);
        GL.BindVertexArray(CubeVertexArray.VertexArrayHandle);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, CubeIndexBuffer.IndexBufferHandle);
   
        var _model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(_time));

        CubeShaderProgramm.SetMatrix4("model", _model);
        CubeShaderProgramm.SetMatrix4("view", _camera.GetViewMatrix());
        CubeShaderProgramm.SetMatrix4("projection", _camera.GetProjectionMatrix());

        CubeShaderProgramm.SetVector3("viewPos", _camera.Position);

        CubeShaderProgramm.SetInt("material.diffuse", 0);
        CubeShaderProgramm.SetInt("material.specular", 1);
        CubeShaderProgramm.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
        CubeShaderProgramm.SetFloat("material.shininess", 32.0f);

        SetPointLightShader(CubeShaderProgramm);
        SetSpotLighShader(CubeShaderProgramm);
         
        GL.DrawElements(PrimitiveType.Triangles, Cube.indexCount, DrawElementsType.UnsignedInt, 0);
    }

    private void DrawLamp(FrameEventArgs args)
    {
        GL.UseProgram(LampShaderProgramm.ShaderProgrammHandle);
        GL.BindVertexArray(LampVertexArray.VertexArrayHandle);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, LampIndexBuffer.IndexBufferHandle);

        Matrix4 lampMatrix = Matrix4.Identity;
        lampMatrix *= Matrix4.CreateScale(0.2f);
        lampMatrix *= lampMatrix * Matrix4.CreateTranslation(new Vector3(1.2f,1.0f,2.0f));

        LampShaderProgramm.SetMatrix4("model", lampMatrix);
        LampShaderProgramm.SetMatrix4("view", _camera.GetViewMatrix());
        LampShaderProgramm.SetMatrix4("projection", _camera.GetProjectionMatrix());

        GL.DrawElements(PrimitiveType.Triangles, Lamp.indexCount, DrawElementsType.UnsignedInt, 0);
    }

    private void SetSpotLighShader(ShaderProgramm shader)
    {
        shader.SetVector3("spotLight.position", _camera.Position);
        shader.SetVector3("spotLight.direction", _camera.Front);
        shader.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
        shader.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
        shader.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
        shader.SetFloat("spotLight.constant", 1.0f);
        shader.SetFloat("spotLight.linear", 0.09f);
        shader.SetFloat("spotLight.quadratic", 0.032f);
        shader.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
        shader.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(17.5f)));
    }

    private void SetPointLightShader(ShaderProgramm shader)
    {
        shader.SetVector3("pointLight.position", new Vector3(1.2f, 1.0f, 2.0f));
        shader.SetVector3("pointLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
        shader.SetVector3("pointLight.diffuse", new Vector3(0.8f, 0.8f, 0.8f));
        shader.SetVector3("pointLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
        shader.SetFloat("pointLight.constant", 1.0f);
        shader.SetFloat("pointLight.linear", 0.09f);
        shader.SetFloat("pointLight.quadratic", 0.032f);
    }
}