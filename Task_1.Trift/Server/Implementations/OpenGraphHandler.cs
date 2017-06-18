using System.Net;
using OpenGraph;

namespace Server.Implementations
{
    public class OpenGraphHandler : OpenGraphService.Iface
    {

        public OpenGraphMeta GetMeta(string url)
        {
            OpenGraph_Net.OpenGraph graph;

            try
            {
                graph = OpenGraph_Net.OpenGraph.ParseUrl(url);
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.NameResolutionFailure)
            {
                throw new NetException();
            }
            catch (WebException ex) when (ex.Message.Contains("404"))
            {
                throw new NotFoundException();
            }
            catch (WebException ex)
            {
                throw new UnknownException();
            }

            if (string.IsNullOrEmpty(graph.Title))
            {
                throw new MetaNotFoundException();
            }

            return new OpenGraphMeta
            {   
                Title = graph.Title,
                Type = graph.Type,
                Image = graph.Image?.AbsoluteUri ?? "",
                Url = graph.Url?.AbsoluteUri ?? ""
            };
        }
    }
}
