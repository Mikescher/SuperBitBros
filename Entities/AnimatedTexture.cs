using SuperBitBros.OpenGL;
using System.Collections.Generic;

namespace SuperBitBros.Entities {

    class AnimatedTexture {
        private int pos;
        private int layer;
        private List<List<OGLTexture>> textureList;
        private int updateCount;

        public int animation_speed; // updates between Frames


        public AnimatedTexture() {
            textureList = new List<List<OGLTexture>>();
            updateCount = 0;
            layer = 0;
            animation_speed = 1;
            pos = 0;
        }

        public OGLTexture GetTexture() {
            if (textureList.Count == 0 || GetCount() == 0)
                return null;

            return textureList[layer][pos];
        }

        public void Update() {
            updateCount++;
            if (updateCount >= animation_speed) {
                pos = (pos + 1) % GetCount();
                updateCount = 0;
            }
        }

        private void createLayer(int l) {
            while (textureList.Count <= l)
                textureList.Add(new List<OGLTexture>());
        }

        public int GetCount() {
            return textureList[layer].Count;
        }

        public void Set(int layer, int pos) {
            SetLayer(layer);
            SetPos(pos);
        }

        public void SetLayer(int l) {
            layer = l;
            if (pos >= GetCount())
                pos = 0;
        }

        public void SetPos(int p) {
            pos = p;
        }

        public void Add(int layer, OGLTexture tex) {
            createLayer(layer);
            textureList[layer].Add(tex);
        }
    }
}
