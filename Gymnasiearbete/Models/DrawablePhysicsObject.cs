using Avalonia.Controls.Shapes;
using Avalonia.Media;
using System;
using System.ComponentModel;
using Engine.Objects;
using Avalonia;
using System.Numerics;

namespace Gymnasiearbete.Models
{
    public class DrawablePhysicsObject : PhysicsObject, INotifyPropertyChanged
    {
        // Event for updating UI
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged(string propertyName = "")
        {
            // Setting propertyName breaks the event
            // Why does it do this?
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Fields
        Color _color;

        // Properties
        public Shape? ControlShape { get; set; }
        public Color Color {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }
        public string ColorString
        {
            get
            {
                return _color.ToString();
            }
        }
        new public Avalonia.Point Position
        {
            get
            {
                return new Avalonia.Point(_position.X, _position.Y);
            }
            set
            {
                _position = new Engine.Objects.Point(value.X, value.Y);
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
                _position = new Engine.Objects.Point(value, _position.Y);
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
                _position = new Engine.Objects.Point(_position.X, value);
            }
        }
        new public Vector2 Velocity
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
        new public Vector2 Acceleration
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