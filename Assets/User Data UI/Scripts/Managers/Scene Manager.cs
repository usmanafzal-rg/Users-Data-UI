using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour, IService
{
    public string GetName()
    {
        return "Scene Manager";
    }
}
