using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public BaseEntitity()
        {

        }

        public void Update()
        {

        }

        public void Draw()
        {

        }



    }
}