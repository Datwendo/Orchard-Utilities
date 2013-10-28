using System.Web.Mvc;
using Orchard.Localization;
using Orchard;
using Orchard.Mvc;
using Orchard.DisplayManagement;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.ContentManagement.MetaData;
using System.Collections.Generic;
using System.Linq;
using System;
using Orchard.UI.Admin;
using Orchard.Themes;
using Orchard.Core.Common.Models;
using System.Reflection;
using Orchard.ContentManagement.MetaData.Models;
using Datwendo.ContentHelpers.Services;


namespace Datwendo.ContentHelpers.Controllers
{
    [Admin]
    [OrchardFeature("Datwendo.ContentHelpers")]
    public class ContentHelpersController : Controller, IUpdateModel  {
        private readonly IContentHelpersService _contentHelpersService;

        public ContentHelpersController(IContentHelpersService contentHelpersService) 
        {
            T                           = NullLocalizer.Instance;
            _contentHelpersService      = contentHelpersService;
        }

        public Localizer T { get; set; }

        

        [HttpPost]
        [RequireHttps]
        [Themed(false)]
        public JsonResult GetProperties(string partName)
        {
            IEnumerable<string> lst         = _contentHelpersService.GetProperties(partName);
            IEnumerable<dynamic> lstFields  = _contentHelpersService.GetFields(partName);
            return Json(new { Properties = lst, Fields = lstFields }, JsonRequestBehavior.AllowGet);
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}
