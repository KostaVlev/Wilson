using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Wilson.Web.Configurations
{
    public static class ProperyResolverFactory
    {
        public static object Resolve<TSource, TDestination, TPropery>(TSource source, TDestination destination, string propertyName)
        {
            var obj = source.GetType().GetProperty(propertyName).GetValue(source, null);
            if (obj == null)
            {
                throw new ArgumentException(
                    string.Format("{0} doesn't have property with name {1}.", source.GetType().GetTypeInfo()), propertyName);
            }

            var property = JsonConvert.DeserializeObject<TPropery>(obj.ToString());

            if (property == null)
            {
                throw new ArgumentException(
                    string.Format(
                        "{0} property with name {1} isn't valid JSON that represent object of type {2}.", 
                        destination.GetType().GetTypeInfo(),
                        propertyName,
                        typeof(TPropery)));
            }

            PropertyInfo propertyInfo = destination.GetType().GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new ArgumentException(
                    string.Format("{0} doesn't have property with name {1}.", destination.GetType().GetTypeInfo(), propertyName));
            }

            if (!typeof(TPropery).IsAssignableFrom(propertyInfo.PropertyType))
            {
                throw new ArgumentException(
                    string.Format("{0} property {1} must be type of {2}.", destination.GetType().GetTypeInfo(), propertyName, typeof(TPropery)));
            }

            propertyInfo.SetValue(destination, property, null);

            return property;
        }
    }
}
