using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TaskApp.Api.Models
{
    public class TaskModelConverter : JsonConverter<TaskModel>
    {
        public override TaskModel ReadJson(JsonReader reader, Type objectType, TaskModel existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var jsonObject = JObject.Load(reader);
            var taskModel = new TaskModel();

            serializer.Populate(jsonObject.CreateReader(), taskModel);

            // Konvertieren Sie die StartDate von DateTime zu einem formatierten String
            taskModel.StartDate = jsonObject["StartDate"]?.ToObject<DateTime>().ToString("yyyy-MM-dd HH:mm:ss");

            return taskModel;
        }

        public override void WriteJson(JsonWriter writer, TaskModel value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}
