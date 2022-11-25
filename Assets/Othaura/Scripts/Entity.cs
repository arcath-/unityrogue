namespace Othaura {

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Entity {

        public Vector2i pos;
        public PackedSpriteID sprite;
        public Color32 color;

        public Entity(Vector2i pos, PackedSpriteID sprite, Color32 color) {

            this.pos = pos;
            this.sprite = sprite;
            this.color = color;
        }

        public void Move(Vector2i delta) {
            
            pos += delta;
        }
    }

}
