using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace valoa_elias_tapani_kansalle.collision
{
    class CollisionShapeRectangle
    {
        private Rectangle shape;
        public Rectangle Shape
        {
            get { return shape; }
            set { shape = value; }
        }

        public CollisionShapeRectangle(int x, int y, int width, int height)
        {
            Shape = new Rectangle(x, y, width, height);
        }

        public CollisionShapeRectangle(Rectangle rect)
        {
            Shape = rect;
        }

        public bool IsColliding(Rectangle rect)
        {
            return shape.Contains(rect);
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