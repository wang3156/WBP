using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CommLibrary.MVC
{
    /// <summary>
    /// 替换系统的JSON序列化器
    /// </summary>
    public class SetNewJsonSoft {
        public static void Set() {
            //重置Json序列化方式，改用JSON.Net
            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.
                OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new JsonNetValueProviderFactory());

            //重置系统的Binder，使Dictionary可以正常json
            ModelBinders.Binders.DefaultBinder = new JsonNetModelBinder();
        }
    }

    public class JsonNetValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext ctlContext)
        {
            //if (!controllerContext.HttpContext.Request.ContentType.
            //    StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
            //{
            //    return null;
            //}

            var reader = new StreamReader(ctlContext.HttpContext.Request.InputStream);
            reader.BaseStream.Position = 0;
            var json = reader.ReadToEnd()?.TrimStart(' ', '\r', '\n', '\t');
            if (string.IsNullOrEmpty(json))
                return null;

            var jtoken = json.StartsWith("[")
                ? JArray.Parse(json) as JContainer
                : JObject.Parse(json) as JContainer;
            return new JsonNetValueProvider(jtoken);
        }
    }

    public class JsonNetValueProvider : IValueProvider
    {
        private JContainer _jValue;

        public JsonNetValueProvider(JContainer jval)
        {
            _jValue = jval;
        }

        public bool ContainsPrefix(string prefix)
        {
            return true;
        }

        public ValueProviderResult GetValue(string key)
        {
            var jtoken = _jValue.SelectToken(key);
            if (jtoken == null)
            {
                jtoken = _jValue;
            }
            return new JsonNetValueProviderResult(jtoken, key, null);
        }
    }

    public class JsonNetValueProviderResult : ValueProviderResult
    {
        private JToken _jtoken;
        public JsonNetValueProviderResult(JToken valueRaw, string key, CultureInfo info)
        {
            _jtoken = valueRaw;
        }
        [System.Diagnostics.DebuggerHidden]
        public override object ConvertTo(Type type, CultureInfo culture)
        {
            return _jtoken?.ToObject(type);
        }
    }

    public class JsonNetModelBinder : DefaultModelBinder
    {
        [System.Diagnostics.DebuggerHidden]
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var provider = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (provider != null)
            {
                try
                {
                    return provider.ConvertTo(bindingContext.ModelType);
                }
                catch { }
            }
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}
