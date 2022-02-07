using Avalonia;
using System;
using System.Numerics;
using System.ComponentModel;

namespace Gymnasiearbete.Models
{
    public abstract class PhysicsObject : INotifyPropertyChanged
    {
        //TODO: Make conversions between pixels and meters.
        // Set fields and default values
        Vector2 _acceleration = new Vector2(0, 0);
        Vector2 _velocity = new Vector2(0, 0);
        public int Mass { get; set; } = 0;
        Point _position = new Point(0, 0);

        // Event for updating UI
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged(String propertyName = "")
        {
            // Setting propertyName breaks the event
            // Why does it do this?
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Point Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                NotifyPropertyChanged();
            }
        }
        public int X
        {
            get
            {
                return (int)_position.X;
            }
            set
            {
                _position = new Point(value, _position.Y);
            }
        }
        public int Y
        {
            get
            {
                return (int)_position.Y;
            }
            set
            {
                _position = new Point(_position.X, value);
            }
        }
        public Vector2 Velocity
        {
            get
            {
                return _velocity;
            }
            set
            {
                _velocity = value;
                NotifyPropertyChanged();
            }
        }
        public int XVelocity
        { 
            get
            {
                return (int)_velocity.X;
            } 
            set
            {
                _velocity.X = value;
            }
        }
        public int YVelocity
        {
            get
            {
                return (int)_velocity.Y;
            }
            set
            {
                _velocity.Y = value;
            }
        }
        public Vector2 Acceleration
        {
            get
            {
                return _acceleration;
            }
            set
            {
                _acceleration = value;
                NotifyPropertyChanged();
            }
        }
        public int XAcceleration
        {
            get
            {
                return (int)_acceleration.X;
            }
            set
            {
                _acceleration.X = value;
            }
        }
        public int YAcceleration
        {
            get
            {
                return (int)_acceleration.Y;
            }
            set
            {
                _acceleration.Y = value;
            }
        }
    }
}
