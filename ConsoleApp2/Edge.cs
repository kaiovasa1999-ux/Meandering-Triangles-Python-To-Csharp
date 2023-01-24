// See https://aka.ms/new-console-template for more information
using ConsoleApp2;

internal class Edge
{
    private (double t1A, double t1B) value1;
    private (double t2A, double t2B) value2;
    private (double t3A, double t3B) value3;
    private (double t4A, double t4B) value4;

    public Edge((double t1A, double t1B) value1, (double t2A, double t2B) value2, (double t3A, double t3B) value3, (double t4A, double t4B) value4)
    {
        this.value1 = value1;
        this.value2 = value2;
        this.value3 = value3;
        this.value4 = value4;
    }
    //public Edge(Point(int t1A),)
    //{

    //}
    public int _t1x { get; set; }
    public int _t1y { get; set; }
    public int _t2x { get; set; }
    public int _t2y { get; set; }
}