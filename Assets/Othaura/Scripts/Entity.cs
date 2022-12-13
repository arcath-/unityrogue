namespace Othaura {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Entity {

        public Vector2i pos;
        public PackedSpriteID sprite;
        public Color32 color;
        public bool blocks;
        public FastString name = new FastString(32);

        public Entity(Vector2i pos, PackedSpriteID sprite, Color32 color, FastString name, bool blocks = false) {

            this.pos = pos;
            this.sprite = sprite;
            this.color = color;
            this.name.Append(name);
            this.blocks = blocks;
        }

        public void Move(Vector2i delta) {
            
            pos += delta;
        }
    }

}
