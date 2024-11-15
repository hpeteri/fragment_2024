using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


// NOTE: This is base class for controlling entities. Goal is to use this class 
// possible use cases could be Camera, Player, Enemy or Static object


namespace valoa_elias_tapani_kansalle.entities {
    public class BaseEntitity {

        private Vector2 position;
        private float speed;
        

        public Vector2 Position 
        {
            get { return position; }
            set { position = value; }
        }

        public float Speed 
        {
            get { return speed; }
            set { speed = value; }
        }

        public virtual void LoadContent(ContentManager content)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }



    }
}