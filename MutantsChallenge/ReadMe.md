# Mutant Scanner

Desarrollo de challenge ML "Mutantes"

## Descripción

El desarrollo se realizó en NetCore 2.1. La misma es una solución Api Rest con el core de la lógica de resolución del problema planteado en la clase estática /Core/Scan.cs
La lógica recorre una a una las bases nitrogenadas de la secuencia de ADN y en cada una evalúa los caminos posibles de identificación de una secuencia de 4 bases iguales.
Utiliza una función recursiva IsSec a la cual se le pasa la base actual, sus coordenadas y la dirección de coordenadas en las que se está evaluando encontrar la secuencia repetitiva.
Si alguna condición no se cumple continúa con la siguiente base nitrogenada del ADN.
Si encuentra que se cumplen todas las condiciones de cadena repetitiva dos veces toda la cadena de ADN se deja de avaluar y se retorna un resultado positivo.

La WebApi contiene dos endpoints a los que se puede acceder por medio de las siguientes rutas (levantando el entorno en visual studio)
* https://localhost:5000/api/Scan/Mutant [POST]
* https://localhost:5000/api/Scan/Stats [GET]
Para el primero la secuencia de ADN se debe pasar por body, se dejan debajo cuatro secuencias que se corren como tests automáticos en el starUp de la misma y que se pueden utilizar para realizar el request desde Postman (herramienta que se utilizó para testeo)
* {"dna":["ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG"]}

* {"dna":["AAAAGA", "AAGTGC", "ATATTT", "AGACGG", "GCGTCA", "TCACTG"]}
* {"dna":["ATGCGA", "CAGTGC", "TTATTT", "AGTCGG", "GTGTCA", "TCACTG"]}
* {"dna":["ATGCGA", "CAGTGC", "TTATTT", "AGACGG", "GCGTCA", "TCACTG"]}

Cada llamada al endpoint /mutant/ almacena en memoria (se puede migrar a una base SQL) el resultado para la secuencia consultada (sólo una vez), hidratando así la base de datos que consume el endpoint /stats/
El segundo enpoint mencionado devuelve de la base de datos el total de resultados obtenidos para cada uno de los tipos de ADN y un ratio.
### Se incluyen imágenes de postman para mutante - humano - stats
