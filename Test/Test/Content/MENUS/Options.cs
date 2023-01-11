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

namespace Test
{
    internal class Options : GameScreen
    {
        private Game1 _myGame;

        public SpriteBatch _spriteBatch;
        private Rectangle[] lesBoutons;
        private new Game1 game;
        Texture2D _textBoutons;

        public Options(Game1 game) : base(game)
        {
            _myGame = game;
            lesBoutons = new Rectangle[1];
            lesBoutons[0] = new Rectangle(10, 10, 10, 10);
        }

        public override void Initialize()
        {
            //_myGame.TailleFenetre(400, 400);
            base.Initialize();
        }
        public override void LoadContent()
        {
            _textBoutons = Content.Load<Texture2D>("OptionsMenu");
            base.LoadContent();
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
                        _myGame.Etat = Game1.Etats.Menu;

                        break;
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_textBoutons, new Vector2(0, 0), Color.White);
            _myGame.SpriteBatch.End();
        }
    }
}