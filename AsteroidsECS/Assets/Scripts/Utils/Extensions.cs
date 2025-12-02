using System;
using System.Collections.Generic;
using Leopotam.EcsProto;

namespace Utils
{
    public static class Extensions
    {
        public static T GetAspect<T>(this ProtoWorld world)
        {
            return (T)world.Aspect(typeof(T));
        }
    
        public static T GetService<T>(this IProtoSystems systems) where T : class
        {
            Dictionary<Type, object> svc = systems.Services();
            return svc[typeof(T)] as T;
        } 
    }
}