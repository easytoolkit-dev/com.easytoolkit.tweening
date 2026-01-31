using EasyToolkit.Fluxion.Core;
using NUnit.Framework;
using UnityEngine;
using EasyToolkit.Fluxion.Core.Tests;

namespace EasyToolkit.Fluxion.Extensions.Tests
{
    /// <summary>
    /// Unit tests for FlowCoreExtensions.
    /// </summary>
    [TestFixture]
    public class FlowCoreExtensionsTests
    {
        private FluxTestRunner _runner;
        private GameObject _testGameObject;
        private Transform _testTransform;
        private SpriteRenderer _testSpriteRenderer;

        [SetUp]
        public void SetUp()
        {
            _runner = new FluxTestRunner();
            _testGameObject = new GameObject("TestObject");
            _testTransform = _testGameObject.transform;
            _testSpriteRenderer = _testGameObject.AddComponent<SpriteRenderer>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_testGameObject != null)
            {
                Object.DestroyImmediate(_testGameObject);
            }
        }

        #region Local Position

        /// <summary>
        /// Tests that FlowLocalMove correctly interpolates local position.
        /// </summary>
        [Test]
        public void FlowLocalMove_WhenPlayed_ShouldInterpolateToTarget()
        {
            // Arrange
            var from = Vector3.zero;
            var to = new Vector3(10f, 20f, 30f);
            var duration = 1f;
            _testTransform.localPosition = from;

            var flow = _testTransform.FlowLocalMove(to, duration);

            // Act
            _runner.UpdateEngine(0.5f); // Halfway through

            // Assert
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state.");
            Assert.AreEqual(5f, _testTransform.localPosition.x, 0.01f, "X should be halfway interpolated.");
            Assert.AreEqual(10f, _testTransform.localPosition.y, 0.01f, "Y should be halfway interpolated.");
            Assert.AreEqual(15f, _testTransform.localPosition.z, 0.01f, "Z should be halfway interpolated.");
        }

        /// <summary>
        /// Tests that FlowLocalMoveX correctly interpolates local X position.
        /// </summary>
        [Test]
        public void FlowLocalMoveX_WhenPlayed_ShouldInterpolateX()
        {
            // Arrange
            var to = 10f;
            var duration = 1f;
            _testTransform.localPosition = new Vector3(0f, 5f, 5f);

            var flow = _testTransform.FlowLocalMoveX(to, duration);

            // Act
            _runner.UpdateEngine(0.5f); // Halfway through

            // Assert
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state.");
            Assert.AreEqual(5f, _testTransform.localPosition.x, 0.01f, "X should be halfway interpolated.");
            Assert.AreEqual(5f, _testTransform.localPosition.y, 0.01f, "Y should remain unchanged.");
            Assert.AreEqual(5f, _testTransform.localPosition.z, 0.01f, "Z should remain unchanged.");
        }

        /// <summary>
        /// Tests that FlowLocalMoveY correctly interpolates local Y position.
        /// </summary>
        [Test]
        public void FlowLocalMoveY_WhenPlayed_ShouldInterpolateY()
        {
            // Arrange
            var to = 10f;
            var duration = 1f;
            _testTransform.localPosition = new Vector3(5f, 0f, 5f);

            var flow = _testTransform.FlowLocalMoveY(to, duration);

            // Act
            _runner.UpdateEngine(0.5f); // Halfway through

            // Assert
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state.");
            Assert.AreEqual(5f, _testTransform.localPosition.x, 0.01f, "X should remain unchanged.");
            Assert.AreEqual(5f, _testTransform.localPosition.y, 0.01f, "Y should be halfway interpolated.");
            Assert.AreEqual(5f, _testTransform.localPosition.z, 0.01f, "Z should remain unchanged.");
        }

        /// <summary>
        /// Tests that FlowLocalMoveZ correctly interpolates local Z position.
        /// </summary>
        [Test]
        public void FlowLocalMoveZ_WhenPlayed_ShouldInterpolateZ()
        {
            // Arrange
            var to = 10f;
            var duration = 1f;
            _testTransform.localPosition = new Vector3(5f, 5f, 0f);

            var flow = _testTransform.FlowLocalMoveZ(to, duration);

            // Act
            _runner.UpdateEngine(0.5f); // Halfway through

            // Assert
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state.");
            Assert.AreEqual(5f, _testTransform.localPosition.x, 0.01f, "X should remain unchanged.");
            Assert.AreEqual(5f, _testTransform.localPosition.y, 0.01f, "Y should remain unchanged.");
            Assert.AreEqual(5f, _testTransform.localPosition.z, 0.01f, "Z should be halfway interpolated.");
        }

