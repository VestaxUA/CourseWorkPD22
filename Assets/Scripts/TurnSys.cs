using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public void TurnSys()
{
    GameObject[] remainingEnemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");

    if (remainingEnemyUnits.Length == 0)
    {
        this.enemyEncounter.GetComponent<CollectReward>().collectReward();
        SceneManager.LoadScene("Town");
    }
    GameObject[] remainingPlayerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");

    if (remainingPlayerUnits.Length == 0)
    {
        SceneManager.LoadScene("Title");
    }
    UnitStats currentUnitStats = unitsStats[0];
    unitsStats.Remove(currentUnitStats);
    if (!currentUnitStats.isDead())
    {
        GameObject currentUnit = currentUnitStats.gameObject;
        currentUnitStats.calculateNextActTurn(currentUnitStats.nextActTurn);
        unitsStats.Add(currentUnitStats);
        unitsStats.Sort();
        if (currentUnit.tag == "PlayerUnit")
        {
            this.playerParty.GetComponent<SelectUnit>().selectCurrentUnit(currentUnit.gameObject);
        }
        else
        {
            currentUnit.GetComponent<EnemyUnitAction>().act();
        }
    }
    else
    {
        this.nextTurn();
    }
}