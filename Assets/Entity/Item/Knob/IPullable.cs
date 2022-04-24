using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPullable
{
    void PullStart(PlayerController player);
    void PullEnd(PlayerController player);
}
