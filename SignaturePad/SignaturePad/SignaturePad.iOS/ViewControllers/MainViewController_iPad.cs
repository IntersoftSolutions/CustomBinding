using SignaturePad.ViewModels;
using Intersoft.Crosslight;
using Intersoft.Crosslight.iOS;
using SignaturePad.Core;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.ObjCRuntime;

namespace SignaturePad.iOS
{
	[ImportBinding(typeof(SignatureBindingProvider))]
    [RegisterNavigation(DeviceKind.Tablet)]
	public partial class MainViewController_iPad : UIViewController<SignatureViewModel>
    {
		private UISignaturePadView _signaturePad;
		public UISignaturePadView SignaturePad
		{
			get {
				if (_signaturePad == null)
					_signaturePad = new UISignaturePadView (this);
				return _signaturePad;
			}
		}
        #region Methods

        protected override void OnViewInitialized()
        {
            base.OnViewInitialized();
            this.Title = "Crosslight App";

			_signaturePad = new UISignaturePadView (this);
			this.View.AddSubview (SignaturePad);
			this.RegisterViewIdentifier("SignaturePad", SignaturePad);

			UIBarButtonItem updateButton = new UIBarButtonItem ();
			updateButton.Title = "Update";
			updateButton.Style = UIBarButtonItemStyle.Plain;
			this.NavigationItem.RightBarButtonItem = updateButton;
			this.RegisterViewIdentifier("UpdateButton", updateButton);
        }

		private void CaptureTouches(UIPanGestureRecognizer panRecognizer)
			{
			}

        #endregion
    }
}