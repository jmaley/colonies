// Title: AssetManager.cs
// Author: Joe Maley
// Date: 6-16-2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Colonies.Client.Core
{
    /// <summary>
    /// Used to load all assets at and to act as a look-up for all asset references.
    /// </summary>
    class AssetManager
    {
        private static AssetManager instance = null;

        private Dictionary<String, Object> assets = new Dictionary<String, Object>();

        /// <summary>
        /// Private to defeat instantiation.
        /// </summary>
        private AssetManager() { }

        /// <summary>
        /// Returns the unique instance of the asset manager.
        /// </summary>
        public static AssetManager GetInstance()
        {
            return instance == null ? instance = new AssetManager() : instance;
        }

        /// <summary>
        /// Loads all assets from the root directory of the specified content manager.
        /// </summary>
        /// <param name="contentManager">The XNA content manager.</param>
        public void LoadAssets(ContentManager contentManager)
        {
            String[] paths = Directory.GetFiles(contentManager.RootDirectory, "*", SearchOption.AllDirectories);
            for (int i = 0; i < paths.Length; i++)
            {
                // the 8 is used to remove "Content/" from the path
                String asset = paths[i].Substring(0, paths[i].LastIndexOf('.')).Substring(8);

                if (!assets.ContainsKey(asset))
                    assets.Add(asset, contentManager.Load<Object>(asset));
            }
        }

        /// <summary>
        /// Retrieves a loaded asset by path.
        /// </summary>
        /// <typeparam name="T">Asset type.</typeparam>
        /// <param name="path">Local path to asset.</param>
        /// <returns>Instance of the loaded asset.</returns>
        public T getAsset<T>(String path)
        {
            if (assets.ContainsKey(path))
                return (T)assets[path];

            throw new Exception("Asset '" + path + "' not found or loaded.");
        }
    }
}