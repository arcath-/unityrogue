namespace Othaura {

    using UnityEngine;

    public class Tile {

        public enum Type {

            EMPTY = 0,
            WALL_BRICK,
            FLOOR_COBBLESTONE,
        }

        public static readonly Tile TEMPLATE_EMPTY = new Tile(Type.EMPTY, S.EMPTY, Color.grey, false);
        public static readonly Tile TEMPLATE_WALL_BRICK = new Tile(Type.WALL_BRICK, S.WALL_BRICK01, Color.white, true);
        public static readonly Tile TEMPLATE_FLOOR_COBBLESTONE = new Tile(Type.FLOOR_COBBLESTONE, S.FLOOR_COBBLESTONE01, Color.white, false);

        public Type type;
        public bool blocked;
        public bool blockSight;

        public PackedSpriteID sprite;
        public Color32 color;

        public Tile(Tile template) {

            type = template.type;
            blocked = template.blocked;
            blockSight = template.blockSight;

            sprite = template.sprite;
            color = template.color;
        }

        public Tile(Type type, PackedSpriteID sprite, Color32 color, bool blocked) {

            this.blocked = blocked;
            this.blockSight = blocked;

            this.sprite = sprite;
            this.color = color;
        }

        public Tile(Type type, PackedSpriteID sprite, Color32 color, bool blocked, bool blockSight) {
            
            this.blocked = blocked;
            this.blockSight = blockSight;

            this.sprite = sprite;
            this.color = color;
        }
    }
}


