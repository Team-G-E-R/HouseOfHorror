using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class AllServices
{
    public static AllServices Singleton { get; private set; }
    public List<ISavedProgress> SavedProgressesServices { get; set; }

    private Dictionary<Type, IService> DI = new Dictionary<Type, IService>();

    public AllServices()
    {
        Singleton = this;
        SavedProgressesServices = new List<ISavedProgress>();
    }
    
    public FromServiceBindGeneric<TService> RegisterSingle<TService>() where TService : IService => 
        new(DI, this);

    public TService Single<TService>() where TService : IService => 
        (TService) DI[typeof(TService)];
}