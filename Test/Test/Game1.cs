using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;

namespace Test
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D backgroundSprite;

        public SpriteBatch SpriteBatch { get; private set; }
        private ScreenManager _screenManager;

        //MapExt
        //private TiledMap _tiledMap;
        //private TiledMapRenderer _tiledMapRenderer;

        //clavier 
        private KeyboardState _keyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "EscapeGame2D";

            _screenManager = new ScreenManager();

            //personnage
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            base.Initialize();
        }

        public void LoadScreen(GameScreen screen)
        {
            _screenManager.LoadScreen(screen, new FadeTransition(GraphicsDevice, Color.Black, .5f));

        }

        //public void LoadMapExt()
        //{
        //    LoadScreen(new MapExt(this));
        //}

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //fond menu
            backgroundSprite = Content.Load<Texture2D>("manoir");

            //MAP EXT
            //LoadMapExt();
            //_tiledMap = Content.Load<TiledMap>("mapGenerale");
            //_tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);

            
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here      
            _screenManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            // TODO: Add your drawing code here           
            _screenManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}