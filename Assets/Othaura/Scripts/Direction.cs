namespace Othaura {

    using UnityEngine;

    public struct Direction {
        
        public static readonly Vector2i zero = new Vector2i(0, 0);
        public static readonly Vector2i none = new Vector2i(0, 0);

        public static readonly Vector2i N = new Vector2i(0, -1);
        public static readonly Vector2i NE = new Vector2i(1, -1);
        public static readonly Vector2i E = new Vector2i(1, 0);
        public static readonly Vector2i SE = new Vector2i(1, 1);
        public static readonly Vector2i S = new Vector2i(0, 1);
        public static readonly Vector2i SW = new Vector2i(-1, 1);
        public static readonly Vector2i W = new Vector2i(-1, 0);
        public static readonly Vector2i NW = new Vector2i(-1, -1);

        public static readonly Vector2i[] All = new Vector2i[] { N, NE, E, SE, S, SW, W, NW };
        public static readonly Vector2i[] Cardinal = new Vector2i[] { N, E, S, W };
        public static readonly Vector2i[] Diagonal = new Vector2i[] { NE, SE, SW, NW };

        public static Vector2i Left45(Vector2i v) {
            return new Vector2i(Mathf.Clamp(v.x + v.y, -1, 1), Mathf.Clamp(v.y - v.x, -1, 1));
        }

        public static Vector2i Right45(Vector2i v) {

            return new Vector2i(Mathf.Clamp(v.x - v.y, -1, 1), Mathf.Clamp(v.y + v.x, -1, 1));
        }

        public static Vector2i Left90(Vector2i v) {

            return new Vector2i(v.y, -v.x);
        }

        public static Vector2i Right90(Vector2i v) {
            
            return new Vector2i(-v.y, v.x);
        }
    }
}


