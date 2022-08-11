using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAffect 
{
    void Touch(Vector3 myDir, Vector3 pos, int intensity);
}
