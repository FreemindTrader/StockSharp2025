// Decompiled with JetBrains decompiler
// Type: Ecng.Xaml.Charting.AxisCollection
// Assembly: Ecng.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\Ecng.Xaml.Charting.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Ecng.Xaml.Charting.Common.Extensions;
using Ecng.Xaml.Charting.Common.Helpers;
using Ecng.Xaml.Charting.Visuals.Axes;

namespace Ecng.Xaml.Charting
{
    public class AxisCollection : ObservableCollection<IAxis>, IXmlSerializable
    {
        public AxisCollection()
        {
            this.SetUpCollection();
        }

        public AxisCollection( IEnumerable<IAxis> collection )
          : base( collection )
        {
            this.SetUpCollection();
        }

        protected bool HasPrimaryAxis
        {
            get
            {
                return this.Any<IAxis>( ( Func<IAxis, bool> ) ( x => x.IsPrimaryAxis ) );
            }
        }

        protected IAxis PrimaryAxis
        {
            get
            {
                return this.FirstOrDefault<IAxis>( ( Func<IAxis, bool> ) ( x => x.IsPrimaryAxis ) );
            }
        }

        public IAxis Default
        {
            get
            {
                if ( this.Count <= 0 )
                    return ( IAxis ) null;
                return this.GetAxisById( "DefaultAxisId", false );
            }
        }

        public IAxis GetAxisById( string axisId, bool assertAxisExists = false )
        {
            try
            {
                IAxis axis = this.SingleOrDefault<IAxis>((Func<IAxis, bool>) (x => x.Id == axisId));
                if ( assertAxisExists && axis == null )
                    throw new InvalidOperationException( string.Format( "AxisCollection.GetAxisById('{0}') returned no axis with ID={0}. Please check you have added an axis with this Id to the AxisCollection", ( object ) ( axisId ?? "NULL" ) ) );
                return axis;
            }
            catch
            {
                throw new InvalidOperationException( string.Format( "AxisCollection.GetAxisById('{0}') returned more than one axis with the ID={0}. Please check you have assigned correct axis Ids when you have multiple axes in Ultrachart", ( object ) ( axisId ?? "NULL" ) ) );
            }
        }

        private void SetUpCollection()
        {
            this.CollectionChanged -= new NotifyCollectionChangedEventHandler( this.AxisCollectionChanged );
            this.CollectionChanged += new NotifyCollectionChangedEventHandler( this.AxisCollectionChanged );
            IAxis axis = this.FirstOrDefault<IAxis>();
            if ( this.HasPrimaryAxis || axis == null )
                return;
            axis.IsPrimaryAxis = true;
        }

        public XmlSchema GetSchema()
        {
            return ( XmlSchema ) null;
        }

        public virtual void ReadXml( XmlReader reader )
        {
            this.AddRange<IAxis>( ( IEnumerable<IAxis> ) AxisSerializationHelper.Instance.DeserializeCollection( reader ) );
        }

        public virtual void WriteXml( XmlWriter writer )
        {
            AxisSerializationHelper.Instance.SerializeCollection( this.OfType<AxisBase>(), writer );
        }

        private void AxisCollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            IAxis axis1 = e.NewItems != null ? e.NewItems.Cast<IAxis>().FirstOrDefault<IAxis>((Func<IAxis, bool>) (x => x.IsPrimaryAxis)) : (IAxis) null;
            if ( axis1 == null )
            {
                axis1 = this.PrimaryAxis;
                if ( axis1 == null )
                {
                    axis1 = this.FirstOrDefault<IAxis>();
                    if ( axis1 != null )
                        axis1.IsPrimaryAxis = true;
                }
            }
            foreach ( IAxis axis2 in ( Collection<IAxis> ) this )
            {
                if ( axis2 != axis1 )
                    axis2.IsPrimaryAxis = false;
            }
        }
    }
}
