using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysKurs
{
    internal class Class1
    {
        private static int Edges = 6;

        public static VertexPositionColor[] vertexes = new VertexPositionColor[Edges * 4];
        public static int[] indexes = new int[Edges * 6];

        private static int vertexCount = 0;
        public static int indexCount = 0;

        public static void CreateCube()
        {
            //Back                            
            vertexes[0] = new VertexPositionColor(new Vector3(-0.5f, -0.5f, -0.5f), new Color4(0f, 0f, 1f, 1f));
            vertexes[0] = new VertexPositionColor(new Vector3(0.5f, -0.5f, -0.5f), new Color4(0f, 1f, 0f, 1f));
            vertexes[0] = new VertexPositionColor(new Vector3(0.5f, 0.5f, -0.5f), new Color4(1f, 0f, 0f, 1f));
            vertexes[0] = new VertexPositionColor(new Vector3(-0.5f, 0.5f, -0.5f), new Color4(1f, 1f, 0f, 1f));

            //Front 
            vertexes[1] = new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.5f), new Color4(1f, 0f, 0f, 1f));
            vertexes[1] = new VertexPositionColor(new Vector3(0.5f, -0.5f, 0.5f), new Color4(0f, 1f, 0f, 1f));
            vertexes[1] = new VertexPositionColor(new Vector3(0.5f, 0.5f, 0.5f), new Color4(0f, 0f, 1f, 1f));
            vertexes[1] = new VertexPositionColor(new Vector3(-0.5f, 0.5f, 0.5f), new Color4(1f, 1f, 0f, 1f));

            //Left
            vertexes[2] = new VertexPositionColor(new Vector3(-0.5f, 0.5f, 0.5f), new Color4(1f, 0f, 0f, 1f));
            vertexes[2] = new VertexPositionColor(new Vector3(-0.5f, 0.5f, -0.5f), new Color4(0f, 1f, 0f, 1f));
            vertexes[2] = new VertexPositionColor(new Vector3(-0.5f, -0.5f, -0.5f), new Color4(0f, 0f, 1f, 1f));
            vertexes[2] = new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.5f), new Color4(1f, 1f, 0f, 1f));

            //Right
            vertexes[3] = new VertexPositionColor(new Vector3(0.5f, 0.5f, 0.5f), new Color4(1f, 0f, 0f, 1f));
            vertexes[3] = new VertexPositionColor(new Vector3(0.5f, 0.5f, -0.5f), new Color4(0f, 1f, 0f, 1f));
            vertexes[3] = new VertexPositionColor(new Vector3(0.5f, -0.5f, -0.5f), new Color4(0f, 0f, 1f, 1f));
            vertexes[3] = new VertexPositionColor(new Vector3(0.5f, -0.5f, 0.5f), new Color4(1f, 1f, 0f, 1f));

            //Bottom
            vertexes[4] = new VertexPositionColor(new Vector3(-0.5f, -0.5f, -0.5f), new Color4(1f, 0f, 0f, 1f));
            vertexes[4] = new VertexPositionColor(new Vector3(0.5f, -0.5f, -0.5f), new Color4(0f, 1f, 0f, 1f));
            vertexes[4] = new VertexPositionColor(new Vector3(0.5f, -0.5f, 0.5f), new Color4(0f, 0f, 1f, 1f));
            vertexes[4] = new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.5f), new Color4(1f, 1f, 0f, 1f));

            //Top
            vertexes[5] = new VertexPositionColor(new Vector3(-0.5f, 0.5f, -0.5f), new Color4(1f, 0f, 0f, 1f));
            vertexes[5] = new VertexPositionColor(new Vector3(0.5f, 0.5f, -0.5f), new Color4(0f, 1f, 0f, 1f));
            vertexes[5] = new VertexPositionColor(new Vector3(0.5f, 0.5f, 0.5f), new Color4(0f, 0f, 1f, 1f));
            vertexes[5] = new VertexPositionColor(new Vector3(-0.5f, 0.5f, 0.5f), new Color4(1f, 1f, 0f, 1f));

            for (int i = 0; i < Edges; i++)
            {
                indexes[indexCount++] = 0 + vertexCount;
                indexes[indexCount++] = 1 + vertexCount;
                indexes[indexCount++] = 2 + vertexCount;
                indexes[indexCount++] = 0 + vertexCount;
                indexes[indexCount++] = 2 + vertexCount;
                indexes[indexCount++] = 3 + vertexCount;
            }
        }
    }
}
