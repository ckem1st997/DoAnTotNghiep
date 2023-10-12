using System.Runtime.CompilerServices;

namespace Share.Base.Core.Infrastructure
{
    /// <summary>
    /// Provides access to the singleton instance of the Nop engine.
    /// </summary>
    public class EngineContext
    {
        #region Methods

        /// <summary>
        /// Create a static instance of the Nop engine.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Create()
        {
            //create NopEngine as engine
            return Singleton<IEngine>.Instance ?? (Singleton<IEngine>.Instance = new BaseEngine());
        }

        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        public static IEngine EngineResolve
        {
            get
            {
                return EngineContext.Current;
            }
        }
        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton  engine used to access  services.
        /// </summary>
        public static IEngine Current
        {
            get
            {
                //if (Singleton<IEngine>.Instance == null)
                //{
                //    Create();
                //}
                return Singleton<IEngine>.Instance ?? Create();
            }
        }

        #endregion
    }
}
