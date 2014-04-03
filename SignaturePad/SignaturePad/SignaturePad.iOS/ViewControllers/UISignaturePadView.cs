using System;
using MonoTouch.UIKit;
using System.Drawing;
using Intersoft.Crosslight;
using SignaturePad.Core;
using MonoTouch.Foundation;

namespace SignaturePad.iOS
{
	public class UISignaturePadView : UIView
	{
		private MainViewController_iPad _mainController;
		private UILabel _testLabel;
        private UIImage _signImage;
        public MySignaturePad Signature { get; set; }

        public UIImage SignImage 
        { 
            get
            {
                return _signImage;
            }
            set
            {
                if (_signImage != value)
                {
                    Byte[] myByteArray;
                    _signImage = value;
                    NSData imageData = _signImage.AsPNG();
                    myByteArray = new Byte[imageData.Length];
                    this.SetPropertyValue(SignaturePadProperties.SignProperty, myByteArray);
                }
            }
        }

		public MainViewController_iPad MainController
		{
			get {
				return _mainController;
			}
		}


		public UILabel TestLabel
		{
			get {
				if(_testLabel==null)
                    _testLabel=new UILabel (new RectangleF (0, 0f, this.MainController.View.Frame.Width, 100f)){ TextAlignment = UITextAlignment.Center };
				return _testLabel;
			}
		}

       
       
		public UISignaturePadView (MainViewController_iPad mainController)
		{
			_mainController = mainController;
			this.Frame=new RectangleF (this.MainController.View.Frame.Left, this.MainController.NavigationController.NavigationBar.Frame.Bottom, this.MainController.View.Frame.Width, this.MainController.View.Frame.Height);
			this.BackgroundColor = UIColor.White;

            Signature=new MySignaturePad(this){Frame = new RectangleF(0,TestLabel.Frame.Bottom,TestLabel.Frame.Width,500)};


            this.AddSubviews (TestLabel,Signature);
		}
	}
}

