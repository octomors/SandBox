using Silk.NET.Windowing;
using Silk.NET.Maths;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using System.Drawing;
using SandBoxEngine;

namespace SilkView
{
    internal class Renderer : IRenderer
    {
        private static IWindow window;

        private static GL gl;

        private static uint texture;
        private static uint vao;
        private static uint vbo;
        private static uint ebo;

        private static uint program;

        private static double time;


        public Renderer()
        {
            WindowOptions options = WindowOptions.Default with
            {
                Size = new Vector2D<int>(1920, 1080),
                Title = "SandBox",
                WindowState = WindowState.Maximized,
                
            };

            window = Window.Create(options);

            window.Load += OnLoad;
            window.Update += OnUpdate;
            window.Render += OnRender;

            window.Run();

            window.Dispose();
        }

        #region Window handlers
        private static unsafe void OnLoad() 
        {
            //input
            IInputContext input = window.CreateInput();
            for (int i = 0; i < input.Keyboards.Count; i++)
                input.Keyboards[i].KeyDown += KeyDown;

            //OpenGL
            gl = window.CreateOpenGL();

            //Textures
            gl.Enable(EnableCap.Texture2D);

            texture = gl.GenTexture();

            gl.BindTexture(TextureTarget.Texture2D, texture);
            gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, 192, 108, 0, PixelFormat.Rgba, PixelType.UnsignedByte, null);

            //Vertex array objects
            vao = gl.GenVertexArray();
            gl.BindVertexArray(vao);


            //Vertrex buffer objects
            float[] vertices = {
                //Positions  |Texture cords
                -1.0f, -1.0f, 0.0f, 0.0f,
                 1.0f, -1.0f, 1.0f, 0.0f,
                 1.0f,  1.0f, 1.0f, 1.0f,
                -1.0f,  1.0f, 0.0f, 1.0f
            };

            uint[] indices = { 0, 1, 2, 2, 3, 0 };

            vbo = gl.GenBuffer();
            gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);
            fixed (float* buf = vertices)
                gl.BufferData(BufferTargetARB.ArrayBuffer,
                    (nuint)(vertices.Length * sizeof(float)), buf, BufferUsageARB.StaticDraw);
            
            ebo = gl.GenBuffer();
            gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, ebo);
            fixed (uint* buf = indices)
                gl.BufferData(BufferTargetARB.ElementArrayBuffer, (nuint)(indices.Length * sizeof(uint)), buf, BufferUsageARB.StaticDraw);

            //attributes
            const uint positionLoc = 0;
            gl.EnableVertexAttribArray(positionLoc);
            gl.VertexAttribPointer(positionLoc, 2, VertexAttribPointerType.Float, false,
                4 * sizeof(float), (void*)0);

            const uint texCoordLoc = 1;
            gl.EnableVertexAttribArray(texCoordLoc);
            gl.VertexAttribPointer(texCoordLoc, 2, VertexAttribPointerType.Float, false,
                4 * sizeof(float), (void*)(3 * sizeof(float)));
            

            //Cleaning up
            gl.BindVertexArray(0);
            gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
            gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);


            //Shaders
            const string vertexCode = @"
                #version 330 core

                layout (location = 0) in vec2 position;
                layout (location = 1) in vec2 texCoord;
                out vec2 TexCoord;
                void main()
                {
                    gl_Position = vec4(position, 0.0, 1.0);
                    TexCoord = texCoord;
                }";

            const string fragmentCode = @"
                #version 330 core

                in vec2 TexCoord;
                out vec4 FragColor;
                uniform sampler2D tex;
                void main()
                {
                    FragColor = texture(tex, TexCoord);
                }";

            uint vertexShader = gl.CreateShader(ShaderType.VertexShader);
            gl.ShaderSource(vertexShader, vertexCode);

            gl.CompileShader(vertexShader);

            gl.GetShader(vertexShader, ShaderParameterName.CompileStatus, out int vStatus);
            if (vStatus != (int)GLEnum.True)
                throw new Exception("Vertex shader failed to compile: " + gl.GetShaderInfoLog(vertexShader));

            uint fragmentShader = gl.CreateShader(ShaderType.FragmentShader);
            gl.ShaderSource(fragmentShader, fragmentCode);

            gl.CompileShader(fragmentShader);

            gl.GetShader(fragmentShader, ShaderParameterName.CompileStatus, out int fStatus);
            if (fStatus != (int)GLEnum.True)
                throw new Exception("Fragment shader failed to compile: " + gl.GetShaderInfoLog(fragmentShader));


            program = gl.CreateProgram();
            gl.AttachShader(program, vertexShader);
            gl.AttachShader(program, fragmentShader);

            gl.LinkProgram(program);

            gl.GetProgram(program, ProgramPropertyARB.LinkStatus, out int lStatus);
            if (lStatus != (int)GLEnum.True)
                throw new Exception("Program failed to link: " + gl.GetProgramInfoLog(program));

            gl.DetachShader(program, vertexShader);
            gl.DetachShader(program, fragmentShader);
            gl.DeleteShader(vertexShader);
            gl.DeleteShader(fragmentShader);
        }

        private static void OnUpdate(double deltaTime)
        {

        }

        private static unsafe void OnRender(double deltaTime)
        {
            gl.Clear(ClearBufferMask.ColorBufferBit);

            // Используем шейдерную программу
            gl.UseProgram(program);

            // Привязываем текстуру
            gl.ActiveTexture(TextureUnit.Texture0);
            gl.BindTexture(TextureTarget.Texture2D, texture);
            var uniformLocation = gl.GetUniformLocation(program, "tex");
            gl.Uniform1(uniformLocation, 0);

            // Рисуем квадрат
            gl.BindVertexArray(vao);
            gl.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);

            window.SwapBuffers();
        }
        #endregion


        private static void KeyDown(IKeyboard keyboard, Key key, int keyCode)
        {
            if (key == Key.Escape)
                window.Close();
        }

        private static unsafe void UpdateTexture(byte[] pixelData)
        {
            fixed (byte* ptr = pixelData)
            {
                gl.BindTexture(TextureTarget.Texture2D, texture);
                gl.TexSubImage2D(
                    TextureTarget.Texture2D,
                    0,
                    0,
                    0,
                    192,
                    108,
                    PixelFormat.Rgba,
                    PixelType.UnsignedByte,
                    (nint)ptr
                );
            }
        }

        public void Render(Map map)
        {
            byte[] pixelData = new byte[map.XLength * map.YLength * 4];
            int index = 0;

            for(int y = 0; y < map.YLength; y++)
            {
                for (int x = 0; x < map.XLength; x++)
                {
                    Color color = Color.FromArgb(255, 0, 0, 0);
                    if(map[y, x] != null)
                    {
                        color = map[y, x].ParticleColor;
                    }

                    pixelData[index++] = color.R;
                    pixelData[index++] = color.G;
                    pixelData[index++] = color.B;
                    pixelData[index++] = color.A;
                }
            }

            UpdateTexture(pixelData);
        }
    }
}
