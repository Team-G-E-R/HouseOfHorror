using System;
using System.Collections.Generic;

public class AllServices
{
    public static AllServices Singleton { get; private set; }
    
    private Dictionary<Type, IService> DI = new Dictionary<Type, IService>();

    public AllServices()
    {
        Singleton = this;
    }
    
    public FromServiceBindGeneric<TService> RegisterSingle<TService>() where TService : IService
    {
        return new FromServiceBindGeneric<TService>(DI, this);
    }

    public TService Single<TService>() where TService : IService => 
        (TService) DI[typeof(TService)];
}