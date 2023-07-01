using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerProperties : MonoBehaviour
{
    public static PlayerProperties playerProp;
    #region Fields
    private float health = 1;
    private float stamina = 100;
    private float brainHealth = 100;
    #endregion
    #region Properties
    public float Health
    {
        get { return health; }
        set { health -= value; }
    }
    public float Stamina
    {
        get { return stamina; }
        set 
        {
            stamina = value;
            stamina = Mathf.Clamp(stamina, 0, 100);
            UIManager.uýManager.staminaBar.DOFillAmount(stamina / 100, 1).SetEase(Ease.Linear);
        }
    }
    public float BrainHealth
    {
        get { return brainHealth; }
        set
        {
            brainHealth = value;
            brainHealth = Mathf.Clamp(brainHealth, 0, 100);
            UIManager.uýManager.brainHealthBar.DOFillAmount(brainHealth / 100, 1).SetEase(Ease.Linear);
        }
    }
    #endregion
    private void Awake()
    {
        playerProp = this;
    }
}
