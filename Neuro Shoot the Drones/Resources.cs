

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Neuro_Shoot_the_Drones
{
    internal static class Resources
    {
        // Закрытые поля
        private static Texture2D _playerTextureAtlas;
        private static Texture2D _gameFrameUI;
        private static Texture2D _bulletTextureAtlas;
        private static Texture2D _drone;
        private static Texture2D _lightDrone;
        private static Texture2D _minawan;
        private static SpriteFont _defaultFont;
        private static Texture2D _healthBarAtlas;
        private static Texture2D _hitCircle;
        private static Texture2D _pickUpAtlas;
        private static Texture2D _cerb;
        private static Texture2D _blackPixel;
        private static Effect _grayScale;


        public static Texture2D PlayerTextureAtlas => _playerTextureAtlas;
        public static Texture2D GameFrameUI => _gameFrameUI;
        public static Texture2D BulletTextureAtlas => _bulletTextureAtlas;
        public static Texture2D Drone => _drone;
        public static Texture2D LightDrone => _lightDrone;
        public static Texture2D Minawan => _minawan;
        public static SpriteFont DefaultFont => _defaultFont;
        public static Texture2D HealthBarAtlas => _healthBarAtlas;
        public static Texture2D HitCircle => _hitCircle;
        public static Texture2D PickUpAtlas => _pickUpAtlas;
        public static Texture2D Cerb => _cerb;
        public static Texture2D BlackPixel => _blackPixel;
        public static Effect GrayScale => _grayScale;

        public static void Initialize(ContentManager content)
        {
            if (content == null)
                throw new System.ArgumentNullException(nameof(content));

            _playerTextureAtlas = content.Load<Texture2D>("PlayerAtlas");
            _gameFrameUI = content.Load<Texture2D>("GameFrame");
            _bulletTextureAtlas = content.Load<Texture2D>("Bullets");
            _drone = content.Load<Texture2D>("drone");
            _lightDrone = content.Load<Texture2D>("lightdrone");
            _minawan = content.Load<Texture2D>("minawan");
            _defaultFont = content.Load<SpriteFont>("ActualFontMap");
            _defaultFont.Spacing = -6;
            _blackPixel = content.Load<Texture2D>("blackPixel");
            _healthBarAtlas = content.Load<Texture2D>("HealthBarAtlas");
            _grayScale = content.Load<Effect>("GrayScale");
            _hitCircle = content.Load<Texture2D>("Hitcircle");
            _pickUpAtlas = content.Load<Texture2D>("PickUpAtlas");
            _cerb = content.Load<Texture2D>("Cerb");
        }
    }

}