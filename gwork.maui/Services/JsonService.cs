using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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
