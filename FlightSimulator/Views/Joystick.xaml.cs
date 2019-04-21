using System;
using System.Windows;
using FlightSimulator.Models.EventArgs;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace FlightSimulator.Views {
    // Class provided by instructor.
    // Interaction logic for Joystick.xaml
    public partial class Joystick : UserControl {
        // Current Aileron.
        public static readonly DependencyProperty AileronProperty =
            DependencyProperty.Register("Aileron", typeof(double), typeof(Joystick), null);
        // Current Elevator.
        public static readonly DependencyProperty ElevatorProperty =
            DependencyProperty.Register("Elevator", typeof(double), typeof(Joystick), null);
        // How often should be raised StickMove event in degrees.
        public static readonly DependencyProperty AileronStepProperty =
            DependencyProperty.Register("AileronStep", typeof(double), typeof(Joystick), new PropertyMetadata(1.0));
        // How often should be raised StickMove event in Elevator units.
        public static readonly DependencyProperty ElevatorStepProperty =
            DependencyProperty.Register("ElevatorStep", typeof(double), typeof(Joystick), new PropertyMetadata(1.0));
        // Current Aileron in degrees from 0 to 360.
        public double Aileron {
            get {
                return Convert.ToDouble(GetValue(AileronProperty));
            }
            set {
                SetValue(AileronProperty, value);
            }
        }
        // Current Elevator (or "power"), from 0 to 100.
        public double Elevator {
            get {
                return Convert.ToDouble(GetValue(ElevatorProperty));
            }
            set {
                SetValue(ElevatorProperty, value);
            }
        }
        // How often should be raised StickMove event in degrees.
        public double AileronStep {
            get {
                return Convert.ToDouble(GetValue(AileronStepProperty));
            }
            set {
                if (value < 1) value = 1; else if (value > 90) value = 90;
                SetValue(AileronStepProperty, Math.Round(value));
            }
        }
        // How often should be raised StickMove event in Elevator units.
        public double ElevatorStep {
            get {
                return Convert.ToDouble(GetValue(ElevatorStepProperty));
            }
            set {
                if (value < 1) value = 1; else if (value > 50) value = 50;
                SetValue(ElevatorStepProperty, value);
            }
        }
        // Indicates whether the joystick knob resets its place after being released.
        public delegate void OnScreenJoystickEventHandler(Joystick sender, 
            VirtualJoystickEventArgs args);
        // Delegate for joystick events that hold no data.
        public delegate void EmptyJoystickEventHandler(Joystick sender);
        // This event fires whenever the joystick moves.
        public event OnScreenJoystickEventHandler Moved;
        // This event fires once the joystick is released and its position is reset.
        public event EmptyJoystickEventHandler Released;
        // This event fires once the joystick is captured.
        public event EmptyJoystickEventHandler Captured;
        private Point _startPos;
        private double _prevAileron, _prevElevator;
        private double canvasWidth, canvasHeight;
        private readonly Storyboard centerKnob;
        public Joystick() {
            InitializeComponent();
            Knob.MouseLeftButtonDown += Knob_MouseLeftButtonDown;
            Knob.MouseLeftButtonUp += Knob_MouseLeftButtonUp;
            Knob.MouseMove += Knob_MouseMove;
            centerKnob = Knob.Resources["CenterKnob"] as Storyboard;
        }
        private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            _startPos = e.GetPosition(Base);
            _prevAileron = _prevElevator = 0;
            canvasWidth = Base.ActualWidth - KnobBase.ActualWidth;
            canvasHeight = Base.ActualHeight - KnobBase.ActualHeight;
            Captured?.Invoke(this);
            Knob.CaptureMouse();
            centerKnob.Stop();
        }
        private void Knob_MouseMove(object sender, MouseEventArgs e) {       
            if (!Knob.IsMouseCaptured) return;
            Point newPos = e.GetPosition(Base);
            Point deltaPos = new Point(newPos.X - _startPos.X, newPos.Y - _startPos.Y);
            double distance = Math.Round(Math.Sqrt(deltaPos.X * deltaPos.X + deltaPos.Y * deltaPos.Y));
            if (distance >= canvasWidth / 2 || distance >= canvasHeight / 2)
                return;
            Aileron = Math.Round(2.1 * deltaPos.X / canvasWidth, 2);
            if (Aileron > 1) Aileron = 1;
            else if (Aileron < -1) Aileron = -1;
            Elevator = Math.Round(-2.1 * deltaPos.Y / canvasHeight, 2);
            if (Elevator > 1) Elevator = 1;
            else if (Elevator < -1) Elevator = 1;
            knobPosition.X = deltaPos.X;
            knobPosition.Y = deltaPos.Y;
            if (Moved == null ||
                (!(Math.Abs(_prevAileron - Aileron) > AileronStep) && !(Math.Abs(_prevElevator - Elevator) > ElevatorStep)))
                return;
            Moved?.Invoke(this, new VirtualJoystickEventArgs { Aileron = Aileron, Elevator = Elevator });
            _prevAileron = Aileron;
            _prevElevator = Elevator;
        }
        private void Knob_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            Knob.ReleaseMouseCapture();
            centerKnob.Begin();
        }
        private void centerKnob_Completed(object sender, EventArgs e) {
            Aileron = Elevator = _prevAileron = _prevElevator = 0;
            Released?.Invoke(this);
        }
    }
}
