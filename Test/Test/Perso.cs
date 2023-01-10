using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Perso
    {
        //personnage
        private const int TAILLE_SPRITE = 32;
        public Vector2 _positionPerso;
        public AnimatedSprite _perso;
        public float _vitesse;
        private float _vitessePerso;
        private Vector2 _sensPerso;
        public string animation;

        private TiledMap _tiledMap;
        public TiledMapTileLayer _mapLayer;

        private KeyboardState _keyboardState;
        private Game1 game1;
        private MapExt _map;

        public Perso(Game1 game1)
        {
            this.game1 = game1;
            Initialize();
            LoadContent();
        }


        public Perso(TiledMap _tiledMap)
        {
            this._tiledMap = _tiledMap;
        }
        public Perso(TiledMapTileLayer _mapLayer)
        {
            this._mapLayer = _mapLayer;            
        }

        public void Initialize()
        {
            _positionPerso = new Vector2(150, 340);
            _sensPerso = new Vector2(0, 0);
            _vitesse = 100;
            _vitessePerso = 70;
        }

        public void LoadContent()
        {
            Perso perso = new Perso(_mapLayer);
            SpriteSheet mike = game1.Content.Load<SpriteSheet>("Personnage.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(mike);
        }

        public void Update(float deltaTime)
        {
            String animation = "idle";
            _keyboardState = Keyboard.GetState();
            _sensPerso = Vector2.Zero;
            float walkSpeed = deltaTime * _vitessePerso; // Vitesse de déplacement du sprite      

            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                _sensPerso.X = 0;
                ushort tx = (ushort)(_positionPerso.X / MapExt._tiledMap.TileWidth + 1);
                ushort ty = (ushort)(_positionPerso.Y / MapExt._tiledMap.TileHeight);
                animation = "walkEast";
                if (!IsCollision(tx, ty,_map))
                {
                    _sensPerso.X -= 1;
                }

            }
            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                _sensPerso.X = 0;
                ushort tx = (ushort)(_positionPerso.X / MapExt._tiledMap.TileWidth - 1);
                ushort ty = (ushort)(_positionPerso.Y / MapExt._tiledMap.TileHeight);
                animation = "walkWest";
                if (!IsCollision(tx, ty,_map))
                {
                    _sensPerso.X += 1;
                }
            }

            if (_keyboardState.IsKeyDown(Keys.Up))
            {
                _sensPerso.Y = 0;
                ushort tx = (ushort)(_positionPerso.X / MapExt._tiledMap.TileWidth );
                ushort ty = (ushort)(_positionPerso.Y / MapExt._tiledMap.TileHeight + 1);
                animation = "walkNorth";
                if (!IsCollision(tx, ty,_map))
                {
                    _sensPerso.Y += 1;
                }
            }
            if (_keyboardState.IsKeyDown(Keys.Down))
            {
                _sensPerso.Y = 0;
                ushort tx = (ushort)(_positionPerso.X / MapExt._tiledMap.TileWidth);
                ushort ty = (ushort)((_positionPerso.Y + TAILLE_SPRITE / 2) / MapExt._tiledMap.TileHeight);
                animation = "walkSouth";
                if (!!IsCollision(tx, ty,_map))
                {
                    _sensPerso.Y += 1;
                }
            }
            _vitessePerso = _vitesse;
            if (_sensPerso != Vector2.Zero)
                _sensPerso.Normalize();

            _perso.Update(deltaTime);
            _perso.Play(animation);
            _positionPerso.X -= _sensPerso.X * _vitessePerso * deltaTime;
            _positionPerso.Y -= _sensPerso.Y * _vitessePerso * deltaTime;
        }
        public bool IsCollision(ushort x, ushort y,MapExt _map)
        {
            //// définition de tile qui peut être null (?)
            TiledMapTileLayer _mapLayer = MapExt._tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
            Console.WriteLine(_mapLayer.GetTile(x, y).GlobalIdentifier);
            TiledMapTile? tile;
            if (_mapLayer.TryGetTile((ushort)x,(ushort) y, out tile))
                return false;
            if (!tile.Value.IsBlank)
                return true;
            else
                return false;
        }
        public void Draw()
        {
            game1._spriteBatch.Begin();
            game1._spriteBatch.Draw(_perso, _positionPerso);
            game1._spriteBatch.End();
        }
    }
}
