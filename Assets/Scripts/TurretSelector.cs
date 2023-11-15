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
        "Enhances the power and range of nearby towers, representing the role of T-cells in coordinating the immune response"
        
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
