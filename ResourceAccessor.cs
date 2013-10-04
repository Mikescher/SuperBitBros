using SuperBitBros.Properties;
using System;
using System.Drawing;

namespace SuperBitBros
{
    public class ResourceAccessor
    {
        public static byte[] GetMap(int world, int lvl)
        {
            switch (world)
            {
                case 0:
                    switch (lvl)
                    {
                        case 0:
                            return Resources.map_00_00; // Debug Map
                        case 1:
                            return Resources.map_00_01; // LevelChooseMap
                        case 2:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 3:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 4:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 1:
                    switch (lvl)
                    {
                        case 1:
                            return Resources.map_01_01;

                        case 2:
                            return Resources.map_01_02;

                        case 3:
                            return Resources.map_01_03;

                        case 4:
                            return Resources.map_01_04;

                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 2:
                    switch (lvl)
                    {
                        case 1:
                            return Resources.map_02_01;

                        case 2:
                            return Resources.map_02_02;

                        case 3:
                            return Resources.map_02_03;

                        case 4:
                            return Resources.map_02_04;

                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 3:
                    switch (lvl)
                    {
                        case 1:
                            return Resources.map_03_01;

                        case 2:
                            return Resources.map_03_02;

                        case 3:
                            return Resources.map_03_03;

                        case 4:
                            return Resources.map_03_04;

                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 4:
                    switch (lvl)
                    {
                        case 1:
                            return Resources.map_04_01;

                        case 2:
                            return Resources.map_04_02;

                        case 3:
                            return Resources.map_04_03;

                        case 4:
                            return Resources.map_04_04;

                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 5:
                    switch (lvl)
                    {
                        case 1:
                            return Resources.map_05_01;

                        case 2:
                            return Resources.map_05_02;

                        case 3:
                            return Resources.map_05_03;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 4:
                            //return Resources.map_05_04;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 6:
                    switch (lvl)
                    {
                        case 1:
                            //return Resources.map_06_01;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 2:
                            //return Resources.map_06_02;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 3:
                            //return Resources.map_06_03;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 4:
                            //return Resources.map_06_04;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 7:
                    switch (lvl)
                    {
                        case 1:
                            //return Resources.map_07_01;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 2:
                            //return Resources.map_07_02;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 3:
                            //return Resources.map_07_03;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 4:
                            //return Resources.map_07_04;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 8:
                    switch (lvl)
                    {
                        case 1:
                            //return Resources.map_08_01;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 2:
                            //return Resources.map_08_02;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 3:
                            //return Resources.map_08_03;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 4:
                            //return Resources.map_08_04;
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                default:
                    throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
            }
        }

        public static Bitmap GetTexturePack(int id)
        {
            switch (id)
            {
                case 0:
                    return Resources.block_textures_0;
                case 1:
                    return Resources.block_textures_1;
                case 2:
                    return Resources.block_textures_2;
                default:
                    throw new NotImplementedException(String.Format("Texturepack {0} not implemented", id));
            }
        }
    }
}