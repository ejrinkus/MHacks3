using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MHacksTestOne
{
    class AbstractPlayerSprite : AbstractSprite // a basic player that can have health and can move
    {
        protected int  health = 100; // we want to use this?
        protected List<AbstractSprite> entities;
        public bool isSquareSquareCollision(Rectangle ent_rect) //collisiong detection between square and square object
        {
            Rectangle my_rect = new Rectangle((int)location.X, (int)location.Y, cur_width, cur_height);
            if (my_rect.Intersects(ent_rect))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool isSphereSquareCollision() //collision between sphere and square object
        {
            return true;
        }
    }
}
