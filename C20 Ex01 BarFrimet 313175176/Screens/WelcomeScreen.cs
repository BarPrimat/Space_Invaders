using System;
using System.Collections.Generic;
using System.Text;
using GameSprites;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Screens.MainMenuScreens;
using SpaceInvaders;

namespace Screens
{
    public class WelcomeScreen : GameScreen
    {
        private const float k_ScalePulseForTitle = 1.25f;
        private const float k_PulsePerSecForTitle = 1f;
        private readonly Background r_Background;
        private readonly Sprite r_SpaceInvadersTitle;
        private TextService m_TextMenu;

        public WelcomeScreen(Game i_Game) : base(i_Game)
        {
            r_Background = new Background(this, SpritesDefinition.BackgroundAsset);
            r_SpaceInvadersTitle = new Sprite(SpritesDefinition.SpaceInvadersTitle, this);
        }

        public override void Initialize()
        {
            base.Initialize();
            r_SpaceInvadersTitle.PositionOrigin = r_SpaceInvadersTitle.SourceRectangleCenter;
            r_SpaceInvadersTitle.Position = this.CenterOfViewPort;
            initAnimations();
            initText();
        }

        private void initAnimations()
        {
            PulseAnimator pulseAnimator = new PulseAnimator("PulseAnimator", TimeSpan.Zero, k_ScalePulseForTitle, k_PulsePerSecForTitle);

            r_SpaceInvadersTitle.PositionOrigin = r_SpaceInvadersTitle.SourceRectangleCenter;
            r_SpaceInvadersTitle.RotationOrigin = r_SpaceInvadersTitle.SourceRectangleCenter;
            r_SpaceInvadersTitle.Animations.Add(pulseAnimator);
            r_SpaceInvadersTitle.Animations.Restart();
        }

        private void initText()
        {
            Vector2 position = new Vector2(this.CenterOfViewPort.X - (this.CenterOfViewPort.X / 2), this.CenterOfViewPort.Y + (this.CenterOfViewPort.Y / 2));
            string textToPrint = string.Format(@"
press Enter for a new game
press Esc to exit
press M for main menu");

            m_TextMenu = new TextService(textToPrint, this, position, Color.DarkBlue);
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if(InputManager.KeyPressed(Keys.Enter))
            {
                ScreensManager.SetCurrentScreen(new PlayScreen(this.Game));
            }
            else if (InputManager.KeyPressed(Keys.Escape))
            {
                this.Game.Exit();
            }
            else if(InputManager.KeyPressed(Keys.M))
            {
                ScreensManager.SetCurrentScreen(new MainMenuScreen(this.Game));
            }
        }
    }
}
