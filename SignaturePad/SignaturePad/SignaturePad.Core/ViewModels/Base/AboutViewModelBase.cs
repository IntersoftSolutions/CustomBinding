using Intersoft.Crosslight.Input;

namespace SignaturePad.ViewModels
{
    public class AboutViewModelBase : SampleViewModelBase
    {
        #region Fields

        private string _aboutText;
        private string _introductionText;

        #endregion

        #region Properties

        public string AboutText
        {
            get { return _aboutText; }
            set
            {
                if (_aboutText != value)
                {
                    _aboutText = value;
                    OnPropertyChanged("AboutText");
                }
            }
        }

        public string IntroductionText
        {
            get { return _introductionText; }
            set
            {
                if (_introductionText != value)
                {
                    _introductionText = value;
                    OnPropertyChanged("IntroductionText");
                }
            }
        }

        public DelegateCommand LearnMoreCommand { get; set; }

        #endregion

        #region Constructors

        public AboutViewModelBase()
        {
            this.IntroductionText = "Intersoft Crosslight makes native cross-platform mobile development truly a breeze -- thanks to its innovative data binding framework that makes MVVM possible in the iOS and Android world.";
            this.LearnMoreCommand = new DelegateCommand(ExecuteLearnMore);
        }

        #endregion

        #region Methods

        private void ExecuteLearnMore(object parameter)
        {
            this.MobileService.Browser.Navigate("http://www.intersoftpt.com/crosslight");
        }

        #endregion
    }
}