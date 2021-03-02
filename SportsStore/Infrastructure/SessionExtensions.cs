using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure
{
    public static class SessionExtensions
    {
        /// <summary>
        /// Serializes object to JSON because session state feature of ASP.NET Core stores
        /// only int, string and byte[] values. Since we need to store Cart object, need to
        /// define extension methods to the ISession interface, which provides access to
        /// session state data to serialize Cart objects into JSON and convert them back.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetJson(this ISession session, string key, object value) {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonSerializer.Deserialize<T>(sessionData);
        }
    }
}
