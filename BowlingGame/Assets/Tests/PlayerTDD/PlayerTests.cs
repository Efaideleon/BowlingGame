using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{
    public class TheMoveMethod
    {
        [Test]
        public void Move_Method_Is_Called()
        {
            var player = new PlayerTDD();

            player.Move();
        }
    }
}