# REST API examples
## POST method
### Request:
```
Method: POST
URL: https://api.example.org/dotnetversions
Data: { "name": ".NET 6.0", "version": 6.0 }
```
### Response:
```
Status: 201 Created
Location: https://api.example.org/dotnetversions/18
Data: { "id": 18, "name": ".NET 6.0", "version": 6.0 }
```
## GET method
### Request:
```
Method: GET
URL: https://api.example.org/dotnetversions/10
```
### Response:
```
Status: 200 OK
Data: { "id": 10, "name": ".NET 5.0", "version": 5.0 }
```
### Request:
```
Method: GET
URL: https://api.example.org/dotnetversions?min-version=4&max-version=5
```
### Response:
```
Status: 200 OK
Data: [
{ "id": 7, "name": "net framework 4", "version": 4.0 },
{ "id": 8, "name": "net framework 4.7.2", "version": 4.7 },
{ "id": 9, "name": "net framework 4.8", "version": 4.8 },
{ "id": 10, "name": ".NET 5.0", "version": 5.0 }
]
```
## PUT method
### Request:
```
Method: PUT
URL: https://api.example.org/dotnetversions/9
Data: { "id": 9, "name": "net framework 4.8.2", "version": 4.8 },
```
### Response:
```
Status: 204 No Content
```
## DELETE method
### Request:
```
Method: DELETE
URL: https://api.example.org/dotnetversions/1
```
### Response:
```
Status: 204 No Content
```
