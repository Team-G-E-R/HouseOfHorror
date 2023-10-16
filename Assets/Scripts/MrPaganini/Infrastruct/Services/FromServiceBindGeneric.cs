using System;
using System.Collections.Generic;

public class FromServiceBindGeneric<TContract>
{
    private readonly Dictionary<Type, IService> _di;
    private readonly AllServices _allServices;

    public FromServiceBindGeneric(Dictionary<Type, IService> di, AllServices allServices)
    {
        _di = di;
        _allServices = allServices;
    }
    
    public new FromServiceBindGeneric<TConcrete> To<TConcrete>(TConcrete implementation)
        where TConcrete : TContract, IService
    {
        _di.Add(typeof(TContract), implementation);
        return new FromServiceBindGeneric<TConcrete>(_di, _allServices);
    }
}