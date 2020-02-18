using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebDeTechCC.API.Models;

namespace WebDeTechCC.API.Utils
{
    public static class UriHelper
    {
        public static HostInfoDto StringToAbsoluteUri(string uriString)
        {
            var hostModel = new HostInfoDto();

            if (!uriString.Contains(Uri.SchemeDelimiter))
                uriString = Uri.UriSchemeHttp + Uri.SchemeDelimiter + uriString;

            if (Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out var resultUri))
            {
                try
                {
                    IPAddress[] addressesOfHost = Dns.GetHostAddresses(resultUri.DnsSafeHost);
                    if (addressesOfHost.Length > 0)
                    {
                        hostModel.Uri = resultUri.ToString();
                        hostModel.SafeHost = resultUri.DnsSafeHost;
                        hostModel.Ips = addressesOfHost
                            .Select(a => a.ToString())
                            .ToList();
                        return hostModel;
                    }
                }
                catch (System.Net.Sockets.SocketException)
                {
                    return null;
                }
            }

            return null;
        }

        public static async Task<HostInfoDto> StringToAbsoluteUriAsync(string uriString)
        {
            var hostModel = new HostInfoDto();

            if (!uriString.Contains(Uri.SchemeDelimiter))
                uriString = Uri.UriSchemeHttp + Uri.SchemeDelimiter + uriString;

            if (Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out var resultUri))
            {
                try
                {
                    IPAddress[] addressesOfHost = await Dns.GetHostAddressesAsync(resultUri.DnsSafeHost);
                    if (addressesOfHost.Length > 0)
                    {
                        hostModel.Uri = resultUri.ToString();
                        hostModel.SafeHost = resultUri.DnsSafeHost;
                        hostModel.Ips = addressesOfHost
                            .Select(a => a.ToString())
                            .ToList();
                        return hostModel;
                    }
                }
                catch (System.Net.Sockets.SocketException)
                {
                    return null;
                }
            }

            return null;
        }
    }
}