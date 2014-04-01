using Intersoft.Crosslight.ViewModels;

namespace SignaturePad.ViewModels
{
    public class SampleViewModelBase : ViewModelBase
    {
        #region Fields

        private string _footerText;

        #endregion

        #region Properties

        public string FooterText
        {
            get { return _footerText; }
            set
            {
                if (_footerText != value)
                {
                    _footerText = value;
                    OnPropertyChanged("FooterText");
                }
            }
        }

        #endregion

        #region Constructors

        public SampleViewModelBase()
        {
            this.FooterText = "Powered by Crosslight®";
        }

        #endregion
    }
}