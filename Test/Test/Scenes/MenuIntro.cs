using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class MenuIntro : GameScreen
    {
        private Texture2D _manoir;
        private SpriteBatch _spriteBatch;

        private new Game1 Game => (Game1)base.Game;

        public MenuIntro(Game1 game) : base(game)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Draw(_manoir, new Vector2(0,0), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Game.LoadMapExt();
            }
        }
        public override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _manoir = Content.Load<Texture2D>("manoir");
        }
    }
}
        