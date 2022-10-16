﻿using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysKurs.Model
{
    internal class Cube
    {
        private static int Edges = 6;
        private static int vertexCount = 0;
        public static int indexCount = 0;
        public static int[] indexes = new int[Edges * 6];


        public static VertexPositionColor[] ColorVertexes = new VertexPositionColor[]
        {
            //Back 
            new VertexPositionColor(new Vector3(-0.5f, -0.5f, -0.5f),new Color4(1f, 0f, 0f, 1f)),
            new VertexPositionColor(new Vector3(0.5f, -0.5f, -0.5f),new Color4(0f, 1f, 0f, 1f)),
            new VertexPositionColor(new Vector3(0.5f, 0.5f, -0.5f),new Color4(0f, 0f, 1f, 1f)),
            new VertexPositionColor(new Vector3(-0.5f, 0.5f, -0.5f),new Color4(1f, 1f, 0f, 1f)),

            //Front
            new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.5f),new Color4(1f, 0f, 0f, 1f)),
            new VertexPositionColor(new Vector3(0.5f, -0.5f, 0.5f),new Color4(0f, 1f, 0f, 1f)),
            new VertexPositionColor(new Vector3(0.5f, 0.5f, 0.5f),new Color4(0f, 0f, 1f, 1f)),
            new VertexPositionColor(new Vector3(-0.5f, 0.5f, 0.5f),new Color4(1f, 1f, 0f, 1f)),

            //Left
            new VertexPositionColor(new Vector3(-0.5f, 0.5f, 0.5f),new Color4(1f, 0f, 0f, 1f)),
            new VertexPositionColor(new Vector3(-0.5f, 0.5f, -0.5f),new Color4(0f, 1f, 0f, 1f)),
            new VertexPositionColor(new Vector3(-0.5f, -0.5f, -0.5f),new Color4(0f, 0f, 1f, 1f)),
            new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.5f),new Color4(1f, 1f, 0f, 1f)),

            //Right
            new VertexPositionColor(new Vector3(0.5f, 0.5f, 0.5f),new Color4(1f, 0f, 0f, 1f)),
            new VertexPositionColor(new Vector3(0.5f, 0.5f, -0.5f),new Color4(0f, 1f, 0f, 1f)),
            new VertexPositionColor(new Vector3(0.5f, -0.5f, -0.5f),new Color4(0f, 0f, 1f, 1f)),
            new VertexPositionColor(new Vector3(0.5f, -0.5f, 0.5f),new Color4(1f, 1f, 0f, 1f)),

            //Bottom
            new VertexPositionColor(new Vector3(-0.5f, -0.5f, -0.5f),new Color4(1f, 0f, 0f, 1f)),
            new VertexPositionColor(new Vector3(0.5f, -0.5f, -0.5f),new Color4(0f, 1f, 0f, 1f)),
            new VertexPositionColor(new Vector3(0.5f, -0.5f, 0.5f),new Color4(0f, 0f, 1f, 1f)),
            new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.5f),new Color4(1f, 1f, 0f, 1f)),

            //Top
            new VertexPositionColor(new Vector3(-0.5f, 0.5f, -0.5f),new Color4(1f, 0f, 0f, 1f)),
            new VertexPositionColor(new Vector3(0.5f, 0.5f, -0.5f),new Color4(0f, 1f, 0f, 1f)),
            new VertexPositionColor(new Vector3(0.5f, 0.5f, 0.5f),new Color4(0f, 0f, 1f, 1f)),
            new VertexPositionColor(new Vector3(-0.5f, 0.5f, 0.5f),new Color4(1f, 1f, 0f, 1f))
        };

        public static VertexPositionTexture[] TextureVertexes = new VertexPositionTexture[]
        {

            //Back
            new VertexPositionTexture(new Vector3(-0.5f, -0.5f, -0.5f),new Vector2(0.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(0.5f, -0.5f, -0.5f),new Vector2(1.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(0.5f, 0.5f, -0.5f),new Vector2(1.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(-0.5f, 0.5f, -0.5f),new Vector2(0.0f, 1.0f)),

            //Front
            new VertexPositionTexture(new Vector3(-0.5f, -0.5f, 0.5f),new Vector2(0.0f,0.0f)), // левый низ
            new VertexPositionTexture(new Vector3(0.5f, -0.5f, 0.5f),new Vector2(1.0f,0.0f)),  // правый низ
            new VertexPositionTexture(new Vector3(0.5f, 0.5f, 0.5f),new Vector2(1.0f,1.0f)),   // правый вверх
            new VertexPositionTexture(new Vector3(-0.5f, 0.5f, 0.5f),new Vector2(0.0f,1.0f)),  // левый вверх

            //Left
            new VertexPositionTexture(new Vector3(-0.5f, 0.5f, 0.5f),new Vector2(1.0f, 0.0f)),  // левый вверх
            new VertexPositionTexture(new Vector3(-0.5f, 0.5f, -0.5f),new Vector2(1.0f, 1.0f)), // правый вверх
            new VertexPositionTexture(new Vector3(-0.5f, -0.5f, -0.5f),new Vector2(0.0f, 1.0f)),// правый низ
            new VertexPositionTexture(new Vector3(-0.5f, -0.5f, 0.5f),new Vector2(0.0f, 0.0f)), // левый низ

            //Right
            new VertexPositionTexture(new Vector3(0.5f, 0.5f, 0.5f),new Vector2(1.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(0.5f, 0.5f, -0.5f),new Vector2(1.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(0.5f, -0.5f, -0.5f),new Vector2(0.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(0.5f, -0.5f, 0.5f),new Vector2(0.0f, 0.0f)),

            //Bottom
            new VertexPositionTexture(new Vector3(-0.5f, -0.5f, -0.5f),new Vector2(0.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(0.5f, -0.5f, -0.5f),new Vector2(1.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(0.5f, -0.5f, 0.5f),new Vector2(1.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(-0.5f, -0.5f, 0.5f),new Vector2(0.0f, 0.0f)),

            //Top
            new VertexPositionTexture(new Vector3(-0.5f, 0.5f, -0.5f),new Vector2(0.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(0.5f, 0.5f, -0.5f),new Vector2(1.0f, 1.0f)),
            new VertexPositionTexture(new Vector3(0.5f, 0.5f, 0.5f),new Vector2(1.0f, 0.0f)),
            new VertexPositionTexture(new Vector3(-0.5f, 0.5f, 0.5f),new Vector2(0.0f, 0.0f))
        };

        public static void CountIndexes()
        {
            for (int i = 0; i < Edges; i++)
            {
                indexes[indexCount++] = 0 + vertexCount;
                indexes[indexCount++] = 1 + vertexCount;
                indexes[indexCount++] = 2 + vertexCount;
                indexes[indexCount++] = 0 + vertexCount;
                indexes[indexCount++] = 2 + vertexCount;
                indexes[indexCount++] = 3 + vertexCount;

                vertexCount += 4;
            }
        }

    }
}