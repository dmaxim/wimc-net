using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Mx.Library.Serialization
{
	public static class JsonExtensions
	{
		public static string ToJson<T>(this T obj, bool includeNull = true)
		{
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				Converters = new JsonConverter[] { new StringEnumConverter() },
				NullValueHandling = includeNull ? NullValueHandling.Include : NullValueHandling.Ignore,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			};

			return JsonConvert.SerializeObject(obj, settings);
		}


		public static T DeserializeJson<T>(this string jsonString, bool includeNull = true) where T : class
		{
			if (string.IsNullOrEmpty(jsonString) || jsonString.Equals("null", StringComparison.InvariantCultureIgnoreCase))
			{
				return default(T);
			}
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver(),
				Converters = new JsonConverter[] { new StringEnumConverter() },
				NullValueHandling = includeNull ? NullValueHandling.Include : NullValueHandling.Ignore
			};
			return JsonConvert.DeserializeObject<T>(jsonString, settings);
		}

	}
}
