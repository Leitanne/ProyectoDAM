using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Barras")]
    [SerializeField] private Image playerHealth;
    [SerializeField] private Image playerMana;
    [SerializeField] private Image playerExp;

    [Header ("Stats")] 
    [SerializeField] private CharacterStats  stats;

    [Header ("Paneles")]
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject questInspectorPanel;
    [SerializeField] private GameObject questCharacterPanel;

    [Header ("Texto")]
    [SerializeField] private TextMeshProUGUI healthTMP;
    [SerializeField] private TextMeshProUGUI manaTMP;
    [SerializeField] private TextMeshProUGUI expTMP;
    [SerializeField] private TextMeshProUGUI levelTMP;
    [SerializeField] private TextMeshProUGUI coinTMP;

    [SerializeField] private TextMeshProUGUI attackStatTMP;
    [SerializeField] private TextMeshProUGUI defenseStatTMP;
    [SerializeField] private TextMeshProUGUI critStatTMP;
    [SerializeField] private TextMeshProUGUI blockStatTMP;
    [SerializeField] private TextMeshProUGUI speedStatTMP;
    [SerializeField] private TextMeshProUGUI levelStatTMP;
    [SerializeField] private TextMeshProUGUI currentExpStatTMP;
    [SerializeField] private TextMeshProUGUI requiredExpStatTMP;
    [SerializeField] private TextMeshProUGUI strengthAttributeTMP;
    [SerializeField] private TextMeshProUGUI intelligenceAttributeTMP;
    [SerializeField] private TextMeshProUGUI dextrityAttributeTMP;
    [SerializeField] private TextMeshProUGUI avaliableAttributePointsTMP;

    private float currentHealth;
    private float maxHealth;
    private float currentMana;
    private float maxMana;
    private float currentExp;
    private float goalExp;

    // Update is called once per frame
    void Update()
    {
        UpdateCharacterUI();
        UpdateStatsPanel();
    }

    //Usamos interpolacion lineal para las barras de vida y mana.
    private void UpdateCharacterUI()
    {
        playerHealth.fillAmount = Mathf.Lerp(playerHealth.fillAmount, 
            currentHealth / maxHealth, 10f * Time.deltaTime);

        playerMana.fillAmount = Mathf.Lerp(playerMana.fillAmount, 
            currentMana / maxMana, 10f * Time.deltaTime);

        playerExp.fillAmount = Mathf.Lerp(playerExp.fillAmount,
            currentExp / goalExp, 10f * Time.deltaTime);

        healthTMP.text = $"{currentHealth}/{maxHealth}";
        manaTMP.text = $"{currentMana}/{maxMana}";
        expTMP.text = $"{((currentExp/goalExp)*100):F2}%";
        levelTMP.text = $"Nivel {stats.level}";
        coinTMP.text = CoinManager.Instance.TotalCoins.ToString();
    }

    private void UpdateStatsPanel()
    {
        if(statsPanel.activeSelf == false)
        {
            return;
        }
        
        attackStatTMP.text                  = stats.attack.ToString();
        defenseStatTMP.text                 = stats.defense.ToString();
        critStatTMP.text                    = $"{stats.critChance}%";
        blockStatTMP.text                   = $"{stats.blockChance}%";
        speedStatTMP.text                   = stats.speed.ToString();
        levelStatTMP.text                   = stats.level.ToString();
        currentExpStatTMP.text              = stats.currentExp.ToString();
        requiredExpStatTMP.text             = stats.requiredExp.ToString();

        strengthAttributeTMP.text           = stats.strength.ToString();
        intelligenceAttributeTMP.text       = stats.intelligence.ToString();
        dextrityAttributeTMP.text           = stats.dextrity.ToString();
        avaliableAttributePointsTMP.text    = $"Puntos: {stats.avaliablePoints}";
    }

    //Entrada de valores de vida.
    public void UpdateCharacterHealth(float inCurrentHealth, float inMaxHealth)
    {
        currentHealth = inCurrentHealth;
        maxHealth = inMaxHealth;
    }

    //Entrada de valores de mana.
    public void UpdateCharacterMana(float inCurrentMana, float inMaxMana)
    {
        currentMana = inCurrentMana;
        maxMana = inMaxMana;
    }

    public void UpdateCharacterExperience(float inCurrentExp, float inGoalExp)
    {
        currentExp = inCurrentExp;
        goalExp = inGoalExp;
    }

    #region Panels

    public void OpenCloseStatsPanel()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
    }

    public void OpenCloseInventoryPanel()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void OpenCloseQuestInspectorPanel()
    {
        questInspectorPanel.SetActive(!questInspectorPanel.activeSelf);
    }

    public void OpenCloseQuestCharacterPanel()
    {
        questCharacterPanel.SetActive(!questCharacterPanel.activeSelf);
    }

    public void OpenCloseShopPanel()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
    public void OpenInteractionPanel(extraInteractionNPC typeInteraction)
    {
        switch(typeInteraction)
        {
            case extraInteractionNPC.Quest:
                OpenCloseQuestInspectorPanel();
                break;
            case extraInteractionNPC.Crafting:
                break;
            case extraInteractionNPC.Shop:
                OpenCloseShopPanel();
                break;
        }
    }

    #endregion
}
