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

        public KeyboardPlayerSprite(Game game_import, ref List<AbstractSprite> ent) : base()
        {
            game_obj = game_import;
            //sprite specific info
            rows = 2; 
            columns = 6;
            velocity.Y -= 1; //set an inital velocity so that the player falls to the ground
            entities = ent; //get a reference to the entity list for collision detection later.
        }

        
        public void Update()
        {
            oldkeyState = curkeyState;
            curkeyState = Keyboard.GetState();
            
            //movement
            
            if (curkeyState.IsKeyDown(Keys.Left) && !curkeyState.IsKeyDown(Keys.Right) ) //moving left
            {
                if (right_counter > 4)
                    right_counter = 1;
                right_counter++;
                cur_row = 1;
                cur_col = right_counter;
                effect = SpriteEffects.FlipHorizontally;
                location.X -= 9;
            }
            else if (!curkeyState.IsKeyDown(Keys.Left) && curkeyState.IsKeyDown(Keys.Right)) //moving right
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



            base.Update();
        }

        public void Draw()
        {
            //get the sub animated pixels
            Rectangle subsection =  new Rectangle(cur_width*cur_col,cur_height*cur_row,cur_width,cur_height);
            spriteBatch.Draw(texture, location, subsection, Color.Red, 0.0f,new Vector2(0,0),1.0f, effect,0.0f); //draw the player in the location specified
        }
    }
}
