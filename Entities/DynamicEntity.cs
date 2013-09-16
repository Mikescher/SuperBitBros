using OpenTK.Input;
using SuperBitBros.Entities.Blocks;
using SuperBitBros.Entities.EnityController;
using SuperBitBros.OpenGL.OGLMath;
using SuperBitBros.Triggers;
using System;
using System.Collections.Generic;

namespace SuperBitBros.Entities {

    public abstract class DynamicEntity : Entity {
        public const double DETECTION_TOLERANCE = 0.005; // Touching Detection Tolerance
        public const int BLOCK_DETECTION_RANGE = 3;

        protected Stack<AbstractEntityController> controllerStack = new Stack<AbstractEntityController>();

        public DynamicEntity()
            : base() {
        }

        public virtual void OnAdd(GameModel model) {
            owner = model;
        }

        public override void Update(KeyboardDevice keyboard) {
            base.Update(keyboard);

            CallControllerStack(keyboard);
        }

        public bool HasController() {
            return controllerStack.Count > 0;
        }

        private void CallControllerStack(KeyboardDevice keyboard) {
            if (controllerStack.Count != 0) {
                controllerStack.Peek().Update(keyboard);

                if (!controllerStack.Peek().IsActive()) {
                    controllerStack.Pop();
                    if (HasController())
                        controllerStack.Peek().OnReshow();
                }
            }
        }

        public virtual void OnAfterMapGen() {
        }

