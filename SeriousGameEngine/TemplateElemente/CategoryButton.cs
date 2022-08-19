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
            lastSelected?.Deselect();
            lastSelected = this;
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
}
