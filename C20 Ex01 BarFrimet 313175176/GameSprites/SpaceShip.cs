using System;
using System.Collections.Generic;
using System.Text;
using C20_Ex01_BarFrimet_313175176;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static C20_Ex01_BarFrimet_313175176.GameDefinitions;


namespace GameSprites
{
    public class Spaceship : Sprite
    {
        private static readonly Color sr_SpaceshipTint = Color.White;
        private const float k_SpaceshipSpeed = 130;
        private readonly InputManager r_InputManager;

        public Spaceship(Game i_Game, string i_TexturePath) : base(i_Game, i_TexturePath, sr_SpaceshipTint)
        {
            r_InputManager = new InputManager();
        }

        public static float SpaceshipSpeed => k_SpaceshipSpeed;

        public override void Initialize()
        {
            base.Initialize();
            InitPosition();
        }

        public override void InitPosition()
        {
            // Init the ship position
            float x = (float) PreferredBackBufferWidth;
            float y = (float) PreferredBackBufferHeight;

            // Offset:
            x -= this.Texture.Width;
            y -= this.Texture.Height;

            // Put it a little bit higher:
            y -= 10;
            this.Position = new Vector2(x, y);
        }

        public override void Update(GameTime i_GameTime)
        {
            moveSpaceship(i_GameTime);
        }

        private void moveSpaceship(GameTime i_GameTime)
        {
            float maxBoundaryWithoutOffset = GraphicsDevice.Viewport.Width - Texture.Width;
            float keyboardNewXPosition = r_InputManager.KeyboardXPositionToMove(i_GameTime, this.Position.X);

            setupNewPosition(keyboardNewXPosition, maxBoundaryWithoutOffset);
            float mouseNewXPosition = this.Position.X + r_InputManager.GetMousePositionDelta().X;
            setupNewPosition(mouseNewXPosition, maxBoundaryWithoutOffset);
        }

        private void setupNewPosition(float i_NewXPosition, float i_MaxBoundaryWithoutOffset)
        {
            i_NewXPosition = MathHelper.Clamp(i_NewXPosition, 0, i_MaxBoundaryWithoutOffset);
            this.Position = new Vector2(i_NewXPosition, Position.Y);
        }
    }
}
