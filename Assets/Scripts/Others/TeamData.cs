using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamData:MonoBehaviour
{
    public List<CharacterBase> CharacterList = new List<CharacterBase>();

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
