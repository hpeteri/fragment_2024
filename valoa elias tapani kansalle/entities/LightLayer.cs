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
            float facing)
        {
            var oldTarget = graphicsDevice.GetRenderTargets();

            graphicsDevice.SetRenderTarget(_renderTarget);
            graphicsDevice.Clear(new Color(0, 0, 0, 220));

            graphicsDevice.BlendState = BlendState.Opaque;

            Vector3 origin = new Vector3(
                (position.X - graphicsDevice.Viewport.Width * 0.5f) / (float)graphicsDevice.Viewport.Width * 2.0f,
                (position.Y - graphicsDevice.Viewport.Height * 0.5f) / (float)graphicsDevice.Viewport.Height * 2.0f,
                0.0f);


            Vector2 toMousePos =
                Vector2.Normalize(new Vector2(Mouse.GetState().X, Mouse.GetState().Y) - position);

            Vector2 toSide = new Vector2(toMousePos.Y, -toMousePos.X);
            if (toSide.X == 0 && toSide.Y == 0)
            {
                toSide.X = 1;
            }

            
            Vector3 side0 = new Vector3(
                (position.X + toMousePos.X * 200 + toSide.X * 64 - graphicsDevice.Viewport.Width * 0.5f) / (float)graphicsDevice.Viewport.Width * 2.0f,
                (position.Y + toMousePos.Y * 200 + toSide.Y * 64 - graphicsDevice.Viewport.Height * 0.5f) / (float)graphicsDevice.Viewport.Height * 2.0f,
                0.0f);
            
            Vector3 side1 = new Vector3(
                (position.X + toMousePos.X * 200 - toSide.X * 64 - graphicsDevice.Viewport.Width * 0.5f) / (float)graphicsDevice.Viewport.Width * 2.0f,
                (position.Y + toMousePos.Y * 200 - toSide.Y * 64 - graphicsDevice.Viewport.Height * 0.5f) / (float)graphicsDevice.Viewport.Height * 2.0f,
                0.0f);
                        
            var vertices = new VertexPositionColor[3]
            {
                new VertexPositionColor(origin, new Color(0,0,0,0)),    // Top vertex (Red)
                new VertexPositionColor(side0, new Color(0,0,0,0)), // Bottom left vertex (Green)
                new VertexPositionColor(side1, new Color(0,0,0,0))   // Bottom right vertex (Blue)
            };

            // Use BasicEffect to set up simple rendering state (could be omitted if you want more control)
            BasicEffect basicEffect = new BasicEffect(graphicsDevice)
            {
                VertexColorEnabled = true, // Enable vertex color
                World = Matrix.Identity,
                View = Matrix.Identity,
                Projection = Matrix.CreateOrthographicOffCenter(-1, 1, 1, -1, 0, 1) // Ortho projection for 2D
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
