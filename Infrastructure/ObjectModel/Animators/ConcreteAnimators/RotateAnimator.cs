using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;

namespace Infrastructure.ObjectModel.Animators.ConcreteAnimators
{
    public class RotateAnimator : SpriteAnimator
    {
        private float m_NumberOfRotatePerSec;
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
            m_eDirection = i_Direction;
            m_AngularVelocity = (float)MathHelper.TwoPi * (float)m_NumberOfRotatePerSec * (int) m_eDirection;
        }

        public RotateAnimator(TimeSpan i_AnimationLength, float i_NumberOfRotatePerSec, eDirectionMove i_Direction)
            : this("Rotate", i_AnimationLength, i_NumberOfRotatePerSec, i_Direction)
        {
        }

        protected override void RevertToOriginal()
        {
           this.BoundSprite.AngularVelocity = 0;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            this.BoundSprite.AngularVelocity = m_AngularVelocity;
        }
    }
}
