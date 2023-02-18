namespace CopyOnWritePattern
{
    class TextOptions
    {
        private readonly string font;
        private readonly float? fontSize;

        public TextOptions(string font)
        {
            this(font, null);
        }

        private TextOptions(string font, float? fontsize)
        {
            this.font = font;
            this.fontSize = fontSize;
        }

        public float? GetFontsize => fontSize;
        public string GetFont => font;

        TextOptions withFont(string newFont)
        {
            return new TextOptions(newFont, fontSize);
        }

        TextOptions withFontSize(float newFontSize)
        {
            return new TextOptions(font, newFontSize);
        }
    }
}