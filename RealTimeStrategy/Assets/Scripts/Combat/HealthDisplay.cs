using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private GameObject healthBarParent;
    [SerializeField] private Image healtBarImage;

    private void Awake()
    {
        health.ClientOnHealthUpdated += HandleHealthUpdate;
    }

    private void OnDestroy()
    {
        health.ClientOnHealthUpdated -= HandleHealthUpdate;
    }

    private void HandleHealthUpdate(int currentHealth, int maxHealth)
    {
        healtBarImage.fillAmount = (float)currentHealth / maxHealth;
    }
}
