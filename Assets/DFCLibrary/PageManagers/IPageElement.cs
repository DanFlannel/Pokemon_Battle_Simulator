using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPageElement
{
    void onInitalize();

    void onOpen();

    void doUpdate();

    void onClose();

    void onReload();

}
