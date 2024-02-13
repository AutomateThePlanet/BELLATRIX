// <copyright file="ServiceContainer.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
// Unauthorized copying of this file, via any medium is strictly prohibited
// Proprietary and confidential
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Resolution;

namespace Bellatrix;

public sealed class ServicesCollection : IDisposable
{
    private static ServicesCollection _serviceProvider;
    private readonly IUnityContainer _container;
    private readonly Dictionary<string, ServicesCollection> _containers;
    private readonly object _lockObject = new object();
    private bool _isDisposed;

    private ServicesCollection(IUnityContainer container)
    {
        _container = container;
        _containers = new Dictionary<string, ServicesCollection>();
    }

    public static ServicesCollection Main
    {
        get
        {
            if (_serviceProvider == null)
            {
                var unityContainer = new UnityContainer();
                _serviceProvider = new ServicesCollection(unityContainer);
                _serviceProvider.RegisterInstance(unityContainer);
            }

            return _serviceProvider;
        }
    }

    public static ServicesCollection Current
    {
        get
        {
            if (_serviceProvider == null)
            {
                var unityContainer = new UnityContainer();
                _serviceProvider = new ServicesCollection(unityContainer);
                _serviceProvider.RegisterInstance(unityContainer);
            }

            var stackTrace = new StackTrace();
            foreach (var currentFrame in stackTrace.GetFrames().Reverse())
            {
                if (currentFrame.GetMethod()?.DeclaringType != null)
                {
                    var methodType = currentFrame.GetMethod().DeclaringType;
                    if (_serviceProvider.IsPresentServicesCollection(methodType.FullName))
                    {
                        return _serviceProvider.FindCollection(methodType.FullName);
                    }
                }
            }

            return _serviceProvider;
        }
    }

    public List<ServicesCollection> GetChildServicesCollections()
    {
        if (!_containers.Values.Any())
        {
            return new List<ServicesCollection>();
        }

        return _containers.Values.ToList();
    }

    public T Resolve<T>(bool shouldThrowResolveException = false, params OverrideParameter[] overrides)
    {
        var parameterOverrides = new ResolverOverride[overrides.Length];
        for (var i = 0; i < overrides.Length; i++)
        {
            parameterOverrides[i] = new ParameterOverride(overrides[i].ParameterName, overrides[i].ParameterValue);
        }

        T result = default;
        try
        {
            result = _container.Resolve<T>(parameterOverrides);
        }
        catch (Exception)
        {
            if (shouldThrowResolveException)
            {
                throw;
            }
        }

        return result;
    }

    public T Resolve<T>(bool shouldThrowResolveException = false)
    {
        T result = default;
        try
        {
            lock (_lockObject)
            {
                result = _container.Resolve<T>();
            }
        }
        catch (Exception)
        {
            if (shouldThrowResolveException)
            {
                throw;
            }
        }

        return result;
    }

    public T Resolve<T>(string name, bool shouldThrowResolveException = false)
    {
        T result = default;
        try
        {
            lock (_lockObject)
            {
                if (!_container.IsRegistered(typeof(T), name))
                {
                    return default;
                }

                result = _container.Resolve<T>(name);
            }
        }
        catch (Exception)
        {
            if (shouldThrowResolveException)
            {
                throw;
            }
        }

        return result;
    }

    public object Resolve(Type type, bool shouldThrowResolveException = false)
    {
        var result = default(object);
        try
        {
            lock (_lockObject)
            {
                result = _container.Resolve(type);
            }
        }
        catch (Exception ex)
        {
            if (ex.InnerException.GetType().FullName.Contains("ConfigurationNotFoundException") && shouldThrowResolveException)
            {
                throw ex.InnerException;
            }
        }

        return result;
    }

    public IEnumerable<T> ResolveAll<T>(bool shouldThrowResolveException = false)
    {
        IEnumerable<T> result;
        try
        {
            lock (_lockObject)
            {
                result = _container.ResolveAll<T>();
            }
        }
        catch (Exception ex)
        {
            if (ex.InnerException != null && shouldThrowResolveException)
            {
                throw ex.InnerException;
            }

            throw;
        }

        return result;
    }

    public IEnumerable<T> ResolveAll<T>(bool shouldThrowResolveException = false, params OverrideParameter[] overrides)
    {
        var parameterOverrides = new ResolverOverride[overrides.Length];
        for (var i = 0; i < overrides.Length; i++)
        {
            parameterOverrides[i] = new ParameterOverride(overrides[i].ParameterName, overrides[i].ParameterValue);
        }

        var result = default(IEnumerable<T>);
        try
        {
            lock (_lockObject)
            {
                result = _container.ResolveAll<T>(parameterOverrides);
            }
        }
        catch (ResolutionFailedException ex)
        {
            if (ex.InnerException.GetType().FullName.Contains("ConfigurationNotFoundException") && shouldThrowResolveException)
            {
                throw ex.InnerException;
            }
        }

        return result;
    }

    public void RegisterType<TFrom, TTo>()
   where TTo : TFrom
    {
        lock (_lockObject)
        {
            _container.RegisterType<TFrom, TTo>();
        }
    }

    public void RegisterType<TFrom, TTo>(string name)
   where TTo : TFrom
    {
        lock (_lockObject)
        {
            _container.RegisterType<TFrom, TTo>(name);
        }
    }

