// Title: Map.cs
// Author: Joe Maley
// Date: 7-10-2013

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Threading;
using Colonies.Client.Core;

namespace Colonies.Terrain
{
    /// <summary>
    /// The map creates, stores, and draws terrain blocks.
    /// 
    /// Blocks are stored in 'chunks'. Chunks are just segments of the map
    /// that are stored together on the hard disk. It is not feasible to store
    /// all of the world blocks into memory, so we load them in as we need them.
    /// </summary>
    class TerrainManager
    {
        Camera camera;

        // specifies the maximum number of chunks in the world
        private const int TERRAIN_CHUNKS_X = 300;
        private const int TERRAIN_CHUNKS_Z = 300;

        // size of the active chunks
        private const int ACTIVE_CHUNKS_X = 3;
        private const int ACTIVE_CHUNKS_Z = 3;

        // the active chunks is an array of chunks stored in memory
        private Chunk[] activeChunks = new Chunk[ACTIVE_CHUNKS_X * ACTIVE_CHUNKS_Z];

        // all neccessary cube terrain models
        private Model[] models = { AssetManager.GetInstance().getAsset<Model>("GrassCube"),
                                   AssetManager.GetInstance().getAsset<Model>("StoneCube"),
                                   AssetManager.GetInstance().getAsset<Model>("BrickCube") };



        // used to determine if neccessary to update active chunks
        int currentChunkIndex = -1;

        private Stream stream;

        Vector3 modelPosition = Vector3.Zero;
        Model myModel;
        //Matrix[] transforms;

        String filePath;

        BoundingBox boundingBox = new BoundingBox();
        Vector3 boundingMin = new Vector3();
        Vector3 boundingMax = new Vector3();

        Thread thread;
        bool refreshChunks = false;

        /// <summary>
        /// Instantiates the 
        /// </summary>
        public TerrainManager(Camera camera) {
            this.camera = camera;

            thread = new Thread(ThreadRefreshActiveChunks);
            thread.Start();
        }

        public void GenerateTerrain(String worldName)
        {
            String directory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Worlds", worldName);
            Console.WriteLine("DIR: " + directory);
            System.IO.Directory.CreateDirectory(directory);

            filePath = Path.Combine(directory, "map.chunks");
            Console.WriteLine("FP: " + filePath);
            stream = new FileStream(filePath, FileMode.Create);

            for (int i = 0; i < TERRAIN_CHUNKS_X * TERRAIN_CHUNKS_Z; i++)
            {
                byte[] pos = BitConverter.GetBytes((long) -1);
                stream.Write(pos, 0, pos.Length);
            }

            //stream.Close();

            for (int x = 0; x < ACTIVE_CHUNKS_X; x++)
            {
                for (int z = 0; z < ACTIVE_CHUNKS_Z; z++)
                {
                    int chunkIndex = GetChunkIndex(x, z);
                    int activeChunkIndex = GetActiveChunkIndex(x, z);

                    //Chunk chunk = new Chunk(filePath, chunkIndex);
                    Chunk chunk = new Chunk(stream, chunkIndex);
                    activeChunks[activeChunkIndex] = chunk;
                }
            }
        }

        private int GetChunkIndex(int x, int z)
        {
            x = (x % TERRAIN_CHUNKS_X + TERRAIN_CHUNKS_X) % TERRAIN_CHUNKS_X;
            z = (z % TERRAIN_CHUNKS_Z + TERRAIN_CHUNKS_Z) % TERRAIN_CHUNKS_Z;

            return z * TERRAIN_CHUNKS_X + x;
        }

        private int GetActiveChunkIndex(int x, int z)
        {
            return z * 3 + x;
        }

        public void Update(GameTime gameTime)
        {
            RefreshActiveChunks();
        }
        
        private void RefreshActiveChunks()
        {
            int x = (int)camera.Position.X / Chunk.SIZE;
            int z = (int)camera.Position.Z / Chunk.SIZE;

            int index = GetChunkIndex(x, z);

            if (index == currentChunkIndex)
                return;

            currentChunkIndex = index;

            refreshChunks = true;
        }

