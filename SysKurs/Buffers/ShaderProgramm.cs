﻿using System;
using OpenTK.Graphics.OpenGL;

namespace SysKurs
{
    public readonly struct ShaderUniform
    {
        public readonly string Name;
        public readonly int Location;
        public readonly ActiveUniformType Type;
       
        public ShaderUniform(string name, int location, ActiveUniformType type)
        {
            Name = name;
            Location = location;
            Type = type;
        }

    }

    public readonly struct ShaderAttribute
    {
        public readonly string Name;
        public readonly int Location;
        public readonly ActiveAttribType Type;

        public ShaderAttribute(string name, int location, ActiveAttribType type)
        {
            Name = name;
            Location = location;
            Type = type;
        }
    }

    public sealed class ShaderProgramm : IDisposable
    {
        private bool disposed;
        public readonly int ShaderProgrammHandle;
        public readonly int VertexShaderHandle;
        public readonly int FragmentShaderHandle;
        private readonly ShaderUniform[] uniforms;
        private readonly ShaderAttribute[] attributes;

        public ShaderProgramm(string vertexShaderCode, string fragmentShaderCode)
        {
            disposed = false;

            if(!CompileVertexShader(vertexShaderCode,out VertexShaderHandle, out string vertexShaderCompileError))
            {
                throw new ArgumentException(vertexShaderCompileError);
            }

            if (!CompileFragmentShader(fragmentShaderCode, out FragmentShaderHandle, out string fragmentShaderCompileError))
            {
                throw new ArgumentException(fragmentShaderCompileError);
            }

            ShaderProgrammHandle = CreateLinkProgram(VertexShaderHandle, FragmentShaderHandle);

            uniforms = CreateUniformList(ShaderProgrammHandle);

            attributes = CreateAttributeList(ShaderProgrammHandle);
        }

        ~ShaderProgramm()
        {
            Dispose();
        }

        public void Dispose()
        {
            if(disposed) return;
   
            GL.DeleteShader(VertexShaderHandle);
            GL.DeleteShader(FragmentShaderHandle);

            GL.UseProgram(0);
            GL.DeleteProgram(ShaderProgrammHandle);

            disposed = true;
            GC.SuppressFinalize(this);
        }

        public static bool CompileVertexShader(string vertexShaderCode, out int vertexShaderHandle, out string errorMessage)
        {
            errorMessage = string.Empty;

            vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShaderHandle, vertexShaderCode);
            GL.CompileShader(vertexShaderHandle);

            string vertexShaderInfo = GL.GetShaderInfoLog(vertexShaderHandle);

            if (vertexShaderInfo != String.Empty)
            {
                errorMessage = vertexShaderInfo;              
                return false;
            }
            return true;
        }

        public static bool CompileFragmentShader(string fragmentShaderCode, out int fragmentShaderHandle, out string errorMessage)
        {
            errorMessage = string.Empty;

            fragmentShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShaderHandle, fragmentShaderCode);
            GL.CompileShader(fragmentShaderHandle);

            string fragmentShaderInfo = GL.GetShaderInfoLog(fragmentShaderHandle);

            if (fragmentShaderInfo != String.Empty)
            {
                errorMessage = fragmentShaderInfo;
                return false;
            }
            return true;
        }

        public static int CreateLinkProgram(int vertexShaderHandle, int fragmentShaderHandle)
        {
            int shaderProgrammHandle = GL.CreateProgram();
            GL.AttachShader(shaderProgrammHandle, vertexShaderHandle);
            GL.AttachShader(shaderProgrammHandle, fragmentShaderHandle);

            GL.LinkProgram(shaderProgrammHandle);

            GL.DetachShader(shaderProgrammHandle, vertexShaderHandle);
            GL.DetachShader(shaderProgrammHandle, fragmentShaderHandle);

            return shaderProgrammHandle;
        }

        public static ShaderUniform[] CreateUniformList(int shaderProgrammHandle)
        {
            GL.GetProgram(shaderProgrammHandle, GetProgramParameterName.ActiveUniforms, out int uniformCount);

            ShaderUniform[] uniforms = new ShaderUniform[uniformCount];

            for (int i = 0; i < uniformCount; i++)
            {
                GL.GetActiveUniform(shaderProgrammHandle, i, 256, out _, out _,out ActiveUniformType type, out string name);
                int location = GL.GetUniformLocation(shaderProgrammHandle, name);
                uniforms[i] = new ShaderUniform(name, location, type);
                
            }
            return uniforms;
        }

        public static ShaderAttribute[] CreateAttributeList(int shaderProgrammHandle)
        {
            GL.GetProgram(shaderProgrammHandle, GetProgramParameterName.ActiveAttributes, out int attributeCount);

            ShaderAttribute[] attributes = new ShaderAttribute[attributeCount];

            for (int i = 0; i < attributeCount; i++)
            {
                GL.GetActiveAttrib(shaderProgrammHandle, i, 256, out _, out _, out ActiveAttribType type, out string name);
                int location = GL.GetAttribLocation(shaderProgrammHandle, name);
                attributes[i] = new ShaderAttribute(name, location, type);

            }
            return attributes;
        }

        public ShaderUniform[] GetUniformList()
        {
            ShaderUniform[] result = new ShaderUniform[uniforms.Length];
            Array.Copy(uniforms, result, uniforms.Length);
            return result;
        }

        public ShaderAttribute[] GetAttributesList()
        {
            ShaderAttribute[] result = new ShaderAttribute[attributes.Length];
            Array.Copy(attributes, result, attributes.Length);
            return result;
        } 

        public void SetUniform(string name, float v1)
        {
            if(!GetShaderUniform(name, out ShaderUniform uniform))
            {
                throw new ArgumentException("Name was not found");
            }

            if(uniform.Type != ActiveUniformType.Float)
            {
                throw new ArgumentException("uniform type is not float");
            }

            GL.UseProgram(ShaderProgrammHandle);
            GL.Uniform1(uniform.Location, v1);
            GL.UseProgram(0);
        }

        public void SetUniform(string name, float v1, float v2)
        {
            if (!GetShaderUniform(name, out ShaderUniform uniform))
            {
                throw new ArgumentException("Name was not found");
            }

            if (uniform.Type != ActiveUniformType.FloatVec2)
            {
                throw new ArgumentException("uniform type is not float");
            }

            GL.UseProgram(ShaderProgrammHandle);
            GL.Uniform2(uniform.Location, v1, v2);
            GL.UseProgram(0);
        }

        private bool GetShaderUniform(string name, out ShaderUniform uniform)
        {
            uniform = new ShaderUniform();

            for (int i = 0; i < uniforms.Length; i++)
            {
                uniform = uniforms[i];

                if(name == uniform.Name)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
