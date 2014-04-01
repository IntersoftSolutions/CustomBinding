using Intersoft.Crosslight;

namespace SignaturePad
{
    public class AboutBindingProvider : BindingProvider
    {
        #region Constructors

        public AboutBindingProvider()
        {
            this.AddBinding("IntroductionLabel", BindableProperties.TextProperty, "IntroductionText");
            this.AddBinding("AboutLabel", BindableProperties.TextProperty, "AboutText");
            this.AddBinding("FooterLabel", BindableProperties.TextProperty, "FooterText");
            this.AddBinding("LearnMoreButton", BindableProperties.CommandProperty, "LearnMoreCommand");
        }

        #endregion
    }
}