using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        private Game1 _game1;
        private Perso personnage;

        private TiledMapTileLayer _mapLayer;


        public MapExt(Game1 game) : base(game)
        {
            _game1 = game;
            personnage = new Perso(game);              
        }

        public override void LoadContent()
        {
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _mapLayer = _tiledMap.GetLayer<TiledMapTileLayer>("obstacles");
            MenuIntro._musique = Content.Load<Song>("foret");
            MediaPlayer.Play(MenuIntro._musique);
        }
        public override void Initialize()
        {
            MediaPlayer.IsRepeating = true;
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

