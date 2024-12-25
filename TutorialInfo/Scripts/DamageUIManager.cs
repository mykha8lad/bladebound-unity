using UnityEngine;
using UnityEngine.UI;

public class DamageUIManager : MonoBehaviour
{
    public static DamageUIManager Instance;

    public GameObject damageTextPrefab;
    public Transform canvas;

    void Awake()
    {
        Instance = this;
    }
    
    public void ShowDamage(Vector3 worldPosition, string damageText)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        
        GameObject damageTextObj = Instantiate(damageTextPrefab, canvas);
        damageTextObj.transform.position = screenPosition;

        Text textComponent = damageTextObj.GetComponent<Text>();
        if (textComponent != null)
        {
            textComponent.text = damageText;
        }

        Destroy(damageTextObj, 1.5f); 
    }
}
