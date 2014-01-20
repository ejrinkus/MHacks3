using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MHacksTestOne
{
    class EnemySprite : AbstractPlayerSprite
    {
        protected AbstractPlayerSprite player;
        protected Music music;
        protected bool colliding;
        protected Random random;

        public EnemySprite(Game game_import, List<AbstractSprite> ent, 
            AbstractPlayerSprite p, ref BulletSprite bull, Music m)
        {
            game_obj = game_import;
            //sprite specific info
            rows = 1;
            columns = 1;
            entities = ent; //get a reference to the entity list for collision detection later.
            spriteColor = Color.White;
            random = new Random();
            inital_fall_speed = 0;
            max_free_fall = 0;
            bullets = bull;
            player = p;
            do
            {
                location.X = random.Next(game_obj.GraphicsDevice.Viewport.Width);
                Console.WriteLine(location.X);
            } while (Math.Abs(location.X - player.location.X) < player.cur_width + cur_width);
            do
            {
                location.Y = random.Next(game_obj.GraphicsDevice.Viewport.Height);
            } while (Math.Abs(location.Y - player.location.Y) < player.cur_height + cur_height);
            scale_factor = 0.3f;
            music = m;
            health = 100;
        }

        public void Respawn()
        {
            do
            {
                location.X = random.Next(game_obj.GraphicsDevice.Viewport.Width);
                Console.WriteLine(location.X);
            } while (Math.Abs(location.X - player.location.X) < player.cur_width + cur_width);
            do
            {
                location.Y = random.Next(game_obj.GraphicsDevice.Viewport.Height);
            } while (Math.Abs(location.Y - player.location.Y) < player.cur_height + cur_height);
            health = 100;
        }

        public void Update()
        {
            Vector2 target = player.location;
            double angle = Math.Atan2(target.Y - location.Y, target.X - location.X);
            location.X += (float)Math.Cos(angle);
            location.Y += (float)Math.Sin(angle);

            // Distortion logic (make the player's ears bleed if they touch the enemy)
            Rectangle playerRect = new Rectangle((int)target.X, (int)target.Y, 
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

            for (int i = 0; i < bullets.bullet_arr.Count; i++)
            {
                BulletSprite.Bullet temp_bullet = bullets.bullet_arr[i];
                Rectangle bull = new Rectangle((int)temp_bullet.location.X, (int)temp_bullet.location.Y, (int)temp_bullet.size.X, (int)temp_bullet.size.Y);
                if (thisRect.Intersects(bull))
                {
                    health -= 10;
                    bullets.bullet_arr.RemoveAt(i);
                }
            }

            base.Update();
        }
    }
}
