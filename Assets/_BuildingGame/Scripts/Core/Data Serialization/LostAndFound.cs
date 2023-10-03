
using System.Collections.Generic;

namespace BuildingGame.Data
{
    public static class LostAndFound
    {
        private static Dictionary<string, object> _data = new();

        public static bool Add(string id, object obj)
        {
            if (_data.ContainsKey(id))
                return false;

            _data.Add(id, obj);
            return true;
        }

        public static bool Remove(string id)
        {
            if (!_data.ContainsKey(id))
                return false;

            _data.Remove(id);
            return true;
        }

        public static object Retrieve(string id)
        {
            object data = _data.GetValueOrDefault(id);
            return data ?? default;
        }
    }
}