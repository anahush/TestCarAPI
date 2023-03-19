using Newtonsoft.Json;
using System.ComponentModel;

namespace TestCarAPI.Models
{
    public class Link
    {
        public const string Get = @"GET";
        public const string Post = @"POST";

        public string? Href { get; set; }

        [JsonProperty(PropertyName = @"rel", NullValueHandling = NullValueHandling.Ignore)]
        public string[]? Relations { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        [DefaultValue(Get)]
        public string Method { get; set; } = Get;

        [JsonIgnore]
        public string RouteName { get; set; } = string.Empty;

        [JsonIgnore]
        public object? RouteValues { get; set; }

        public static Link To(string routeName, object? routeValues = null)
        {
            return new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = Get,
                Relations = null
            };
        }

        public static Link ToCollection(string routeName, object? routeValues = null)
        {
            return new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = Get,
                Relations = new[] { "collection" }
            };
        }

        public static Link ToForm(
            string routeName,
            object? routeValues = null,
            string method = Post,
            params string[] relations)
        {
            return new Link
            {
                RouteName = routeName,
                RouteValues = routeValues,
                Method = method,
                Relations = relations
            };
        }
    }
}
