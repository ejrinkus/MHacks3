using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MHacksTestOne
{
    class AbstractSprite : Game
    {
        public Vector2 location; // the standard location of the sprite
        protected Vector2 old_location; // the old location of the sprite
        protected Vector2 velocity; // the current velocity of the sprite
        protected SpriteBatch spriteBatch;  // the sprite batch that will be used to draw the sprite
        protected Texture2D texture; //the texture that the object holds
        protected Game game_obj; // a copy of the main  game object
        public int rows { get; set; } // the number of rows
        public int columns { get; set; } // the number of cols
        protected int cur_col = 0; // the runtime active col of the sprite
        protected int cur_row = 0; // the runtime active row of the sprite
        public int cur_height = 0; // the runtime height of the object
        public int cur_width = 0; // the runtime width of the object
        protected SpriteEffects effect; //the applied effect to the sprite
        public int size_type; //size 0 for square type, any positive int for sphere type
        public float scale_factor = 1.0f;
        protected Color spriteColor;

        public void Set_Sprite_Batch(SpriteBatch sprite)
        {
            spriteBatch = sprite;
        }

        public void Content_Load(string texture_loc)
        {
            texture = game_obj.Content.Load<Texture2D>(texture_loc);
        }
    }
}
