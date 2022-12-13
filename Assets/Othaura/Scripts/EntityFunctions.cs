namespace Othaura {

    using UnityEngine;
    using System.Collections.Generic;

    public static class EntityFunctions {

        public static Entity GetBlockingEntityAtPos(List<Entity> entities, Vector2i pos) {

            for (int i = 0; i < entities.Count; i++) {

                var entity = entities[i];

                if (entity.blocks && entity.pos == pos) {

                    return entities[i];
                }
            }
            return null;
        }
    }
}

