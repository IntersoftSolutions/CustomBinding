using Intersoft.Crosslight.Containers;

namespace SignaturePad.Infrastructure
{
    public class Container
    {
        #region Fields

        private static IDependencyContainer _current;

        #endregion

        #region Properties

        public static IDependencyContainer Current
        {
            get
            {
                if (_current == null)
                    _current = new IocContainer();

                return _current;
            }
        }

        #endregion
    }
}