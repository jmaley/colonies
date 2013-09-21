using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;

namespace Colonies.Terrain
{
    class Chunk
    {
        private int index;
        private byte[] blocks = new byte[SIZE * SIZE * SIZE];
        private byte[] maskedBlocks = new byte[SIZE * SIZE * SIZE];

        public const int SIZE = 16;

        //private String filePath;
        private Stream stream;

        //private float length = 0f;
        //private BoundingBox bounds = default(BoundingBox);
        //Vector3 center;
        //private Chunk[] children = null;

        public int Index { get { return index; } }
        public byte[] Blocks { get { return blocks; } }
        //public byte[] MaskedBlocks { get { return maskedBlocks; } }

        /*public Chunk(String filePath, int index)
        {
            this.filePath = filePath;
            this.index = index;

            Load();
        }*/

        public Chunk(Stream stream, int index)
        {
            //this.filePath = filePath;
            this.stream = stream;
            this.index = index;

            Load();

            //Vector3 min = this.center - new Vector3(length / 2.0f);
            //Vector3 max = this.center + new Vector3(length / 2.0f);
            //this.bounds = new BoundingBox(min, max);

            //if (this.depth < maxDepth)
            //{
            //    this.Split(maxDepth);
            //}
        }

        /*private void Split(int maxDepth)
        {
            this.children = new Chunk[Chunk.ChildCount];
            int depth = this.depth + 1;
            float quarter = this.length / this.looseness / 4f;

            this.children[0] = new Chunk(maxDepth, depth, this.center + new Vector3(-quarter, quarter, -quarter));
            this.children[1] = new Chunk(maxDepth, depth, this.center + new Vector3(quarter, quarter, -quarter));
            this.children[2] = new Chunk(maxDepth, depth, this.center + new Vector3(-quarter, quarter, quarter));
            this.children[3] = new Chunk(maxDepth, depth, this.center + new Vector3(quarter, quarter, quarter));
            this.children[4] = new Chunk(maxDepth, depth, this.center + new Vector3(-quarter, -quarter, -quarter));
            this.children[5] = new Chunk(maxDepth, depth, this.center + new Vector3(quarter, -quarter, -quarter));
            this.children[6] = new Chunk(maxDepth, depth, this.center + new Vector3(-quarter, -quarter, quarter));
            this.children[7] = new Chunk(maxDepth, depth, this.center + new Vector3(quarter, -quarter, quarter));
        }*/

        public Chunk(Chunk chunk)
        {
            chunk.Blocks.CopyTo(blocks, 0);
            index = chunk.Index;

            //updateMask();
        }

        private void GenerateBlocks()
        {
            Random random = new Random();

            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    for (int z = 0; z < SIZE; z++)
                    {
                        blocks[x * SIZE * SIZE + y * SIZE + z] = (byte)random.Next(3);
                    }
                }
            }

            //updateMask();
        }

        /*private void updateMask()
        {
            blocks.CopyTo(maskedBlocks, 0);
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    for (int z = 0; z < SIZE; z++)
                    {
                        //maskedBlocks[x * SIZE * SIZE + y * SIZE + z] = blocks[x * SIZE * SIZE + y * SIZE + z];
                        // check up
                       // if (y < SIZE - 1 && blocks[x * SIZE * SIZE + (y + 1) * SIZE + z] == 0)
                       // {
                            //maskedBlocks[x * SIZE * SIZE + y * SIZE + z] = blocks[x * SIZE * SIZE + y * SIZE + z];
                           // continue;
                       // }
                        // check down

                        // check left

                        // check right

                        // check close

                        // check far

                        //maskedBlocks[x * SIZE * SIZE + y * SIZE + z] = 0;
                    }
                }
            }
        }*/

        private void Load()
        {
           // Stream stream = new FileStream(filePath, FileMode.Open);

            // read the position from the position-list at the
            // top of the file, at the specified index
            stream.Seek(index * sizeof(long), SeekOrigin.Begin);
            byte[] posBuffer = new byte[sizeof(long)];
            stream.Read(posBuffer, 0, posBuffer.Length);
            long pos = BitConverter.ToInt64(posBuffer, 0);

            if (pos == -1) // default value
            {
                //stream.Close();
                GenerateBlocks();
                Save();
            }
            else // position exists
            {
                // read in the blocks from the specified position
                stream.Seek(pos, SeekOrigin.Begin);
                stream.Read(blocks, 0, blocks.Length);
                //stream.Close();
            }
        }

        public void Save()
        {
            //Stream stream = new FileStream(filePath, FileMode.Open);

            // read the position from the position-list at the
            // top of the file, at the specified index
            stream.Seek(index * sizeof(long), SeekOrigin.Begin);
            byte[] posBuffer = new byte[sizeof(long)];
            stream.Read(posBuffer, 0, posBuffer.Length);
            long pos = BitConverter.ToInt64(posBuffer, 0);

            if (pos == -1) // default value
            {
                long eof = stream.Seek(0, SeekOrigin.End);
                stream.Write(blocks, 0, blocks.Length);

                stream.Seek(index * sizeof(long), SeekOrigin.Begin);
                byte[] eofBuffer = BitConverter.GetBytes(eof);
                stream.Write(eofBuffer, 0, eofBuffer.Length);
            }
            else // position exists
            {
                stream.Seek(pos, SeekOrigin.Begin);
                stream.Write(blocks, 0, blocks.Length);
            }

            //stream.Close();
        }
    }
}
