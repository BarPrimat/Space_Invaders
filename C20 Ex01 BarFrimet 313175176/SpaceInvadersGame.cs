using System.Collections.Generic;
using GameSprites;
using Infrastructure;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Screens;
using static SpaceInvaders.GameDefinitions;


namespace SpaceInvaders
{
    public class SpaceInvadersGame : Game
    {
        private const bool k_SongToRepeat = true;
        // It is not necessary to save the elements game but they may be used in the future
        private readonly GraphicsDeviceManager r_Graphics;
        private readonly InputManager r_InputManager;
        private readonly ScreensManager r_ScreensManager;
        private readonly SoundManager r_SoundManager;

        public SpaceInvadersGame()
        {
            r_Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            r_InputManager = new InputManager(this);
            r_ScreensManager = new ScreensManager(this);
            r_SoundManager = new SoundManager(this);
            new CollisionsManager(this);
            r_ScreensManager.SetCurrentScreen(new WelcomeScreen(this));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            reloadSounds();
        }

        private void reloadSounds()
        {
            r_SoundManager.AddSoundEffect(GameDefinitions.SoundNameForSSGunShot, this.Content.Load<SoundEffect>(GameDefinitions.SoundPathForSSGunShot));
            r_SoundManager.AddSoundEffect(GameDefinitions.SoundNameForEnemyGunShot, this.Content.Load<SoundEffect>(GameDefinitions.SoundPathForEnemyGunShot));
            r_SoundManager.AddSoundEffect(GameDefinitions.SoundNameForEnemyKill, this.Content.Load<SoundEffect>(GameDefinitions.SoundPathForEnemyKill));
            r_SoundManager.AddSoundEffect(GameDefinitions.SoundNameForMotherShipKill, this.Content.Load<SoundEffect>(GameDefinitions.SoundPathForMotherShipKill));
            r_SoundManager.AddSoundEffect(GameDefinitions.SoundNameForBarrierHit, this.Content.Load<SoundEffect>(GameDefinitions.SoundPathForBarrierHit));
            r_SoundManager.AddSoundEffect(GameDefinitions.SoundNameForGameOver, this.Content.Load<SoundEffect>(GameDefinitions.SoundPathForGameOver));
            r_SoundManager.AddSoundEffect(GameDefinitions.SoundNameForLevelWin, this.Content.Load<SoundEffect>(GameDefinitions.SoundPathForLevelWin));
            r_SoundManager.AddSoundEffect(GameDefinitions.SoundNameForLifeDie, this.Content.Load<SoundEffect>(GameDefinitions.SoundPathForLifeDie));
            r_SoundManager.AddSoundEffect(GameDefinitions.SoundNameForMenuMove, this.Content.Load<SoundEffect>(GameDefinitions.SoundPathForMenuMove));
            r_SoundManager.AddSong(GameDefinitions.SoundNameForBGMusic, this.Content.Load<Song>(GameDefinitions.SoundPathForBGMusic));
            r_SoundManager.PlaySong(GameDefinitions.SoundNameForBGMusic, k_SongToRepeat);
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.r_Graphics.PreferredBackBufferWidth = GameDefinitions.PreferredBackBufferWidth;
            this.r_Graphics.PreferredBackBufferHeight = GameDefinitions.PreferredBackBufferHeight;
            this.r_Graphics.ApplyChanges();
            Mouse.SetPosition(0, GraphicsDevice.Viewport.Height);
            this.Window.Title = GameDefinitions.GameName;
        }

        protected override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            if(r_InputManager.KeyPressed(Keys.M))
            {
                r_SoundManager.ToggleSound();
            }
        }

        protected override void Draw(GameTime i_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(i_GameTime);
        }
    }
}
