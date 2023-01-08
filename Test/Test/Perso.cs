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

        public Perso(Game1 game1)
        {
            this.game1 = game1;
            Initialize();
            LoadContent();
        }

        public Perso(TiledMapTileLayer _mapLayer)
        {
            this._mapLayer = _mapLayer;
        }

        public Perso(TiledMap _tiledMap)
        {
            this._tiledMap = _tiledMap;
        }

        public void Initialize()
        {
            _positionPerso = new Vector2(150, 340);
            _sensPerso = new Vector2(0, 0);
            _vitesse = 70;
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
            ushort tx = (ushort)(_positionPerso.X / MapExt._tiledMap.TileWidth);
            ushort ty = (ushort)(_positionPerso.Y / MapExt._tiledMap.TileHeight);
            _keyboardState = Keyboard.GetState();
            _sensPerso = Vector2.Zero;
            float walkSpeed = deltaTime * _vitessePerso; // Vitesse de déplacement du sprite      

            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                _sensPerso.X = 1;
                animation = "walkEast";
            }
            //si fleche gauche
            else if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                _sensPerso.X = -1;
                animation = "walkWest";
            }
            if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {
                _sensPerso.Y = -1;
                animation = "walkNorth";
                if (_keyboardState.IsKeyDown(Keys.Down))
                {                    

                    Console.WriteLine(_mapLayer.GetTile(tx, ty).GlobalIdentifier);
                    if (!IsCollision(tx, ty))
                    {
                        _sensPerso.Y -= walkSpeed;
                    }
                }
            }
            else if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {
                _sensPerso.Y = 1;
                animation = "walkSouth";

                if (_keyboardState.IsKeyDown(Keys.Up))
                {
                    if (!IsCollision(tx, ty))
                    {                        
                        _sensPerso.Y += walkSpeed;
                    }
                }
            }
            _vitessePerso = _vitesse;
            if (_sensPerso != Vector2.Zero)
                _sensPerso.Normalize();

            _positionPerso += _sensPerso * _vitessePerso * deltaTime;
            _perso.Play(animation);
            _perso.Update(deltaTime);


        }
        public bool IsCollision(ushort x, ushort y)
        {
            //// définition de tile qui peut être null (?)
            TiledMapTileLayer _mapLayer = MapExt._tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
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
