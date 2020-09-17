using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;

namespace Animators
{
    public class RotateAnimator : SpriteAnimator
    {
        private float m_NumberOfRotatePerSec;
        private TimeSpan m_AnimationLength;
        private eDirectionMove m_eDirection;
        private float m_AngularVelocity;

        public enum eDirectionMove
        {
            Left = -1,
            Right = 1
        }

        public RotateAnimator(string i_Name, TimeSpan i_AnimationLength, float i_NumberOfRotatePerSec, eDirectionMove i_Direction)
            : base(i_Name, i_AnimationLength)
        {
            m_NumberOfRotatePerSec = i_NumberOfRotatePerSec;
            m_AnimationLength = i_AnimationLength;
            m_eDirection = i_Direction;
        }

        public RotateAnimator(TimeSpan i_AnimationLength, float i_NumberOfRotatePerSec, eDirectionMove i_Direction)
            : this("Rotate", i_AnimationLength, i_NumberOfRotatePerSec, i_Direction)
        {
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Scales = this.m_OriginalSpriteInfo.Scales;
            this.BoundSprite.Rotation = 0;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            float rotationVelocity = this.m_NumberOfRotatePerSec * MathHelper.TwoPi * (int) m_eDirection;
            var currentTime = (float)this.m_AnimationLength.TotalSeconds - (float)i_GameTime.ElapsedGameTime.TotalSeconds;

            this.BoundSprite.Rotation += rotationVelocity * currentTime;
        }
    }
}
