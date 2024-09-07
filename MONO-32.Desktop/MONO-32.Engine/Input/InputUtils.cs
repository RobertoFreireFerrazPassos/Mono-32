using Microsoft.Xna.Framework.Input;

namespace MONO_32.Engine.Input;

public static class InputUtils
{
    private static KeyboardState _currentKeyboardState;
    private static KeyboardState _previousKeyboardState;

    public static void Update()
    {
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();
    }

    public static Keys[] GetReleasedKeys()
    {
        return _previousKeyboardState.GetPressedKeys()
                .Where(key => !_currentKeyboardState.IsKeyDown(key))
                .ToArray();
    }
}