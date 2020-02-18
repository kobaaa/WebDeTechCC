using System.Collections.Generic;
using System.Net.Http.Headers;
using WebDeTechCC.API.Models;

namespace WebDeTechCC.API.Utils
{
    public static class ContentAnalyzer
    {
        public static List<TechDto> AnalyzeHeaders(HttpResponseHeaders headers)
        {
            var foundTechs = new List<TechDto>();
            if (headers == null) return foundTechs;

            var serverHeader = headers.Server?.ToString();
            if (serverHeader != null && serverHeader.ToLower().Contains("nginx"))
            {
                foundTechs.Add(new TechDto()
                {
                    Name = "NGINX",
                    Description = "High Performance Load Balancer, Web Server, & Reverse Proxy"
                });
            }
            
            return foundTechs;
        }
        
        public static List<string> AnalyzeCookies(string cookies)
        {
            var foundTechs = new List<string>();
            return foundTechs;
        }
        
        public static List<string> AnalyzeHtml(string html)
        {
            var foundTechs = new List<string>();
            return foundTechs;
        }
        
        public static List<string> AnalyzeScripts(string scripts)
        {
            var foundTechs = new List<string>();
            return foundTechs;
        }
        
        public static List<string> AnalyzeUrl(string url)
        {
            var foundTechs = new List<string>();
            return foundTechs;
        }
    }
}