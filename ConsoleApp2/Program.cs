// See https://aka.ms/new-console-template for more information
using ConsoleApp2;
using System.Drawing;
using System.Xml.Serialization;

Console.WriteLine("Hello, World!");
double elevation_function(int x, int y)
{
    var res = 1 / (2 + Math.Sin(2 * Math.Sqrt(x * x + y * y))) * (0.75 + 0.5 * Math.Sin(x * 2));
    return res;
}

Dictionary<(int x, int y), double> elevation_data = new Dictionary<(int x, int y), double>();

int SPACING = 1;

int WIDTH = 100;
int HEIGHT = 100;

for (int x = 0; x < WIDTH; x += SPACING)
{
    for (int y = 0; y < HEIGHT; y += SPACING)
    {
        elevation_data.Add((x, y), elevation_function(x, y));
    }
}
Triangle triangle = new Triangle();
//точките на ъглите на триъгълниците
//List<Tuple<(int,int), (int, int), (int,int)>> trianglesEdgesPounts = new List<Tuple<(int,int),(int,int),(int,int)>>();

List<Triangle> triangles = new List<Triangle>();

for (int x = 0; x < WIDTH - 1; x += SPACING)
{
    for (int y = 0; y < HEIGHT - 1; y += SPACING)
    {
        Triangle triangle1 = new Triangle((x, y), (x + SPACING, y), (x, y + SPACING));
        triangles.Add(triangle1);
        Triangle triangle2 = new Triangle((x + SPACING, y), (x, y + SPACING), (x + SPACING, y + SPACING));
        triangles.Add(triangle2);
    }
}

var threshold = 0.5;

List<Edge> contour_segments = new List<Edge>();

List<string> below = new List<string>();
List<string> above = new List<string>();

List<string> minority = new List<string>();
List<string> majority = new List<string>();

