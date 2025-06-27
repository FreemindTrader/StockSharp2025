//using System;
//using System.Collections.Generic; using fx.Collections;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace fx.Charting.Definitions
//{
//    public class GenericPoint2D<TY> : IPoint where TY : ISeriesPoint<double>
//    {
//        private double _x;
//        private TY _y;

//        public GenericPoint2D( double x, TY y )
//        {
//            this._x = x;
//            this._y = y;
//        }

//        public double X
//        {
//            get
//            {
//                return this._x;
//            }
//        }

//        public double Y
//        {
//            get
//            {
//                return this._y.Y;
//            }
//        }

//        public TY YValues
//        {
//            get
//            {
//                return this._y;
//            }
//        }
//    }
//}
