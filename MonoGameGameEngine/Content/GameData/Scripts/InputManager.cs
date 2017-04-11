using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameGameEngine
{
    [MoonSharpUserData]
    public class InputManager
    {
        // Keyboard states used to determine key presses
        private static KeyboardState _currentKeyboardState;
        private static KeyboardState _previousKeyboardState;

        // Gamepad states used to determine button presses
        private static GamePadState _currentGamePadState;
        private static GamePadState _previousGamePadState;

        //Mouse states used to track Mouse button press
        private static MouseState _currentMouseState;
        private static MouseState _previousMouseState;

        private static Vector2 _mousePos = new Vector2();

        public static Vector2 MousePos
        {
            get
            {
                return _mousePos;
            }
        }

        private static Dictionary<string, KeyBind> _axis = new Dictionary<string, KeyBind>();
        public static void SetUpBindings()
        {
            KeyBind exit = new KeyBind();
            exit._negativeButton = 0;
            exit._altNegativeButton = 0;
            exit._positiveButton = Keys.Escape;
            exit._altPositiveButton = Keys.Escape;
            _axis.Add("exit", exit);

            KeyBind lr = new KeyBind();
            lr._negativeButton = Keys.Left;
            lr._altNegativeButton = Keys.A;
            lr._positiveButton = Keys.Right;
            lr._altPositiveButton = Keys.D;
            _axis.Add("horizontal", lr);

            KeyBind ud = new KeyBind();
            ud._negativeButton = Keys.Up;
            ud._altNegativeButton = Keys.W;
            ud._positiveButton = Keys.Down;
            ud._altPositiveButton = Keys.S;
            _axis.Add("vertical", ud);

            KeyBind select = new KeyBind();
            select._negativeButton = 0;
            select._altNegativeButton = 0;
            select._positiveButton = Keys.Enter;
            select._altPositiveButton = Keys.Enter;
            _axis.Add("select", select);
        }
        [MoonSharpHidden]
        public static void Update()
        {
            // Save the previous state of the keyboard and game pad so we can determine single key/button presses

            _previousGamePadState = _currentGamePadState;

            _previousKeyboardState = _currentKeyboardState;



            // Read the current state of the keyboard and gamepad and store it

            _currentKeyboardState = Keyboard.GetState();

            _currentGamePadState = GamePad.GetState(PlayerIndex.One);

            foreach (KeyValuePair<string, KeyBind> entry in _axis)
            {
                KeyBind currentAxis = entry.Value;
                int axisVal = 0;
                if (IsKeyDown(currentAxis._positiveButton) || IsKeyDown(currentAxis._altPositiveButton)) axisVal += 1;
                if (IsKeyDown(currentAxis._negativeButton) || IsKeyDown(currentAxis._altNegativeButton)) axisVal -= 1;
                //if (keyboardState.IsKeyUp(currentAxis.positiveButton) || keyboardState.IsKeyUp(currentAxis.altPositiveButton)) axisVal -= 1;
                //if (keyboardState.IsKeyUp(currentAxis.negativeButton) || keyboardState.IsKeyUp(currentAxis.altNegativeButton)) axisVal += 1;
                axisVal = Math.Max(-1, Math.Min(axisVal, 1));
                currentAxis._value = axisVal;
            }
        }
        public static bool IsKeyDown(Keys key)
        {
            if (_currentKeyboardState.IsKeyDown(key)) return true;
            else return false;
        }
        public static bool WasFullPress(Keys key)
        {
            if (_currentKeyboardState.IsKeyUp(key) &&
                _previousKeyboardState.IsKeyDown(key))
            {
                return true;
            }
            else return false;
        }
        public static bool WasFullPress(string key)
        {
            try
            {
                return WasFullPress((Keys)Enum.Parse(typeof(Keys), key));
            }
            catch
            {
                Debug.WriteLine("Key '" + key + "' was not found");
                return false;
            }
        }
        public static bool IsKeyDown(string key)
        {
            try
            {
                return IsKeyDown((Keys)Enum.Parse(typeof(Keys), key));
            }
            catch
            {
                Debug.WriteLine("Key '" + key + "' was not found");
                return false;
            }
        }
        public static int GetAxis(string name)
        {
            KeyBind axisVal;
            if (_axis.TryGetValue(name, out axisVal))
            {
                return axisVal._value;
            }
            else
            {
                Debug.WriteLine("Axis " + name + " does not exsist!");
                return 0;
            }
        }
        public static KeyBind GetKeybind(string name)
        {
            KeyBind axisVal;
            if (_axis.TryGetValue(name, out axisVal))
            {
                return axisVal;
            }
            else
            {
                Debug.WriteLine("Axis " + name + " does not exsist!");
                return null;
            }
        }

    }

    public class KeyBind
    {
        public Keys _positiveButton;
        public Keys _negativeButton;
        public Keys _altPositiveButton;
        public Keys _altNegativeButton;
        public int _value = 0;
    }
}
