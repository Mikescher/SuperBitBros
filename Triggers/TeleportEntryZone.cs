using OpenTK.Input;
using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.Entities.DynamicEntities.Mobs;
using SuperBitBros.OpenGL.OGLMath;
using System.Collections.Generic;
using System.Drawing;

namespace SuperBitBros.Triggers
{
    public class TeleportEntryZone : Trigger
    {
        private const int COOLDOWN = 3;

        private const int ANY = 0;

        private int active = 0;
        private int id;

        private Vec2d deltaTeleportation = Vec2d.Zero;

        private TeleportEntryZone baseZone;

        public TeleportEntryZone(Vec2i pos, int ident)
            : base(pos)
        {
            id = ident;
        }

        public override void OnCollide(DynamicEntity collider)
        {
            Player p = collider as Player;
            if (p != null && baseZone.active == 0)
            {
                (owner as GameWorld).TeleportPlayer(deltaTeleportation);
                baseZone.active = COOLDOWN;
            }

            DynamicEntity m = collider as DynamicEntity;
            if (m != null && baseZone.active == 0)
            {
                m.position += deltaTeleportation;
            }
        }

        public override void Update(KeyboardDevice keyboard)
        {
            if (active > 0)
                active--;
        }

        public override void OnAfterMapGen()
        {
            Vec2i mypos = GetTeleportPosition();

            foreach (Trigger t in owner.triggerList)
            {
                TeleportExitZone tel = t as TeleportExitZone;
                if (tel != null && tel.GetTeleportID() == id)
                {
                    Vec2i exitpos = tel.GetTeleportPosition();

                    Vec2i delta = exitpos - mypos;

                    deltaTeleportation = delta * Block.BLOCK_SIZE;

                    return;
                }
            }
        }

        public Vec2i GetTeleportPosition()
        {
            Vec2i pos = new Vec2i(position);

            for (; ; )
            {
                List<Trigger> tx, ty;

                ty = owner.getTriggerList(pos.X, pos.Y - 1);
                tx = owner.getTriggerList(pos.X - 1, pos.Y);
                if (ty != null && ty.Exists(x => ((x is TeleportEntryZone) && ((x as TeleportEntryZone).id == id))))
                {
                    pos.Y -= 1;
                }
                else if (tx != null && tx.Exists(x => ((x is TeleportEntryZone) && ((x as TeleportEntryZone).id == id))))
                {
                    pos.X -= 1;
                }
                else
                    break;
            }


            baseZone = owner.getTriggerList(pos.X, pos.Y).Find(x => x is TeleportEntryZone) as TeleportEntryZone;

            return pos;
        }

        public int GetTeleportID()
        {
            return id;
        }

        public static Color color = Color.FromArgb(ANY, 0, 200);

        public static Color GetColor()
        {
            return color;
        }

        public override Color GetTriggerColor()
        {
            return GetColor();
        }
    }
}
