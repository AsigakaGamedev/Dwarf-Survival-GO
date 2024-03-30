using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShortBuffItem : PoolableObject
{
    [SerializeField] private Image buffIconImg;
    [SerializeField] private Image buffSliderImg;
    [SerializeField] private Slider lifeTimeSlider;

    private BuffEntity buffEntity;

    public void SetBuff(BuffEntity buffEntity)
    {
        if (this.buffEntity != null)
        {
            this.buffEntity.onLifeTimeChange -= OnLifeTimeChange;
        }

        lifeTimeSlider.maxValue = buffEntity.LifeTime;
        lifeTimeSlider.value = buffEntity.LifeTime;
        buffIconImg.sprite = buffEntity.Data.Icon;
        buffSliderImg.sprite = buffEntity.Data.Icon;

        this.buffEntity = buffEntity;
        this.buffEntity.onLifeTimeChange += OnLifeTimeChange;
    }

    private void OnLifeTimeChange(float lifeTime)
    {
        lifeTimeSlider.value = lifeTime;
    }
}
