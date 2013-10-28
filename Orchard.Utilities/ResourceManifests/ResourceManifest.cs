using Orchard.UI.Resources;

namespace Datwendo.ContentHelpers
{
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();
            manifest.DefineScript("FillProperties").SetDependencies("jQuery").SetUrl("fillproperties.js");
        }
    }
}
