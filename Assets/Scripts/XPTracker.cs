using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPTracker : MonoBehaviour
{
    [SerializeField] private BaseXPTranslation XpTranslationType;
    private BaseXPTranslation _xpTranslation;
    private void Awake()
    {
        _xpTranslation = ScriptableObject.Instantiate(XpTranslationType);
    }

    public void AddXP(int amout)
    {
        _xpTranslation.AddXP(amout);
    }
    
    public void SetLevel(int level)
    {
        _xpTranslation.SetLevel(level);
    }
    
    public 

    // Update is called once per frame
    void Update()
    {
        
    }
}
