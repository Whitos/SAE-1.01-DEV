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
using System.Text;
using System.Collections.Generic;

namespace Test
{
    public class Game1 : Game
    {
        public static Game1 _game;
        public GraphicsDeviceManager _graphics;
        public SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch { get;  set; }
        private readonly ScreenManager _screenManager;

        // on définit les différents états possibles du jeu ( à compléter) 
        public enum Etats { Play, Options, Quit, Menu };

        // on définit un champ pour stocker l'état en cours du jeu
        public static Etats etat;

        // on définit  2 écrans ( à compléter )
        private MenuIntro _screenMenu;
        private Options _screenOptions;
        private MapExt _screenJouer;

        public  Etats Etat { get;  set; }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            Etat = Etats.Play;

            //écran MenuIntro
            _screenMenu = new MenuIntro(this);
            //écran Options
            _screenOptions = new Options(this);
            //écran Jouer
            _screenJouer = new MapExt(this);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "EscapeGame2D";
            _graphics.PreferredBackBufferWidth = 700;
            _graphics.PreferredBackBufferHeight = 430;           
            _graphics.ApplyChanges();
            base.Initialize();
        }

        public void LoadScreen(GameScreen screen) //Chargement écran
        {
            _screenManager.LoadScreen(screen, new FadeTransition(GraphicsDevice, Color.Black, .5f));
        }

        public void LoadMenu() // chargement Menu
        {
            LoadScreen(new MenuIntro(this));
        }

        public void LoadMapExt() // chargement MapExt
        {
            LoadScreen(new MapExt(this));

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            LoadMenu();
            LoadMapExt();
            //MapExt._tiledMap = Content.Load<TiledMap>("MapExt2");
            //TiledMapTileLayer mapLayer = MapExt._tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
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
                    _screenManager.LoadScreen(_screenJouer, new FadeTransition(GraphicsDevice, Color.Black));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Back))
            {
                if (this.Etat == Etats.Options)
                    _screenManager.LoadScreen(_screenOptions, new FadeTransition(GraphicsDevice, Color.Black));
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

        public void TailleFenetre(int w, int h)
        {
            _graphics.PreferredBackBufferHeight = h;
            _graphics.PreferredBackBufferWidth = w;
            _graphics.ApplyChanges();
        }
    }
}