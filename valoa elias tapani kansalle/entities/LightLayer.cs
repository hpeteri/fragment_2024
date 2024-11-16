using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace valoa_elias_tapani_kansalle.entities
{
    public class LightLayer
    {
        RenderTarget2D _renderTarget;

        public LightLayer(
            GraphicsDevice graphicsDevice)
        {
            _renderTarget = new RenderTarget2D(graphicsDevice,
                                              graphicsDevice.PresentationParameters.BackBufferWidth,
                                              graphicsDevice.PresentationParameters.BackBufferHeight,
                                              false,
                                              SurfaceFormat.Rgba64,
                                              DepthFormat.None);        
        }

        public void UpdateTorchBeam(
            GraphicsDevice graphicsDevice,
            Vector2 position,
            Vector2 direction)
        {
            var oldTarget = graphicsDevice.GetRenderTargets();

            graphicsDevice.SetRenderTarget(_renderTarget);
            graphicsDevice.Clear(new Color(0, 0, 0, 220));

            graphicsDevice.BlendState = BlendState.Opaque;
 
            var vertices = new VertexPositionColor[3]
            {
                new VertexPositionColor(new Vector3(0, 1, 0f), new Color(0,0,0,0)),    // Top vertex (Red)
                new VertexPositionColor(new Vector3(-1, -1, 0f), new Color(0,0,0,0)), // Bottom left vertex (Green)
                new VertexPositionColor(new Vector3(1, -1, 0f), new Color(0,0,0,0))   // Bottom right vertex (Blue)
            };

            // Use BasicEffect to set up simple rendering state (could be omitted if you want more control)
            BasicEffect basicEffect = new BasicEffect(graphicsDevice)
            {
                VertexColorEnabled = true, // Enable vertex color
                World = Matrix.Identity,
                View = Matrix.Identity,
                Projection = Matrix.CreateOrthographicOffCenter(-1, 1, -1, 1, 0, 1) // Ortho projection for 2D
            };
            
            VertexBuffer vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);  
            vertexBuffer.SetData<VertexPositionColor>(vertices);

            graphicsDevice.RasterizerState = RasterizerState.CullNone;
            graphicsDevice.SetVertexBuffer(vertexBuffer);

            // Set the effect parameters and apply them
            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, 1); // Draw the triangle
            }

            graphicsDevice.SetRenderTargets(oldTarget);
            graphicsDevice.BlendState = BlendState.AlphaBlend;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            
            spriteBatch.Draw((Texture2D)_renderTarget,
                             new Vector2(_renderTarget.Width * 0.5f,
                                         _renderTarget.Height * 0.5f),
                             null,
                             Color.White,
                             0,
                             new Vector2(_renderTarget.Width * 0.5f,
                                         _renderTarget.Height * 0.5f),
                             Vector2.One,
                             SpriteEffects.None,
                             EntityUtil.GetEntityLayer(EntityLayer.ENTITY_LAYER_FOREGROUND));
        }
    }
}