        #endregion

        #region World Position

        /// <summary>
        /// Tests that FlowMove correctly interpolates world position.
        /// </summary>
        [Test]
        public void FlowMove_WhenPlayed_ShouldInterpolateToTarget()
        {
            // Arrange
            var from = Vector3.zero;
            var to = new Vector3(10f, 20f, 30f);
            var duration = 1f;
            _testTransform.position = from;

            var flow = _testTransform.FlowMove(to, duration);

            // Act
            _runner.UpdateEngine(0.5f); // Halfway through

            // Assert
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state.");
            Assert.AreEqual(5f, _testTransform.position.x, 0.01f, "X should be halfway interpolated.");
            Assert.AreEqual(10f, _testTransform.position.y, 0.01f, "Y should be halfway interpolated.");
            Assert.AreEqual(15f, _testTransform.position.z, 0.01f, "Z should be halfway interpolated.");
        }

        /// <summary>
        /// Tests that FlowMoveX correctly interpolates world X position.
        /// </summary>
        [Test]
        public void FlowMoveX_WhenPlayed_ShouldInterpolateX()
        {
            // Arrange
            var to = 10f;
            var duration = 1f;
            _testTransform.position = new Vector3(0f, 5f, 5f);

            var flow = _testTransform.FlowMoveX(to, duration);

            // Act
            _runner.UpdateEngine(0.5f); // Halfway through

            // Assert
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state.");
            Assert.AreEqual(5f, _testTransform.position.x, 0.01f, "X should be halfway interpolated.");
            Assert.AreEqual(5f, _testTransform.position.y, 0.01f, "Y should remain unchanged.");
            Assert.AreEqual(5f, _testTransform.position.z, 0.01f, "Z should remain unchanged.");
        }

        /// <summary>
        /// Tests that FlowMoveY correctly interpolates world Y position.
        /// </summary>
        [Test]
        public void FlowMoveY_WhenPlayed_ShouldInterpolateY()
        {
            // Arrange
            var to = 10f;
            var duration = 1f;
            _testTransform.position = new Vector3(5f, 0f, 5f);

            var flow = _testTransform.FlowMoveY(to, duration);

            // Act
            _runner.UpdateEngine(0.5f); // Halfway through

            // Assert
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state.");
            Assert.AreEqual(5f, _testTransform.position.x, 0.01f, "X should remain unchanged.");
            Assert.AreEqual(5f, _testTransform.position.y, 0.01f, "Y should be halfway interpolated.");
            Assert.AreEqual(5f, _testTransform.position.z, 0.01f, "Z should remain unchanged.");
        }

        /// <summary>
        /// Tests that FlowMoveZ correctly interpolates world Z position.
        /// </summary>
        [Test]
        public void FlowMoveZ_WhenPlayed_ShouldInterpolateZ()
        {
            // Arrange
            var to = 10f;
            var duration = 1f;
            _testTransform.position = new Vector3(5f, 5f, 0f);

            var flow = _testTransform.FlowMoveZ(to, duration);

            // Act
            _runner.UpdateEngine(0.5f); // Halfway through

            // Assert
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state.");
            Assert.AreEqual(5f, _testTransform.position.x, 0.01f, "X should remain unchanged.");
            Assert.AreEqual(5f, _testTransform.position.y, 0.01f, "Y should remain unchanged.");
            Assert.AreEqual(5f, _testTransform.position.z, 0.01f, "Z should be halfway interpolated.");
        }

        #endregion

        #region Scale

        /// <summary>
        /// Tests that FlowScale with Vector3 correctly interpolates scale.
        /// </summary>
        [Test]
        public void FlowScale_Vector3_WhenPlayed_ShouldInterpolateScale()
        {
            // Arrange
            var from = Vector3.one;
            var to = new Vector3(2f, 3f, 4f);
            var duration = 1f;
            _testTransform.localScale = from;

            var flow = _testTransform.FlowScale(to, duration);

            // Act
            _runner.UpdateEngine(0.5f); // Halfway through

            // Assert
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state.");
            Assert.AreEqual(1.5f, _testTransform.localScale.x, 0.01f, "X should be halfway interpolated.");
            Assert.AreEqual(2f, _testTransform.localScale.y, 0.01f, "Y should be halfway interpolated.");
            Assert.AreEqual(2.5f, _testTransform.localScale.z, 0.01f, "Z should be halfway interpolated.");
        }

