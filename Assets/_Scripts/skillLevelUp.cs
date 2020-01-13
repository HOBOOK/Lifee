using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skillLevelUp : MonoBehaviour
{
    //스킬레벨업비용계산
    private void Start()
    {
        if (this.transform.GetChild(2).GetComponent<Text>().text == "배우기")
            this.transform.GetChild(0).GetComponent<Text>().text = GetHeartCost(0,0).ToString("N0");
    }
    private float GetHeartCost(float level, int t)
    {
        float cost = 0;
        float bonus = t * 10;
        if (level == 0)
        {
            cost = 20 + bonus;
        }
        else
        {
            cost = (level * 20) + bonus;
        }

        return cost;
    }

    //인생스킬레벨업
    public void PassiveSKillLevelUp()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        int i = System.Convert.ToInt32(gameObject.name.Substring(1, 1));
        

        if (User.passiveLevel[i] >= User.house*2)
        {
            ScriptOnGUI("아직은 더 올릴 수 없어.");
            return;
        }
        if(User.passiveLevel[i]>=10)
        {
            ScriptOnGUI("최고의 경지야!");
            return;
        }
        if (User.particle - GetHeartCost(User.passiveLevel[i], i) < 0)
        {
            ScriptOnGUI("행복의파편이 부족해.");
            return;
        }
        User.particle -= GetHeartCost(User.passiveLevel[i],i);
        User.passiveLevel[i] += 1;
    }

    //인생스킬배우기
    public void PassiveSKillLearn()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        int i = System.Convert.ToInt32(gameObject.name.Substring(1, 1));
        if (User.particle - GetHeartCost(User.passiveLevel[i],0) < 0)
            return;
        User.particle -= GetHeartCost(0,0);
        User.passiveLevel[i] = 1;
        gameObject.SetActive(false);
    }

    //직장스킬레벨업
    public void SKillLevelUp()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        int i = System.Convert.ToInt32(gameObject.name);

        int j = 0;
        if(i>5)
        {
            if (User.skillLevel[i] >= User.jobLevel -14)
            {
                ScriptOnGUI("아직은 더 올릴 수 없어.");
                return;
            }
            else
                j = 2;
        }
        else if(i>2)
        {
            if (User.skillLevel[i] >= User.jobLevel - 9)
            {
                ScriptOnGUI("아직은 더 올릴 수 없어.");
                return;
            }
            else
                j = 1;
        }
        else
        {
            if (User.skillLevel[i] >= User.jobLevel)
            {
                ScriptOnGUI("아직은 더 올릴 수 없어.");
                return;
            }
            else
                j = 0;
        }

        if (User.skillLevel[i] >= 10)
        {
            ScriptOnGUI("최고의 경지야!");
            return;
        }
        if (User.particle - GetHeartCost(User.skillLevel[i],j) < 0)
        {
            ScriptOnGUI("행복의파편이 부족해.");
            return;
        }
        User.particle -= GetHeartCost(User.skillLevel[i],j);
        User.skillLevel[i] += 1;
    }

    //직장스킬배우기
    public void SKillLearn()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        int i = System.Convert.ToInt32(gameObject.name);

        if (User.particle - GetHeartCost(User.skillLevel[i],0) < 0)
        {
            ScriptOnGUI("행복의파편이 부족해.");
            return;
        }
        switch(i)
        {
            case 0:
                break;
            case 1:
                if (User.jobLevel < 5)
                {
                    ScriptOnGUI("직장레벨 5는 돼야 배울 수 있어.");
                    return;
                }
                break;
            case 2:
                if (User.jobLevel < 15)
                {
                    ScriptOnGUI("직장레벨 15은 돼야 배울 수 있어.");
                    return;
                }
                break;
        }
        User.particle -= GetHeartCost(0,0);
        User.skillLevel[i] = 1;
        gameObject.SetActive(false);
    }

    //힐링스킬레벨업
    public void HealingSKillLevelUp()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        int i = System.Convert.ToInt32(gameObject.name.Substring(1, 1));
        

        switch(i)
        {
            case 0:
                if (User.healingLevel[i] >= User.tourLevel)
                {
                    ScriptOnGUI("아직은 더 올릴 수 없어.");
                    return;
                }
                break;
            case 1:
                if (User.healingLevel[i] >= User.tourLevel - 9)
                {
                    ScriptOnGUI("아직은 더 올릴 수 없어.");
                    return;
                }
                break;
            case 2:
                if (User.healingLevel[i] >= User.tourLevel - 14)
                {
                    ScriptOnGUI("아직은 더 올릴 수 없어.");
                    return;
                }
                break;
        }
        
        if (User.healingLevel[i] >= 10)
        {
            ScriptOnGUI("최고의 경지야!");
            return;
        }
        if (User.particle - GetHeartCost(User.healingLevel[i],i) < 0)
        {
            ScriptOnGUI("행복의파편이 부족해.");
            return;
        }

        User.particle -= GetHeartCost(User.healingLevel[i],i);
        User.healingLevel[i] += 1;
    }

    //힐링스킬배우기
    public void HealingSKillLearn()
    {
        if (!GameObject.Find("SoundOfButton").GetComponent<AudioSource>().isPlaying && !User.isEffectSound)
        {
            GameObject.Find("SoundOfButton").GetComponent<AudioSource>().Play();
        }
        int i = System.Convert.ToInt32(gameObject.name.Substring(1, 1));
        if (User.particle - GetHeartCost(User.healingLevel[i],0) < 0)
        {
            ScriptOnGUI("행복의파편이 부족해.");
            return;
        }
        switch (i)
        {
            case 0:
                break;
            case 1:
                if (User.tourLevel < 5)
                {
                    ScriptOnGUI("여행레벨 5는 돼야 배울 수 있어.");
                    return;
                }
                break;
            case 2:
                if (User.tourLevel < 15)
                {
                    ScriptOnGUI("여행레벨 15는 돼야 배울 수 있어.");
                    return;
                }
                break;
        }
        User.particle -= GetHeartCost(0,0);
        User.healingLevel[i] = 1;
        gameObject.SetActive(false);
    }

    public void ScriptOnGUI(string script)
    {
        GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0).gameObject.SetActive(true);
        if (GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0).gameObject != null)
        {
            GameObject.Find("CanvasOverlay/Pop").transform.GetChild(0).GetChild(0).GetComponent<Text>().text = script;
        }
    }
}
