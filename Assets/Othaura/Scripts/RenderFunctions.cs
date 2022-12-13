namespace Othaura { 
    
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class RenderFunctions
    {
        public static void RenderAll(GameMap map, List<Entity> entities)
        {
            RB.DrawMapLayer(C.LAYER_TERRAIN);

            var previousTintColor = RB.TintColorGet();

            foreach (var entity in entities)
            {
                DrawEntity(entity);
            }

            RB.TintColorSet(previousTintColor);

            // Draw visibility layer on top, covering tiles as needed
            RB.DrawMapLayer(C.LAYER_VISIBILITY);

            // Draw filled rectangles on all sides of the map to cover up areas with no tiles in the same color
            // as tiles that are currently not visible
            RB.DrawRectFill(
                new Rect2i(
                    -RB.DisplaySize.width,
                    0,
                    RB.DisplaySize.width,
                    map.size.height * C.PIXEL_SIZE),                    
                    C.COLOR_INVISIBLE);

            RB.DrawRectFill(
                new Rect2i(
                    map.size.width * C.PIXEL_SIZE,
                    0,
                    RB.DisplaySize.width,
                    map.size.height * C.PIXEL_SIZE),
                C.COLOR_INVISIBLE);

            RB.DrawRectFill(
                new Rect2i(
                    -RB.DisplaySize.width,
                    -RB.DisplaySize.height,
                    map.size.width * C.PIXEL_SIZE + RB.DisplaySize.width * 2,
                    RB.DisplaySize.height),
                C.COLOR_INVISIBLE);

            RB.DrawRectFill(
                new Rect2i(
                    -RB.DisplaySize.width,
                    map.size.height * C.PIXEL_SIZE,
                    map.size.width * C.PIXEL_SIZE + RB.DisplaySize.width * 2,
                    RB.DisplaySize.height),
                C.COLOR_INVISIBLE);

        }

        public static void DrawEntity(Entity entity) {

            if (FOVFunctions.IsInFOV(entity.pos)) {
                
                RB.TintColorSet(entity.color);
                ////old was using RB.SpriteSize(0).width and .height. changed C.PIXEL_SIZE
                RB.DrawSprite(entity.sprite, new Vector2i(entity.pos.x * C.PIXEL_SIZE, entity.pos.y * C.PIXEL_SIZE));
            }
        }
    }
}
