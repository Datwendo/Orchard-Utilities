using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.ContentManagement;
using System.Text;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Settings;
using System.Reflection;
using System.Dynamic;
using Microsoft.CSharp.RuntimeBinder;
using Orchard.ContentManagement.MetaData.Models;

namespace Datwendo.ContentHelpers.Services
{
    public interface IContentHelpersService : IDependency
    {
        Type GetPartType(string partName);
        IEnumerable<string> GetProperties(string partName);
        IEnumerable<dynamic> GetFields(string partName);
    }

    public class ContentHelpersService : IContentHelpersService
    {
        public IOrchardServices Services { get; set; }
        private readonly IContentDefinitionManager _contentDefinitionManager;


        public ContentHelpersService(IOrchardServices services, IContentDefinitionManager contentDefinitionManager)
        {
            Services                    = services;
            T                           = NullLocalizer.Instance;
            Logger                      = NullLogger.Instance;
            _contentDefinitionManager   = contentDefinitionManager;
        }

        public ILogger Logger { get; set; }

        public Localizer T { get; set; }

        public IEnumerable<string> GetProperties(string partName)
        {
            return GetPartType(partName).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance)
                .Select(p => new
                {
                    Name = p.Name,
                    IndexParameters = p.GetIndexParameters(),
                    Accessors = p.GetAccessors(false)
                })
                .Where(x => x.IndexParameters.Count() == 0) // must not be an indexer
                .Where(x => x.Accessors.Length != 1 || x.Accessors[0].ReturnType == typeof(void)) //must have get/set, or only set
                .Select(p => p.Name);
        }

        public Type GetPartType(string partName)
        {
            Type x = typeof(void);
            try
            {
                var lst = AppDomain.CurrentDomain.GetAssemblies();
                List<Assembly> badasm = new List<Assembly>();
                foreach (var ass in lst)
                {
                    try
                    {
                        var types = ass.GetTypes();
                    }
                    catch
                    { badasm.Add(ass); }
                }

                x = lst.Where(a => !badasm.Contains(a))
                                            .SelectMany(a => a.GetTypes())
                                            .Where(t => t.Name == partName)
                                            .FirstOrDefault();
            }
            catch { }
            return (x == null) ? typeof(void) : x;
        }

        public IEnumerable<dynamic> GetFields(string partName)
        {
            ContentPartDefinition ct = _contentDefinitionManager.GetPartDefinition(partName);
            return ct.Fields.Any() ? ct.Fields.Select(f => new { Name = f.Name, DisplayName = f.DisplayName }) : new[] { new { Name = string.Empty, DisplayName = "No Fields" } };
        }
    }
}