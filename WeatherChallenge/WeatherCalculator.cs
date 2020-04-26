namespace WetherAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public static class WeatherCalculator
    {
        public static string GetNextTenYearsReport()
        {
            // evalúo de acá a 10 años
            var lluvia = 0;
            var sequia = 0;
            var condOptima = 0;
            var lluviaMaxima = new List<DateTime>();

            // se asume dos años biciestos en el período
            for (int i = 0; i <= (360 + 2) * 10; i++)
            {
                switch (GetWeather(i))
                {
                    // se asume los días de lluvia máxima también deben ser contados como días de lluvia
                    case "lluvia":
                        lluvia++;
                        break;
                    case "lluviaMaxima":
                        lluvia++;
                        lluviaMaxima.Add(DateTime.Now.AddDays(i));
                        break;
                    case "sequia":
                        sequia++;
                        break;
                    case "optimo":
                        condOptima++;
                        break;
                }
            }

            return ImprimirValores(sequia, lluvia, lluviaMaxima, condOptima);
        }
        public static string GetWeather(int day)
        {
            var F = new coordinates();
            var B = new coordinates();
            var V = new coordinates();
            //obtengo las coordenadas para los tres planetas en el día actual
            GetCoordinates(day, out F, out B, out V);

            // check si los planetas se encuentran alineados
            if (Aligned(F, B, V))
            {
                // si están alineados, verificar si están alineados con el sol también
                if (AlignedWithSun(F, B, V))
                {
                    // si es así éste día es de sequía
                    return "sequia";
                }
                else
                {
                    // si no están alineados con el sol la condición es óptima
                    return "optimo";
                }
            }
            else
            {
                //no están alineados así que forman un triángulo
                if (TriangleIncludesSun(F, B, V))
                {
                    if (TriangleIsMax(F, B, V))
                    {
                        // si además el triángulo es máximo es día de lluvia máxima
                        //acumulo la fecha en la lista
                        return "lluviaMaxima";
                    }
                    else
                    {
                        return "lluvia";
                    }
                }
                else
                {
                    // el enunciado no determina que tipo de clima padece el sistema cuando el triángulo
                    // no incluye al sol, se asume que es un período climático normal
                    return "normal";
                }
            }
        }

        public static string ImprimirValores(int sequia, int lluvia, List<DateTime> lluviaMaxima, int condOptima)
        {
            var result = "¿Cuántos períodos de sequía habrá? Respuesta: " + sequia + Environment.NewLine;
            result += "¿Cuántos períodos de lluvia habrá y qué día será el pico máximo de lluvia? Respuesta: " +
                lluvia + " con sus picos en los días: " + Environment.NewLine;
            foreach (var fecha in lluviaMaxima)
            {
                result += fecha.ToString() + Environment.NewLine;
            }
            result += "¿Cuántos períodos de condiciones óptimas de presión y temperatura habrá? Respuesta: " +
                condOptima + Environment.NewLine;

            return result;
        }

        public static bool TriangleIsMax(coordinates F, coordinates B, coordinates V)
        {
            var ladoA = Math.Atan(System.Math.Abs(F.X - B.X) / System.Math.Abs(F.Y - B.Y));
            var ladoB = Math.Atan(System.Math.Abs(B.X - V.X) / System.Math.Abs(B.Y - V.Y));
            var ladoC = Math.Atan(System.Math.Abs(V.X - F.X) / System.Math.Abs(V.Y - F.Y));
            return ladoA == ladoB && ladoB == ladoC;
        }

        public static int Area(coordinates a, coordinates b, coordinates c)
        {
            // devuelvo el área en entero para hacer menos precisa la comparación y obtener
            // comparaciones de áreas similares como idénticas sino la inclusión del sol por comparación
            // de áreas me va a dar siempre falso por redondeos de operaciones
            return (int)Math.Abs((a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y)) / 2);
        }
        
        public static bool TriangleIncludesSun(coordinates F, coordinates B, coordinates V)
        {
            // el eje central está incluído en el triángulo la suma de las áreas de los triángulos con 
            // el centro es igual al área del triángulo que forman los puntos
            var centerCoords = new coordinates
            {
                X = 0,
                Y = 0
            };
            var A = Area(F, B, V);
            var a1 = Area(centerCoords, B, V);
            var a2 = Area(F, centerCoords, V);
            var a3 = Area(F, B, centerCoords);
            return A == (a1 + a2 + a3);
        }

        public static double AreaOfTriangle(coordinates a, coordinates b, coordinates c)
        {
            return ((a.X - b.X) * (b.Y - c.Y) - (b.X - c.X) * (a.Y - b.Y) / 2);
        }

        public static bool AlignedWithSun(coordinates F, coordinates B, coordinates V)
        {
            var oo = new coordinates
            {
                X = 0,
                Y = 0
            };
            return Aligned(oo, B, V) && Aligned(F, oo, V) && Aligned(F, B, oo);
        }

        public static bool Aligned(coordinates F, coordinates B, coordinates V)
        {
            return AreaOfTriangle(F, B, V) == 0;
        }

        public static void GetCoordinates(int day, out coordinates F, out coordinates B, out coordinates V)
        {
            const int rF = 500;
            const int rB = 2000;
            const int rV = 1000;
            int aF;
            int aB;
            int aV;
            Math.DivRem(day * 1, 360, out aF);
            Math.DivRem(day * 3, 360, out aB);
            Math.DivRem(day * 5, 360, out aV);

            F = GetCoordinatesFor(aF, rF);
            B = GetCoordinatesFor(aB, rB);
            V = GetCoordinatesFor(aV, rV);
        }

        public static coordinates GetCoordinatesFor(int angle, int radius)
        {
            var result = new coordinates
            {
                X = Math.Cos(angle) * radius,
                Y = Math.Sin(angle) * radius
            };
            return result;
        }

        public class coordinates
        {
            public double X { get; set; }
            public double Y { get; set; }
        }


        // ------------------ Fin desarrollo
        // Bonus----------------------------------------------------¬
        public class dayData
        {
            public int dia;
            public string clima;
        }
    }
}