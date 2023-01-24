using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Triangle
    {
        public (int, int) value1;
        public (int, int) value2;
        public (int, int) value3;

        public Triangle(Tuple<int,int>t1, Tuple<int, int> t2, Tuple<int, int> t3)
        {
            (this._v1x, this._v1y) = t1;
            (this._v2x,this._v2y) = t2;
            (this._v3x,this._v3y) = t3;
        }
        public Triangle()
        {

        }

        public Triangle((int x, int y) value1, (int, int y) value2, (int x, int) value3)
        {
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
        }

        public int _v1x { get; set; }
        public int _v1y { get; set; }
        public int _v2x { get; set; }
        public int _v2y { get; set; }
        public int _v3x { get; set; }
        public int _v3y { get; set; }
    }
}
