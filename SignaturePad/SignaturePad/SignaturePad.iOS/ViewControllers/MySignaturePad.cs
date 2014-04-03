using System;
using SignaturePad;
using MonoTouch.UIKit;
using Intersoft.Crosslight;
using SignaturePad.Core;
using MonoTouch.Foundation;
using System.IO;


namespace SignaturePad.iOS
{
    public class MySignaturePad : SignaturePadView
    {
        private UISignaturePadView _ownerView;
        public UISignaturePadView OwnerView
        { 
            get
            {
                return _ownerView;
            }
            set
            {
                _ownerView = value;
            }
        }
        public MySignaturePad(UISignaturePadView owner)
        {
            _ownerView = owner;
        }

        public override void TouchesEnded(MonoTouch.Foundation.NSSet touches, MonoTouch.UIKit.UIEvent evt)
        {
            base.TouchesEnded(touches, evt);
            UIImage image = this.GetImage(false);
            this.OwnerView.SignImage = image;
        }


    }
}

