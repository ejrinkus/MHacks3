using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MHacksTestOne
{
    class PlatformSprite : AbstractSprite
    {
        public PlatformSprite(Game cur_game, float scale, int x , int y)
        {
            game_obj = cur_game; //load in current game object
            size_type = 0; //indicate square
            scale_factor = scale;
            location.X = x;
            location.Y = y;
        }

        public void Update()
        {
            cur_width = texture.Width; //fill empty vars
            cur_height = texture.Height;
        }

        public void Draw()
        {
            Rectangle subsection = new Rectangle(0, 0, cur_width, cur_height);
            spriteBatch.Draw(texture, location, subsection, Color.White, 0.0f, new Vector2(0, 0), scale_factor, effect, 0.0f); //draw the player in the location specified
        }
    }
}
