using System;
using OpenTK.Mathematics;

namespace SysKurs
{
    public readonly struct VertexAttribute
    {
        public readonly string Name;
        public readonly int Index;
        public readonly int ComponentCount;
        public readonly int Offset;

        public VertexAttribute(string name, int index, int componentCount, int offset)
        {
            Name = name;
            Index = index;
            ComponentCount = componentCount;
            Offset = offset;
        }
    }

    public sealed class VertexInfo
    {
        public readonly Type Type;
        public readonly int SizeInBytes;
        public readonly VertexAttribute[] VertexAttributes;

        public VertexInfo(Type type,params VertexAttribute[] vertexAttributes)
        {
            Type = type;
            SizeInBytes = 0;
            VertexAttributes = vertexAttributes;

            for (int i = 0; i < vertexAttributes.Length; i++)
            {
                VertexAttribute attribute = VertexAttributes[i];
                SizeInBytes += attribute.ComponentCount * sizeof(float);
            }
        }
    }

    public readonly struct VertexPositionColor
    {
        public readonly Vector3 Position;
        public readonly Color4 Color;
        public static readonly VertexInfo VertexInfo = new VertexInfo(
            typeof(VertexPositionColor),
            new VertexAttribute("Position", 0, 3, 0),
            new VertexAttribute("Color",1, 4, 3 * sizeof(float))
            );

        public VertexPositionColor(Vector3 position, Color4 color)
        {
            Position = position;
            Color = color;
        }
    }

    public readonly struct VertexPositionTexture
    {
        public readonly Vector3 Position;
        public readonly Vector2 TexCoord;
        public static readonly VertexInfo VertexInfo = new VertexInfo(
            typeof(VertexPositionTexture),
            new VertexAttribute("Position", 0, 3, 0),
            new VertexAttribute("TextCoord", 1, 2, 3 * sizeof(float))
            );

        public VertexPositionTexture(Vector3 position, Vector2 texCoord)
        {
            Position = position;
            TexCoord = texCoord;
        }
    }

    public readonly struct VertexPosition
    {
        public readonly Vector3 Position;
        public static readonly VertexInfo VertexInfo = new VertexInfo(
            typeof(VertexPosition),
            new VertexAttribute("Position", 0, 3, 0)
            );

        public VertexPosition(Vector3 position)
        {
            Position = position;
        }
    }

    public readonly struct VertexPositionNormal
    {
        public readonly Vector3 Position;
        public readonly Vector3 Normal;
        public readonly Vector2 TexCoord;
        public static readonly VertexInfo VertexInfo = new VertexInfo(
            typeof(VertexPositionNormal),
            new VertexAttribute("Position", 0, 3, 0),
            new VertexAttribute("Normal", 1, 3, 3 * sizeof(float)),
            new VertexAttribute("TextCoord", 2, 2, 6 * sizeof(float))
            );

        public VertexPositionNormal(Vector3 position,Vector3 normal, Vector2 texCoord)
        {
            Position = position;
            Normal = normal;
            TexCoord = texCoord;
        }
    }
}
