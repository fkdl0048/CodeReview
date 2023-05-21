namespace BuilderPattern
{
    class TextOptions
    {
        private readonly string font;
        private readonly float? fontSize;

        public TextOptions(string font, float? fontSize)
        {
            this.font = font;
            this.fontSize = fontSize;
        }

        public string GetFont => font;
        public float? GetFontSize => fontSize;
    }
}