using OpenTK.Input;
using SuperBitBros.Entities;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.DynamicEntities;
using SuperBitBros.OpenGL.OGLMath;
using System;
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
            foreach (Trigger t in owner.triggerList)
            {
                TeleportEntryZone tel = t as TeleportEntryZone;
                if (tel != null && tel.GetTeleportID() == id)
                {
                    pos.X = Math.Min(pos.X, tel.position.X);
                    pos.Y = Math.Min(pos.Y, tel.position.Y);
                }
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
