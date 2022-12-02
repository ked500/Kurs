using OpenTK.Mathematics;
using SysKurs.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysKurs.Model
{
    internal class Lamp
    {
        private static int Edges = 6;
        private static int vertexCount = 0;
        public static int indexCount = 0;
        public static int[] indexes = new int[Edges * 6];


        public static VertexPosition[] ColorVertexes = new VertexPosition[]
        {
            //Back 
            new VertexPosition(new Vector3(0.5f, 0.5f, 0.5f)),
            new VertexPosition(new Vector3(1.0f, 0.5f, 0.5f)),
            new VertexPosition(new Vector3(1.0f, 1.0f, 0.5f)),
            new VertexPosition(new Vector3(0.5f, 1.0f, 0.5f)),

            //Front
            new VertexPosition(new Vector3(0.5f, 0.5f, 1.0f)),
            new VertexPosition(new Vector3(1.0f, 0.5f, 1.0f)),
            new VertexPosition(new Vector3(1.0f, 1.0f, 1.0f)),
            new VertexPosition(new Vector3(0.5f, 1.0f, 1.0f)),

            //Left
            new VertexPosition(new Vector3(0.5f, 1.0f, 1.0f)),
            new VertexPosition(new Vector3(0.5f, 1.0f, 0.5f)),
            new VertexPosition(new Vector3(0.5f, 0.5f, 0.5f)),
            new VertexPosition(new Vector3(0.5f, 0.5f, 1.0f)),

            //Right
            new VertexPosition(new Vector3(1.0f, 1.0f, 1.0f)),
            new VertexPosition(new Vector3(1.0f, 1.0f, 0.5f)),
            new VertexPosition(new Vector3(1.0f, 0.5f, 0.5f)),
            new VertexPosition(new Vector3(1.0f, 0.5f, 1.0f)),

            //Bottom
            new VertexPosition(new Vector3(0.5f, 0.5f, 0.5f)),
            new VertexPosition(new Vector3(1.0f, 0.5f, 0.5f)),
            new VertexPosition(new Vector3(1.0f, 0.5f, 1.0f)),
            new VertexPosition(new Vector3(0.5f, 0.5f, 1.0f)),

            //Top
            new VertexPosition(new Vector3(0.5f, 1.0f, 0.5f)),
            new VertexPosition(new Vector3(1.0f, 1.0f, 0.5f)),
            new VertexPosition(new Vector3(1.0f, 1.0f, 1.0f)),
            new VertexPosition(new Vector3(0.5f, 1.0f, 1.0f))
        };
      
        public static void CountIndexes()
        {
            for (int i = 0; i < Edges; i++)
            {
                indexes[indexCount++] = 0 + vertexCount;
                indexes[indexCount++] = 1 + vertexCount;
                indexes[indexCount++] = 2 + vertexCount;
                indexes[indexCount++] = 2 + vertexCount;
                indexes[indexCount++] = 3 + vertexCount;
                indexes[indexCount++] = 0 + vertexCount;

                vertexCount += 4;
            }
        }

        public static void CreateLamp(out VertexBuffer vertexBuffer, out IndexBuffer indexBuffer,
            out VertexArray vertexArray, out ShaderProgramm shaderProgram)
        {
            CountIndexes();
            vertexBuffer = new VertexBuffer(VertexPosition.VertexInfo, ColorVertexes.Length);
            vertexBuffer.SetData(ColorVertexes, ColorVertexes.Length);

            indexBuffer = new IndexBuffer(indexes.Length);
            indexBuffer.SetData(indexes, indexes.Length);

            vertexArray = new VertexArray(vertexBuffer);

            shaderProgram = new ShaderProgramm("../../../Shaders/Lighting Shaders/shader.vert", 
                "../../../Shaders/Lighting Shaders/shader.frag");
        }   
    }
}
