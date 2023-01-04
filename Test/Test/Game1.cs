using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using System;

namespace Test
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D backgroundSprite;

        private TiledMap _TiledManoirEXT;

        //personnage
        private Vector2 _positionPerso;
        private AnimatedSprite _perso;
        private TiledMapTileLayer mapLayer;
        private int vitesse;
        String animation = "idle";

        private bool IsCollision(ushort x, ushort y)
        {
            // définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile) == false)
                return false;
            if (!tile.Value.IsBlank)
                return true;
            return false;
        }
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
            vitesse = 50;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            backgroundSprite = Content.Load<Texture2D>("manoir");
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("personnage.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(spriteSheet);
            mapLayer = _TiledManoirEXT.GetLayer<TiledMapTileLayer>("obstacles");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaSeconds * vitesse; // Vitesse de déplacement du sprite
            KeyboardState keyboardState = Keyboard.GetState();


            animation = "idle";

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                ushort tx = (ushort)(_positionPerso.X / _TiledManoirEXT.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _TiledManoirEXT.TileHeight - 1);
                animation = "walkNorth";
                if (!IsCollision(tx, ty))
                    _positionPerso.Y -= walkSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                ushort tx = (ushort)(_positionPerso.X / _TiledManoirEXT.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _TiledManoirEXT.TileHeight + 1);
                animation = "walkSouth";
                if (!IsCollision(tx, ty))
                    _positionPerso.Y += walkSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                ushort tx = (ushort)(_positionPerso.X / _TiledManoirEXT.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _TiledManoirEXT.TileHeight + 1);
                animation = "walkWest";
                if (!IsCollision(tx, ty))
                    _positionPerso.X -= walkSpeed;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                ushort tx = (ushort)(_positionPerso.X / _TiledManoirEXT.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _TiledManoirEXT.TileHeight + 1);
                animation = "walkEast";
                if (!IsCollision(tx, ty))
                    _positionPerso.X += walkSpeed;
            }

            // TODO: Add your update logic here
            _TiledManoirEXT.Update(gameTime);
            _perso.Play(animation); // une des animations définies dans « persoAnimation.sf »
            _perso.Update(deltaSeconds); // time écoulé

            // TODO: Add your update logic here
            _TiledManoirEXT.Update(gameTime);// une des animations définies dans « persoAnimation.sf »
            _perso.Update(deltaSeconds); // time écoulé
            base.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            _spriteBatch.End();

            //personnage
            _spriteBatch.Begin();
            _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}