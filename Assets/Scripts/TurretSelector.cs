using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TurretSelector : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler // Implement both interfaces
{
    public TextMeshProUGUI descriptionText;
    public int towerIndex; // Add this line

    private string[] towerDescriptions = {
        "COST : 50 | Attacks and consumes viruses it comes into contact with. It's a basic but essential defense, effective against common viral enemies.",
        "COST : 100 | T-Cel agitates the immune system and slows Nearby Enemies in range.",
        "COST : 50 | Temporarily enhance the body's immune system, blocking parts of the paths for all enemies.",
        "COST : 100 | Macrophage Attacks and consumes viruses and mutated cells, it deals heavy damage but have a long reload.",
        "COST : 50 | Dendric Cell is an antigen-presenting cell, it enhances nearby towers' attack powers for a period when the enemy is in range."

    };

    public void OnPointerEnter(PointerEventData eventData)
    {
        UpdateDescription(towerIndex);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BuildManager.main.selectedTower = towerIndex;
    }

    private void UpdateDescription(int towerIndex)
    {
        if (towerIndex >= 0 && towerIndex < towerDescriptions.Length)
        {
            descriptionText.text = towerDescriptions[towerIndex];
        }
    }
}
