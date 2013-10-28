using System;
using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;
using Orchard.Localization;
using Orchard.UI.Notify;
using Orchard.Utility.Extensions;
using Orchard.Core.Contents.Settings;

namespace Datwendo.ContentHelpers.Settings
{
    public class DummyAntiForgerySettingsHooks : ContentDefinitionEditorEventsBase
    {
        //private readonly INotifier _notifier;
        //private readonly IContentDefinitionManager _contentDefinitionManager;

        public DummyAntiForgerySettingsHooks(/*INotifier notifier, IContentDefinitionManager contentDefinitionManager*/)
        {
            T                           = NullLocalizer.Instance;
            //_contentDefinitionManager   = contentDefinitionManager;
            //_notifier                   = notifier;
        }

        public Localizer T { get; set; }

        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition)
        {
            if (definition.PartDefinition.Name != "DummyAntiForgeryPart")
                yield break;

            var settings = definition.Settings.GetModel<DummyAntiForgerySettings>();
            yield return DefinitionTemplate(settings);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel)
        {
            if (builder.Name != "DummyAntiForgeryPart")
                yield break;

            var settings        = new DummyAntiForgerySettings();
            if (updateModel.TryUpdateModel(settings, "DummyAntiForgerySettings", null, null))
            {
                settings.Build(builder);
            }

            yield return DefinitionTemplate(settings);
        }
    }
}
