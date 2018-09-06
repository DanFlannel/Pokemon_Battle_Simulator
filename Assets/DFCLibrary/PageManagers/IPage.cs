using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPage {

    void Open();

    void Close();

    void Initalize();

    void Reload();

    void doUpdate();

    void doFixedUpdate();

    void doLateUpdate();
}