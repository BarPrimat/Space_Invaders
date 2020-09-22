using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class FadeoutAnimator : SpriteAnimator
    {
        private float m_CurrentTimeToFinish = 0;
        private const float k_MinimumValueToFadeout = 0;

        public FadeoutAnimator(string i_Name, TimeSpan i_AnimationLength)
            : base(i_Name, i_AnimationLength)
        {
            m_CurrentTimeToFinish = (float) i_AnimationLength.TotalSeconds;
        }

        public FadeoutAnimator(TimeSpan i_AnimationLength)
            : this("Fadeout", i_AnimationLength)
        {
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Opacity = m_OriginalSpriteInfo.Opacity;
            m_CurrentTimeToFinish = (float) this.AnimationLength.TotalSeconds;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            m_CurrentTimeToFinish -= (float)i_GameTime.ElapsedGameTime.TotalSeconds;
            this.BoundSprite.Opacity = MathHelper.Clamp(m_CurrentTimeToFinish / (float) this.AnimationLength.TotalSeconds, k_MinimumValueToFadeout, this.BoundSprite.Opacity);
        }
    }
}