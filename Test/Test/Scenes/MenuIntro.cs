using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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
        public static Song _musique;

        private new Game1 Game => (Game1)base.Game;

        public MenuIntro(Game1 game) : base(game)
        {}

        public override void LoadContent()
        {
            _manoir = Content.Load<Texture2D>("manoir");
            _musique = Content.Load<Song>("creepy");
            MediaPlayer.Play(_musique);
        }

        public override void Initialize()
        {
            MediaPlayer.IsRepeating = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Game.LoadMapExt();
                MediaPlayer.Stop();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            Game._spriteBatch.Begin();
            Game._spriteBatch.Draw(_manoir, new Vector2(0,0), Color.White);
            Game._spriteBatch.End();
        }
    }
}
        