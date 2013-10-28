using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Orchard.ContentManagement.MetaData.Builders;
using Datwendo.ContentHelpers.Models;
using Orchard.Localization;
using System;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using Orchard.Environment.Extensions;

namespace Datwendo.ContentHelpers.Settings
{
    [OrchardFeature("Datwendo.ContentHelpers")]
    public class DummyAntiForgerySettings
    {

        public DummyAntiForgerySettings()
        {}
        public virtual bool InsertToken { get; set; }
        public void Build(ContentTypePartDefinitionBuilder builder) {
            builder.WithSetting("DummyAntiForgerySettings.InsertToken", InsertToken.ToString(CultureInfo.InvariantCulture));
        }
    }
}
