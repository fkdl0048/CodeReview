namespace BuilderPattern
{
    class TextOptionsBuilder
    {
        private readonly string font;
        private float? fontSize;

        public TextOptionsBuilder(string font)
        {
            this.font = font;
        }

        public TextOptionsBuilder SetFontSize(float fontSize)
        {
            this.fontSize = fontSize;
            return this;
        }

        public TextOptions bulid()
        {
            return new TextOptions(font, fontSize);
        }
    }
}