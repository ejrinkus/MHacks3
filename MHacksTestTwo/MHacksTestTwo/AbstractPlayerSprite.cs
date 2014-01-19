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
        protected float acceleration = -0.05f; // the amount of pixels per tick that free fall is increased
        protected float max_free_fall = -15; // the max pixels per second a person can fall at
        protected float inital_fall_speed = -1; // the inital free fall speed that must be less than zero
        protected float jump_velocity = 20; // the amount on inital velocity for a jump
        protected float jump_correct;

        protected AbstractPlayerSprite() 
        {
            jump_correct = acceleration + inital_fall_speed;
        }
        

        public bool isSquareSquareCollision(Rectangle ent_rect, Rectangle my_rect) //collisiong detection between square and square object
        {
            
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



        protected void Update()
        {
            //move based on velocity after checking for collisions
            bool collision = false;
            for (int i = 0; i < entities.Count; i++)
            {
                AbstractSprite platform = entities.ElementAt(i);
                Rectangle my_rect = new Rectangle((int)location.X - (int)velocity.X, (int)location.Y - (int)velocity.Y, cur_width, cur_height);
                Rectangle ent_rec = new Rectangle((int)platform.location.X, (int)platform.location.Y, (int)(platform.cur_width*platform.scale_factor), (int)(platform.cur_height*platform.scale_factor));
                if (platform.size_type == 0)//square case
                {
                    collision = isSquareSquareCollision(ent_rec, my_rect);
                    if (collision == true)
                    {
                        break;
                    }
                }
            }
            if (collision == false) // no collisions
            {
                location.X -= velocity.X;
                location.Y -= velocity.Y; //the 10 is for gravity //this is a useless comment //then why is it in here? //idk
                velocity.Y += inital_fall_speed;

                //gravity
                if (velocity.Y != 0) //if they're falling then keep accelerating them
                {
                    velocity.Y += acceleration;
                    if (velocity.Y == 0) //if we hit a stop condition, prevent that from triggering the user to jump
                        velocity.Y = inital_fall_speed;
                }


                if (velocity.Y < max_free_fall) //limit free fall rate
                {
                    velocity.Y = max_free_fall;
                }
            }
            else //on collision
            {
                velocity.Y = 0;
            }






            //edge detection
            cur_width = texture.Width / columns;
            cur_height = texture.Height / rows;
            if ((location.X) < 0) //going off left edge of screen
            {
                location.X = 0;
            }
            else if ((location.X + cur_width) > game_obj.GraphicsDevice.Viewport.Width)
            {
                location.X = game_obj.GraphicsDevice.Viewport.Width - cur_width;
            }
            if ((location.Y - cur_height / 2) < 0)
            {
                location.Y = cur_height / 2;
                velocity.Y -= 1; //start the player on a downward path
            }
            else if (((location.Y + cur_height) > game_obj.GraphicsDevice.Viewport.Height))
            {
                location.Y = game_obj.GraphicsDevice.Viewport.Height - cur_height;
                velocity.Y = 0; //they're at the bottom, stop them
            }
        }

        public void Draw()
        {
            //get the sub animated pixels
            Rectangle subsection = new Rectangle(cur_width * cur_col, cur_height * cur_row, cur_width, cur_height);
            spriteBatch.Draw(texture, location, subsection, spriteColor, 0.0f, new Vector2(0, 0), 1.0f, effect, 0.0f); //draw the player in the location specified
        }
    }
}
