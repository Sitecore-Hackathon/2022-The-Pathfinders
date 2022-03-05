using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Sitecore;
using Sitecore.Data;
using Sitecore.DependencyInjection;
using Sitecore.Services.GraphQL.Content;
using Sitecore.Services.GraphQL.Content.GraphTypes.FieldTypes;
using Sitecore.Services.GraphQL.Content.TemplateGeneration;
using Sitecore.Services.GraphQL.Schemas;
using Sitecore.Xml;

namespace Pathfinders.Foundation.CLI.Users.GraphQL.ContentProviders
{
    public class UserContentProvider : SchemaProviderBase
    {
        //Todo:  The above provider is meant for querying items.  We may need to update that for our purpose.

        private Database _database;
        private FieldType rootFieldType;

        protected List<XmlNode> QueriesInternal { get; } = new List<XmlNode>();
        public virtual Database Database => this._database ?? Context.Database;
        public override object CreateUserContext() => (object)new ContentSchemaContext(this.Database);
        public override IEnumerable<FieldType> CreateRootQueries() => this.QueriesInternal.Select<XmlNode, FieldType>(new Func<XmlNode, FieldType>(this.ParseRootFieldType));
        public virtual void AddQuery(XmlNode node) => this.QueriesInternal.Add(node);

        public FieldTypeToGraphQLTypeMapper FieldTypeMapping { get; set; }

        protected virtual FieldType ParseRootFieldType(XmlNode configNode)
        {
            string attribute1 = XmlUtil.GetAttribute("name", configNode);
            if (attribute1 == string.Empty)
                throw new InvalidOperationException("The root type definition was missing its 'name' attribute: " + configNode.OuterXml);
            string attribute2 = XmlUtil.GetAttribute("type", configNode);
            Type type = attribute2 != string.Empty ? Type.GetType(attribute2) : throw new InvalidOperationException("The root type definition was missing its 'type' attribute: " + configNode.OuterXml);
            if (type == (Type)null)
                throw new InvalidOperationException("Unable to find root field type " + attribute2 + ". Ensure it is a valid .NET assembly qualified type name. (Defined in " + configNode.OuterXml + ")");
            if (XmlUtil.GetAttribute("resolve", configNode) == string.Empty)
                rootFieldType = Activator.CreateInstance(type) as FieldType;
            else if (!(ActivatorUtilities.GetServiceOrCreateInstance(ServiceLocator.ServiceProvider, type) is FieldType rootFieldType))
                throw new InvalidOperationException("Root field type " + attribute2 + " was not constructable using dependency injection. This likely means at least one of its dependencies is not registered with the container.");
            if (rootFieldType == null)
                throw new InvalidOperationException("Root field type " + attribute2 + " did not inherit from FieldType. (Defined in " + configNode.OuterXml + ")");
            if (rootFieldType is IContentSchemaRootFieldType schemaRootFieldType)
                schemaRootFieldType.Database = this.Database;
            rootFieldType.Name = attribute1;
            return rootFieldType;
        }
    }
}