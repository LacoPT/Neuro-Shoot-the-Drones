using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones
{
    internal static class Resources
    {
        private static Texture2D playerTextureAtlas;
        private static Texture2D gameFrameUI;
        private static Texture2D bulletTextureAtlas;
        private static Texture2D drone;

        public static Texture2D PlayerTextureAtlas { get => playerTextureAtlas; set => playerTextureAtlas = playerTextureAtlas == null? value : playerTextureAtlas; }
        public static Texture2D GameFrameUI { get => gameFrameUI; set => gameFrameUI = gameFrameUI == null? value : gameFrameUI; }
        public static Texture2D BulletTextureAtlas { get => bulletTextureAtlas; set => bulletTextureAtlas = bulletTextureAtlas == null? value : gameFrameUI; }
        public static Texture2D Drone { get => drone; set => drone = drone == null ? value : drone; }
    }
}