    public void RegisterType<TFrom, TTo>(bool useSingleInstance)
   where TTo : TFrom
    {
        lock (_lockObject)
        {
            _container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }
    }

    public void RegisterType<TFrom>(bool shouldUseSingleton)
    {
        lock (_lockObject)
        {
            _container.RegisterType<TFrom>(new ContainerControlledLifetimeManager());
        }
    }

    public void RegisterInstance<TFrom>(TFrom instance, bool useSingleInstance = false)
    {
        if (useSingleInstance)
        {
            lock (_lockObject)
            {
                _container.RegisterInstance(instance, new ContainerControlledLifetimeManager());
            }
        }
        else
        {
            lock (_lockObject)
            {
                _container.RegisterInstance(instance);
            }
        }
    }

    public void RegisterType<TFrom>()
    {
        lock (_lockObject)
        {
            _container.RegisterType<TFrom>();
        }
    }

    public void RegisterNull<TFrom>()
    {
        lock (_lockObject)
        {
            _container.RegisterFactory<TFrom>(c => null);
        }
    }

    public void RegisterSingleInstance<TFrom, TTo>(InjectionConstructor injectionConstructor)
   where TTo : TFrom
    {
        lock (_lockObject)
        {
            var ctor = new Unity.Injection.InjectionConstructor(injectionConstructor.Parameters);
            _container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager(), ctor);
        }
    }

    public void RegisterSingleInstance<TFrom>(Type instanceType, InjectionConstructor injectionConstructor)
    {
        lock (_lockObject)
        {
            var ctor = new Unity.Injection.InjectionConstructor(injectionConstructor.Parameters);
            _container.RegisterType(typeof(TFrom), instanceType, new ContainerControlledLifetimeManager(), ctor);
        }
    }

    public void UnregisterSingleInstance<TFrom>()
    {
        lock (_lockObject)
        {
            var singleInstanceType = typeof(TFrom);
            var containerControlledLifetimeManagerType = typeof(ContainerControlledLifetimeManager);
            var singleInstanceRegistrations = _container.Registrations.Where(
            r =>
            r.RegisteredType == singleInstanceType &&
            r.LifetimeManager.GetType() == containerControlledLifetimeManagerType).
            ToArray();
            foreach (var singleInstanceRegistration in singleInstanceRegistrations)
            {
                singleInstanceRegistration.LifetimeManager.RemoveValue();
            }
        }
    }

    public void UnregisterSingleInstance<TFrom>(string name)
    {
        lock (_lockObject)
        {
            var singleInstanceType = typeof(TFrom);
            var containerControlledLifetimeManagerType = typeof(ContainerControlledLifetimeManager);
            var singleInstanceRegistrations = _container.Registrations.Where(
            r =>
            r.RegisteredType == singleInstanceType &&
            r.LifetimeManager.GetType() == containerControlledLifetimeManagerType
            && r.Name.Equals(name)).
            ToArray();
            foreach (var singleInstanceRegistration in singleInstanceRegistrations)
            {
                singleInstanceRegistration.LifetimeManager.RemoveValue();
            }
        }
    }

    public void RegisterType<TFrom, TTo>(string name, InjectionConstructor injectionConstructor)
   where TTo : TFrom
    {
        lock (_lockObject)
        {
            var ctor = new Unity.Injection.InjectionConstructor(injectionConstructor.Parameters);
            _container.RegisterType<TFrom, TTo>(name, ctor);
        }
    }

    public object CreateInjectionParameter<TInstance>() => new ResolvedParameter<TInstance>();

    public object CreateValueParameter(object value) => new InjectionParameter(value);

    public bool IsRegistered<TInstance>() => _container.IsRegistered<TInstance>();

    public ServicesCollection CreateChildServicesCollection(string collectionName)
    {
        lock (_lockObject)
        {
            var childNativeContainer = _container.CreateChildContainer();
            var childServicesCollection = new ServicesCollection(childNativeContainer);
            if (_containers.ContainsKey(collectionName))
            {
                _containers[collectionName] = childServicesCollection;
            }
            else
            {
                _containers.Add(collectionName, childServicesCollection);
            }

            return childServicesCollection;
        }
    }

    public void Dispose()
    {
        if (!_isDisposed)
        {
            _container.Dispose();
            GC.SuppressFinalize(this);
            _isDisposed = true;
        }
    }

    public ServicesCollection FindCollection(string collectionName)
    {
        lock (_lockObject)
        {
            if (_containers.ContainsKey(collectionName))
            {
                return _containers[collectionName];
            }

            return this;
        }
    }

    public bool IsPresentServicesCollection(string collectionName)
    {
        lock (_lockObject)
        {
            if (_containers.ContainsKey(collectionName))
            {
                return true;
            }

            return false;
        }
    }

    public void UnregisterAllRegisteredTypesByInterface<TPrimaryInterfaceType>()
    {
        lock (_lockObject)
        {
            var singleInstanceRegistrations = _container.Registrations.Where(
            r =>
            r.LifetimeManager.GetType().GetInterfaces().Contains(typeof(TPrimaryInterfaceType)));
            foreach (var registration in singleInstanceRegistrations)
            {
                registration.LifetimeManager.RemoveValue();
            }
        }
    }

    public void RegisterInstance<TFrom>(TFrom instance, string name)
    {
        lock (_lockObject)
        {
            _container.RegisterInstance(name, instance, new ContainerControlledLifetimeManager());
        }
    }
}