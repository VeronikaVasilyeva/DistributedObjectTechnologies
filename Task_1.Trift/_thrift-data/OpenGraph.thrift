namespace csharp OpenGraph
namespace js OpenGraph

typedef string url

struct OpenGraphMeta {
    1: string title,
    2: string type,
    3: string image,
    4: string url,
}

exception NetException {}
exception NotFoundException {}
exception UnknownException {}
exception MetaNotFoundException {}

service OpenGraphService {
    OpenGraphMeta GetMeta(1: url url) throws 
										(1: NetException netException, 
											2: NotFoundException notFoundException, 
											3: UnknownException unknownException, 
											4: MetaNotFoundException metaNotFoundException)
}