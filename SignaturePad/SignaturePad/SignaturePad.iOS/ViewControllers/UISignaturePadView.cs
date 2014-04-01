using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace SignaturePad.iOS
{
	public class UISignaturePadView : UIView
	{
		private MainViewController_iPad _mainController;
		private UILabel _testLabel;


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
					_testLabel=new UILabel (new RectangleF (0, 200f, this.MainController.View.Frame.Width, 100f)){ TextAlignment = UITextAlignment.Center };
				return _testLabel;
			}
		}

		public UISignaturePadView (MainViewController_iPad mainController)
		{
			_mainController = mainController;
			this.Frame=new RectangleF (this.MainController.View.Frame.Left, this.MainController.NavigationController.NavigationBar.Frame.Bottom, this.MainController.View.Frame.Width, this.MainController.View.Frame.Height);
			this.BackgroundColor = UIColor.White;


			this.AddSubview (TestLabel);
		}
	}
}

