using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class ShrinkAnimator : SpriteAnimator
    {
        private TimeSpan m_ShrinkLength;
        private float m_CurrentTimeToFinish;

        public ShrinkAnimator(string i_Name, TimeSpan i_AnimationLength) : base(i_Name, i_AnimationLength)
        {
            m_ShrinkLength = i_AnimationLength;
            m_CurrentTimeToFinish = (float) i_AnimationLength.TotalSeconds;
        }

        public ShrinkAnimator(TimeSpan i_AnimationLength) : this("Shrink", i_AnimationLength)
        {
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Scales = m_OriginalSpriteInfo.Scales;
            m_CurrentTimeToFinish = (float) this.AnimationLength.TotalSeconds;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            m_CurrentTimeToFinish -= (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            this.BoundSprite.Scales = m_OriginalSpriteInfo.Scales * new Vector2(m_CurrentTimeToFinish / (float) this.m_ShrinkLength.TotalSeconds);
        }
    }
}
