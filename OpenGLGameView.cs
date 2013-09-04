using System;

namespace SuperBitBros
{
    class OpenGLGameView : OpenGLView
    {
        public OpenGLGameView(GameModel model) : base(model) 
        {
            //
        }

        protected override void OnRender(object sender, EventArgs e)
        {
            StartRender();

            foreach (Entity entity in model.entityList)
            {
                renderRectangle(entity.GetTopLeft(), entity.GetTopRight(), entity.GetBottomRight(), entity.GetBottomLeft(), entity.distance);
            }

            EndRender();
        }
    }
}