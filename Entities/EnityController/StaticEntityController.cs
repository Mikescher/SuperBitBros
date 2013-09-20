using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using SuperBitBros.OpenGL.OGLMath;

namespace SuperBitBros.Entities.EnityController
{
    class StaticEntityController : AbstractEntityController
    {
        public StaticEntityController(DynamicEntity p)
            : base(p)
        {

        }

        public override void Update(KeyboardDevice keyboard, double ucorrection)
        {
            ent.DoCollisions();
        }

        public override bool IsActive()
        {
            return true;
        }

        public override void OnIllegalIntersection(Entity other)
        {
            //ignore
        }

        public override Vec2d GetDelta()
        {
            return Vec2d.Zero;
        }

        public override void OnHide()
        {
            //empty
        }

        public override void OnReshow()
        {
            //empty
        }
    }
}
