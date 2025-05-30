using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    //TODO: Instead of cheking for null, make an initialization method
    internal static class Resources
    {
        private static Texture2D playerTextureAtlas;
        private static Texture2D gameFrameUI;
        private static Texture2D bulletTextureAtlas;
        private static Texture2D drone;
        private static Texture2D lightDrone;
        private static Texture2D minawan;
        private static SpriteFont defaultFont;
        private static Texture2D healthBarAtlas;
        private static Texture2D hitcircle;

        public static Texture2D PlayerTextureAtlas { get => playerTextureAtlas; set => playerTextureAtlas = playerTextureAtlas == null? value : playerTextureAtlas; }
        public static Texture2D GameFrameUI { get => gameFrameUI; set => gameFrameUI = gameFrameUI == null? value : gameFrameUI; }
        public static Texture2D BulletTextureAtlas { get => bulletTextureAtlas; set => bulletTextureAtlas = bulletTextureAtlas == null? value : gameFrameUI; }
        public static Texture2D Drone { get => drone; set => drone = drone == null ? value : drone; }
        public static Texture2D Minawan { get => minawan; set => minawan = minawan == null ? value : minawan; }
        public static Texture2D LightDrone { get => lightDrone; set => lightDrone = lightDrone == null ? value : lightDrone; }
        public static SpriteFont DefaultFont { get => defaultFont; set => defaultFont = defaultFont == null ? value : defaultFont; }
        public static Texture2D BlackPixel;
        public static Texture2D HealthBarAtlas {get => healthBarAtlas; set => healthBarAtlas = healthBarAtlas == null? value : healthBarAtlas; }
        public static Effect GrayScale;
        public static Texture2D Hitcirle { get => hitcircle; set => hitcircle = hitcircle == null ? value : hitcircle; }
    }
}
