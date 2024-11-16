using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace valoa_elias_tapani_kansalle.entities
{
    public class Item : BaseEntitity
    {
        public Item( Vector2 position )
        {
            Speed = 0;
            Position = position;
        }

        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime); 
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        

    }

}