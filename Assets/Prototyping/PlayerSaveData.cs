using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerSaveData
{
    public static float saveDataVersion = 0.1f;

    public Dictionary<string, int> stats;

    public int GetStat(string name)
    {
        int val;
        if (stats.TryGetValue(name, out val))
            return val;
        else
            CreateStat(name);
        return 0;
    }
    public void ChangeStat(string name, int change)
    {
        if (!stats.ContainsKey(name))
            CreateStat(name);
        stats[name] += change;
    }
    public void SetStat(string name, int value)
    {
        if (!stats.ContainsKey(name))
            CreateStat(name);
        stats[name] = value;
    }
    public void CreateStat(string name) => stats.Add(name, 0);

    public void RenameStat(string name, string newName)
    {
        int val;
        if (stats.TryGetValue(name, out val))
            stats.Remove(name);

        stats.Add(newName, val);
    }
}
