Elegí el enfoque Code First de Entity Framework Core por una razón muy simple: el código ya estaba hecho. No tenía sentido empezar de cero con la base de datos si ya tenía los modelos (Libro.cs y Autor.cs) listos.

Esto hizo que la transición fuera mucho más fácil y rápida, ya que el programa usó mis clases para crear las tablas de la base de datos automáticamente.

Lo bueno y lo no tan bueno de este enfoque
Ventajas: Es super rápido para arrancar un proyecto nuevo cuando ya tenés los modelos. Tenés control total sobre el código y si cambiás algo en tus clases, las migraciones de Entity Framework Core se encargan de actualizar la base de datos sin perder datos.

Desventajas: A veces, manejar las migraciones puede volverse un poco complicado, especialmente en proyectos muy grandes. Hay que saber cómo funcionan para no cometer errores.
