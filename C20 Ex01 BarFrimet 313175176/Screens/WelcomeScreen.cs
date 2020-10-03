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
        private IInputManager m_InputManager;
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
            if (m_InputManager == null)
            {
                m_InputManager = this.Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            }
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
press M for main menu
press Esc to exit");
            m_TextMenu = new TextService(textToPrint, this, position, Color.DarkBlue);
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if(m_InputManager.KeyPressed(Keys.Enter))
            {
                ScreensManager.SetCurrentScreen(new LevelTransitionScreen(this.Game));
            }
            else if (m_InputManager.KeyPressed(Keys.Escape))
            {
                this.Game.Exit();
            }
            else if(m_InputManager.KeyPressed(Keys.M))
            {
                ScreensManager.SetCurrentScreen(new MainMenuScreen(this.Game));
            }
        }
    }
}
