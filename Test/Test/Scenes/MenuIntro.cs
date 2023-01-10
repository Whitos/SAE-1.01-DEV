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
        public Game1 _game;
        private new Game1 Game => (Game1)base.Game;

        public MenuIntro(Game1 game) : base(game)
        {
            _game = game;
            Options.lesBoutons = new Rectangle[3];
            Options.lesBoutons[0] = new Rectangle(350, 100, 50, 50);
            Options.lesBoutons[1] = new Rectangle(350, 300, 100, 50);
            Options.lesBoutons[2] = new Rectangle(350, 500, 50, 50);
        }
        public override void Initialize()
        {
            MediaPlayer.IsRepeating = true;
            _game.TailleFenetre(1000, 1000);
        }

        public override void LoadContent()
        {
            _manoir = Content.Load<Texture2D>("ManoirMenu");
            _musique = Content.Load<Song>("creepy");
            MediaPlayer.Play(_musique);
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
            Game1._spriteBatch.Begin();
            Game1._spriteBatch.Draw(_manoir, new Vector2(0,0), Color.White);
            Game1._spriteBatch.End();
        }
    }
}
        