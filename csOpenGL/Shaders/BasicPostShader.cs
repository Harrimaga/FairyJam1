using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace FairyJam
{
    class BasicPostShader : Shader
    {

        private int prev, screenSize;

        public BasicPostShader() : base("Shaders/vsPost.glsl", "Shaders/fsPost.glsl")
        {

        }

        protected override void ShaderSetup()
        {
            base.ShaderSetup();
            prev = GL.GetUniformLocation(Handle, "prev");
            screenSize = GL.GetUniformLocation(Handle, "screenSize");
        }

        protected override void Run(long previous)
        {
            if(previous == -1)
            {
                throw new ArgumentException("Previous was a non existing framebuffer");
            }
            GL.Arb.MakeImageHandleResident(previous, All.ReadOnly);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.Arb.UniformHandle(prev, previous);
            GL.Uniform2(screenSize, Globals.Width, Globals.Height);
            GL.MemoryBarrier(MemoryBarrierFlags.AllBarrierBits);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.MemoryBarrier(MemoryBarrierFlags.AllBarrierBits);
            GL.Arb.MakeImageHandleNonResident(previous);
        }

    }
}
