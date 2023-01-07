using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class MapExt : GameScreen
    {
      
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Game1 _game;
        private Texture2D _perso;
        private Vector2 _positionPerso;
        private Perso personnage;
        private string _animation;
        private float _vitessePerso;

        private TiledMapTileLayer mapLayer;
        private KeyboardState _keyboardState;

        public MapExt(Game1 game) : base(game)
        {
            this._game = game;
            this.personnage = new Perso(game);
        }

        public override void LoadContent()
        {
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _tiledMap = Content.Load<TiledMap>("MapExt2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
        }

        public override void Update(GameTime gameTime)
        {
            personnage.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            _tiledMapRenderer.Update(gameTime);           

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaTime * _vitessePerso; // Vitesse de déplacement du sprite

            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                ushort tx = (ushort)(_positionPerso.X / _tiledMap.TileWidth);
                ushort ty = (ushort)(_positionPerso.Y / _tiledMap.TileHeight );
                if (IsCollision(tx, ty))
                    _positionPerso.Y += walkSpeed;
            }
        }
        private bool IsCollision(ushort x, ushort y)
        {
            Console.WriteLine(mapLayer.GetTile(x, y).GlobalIdentifier);          
            //// définition de tile qui peut être null (?)
            TiledMapTile? tile;
            if (mapLayer.TryGetTile(x, y, out tile))
                return false;
            if (!tile.Value.IsBlank)
                return true;
            else
                return false;
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _tiledMapRenderer.Draw();
            personnage.Draw();
        }
    }
}

