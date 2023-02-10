using System.Windows;
using CoreWPF.Prism;

namespace ioList.Shared.Services
{
    public interface IScopedShellCreator
    {
        /// <summary>
        /// Creates a new shell window with a scoped region manager using the <see cref="RegionManagerAware"/> extensions
        /// or prism.
        /// </summary>
        /// <returns>A new <see cref="Window"/> representing an application shell.</returns>
        Window Create();

        /// <summary>
        /// Creates a new shell window with a scoped region manager using the <see cref="RegionManagerAware"/> extensions
        /// or prism. Also performs navigation of the specified view uri to the specified region name.
        /// </summary>
        /// <param name="region"></param>
        /// <param name="uri"></param>
        /// <returns></returns>
        Window Create(string region, string uri);
    }
}