foreach (var item in triangles)
{
    //v мисля че е веkтро с точка X и Y което е страната на триъгилника

    // преверявам всяка една страна какви са нейните размери 

    //side A => B
    if (Calculate(item._v1x, item._v1y, item._v2x, item._v2y) < threshold)
    {
        //запазваме кординтите на двете точки които образуват страна на триъгинлника
        var sideX1 = item._v1x.ToString();
        var sideY1 = item._v1y.ToString();
        var sideX2 = item._v2x.ToString();
        var sideY2 = item._v2y.ToString();
        var coordinates = $"{sideX1} {sideY1} {sideX2} {sideY2}";
        below.Add(coordinates);

    }

    //side B => C
    if (Calculate(item._v2x, item._v2y, item._v3x, item._v3y) < threshold)
    {
        //запазваме кординтите на двете точки които образуват страна на триъгинлника
        var sideX1 = item._v2x.ToString();
        var sideY1 = item._v2y.ToString();
        var sideX2 = item._v3x.ToString();
        var sideY2 = item._v3y.ToString();
        var coordinates = $"{sideX1} {sideY1} {sideX2} {sideY2}";
        below.Add(coordinates);
    }
    // C => A
    if (Calculate(item._v3x, item._v3y, item._v1x, item._v1y) < threshold)
    {
        //запазваме кординтите на двете точки които образуват страна на триъгинлника
        var sideX1 = item._v3x.ToString();
        var sideY1 = item._v3y.ToString();
        var sideX2 = item._v1x.ToString();
        var sideY2 = item._v1y.ToString();
        var coordinates = $"{sideX1} {sideY1} {sideX2} {sideY2}";
        below.Add(coordinates);
    }

    //side A => B
    if (Calculate(item._v1x, item._v1y, item._v2x, item._v2y) >= threshold)
    {
        //запазваме кординтите на двете точки които образуват страна на триъгинлника
        var sideX1 = item._v1x.ToString();
        var sideY1 = item._v1y.ToString();
        var sideX2 = item._v2x.ToString();
        var sideY2 = item._v2y.ToString();
        var coordinates = $"{sideX1} {sideY1} {sideX2} {sideY2}";
        above.Add(coordinates);
    }
    //side B => C
    if (Calculate(item._v2x, item._v2y, item._v3x, item._v3y) >= threshold)
    {
        //запазваме кординтите на двете точки които образуват страна на триъгинлника
        var sideX1 = item._v2x.ToString();
        var sideY1 = item._v2y.ToString();
        var sideX2 = item._v3x.ToString();
        var sideY2 = item._v3y.ToString();
        var coordinates = $"{sideX1} {sideY1} {sideX2} {sideY2}";
        above.Add(coordinates);
    }
    // C => A
    if (Calculate(item._v3x, item._v3y, item._v1x, item._v1y) >= threshold)
    {
        //запазваме кординтите на двете точки които образуват страна на триъгинлника
        var sideX1 = item._v3x.ToString();
        var sideY1 = item._v3y.ToString();
        var sideX2 = item._v1x.ToString();
        var sideY2 = item._v1y.ToString();
        var coordinates = $"{sideX1} {sideY1} {sideX2} {sideY2}";
        above.Add(coordinates);
    }

    if (below.Count == 0 || above.Count == 0) continue;

    if (above.Count < below.Count)
    {
        minority = above;
    }
    else
    {
        minority = below;
    }

    if (above.Count > below.Count)
    {
        majority = above;
    }
    else
    {
        majority = below;
    }


    //var crossed_edges = (Edge(minority[0], majority[0]))

    //това е равно на  Edge(minority[0] в PYthon просто защото python пази не деклариран ти данни;
    var coordinatesMinority = minority[0].Split(" ");
    var t1X = Int32.Parse(coordinatesMinority[0]);
    var t1Y = Int32.Parse(coordinatesMinority[1]);
    var t2X = Int32.Parse(coordinatesMinority[2]);
    var t2Y = Int32.Parse(coordinatesMinority[3]);

    //това е равно на  Edge(majority[0]
    var majorityCoordinates = majority[0].Split(" ");
    var t3X = Int32.Parse(majorityCoordinates[0]);
    var t3Y = Int32.Parse(majorityCoordinates[1]);
    var t4X = Int32.Parse(majorityCoordinates[2]);
    var t4Y = Int32.Parse(majorityCoordinates[3]);

    //това е равно на  Edge(majority[1]
    var majorityCoordinatesSecond = majority[1].Split(" ");
    var t3Xs = Int32.Parse(majorityCoordinatesSecond[0]);
    var t3Ys = Int32.Parse(majorityCoordinatesSecond[1]);
    var t4Xs = Int32.Parse(majorityCoordinatesSecond[2]);
    var t4Ys = Int32.Parse(majorityCoordinatesSecond[3]);


    List<Edge> crossed_edges = new List<Edge>();

    Edge edge1 = new Edge((t1X, t1Y), (t2X, t2Y), (t3X, t3Y), (t4X, t4Y));
    crossed_edges.Add(edge1);
    Edge edge2 = new Edge((t1X, t1Y), (t2X, t2Y), (t3Xs, t3Ys), (t4Xs, t4Ys));
    crossed_edges.Add(edge2);

    var contour_points = new List<Tuple<double, double, double, double>>();
    var t1x = 0.0;
    var t1y = 0.0;
    var t2x = 0.0;
    var t2y = 0.0;

    for (int i = 0; i < crossed_edges.Count; i++)
    {
        //това е пример с два тъла, техните координат
        var e1 = crossed_edges[0];
        var e2 = crossed_edges[1];
        //тук взима пресечните точки дефакто на edge 2 
        //t1A + t1B + t2A + t2B прават едната страна на ъгъла 
        //трябва да се добават и координтитте по Y
        var how_far = ((threshold - elevation_data[(e2._t1x, e2._t2x)])
                      / ((elevation_data[(e1._t1x, e1._t2x)] - elevation_data[(e2._t1x, e2._t2x)])));

        var crossing_point_coordintesX1 = (how_far * e1._t1x + (1 - how_far) * e2._t2x);
        var crossing_point_coordintesY1 = (how_far * e1._t1y + (1 - how_far) * e2._t1y);//second points[1]
        var crossing_point_coordintesX2 = (how_far * e1._t2x + (1 - how_far) * e2._t2x);
        var crossing_point_coordintesY2 = (how_far * e1._t2y + (1 - how_far) * e2._t2y);

        t1x = crossing_point_coordintesX1;
        t1y = crossing_point_coordintesY1;
        t2x = crossing_point_coordintesX2;
        t2y = crossing_point_coordintesY2;

        //ще хардкодна координатитте на точките на ЪГЛИТЕ за да мога да подам тези точки като параметар на ъглите
        //които ще ще са в листа с кортурни точки  => contour_segments


        var crossing_point = Tuple.Create(crossing_point_coordintesX1,
                                          crossing_point_coordintesY1,
                                          crossing_point_coordintesX2,
                                          crossing_point_coordintesY2);

        contour_points.Add(crossing_point);
    }

    //counterPoints[0] садържа 1вата двойка координати, contour_points[1] втора двойка координати

    contour_segments.Add(new Edge((contour_points[0].Item1, contour_points[0].Item2),
                                 (contour_points[1].Item1, contour_points[1].Item2),
                                 (contour_points[2].Item1, contour_points[2].Item2),
                                 (contour_points[3].Item1, contour_points[3].Item2)));


    var unused_segments = new Stack<Edge>(contour_segments);
    var segments_by_point = new Dictionary<Tuple<int,int,int,int>, Edge>();

    //to be  continued :) 
    
    //foreach (var segment in contour_segments)
    //{
    //    segments_by_point.Add((segment._t1y,segment._t1y,segment._t2x,segment._t2y),segment);
    //}
    ////contour_lines = []
    //while (true)
    //{
    //    var line = unused_segments.Pop();
    //    while (true)
    //    {
    //        var unused_segments = segments_by_point(line._t1x, line._t1y, line._t2x, line._t2y);
    //            ////
    //    }
    //}

}
double Calculate(double sLatitude, double sLongitude, double eLatitude,
                               double eLongitude)
{
    var radiansOverDegrees = (Math.PI / 180.0);

    var sLatitudeRadians = sLatitude * radiansOverDegrees;
    var sLongitudeRadians = sLongitude * radiansOverDegrees;
    var eLatitudeRadians = eLatitude * radiansOverDegrees;
    var eLongitudeRadians = eLongitude * radiansOverDegrees;

    var dLongitude = eLongitudeRadians - sLongitudeRadians;
    var dLatitude = eLatitudeRadians - sLatitudeRadians;

    var result1 = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                  Math.Cos(sLatitudeRadians) * Math.Cos(eLatitudeRadians) *
                  Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

    // Using 3956 as the number of miles around the earth
    var result2 = 3956.0 * 2.0 *
                  Math.Atan2(Math.Sqrt(result1), Math.Sqrt(1.0 - result1));

    return result2;
}