using System.Text.Json;

namespace gwork.maui.Services
{
    public static class JsonService
    {
        public static void WriteFile(object obj, string filename)//metoda do zapisu danych do pliku json
        {
            var jsonString = JsonSerializer.Serialize(obj);
            File.WriteAllText(filename, jsonString);
        }
    }
}