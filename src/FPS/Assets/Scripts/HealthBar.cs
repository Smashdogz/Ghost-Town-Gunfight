using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
//controls the appearance of the health bar

    public Slider slider;

  public void SetHealth(int health)
  {
      slider.value = health;

  }
}
