using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NodeGraphTest
{
    [Test]
    public void NodeGraphTestSimplePasses()
    {
        var noneNode = GameResources.Instance.roomNodeTypeList.roomNodeTypeList.Find(x => x.isNone);
        Assert.NotNull(noneNode);

    }
}