        public void DoCollisions() {
            // Collide with Entities

            Rect2d nocollnewpos = new Rect2d(
                new Vec2d(
                    position.X - DETECTION_TOLERANCE,
                    position.Y - DETECTION_TOLERANCE),
                width + DETECTION_TOLERANCE * 2,
                height + DETECTION_TOLERANCE * 2);
            Rect2d currPosition = GetPosition();

            foreach (Entity e in owner.GetCurrentEntityList()) {
                if (nocollnewpos.IsColldingWith(e.GetPosition()) && e != this) {
                    bool isColl = currPosition.IsColldingWith(e.GetPosition());
                    bool isTouch = currPosition.IsTouching(e.GetPosition());
                    bool isBlock = Entity.TestBlocking(e, this);

                    e.onCollide(this, false, isBlock, isColl, isTouch);
                    this.onCollide(e, true, isBlock, isColl, isTouch);

                    if (isBlock && isColl) {
                        Console.Error.WriteLine("Entity PUSHBACK !!!");
                        OnIllegalIntersection(e);
                    }
                }
            }

            // Collide with Blocks

            int left = (int)((position.X - BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int bottom = (int)((position.Y - BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);
            int right = (int)Math.Ceiling((position.X + width + BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int top = (int)Math.Ceiling((position.Y + height + BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);

            for (int x = left; x < right; x++) {
                for (int y = bottom; y < top; y++) {
                    Block b = owner.GetBlock(x, y);
                    if (b != null && nocollnewpos.IsColldingWith(b.GetPosition())) {
                        bool isColl = currPosition.IsColldingWith(b.GetPosition());
                        bool isTouch = currPosition.IsTouching(b.GetPosition());
                        bool isBlock = Entity.TestBlocking(b, this);

                        b.onCollide(this, false, isBlock, isColl, isTouch);
                        this.onCollide(b, true, isBlock, isColl, isTouch);

                        if (isBlock && isColl) {
                            Console.Error.WriteLine("Block PUSHBACK !!!");
                            OnIllegalIntersection(b);
                        }
                    }
                }
            }

            // Collide with TriggerZones

            for (int x = left; x < right; x++) {
                for (int y = bottom; y < top; y++) {
                    List<Trigger> tlist = owner.getTriggerList(x, y);

                    if (tlist != null) {
                        foreach (Trigger t in tlist) {
                            bool isColl = currPosition.IsColldingWith(t.GetPosition());

                            if (isColl) {
                                t.OnCollide(this);
                            }
                        }
                    }
                }
            }
        }

        private void OnIllegalIntersection(Entity other) {
            if (HasController()) {
                controllerStack.Peek().OnIllegalIntersection(other);
            }
        }

        public bool IsOnGround() {
            Rect2d newpos = new Rect2d(new Vec2d(position.X, position.Y - DETECTION_TOLERANCE), width, height);

            //TEST BLOCKS IN RANGE

            int left = (int)((newpos.br.X - DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int bottom = (int)((newpos.br.Y - DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);
            int right = (int)Math.Ceiling((newpos.tl.X + DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int top = (int)Math.Ceiling((newpos.tl.Y + DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);

            for (int x = left; x < right; x++)
                for (int y = bottom; y < top; y++) {
                    Block b = owner.GetBlock(x, y);
                    if (b != null && Entity.TestBlocking(b, this) && newpos.IsColldingWith(b.GetPosition()) && b.GetMiddle().Y < this.GetMiddle().Y)
                        return true;
                }

            // TEST ENTITIES

            foreach (Entity e in owner.entityList)
                if (e != this && Entity.TestBlocking(e, this) && newpos.IsColldingWith(e.GetPosition()) && e.GetMiddle().Y < this.GetMiddle().Y)
                    return true;

            return false;
        }

        public bool IsOnCeiling() {
            Rect2d newpos = new Rect2d(new Vec2d(position.X, position.Y + DETECTION_TOLERANCE), width, height);

            //TEST BLOCKS IN RANGE

            int left = (int)((newpos.br.X - DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int bottom = (int)((newpos.br.Y - DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);
            int right = (int)Math.Ceiling((newpos.tl.X + DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int top = (int)Math.Ceiling((newpos.tl.Y + DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);

            for (int x = left; x < right; x++)
                for (int y = bottom; y < top; y++) {
                    Block b = owner.GetBlock(x, y);
                    if (b != null && Entity.TestBlocking(b, this) && newpos.IsColldingWith(b.GetPosition()) && b.GetMiddle().Y > this.GetMiddle().Y)
                        return true;
                }

            // TEST ENTITIES

            foreach (Entity e in owner.entityList)
                if (e != this && Entity.TestBlocking(e, this) && newpos.IsColldingWith(e.GetPosition()) && e.GetMiddle().Y > this.GetMiddle().Y)
                    return true;

            return false;
        }

        public bool IsCollidingRight() {
            Rect2d newpos = new Rect2d(new Vec2d(position.X + DETECTION_TOLERANCE, position.Y), width, height);

            //TEST BLOCKS IN RANGE

            int left = (int)((newpos.br.X - DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int bottom = (int)((newpos.br.Y - DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);
            int right = (int)Math.Ceiling((newpos.tl.X + DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int top = (int)Math.Ceiling((newpos.tl.Y + DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);

            for (int x = left; x < right; x++)
                for (int y = bottom; y < top; y++) {
                    Block b = owner.GetBlock(x, y);
                    if (b != null && Entity.TestBlocking(b, this) && newpos.IsColldingWith(b.GetPosition()) && b.GetMiddle().X > this.GetMiddle().X)
                        return true;
                }

            // TEST ENTITIES

            foreach (Entity e in owner.entityList)
                if (e != this && Entity.TestBlocking(e, this) && newpos.IsColldingWith(e.GetPosition()) && e.GetMiddle().X > this.GetMiddle().X)
                    return true;

            return false;
        }

        public bool IsCollidingLeft() {
            Rect2d newpos = new Rect2d(new Vec2d(position.X - DETECTION_TOLERANCE, position.Y), width, height);

            //TEST BLOCKS IN RANGE

            int left = (int)((newpos.br.X - DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int bottom = (int)((newpos.br.Y - DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);
            int right = (int)Math.Ceiling((newpos.tl.X + DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_WIDTH) / Block.BLOCK_WIDTH);
            int top = (int)Math.Ceiling((newpos.tl.Y + DynamicEntity.BLOCK_DETECTION_RANGE * Block.BLOCK_HEIGHT) / Block.BLOCK_HEIGHT);

            for (int x = left; x < right; x++)
                for (int y = bottom; y < top; y++) {
                    Block b = owner.GetBlock(x, y);
                    if (b != null && Entity.TestBlocking(b, this) && newpos.IsColldingWith(b.GetPosition()) && b.GetMiddle().X < this.GetMiddle().X)
                        return true;
                }

            // TEST ENTITIES

            foreach (Entity e in owner.entityList)
                if (e != this && Entity.TestBlocking(e, this) && newpos.IsColldingWith(e.GetPosition()) && e.GetMiddle().X < this.GetMiddle().X)
                    return true;

            return false;
        }

        public Vec2d GetMovement() {
            if (!HasController() || !(controllerStack.Peek() is AbstractNewtonEntityController))
                return Vec2d.Zero;
            else
                return (controllerStack.Peek() as AbstractNewtonEntityController).movementDelta;
        }

        protected void AddController(AbstractEntityController c) {
            if (HasController())
                controllerStack.Peek().OnHide();

            controllerStack.Push(c);
        }

        public void Kill() {
            owner.RemoveEntity(this);
        }
    }
}