using Entities.SuperBitBros;
using OpenTK.Input;
using SuperBitBros.OpenGL.Entities.Blocks;
using System;

namespace SuperBitBros.OpenGL.Entities {
    class PiranhaPlant : Mob {
        private const int UPDATE_SPEED = 5;
        private const int CYCLUS_SPEED = 60;
        private const int STATE_COUNT = 13;

        private int lastUpdate = 0;
        private bool direction = true;
        private int state = 0;

        private Rectangle2d pipeUnder = null;

        public PiranhaPlant() {
            distance = Entity.DISTANCE_MOBS;
            width = 2 * Block.BLOCK_WIDTH;
            height = (Block.BLOCK_HEIGHT * 2.0) * (state / STATE_COUNT);

            texture = Textures.piranhaplant_sheet.GetTextureWrapper(0);
        }

        public override Rectangle2d GetTexturePosition() {
            return new Rectangle2d(position, 2 * Block.BLOCK_WIDTH, Block.BLOCK_HEIGHT * 2);
        }

        public override void Update(KeyboardDevice keyboard) {
            base.Update(keyboard);

            if (((state == 0 || state == STATE_COUNT - 1) && lastUpdate > CYCLUS_SPEED) || (!(state == 0 || state == STATE_COUNT - 1) && lastUpdate > UPDATE_SPEED)) {
                state = state + ((direction) ? (1) : (-1));
                if (state == 0)
                    direction = true;
                if (state == STATE_COUNT - 1)
                    direction = false;

                texture = Textures.piranhaplant_sheet.GetTextureWrapper(state);
                height = (Block.BLOCK_HEIGHT * 2.0) * (state * 1.0 / STATE_COUNT);
                lastUpdate = 0;
            }
            lastUpdate++;

            if (state == 0)
                testPlayerBlocking();
        }

        public override void OnAfterMapGen() {
            base.OnAfterMapGen();
            calcUnderlyingPipe();
        }

        private void calcUnderlyingPipe() {
            int minX = (int)(position.X / Block.BLOCK_WIDTH);
            int maxX = minX;

            int minY = (int)((position.Y - 0.5) / Block.BLOCK_HEIGHT);
            int maxY = minY;

            Block blockUnder = owner.GetBlock(minX, maxY);
            if (blockUnder == null) {
                pipeUnder = GetPosition();
                return;
            }
            Type typeUnder = blockUnder.GetType();

            for (; ; ) { // minX
                Block b = owner.GetBlock(minX - 1, maxY);
                if (b != null && b.GetType() == typeUnder)
                    minX--;
                else
                    break;
            }

            for (; ; ) { // maxX
                Block b = owner.GetBlock(maxX + 1, maxY);
                if (b != null && b.GetType() == typeUnder)
                    maxX++;
                else
                    break;
            }

            for (; ; ) { // minY
                Block b = owner.GetBlock(maxX, minY - 1);
                if (b != null && b.GetType() == typeUnder)
                    minY--;
                else
                    break;
            }

            for (; ; ) { // maxY
                Block b = owner.GetBlock(maxX, maxY + 1);
                if (b != null && b.GetType() == typeUnder)
                    maxY++;
                else
                    break;
            }

            int width = maxX - minX + 1;
            int height = maxY - minY + 1;

            pipeUnder = new Rectangle2d(minX * Block.BLOCK_WIDTH, minY * Block.BLOCK_HEIGHT, width * Block.BLOCK_WIDTH, height * Block.BLOCK_HEIGHT);
        }

        private void testPlayerBlocking() {
            Player p = ((GameWorld)owner).player;
            Rectangle2d prect = new Rectangle2d(
                p.position.X - DynamicEntity.DETECTION_TOLERANCE,
                p.position.Y - DynamicEntity.DETECTION_TOLERANCE,
                p.width + DynamicEntity.DETECTION_TOLERANCE * 2,
                p.height + DynamicEntity.DETECTION_TOLERANCE);

            if (prect.isColldingWith(pipeUnder)) {
                lastUpdate = 0;
            }
        }

        public override void OnHeadJump(Entity e) {
            if (e.GetType() == typeof(Player))
                Console.Out.WriteLine("DEAD_PP_N00b");
        }

        public override void OnTouch(Entity e, bool isCollider, bool isBlockingMovement) {
            if (e.GetType() == typeof(Player))
                Console.Out.WriteLine("DEAD_PP");
        }
    }
}
