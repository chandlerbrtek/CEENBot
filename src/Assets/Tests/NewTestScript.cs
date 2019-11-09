using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class NewTestScript
    {
        // A Test behaves as an ordinary method
        [UnityTest]
        public IEnumerator Scenebased()
        {
            SceneManager.LoadScene("BlockProgramming", LoadSceneMode.Single);
            yield return null;
            var go = GameObject.Find("rnadomasnfoasinf");

            Assert.IsNotNull(go, "No object found in scene.");
        }

    }
}
