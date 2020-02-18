using System.Collections.Generic;

namespace WebDeTechCC.API.Models
{
    public class HostInfoDto
    {
        public string Uri { get; set; }
        public string SafeHost { get; set; }
        public List<string> Ips   { get; set; } = new List<string>();
        public List<TechDto> Techs  { get; set; } = new List<TechDto>();
    }

    public class TechDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}