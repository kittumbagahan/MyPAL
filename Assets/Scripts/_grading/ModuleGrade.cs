using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleGrade {

    List<string> lstGrade;
    int max = 0;
    public ModuleGrade()
    {
        lstGrade = new List<string>();
    }

    public void Add(string grade)
    {
        lstGrade.Add(grade);
    }

    public virtual double GetAccuracy()
    {
        double totalScore = 0;
        for (int i = 0; i < lstGrade.Count; i++)
        {
            if (lstGrade[i].Equals("A++"))
            {
                totalScore += 100;
            }
            else if (lstGrade[i].Equals("A"))
            {
                totalScore += 95;
            }
            else if (lstGrade[i].Equals("B+"))
            {
                totalScore += 90;
            }
            else if (lstGrade[i].Equals("B"))
            {
                totalScore += 85;
            }
            else if (lstGrade[i].Equals("C+"))
            {
                totalScore += 80;
            }
            else if (lstGrade[i].Equals("C"))
            {
                totalScore += 75;
            }
            else if (lstGrade[i].Equals("D+"))
            {
                totalScore += 70;
            }
            else if (lstGrade[i].Equals("D"))
            {
                totalScore += 65;
            }
            else if (lstGrade[i].Equals("E+"))
            {
                totalScore += 60;
            }
            else if (lstGrade[i].Equals("E"))
            {
                totalScore += 55;
            }
            else if (lstGrade[i].Equals("F"))
            {
                totalScore += 50;
            }
            else
            {
                totalScore += 100;
            }
        }
        //number of activities * 100
        max = lstGrade.Count * 100;

        double res = (totalScore / (double)max) * 100;
        if (res.HasValue())
        {
            return res;
        }
        //Debug.Log("Res " + res);

        return 0;
    }
}
