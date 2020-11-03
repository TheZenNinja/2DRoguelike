using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZenUtil;

public class HotbarSlotUI : MonoBehaviour
{
    public Image cooldown;
    public Image bg;
    public Image sprite;

    public void UpdateCooldown(Timer timer)
    {
        cooldown.fillAmount = timer.finished ? 0 : 1 - timer.percent;
    }

}
