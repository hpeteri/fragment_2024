using Microsoft.Xna.Framework;

namespace valoa_elias_tapani_kansalle
{
    public class MenuItem
    {
        public string text;
        public float scale;

        public MenuItem(
            string text,
            float scale)
        {
            this.text = text;
            this.scale = scale;
        }

        public void UpdateHovered(
            GameTime gameTime,
            bool     isHovered)
        {


            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float baseSpeed_inc = 6.0f;
            float baseSpeed_dec = 12.0f;
            if (isHovered) {
                float speed =  dt * baseSpeed_inc;
                this.scale += speed;

                if (this.scale > 2.0f)
                {
                    this.scale = 2.0f;
                }
                
            } else {
                float speed = dt * baseSpeed_dec;
                this.scale -= speed;

                if (this.scale < 1.0f)
                {
                    this.scale = 1.0f;
                }
                
            }
        }
    }    
}
