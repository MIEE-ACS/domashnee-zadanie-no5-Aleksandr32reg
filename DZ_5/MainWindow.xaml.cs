using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DZ_5
{
    public partial class MainWindow : Window
    {
        class Circle
        {
            private double radius;

            public double Radius
            {
                get
                {
                    return radius;
                }
                set
                {
                    if (value > 0)
                    {
                        radius = value;
                    }
                    else
                    {
                        throw new Exception("Радиус должен быть положительным.");
                    }
                }
            }
            public double CalculateSquare()
            {
                return Math.Round(Math.PI * Math.Pow(radius, 2), 2);
            }
            public double CalculateLength()
            {
                return Math.Round(2 * Math.PI * radius, 2);
            }
            public bool DotInCircle(double X, double Y)
            {
                if (radius >= Math.Abs(X))
                {
                    if (Math.Sqrt(Math.Abs(Math.Pow(radius, 2) - Math.Pow(X, 2))) >= Math.Abs(Y))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            public override string ToString()
            {
                return $"Радиус: {radius}";
            }
        }

        List<Circle> circles = new List<Circle>
        {
            new Circle { Radius = 5},
            new Circle { Radius = 2},
            new Circle { Radius = 2.5},
            new Circle { Radius = 0.5},
            new Circle { Radius = 10},
        };

        public void updateCircleList()
        {
            CirclesLB.Items.Clear();
            foreach (var circle in circles)
            {
                CirclesLB.Items.Add(circle);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            updateCircleList();
        }

        private void CreateCircleBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Circle circle = new Circle { Radius = double.Parse(RadiusTB.Text) };
                circles.Add(circle);
                updateCircleList();
            }
            catch (FormatException)
            {
                MessageBox.Show("Заданы нечисловые значения радиуса!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Текст ошибки:\r\n" + ex.Message, "Неизвестная ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void СalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CirclesLB.SelectedItem == null)
                {
                    throw new Exception("Не был выбран круг!");
                }
                Circle circle = circles.ElementAt(CirclesLB.SelectedIndex);
                SquareTB.Text = circle.CalculateSquare().ToString();
                LengthTB.Text = circle.CalculateLength().ToString();
                if (double.TryParse(xTB.Text, out double X) && double.TryParse(yTB.Text, out double Y))
                {
                    DotResultTB.Text = $"Точка ({X};{Y})";
                    if (circle.DotInCircle(X, Y))
                    {
                        DotResultTB.Text += " лежит в окружности";
                    }
                    else
                    {
                        DotResultTB.Text += " не лежит в окружности";
                    }
                }
                else if (xTB.Text != "" || yTB.Text != "")
                {
                    throw new Exception("Заданы нечисловые координаты точки!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Текст ошибки:\r\n" + ex.Message, "Неизвестная ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
