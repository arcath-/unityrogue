namespace Othaura { 
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class RenderFunctions
    {
        public static void RenderAll(List<Entity> entities)
        {
            RB.DrawMapLayer(C.LAYER_TERRAIN);

            var previousTintColor = RB.TintColorGet();

            foreach (var entity in entities)
            {
                DrawEntity(entity);
            }

            RB.TintColorSet(previousTintColor);
        }

        public static void DrawEntity(Entity entity)
        {
            RB.TintColorSet(entity.color);
            ////old was using RB.SpriteSize(0).width and .height. changed C.MOVE_MULTIPLE
            RB.DrawSprite(entity.sprite, new Vector2i(entity.pos.x * C.MOVE_MULTIPLE, entity.pos.y * C.MOVE_MULTIPLE));
        }
    }
}
