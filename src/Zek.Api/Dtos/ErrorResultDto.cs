using System.Collections.Generic;

namespace Zek.Api.Dtos
{
    public class ErrorResultDto
    {
        public string Error { get; set; }
        public string StackTrace { get; set; }
        public IReadOnlyDictionary<string, string[]> Issues { get; set; }
    }
}