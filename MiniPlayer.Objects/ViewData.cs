using System;

namespace MiniPlayer.Objects
{
    public class ViewData
    {
        public ViewData(string name, Type context, Type view)
        {
            Name = name;
            Context = context;
            View = view;
        }

        public string Name { get; }
        public Type Context { get; }
        public Type View { get; }
    }
}
