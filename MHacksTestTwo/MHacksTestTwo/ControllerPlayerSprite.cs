using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MHacksTestOne
{
    class ControllerPlayerSprite : AbstractPlayerSprite // a player that uses a generic controller to move
    {
        PlayerIndex player; // the controller number 1-4
        GamePadState oldgamePadState; // the last known state of the controller
        GamePadState curgamePadState; // the current state of the controller
        //counters used for tracking the animated movements from the sprite
        int standing_counter = 0; 
        int left_counter = 0;
        int right_counter = 0;
        Random rnd1 = new Random(); //random number generator used for watching the plaer stand still

        public ControllerPlayerSprite(PlayerIndex play, Game game_import, ref List<AbstractSprite> ent) : base()
        {
            player = play;
            game_obj = game_import;
            //sprite specific info
            rows = 2; 
            columns = 6;
            velocity.Y -= 1; //set an inital velocity so that the player falls to the ground
            entities = ent; //get a reference to the entity list for collision detection later.
        }

        
        public void Update()
        {
            oldgamePadState = curgamePadState;
            curgamePadState = GamePad.GetState(player);
            
            //movement
            location.X += curgamePadState.ThumbSticks.Left.X * 9;
            
            if (curgamePadState.ThumbSticks.Left.X < 0 ) //moving left
            {
                if (right_counter > 4)
                    right_counter = 1;
                right_counter++;
                cur_row = 1;
                cur_col = right_counter;
                effect = SpriteEffects.FlipHorizontally;
            }
            else if (curgamePadState.ThumbSticks.Left.X > 0) //moving right
            {
                if (left_counter > 4)
                    left_counter = 1;
                left_counter++;
                cur_row = 1;
                cur_col = left_counter;
                effect = SpriteEffects.None;
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
            if (curgamePadState.Buttons.A == ButtonState.Pressed && oldgamePadState.Buttons.A == ButtonState.Released && (velocity.Y == 0 || velocity.Y == jump_correct)) {
                velocity.Y += 20;
                cur_row = 0;
                cur_col = 0;
            }



            base.Update();
        }

        public void Draw()
        {
            //get the sub animated pixels
            Rectangle subsection =  new Rectangle(cur_width*cur_col,cur_height*cur_row,cur_width,cur_height);
            spriteBatch.Draw(texture, location, subsection, Color.White, 0.0f,new Vector2(0,0),1.0f, effect,0.0f); //draw the player in the location specified
        }
    }
}
