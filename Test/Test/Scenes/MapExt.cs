using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using System;
using System.Collections.Generic;
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
            _tiledMap = Content.Load<TiledMap>("MapExt2");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _tiledMapRenderer.Draw();
            personnage.Draw();
        }
        public override void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);           
            personnage.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float walkSpeed = deltaTime * _vitessePerso; // Vitesse de déplacement du sprite

        }                  
    }
}

