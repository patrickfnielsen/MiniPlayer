using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniPlayer
{
    public class IoC
    {
        private readonly Dictionary<Type, object> _map;

        public IoC()
        {
            _map = new Dictionary<Type, object>();
        }

        public void Register<TIn>()
        {
            if (_map.ContainsKey(typeof(TIn))) return;
            var parameters = GetConstructorParameter(typeof(TIn)).Select(Resolve).ToList();
            var instance = Activator.CreateInstance(typeof(TIn), parameters.ToArray());
            _map[typeof(TIn)] = instance;
        }

        public void Register<TIn, TOut>()
        {
            if (_map.ContainsKey(typeof(TIn))) return;
            var parameters = GetConstructorParameter(typeof(TOut)).Select(Resolve).ToList();
            var instance = Activator.CreateInstance(typeof(TOut), parameters.ToArray());
            _map[typeof(TIn)] = instance;
        }

        public T Resolve<T>() where T : class
        {
            if (_map.ContainsKey(typeof(T)))
                return _map[typeof(T)] as T;

            throw new ApplicationException("The type " + typeof(T).FullName + " is not registered in the container");
        }

        private object Resolve(Type type)
        {
            if (_map.ContainsKey(type))
                return _map[type];

            throw new ApplicationException("The type " + type.FullName + " is not registered in the container");
        }

        private IEnumerable<Type> GetConstructorParameter(Type type)
        {
            var constuctors = type.GetConstructors();
            var constructor = constuctors[0]; // Assuming class ObjectType has only one constructor:

            return constructor.GetParameters().ToList().OrderBy(x => x.Position).Select(x => x.ParameterType);
        }
    }
}
