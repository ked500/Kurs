using System;
using OpenTK.Graphics.OpenGL;

namespace SysKurs
{
    public sealed class VertexArray : IDisposable
    {
        private bool disposed;
        public readonly VertexBuffer VertexBuffer;
        public readonly int VertexArrayHandle;

        public VertexArray(VertexBuffer vertexBuffer)
        {
            disposed = false;

            if(vertexBuffer is null)
            {
                throw new ArgumentNullException(nameof(vertexBuffer));
            }

            VertexBuffer = vertexBuffer;
            VertexAttribute[] attributes = VertexBuffer.VertexInfo.VertexAttributes;

            VertexArrayHandle = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayHandle);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer.VertexBufferHandle);

         
            for (int i = 0; i < attributes.Length; i++)
            {
                VertexAttribute attribute = attributes[i];
                GL.VertexAttribPointer(attribute.Index, attribute.ComponentCount, VertexAttribPointerType.Float, false, VertexBuffer.VertexInfo.SizeInBytes, attribute.Offset);
                GL.EnableVertexAttribArray(attribute.Index);
            }
            GL.BindVertexArray(0);
        }

        ~VertexArray()
        {
            Dispose();
        }

        public void Dispose()
        {
            if(disposed) return;
         
            GL.BindVertexArray(0);
            GL.DeleteVertexArray(VertexArrayHandle);

            disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
