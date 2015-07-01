using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QsTech.Framework.Json;

namespace QsTech.Core.Interface.StateBar
{
    public class StateItemBuilder<TKey, TValue>
    {
        private List<StateItem<TValue>> m_Items = new List<StateItem<TValue>>();
        
        public List<StateItem<TValue>> Build(IDictionary<TKey, int> data, 
            Func<TKey, string> textFactory,
            Func<TKey, TValue> valueFactory, 
            params TKey[] keys)
        {
            foreach (TKey key in keys)
            {
                string text = textFactory(key);
                var item = new StateItem<TValue> { Text = text, Value = valueFactory(key) };
                if (data.ContainsKey(key))
                {
                    item.RecordCount = data[key];
                }
                m_Items.Add(item);
            }
            return m_Items;
        }
        
        public List<StateItem<TValue>> Build(IDictionary<TKey, int> data, Func<TKey, TValue> valueFactory, params TKey[] keys)
        {
            return Build(data, key => key.ToString(), valueFactory, keys);
        }

        public List<StateItem<TValue>> Build(IDictionary<TKey, int> data, params TKey[] keys)
        {
            return Build(data, key =>
            {
                var obj = (object)key;
                return (TValue)obj;
            }, keys);
        }

        public List<StateItem<TValue>> Build(IDictionary<TKey, int> data)
        {
            return Build(data, data.Keys.ToArray());
        }
    }

    public class StateItemBuilder<TKey> : StateItemBuilder<TKey, object>
    {
    }



    public static class StateItemBuilderExtensions
    {

        public static string ToJson<TKey, TValue>(this StateItemBuilder<TKey, TValue> builder, IDictionary<TKey, int> data)
        {
            var jsonData = builder.Build(data);
            return jsonData.ToJsonString();
        }

        public static string ToJson<TKey, TValue>(this StateItemBuilder<TKey, TValue> builder, IDictionary<TKey, int> data, params TKey[] keys)
        {
            var jsonData = builder.Build(data, keys);
            return jsonData.ToJsonString();
        }

        public static string ToJson<TKey, TValue>(this StateItemBuilder<TKey, TValue> builder, IDictionary<TKey, int> data, Func<TKey, TValue> valueFactory, params TKey[] keys)
        {
            var jsonData = builder.Build(data, valueFactory, keys);
            return jsonData.ToJsonString();
        }

        public static string ToJson<TKey, TValue>(this StateItemBuilder<TKey, TValue> builder, IDictionary<TKey, int> data, Func<TKey, string> textFactory, Func<TKey, TValue> valueFactory, params TKey[] keys)
        {
            var jsonData = builder.Build(data, textFactory, valueFactory, keys);
            return jsonData.ToJsonString();
        }

        public static string ToJson<TKey, TValue>(this StateItemBuilder<TKey, TValue> builder, IDictionary<TKey, int> data, Func<TKey, string> textFactory, Func<TKey, TValue> valueFactory)
        {
            var keys = data.Keys.ToArray();
            return builder.ToJson(data, textFactory, valueFactory, keys);
        }

        /// <summary>
        /// 保留了原有对 System.Object 类型的构建方法。
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="builder"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [Obsolete]
        public static List<StateItem> OldBuild<TKey>(this StateItemBuilder<TKey> builder, IDictionary<TKey, int> data)
        {
            var list = builder.Build(data);
            Converter<StateItem<object>, StateItem> converter = gsi =>
            {
                return new StateItem
                {
                    Text = gsi.Text,
                    Value = gsi.Value,
                    RecordCount = gsi.RecordCount,
                    Selected = gsi.Selected
                };
            };
            List<StateItem> items = new List<StateItem>(list.Count);
            foreach (var gsi in list)
            {
                items.Add(converter(gsi));
            }
            return items;
        }
    }
}
