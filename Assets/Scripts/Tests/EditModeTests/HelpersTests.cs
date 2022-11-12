using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HelpersTests
{
    [Test]
    public void Map_ChangeRange()
    {
        var toBeMappedRangeStart = 0f;
        var toBeMappedRangeEnd = 1f;
        var toBeMappedValue = 0.5f;

        var mappedRangeStart = -1f;
        var mappedRangeEnd = 1f;
        var mappedValue = Helpers.Map(toBeMappedValue, toBeMappedRangeStart, toBeMappedRangeEnd, mappedRangeStart,
            mappedRangeEnd);

        Assert.AreEqual(mappedValue, 0f);
    }
}
