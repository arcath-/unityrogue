namespace Othaura {

    using UnityEngine;

    public class Camera {
        
        private Vector2 mPos;
        private float mSpeed = 0.1f;

        public void SetPos(Entity entity) {

            //old was using RB.SpriteSize(0).width and .height. changed to C.MOVE_MULTIPLE
            mPos = new Vector2(entity.pos.x * C.MOVE_MULTIPLE, entity.pos.y * C.MOVE_MULTIPLE);
        }

        public void Follow(Entity entity) {

            //old was using RB.SpriteSize(0).width and .height. changed to C.MOVE_MULTIPLE
            mPos.x += ((entity.pos.x * C.MOVE_MULTIPLE) - mPos.x) * mSpeed;
            mPos.y += ((entity.pos.y * C.MOVE_MULTIPLE) - mPos.y) * mSpeed;
        }

        public void Apply() {

            Vector2i pos = mPos;
            //old was using RB.SpriteSize(0).width and .height. changed to C.MOVE_MULTIPLE
            pos.x -= RB.DisplaySize.width / 2 - (C.MOVE_MULTIPLE / 2);
            pos.y -= RB.DisplaySize.height / 2 - (C.MOVE_MULTIPLE / 2);

            RB.CameraSet(pos);
        }
    }
}

