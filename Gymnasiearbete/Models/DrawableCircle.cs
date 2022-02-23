using Avalonia.Media;
using Engine.Objects;
using System.ComponentModel;
using System.Numerics;

namespace Gymnasiearbete.Models
{
    public class DrawableCircle : Circle, IDrawable, IPhysicsObject
    {
        // Convert Position to Engine.Objects.Point and from Avalonia.Point 
        new public Avalonia.Point Position
        {
            get
            {
                return new Avalonia.Point(base.Position.X, base.Position.Y);
            }
            set
            {
                base.Position = new Point(value.X, value.Y);
                InvokePropertyChanged();
            }
        }

        // Invoke PropertyChanged event each time a property changes.
        public override Vector2 Velocity
        {
            get => base.Velocity;
            set
            {
                base.Velocity = value;
                InvokePropertyChanged();
            }
        }
        public override Vector2 Acceleration
        {
            get => base.Acceleration;
            set
            {
                base.Acceleration = value;
                InvokePropertyChanged();
            }
        }
        public override int Mass
        {
            get => base.Mass;
            set
            {
                base.Mass = value;
                InvokePropertyChanged();
            }
        }

        public override int Radius 
        { 
            get
            {
                return base.Radius;
            } 
            set
            {
                base.Radius = value;
                InvokePropertyChanged();
            }
        }

        public Color Color { get; set; }
        public int X
        {
            get
            {
                return (int)Position.X;
            }
            set
            {
                Position = new Avalonia.Point(value, Position.Y);
            }
        }
        public int Y
        {
            get
            {
                return (int)Position.Y;
            }
            set
            {
                Position = new Avalonia.Point(Position.X, value);
            }
        }
        public int XVelocity
        {
            get
            {
                return (int)Velocity.X;
            }
            set
            {
                Velocity = new Vector2(value, Velocity.Y);
            }
        }
        public int YVelocity
        {
            get
            {
                return (int)Velocity.Y;
            }
            set
            {
                Velocity = new Vector2(Velocity.X, value);
            }
        }
        public int XAcceleration
        {
            get
            {
                return (int)Acceleration.X;
            }
            set
            {
                Acceleration = new Vector2(value, Acceleration.Y);
            }
        }
        public int YAcceleration
        {
            get
            {
                return (int)Acceleration.Y;
            }
            set
            {
                Acceleration = new Vector2(Acceleration.X, value);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void InvokePropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}