using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using valoa_elias_tapani_kansalle.entities;


namespace valoa_elias_tapani_kansalle.collision
{
    public class CollisionSystem 
    {
        public static GameObject IsColliding(GameObject obj, GameObject[] objects)
        {
            foreach(var otherObj in objects)
            {
                if( obj != null && otherObj != null &&
                    otherObj.IsCollisionActive && obj.IsCollisionActive &&
                    otherObj != obj && obj.BoundingBox.Intersects(otherObj.BoundingBox) ) 
                {
                    return otherObj;
                }
            }
            return null;
        }
    }
}

/*
        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void LoadContent(ContentManager content)
        {

        }

    }
}
*/