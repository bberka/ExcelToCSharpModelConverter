namespace XSharp.Lib;

public static class XSerializer
{
  public static string SerializeJson(object obj) {
    return JsonConvert.SerializeObject(obj, GetJsonSerializerSettings());
  }

  public static T? DeserializeJson<T>(string json) {
    return JsonConvert.DeserializeObject<T>(json, GetJsonSerializerSettings());
  }

  private static JsonSerializerSettings GetJsonSerializerSettings() {
    var serializerOption = new JsonSerializerSettings();
    serializerOption.Formatting = Formatting.Indented;
    serializerOption.Converters.Add(new StringEnumConverter());
    serializerOption.NullValueHandling = NullValueHandling.Include;
    serializerOption.DefaultValueHandling = DefaultValueHandling.Include;
    serializerOption.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    serializerOption.PreserveReferencesHandling = PreserveReferencesHandling.None;
    serializerOption.TypeNameHandling = TypeNameHandling.None;
    return serializerOption;
  }
}