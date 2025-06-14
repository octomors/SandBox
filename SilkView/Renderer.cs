using Silk.NET.Windowing;
using Silk.NET.Maths;
using Silk.NET.Input;

namespace SilkView
{
    internal class Renderer
    {
        private static IWindow window;

        public Renderer()
        {
            WindowOptions options = WindowOptions.Default with
            {
                Size = new Vector2D<int>(800, 600),
                Title = "SandBox"
            };

            window = Window.Create(options);

            window.Load += OnLoad;
            window.Update += OnUpdate;
            window.Render += OnRender;

            window.Run();

            window.Dispose();

        }

        #region Window handlers
        private static void OnLoad() 
        {
            IInputContext input = window.CreateInput();
            for (int i = 0; i < input.Keyboards.Count; i++)
                input.Keyboards[i].KeyDown += KeyDown;
        }

        private static void OnUpdate(double deltaTime)
        {

        }

        private static void OnRender(double deltaTime)
        {

        }
        #endregion


        private static void KeyDown(IKeyboard keyboard, Key key, int keyCode)
        {
            if (key == Key.Escape)
                window.Close();
        }
    }
}
