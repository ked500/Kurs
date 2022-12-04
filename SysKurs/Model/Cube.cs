using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using SysKurs.Buffers;
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

        public static VertexPositionNormal[] NormalVertexes = new VertexPositionNormal[]
        {
            //Back
            new VertexPositionNormal(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.0f, 0.0f, -1.0f), new Vector2(0.0f, 0.0f)),
            new VertexPositionNormal(new Vector3(0.5f, -0.5f, -0.5f),  new Vector3(0.0f, 0.0f, -1.0f), new Vector2(1.0f, 0.0f)),
            new VertexPositionNormal(new Vector3(0.5f, 0.5f, -0.5f),   new Vector3(0.0f, 0.0f, -1.0f), new Vector2(1.0f, 1.0f)),
            new VertexPositionNormal(new Vector3(-0.5f, 0.5f, -0.5f),  new Vector3(0.0f, 0.0f, -1.0f), new Vector2(0.0f, 1.0f)),

            //Front
            new VertexPositionNormal(new Vector3(-0.5f, -0.5f, 0.5f),  new Vector3(0.0f, 0.0f, 1.0f), new Vector2(0.0f,0.0f)), 
            new VertexPositionNormal(new Vector3(0.5f, -0.5f, 0.5f),   new Vector3(0.0f, 0.0f, 1.0f), new Vector2(1.0f,0.0f)),  
            new VertexPositionNormal(new Vector3(0.5f, 0.5f, 0.5f),    new Vector3(0.0f, 0.0f, 1.0f), new Vector2(1.0f,1.0f)),  
            new VertexPositionNormal(new Vector3(-0.5f, 0.5f, 0.5f),   new Vector3(0.0f, 0.0f, 1.0f), new Vector2(0.0f,1.0f)), 

            //Left
            new VertexPositionNormal(new Vector3(-0.5f, 0.5f, 0.5f),   new Vector3(-1.0f, 0.0f, 0.0f), new Vector2(1.0f, 0.0f)), 
            new VertexPositionNormal(new Vector3(-0.5f, 0.5f, -0.5f),  new Vector3(-1.0f, 0.0f, 0.0f), new Vector2(1.0f, 1.0f)), 
            new VertexPositionNormal(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(-1.0f, 0.0f, 0.0f), new Vector2(0.0f, 1.0f)),
            new VertexPositionNormal(new Vector3(-0.5f, -0.5f, 0.5f),  new Vector3(-1.0f, 0.0f, 0.0f), new Vector2(0.0f, 0.0f)),
        
            //Right
            new VertexPositionNormal(new Vector3(0.5f, 0.5f, 0.5f),    new Vector3(1.0f, 0.0f, 0.0f), new Vector2(1.0f, 0.0f)),
            new VertexPositionNormal(new Vector3(0.5f, 0.5f, -0.5f),   new Vector3(1.0f, 0.0f, 0.0f), new Vector2(1.0f, 1.0f)),
            new VertexPositionNormal(new Vector3(0.5f, -0.5f, -0.5f),  new Vector3(1.0f, 0.0f, 0.0f) ,new Vector2(0.0f, 1.0f)),
            new VertexPositionNormal(new Vector3(0.5f, -0.5f, 0.5f),   new Vector3(1.0f, 0.0f, 0.0f), new Vector2(0.0f, 0.0f)),     
               
            //Bottom
            new VertexPositionNormal(new Vector3(-0.5f, -0.5f, -0.5f), new Vector3(0.0f, -1.0f, 0.0f), new Vector2(0.0f, 1.0f)),
            new VertexPositionNormal(new Vector3(0.5f, -0.5f, -0.5f),  new Vector3(0.0f, -1.0f, 0.0f), new Vector2(1.0f, 1.0f)),
            new VertexPositionNormal(new Vector3(0.5f, -0.5f, 0.5f),   new Vector3(0.0f, -1.0f, 0.0f), new Vector2(1.0f, 0.0f)),
            new VertexPositionNormal(new Vector3(-0.5f, -0.5f, 0.5f),  new Vector3(0.0f, -1.0f, 0.0f), new Vector2(0.0f, 0.0f)),
                        
            //Top
            new VertexPositionNormal(new Vector3(-0.5f, 0.5f, -0.5f),  new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 1.0f)),
            new VertexPositionNormal(new Vector3(0.5f, 0.5f, -0.5f),   new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 1.0f)),
            new VertexPositionNormal(new Vector3(0.5f, 0.5f, 0.5f),    new Vector3(0.0f, 1.0f, 0.0f), new Vector2(1.0f, 0.0f)),
            new VertexPositionNormal(new Vector3(-0.5f, 0.5f, 0.5f),   new Vector3(0.0f, 1.0f, 0.0f), new Vector2(0.0f, 0.0f))
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

        public static void CreateRGBCube(out VertexBuffer vertexBuffer, out IndexBuffer indexBuffer,
            out VertexArray vertexArray, out ShaderProgramm shaderProgram)
        {
            CountIndexes();
            vertexBuffer = new VertexBuffer(VertexPositionColor.VertexInfo, ColorVertexes.Length);
            vertexBuffer.SetData(ColorVertexes, ColorVertexes.Length);

            indexBuffer = new IndexBuffer(indexes.Length);
            indexBuffer.SetData(indexes, indexes.Length);

            vertexArray = new VertexArray(vertexBuffer);

            shaderProgram = new ShaderProgramm("../../../Shaders/RGB Shaders/shader_rgba.vert", "../../../Shaders/RGB Shaders/shader_rgba.frag");
        }

        public static void CreateTexturedCube(out VertexBuffer vertexBuffer, out IndexBuffer indexBuffer, 
            out VertexArray vertexArray, out ShaderProgramm shaderProgram,out Texture _texture)
        {
            CountIndexes();
            vertexBuffer = new VertexBuffer(VertexPositionTexture.VertexInfo, TextureVertexes.Length);
            vertexBuffer.SetData(TextureVertexes, TextureVertexes.Length);

            indexBuffer = new IndexBuffer(indexes.Length);
            indexBuffer.SetData(indexes, indexes.Length);

            vertexArray = new VertexArray(vertexBuffer);

            shaderProgram = new ShaderProgramm("../../../Shaders/Texture Shaders/shader_text.vert", "../../../Shaders/Texture Shaders/shader_text.frag");

            _texture = Texture.LoadFromFile("../../../Resources/container.png");
            _texture.Use(TextureUnit.Texture0);
        }

        public static void CreateCubeForLighting(out VertexBuffer vertexBuffer, out IndexBuffer indexBuffer,
            out VertexArray vertexArray, out ShaderProgramm shaderProgram, out Texture _diffuseMap, out Texture _specularMap)
        {
            CountIndexes();
            vertexBuffer = new VertexBuffer(VertexPositionNormal.VertexInfo, NormalVertexes.Length);
            vertexBuffer.SetData(NormalVertexes,NormalVertexes.Length);

            indexBuffer = new IndexBuffer(indexes.Length);
            indexBuffer.SetData(indexes, indexes.Length);

            vertexArray = new VertexArray(vertexBuffer);

            shaderProgram = new ShaderProgramm("../../../Shaders/Lighting Shaders/shader.vert", "../../../Shaders/Lighting Shaders/lighting.frag");

            _diffuseMap = Texture.LoadFromFile("../../../Resources/Container 2/container2.png");       
            _specularMap = Texture.LoadFromFile("../../../Resources/Container 2/container2_specular.png");

            _diffuseMap.Use(TextureUnit.Texture0);
            _specularMap.Use(TextureUnit.Texture1);
        }
    }
}
