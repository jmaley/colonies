using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Colonies.Terrain;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Colonies.Client.Core;

namespace Colonies.Client.Screen.Screens
{
    class GameScreen : Screen
    {
        Camera camera;
        TerrainManager terrainManager;

        public GameScreen(ScreenManager screenManager)
            : base(screenManager)
        {
            camera = new Camera();
            terrainManager = new TerrainManager(camera);

            terrainManager.GenerateTerrain("World 1");
        }

        /// <summary>
        /// TO-DO
        /// </summary>
        protected override void HandleScreenInput()
        {
            camera.HandleInput();
        }

        /// <summary>
        /// TO-DO
        /// </summary>
        protected override void DrawScreen()
        {
            //screenManager.getGraphicsDeviceManager().GraphicsDevice.BlendState = BlendState.Opaque;
            screenManager.getGraphicsDeviceManager().GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            //screenManager.getGraphicsDeviceManager().GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;
            //screenManager.getGraphicsDeviceManager().GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            terrainManager.Draw();
        }

        /// <summary>
        /// TO-DO
        /// </summary>
        protected override void UpdateScreen(GameTime gameTime)
        {
            terrainManager.Update(gameTime);
        }
    }
}
