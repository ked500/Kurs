using System;
using OpenTK.Graphics.OpenGL;

namespace SysKurs
{
    public sealed class VertexBuffer : IDisposable
    {
        public static readonly int MinVertexCount = 1;
        public static readonly int MaxVertexCount = 100_000;

        private bool disposed;

        public readonly int VertexBufferHandle;
        public readonly int VertexCount;
        public readonly VertexInfo VertexInfo;
        public readonly bool IsStatic;

        public VertexBuffer(VertexInfo vertexInfo, int vertexCount, bool isStatic = true)
        {
            disposed = false;

            if(vertexCount < MinVertexCount || vertexCount > MaxVertexCount)
            {
                throw new ArgumentOutOfRangeException(nameof(vertexCount));
            }

            VertexInfo = vertexInfo;
            VertexCount = vertexCount;
            IsStatic = isStatic;

            BufferUsageHint hint = BufferUsageHint.StaticDraw;

            if(!IsStatic)
            {
                hint = BufferUsageHint.StreamDraw;
            }

            VertexBufferHandle = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferHandle);
            GL.BufferData(BufferTarget.ArrayBuffer, VertexCount * VertexInfo.SizeInBytes, IntPtr.Zero, hint);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        ~VertexBuffer()
        {
            Dispose();
        }

        public void Dispose()
        {
            if(disposed)
                return;

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferHandle);

            disposed = true;
            GC.SuppressFinalize(this);
        }

        public void SetData<T>(T[] data, int count) where T : struct
        {
            if(typeof(T) != this.VertexInfo.Type)
            {
                throw new ArgumentException("Generic type 'T' does not match the vertex type");
            }

            if(data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if(data.Length <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(data));
            }

            if(count <= 0 || count > VertexCount || count > data.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferHandle);
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, count * VertexInfo.SizeInBytes, data);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
