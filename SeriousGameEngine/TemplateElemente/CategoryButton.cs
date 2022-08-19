using System.Windows.Controls;
using System.Windows.Media;

namespace SeriousGameEngine.TemplateElemente
{
    /// <summary>
    /// A class that defines a category button
    /// </summary>
    internal class CategoryButton : Button
    {
        public bool HasEventHandler { get; set; }
        public CategoryButton(string catName)
        {
            this.Name = catName;
            this.Content = catName;
            this.Height = 30;
            this.Margin = new System.Windows.Thickness(10);

            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            this.FontFamily = new FontFamily("Sinkin Sans 300 Light");
            this.FontSize = 14;

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.BorderBrush = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3D77B1"));
        }
    }
}
