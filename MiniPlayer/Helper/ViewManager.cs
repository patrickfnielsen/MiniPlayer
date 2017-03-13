using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using MiniPlayer.Objects;

namespace MiniPlayer.Helper
{
    public class ViewManager
    {
        private List<IView> _views = new List<IView>();
        private readonly DataTemplateManager _templateManger = new DataTemplateManager();
        private readonly AssemblyManager _assembly = new AssemblyManager();

        public void RegisterViews(string path)
        {
            Directory.CreateDirectory(path);
            _views = _assembly.LoadFromPath<IView>(path);

            RegisterDataTemplates();
        }

        private void RegisterDataTemplates()
        {
            foreach (var view in _views)
            {
                var viewModelType = view.GetType();
                var viewType = viewModelType.Assembly.DefinedTypes.SingleOrDefault(x => x.BaseType == typeof(UserControl));
                _templateManger.RegisterDataTemplate(viewModelType, viewType);
            }
        }

        public IView GetViewByName(string name)
        {
            return _views.Single(x => x.Name == name);
        }

        public List<IView> GetViews()
        {
            return _views;
        }
    }
}
