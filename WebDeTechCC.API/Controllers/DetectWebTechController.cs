using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebDeTechCC.API.Models;
using WebDeTechCC.API.Utils;

namespace WebDeTechCC.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class DetectWebTechController : ControllerBase
    {
        private readonly ILogger<DetectWebTechController> _logger;
        private readonly HttpClient _httpClient;

        public DetectWebTechController(ILogger<DetectWebTechController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Check a list of domain names for Nginx
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        /// 
        ///     [
        ///       "demo.nginx.com", "www.google.com", "www.wikipedia.org"
        ///     ]
        ///
        /// </remarks>
        /// <returns>A list of host-IPs pairs that run Nginx</returns>
        [HttpPost("nginx")]
        // public async Task<IEnumerable<HostModel>> CheckDomainNames(string[] items)
        public async Task<List<Dictionary<string, IEnumerable<string>>>> CheckDomainNamesForNginx(
            IEnumerable<string> domains)
        {
            var hostIpsList = new List<Dictionary<string, IEnumerable<string>>>();

            var tasks = domains
                .Select(async domain =>
                {
                    var host = await UriHelper.StringToAbsoluteUriAsync(domain);
                    if (host != null)
                    {
                        var response = await _httpClient.GetAsync(host.Uri);
                        var serverHeader = response.Headers?.Server;
                        if (serverHeader != null && serverHeader.ToString().ToLower().Contains("nginx"))
                            hostIpsList.Add(new Dictionary<string, IEnumerable<string>>
                                {{host.SafeHost, host.Ips}});
                    }
                });

            await Task.WhenAll(tasks);

            return hostIpsList;
        }

        /// <summary>
        /// Get tech stack for a list of domain names
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        /// 
        ///     [
        ///       "demo.nginx.com", "www.google.com", "www.wikipedia.org"
        ///     ]
        ///
        /// </remarks>
        /// <returns>A list of used technologies</returns>
        [HttpPost("fullscan")]
        public async Task<List<HostInfoDto>> CheckDomainNames(IEnumerable<string> domains)
        {
            var hostInfoDtos = new List<HostInfoDto>();

            var tasks = domains
                .Select(async domain =>
                {
                    var host = await UriHelper.StringToAbsoluteUriAsync(domain);
                    if (host != null)
                    {
                        var hostInfoDto = new HostInfoDto()
                        {
                            Ips = host.Ips,
                            SafeHost = host.SafeHost,
                            Uri = host.Uri
                        };
                        var response = await _httpClient.GetAsync(host.Uri);

                        // analyze cookies
                        // analyze url
                        // analyze html content
                        // analyze scripts

                        var headersTechs = ContentAnalyzer.AnalyzeHeaders(response.Headers);
                        hostInfoDto.Techs.AddRange(headersTechs);

                        hostInfoDtos.Add(hostInfoDto);
                    }
                });

            await Task.WhenAll(tasks);

            return hostInfoDtos;
        }
    }
}