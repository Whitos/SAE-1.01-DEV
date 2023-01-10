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
      
        public static TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Game1 _game;
        private Perso personnage;

        private TiledMapTileLayer _mapLayer;
        private KeyboardState _keyboardState;

        public MapExt(Game1 game) : base(game)
        {
            this._game = game;
            this.personnage = new Perso(game);              
        }

        public override void LoadContent()
        {
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
        }
        
        public override void Update(GameTime gameTime)
        {
            personnage.Update((float)gameTime.ElapsedGameTime.TotalSeconds); 
            _tiledMapRenderer.Update(gameTime);                 
        }
        
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _tiledMapRenderer.Draw();
            personnage.Draw();
        }
    }
}

