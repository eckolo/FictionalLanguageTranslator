namespace FictionalLanguageTranslator.Models.Application.Value
{
    public class GermanStylePronunciationModel
    {
        public GermanStylePronunciationModel(string fictional, string pronunciation)
        {
            this.fictional = fictional;
            this.pronunciation = pronunciation;
        }

        public string fictional { get; }
        public string pronunciation { get; }
    }
}