        /// <summary>
        /// Tests that FlowScale with float correctly interpolates uniform scale.
        /// </summary>
        [Test]
        public void FlowScale_Float_WhenPlayed_ShouldInterpolateUniformScale()
        {
            // Arrange
            var from = 1f;
            var to = 2f;
            var duration = 1f;
            _testTransform.localScale = Vector3.one * from;

            var flow = _testTransform.FlowScale(to, duration);

            // Act
            _runner.UpdateEngine(0.5f); // Halfway through

            // Assert
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state.");
            Assert.AreEqual(1.5f, _testTransform.localScale.x, 0.01f, "X should be halfway interpolated.");
            Assert.AreEqual(1.5f, _testTransform.localScale.y, 0.01f, "Y should be halfway interpolated.");
            Assert.AreEqual(1.5f, _testTransform.localScale.z, 0.01f, "Z should be halfway interpolated.");
        }

        #endregion

        #region Sprite Animation

        /// <summary>
        /// Tests that FlowSpritesAnim correctly cycles through sprites.
        /// </summary>
        [Test]
        public void FlowSpritesAnim_WhenPlayed_ShouldCycleThroughSprites()
        {
            // Arrange
            var sprites = new[]
            {
                Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), Vector2.one * 0.5f),
                Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), Vector2.one * 0.5f),
                Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), Vector2.one * 0.5f)
            };
            var duration = 1f;
            _testSpriteRenderer.sprite = sprites[0];

            var flow = _testSpriteRenderer.FlowSpritesAnim(sprites, duration);

            // Act - Run halfway through (should be at second sprite)
            _runner.UpdateEngine(0.5f);

            // Assert
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state.");
            Assert.AreEqual(sprites[1], _testSpriteRenderer.sprite, "Sprite should be at second frame.");
        }

        /// <summary>
        /// Tests that FlowSpritesAnim completes when duration elapses.
        /// </summary>
        [Test]
        public void FlowSpritesAnim_WhenCompleted_ShouldReachLastSprite()
        {
            // Arrange
            var sprites = new[]
            {
                Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), Vector2.one * 0.5f),
                Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), Vector2.one * 0.5f),
                Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), Vector2.one * 0.5f)
            };
            var duration = 0.5f;

            var flow = _testSpriteRenderer.FlowSpritesAnim(sprites, duration);

            // Act
            _runner.UpdateEngine();
            _runner.RunToCompletion(flow, timeStep: 0.1f, maxTime: 5f);

            // Assert
            Assert.AreEqual(FluxState.Completed, flow.CurrentState, "Flow should be in Completed state.");
            Assert.AreEqual(sprites[2], _testSpriteRenderer.sprite, "Sprite should be at last frame.");
        }

        #endregion

        #region Completion

        /// <summary>
        /// Tests that FlowLocalMove completes correctly.
        /// </summary>
        [Test]
        public void FlowLocalMove_WhenCompleted_ShouldReachTarget()
        {
            // Arrange
            var to = new Vector3(10f, 20f, 30f);
            var duration = 0.5f;
            _testTransform.localPosition = Vector3.zero;

            var flow = _testTransform.FlowLocalMove(to, duration);

            // Act
            _runner.UpdateEngine();
            _runner.RunToCompletion(flow, timeStep: 0.1f, maxTime: 5f);

            // Assert
            Assert.AreEqual(FluxState.Completed, flow.CurrentState, "Flow should be in Completed state.");
            Assert.AreEqual(to.x, _testTransform.localPosition.x, 0.001f, "Should reach target X position.");
            Assert.AreEqual(to.y, _testTransform.localPosition.y, 0.001f, "Should reach target Y position.");
            Assert.AreEqual(to.z, _testTransform.localPosition.z, 0.001f, "Should reach target Z position.");
        }

        /// <summary>
        /// Tests that FlowScale completes correctly.
        /// </summary>
        [Test]
        public void FlowScale_WhenCompleted_ShouldReachTarget()
        {
            // Arrange
            var to = new Vector3(2f, 3f, 4f);
            var duration = 0.5f;
            _testTransform.localScale = Vector3.one;

            var flow = _testTransform.FlowScale(to, duration);

            // Act
            _runner.UpdateEngine();
            _runner.RunToCompletion(flow, timeStep: 0.1f, maxTime: 5f);

            // Assert
            Assert.AreEqual(FluxState.Completed, flow.CurrentState, "Flow should be in Completed state.");
            Assert.AreEqual(to.x, _testTransform.localScale.x, 0.001f, "Should reach target X scale.");
            Assert.AreEqual(to.y, _testTransform.localScale.y, 0.001f, "Should reach target Y scale.");
            Assert.AreEqual(to.z, _testTransform.localScale.z, 0.001f, "Should reach target Z scale.");
        }

        #endregion
    }
}
