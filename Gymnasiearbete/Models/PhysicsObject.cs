using Avalonia;
using System;
using System.Numerics;
using System.ComponentModel;

namespace Gymnasiearbete.Models
{
    public abstract class PhysicsObject : INotifyPropertyChanged
    {
        //TODO: Make conversions between pixels and meters.
        Vector2 _acceleration;
        Vector2 _velocity;
        public int Mass { get; set; }
        Point _position;

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
        }
        public int Y
        {
            get
            {
                return (int)_position.Y;
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
        public float XVelocity
        { 
            get
            {
                return _velocity.X;
            } 
        }
        public float YVelocity
        {
            get
            {
                return _velocity.Y;
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
    }
}
