using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.VisualStyles;
using GameSprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders;

namespace Screens
{
    public class PauseScreen : GameScreen
    {
        private TextService m_ResumeText;

        public PauseScreen(Game i_Game) : base(i_Game)
        {
            this.IsModal = true;
            this.IsOverlayed = true;
            this.BlackTintAlpha = 0.40f;
            this.UseGradientBackground = false;
        }

        public override void Initialize()
        {
            base.Initialize();
            initText();
        }

        private void initText()
        {
            int xOffset = (int)(this.CenterOfViewPort.X / 5);
            int yOffset = (int)(this.CenterOfViewPort.X / 2);
            Vector2 position = new Vector2(this.CenterOfViewPort.X - xOffset, this.CenterOfViewPort.Y - yOffset);

            m_ResumeText = new TextService(SpritesDefinition.TextFont, this, position, Color.White);
            m_ResumeText.TextToPrint = @"
[ Game Paused ]
R - To Resume Game";
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if(InputManager.KeyPressed(Keys.R))
            {
                this.ExitScreen();
            }
        }
    }
}
