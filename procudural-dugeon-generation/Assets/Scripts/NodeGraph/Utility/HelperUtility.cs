using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtility
{

    public static bool ValidateCheckEmptyString(Object thisObject, string fileName, string stringTocheck)
    {

        if(stringTocheck == "")
        {
            Debug.Log(fileName + " is empty and must contain a value in objetc" + thisObject.name.ToString());

            return true;
        }

        return false;
    }
}
