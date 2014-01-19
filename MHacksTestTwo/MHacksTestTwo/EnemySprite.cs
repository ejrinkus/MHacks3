using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MHacksTestOne
{
    class EnemySprite : AbstractPlayerSprite
    {
        public EnemySprite(Game game_import, List<AbstractSprite> ent)
        {
            game_obj = game_import;
            //sprite specific info
            rows = 2;
            columns = 6;
            velocity.Y -= 1; //set an inital velocity so that the player falls to the ground
            entities = ent; //get a reference to the entity list for collision detection later.
            spriteColor = Color.Blue;
        }
    }
}
