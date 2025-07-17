// Decompiled with JetBrains decompiler
// Type: MatterHackers.VectorMath.TrackBallController
// Assembly: fx.Xaml.Charting, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b10e79ed0227b515
// MVID: C2F11401-C1E6-47FC-9255-FC66EA027789
// Assembly location: A:\10 - StockSharp\Hydra\fx.Xaml.Charting.dll

using System;

namespace MatterHackers.VectorMath
{
    internal class TrackBallController
    {
        private Matrix4X4 currentRotationMatrix = Matrix4X4.Identity;
        private Matrix4X4 currentTranslationMatrix = Matrix4X4.Identity;
        private Quaternion activeRotationQuaternion = Quaternion.Identity;
        private Vector2 lastTranslationMousePosition = Vector2.Zero;
        private const double Epsilon = 1E-05;
        private Vector2 screenCenter;
        private double rotationTrackingRadius;
        private TrackBallController.MouseDownType currentTrackingType;
        private Matrix4X4 localToScreenTransform;
        private Vector3 rotationStart;
        private Vector3 rotationCurrent;

        public TrackBallController( Vector2 screenCenter, double trackBallRadius )
        {
            this.rotationStart = new Vector3();
            this.rotationCurrent = new Vector3();
            this.screenCenter = screenCenter;
            this.rotationTrackingRadius = trackBallRadius;
        }

        public TrackBallController( TrackBallController trackBallToCopy )
        {
            this.screenCenter = trackBallToCopy.screenCenter;
            this.rotationTrackingRadius = trackBallToCopy.rotationTrackingRadius;
            this.currentRotationMatrix = trackBallToCopy.currentRotationMatrix;
            this.currentTranslationMatrix = trackBallToCopy.currentTranslationMatrix;
        }

        public double Scale
        {
            get
            {
                return Vector3.TransformPosition( Vector3.UnitX, this.GetTransform4X4() ).Length;
            }
            set
            {
                this.currentTranslationMatrix *= Matrix4X4.CreateScale( value / this.Scale );
            }
        }

        public void Translate( Vector3 deltaPosition )
        {
            this.currentTranslationMatrix = Matrix4X4.CreateTranslation( deltaPosition ) * this.currentTranslationMatrix;
        }

        public TrackBallController.MouseDownType CurrentTrackingType
        {
            get
            {
                return this.currentTrackingType;
            }
        }

        private void MapToSphere( Vector2 screenPoint, out Vector3 vector )
        {
            Vector2 vector2 = screenPoint - this.screenCenter;
            vector2.x /= this.rotationTrackingRadius;
            vector2.y /= this.rotationTrackingRadius;
            double d = vector2.x * vector2.x + vector2.y * vector2.y;
            if ( d > 1.0 )
            {
                double num = 1.0 / Math.Sqrt(d);
                vector.x = vector2.x * num;
                vector.y = vector2.y * num;
                vector.z = 0.0;
            }
            else
            {
                vector.x = vector2.x;
                vector.y = vector2.y;
                vector.z = Math.Sqrt( 1.0 - d );
            }
            vector = Vector3.TransformVector( vector, this.localToScreenTransform );
        }

        public virtual void OnMouseDown( Vector2 mousePosition, Matrix4X4 screenToLocal, TrackBallController.MouseDownType trackType = TrackBallController.MouseDownType.Rotation )
        {
            if ( this.currentTrackingType != TrackBallController.MouseDownType.None )
                return;
            this.localToScreenTransform = Matrix4X4.Invert( screenToLocal );
            this.currentTrackingType = trackType;
            switch ( this.currentTrackingType )
            {
                case TrackBallController.MouseDownType.Translation:
                    this.lastTranslationMousePosition = mousePosition;
                    break;
                case TrackBallController.MouseDownType.Rotation:
                    this.MapToSphere( mousePosition, out this.rotationStart );
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void OnMouseMove( Vector2 mousePosition )
        {
            switch ( this.currentTrackingType )
            {
                case TrackBallController.MouseDownType.Translation:
                    Vector2 vector2 = (mousePosition - this.lastTranslationMousePosition) / this.screenCenter.x * 4.75;
                    this.currentTranslationMatrix *= Matrix4X4.CreateTranslation( Vector3.TransformPosition( Vector3.TransformPosition( new Vector3( vector2.x, vector2.y, 0.0 ), Matrix4X4.Invert( this.CurrentRotation ) ), this.localToScreenTransform ) );
                    this.lastTranslationMousePosition = mousePosition;
                    break;
                case TrackBallController.MouseDownType.Rotation:
                    this.activeRotationQuaternion = Quaternion.Identity;
                    this.MapToSphere( mousePosition, out this.rotationCurrent );
                    Vector3 vector3 = Vector3.Cross(this.rotationStart, this.rotationCurrent);
                    if ( vector3.Length <= 1E-05 )
                        break;
                    this.activeRotationQuaternion.X = vector3.x;
                    this.activeRotationQuaternion.Y = vector3.y;
                    this.activeRotationQuaternion.Z = vector3.z;
                    this.activeRotationQuaternion.W = Vector3.Dot( this.rotationStart, this.rotationCurrent );
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public void OnMouseUp()
        {
            switch ( this.currentTrackingType )
            {
                case TrackBallController.MouseDownType.Translation:
                    this.currentTrackingType = TrackBallController.MouseDownType.None;
                    break;
                case TrackBallController.MouseDownType.Rotation:
                    this.currentRotationMatrix *= Matrix4X4.CreateRotation( this.activeRotationQuaternion );
                    this.activeRotationQuaternion = Quaternion.Identity;
                    goto case TrackBallController.MouseDownType.Translation;
                default:
                    throw new NotImplementedException();
            }
        }

        public void OnMouseWheel( int wheelDelta )
        {
            double scale = 1.0;
            if ( wheelDelta > 0 )
                scale = 1.2;
            else if ( wheelDelta < 0 )
                scale = 0.8;
            this.currentTranslationMatrix *= Matrix4X4.CreateScale( scale );
        }

        private Matrix4X4 CurrentRotation
        {
            get
            {
                if ( this.activeRotationQuaternion == Quaternion.Identity )
                    return this.currentRotationMatrix;
                return this.currentRotationMatrix * Matrix4X4.CreateRotation( this.activeRotationQuaternion );
            }
        }

        public Matrix4X4 GetTransform4X4()
        {
            return this.currentTranslationMatrix * this.CurrentRotation;
        }

        public Vector2 ScreenCenter
        {
            get
            {
                return this.screenCenter;
            }
            set
            {
                this.screenCenter = value;
            }
        }

        public double TrackBallRadius
        {
            get
            {
                return this.rotationTrackingRadius;
            }
            set
            {
                this.rotationTrackingRadius = value;
            }
        }

        public enum MouseDownType
        {
            None,
            Translation,
            Rotation,
            Scale,
        }
    }
}
