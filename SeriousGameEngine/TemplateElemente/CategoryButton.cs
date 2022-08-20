using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System;
using System.Windows;

namespace SeriousGameEngine.TemplateElemente
{
    /// <summary>
    /// A class that defines a category button
    /// </summary>
    internal class CategoryButton : TextBlock, IDisposable
    {
        static CategoryButton lastSelected = null;

        bool isSelected = false;

        public delegate void Click(object sender, RoutedEventArgs args);
        public event Click click;

        public bool HasEventHandler { get; set; }
        public FontFamily normalFont = new FontFamily("Sinkin Sans 300 Light");
        public FontFamily highlightedFont =new FontFamily("Sinkin Sans 500 Medium");
        public CategoryButton(string catName)
        {
            this.Name = catName;
            this.Text = catName;
            this.Height = 30;
            this.Margin = new Thickness(10);

            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Center;

            this.FontFamily = normalFont;
            this.FontSize = 14;

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));

            this.MouseEnter += new MouseEventHandler(MEnter);
            this.MouseLeave += new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);

        }

        /// <summary>
        /// Mouse entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MEnter(object sender, MouseEventArgs args )
        {
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Mouse exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MExit(object sender, MouseEventArgs args)
        {
            if(!isSelected)
                this.FontFamily = normalFont;
        }

        private void MDown(object sender, MouseButtonEventArgs args)
        {
            isSelected = true;
            click?.Invoke(sender, args);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
                this.FontFamily = highlightedFont;
            }
        }

        public void Deselect()
        {
            this.isSelected = false;
            this.FontFamily = normalFont;
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.MouseEnter -= new MouseEventHandler(MEnter);
            this.MouseLeave -= new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);
        }
    }

    internal class TemplateButton : TextBlock, IDisposable
    {
        static TemplateButton lastSelected = null;

        bool isSelected = false;

        public delegate void Click(object sender, RoutedEventArgs args);
        public event Click click;

        public bool HasEventHandler { get; set; }
        public FontFamily normalFont = new FontFamily("Sinkin Sans 300 Light");
        public FontFamily highlightedFont = new FontFamily("Sinkin Sans 500 Medium");
        public TemplateButton(string catName)
        {
            this.Name = catName;
            this.Text = catName;
            this.Height = 30;
            this.Margin = new System.Windows.Thickness(10);

            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            this.FontFamily = normalFont;
            this.FontSize = 14;

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));

            this.MouseEnter += new MouseEventHandler(MEnter);
            this.MouseLeave += new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);

        }

        /// <summary>
        /// Mouse entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MEnter(object sender, MouseEventArgs args)
        {
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Mouse exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MExit(object sender, MouseEventArgs args)
        {
            if (!isSelected)
                this.FontFamily = normalFont;
        }

        private void MDown(object sender, MouseButtonEventArgs args)
        {
            isSelected = true;
            click?.Invoke(sender, args);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
                this.FontFamily = highlightedFont;
            }
        }

        public void Deselect()
        {
            this.isSelected = false;
            this.FontFamily = normalFont;
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.MouseEnter -= new MouseEventHandler(MEnter);
            this.MouseLeave -= new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);
        }
    }

    internal class SubjectButton : TextBlock, IDisposable
    {
        static SubjectButton lastSelected = null;

        bool isSelected = false;

        public delegate void Click(object sender, RoutedEventArgs args);
        public event Click click;

        public bool HasEventHandler { get; set; }
        public FontFamily normalFont = new FontFamily("Sinkin Sans 300 Light");
        public FontFamily highlightedFont = new FontFamily("Sinkin Sans 500 Medium");
        public SubjectButton(string catName)
        {
            this.Name = catName;
            this.Text = catName;
            this.Height = 30;
            this.Margin = new System.Windows.Thickness(10);

            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            this.FontFamily = normalFont;
            this.FontSize = 14;

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));

            this.MouseEnter += new MouseEventHandler(MEnter);
            this.MouseLeave += new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);

        }

        /// <summary>
        /// Mouse entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MEnter(object sender, MouseEventArgs args)
        {
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Mouse exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MExit(object sender, MouseEventArgs args)
        {
            if (!isSelected)
                this.FontFamily = normalFont;
        }

        private void MDown(object sender, MouseButtonEventArgs args)
        {
            isSelected = true;
            click?.Invoke(sender, args);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
                this.FontFamily = highlightedFont;
            }
        }

        public void Deselect()
        {
            this.isSelected = false;
            this.FontFamily = normalFont;
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.MouseEnter -= new MouseEventHandler(MEnter);
            this.MouseLeave -= new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);
        }
    }

    internal class NavigationButton : TextBlock, IDisposable
    {
        public static NavigationButton lastSelected = null;

        bool isSelected = false;

        public delegate void Click(object sender, RoutedEventArgs args);
        public event Click click;

        public bool HasEventHandler { get; set; }
        public FontFamily normalFont = new FontFamily("Sinkin Sans 200 X Light");
        public FontFamily highlightedFont = new FontFamily("Sinkin Sans 500 Medium");
        public NavigationButton(string catName, int width)
        {
            this.Name = catName;
            this.Text = catName;
            this.Height = 30;
            this.FontFamily = normalFont;
            this.FontSize = 14;
            this.Width = width;
            this.Margin = new Thickness(20,5,0,0);

            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));

            this.MouseEnter += new MouseEventHandler(MEnter);
            this.MouseLeave += new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);

        }

        /// <summary>
        /// Mouse entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MEnter(object sender, MouseEventArgs args)
        {
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Mouse exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MExit(object sender, MouseEventArgs args)
        {
            if (!isSelected)
                this.FontFamily = normalFont;
        }

        private void MDown(object sender, MouseButtonEventArgs args)
        {
            isSelected = true;
            click?.Invoke(sender, args);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
                this.FontFamily = highlightedFont;
            }
        }

        public void Deselect()
        {
            this.isSelected = false;
            this.FontFamily = normalFont;
        }

        public void Select()
        {
            isSelected = true;
            click?.Invoke(this, null);
            if (lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
                this.FontFamily = highlightedFont;
            }
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.MouseEnter -= new MouseEventHandler(MEnter);
            this.MouseLeave -= new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);
        }
    }

    internal class NewGameButton : TextBlock, IDisposable
    {
        public static NewGameButton lastSelected = null;

        bool isSelected = false;

        public delegate void Click(object sender, RoutedEventArgs args);
        public event Click click;

        public bool HasEventHandler { get; set; }
        public FontFamily normalFont = new FontFamily("Sinkin Sans 300 Light");
        public FontFamily highlightedFont = new FontFamily("Sinkin Sans 500 Medium");
        public NewGameButton(string catName)
        {
            this.Name = catName.Replace(' ', '_');
            this.Text = catName;
            this.FontFamily = normalFont;
            this.FontSize = 12;
            this.Margin = new Thickness(10);

            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.VerticalAlignment = VerticalAlignment.Center;
            

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));

            this.MouseEnter += new MouseEventHandler(MEnter);
            this.MouseLeave += new MouseEventHandler(MExit);
            this.MouseDown += new MouseButtonEventHandler(MDown);

        }

        /// <summary>
        /// Mouse entering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MEnter(object sender, MouseEventArgs args)
        {
            this.FontFamily = highlightedFont;
        }

        /// <summary>
        /// Mouse exiting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void MExit(object sender, MouseEventArgs args)
        {
            if (!isSelected)
                this.FontFamily = normalFont;
        }

        private void MDown(object sender, MouseButtonEventArgs args)
        {
            click?.Invoke(sender, args);
        }

        public void Deselect()
        {
            this.isSelected = false;
            this.FontFamily = normalFont;
        }

        public void Select()
        {
            isSelected = true;
            click?.Invoke(this, null);
            if(lastSelected != this)
            {
                lastSelected?.Deselect();
                lastSelected = this;
                this.FontFamily = highlightedFont;
            }
        }

        /// <summary>
        /// Dispose object
        /// </summary>
        public void Dispose()
        {
            this.MouseEnter -= new MouseEventHandler(MEnter);
            this.MouseLeave -= new MouseEventHandler(MExit);
        }
    }

}
