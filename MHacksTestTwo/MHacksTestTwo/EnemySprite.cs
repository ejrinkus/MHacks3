using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MHacksTestOne
{
    class EnemySprite : AbstractPlayerSprite
    {
        protected BulletSprite bullet;
        protected AbstractPlayerSprite player;

        public EnemySprite(Game game_import, List<AbstractSprite> ent, AbstractPlayerSprite p, ref BulletSprite bull)
        {
            game_obj = game_import;
            //sprite specific info
            rows = 1;
            columns = 1;
            entities = ent; //get a reference to the entity list for collision detection later.
            spriteColor = Color.White;
            inital_fall_speed = 0;
            max_free_fall = 0;
            bullet = bull;
            player = p;
            location.X = 30f;
            location.Y = 30f;
            scale_factor = 0.3f;
        }

        public void Update()
        {
            Vector2 target = player.location;
            double angle = Math.Atan2(target.Y - location.Y, target.X - location.X);
            location.X += (float)Math.Cos(angle);
            location.Y += (float)Math.Sin(angle);

            base.Update();
        }
    }
}
