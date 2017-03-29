using System.Collections.Generic;

namespace Wilson.Web.Seed
{
    /// <summary>
    /// Non-generic class allowing properties to be copied from one instance
    /// to another existing instance of a potentially different type.
    /// </summary>
    public static class PropertyCopy
    {
        /// <summary>
        /// Copies all public, readable properties from the source object to the
        /// target. The target type does not have to have a parameterless constructor,
        /// as no new instance needs to be created.
        /// </summary>
        /// <remarks>Only the properties of the source and target types themselves
        /// are taken into account, regardless of the actual types of the arguments.</remarks>
        /// <typeparam name="TSource">Type of the source</typeparam>
        /// <typeparam name="TTarget">Type of the target</typeparam>
        /// <param name="source">Source to copy properties from</param>
        /// <param name="target">Target to copy properties to</param>
        public static void Copy<TSource, TTarget>(TSource source, TTarget target)
            where TSource : class
            where TTarget : class
        {
            PropertyCopier<TSource, TTarget>.Copy(source, target);
        }

        public static IEnumerable<TTarget> CopyCollection<TTarget, TSource>(IEnumerable<TSource> sourceCollection)
            where TTarget : class, new() where TSource : class
        {
            var entities = new List<TTarget>();
            foreach (var source in sourceCollection)
            {
                var target = new TTarget();
                Copy(source, target);
                entities.Add(target);
            };

            return entities;
        }
    }
}
