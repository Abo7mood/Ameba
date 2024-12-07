using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditiorManager : MonoBehaviour
{
    public static EditiorManager instance;

    public bool Die;
    private void Awake()
    {
        if (!Application.isEditor) Destroy(this.gameObject);
        init();
    }
    private void init()
    {
        if (instance == null)
            instance = this;
        else
        {
            if (instance != this)
            {
                Destroy(instance);
                instance = this;
            }
            else
            {
                instance = this;
            }

        }
    }
    private void Start()
    {
        
    }
}
