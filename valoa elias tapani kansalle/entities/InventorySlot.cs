using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace valoa_elias_tapani_kansalle.entities
{
    internal class InventorySlot : GuiItem
    {
        private Texture2D _inventorySlot;
        public Texture2D _InventorySlot
        {
            get { return _inventorySlot; }
            set { _inventorySlot = value; }
        }
        private Texture2D _screwdriver;
        public Texture2D _Screwdriver
        {
            get { return _screwdriver; }
            set { _screwdriver = value; }
        }
        private Texture2D _key;
        public Texture2D _Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public InventorySlot(int x, int y)
        {
            Position = new Vector2(x, y);
        }

        public void LoadContent(ContentManager content)
        {
            _inventorySlot = content.Load<Texture2D>("sprites/inventory_slot");
            _screwdriver = content.Load<Texture2D>("sprites/inventory_screwdriver");
            _key = content.Load<Texture2D>("sprites/inventory_key");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_inventorySlot,
                                Position,
                                null,
                                Color.White,
                                0,
                                Vector2.Zero,
                                0.5f,
                                SpriteEffects.None,
                                EntityUtil.GetEntityLayer(EntityLayer.ENTITY_LAYER_UI));

        }

        public void DrawItem(SpriteBatch spriteBatch, string item)
        {
            if (item == "screwdriver")
            {
                spriteBatch.Draw(_screwdriver,
                                    Position,
                                    null,
                                    Color.White,
                                    0,
                                    Vector2.Zero,
                                    0.5f,
                                    SpriteEffects.None,
                                    EntityUtil.GetEntityLayer(EntityLayer.ENTITY_LAYER_UI));
            } else if (item == "key")
            {
                spriteBatch.Draw(_key,
                                    Position,
                                    null,
                                    Color.White,
                                    0,
                                    Vector2.Zero,
                                    0.5f,
                                    SpriteEffects.None,
                                    EntityUtil.GetEntityLayer(EntityLayer.ENTITY_LAYER_UI));
            }
        }
    }
}
