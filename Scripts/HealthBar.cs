using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
   
    public int hp = 100;
    //public bool isGameOver;

    void Start()
    {
        //isGameOver = false;
    }

    void Update()
    {
       
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);

    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

       fill.color = gradient.Evaluate(1f);
    }

    /*public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            isGameOver = true;

    }
    */
}
