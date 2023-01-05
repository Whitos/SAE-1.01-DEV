using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
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
        private Vector2 _positionPerso;
        private AnimatedSprite _perso;
        public float _vitesse;
        private float _vitessePerso;
        private int _sensPersoX;
        private int _sensPersoY;

        private KeyboardState _keyboardState;

        public Vector2 Position { get => _positionPerso; set => _positionPerso = value; }

        public void LoadContent(Game game1)
        {
            SpriteSheet mike = game1.Content.Load<SpriteSheet>("Personnage.sf", new JsonContentLoader());
            _perso = new AnimatedSprite(mike);
        }

        public void Update(float deltaTime)
        {
            _keyboardState = Keyboard.GetState();
            _sensPersoX = 0;
            _sensPersoY = 0;

            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                _sensPersoX = 1;
            }

            //si fleche gauche
            else if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                _sensPersoX = -1;
            }

            if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {
                _sensPersoY = -1;
            }

            else if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {
                _sensPersoY = 1;
            }

            _vitessePerso = _vitesse;

            if (_sensPersoX != 0 && _sensPersoY != 0)
                _vitessePerso = _vitesse / (float)Math.Sqrt(2);
            else
                _vitessePerso = _vitesse;

            if (_sensPersoX == 0 && _sensPersoY == 0)
                _perso.Play("idle");

            else if (_sensPersoX == 1 && _sensPersoY == 0)
                _perso.Play("walkEast");

            else if (_sensPersoX == -1 && _sensPersoY == 0)
                _perso.Play("walkWest");

            //Si cest vers le bas = → && ↓ || ← && ↓ || 0 && ↓
            else if (_sensPersoX == 1 && _sensPersoY == 1 || _sensPersoX == -1 && _sensPersoY == 1 || _sensPersoX == 0 && _sensPersoY == 1)
                _perso.Play("walkSouth");

            //Si cest vers le bas = → && ↑ || ← && ↑ || 0 && ↑
            else if (_sensPersoX == 1 && _sensPersoY == -1 || _sensPersoX == -1 && _sensPersoY == -1 || _sensPersoX == 0 && _sensPersoY == -1)
                _perso.Play("walkNorth");

            _perso.Update(deltaTime);
            _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;
            _positionPerso.Y += _sensPersoY * _vitessePerso * deltaTime;
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Begin();
            _spriteBatch.Draw(_perso, _positionPerso);
            _spriteBatch.End();
        }
    }
}
