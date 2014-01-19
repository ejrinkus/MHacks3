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
        protected Music music;
        protected bool colliding;

        public EnemySprite(Game game_import, List<AbstractSprite> ent, 
            AbstractPlayerSprite p, ref BulletSprite bull, Music m)
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
            music = m;
        }

        public void Update()
        {
            Vector2 target = player.location;
            double angle = Math.Atan2(target.Y - location.Y, target.X - location.X);
            location.X += (float)Math.Cos(angle);
            location.Y += (float)Math.Sin(angle);

            // Distortion logic (make the player's ears bleed if they touch the enemy)
            Rectangle playerRect = new Rectangle((int)player.location.X, (int)player.location.Y, 
                (int)(player.cur_width*player.scale_factor), (int)(player.cur_height*player.scale_factor));
            Rectangle thisRect = new Rectangle((int)location.X, (int)location.Y, 
                (int)(cur_width*scale_factor), (int)(cur_height*scale_factor));
            if (isSquareSquareCollision(playerRect, thisRect) && !colliding)
            {
                music.toggleDistortion();
                music.toggleReverb();
                colliding = true;
            }
            if (!isSquareSquareCollision(playerRect, thisRect) && colliding)
            {
                music.toggleDistortion();
                music.toggleReverb();
                colliding = false;
            }
            if (isSquareSquareCollision(playerRect, thisRect))
            {
                player.health -= 0.1f;
            }

            base.Update();
        }
    }
}
