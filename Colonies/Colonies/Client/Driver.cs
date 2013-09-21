// Title: Driver.cs
// Author: Joe Maley
// Date: 6-8-2013

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Colonies.Client.Screen;
using Colonies.Client.Screen.Screens;
using Colonies.Client.Core;

namespace Colonies.Client
{
    /// <summary>
    /// Handles initialization, content, and game loops.
    /// </summary>
    public sealed class Driver : Microsoft.Xna.Framework.Game
    {
        private ScreenManager screenManager;
        private GraphicsDeviceManager graphicsDeviceManager;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            new Driver().Run();
        }

        /// <summary>
        /// Constructs a new driver.
        /// </summary>
        public Driver()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world, checking for collisions,
        /// gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            screenManager.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Initialization. 
        /// </summary>
        protected override void Initialize()
        {
            AssetManager.GetInstance().LoadAssets(this.Content);
            SettingsManager.GetInstance().ApplyVideoSettings(graphicsDeviceManager);

            screenManager = new ScreenManager(graphicsDeviceManager);

            screenManager.SetScreen(new MainScreen(screenManager));

            this.IsMouseVisible = true;
            this.IsFixedTimeStep = false;

            base.Initialize();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            screenManager.Draw();
            base.Draw(gameTime);
        }
    }
}
