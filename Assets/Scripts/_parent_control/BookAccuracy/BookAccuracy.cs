using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAccuracy : MonoBehaviour {

    public List<string> lstGrade;

    public double total;
    public int max;
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
        max = lstGrade.Count * 100;
        return totalScore;
    }

    public void SetList(List<string> lst){
        lstGrade = lst;
    }
    public string Get(string s)
    {
        if (s.Equals(""))
            return "0";
        return s;
    }
}
