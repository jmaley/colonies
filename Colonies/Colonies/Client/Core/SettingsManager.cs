// Title: SettingsManager.cs
// Author: Joe Maley
// Date: 6-16-2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Colonies.Client.Core
{
    /// <summary>
    /// Container for client settings.
    /// </summary>
    class SettingsManager
    {
        private static SettingsManager instance = null;

        // gameplay settings
        // TODO

        // video settings
        private int resolutionX = 1280;
        private int resolutionY = 720;
        private bool fullscreen = false;
        private bool multiSampling = false;
        private bool verticleSync = false;

        // audio settings
        // TODO

        // control settings
        // TODO

        public int ResolutionX { set { resolutionX = value; } get { return resolutionX; } }
        public int ResolutionY { set { resolutionY = value; } get { return resolutionY; } }
        public bool Fullscreen { set { fullscreen = value; } get { return fullscreen; } }
        public bool MultiSampling { set { multiSampling = value; } get { return multiSampling; } }
        public bool VerticleSync  { set { verticleSync = value;  } get { return verticleSync;  } }

        /// <summary>
        /// Creates a new settings manager.
        /// Private to defeat instantiation.
        /// </summary>
        private SettingsManager() { }

        /// <summary>
        /// Returns the unique instance of the settings manager.
        /// </summary>
        public static SettingsManager GetInstance()
        {
            return instance == null ? instance = new SettingsManager() : instance;
        }

        /// <summary>
        /// Applies current video settings.
        /// </summary>
        /// <param name="graphicsDeviceManager">Game's Graphics Device Manager</param>
        public void ApplyVideoSettings(GraphicsDeviceManager graphicsDeviceManager)
        {
            if (resolutionX == -1)
                resolutionX = graphicsDeviceManager.GraphicsDevice.Adapter.CurrentDisplayMode.Width;

            if (resolutionY == -1)
                resolutionY = graphicsDeviceManager.GraphicsDevice.Adapter.CurrentDisplayMode.Height;

            graphicsDeviceManager.PreferredBackBufferWidth = resolutionX;
            graphicsDeviceManager.PreferredBackBufferHeight = resolutionY;
            graphicsDeviceManager.IsFullScreen = fullscreen;
            graphicsDeviceManager.PreferMultiSampling = multiSampling;
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = verticleSync;
            graphicsDeviceManager.ApplyChanges();
        }
    }
}
