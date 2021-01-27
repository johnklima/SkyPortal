using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetpackBar : MonoBehaviour
{
   public Image _Image;
   public GameObject player;
   private PlayerScript playerController;
   public float maximum;
   public float minimum = 0f;
   public float current;

   private void Start()
   {
      playerController = player.GetComponent<PlayerScript>();
   }

   private void Update()
   {
      maximum = playerController.maxFuel;
      minimum = 0f;
      current = playerController.fuel;

      float currentOffset = current - minimum;
      float maximumOffset = maximum - minimum;
      float fillAmount = currentOffset / maximumOffset;
      _Image.fillAmount = fillAmount;
   }
}
