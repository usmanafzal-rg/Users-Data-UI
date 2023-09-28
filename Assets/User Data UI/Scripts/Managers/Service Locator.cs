using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private Dictionary<Type, IService> _services = new Dictionary<Type, IService>();
    public static ServiceLocator Instance;

    public void Initialize()
    {
        if(Instance != null)
            Destroy(gameObject);
        Instance = this;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform trans = gameObject.transform.GetChild(i);
            if (trans.gameObject.TryGetComponent<IService>(out IService service))
            {
                _services.Add(service.GetType(),service);
            }
        }   
    }

    public TService Get<TService>()
    {
        if(_services.TryGetValue(typeof(TService), out IService service))
        {
            return (TService)service;
        }
        return default(TService);
    }
}
