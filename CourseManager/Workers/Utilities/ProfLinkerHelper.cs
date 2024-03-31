using System.Text;
using Newtonsoft.Json;

namespace Scrapers.Utilities;

public static class ProfLinkerHelper
{
    public record ProfessorInformation
    {
        public string ProfessorName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
    }

    public static async Task<ProfessorInformation> LinkProfessor(string professorInformation, string professorOptions)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:8000/linkprofessor/invoke");
        var jsonPayload = $@"{{
        ""input"": {{
            ""professorInformation"": ""{professorInformation}"",
            ""professorOptions"": ""{professorOptions}""
            }}
        }}";
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        Console.WriteLine(await response.Content.ReadAsStringAsync());
        
        var professorInformationJson = await response.Content.ReadAsStringAsync();
        
        return ParseProfessorInformation(professorInformationJson) ?? new ProfessorInformation();
    }
    
    public static ProfessorInformation? ParseProfessorInformation(string professorInformationJson)
    {
        var outerResponse = JsonConvert.DeserializeObject<OuterResponse>(professorInformationJson);
        string? contentJson = outerResponse?.Output?.Content ?? string.Empty;

        if (!string.IsNullOrEmpty(contentJson))
        {
            // Deserialize the 'content' JSON string to get the inner object
            var contentObject = JsonConvert.DeserializeObject<LinkedProfessor>(contentJson);

            return new ProfessorInformation
            {
                ProfessorName = contentObject?.ProfessorName ?? string.Empty,
                Email = contentObject?.EmailAddress ?? string.Empty
            };
        }

        return null;
    }
    
    public class OuterResponse
    {
        [JsonProperty("output")]
        public OutputContent Output { get; set; } = new OutputContent();
    }

    public class OutputContent
    {
        [JsonProperty("content")]
        public string Content { get; set; } = String.Empty;
    }

    public class LinkedProfessor
    {
        [JsonProperty("professorName")] public string ProfessorName { get; set; } = String.Empty;

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; } = String.Empty;
    }
}