using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MHacksTestOne
{
    class AbstractPlayerSprite : AbstractSprite // a basic player that can have health and can move
    {
        protected int  health = 100; // we want to use this?
        protected List<AbstractSprite> entities;
        public bool isSquareSquareCollision() //collisiong detection between square and square object
        {
            return true;
        }

        public bool isSphereSquareCollision() //collision between sphere and square object
        {
            return true;
        }
    }
}
