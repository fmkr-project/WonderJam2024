using Managers;
using TMPro;
using UnityEngine;
using Upgrades;

public class RebirthUpgradeButton : MonoBehaviour
{
    public string upgradeName;
    public TMP_Text tmpText;
    public int targetValue;

    public void OnButtonClick()
    {
        if (int.TryParse(tmpText.text, out int tmpValue))
        {
            GameManager.ether += 200;
            targetValue = GameManager.ether;
            if (targetValue >= tmpValue)
            {
                GameManager.ether -= tmpValue;
                RebirthUpgrade newUpgrade = new RebirthUpgrade
                {
                    Name = upgradeName
                };


                GameManager.RebirthUpgrades.Add(newUpgrade);

                Debug.Log($"Upgrade ajouté : {upgradeName}");
            }
        }
    }
}
        
       
    
