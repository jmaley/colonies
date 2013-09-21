// Title: InputManager.cs
// Author: Joe Maley
// Date: 6-8-2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Colonies.Client.Core
{
    /// <summary>
    /// Contains current and previous input states, with helper functionallity.
    /// </summary>
    public class InputManager
    {
        private static InputManager instance = null;

        private KeyboardState lastKeyboardState;
        private KeyboardState currentkeyboardState;

        private MouseState lastMouseState;
        private MouseState currentMouseState;

        /// <summary>
        /// Creates a new input manager and retrieves current input states.
        /// Private to defeat instantiation.
        /// </summary>
        private InputManager()
        {
            lastKeyboardState = Keyboard.GetState();
            currentkeyboardState = Keyboard.GetState();

            lastMouseState = Mouse.GetState();
            currentMouseState = Mouse.GetState();
        }

        /// <summary>
        /// Returns the unique instance of the input manager.
        /// </summary>
        public static InputManager GetInstance()
        {
            return instance == null ? instance = new InputManager() : instance;
        }

        /// <summary>
        /// Refreshes input states.
        /// </summary>
        public void Update()
        {
            lastKeyboardState = currentkeyboardState;
            currentkeyboardState = Keyboard.GetState();

            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        /// <summary>
        /// Checks if key is pressed down.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns>True if key is pressed, otherwise false.</returns>
        public bool IsKeyDown(Keys key) { return currentkeyboardState.IsKeyDown(key); }

        /// <summary>
        /// Checks if it is a new key press. (Not held down previously).
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns>True if this is a new key press, false if not pressed or if pressed in last update.</returns>
        public bool IsNewKeyPress(Keys key) { return currentkeyboardState.IsKeyDown(key) && !lastKeyboardState.IsKeyDown(key); }

        /// <summary>
        /// Checks if the left mouse button is down.
        /// </summary>
        /// <returns>True if left click is pressed, otherwise false.</returns>
        public bool IsLeftClick() { return currentMouseState.LeftButton == ButtonState.Pressed; }

        /// <summary>
        /// Checks if it is a new left click. (Not held down previously).
        /// </summary>
        /// <returns>True if this is a new left click, false if not pressed or if pressed in last update.</returns>
        public bool IsNewLeftClick() { return currentMouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton != ButtonState.Pressed; }

        /// <summary>
        /// Checks if the right mouse button is down.
        /// </summary>
        /// <returns>True if right click is pressed, otherwise false.</returns>
        public bool IsRightClick() { return currentMouseState.RightButton == ButtonState.Pressed; }

        /// <summary>
        /// Checks if it is a new right click. (Not held down previously).
        /// </summary>
        /// <returns>True if this is a new right click, false if not pressed or if pressed in last update.</returns>
        public bool IsNewRightClick() { return currentMouseState.RightButton == ButtonState.Pressed && lastMouseState.RightButton != ButtonState.Pressed; }

        /// <summary>
        /// Returns the X coordinate of the mouse position.
        /// </summary>
        /// <returns>X coordinate of mouse position.</returns>
        public int GetMouseX() { return currentMouseState.X; }

        /// <summary>
        /// Returns the Y coordinate of the mouse position.
        /// </summary>
        /// <returns>Y coordinate of mouse position.</returns>
        public int GetMouseY() { return currentMouseState.Y; }

        /// <summary>
        /// Returns the X distance that the mouse has travelled since the last update poll.
        /// </summary>
        /// <returns>X distance that the mouse has travelled since the last update poll.</returns>
        public int GetMouseXDelta() { return currentMouseState.X - lastMouseState.X; }

        /// <summary>
        /// Returns the Y distance that the mouse has travelled since the last update poll.
        /// </summary>
        /// <returns>Y distance that the mouse has travelled since the last update poll.</returns>
        public int GetMouseYDelta() { return currentMouseState.Y - lastMouseState.Y; }
    }
}