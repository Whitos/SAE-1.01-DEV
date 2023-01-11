using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;
using static Test.Game1;

namespace Test
{
    internal class Options : GameScreen
    {
        private Game1 _game;
        public static Rectangle[] lesBoutons;
        public static Texture2D _textBoutons;
        
        public Options(Game1 game) : base(game)
        {
            _game = game;
            lesBoutons = new Rectangle[1];
            lesBoutons[0] = new Rectangle(10, 10, 10, 10);
        }

        public override void Initialize()
        {
            //Game1._game.TailleFenetre(400, 400);
        }

        public override void LoadContent()
        {
            _textBoutons = Content.Load<Texture2D>("MenuOptions");
        }

        public override void Update(GameTime gameTime)
        {
            MouseState _mouseState = Mouse.GetState();
            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                for (int i = 0; i < lesBoutons.Length; i++)
                {
                    // si le clic correspond à un des 3 boutons
                    if (lesBoutons[i].Contains(Mouse.GetState().X, Mouse.GetState().Y))
                    {
                        // on change l'état défini dans Game1 en fonction du bouton cliqué
                        _game.Etat = Game1.Etats.Menu;

                        break;
                    }
                }
            }
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Game1._game.SpriteBatch.Begin();
            Game1._game.SpriteBatch.Draw(_textBoutons, new Vector2(0, 0), Color.White);
            Game1._game.SpriteBatch.End();
        }
    }
}
