using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDepot : Singleton<MonsterDepot>
{
    public Dictionary<int, int[]> monster = new Dictionary<int, int[]>();
}
