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
using Project1;
using System;

namespace Test
{
    public class Game1 : Game
    {
        //private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        private ScreenManager _screenManager;

        //clavier 
        //private KeyboardState _keyboardState;

        // on définit les différents états possibles du jeu ( à compléter) 
        public enum Etats { Play, Options, Quit, Menu };

        // on définit un champ pour stocker l'état en cours du jeu
        private Etats etat;

        // on définit  2 écrans ( à compléter )
        private MenuIntro _screenMenu;
        private Options _screenOption;
        private MapExt _screenPlay;

        public GraphicsDeviceManager graphics { get; private set; }
        //internal static object _game;

        public SpriteBatch SpriteBatch
        {
            get
            {
                return this._spriteBatch;
            }

            set
            {
                this._spriteBatch = value;
            }
        }
        public Etats Etat
        {
            get
            {
                return this.etat;
            }
            set
            {
                this.etat = value;
            }
        }
        public Game1()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            graphics = new GraphicsDeviceManager(this);

            // Par défaut, le 1er état flèche l'écran de menu
            Etat = Etats.Play;

            // on charge les écrans 
            _screenMenu = new MenuIntro(this);
            _screenOption = new Options(this);
            _screenPlay = new MapExt(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "EscapeGame2D";
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _screenManager = new ScreenManager();
            /*_graphics.PreferredBackBufferWidth = 650;
            _graphics.PreferredBackBufferHeight = 400;
            _graphics.ApplyChanges();*/

            base.Initialize();
        }

        public void LoadScreen(GameScreen screen)
        {
            _screenManager.LoadScreen(screen, new FadeTransition(GraphicsDevice, Color.Black, .5f));
        }

        public void LoadMenu()
        {
            LoadScreen(new MapExt(this));
        }

        public void LoadMapExt()
        {
            LoadScreen(new MapExt(this));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // on charge l'écran de menu par défaut 
            _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));

            // TODO: use this.Content to load your game content here
            LoadMenu();
            LoadMapExt();
            MapExt._tiledMap = Content.Load<TiledMap>("MapExt2");
            TiledMapTileLayer mapLayer = MapExt._tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // On teste le clic de souris et l'état pour savoir quelle action faire 
            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                // Attention, l'état a été mis à jour directement par l'écran en question
                if (this.Etat == Etats.Quit)
                    Exit();

                else if (this.Etat == Etats.Play)
                    _screenManager.LoadScreen(_screenPlay, new FadeTransition(GraphicsDevice, Color.Black));

                else if (this.Etat == Etats.Options)
                    _screenManager.LoadScreen(_screenOption, new FadeTransition(GraphicsDevice, Color.Black));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                if (this.Etat == Etats.Menu)
                    _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
            }

            // TODO: Add your update logic here      
            _screenManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here           
            _screenManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}