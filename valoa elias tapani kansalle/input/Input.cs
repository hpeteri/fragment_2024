using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace valoa_elias_tapani_kansalle
{
    public static class Input {
        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;

        public static KeyboardState CurrentKeyState
        {
            get { return currentKeyState; }
        }

        public static KeyboardState PreviousKeyState
        {
            get { return previousKeyState; }
        }

        public static void Initialize(){
            previousKeyState = Keyboard.GetState();
            currentKeyState = Keyboard.GetState();
        }

        public static void Update() {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
        }

        public static bool IsKeyPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key) &&
                !previousKeyState.IsKeyDown(key);
        }

        public static bool IsKeyDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }
    }
}
