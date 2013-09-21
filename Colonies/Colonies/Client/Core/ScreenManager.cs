// Title: ScreenManager.cs
// Author: Joe Maley
// Date: 6-9-2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Colonies.Client.Core
{
    /// <summary>
    /// Maintains current screen state, and hooks with game loops.
    /// </summary>
    class ScreenManager
    {
        private Screen.Screen screen = null;

        private GraphicsDeviceManager graphicsDeviceManager;
        public GraphicsDeviceManager getGraphicsDeviceManager() { return graphicsDeviceManager; }

        private InputManager inputManager;

        /// <summary>
        /// Creates a new screen manager.
        /// </summary>
        public ScreenManager(GraphicsDeviceManager graphicsDeviceManager)
        {
            this.graphicsDeviceManager = graphicsDeviceManager;

            inputManager = InputManager.GetInstance();
        }

        /// <summary>
        /// Sets the currently displayed screen.
        /// </summary>
        /// <param name="screen">The screen to go to.</param>
        public void SetScreen(Screen.Screen screen)
        {
            this.screen = screen;
        }

        /// <summary>
        /// Uppdates the current screen.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            inputManager.Update();

            screen.HandleInput();
            screen.Update(gameTime);
        }

        /// <summary>
        /// Draws the current screen.
        /// </summary>
        public void Draw()
        {
            if (screen != null)
                screen.Draw();
        }
    }
}
