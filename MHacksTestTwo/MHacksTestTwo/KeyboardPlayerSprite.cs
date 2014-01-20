using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MHacksTestOne
{
    class KeyboardPlayerSprite : AbstractPlayerSprite
    {
        KeyboardState oldkeyState; // the last known state of the controller
        KeyboardState curkeyState; // the current state of the controller
        //counters used for tracking the animated movements from the sprite
        int standing_counter = 0; 
        int left_counter = 0;
        int right_counter = 0;
        Random rnd1 = new Random(); //random number generator used for watching the plaer stand still
        int bullet_delay_counter = 0;

        public KeyboardPlayerSprite(Game game_import, ref List<AbstractSprite> ent, ref BulletSprite bull) : base()
        {
            game_obj = game_import;
            //sprite specific info
            rows = 2; 
            columns = 6;
            velocity.Y -= 1; //set an inital velocity so that the player falls to the ground
            entities = ent; //get a reference to the entity list for collision detection later.
            spriteColor = Color.White;
            bullets = bull;
        }

        
        public void Update()
        {
            oldkeyState = curkeyState;
            curkeyState = Keyboard.GetState();
            
            
            
            //movement
            
            if (curkeyState.IsKeyDown(Keys.A) && !curkeyState.IsKeyDown(Keys.D) ) //moving left
            {
                if (right_counter > 4)
                    right_counter = 1;
                right_counter++;
                cur_row = 1;
                cur_col = right_counter;
                effect = SpriteEffects.FlipHorizontally;
                location.X -= 9;
            }
            else if (!curkeyState.IsKeyDown(Keys.A) && curkeyState.IsKeyDown(Keys.D)) //moving right
            {
                if (left_counter > 4)
                    left_counter = 1;
                left_counter++;
                cur_row = 1;
                cur_col = left_counter;
                effect = SpriteEffects.None;
                location.X += 9;
            }
            else //standing still
            {
                if (rnd1.Next(10) == 1)
                    standing_counter++;
                if (standing_counter > 5)
                    standing_counter = 0;
                cur_col = standing_counter;
                cur_row = 0;
            }

            //jumping
            if (curkeyState.IsKeyDown(Keys.Space) && oldkeyState.IsKeyUp(Keys.Space) && (velocity.Y == 0 || velocity.Y == jump_correct)) {
                velocity.Y += jump_velocity;
                cur_row = 0;
                cur_col = 0;
            }

            //firing
            if (curkeyState.IsKeyDown(Keys.W) && bullet_delay_counter == 5) // while holding mouse down
            {
                System.Drawing.Point mouse = System.Windows.Forms.Control.MousePosition;
                Vector2 mouse_coords = new Vector2(); //get mouse coords
                mouse_coords.X = mouse.X;
                mouse_coords.Y = mouse.Y;
                Vector2 launch_dir = mouse_coords - location; //subtract/add the mouse coords from player loc to make a vector
                launch_dir.Normalize();
                launch_dir.X *= 3;
                launch_dir.Y *= 3;
                bullets.spawn_bullet(launch_dir, location, this);
            }
           

            bullet_delay_counter++;
            if (bullet_delay_counter >= 7)
                bullet_delay_counter = 0;


            base.Update();
        }

    }
}
