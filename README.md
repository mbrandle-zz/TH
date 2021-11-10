## Sobre El Api de TH Test
### Agregar Una Propiedad

> Para agregar una propiedad se requiere una petición **PUT** al siguiente URL {url asignado}/api/Property

Se requiere enviar un objeto de tipo JSON con los siguientes datos
```
{
    "title" : "Property 5",
    "address" : "Adress 5",
    "description": "Description 5",
    "created_at": "2012-04-23T18:25:43.511Z",
    "updated_at": "2012-04-23T18:25:43.511Z",
    "status": "Active"
}
```
La respuesta es un objeto de tipo JSON con los siguientes datos
```
{
    "id": 5,
    "title": "Property 5",
    "address": "Adress 5",
    "description": "Description 5",
    "created_at": "2012-04-23T18:25:43.511Z",
    "updated_at": "2012-04-23T18:25:43.511Z",
    "disabled_at": "0001-01-01T00:00:00",
    "status": "Active"
}
```
### Consultar Todas Las Propiedades
> Para consultar la lista de propiedad se requiere una petición **GET** al siguiente URL {url asignado}/api/Property

La respuesta es un objeto de tipo JSON con los siguientes datos
```
[
    {
        "id": 1,
        "title": "Property 1",
        "address": "Adress ",
        "description": "Description 1",
        "created_at": "2012-04-23T18:25:43.511",
        "updated_at": "2012-04-23T18:25:43.511",
        "disabled_at": "0001-01-01T00:00:00",
        "status": "Active"
    },
    {
        "id": 2,
        "title": "Property 1",
        "address": "Adress ",
        "description": "Description 1",
        "created_at": "2012-04-23T18:25:43.511",
        "updated_at": "2012-04-23T18:25:43.511",
        "disabled_at": "0001-01-01T00:00:00",
        "status": "Active"
    },
]
```
### Consultar Una Propiedades Por Id
> Para consultar una propiedad se requiere una petición **GET** al siguiente URL {url asignado}/api/Property/{Id}

La respuesta es un objeto de tipo JSON con los siguientes datos
```
[
    {
        "id": 2,
        "title": "Property 1",
        "address": "Adress ",
        "description": "Description 1",
        "created_at": "2012-04-23T18:25:43.511",
        "updated_at": "2012-04-23T18:25:43.511",
        "disabled_at": "0001-01-01T00:00:00",
        "status": "Active"
    },
]
```
### Desactivar Una Propiedad
> Para desactivar una propiedad se requiere hacer una petición **DELETE** al siguiente URL {url asignado}/api/Property/{Id}

La respuesta es un HTTPResponse con código **202 Accepted**

### Agregar Una Actividad

> Para agregar una actividad se requiere una petición **PUT** al siguiente URL {url asignado}/api/Activity

Se requiere enviar un objeto de tipo JSON con los siguientes datos.
```
{
    "property_id" : 1,
    "schedule" : "2021-11-13T10:00:00.001Z",
    "title" : "Activity 4",
    "created_at": "2021-11-01T10:00:00.001Z",
    "updated_at": "2021-11-01T10:00:00.001Z",
    "status": "Active"
}
```
La respuesta es un objeto de tipo JSON con los siguientes datos.
```
{
    "id": 5,
    "property_id": 1,
    "property": {
        "id": 1,
        "title": "Property 1",
        "address": "Adress ",
        "description": "Description 1",
        "created_at": "2012-04-23T18:25:43.511",
        "updated_at": "2012-04-23T18:25:43.511",
        "disabled_at": "0001-01-01T00:00:00",
        "status": "Active"
    },
    "schedule": "2021-11-20T10:00:00.001Z",
    "title": "Activity 50",
    "created_at": "2021-11-01T10:00:00.001Z",
    "updated_at": "2021-11-01T10:00:00.001Z",
    "status": "Active"
}
```
>Al agregar una actividad debes tener las siguientes consideraciones.
>- Se requiere de una propiedad valida para agregar una actividad.
>- No se pueden crear actividades si una Propiedad está desactivada.
>- No se pueden crear actividades en la misma fecha y hora (para la misma propiedad), tomando en cuenta que cada actividad debe durar máximo una hora.

### Reagendar una actividad  
> Para reagendar una actividad se requiere una petición **PATCH** al siguiente URL {url asignado}/api/Activity/{Id}?newSchedule={nueva fecha de actividad}

La respuesta es un HTTPResponse con código **202 Accepted** y la siguiente información
```
Schedule Updated ID: 3
```
>Al agregar una actividad debes tener las siguientes consideraciones.
>- No se pueden re-agendar actividades canceladas.
>- No se pueden reagendar actividades en la misma fecha y hora (para la misma propiedad), tomando en cuenta que cada actividad debe durar máximo una hora.

### Cancelar Actividad
> Para cancelar una activida se requiere una petición **DELETE** al siguiente URL {url asignado}/api/Activity/{Id}

La respuesta es un HTTPResponse con código **200 OK** y la siguiente información
```
 Schedule Cancel ID: 5
```
### Consultar Todas Las Actividades
>Para consultar todas las actividades se requiere una petición **GET** al siguiente URL {url asignado}/api/Activity

La respuesta es un objeto de tipo JSON con los siguientes datos
```
[
    {
        "id": 1,
        "schedule": "2021-11-10T10:00:00.001",
        "title": "Activity 1",
        "created_at": "2021-11-01T10:00:00.001",
        "status": "Active",
        "condition": "Atrasada",
        "property": {
            "id": 1,
            "title": "Property 1",
            "address": "Adress "
        },
        "survey": "https://localhost:44327/api/Survey/1"
    },
    {
        "id": 2,
        "schedule": "2021-11-11T10:00:00.001",
        "title": "Activity 2",
        "created_at": "2021-11-01T10:00:00.001",
        "status": "Active",
        "condition": "Pendiente a realizar",
        "property": {
            "id": 2,
            "title": "Property 1",
            "address": "Adress "
        },
        "survey": "https://localhost:44327/api/Survey/2"
    },
]
```
