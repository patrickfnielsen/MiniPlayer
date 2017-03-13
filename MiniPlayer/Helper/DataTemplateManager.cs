using System;
using System.Windows;
using System.Windows.Markup;

namespace MiniPlayer.Helper
{
    public class DataTemplateManager
    {
        public void RegisterDataTemplate<TViewModel, TView>() where TView : FrameworkElement
        {
            RegisterDataTemplate(typeof(TViewModel), typeof(TView));
        }

        public void RegisterDataTemplate(Type viewModelType, Type viewType)
        {
            var template = CreateTemplate(viewModelType, viewType);
            var key = template?.DataTemplateKey;

            if (key != null)
                Application.Current.Resources.Add(key, template);
        }

        private DataTemplate CreateTemplate(Type viewModelType, Type viewType)
        {
            const string xamlTemplate = "<DataTemplate DataType=\"{{x:Type vm:{0}}}\"><view:{1} /></DataTemplate>";
            var xaml = string.Format(xamlTemplate, viewModelType.Name, viewType.Name);

            var context = new ParserContext
            {
                XamlTypeMapper = new XamlTypeMapper(new string[0])
            };

            if (viewModelType.Namespace == null || viewType.Namespace == null) return null;
            context.XamlTypeMapper.AddMappingProcessingInstruction("vm", viewModelType.Namespace, viewModelType.Assembly.FullName);
            context.XamlTypeMapper.AddMappingProcessingInstruction("view", viewType.Namespace, viewType.Assembly.FullName);

            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("vm", "vm");
            context.XmlnsDictionary.Add("view", "view");

            var template = (DataTemplate)XamlReader.Parse(xaml, context);
            return template;
        }
    }
}
