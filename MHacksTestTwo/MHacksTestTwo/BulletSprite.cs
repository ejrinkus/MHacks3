using System;
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
            Vector2 velocity;
            Vector2 location;
            Vector2 size;
        }
        public List<Bullet> bullet_arr;

        public BulletSprite()
        {
            bullet_arr = new List<Bullet>();
        }

        public void spawn_bullet(Vector2 dir)
        {

        }

        public void Update()
        {
            for (int i = 0; i < bullet_arr.Count; i++)
            {
                Bullet temp = bullet_arr.ElementAt(i);

                //bullet_arr.
            }
        }

        public void Draw()
        {
        }
    }
}
