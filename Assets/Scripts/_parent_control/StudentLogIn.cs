using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentLogIn : MonoBehaviour {

	public void LogIn()
    {
        UserRestrictionController.ins.Restrict(1);
    }
}
