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
        public Vector2 _positionPerso;
        public AnimatedSprite _perso;
        public float _vitesse;
        private float _vitessePerso;
        private int _sensPersoX;
        private int _sensPersoY;

        private KeyboardState _keyboardState;
        private Game1 game1;

        public Perso(Game1 game1) 
        {
            this.game1 = game1;
            Initialize();
            LoadContent();
        }

        public void Initialize()
        {
            _positionPerso = new Vector2(150, 340);
            _vitesse = 200;
            _vitessePerso = 70;
        }

        public void LoadContent()
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

            _positionPerso.X += _sensPersoX * _vitessePerso * deltaTime;
            _positionPerso.Y += _sensPersoY * _vitessePerso * deltaTime;
            _perso.Update(deltaTime);
        }
        public void Draw()
        {
            game1._spriteBatch.Begin();
            game1._spriteBatch.Draw(_perso, _positionPerso);
            game1._spriteBatch.End();
        }
    }
}
