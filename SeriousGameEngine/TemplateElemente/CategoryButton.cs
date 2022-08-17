using System.Windows.Controls;
using System.Windows.Media;

namespace SeriousGameEngine.TemplateElemente
{
    internal class CategoryButton : Button
    {
        public bool HasEventHandler { get; set; }
        public CategoryButton(string catName)
        {
            this.Name = catName;
            this.Content = catName;
            this.Height = 18;
            this.Margin = new System.Windows.Thickness(0,10,0,0);

            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            this.FontFamily = new FontFamily("Arial");
            this.FontSize = 13;

            this.Background = new SolidColorBrush(Colors.Transparent);
            this.BorderBrush = new SolidColorBrush(Colors.Transparent);
            this.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(OptionUIElement.foregroundColor2));
        }
    }
}
