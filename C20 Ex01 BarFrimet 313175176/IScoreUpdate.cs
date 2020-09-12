using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders
{
    public interface IScoreUpdate
    {
        event EventHandler<EventArgs> UpdateScore;
        void ScoreUpdate(IScoreUpdate i_UpdateScore);
    }
}
