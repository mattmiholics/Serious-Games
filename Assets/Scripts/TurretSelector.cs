using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class TurretSelector : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler // Implement both interfaces
{
    public TextMeshProUGUI descriptionText;
    public int towerIndex; // Add this line

    private string[] towerDescriptions = {
        "Attacks and consumes viruses it comes into contact with. It's a basic but essential defense, effective against common viral enemies.",
        "T-Cel agitates the immune system and slows Nearby Enemies in range.",
        "Temporarily enhance the body's immune system, blocking parts of the paths for all enemies.",
        "Macrophage Attacks and consumes viruses and mutated cells, it deals heavy damage but have a long reload.",
        "Dendric Cell is an antigen-presenting cell, it enhances nearby towers' attack powers for a period when the enemy is in range."

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
