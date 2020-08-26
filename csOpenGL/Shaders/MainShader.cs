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
    class MainShader : Shader
    {

        private int vao, vbo, ssbo;
        public bool late = false;
        PassThroughShader pts;
        float[] data = {
                0.0f, 1.0f, 0,
                1.0f, 0.0f, 0,
                0.0f, 0.0f, 0,
                1.0f, 1.0f, 0,
                0.0f, 1.0f, 0,
                1.0f, 0.0f, 0
            };

        public MainShader(bool amd) : base(amd ? "Shaders/vsAMD.glsl" : "Shaders/vs.glsl", amd ? "Shaders/fsAMD.glsl" : "Shaders/fs.glsl")
        {
            
        }

        protected override void ShaderSetup()
        {
            base.ShaderSetup();

            vbo = GL.GenBuffer();
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            ssbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ShaderStorageBuffer, ssbo);
            GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, ssbo);
            pts = new PassThroughShader(vao);
        }

        protected override void Run(long previous)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            SData[] sdd = Window.sd.getData();
            int num = Window.sd.Count();
            if (late)
            {
                sdd = Window.lateDraw.getData();
                num = Window.lateDraw.Count();
            }
            GL.BindVertexArray(vao);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.BindVertexArray(vao);
            GL.BindBufferBase(BufferRangeTarget.ShaderStorageBuffer, 0, ssbo);
            GL.BufferData<SData>(BufferTarget.ShaderStorageBuffer, (sizeof(int) * 5 + 1 * sizeof(long) + 9 * sizeof(float)) * num, sdd, BufferUsageHint.DynamicDraw);
            GL.Uniform2(GL.GetUniformLocation(Handle, "screenSize"), Globals.Width, Globals.Height);
            GL.MemoryBarrier(MemoryBarrierFlags.AllBarrierBits);
            GL.DrawArraysInstanced(PrimitiveType.Triangles, 0, 6, num);
            GL.MemoryBarrier(MemoryBarrierFlags.AllBarrierBits);
        }

        public void RunLate(long previous)
        {
            late = true;
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, sizeof(float) * data.Length, data, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            pts.Use(previous);
            GL.UseProgram(Handle);
            Run(previous);
            GL.UseProgram(0);
            late = false;
        }

    }

    class PassThroughShader : Shader
    {

        private int prev, screenSize, vao;

        public PassThroughShader(int vao) : base("Shaders/vsPost.glsl", "Shaders/fsPass.glsl")
        {
            this.vao = vao;
        }

        protected override void ShaderSetup()
        {
            base.ShaderSetup();
            prev = GL.GetUniformLocation(Handle, "prev");
            screenSize = GL.GetUniformLocation(Handle, "screenSize");
        }

        protected override void Run(long previous)
        {
            if (previous == -1)
            {
                throw new ArgumentException("Previous was a non existing framebuffer");
            }
            GL.Arb.MakeImageHandleResident(previous, All.ReadOnly);
            GL.BindVertexArray(vao);
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
