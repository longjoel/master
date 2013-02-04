using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Demo3
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameInstance : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<VertexPositionColor> _mesh;

        public GameInstance()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _mesh = new List<VertexPositionColor>()
            {
                new VertexPositionColor( new Vector3(-1,-1,0),Color.Red),
                  new VertexPositionColor( new Vector3(1,1,0),Color.Red),
                new VertexPositionColor( new Vector3(-1,1,0),Color.Red),
              
                
            };
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //var viewMatrix = Matrix.CreateLookAt(
            //    new Vector3(0.0f, 0.0f, 1.0f),
            //    Vector3.Zero,
            //    Vector3.Up
            //    );

            //var projectionMatrix = Matrix.CreateOrthographicOffCenter(
            //    0,
            //    (float)GraphicsDevice.Viewport.Width,
            //    (float)GraphicsDevice.Viewport.Height,
            //    0,
            //    1.0f, 1000.0f);


            RasterizerState rasterizerState1 = new RasterizerState();
            rasterizerState1.CullMode = CullMode.None;
            graphics.GraphicsDevice.RasterizerState = rasterizerState1;

            var be = new BasicEffect(GraphicsDevice);

            
            foreach (var p in be.CurrentTechnique.Passes)
            {
                p.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, _mesh.ToArray(), 0, _mesh.Count /3);
            }
            base.Draw(gameTime);
        }
    }
}
