using SignaturePad.Infrastructure;
using Intersoft.Crosslight;

namespace SignaturePad.iOS.Infrastructure
{
    public sealed class AppInitializer : IApplicationInitializer
    {
        #region IApplicationInitializer Members

        public IApplicationService GetApplicationService(IApplicationContext context)
        {
            return new CrosslightAppAppService(context);
        }

        public void InitializeApplication(IApplicationHost appHost)
        {
        }

        public void InitializeComponents(IApplicationHost appHost)
        {
        }

        public void InitializeServices(IApplicationHost appHost)
        {
			BindingManager.AddBindingAdapter<UISignaturePadView, SignaturePadBindingAdapter> ();
        }

        #endregion
    }
}