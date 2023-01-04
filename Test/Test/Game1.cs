using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
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

        /*private TiledMap _TiledManoirEXT;
        private TiledMapRenderer _tiledManoirRenderer;*/


        //personnage
        private Vector2 _positionPerso;
        private AnimatedSprite _perso;
        public float _vitesse;
        private float _vitessePerso;
        private int _sensPersoX;
        private int _sensPersoY;
        private string _animation;

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
            Window.Title = "Nom de fenetre";

            //personnage
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _positionPerso = new Vector2(20, 340);
            _vitesse = 70;
            _vitessePerso = 70;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            backgroundSprite = Content.Load<Texture2D>("manoir");

            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("Personnage.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaSeconds * _vitessePerso; // Vitesse de déplacement du sprite

            _keyboardState = Keyboard.GetState();

            _sensPersoX = 0;
            _sensPersoY = 0;

            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                _sensPersoX = 1;
            }

            //si fleche gauche
            else if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                _sensPersoX = -1;
            }

            if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {
                _sensPersoY = -1;
            }

            else if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {
                _sensPersoY = 1;
            }

            _vitessePerso = _vitesse;

            if (_sensPersoX != 0 && _sensPersoY != 0)
                _vitessePerso = _vitesse / (float)Math.Sqrt(2);
            else
                _vitessePerso = _vitesse;

            if (_sensPersoX == 0 && _sensPersoY == 0)
                _perso.Play("idle");

            else if (_sensPersoX == 1 && _sensPersoY == 0)
                _perso.Play("walkEast");

            else if (_sensPersoX == -1 && _sensPersoY == 0)
                _perso.Play("walkWest");

            //Si cest vers le bas = → && ↓ || ← && ↓ || 0 && ↓
            else if (_sensPersoX == 1 && _sensPersoY == 1 || _sensPersoX == -1 && _sensPersoY == 1 || _sensPersoX == 0 && _sensPersoY == 1)
                _perso.Play("walkSouth");

            //Si cest vers le bas = → && ↑ || ← && ↑ || 0 && ↑
            else if (_sensPersoX == 1 && _sensPersoY == -1 || _sensPersoX == -1 && _sensPersoY == -1 || _sensPersoX == 0 && _sensPersoY == -1)
                _perso.Play("walkNorth");

            _perso.Update(deltaSeconds);
            _positionPerso.X += _sensPersoX * _vitessePerso * deltaSeconds;
            _positionPerso.Y += _sensPersoY * _vitessePerso * deltaSeconds;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);

            _spriteBatch.Draw(_perso, _positionPerso);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}