﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MHacksTestOne
{
    class BulletSprite : AbstractSprite
    {
        public struct Bullet
        {
            public Vector2 velocity;
            public Vector2 location;
            public Vector2 size;
            public AbstractPlayerSprite owner;
        }
        public List<Bullet> bullet_arr;

        public BulletSprite(Game game_import)
        {
            bullet_arr = new List<Bullet>();
            game_obj = game_import;
            spriteColor = Color.White;
            scale_factor = 0.1f;
        }

        public void spawn_bullet(Vector2 dir, Vector2 loc, AbstractPlayerSprite owner)
        {
            Bullet temp = new Bullet();
            temp.owner = owner;
            temp.location = loc;
            temp.velocity = dir;
            bullet_arr.Add(temp);
        }

        public void Update()
        {
            cur_width = texture.Width;
            cur_height = texture.Height;
            for (int i = 0; i < bullet_arr.Count; i++)
            {
                Bullet temp = bullet_arr[i];
                temp.location += temp.velocity;
                Rectangle rect = new Rectangle((int)temp.location.X, (int)temp.location.Y, cur_width, cur_height);
                if (game_obj.GraphicsDevice.Viewport.Bounds.Contains(rect))
                {
                    bullet_arr[i] = temp;
                }
                else
                {
                    bullet_arr.RemoveAt(i);
                }
            }
        }

        public void Draw()
        {
            //get the sub animated pixels
            Rectangle subsection = new Rectangle(0, 0, cur_width, cur_height);
            for (int i = 0; i < bullet_arr.Count; i++)
            {
                spriteBatch.Draw(texture, location, subsection, spriteColor, 0.0f, new Vector2(0, 0), 1.0f, effect, 0.0f); //draw the player in the location specified
            }
        }
    }
}
