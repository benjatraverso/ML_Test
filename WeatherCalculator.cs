using System;

namespace WetherAPI
{
    public static class WeatherCalculator
    {
        public class MaxRain
        {
            public int Day { get; set; }
            public double Perim { get; set; }

            public MaxRain()
            {
                Day = 0;
                Perim = 0;
            }
        }
        
        public static string GetNextTenYearsReport()
        {
            // evalúo de acá a 10 años
            var lluvia = 0;
            var sequia = 0;
            var condOptima = 0;
            var DiaLluviaMaxima = new MaxRain();

            // se asume dos años biciestos en el período
            for (int i = 0; i <= (360 + 2) * 10; i++)
            {
                switch (GetWeather(i, ref DiaLluviaMaxima))
                {
                    // se asume los días de lluvia máxima también deben ser contados como días de lluvia
                    case "lluvia":
                        lluvia++;
                        break;
                    case "sequia":
                        sequia++;
                        break;
                    case "optimo":
                        condOptima++;
                        break;
                }
            }

            return ImprimirValores(sequia, lluvia, DiaLluviaMaxima.Day, condOptima);
        }

        public static string GetWeather(int day, ref MaxRain max)
        {
            var F = new Coords();
            var B = new Coords();
            var V = new Coords();
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
                    var per = Perimetro(F, B, V);
                    if (max.Perim < per)
                    {
                        max.Perim = per;
                        max.Day = day;
                    }

                    return "lluvia";
                }
                else
                {
                    // el enunciado no determina que tipo de clima padece el sistema cuando el triángulo
                    // no incluye al sol, se asume que es un período climático normal
                    return "normal";
                }
            }
        }

        public static string ImprimirValores(int sequia, int lluvia, int lluviaMaxima, int condOptima)
        {
            var result = "¿Cuántos períodos de sequía habrá? Respuesta: " + sequia + Environment.NewLine;
            result += "¿Cuántos períodos de lluvia habrá y qué día será el pico máximo de lluvia? Respuesta: " +
                lluvia + " con su pico en el días " + lluviaMaxima + ": " + DateTime.Now.AddDays(lluviaMaxima) + Environment.NewLine;
            result += "¿Cuántos períodos de condiciones óptimas de presión y temperatura habrá? Respuesta: " +
                condOptima + Environment.NewLine;

            return result;
        }

        public static double Perimetro(Coords F, Coords B, Coords V)
        {
            // es máximo si todos sus lados son iguales
            var sa = GetDistance(F, B);
            var sb = GetDistance(B, V);
            var sc = GetDistance(V, F);
            return sa + sb + sc;
        }

        public static double GetDistance(Coords a, Coords b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }

        public static double Area(Coords a, Coords b, Coords c)
        {
            // devuelvo el área en entero para hacer menos precisa la comparación y obtener
            // comparaciones de áreas similares como idénticas sino la inclusión del sol por comparación
            // de áreas me va a dar siempre falso por redondeos de operaciones
            return Math.Abs((a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y)) / 2);
        }

        public static bool TriangleIncludesSun(Coords F, Coords B, Coords V)
        {
            // el eje central está incluído en el triángulo la suma de las áreas de los triángulos con 
            // el centro es igual al área del triángulo que forman los puntos
            var centerCoords = new Coords
            {
                X = 0,
                Y = 0
            };
            var A = Area(F, B, V);
            var a1 = Area(centerCoords, B, V);
            var a2 = Area(F, centerCoords, V);
            var a3 = Area(F, B, centerCoords);
            return Math.Round(A, 2) == Math.Round(a1 + a2 + a3, 2);
        }

        public static bool AlignedWithSun(Coords F, Coords B, Coords V)
        {
            var o = new Coords
            {
                X = 0,
                Y = 0
            };
            return Aligned(o, B, V) && Aligned(F, o, V) && Aligned(F, B, o);
        }

        public static bool Aligned(Coords F, Coords B, Coords V)
        {
            return Math.Round(Area(F, B, V)) == 0;
        }

        public static void GetCoordinates(int day, out Coords F, out Coords B, out Coords V)
        {
            const int rF = 500;
            const int rB = 2000;
            const int rV = 1000;

            // evalúo a que ángulo con respecto a cero se trata si ya excedí los 360
            Math.DivRem(day * 1, 360, out int aF);
            Math.DivRem(day * 3, 360, out int aB);
            Math.DivRem(day * 5, 360, out int aV);

            // obtengo las coordenadas para cada planeta
            F = GetCoordinatesFor(aF, rF);
            B = GetCoordinatesFor(aB, rB);
            V = GetCoordinatesFor(aV, rV);
        }

        public static Coords GetCoordinatesFor(int angle, int radius)
        {
            var result = new Coords
            {
                X = Math.Cos(angle * Math.PI / 180) * radius,
                Y = Math.Sin(angle * Math.PI / 180) * radius
            };
            return result;
        }

        public class Coords
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