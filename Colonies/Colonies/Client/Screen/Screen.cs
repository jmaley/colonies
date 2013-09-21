// Title: Screen.cs
// Author: Joe Maley
// Date: 6-9-2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Colonies.Client.UI;
using Colonies.Client.Core;

namespace Colonies.Client.Screen
{
    /// <summary>
    /// Provides a basic template for a game screen.
    /// </summary>
    abstract class Screen
    {
        protected List<Widget> widgets = new List<Widget>();

        protected ScreenManager screenManager;
        protected SpriteBatch spriteBatch;
        protected Rectangle body;

        /// <summary>
        /// Create a game screen.
        /// </summary>
        public Screen(ScreenManager screenManager)
        {
            this.screenManager = screenManager;

            spriteBatch = new SpriteBatch(screenManager.getGraphicsDeviceManager().GraphicsDevice);

            body = new Rectangle(0, 0, SettingsManager.GetInstance().ResolutionX, SettingsManager.GetInstance().ResolutionY);
        }

        /// <summary>
        /// Handles input to the screen.
        /// </summary>
        public void HandleInput()
        {
            foreach (Widget widget in widgets)
                if (widget.HandleInput() == true)
                    return;

            HandleScreenInput();
        }

        /// <summary>
        /// Updates the screen. Game logic, A.I., physics, etc should go here.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            UpdateScreen(gameTime);
        }

        /// <summary>
        /// Draws the screen.
        /// </summary>
        public void Draw()
        {
            screenManager.getGraphicsDeviceManager().GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            DrawScreen();

            foreach (Widget widget in widgets)
                widget.Draw();

            spriteBatch.End();
        }

        /// <summary>
        /// Handles input to the screen (child implementation).
        /// </summary>
        protected abstract void HandleScreenInput();

        /// <summary>
        /// Updates the screen. Game logic, A.I., physics, etc should go here (child implementation).
        /// </summary>
        protected abstract void UpdateScreen(GameTime gameTime);

        /// <summary>
        /// Draws the screen (child implementation).
        /// </summary>
        protected abstract void DrawScreen();
    }
}
