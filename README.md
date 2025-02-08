# RobotSpiders
Repository containing source code for the RobotSpiders project

## Outline

This project has been implemented as a WEB API.

The spider's Start method is made accessible through two separate POST endpoints.

The first of which requires a body containing text in the INPUT format, for example:

```
7 15
2 4 Left
FLFLFRFFLF
```

And returns a string, for example "3 1 Right"

The second requires a structured JSON body, for example:

```json
{
    "Wall": [7, 15],
    "SpiderPosition": [2, 4, "left"],
    "Directions": "FLFLFRFFLF"
}
```

and returns an object representing the details of the spider's end position, for example

```json
{
    "x": 3,
    "y": 1,
    "orientation": "Right"
}
```

The behaviour of both endpoints is identical, only their usage differs.

## Usage

Please run the solution through Visual Studio, with https.

Example curl request for the string endpoint:

```sh
curl --location 'https://localhost:7174/RobotSpiders/Start' \
--header 'Content-Type: text/plain' \
--data '7 15
2 4 left
FLFLFRFFLF'
```

Example curl request for the json endpoint:

```sh
curl --location 'https://localhost:7174/RobotSpiders/JSONStart' \
--header 'Content-Type: application/json' \
--data '{
    "Wall": [7, 15],
    "SpiderPosition": [2, 4, "left"],
    "Directions": "FLFLFRFFLF"
}'
```