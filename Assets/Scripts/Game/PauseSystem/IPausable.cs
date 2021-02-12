﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPausable 
{
     void SetPause();
     void Resume();
     void OnStart();
}
