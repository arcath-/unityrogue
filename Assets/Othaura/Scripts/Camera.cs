namespace Othaura {

    using UnityEngine;

    public class Camera {
        
        private Vector2 mPos;
        private float mSpeed = 0.1f;

        public void SetPos(Entity entity) {

            //old was using RB.SpriteSize(0).width and .height. changed to C.PIXEL_SIZE
            mPos = new Vector2(entity.pos.x * C.PIXEL_SIZE, entity.pos.y * C.PIXEL_SIZE);
        }

        public void Follow(Entity entity) {

            //old was using RB.SpriteSize(0).width and .height. changed to C.PIXEL_SIZE
            mPos.x += ((entity.pos.x * C.PIXEL_SIZE) - mPos.x) * mSpeed;
            mPos.y += ((entity.pos.y * C.PIXEL_SIZE) - mPos.y) * mSpeed;
        }

        public void Apply() {

            Vector2i pos = mPos;
            //old was using RB.SpriteSize(0).width and .height. changed to C.PIXEL_SIZE
            pos.x -= RB.DisplaySize.width / 2 - (C.PIXEL_SIZE / 2);
            pos.y -= RB.DisplaySize.height / 2 - (C.PIXEL_SIZE / 2);

            RB.CameraSet(pos);
        }
    }
}

