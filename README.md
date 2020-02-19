# WebDeTechCC
![image](/assets/webdetech-endpoints.png)

Two endpoints receiving a list of domain names:

1. Nginx detection service (api/DetectWebTech/nginx)
*Naive approach for analyzing response Server header*

2. Generalized version (api/DetectWebTech/fullscan)
*The main idea is to utilize all the possible info that could be received from server (headers, cookies, markdown, scripts, etc) to compare with up-to-date library of server technology signatures (differential characteristics)*
*WIP.., not fully implemented*

Both endpoint responses contain IPs associated with hostnames.


## UP & RUNNING

```sh
docker build -t web-tech-detect .
```

```sh
docker run -p 8080:80 --name webdetect web-tech-detect 
```

### SWAGGER UI 
http://localhost:8080/index.html

### USAGE
curl -X POST "http://localhost:8080/api/DetectWebTech/nginx" -H  "accept: application/json" -H  "Content-Type: application/json" -d "[\"www.nginx.com\",\"www.google.com\"]"


## ROADMAP 

### GENERALIZED VER.
- [ ] regexp for advanced comparison
- [ ] detection confidence level

### TODO
- [ ] advanced uri validation logic
- [ ] support for IDN (Internationalized Domain Names, punycode)
- [ ] cache
- [ ] logging
- [ ] auth
- [ ] tests
- [ ] monitoring and health-check


## SCALING IDEAS

- elaborate caching policy
- API gateway to balance requests ( + possible static assets like tech logos/icons for generalized version)
- decouple independent functionality (IP lookup, get-requests, url-analyzers, etc) to microservices/lambdas
- batch processing to handle requests with huge payload
- more advanced: consider service mesh (abstract away infrastructure, reduce vendor-lock, utilize on-premise resources, etc)
