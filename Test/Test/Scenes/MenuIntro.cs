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
        // pour récupérer une référence à l’objet game pour avoir accès à tout ce qui est 
        // défini dans Game1
        public static Game1 _game;

        // texture du menu avec 3 boutons
        private Texture2D _textBoutons;

        // contient les rectangles : position et taille des 3 boutons présents dans la texture 
        private Rectangle[] lesBoutons;


        private Texture2D _manoir;
        public static Song _musique;
        public Game1 _game;
        private new Game1 Game => (Game1)base.Game;

        public MenuIntro(Game1 game) : base(game)
        {
            lesBoutons = new Rectangle[3];
            lesBoutons[0] = new Rectangle(350, 100, 50, 50);
            lesBoutons[1] = new Rectangle(350, 300, 100, 50);
            lesBoutons[2] = new Rectangle(350, 500, 50, 50);
        }

        public override void LoadContent()
        {
            _manoir = Content.Load<Texture2D>("ManoirMenu");
            _textBoutons = Content.Load<Texture2D>("ManoirMenu");
            _musique = Content.Load<Song>("creepy");
            MediaPlayer.Play(_musique);
        }

        public override void Initialize()
        {
            //_game.TailleFenetre(1000, 1000);
            MediaPlayer.IsRepeating = true;
        }

        public override void Update(GameTime gameTime)
        {
            {            
               // Game.LoadMapExt();
                MediaPlayer.Stop();
            }
        }
        public override void Draw(GameTime gameTime)
        {
            Game.spriteBatch.Begin();
            Game.spriteBatch.Draw(_manoir, new Vector2(0,0), Color.White);
            Game.spriteBatch.End();
        }
    }
}
        