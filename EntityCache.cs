using SuperBitBros.Entities;
using System;
using System.Collections.Generic;

namespace SuperBitBros
{
    public class EntityCache
    {
        public const int DISTANCE_COUNT = 50 + 1;

        public static readonly int RenderTypeCount = Enum.GetNames(typeof(EntityRenderType)).Length;

        private List<Entity>[,] cache;

        public EntityCache()
        {
            cache = new List<Entity>[RenderTypeCount, DISTANCE_COUNT + 1];
            for (int rt = 0; rt < RenderTypeCount; rt++)
            {
                for (int dist = 0; dist <= DISTANCE_COUNT; dist++)
                {
                    cache[rt, dist] = new List<Entity>();
                }
            }
        }

        public void AddEntity(Entity e)
        {
            cache[(int)e.GetRenderType(), e.GetDistance()].Add(e);
        }

        public void RemoveEntity(Entity e)
        {
            if (!cache[(int)e.GetRenderType(), e.GetDistance()].Remove(e))
            {
                Console.Error.WriteLine("Could not remove Entity " + e + " from EntityCache");
            }
        }

        public List<Entity> GetEntitiesAt(int depth, int type)
        {
            return cache[type, depth];
        }
    }
}