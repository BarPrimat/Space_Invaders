using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using SpaceInvaders;

namespace GameSprites
{
    public class BarriersGroup : GameComponent
    {
        private readonly List<Barrier> r_ListOfBarriers = new List<Barrier>();
        private readonly int r_NumberOfWalls;

        public BarriersGroup(GameScreen i_GameScreen, string i_TexturePath, int i_NumberOfWalls) : base(i_GameScreen.Game)
        {
            if(i_NumberOfWalls > 0)
            {
                r_NumberOfWalls = i_NumberOfWalls;
                for (int i = 0; i < i_NumberOfWalls; i++)
                {
                    r_ListOfBarriers.Add(new Barrier(i_GameScreen, i_TexturePath, GameDefinitions.BarrierStartDirectionToMove));
                }

                i_GameScreen.Add(this);
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            initPosition();
        }

        private void initPosition()
        {
            float middleOfViewportWidth = this.Game.GraphicsDevice.Viewport.Width / 2;
            // Find the closest start position to print the texture
            float spaceBetweenBarrier = GameDefinitions.SpaceBetweenBarrier * r_ListOfBarriers[0].Texture.Width;
            // If the number of Barriers is even find the closest right print if is it odd find the closest left print 
            float xPosition = r_ListOfBarriers.Count % 2 == 0 ? middleOfViewportWidth + spaceBetweenBarrier / 2
                                  : middleOfViewportWidth - (r_ListOfBarriers[0].Texture.Width / 2);
            float yPosition = GameDefinitions.SpaceshipYStartPosition - (r_ListOfBarriers[0].Texture.Height * GameDefinitions.VerticalSpaceBetweenSpaceshipAndBarrier) - r_ListOfBarriers[0].Texture.Height;

            spaceBetweenBarrier += r_ListOfBarriers[0].Texture.Width;
            // Go to the left most position to print the texture
            xPosition -= r_ListOfBarriers.Count / 2 * spaceBetweenBarrier;
            foreach (Barrier barrier in r_ListOfBarriers)
            {
                barrier.SetFirstPosition(new Vector2(xPosition, yPosition));
                xPosition += spaceBetweenBarrier;
            }
        }
    }
}