        public void ThreadRefreshActiveChunks()
        {
            while (true)
            {
                if (refreshChunks == true)
                {
                    int x = (int)camera.Position.X / Chunk.SIZE;
                    int z = (int)camera.Position.Z / Chunk.SIZE;

                    activeChunks[GetActiveChunkIndex(0, 0)] = new Chunk(stream, GetChunkIndex(x - 1, z - 1));
                    activeChunks[GetActiveChunkIndex(1, 0)] = new Chunk(stream, GetChunkIndex(x, z - 1));
                    activeChunks[GetActiveChunkIndex(2, 0)] = new Chunk(stream, GetChunkIndex(x + 1, z - 1));
                    activeChunks[GetActiveChunkIndex(0, 1)] = new Chunk(stream, GetChunkIndex(x - 1, z));
                    activeChunks[GetActiveChunkIndex(1, 1)] = new Chunk(stream, GetChunkIndex(x, z));
                    activeChunks[GetActiveChunkIndex(2, 1)] = new Chunk(stream, GetChunkIndex(x + 1, z));
                    activeChunks[GetActiveChunkIndex(0, 2)] = new Chunk(stream, GetChunkIndex(x - 1, z + 1));
                    activeChunks[GetActiveChunkIndex(1, 2)] = new Chunk(stream, GetChunkIndex(x, z + 1));
                    activeChunks[GetActiveChunkIndex(2, 2)] = new Chunk(stream, GetChunkIndex(x + 1, z + 1));

                    refreshChunks = false;
                }
            }
        }

        public void Draw()
        {
            int index;
            byte[] blocks;

            int i, j;
            for (i = 0; i < ACTIVE_CHUNKS_X; i++)
            {
                for (j = 0; j < ACTIVE_CHUNKS_Z; j++)
                {
                    index = activeChunks[GetActiveChunkIndex(i, j)].Index;

                    boundingMin.Y = 0;
                    boundingMin.X = (index % TERRAIN_CHUNKS_X - 0) * Chunk.SIZE;
                    boundingMin.Z = (index / TERRAIN_CHUNKS_X - 0) * Chunk.SIZE;

                    boundingMax.Y = Chunk.SIZE;
                    boundingMax.X = (index % TERRAIN_CHUNKS_X - 0) * Chunk.SIZE + Chunk.SIZE;
                    boundingMax.Z = (index / TERRAIN_CHUNKS_X - 0) * Chunk.SIZE + Chunk.SIZE;

                    boundingBox.Min = boundingMin;
                    boundingBox.Max = boundingMax;

                    if (!camera.Frustum.Intersects(boundingBox))
                        continue;
                   
                    blocks = activeChunks[GetActiveChunkIndex(i, j)].Blocks;

                    int x, y, z;
                    for (x = 0; x < Chunk.SIZE; x++)
                    {
                        for (z = 0; z < Chunk.SIZE; z++)
                        {
                            for (y = 0; y < Chunk.SIZE; y++)
                            {
                                int modelIndex = blocks[x * Chunk.SIZE * Chunk.SIZE + y * Chunk.SIZE + z];

                                //if (modelIndex == 0)
                                  //  continue;

                                modelPosition.X = (x + (Chunk.SIZE * (index % TERRAIN_CHUNKS_X)));
                                modelPosition.Z = (z + (Chunk.SIZE * (index / TERRAIN_CHUNKS_X)));
                                modelPosition.Y = y;

                                //myModel = models[modelIndex + 1];

                                myModel = models[blocks[x * Chunk.SIZE * Chunk.SIZE + y * Chunk.SIZE + z]];

                                //transforms = new Matrix[myModel.Bones.Count];
                                //myModel.CopyAbsoluteBoneTransformsTo(transforms);

                                foreach (ModelMesh mesh in myModel.Meshes)
                                {

                                    foreach (BasicEffect effect in mesh.Effects)
                                    {
                                        effect.EnableDefaultLighting();
                                        effect.World = Matrix.CreateTranslation(modelPosition);
                                        effect.View = camera.View;
                                        effect.Projection = camera.Projection;
                                    }
                                  
                                    mesh.Draw();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
