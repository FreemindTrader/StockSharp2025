using fx.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Definitions
{
    // The structure for storing the points, found by the zigzag
    //public struct TPoints
    //{
    //    public double[] ValuePoints;   // the values of the found points
    //    public int[]    IndexPoints;   // the indexes of the found points
    //    public int      NumPoints;       // the number of found points
    //};


    // A class for storing the parameters of a wave
    public class TWave
    {
        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
            }
        }

        string _formula;
        public string Formula
        {
            get { return _formula; }
            set
            {
                _formula = value;
            }
        }

        int _level;
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
            }
        }

        double[] _valueVertex;
        public double[] Value
        {
            get { return _valueVertex; }
            set
            {
                _valueVertex = value;
            }
        }


        int[] _indexVertex;
        public int [ ] Index
        {
            get { return _indexVertex; }
            set
            {
                _indexVertex = value;
            }
        }              
    };

    public class TNode 
    {
        private PooledList< TNode > _child = new PooledList<TNode>();

        public PooledList< TNode>  Child
        {
            get { return _child; }
            set
            {
                _child = value;
            }
        }

        TWave _wave;
        public TWave Wave
        {
            get { return _wave; }
            set
            {
                _wave = value;
            }
        }

        string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
            }
        }

        public TNode Add( string Text, TWave wave = null ) // function of adding a node into the tree
        {
            TNode node = new TNode();

            node.Child = new PooledList<TNode>( );
            node.Text  = Text;
            node.Wave  = Wave;

            Child.Add( node );

            return ( node );
        }
    };

    // The structure of the description of the analyzed waves in the program
    public class TWaveDescription
    {
        string _waveName; // name of the wave
        public string WaveName
        {
            get { return _waveName; }
            set
            {
                _waveName = value;
            }
        }

        int _numOfSubWaves; // number of sub-waves in a wave
        public int NumOfSubWaves
        {
            get { return _numOfSubWaves; }
            set
            {
                _numOfSubWaves = value;
            }
        }

        private PooledList< string > _subwaves; // the names of the possible sub-waves in the wave

        public PooledList<string> SubWaves
        {
            get { return _subwaves; }
            set
            {
                _subwaves = value;
            }
        }        
    };

    public class TZigzag 
    {
        private PooledList< int > _indexVertex = new PooledList<int>(); // indexes of the vertexes of the zigzag
        private PooledList< double > _valueVertex = new PooledList<double>(); // value of the vertexes of the zigzags

        public PooledList<int> Index
        {
            get { return _indexVertex; }
            set
            {
                _indexVertex = value;
            }
        }

        public PooledList<double> Value
        {
            get { return _valueVertex; }
            set
            {
                _valueVertex = value;
            }
        }
        
    };

    public class TNodeInfo
    {
        // the range of the already analyzed section
        int _indexStart;
        public int IndexStart
        {
            get { return _indexStart; }
            set
            {
                _indexStart = value;
            }
        }

        int _indexFinish;
        public int IndexFinish
        {
            get { return _indexFinish; }
            set
            {
                _indexFinish = value;
            }
        }

        // the edge value of the already analyzed section

        double _valueStart;
        public double ValueStart
        {
            get { return _valueStart; }
            set
            {
                _valueStart = value;
            }
        }

        double _valueFinish;
        public double ValueFinish
        {
            get { return _valueFinish; }
            set
            {
                _valueFinish = value;
            }
        }

        string _subwaves;       // the name of the wave and the group of the waves
        public string SubWaves
        {
            get { return _subwaves; }
            set
            {
                _subwaves = value;
            }
        }


        TNode            _node; // the node, pointing to the already analyzed range of the chart
        public TNode Node
        {
            get { return _node; }
            set
            {
                _node = value;
            }
        }              
    };

}
