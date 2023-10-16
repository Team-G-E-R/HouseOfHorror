using System;

namespace MrPaganini.Infrastruct.Settings
{
    [Serializable]
    public class ScreenSettings
    {
        public int Width { get; private set; }
        public int Height{ get; private set; }
        public int Hz { get; private set; }

        private int _widthDefault = 1920;
        private int _heightDefault = 1080;
        private int _hzDefault = 60;
        
        
        public void SetStartResolution()
        {
            Width = _widthDefault;
            Height = _heightDefault;
            Hz = _hzDefault;
        }

        public void SetResolution(int width, int height, int hz)
        {
            Width = width;
            Height = height;
            Hz = hz;
        }
        
        
    }
}