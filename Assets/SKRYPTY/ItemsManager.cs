/*using UnityEngine;
using TMPro;
using System;


public class ItemsManager : MonoBehaviour
{
    [HideInInspector] public LevelManager levelManager;
    [HideInInspector] public CoinCollector coinCollector;
    [HideInInspector] public SkillsUpgradeManager shopPanel;
    public GameObject SHOPcanvas;

    public TextMeshProUGUI[] gemCounterHEROText;
    public TextMeshProUGUI[] gemValuesHEROText;
    // public TextMeshProUGUI gemCounterHERO2;
    // public TextMeshProUGUI gemCounterHERO3;
    //public TextMeshProUGUI gemCounterHERO4;
    [HideInInspector] public float chestGemDrop = 0.1f;
    public GameObject[] gems;
    // public int[] gemCounterHERO = new int[4];
    [HideInInspector] public int gemCounterHERO1 =0;
    [HideInInspector] public int gemCounterHERO2 =0;
    [HideInInspector] public int gemCounterHERO3 =0;
    [HideInInspector] public int gemCounterHERO4 =0;
    [HideInInspector] public int gemCounterHERO5 =0;
    [HideInInspector] public int gemCounterHERO6 =0;

    // public GameObject shopMenuPanelHERO1;
    // public GameObject shopMenuPanelHERO2;
    //public GameObject shopMenuPanelHERO3;
    // public GameObject shopMenuPanelHERO4;

    public GameObject inventoryCanvas;
    public Transform[] inventorySlots;
    public GameObject[] itemIcons;
    public GameObject statsCanvas;

    public GameObject[] HEROpanels;

    //  public float additionalCritMultiplierHERO2 = 0f;
    [HideInInspector] public static bool knockbackWeaknessHERO2;

  //  public GameObject[] itemsPrefabsComon;
   // public GameObject[] itemsPrefabsRare;
   // public GameObject[] itemsPrefabsLegendary;
  //  [HideInInspector] public int uzyteSloty = 0;
    //public GameObject tak;
    public GameObject itemToDrop;

    //  public bool czyJuzWypadl = false;
    /*
    [HideInInspector] public bool comonItem1;
    [HideInInspector] public bool comonItem2;
    [HideInInspector] public bool comonItem3;
    [HideInInspector] public bool comonItem4;
    [HideInInspector] public bool comonItem5;
    [HideInInspector] public bool comonItem6;

    [HideInInspector] public bool rareItem1;
    [HideInInspector] public bool rareItem2;
    [HideInInspector] public bool rareItem3;
    [HideInInspector] public bool rareItem4;
    [HideInInspector] public bool rareItem5;

    [HideInInspector] public bool legendaryItem1;
    [HideInInspector] public bool legendaryItem2;
    *//*
    [HideInInspector] public int gemToDrop =0 ;

  [HideInInspector]  public float HERO4QuantityEmisjaNaSec;

    [HideInInspector] public float itemPowiekszeniePociskuHERO1 = 0f; // ?? ????????????przy wdrazaniu
    [HideInInspector] public float itemKnockbackPociskuHERO1 = 0f; //DONE
    [HideInInspector] public float itemPiercePociskuHERO1 = 0f; //DONE
    [HideInInspector] public float itemSpeedPociskuHERO1 = 0f; //DONE 
    [HideInInspector] public float itemAttackSpeedPociskuHERO1 = 0f; //DONE
    [HideInInspector] public float itemDamagePociskuHERO1 = 0f;  //DONE

    [HideInInspector] public float itemDamageSwordHERO2 = 0f;  //DONE
    [HideInInspector] public float itemSwordKnockbackHERO2 = 0f; // DONE
    [HideInInspector] public float itemAttackSpeedSwordHERO2 = 0f; // DONE DOGŁĘBNIE SPRAWDZIĆ ????????????
    [HideInInspector] public float itemCritChanceSwordHERO2 = 0f;    //DONE
    [HideInInspector] public float itemCritMultiplierSwordHERO2 = 0f;  //DONE

    // public float KosztLife;

    [HideInInspector] public float itemCanoonDamageHERO3 = 0f;    //DONE
    [HideInInspector] public float itemCanoonAttackSpeedHERO3 = 0f;  //DONE
    [HideInInspector] public float itemCanoonLifeTimeHERO3 = 0f; //DONE
    [HideInInspector] public float itemCanoonMaxNumberHERO3 = 0f; //DONE
    [HideInInspector] public float itemCanoonTimeToBuildHERO3 = 0f;  //Done
    [HideInInspector] public float itemCanoonBulletSizeHERO3 = 0f;  // ?? ????????????przy wdrazaniu

    [HideInInspector] public float itemDamageHERO4 = 0f;    //DONE
    [HideInInspector] public float itemBeamSizeHERO4 = 0f;   //DONE 0.1 = +10% etc.
    [HideInInspector] public float itemBeamStrenghtHERO4 = 0f; //DONE 0.1 = +10% etc.
    [HideInInspector] public float itemQuatityHERO4 = 0f;  //DONE SPRAWDZIC CZY DZIALA WGL 0.1 = +10%, emisja 5 razy na sekunde, dokładność co 5 dodatkowych wiżzek
    [HideInInspector] public float itemEnergyRegenerationHERO4 = 0f;  //DONE 0.1 = 10%

    [HideInInspector] public float itemDamageHERO5 = 0f;    //DONE
    [HideInInspector] public float itemAttackSpeedHERO5 = 0f;    //DONE
    [HideInInspector] public float itemParticlekSpeedHERO5 = 0f;   //DONE
    [HideInInspector] public float itemParticlekLifeHERO5 = 0f;   //DONE
    [HideInInspector] public float itemParticlekBurstHERO5 = 0f;    //DONE


    [HideInInspector] public float itemDamageHERO6 = 0f; //DONE
    [HideInInspector] public float itemAttackSpeedHERO6 = 0f; //DONE
    [HideInInspector] public float itemProjectileSpeedHERO6 = 0f; //DONE 
    [HideInInspector] public float itemSizeMultiplierHERO6 = 0f;  // DONE ??

    /*
    public Material comonItemMaterial;
    public Material rareItemMaterial;
    public Material legItemMaterial;*//*
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        coinCollector = FindObjectOfType<CoinCollector>();
        shopPanel = FindObjectOfType<SkillsUpgradeManager>();

        chestGemDrop = 0.0f;
    }


    public void GemDrop(Vector2 dropPosition, float chance)
    {
        var ranNumb = UnityEngine.Random.Range(1, 10001);

        if (ranNumb <= chance)
        {

                gemToDrop = UnityEngine.Random.Range(0, gems.Length);
                itemToDrop = Instantiate(gems[gemToDrop], dropPosition, gameObject.transform.rotation);
             /*   gemCounterHERO[gemToDrop]++;
                gemCounterHEROText[gemToDrop].text = ""+gemCounterHERO[gemToDrop];*/
                
         /*   
        }
    }

    public void RefreashGemValuesTexts()
    {
        gemValuesHEROText[0].text = "+"+ itemDamagePociskuHERO1 + "\n" + "+" + itemKnockbackPociskuHERO1 + "\n" + "+" + itemSpeedPociskuHERO1*100+ "%\n" + "+" + itemAttackSpeedPociskuHERO1 * 100 + "%";
        gemValuesHEROText[1].text = "+" + itemDamageSwordHERO2 + "\n" + "+" + itemSwordKnockbackHERO2 + "\n" + "+" + itemAttackSpeedSwordHERO2 * 100 + "%\n" + "+" + itemCritChanceSwordHERO2+ "%\n"+"+"+itemCritMultiplierSwordHERO2*100+"%";
        gemValuesHEROText[2].text = "+" + itemCanoonDamageHERO3 + "\n" + "+" + itemCanoonAttackSpeedHERO3*100 + "%\n" + "+" + itemCanoonLifeTimeHERO3 + "s\n" + "-" +itemCanoonTimeToBuildHERO3*100 + "%";
        gemValuesHEROText[3].text = "+" + itemDamageHERO4 + "\n" + "+" + itemBeamSizeHERO4*100 + "%\n" + "+" + itemBeamStrenghtHERO4*100 + "%\n" + "+"+ itemQuatityHERO4*100 + "%\n"+"+"+itemEnergyRegenerationHERO4*100+"%";
        gemValuesHEROText[4].text = "+" + itemDamageHERO5 + "\n" + "+" + itemAttackSpeedHERO5*100 + "%\n" + "+" + itemParticlekLifeHERO5*100 + "%\n" + "+"+ itemParticlekSpeedHERO5*100 + "%\n"+"+"+itemParticlekBurstHERO5*100+"%";
        gemValuesHEROText[5].text = "+" + itemDamageHERO6 + "\n" + "+" + itemAttackSpeedHERO6*100 + "%\n" + "+" + itemProjectileSpeedHERO6*100 + "%\n" + "+"+ itemSizeMultiplierHERO6*100 + "%";

    }

    public void AddGemProfits(string gemName)
    {
        switch (gemName)
        {
            case "HERO1_GemStone(Clone)": 
                
                int wchihBonus = UnityEngine.Random.Range(1, 5);
                int bonusWelth = UnityEngine.Random.Range(1, 6);
               // Debug.Log("Wchih BONUS: "+ wchihBonus+"   bonus WELTH: "+bonusWelth);
                if (wchihBonus == 1) { itemKnockbackPociskuHERO1 += bonusWelth; }
                else if (wchihBonus == 2) { itemSpeedPociskuHERO1 += ((float)bonusWelth) / 100f; }
                else if (wchihBonus == 3) { itemAttackSpeedPociskuHERO1 += ((float)bonusWelth)/ 100f; }
                else if (wchihBonus == 4) { bonusWelth = UnityEngine.Random.Range(1, 4); itemDamagePociskuHERO1 += bonusWelth; }
                gemCounterHERO1++;
             //   gemCounterHEROText[0].text = "" + gemCounterHERO1;

                break;

            case "HERO2_GemStone(Clone)":

                int wchihBonus1 = UnityEngine.Random.Range(1, 6);
                int bonusWelth1 = UnityEngine.Random.Range(1, 6);
              //  Debug.Log("Wchih BONUS: " + wchihBonus1 + "   bonus WELTH: " + bonusWelth1);

                if (wchihBonus1 == 1) {bonusWelth1 = UnityEngine.Random.Range(1, 3); itemSwordKnockbackHERO2 += bonusWelth1; }
                else if (wchihBonus1 == 2) { bonusWelth1 = UnityEngine.Random.Range(2, 5); itemDamageSwordHERO2 += bonusWelth1; }
                else if (wchihBonus1 == 3) { itemAttackSpeedSwordHERO2 += ((float)bonusWelth1) / 100f; }
                else if (wchihBonus1 == 4) { bonusWelth1 = UnityEngine.Random.Range(1, 3); itemCritChanceSwordHERO2 += bonusWelth1; }
                else if (wchihBonus1 == 5) { bonusWelth1 = UnityEngine.Random.Range(1, 6); itemCritMultiplierSwordHERO2 += ((float)bonusWelth1) / 100f; }
                gemCounterHERO2++;
             //  gemCounterHEROText[1].text = "" + gemCounterHERO2;

                break;

            case "HERO3_GemStone(Clone)":

                int wchihBonus2 = UnityEngine.Random.Range(1, 5);
                int bonusWelth2 = UnityEngine.Random.Range(1, 6);
               // Debug.Log("Wchih BONUS: " + wchihBonus2 + "   bonus WELTH: " + bonusWelth2);

                if (wchihBonus2 == 1) { bonusWelth2 = UnityEngine.Random.Range(1, 2); itemCanoonDamageHERO3 += bonusWelth2; }
                else if (wchihBonus2 == 2) { itemCanoonAttackSpeedHERO3 += ((float)bonusWelth2) / 100f; }
                else if (wchihBonus2 == 3) { float welth = UnityEngine.Random.Range((float)0.1, (float)0.4) ; welth = (float)Math.Round(welth,2); itemCanoonLifeTimeHERO3+= welth; }
                else if (wchihBonus2 == 4) { bonusWelth2 = UnityEngine.Random.Range(1, 6); itemCanoonTimeToBuildHERO3 += ((float)bonusWelth2) / 100f; }
                gemCounterHERO3++;
               // gemCounterHEROText[2].text = "" + gemCounterHERO3;

                break;

            case "HERO4_GemStone(Clone)":

                int wchihBonus3 = UnityEngine.Random.Range(1, 5);
                int bonusWelth3 = UnityEngine.Random.Range(1, 6);
               // Debug.Log("Wchih BONUS: " + wchihBonus3 + "   bonus WELTH: " + bonusWelth3);

                if (wchihBonus3 == 1) { float welth = UnityEngine.Random.Range((float)0.2, (float)0.8); welth = (float)Math.Round(welth, 2); itemDamageHERO4 += welth; }
                else if (wchihBonus3 == 2) { itemQuatityHERO4 += ((float)bonusWelth3) / 100f; }
                else if (wchihBonus3 == 3) { itemBeamSizeHERO4 += ((float)bonusWelth3) / 100f; }
                else if (wchihBonus3 == 4) {itemBeamStrenghtHERO4 += ((float)bonusWelth3) / 100f; }
                gemCounterHERO4++;
             //  gemCounterHEROText[3].text = "" + gemCounterHERO4;

                break;

            case "HERO5_GemStone(Clone)":

                int wchihBonus4 = UnityEngine.Random.Range(1, 6);
                int bonusWelth4 = UnityEngine.Random.Range(1, 6);
                // Debug.Log("Wchih BONUS: " + wchihBonus3 + "   bonus WELTH: " + bonusWelth3);

                if (wchihBonus4 == 1) { float welth = UnityEngine.Random.Range((float)0.3, (float)1.0); welth = (float)Math.Round(welth, 2); itemDamageHERO5 += welth; }
                else if (wchihBonus4 == 2) { itemAttackSpeedHERO5 += ((float)bonusWelth4) / 100f; }
                else if (wchihBonus4 == 3) { itemParticlekLifeHERO5 += ((float)bonusWelth4) / 100f; }
                else if (wchihBonus4 == 4) { itemParticlekSpeedHERO5 += ((float)bonusWelth4) / 100f; }
                else if (wchihBonus4 == 5) { itemParticlekBurstHERO5 += ((float)bonusWelth4) / 100f; }
                gemCounterHERO5++;
              //  gemCounterHEROText[4].text = "" + gemCounterHERO5;

                break;

            case "HERO6_GemStone(Clone)":

                int wchihBonus5 = UnityEngine.Random.Range(1, 5);
                int bonusWelth5 = UnityEngine.Random.Range(1, 5);
                // Debug.Log("Wchih BONUS: " + wchihBonus3 + "   bonus WELTH: " + bonusWelth3);

                if (wchihBonus5 == 1) { float welth = UnityEngine.Random.Range((float)0.8, (float)1.2); welth = (float)Math.Round(welth, 2); itemDamageHERO6 += welth; }
                else if (wchihBonus5== 2) { itemAttackSpeedHERO6 += ((float)bonusWelth5) / 100f; }
                else if (wchihBonus5 == 3) { itemProjectileSpeedHERO6 += ((float)bonusWelth5) / 100f; }
                else if (wchihBonus5 == 4) { itemSizeMultiplierHERO6 += ((float)bonusWelth5) / 100f; }
                gemCounterHERO6++;
               // gemCounterHEROText[5].text = "" + gemCounterHERO6;

                break;

          

        }

       
    }
    /*
    public void ItemDrop(Vector2 dropPosition, float chance)
    {
        var ranNumb = UnityEngine.Random.Range(1, 10001);

        if (ranNumb <= chance)
        {
            var ranNumb2 = UnityEngine.Random.Range(1, 21);
            // var ranNumb3 = Random.Range(0,4); // zakladając że wszedzie są 5 przedmioty
            if (ranNumb2 <= 14)
            {
                itemToDrop = Instantiate(itemsPrefabsComon[UnityEngine.Random.Range(0, itemsPrefabsComon.Length)], dropPosition, gameObject.transform.rotation);
            }
            else if (ranNumb2 >= 15 && ranNumb2 <= 19)
            {
                itemToDrop = Instantiate(itemsPrefabsRare[UnityEngine.Random.Range(0, itemsPrefabsRare.Length)], dropPosition, gameObject.transform.rotation);
            }
            else
            {
                itemToDrop = Instantiate(itemsPrefabsLegendary[UnityEngine.Random.Range(0, itemsPrefabsLegendary.Length)], dropPosition, gameObject.transform.rotation);
            }
        }

    }

    public void CheckItemDropped(string nameOfItem, Vector2 itemPosition)
    {
        Vector2 itemPozycja = itemPosition;
        coinCollector = FindObjectOfType<CoinCollector>();
        Debug.Log("DROPNELOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO:   " + nameOfItem);
        switch (nameOfItem)
        {
            case "comonItem1(Clone)":
                if (comonItem1 == false)
                {
                    // +6 DAMAGE SWORD i + 10% ATTACK SPEED SWORD Player2
                    comonItem1 = true;
                    //  levelManager.damageMiecza += 6f;
                    itemDamageSwordHERO2 += 6f;

                    // levelManager.timeToAttack = levelManager.timeToAttackStala / levelManager.attspeedIncValue;
                    //  levelManager.attackSpeed = levelManager.attackspeedStala * levelManager.attspeedIncValue;
                    //  levelManager.attspeedIncValue += 0.25f;
                    itemAttackSpeedSwordHERO2 += 0.10f;

                  //  levelManager.LvlAttackSpeedSword += 1;
                  //  levelManager.LvlDamageSword += 2f;

                    AddToInventory(0, uzyteSloty, 1);


                }
                else coinCollector.ItemToCoins(1, itemPozycja);
                Debug.Log("CZYYYYYYYYYYYYYYYYYYYYYYYYYY   " + comonItem1);
                break;
            case "comonItem2(Clone)":
                if (comonItem2 == false)
                {
                    //Wydłuza czas ochorny po skuciu o 2 sekundyu All
                    comonItem2 = true;
                    levelManager.respShieldStala += 2f;

                    AddToInventory(1, uzyteSloty, 1);


                }
                else coinCollector.ItemToCoins(1, itemPozycja);
                Debug.Log("CZYYYYYYYYYYYYYYYYYYYYYYYYYY   " + comonItem2);
                break;
            case "comonItem3(Clone)":
                if (comonItem3 == false)
                {
                    // -10% TimeTo build
                    comonItem3 = true;
                    //  for (int i = 0; i < 4; i++)
                    //  {
                    //     levelManager.timeToBuildCanoon *= 0.95f;
                    // }
                    //  levelManager.LvlCanoonTimeToBuild += 4f;
                    itemCanoonTimeToBuildHERO3 += 0.10f;

                    AddToInventory(2, uzyteSloty, 1);


                }
                else coinCollector.ItemToCoins(1, itemPozycja);
                Debug.Log("CZYYYYYYYYYYYYYYYYYYYYYYYYYY   " + comonItem3);
                break;
            case "comonItem4(Clone)":
                if (comonItem4 == false)
                {
                    // -5% TimeToBuild, +5% AttackSpeed, +1 s CanoonLifeTime = Player3
                    comonItem4 = true;
                    //    levelManager.timeToBuildCanoon *= 0.95f;
                    itemCanoonTimeToBuildHERO3 += 0.05f;
                    //   levelManager.canoonAttackSpeed *= 0.95f;
                    itemCanoonAttackSpeedHERO3 += 0.05f;
                    //  levelManager.fireRateCanoon *= 0.95f;

                    //  levelManager.canoonLifeTime += 1f;
                    itemCanoonLifeTimeHERO3 += 1f;

                 //   levelManager.LvlCanoonTimeToBuild++;
                  //  levelManager.LvlCanoonAttackSpeed++;
                  //  levelManager.LvlCanoonLifeTime++;

                    AddToInventory(3, uzyteSloty, 1);



                }
                else coinCollector.ItemToCoins(1, itemPozycja);
                Debug.Log("CZYYYYYYYYYYYYYYYYYYYYYYYYYY   " + comonItem4);
                break;
            case "comonItem5(Clone)":
                if (comonItem5 == false)
                {
                    // +4  damage, +10% attackSpeed = Player1
                    comonItem5 = true;
                    //  levelManager.damagePociskuHERO1 += 4f;
                    itemDamagePociskuHERO1 += 4f;

                  //  levelManager.HERO1attackSpeed *= 0.95f;
                  itemAttackSpeedPociskuHERO1  += 0.10f;
                 //   levelManager.LvlDamagePocisku += 2f;
                   // levelManager.LvlAttackSpeedPocisku++;

                    AddToInventory(4, uzyteSloty, 1);



                }
                else coinCollector.ItemToCoins(1, itemPozycja);
                Debug.Log("CZYYYYYYYYYYYYYYYYYYYYYYYYYY   " + comonItem5);
                break;
            case "comonItem6(Clone)":
                if (comonItem6 == false)
                {
                    // +1 Walking speed = ALL
                    comonItem6 = true;
                    levelManager.walkSpeed++;

                    AddToInventory(5, uzyteSloty, 1);


                }
                else coinCollector.ItemToCoins(1, itemPozycja);
                Debug.Log("CZYYYYYYYYYYYYYYYYYYYYYYYYYY   " + comonItem6);
                break;
            case "rareItem1(Clone)":
                if (rareItem1 == false)
                {
                    // +6  damage pocisku oraz +12% predkość pocisku Player1
                    rareItem1 = true;
                    //   levelManager.damagePociskuHERO1 += 6f;
                    itemDamagePociskuHERO1 += 6f;

                    //  levelManager.HERO1predkoscPocisku += 120f;
                    itemSpeedPociskuHERO1 += 0.12f;
                   // levelManager.LvlDamagePocisku += 3f;
                  //  levelManager.LvlSpeedPocisku += 4f;

                    AddToInventory(6, uzyteSloty, 2);

                }
                else coinCollector.ItemToCoins(2, itemPozycja);
                Debug.Log("CZYYYYYYYYYYYYYYYYYYYYYYYYYY   " + rareItem1);
                break;
            case "rareItem2(Clone)":
                if (rareItem2 == false)
                {
                    // + 10% critchance oraz + 10% crit multiplier Player2
                    rareItem2 = true;
                    //  levelManager.critChanceHERO2 += 10f;
                    itemCritChanceSwordHERO2 += 10f;
                    itemCritMultiplierSwordHERO2 += 0.10f;
                   // levelManager.LvlCritChanceSword += 5f;

                    AddToInventory(7, uzyteSloty, 2);

                }
                else coinCollector.ItemToCoins(2, itemPozycja);
                Debug.Log("CZYYYYYYYYYYYYYYYYYYYYYYYYYY   " + rareItem2);
                break;
            case "rareItem3(Clone)":
                if (rareItem3 == false)
                {
                    // +12 damage = Player1
                    rareItem3 = true;
                    itemDamagePociskuHERO1 += 12f;
                    //levelManager.LvlDamagePocisku += 6f;

                    AddToInventory(8, uzyteSloty, 2);

                }
                else coinCollector.ItemToCoins(2, itemPozycja);
                Debug.Log("CZYYYYYYYYYYYYYYYYYYYYYYYYYY   " + rareItem3);
                break;
            case "rareItem4(Clone)":
                if (rareItem4 == false)
                {
                    // +6 damage, +5% attackSpeed = Player3
                    rareItem4 = true;
                    //    levelManager.canonDamage += 4f;
                    itemCanoonDamageHERO3 += 6f;
                    //levelManager.canoonAttackSpeed *= 0.95f;
                    itemCanoonAttackSpeedHERO3 += 0.05f;
                  //  levelManager.fireRateCanoon *= 0.95f;
                  
                 //   levelManager.canonDamage += 4f;
                   // levelManager.LvlCanoonAttackSpeed++;

                    AddToInventory(9, uzyteSloty, 2);

                }
                else coinCollector.ItemToCoins(2, itemPozycja);
                Debug.Log("CZYYYYYYYYYYYYYYYYYYYYYYYYYY   " + rareItem4);
                break;
            case "rareItem5(Clone)":
                if (rareItem5 == false)
                {
                    // +9 damage, +5% attackspeed, +5% CritMultiplier = Player2
                    rareItem5 = true;
                    // levelManager.damageMiecza += 9f;
                    itemDamageSwordHERO2 += 9f;
                    //levelManager.timeToAttack = levelManager.timeToAttackStala / levelManager.attspeedIncValue;
                    //   levelManager.attackSpeed = levelManager.attackspeedStala * levelManager.attspeedIncValue;
                    //  levelManager.attspeedIncValue += 0.25f;
                    itemAttackSpeedSwordHERO2 += 0.05f;
                    itemCritMultiplierSwordHERO2 += 0.05f;

                  //  levelManager.LvlDamageSword += 3f;
                  //  levelManager.LvlAttackSpeedSword++;

                    AddToInventory(10, uzyteSloty, 2);

                }
                else coinCollector.ItemToCoins(2, itemPozycja);
                Debug.Log("CZYYYYYYYYYYYYYYYYYYYYYYYYYY   " + rareItem5);
                break;
            case "legendaryItem1(Clone)":
                if (legendaryItem1 == false)
                {
                    // +10 damagfe oraz +10% canon attack speed Player3
                    legendaryItem1 = true;
                    //  levelManager.canonDamage += 10f;
                    itemCanoonDamageHERO3 += 10f;
                    //  for (int i = 0; i < 2; i++)
                    // {
                    //      levelManager.canoonAttackSpeed *= 0.95f;
                    //      levelManager.fireRateCanoon *= 0.95f;
                    //  }
                    itemCanoonAttackSpeedHERO3 += 0.10f;
                  //  levelManager.LvlCanoonDamage += 10f;
                  //  levelManager.LvlCanoonAttackSpeed += 2f;

                    AddToInventory(11, uzyteSloty, 3);


                }
                else coinCollector.ItemToCoins(5, itemPozycja);
                Debug.Log("CZYYYYYYYYYYYYYYYYYYYYYYYYYY   " + legendaryItem1);
                break;
            case "legendaryItem2(Clone)":
                if (legendaryItem2 == false)
                {
                    // +25% CritMultiplier, +8% CritChance, +9 damge, +10%  AttackSpeed, knockback/2 = Player2 
                    legendaryItem2 = true;
                    itemCritMultiplierSwordHERO2 += 0.25f;
                    //  levelManager.critChanceHERO2 += 8f;
                    itemCritChanceSwordHERO2 += 8f;
                    //  levelManager.damageMiecza += 9f;
                    itemDamageSwordHERO2 += 9f;

                    // for (int i = 0; i < 2; i++)
                    //  {
                    //      levelManager.timeToAttack = levelManager.timeToAttackStala / levelManager.attspeedIncValue;
                    //     levelManager.attackSpeed = levelManager.attackspeedStala * levelManager.attspeedIncValue;
                    //     levelManager.attspeedIncValue += 0.25f;
                    //  }
                    itemAttackSpeedSwordHERO2 += 0.10f;
                    knockbackWeaknessHERO2 = true;

                   // levelManager.LvlCritChanceSword += 4f;
                   // levelManager.LvlDamageSword += 3f;
                   // levelManager.LvlAttackSpeedSword += 2f;

                    AddToInventory(12, uzyteSloty, 3);

                }
                else coinCollector.ItemToCoins(5, itemPozycja);
                Debug.Log("CZYYYYYYYYYYYYYYYYYYYYYYYYYY   " + legendaryItem2);
                break;
        }



    }

    public void AddToInventory(int coNrTablicy, int gdzieNrTablicy, int rarity)
    {

        var tak = Instantiate(itemIcons[coNrTablicy], inventorySlots[gdzieNrTablicy].position, gameObject.transform.rotation, GameObject.FindGameObjectWithTag("InventoryCanvas").transform);
        uzyteSloty++;
        switch (rarity)
        {
            case 1: inventorySlots[gdzieNrTablicy].gameObject.GetComponent<Image>().material = comonItemMaterial; break;
            case 2: inventorySlots[gdzieNrTablicy].gameObject.GetComponent<Image>().material = rareItemMaterial; break;
            case 3: inventorySlots[gdzieNrTablicy].gameObject.GetComponent<Image>().material = legItemMaterial; break;
        }

    }*/
    /*
    public void HideCanvas()
    {
        inventoryCanvas.SetActive(false) ;
        Time.timeScale = 1.0f;
    }

    public void ShowCanvas()
    {
        if (statsCanvas.activeSelf)
            statsCanvas.SetActive(false);
        if (HEROpanels[0].activeSelf)
            HEROpanels[0].SetActive(false);
        if (HEROpanels[1].activeSelf)
            HEROpanels[1].SetActive(false);
        if (HEROpanels[2].activeSelf)
            HEROpanels[2].SetActive(false);
        if (HEROpanels[3].activeSelf)
            HEROpanels[3].SetActive(false);
        if (HEROpanels[4].activeSelf)
            HEROpanels[4].SetActive(false);
        if (HEROpanels[5].activeSelf)
            HEROpanels[5].SetActive(false);


      
            inventoryCanvas.SetActive(true);

            Time.timeScale = 0.0f;
       
    }

    public void CloseinventoryCanvas()
    {
        if (inventoryCanvas.activeSelf)
        {
            inventoryCanvas.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void DeactivateHeroCanvas()
    {

            HEROpanels[0].SetActive(false);

            HEROpanels[1].SetActive(false);

            HEROpanels[2].SetActive(false);

            HEROpanels[3].SetActive(false);

            HEROpanels[4].SetActive(false);

            HEROpanels[5].SetActive(false);

    }

    public void OpenStatsPanel()
    {

        statsCanvas.SetActive(true);
            Time.timeScale = 0.0f;
    
    }
    public void ClickButtonSound()
    {
        FindObjectOfType<AudioManager>().Play("ButtonPress");

    }

    public void TurnOffWrongPowerBar()
    {
        if (FindObjectOfType<Player>())
        {
            heroesManager.heroPowerBars[0].SetActive(true);
            heroesManager.heroPowerBars[1].SetActive(false);
            heroesManager.heroPowerBars[2].SetActive(false);
            heroesManager.heroPowerBars[3].SetActive(false);
            heroesManager.heroPowerBars[4].SetActive(false);
            heroesManager.heroPowerBars[5].SetActive(false);
        }
        else if (FindObjectOfType<Player2>())
        {
            heroesManager.heroPowerBars[0].SetActive(false);
            heroesManager.heroPowerBars[1].SetActive(true);
            heroesManager.heroPowerBars[2].SetActive(false);
            heroesManager.heroPowerBars[3].SetActive(false);
            heroesManager.heroPowerBars[4].SetActive(false);
            heroesManager.heroPowerBars[5].SetActive(false);
        }
        else if (FindObjectOfType<Player3>())
        {
            heroesManager.heroPowerBars[0].SetActive(false);
            heroesManager.heroPowerBars[1].SetActive(false);
            heroesManager.heroPowerBars[2].SetActive(true);
            heroesManager.heroPowerBars[3].SetActive(false);
            heroesManager.heroPowerBars[4].SetActive(false);
            heroesManager.heroPowerBars[5].SetActive(false);
        }
        else if (FindObjectOfType<Player4>())
        {
            heroesManager.heroPowerBars[0].SetActive(false);
            heroesManager.heroPowerBars[1].SetActive(false);
            heroesManager.heroPowerBars[2].SetActive(false);
            heroesManager.heroPowerBars[3].SetActive(true);
            heroesManager.heroPowerBars[4].SetActive(false);
            heroesManager.heroPowerBars[5].SetActive(false);
        }
        else if (FindObjectOfType<Player5>())
        {
            heroesManager.heroPowerBars[0].SetActive(false);
            heroesManager.heroPowerBars[1].SetActive(false);
            heroesManager.heroPowerBars[2].SetActive(false);
            heroesManager.heroPowerBars[3].SetActive(false);
            heroesManager.heroPowerBars[4].SetActive(true);
            heroesManager.heroPowerBars[5].SetActive(false);
        }
        else if (FindObjectOfType<Player6>())
        {
            heroesManager.heroPowerBars[0].SetActive(false);
            heroesManager.heroPowerBars[1].SetActive(false);
            heroesManager.heroPowerBars[2].SetActive(false);
            heroesManager.heroPowerBars[3].SetActive(false);
            heroesManager.heroPowerBars[4].SetActive(false);
            heroesManager.heroPowerBars[5].SetActive(true);
        }

        
    }

    public void ShowHeroUpgrades()
    {
        if (statsCanvas.activeSelf)
            statsCanvas.SetActive(false);

        SHOPcanvas.SetActive(true);

      //  shopPanel.StatsCostRefresh();
        shopPanel.HERO1StatsValuesRefresh();
        shopPanel.HERO2StatsValuesRefresh();
        shopPanel.HERO3StatsValuesRefresh();
        shopPanel.HERO4StatsValuesRefresh();
        shopPanel.HERO5StatsValuesRefresh();
        shopPanel.HERO6StatsValuesRefresh();

    //    if (levelManager.HeroDialogueFrame.activeSelf == false)
     //   {
            if (FindObjectOfType<Player>())
            {
                HEROpanels[0].SetActive(true);
                HEROpanels[1].SetActive(false);
                HEROpanels[2].SetActive(false);
                HEROpanels[3].SetActive(false);
                HEROpanels[4].SetActive(false);
                HEROpanels[5].SetActive(false);
                Time.timeScale = 0.0f;
            }
            else if (FindObjectOfType<Player2>())
            {
                HEROpanels[0].SetActive(false);
                HEROpanels[1].SetActive(true);
                HEROpanels[2].SetActive(false);
                HEROpanels[3].SetActive(false);
                HEROpanels[4].SetActive(false);
                HEROpanels[5].SetActive(false);
                Time.timeScale = 0.0f;
            }
            else if (FindObjectOfType<Player3>())
            {
                HEROpanels[0].SetActive(false);
                HEROpanels[1].SetActive(false);
                HEROpanels[2].SetActive(true);
                HEROpanels[3].SetActive(false);
                HEROpanels[4].SetActive(false);
                HEROpanels[5].SetActive(false);
                Time.timeScale = 0.0f;
            }
            else if (FindObjectOfType<Player4>())
            {
                HEROpanels[0].SetActive(false);
                HEROpanels[1].SetActive(false);
                HEROpanels[2].SetActive(false);
                HEROpanels[3].SetActive(true);
                HEROpanels[4].SetActive(false);
                HEROpanels[5].SetActive(false);
                Time.timeScale = 0.0f;
            }
            else if (FindObjectOfType<Player5>())
            {
                HEROpanels[0].SetActive(false);
                HEROpanels[1].SetActive(false);
                HEROpanels[2].SetActive(false);
                HEROpanels[3].SetActive(false);
                HEROpanels[4].SetActive(true);
                HEROpanels[5].SetActive(false);
                Time.timeScale = 0.0f;
            }
            else if (FindObjectOfType<Player6>())
            {
                HEROpanels[0].SetActive(false);
                HEROpanels[1].SetActive(false);
                HEROpanels[2].SetActive(false);
                HEROpanels[3].SetActive(false);
                HEROpanels[4].SetActive(false);
                HEROpanels[5].SetActive(true);
                Time.timeScale = 0.0f;
            }
     //   }
    }
}
*/