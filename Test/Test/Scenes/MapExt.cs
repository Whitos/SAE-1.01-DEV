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
    internal class MapExt : GameScreen
    {       
        private new Game1 Game => (Game1)base.Game;

        public MapExt(Game1 game) : base(game)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Game.LoadMenu();
            }
        }
    }
}

