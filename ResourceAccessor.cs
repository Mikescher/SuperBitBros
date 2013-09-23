using SuperBitBros.Properties;
using System;

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
                            return Resources.map_debug;
                        case 1:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
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
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 2:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 3:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 4:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 3:
                    switch (lvl)
                    {
                        case 1:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 2:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 3:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 4:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 4:
                    switch (lvl)
                    {
                        case 1:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 2:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 3:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 4:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 5:
                    switch (lvl)
                    {
                        case 1:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 2:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 3:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 4:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 6:
                    switch (lvl)
                    {
                        case 1:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 2:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 3:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 4:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 7:
                    switch (lvl)
                    {
                        case 1:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 2:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 3:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 4:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                case 8:
                    switch (lvl)
                    {
                        case 1:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 2:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 3:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        case 4:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                        default:
                            throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
                    }
                default:
                    throw new NotImplementedException(String.Format("Map {0}-{1} not implemented", world, lvl));
            }
        }
    }
}
