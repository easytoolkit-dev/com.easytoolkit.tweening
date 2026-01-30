using NUnit.Framework;
using UnityEngine;
using EasyToolkit.Fluxion.Core.Implementations;

namespace EasyToolkit.Fluxion.Core.Tests
{
    /// <summary>
    /// Unit tests for Flow functionality.
    /// </summary>
    [TestFixture]
    public class FlowTests
    {
        private FluxTestRunner _runner;
        private float _testValue;

        [SetUp]
        public void SetUp()
        {
            _runner = new FluxTestRunner();
            _testValue = 0f;
        }

        #region Basic Interpolation

        /// <summary>
        /// Tests that Flow correctly interpolates values from start to end.
        /// </summary>
        [Test]
        public void Flow_ShouldInterpolateValueCorrectly()
        {
            // Arrange
            var flow = CreateFlow(0f, 10f, 1f);
            flow.Start();

            // Act
            _runner.UpdateFlux(flow, 0.5f); // Halfway through

            // Assert
            Assert.AreEqual(5f, _testValue, 0.01f, "Value should be halfway interpolated.");
        }

        /// <summary>
        /// Tests that Flow completes when the full duration elapses.
        /// </summary>
        [Test]
        public void Flow_WhenDurationCompletes_ShouldSetStateToCompleted()
        {
            // Arrange
            var flow = CreateFlow(0f, 10f, 1f);
            flow.Start();

            // Act
            _runner.RunToCompletion(flow, timeStep: 0.1f, maxTime: 5f);

            // Assert
            Assert.AreEqual(FluxState.Completed, flow.CurrentState, "Flow should be in Completed state.");
            Assert.AreEqual(10f, _testValue, 0.01f, "Value should reach end value.");
        }

        #endregion

        #region Delay

        /// <summary>
        /// Tests that Flow with delay waits before starting interpolation.
        /// </summary>
        [Test]
        public void Flow_WithDelay_ShouldWaitBeforePlaying()
        {
            // Arrange
            var flow = CreateFlow(0f, 10f, 1f);
            flow.Delay = 0.5f;
            flow.Start();

            // Act - Update less than delay
            _runner.UpdateFlux(flow, 0.25f);

            // Assert - Should still be in delay state, value unchanged
            Assert.AreEqual(FluxState.DelayAfterPlay, flow.CurrentState, "Flow should be in DelayAfterPlay state.");
            Assert.AreEqual(0f, _testValue, 0.001f, "Value should not change during delay.");

            // Act - Update past delay
            _runner.UpdateFlux(flow, 0.5f);

            // Assert - Should now be playing
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state after delay.");
        }

        #endregion

        #region Loop

        /// <summary>
        /// Tests that Flow with LoopCount > 1 repeats the animation.
        /// </summary>
        [Test]
        public void Flow_WithLoop_ShouldRepeat()
        {
            // Arrange
            var flow = CreateFlow(0f, 10f, 0.5f);
            flow.LoopCount = 3;
            flow.Start();

            // Act - Run through first loop
            _runner.RunForDuration(flow, 0.6f, timeStep: 0.1f);

            // Assert - Should have restarted and be playing again
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state for second loop.");
            Assert.AreEqual(2, flow.LoopCount, "LoopCount should have decremented.");

            // Act - Complete all loops
            _runner.RunToCompletion(flow, timeStep: 0.1f, maxTime: 5f);

            // Assert
            Assert.AreEqual(FluxState.Completed, flow.CurrentState, "Flow should be Completed after all loops.");
        }

        /// <summary>
        /// Tests that Flow with Yoyo loop type reverses direction.
        /// </summary>
        [Test]
        public void Flow_WithYoyoLoop_ShouldReverseDirection()
        {
            // Arrange
            var flow = CreateFlow(0f, 10f, 0.5f);
            flow.LoopCount = 2;
            flow.LoopType = LoopType.Yoyo;
            flow.Start();

            // Act - Complete first loop
            _runner.RunForDuration(flow, 0.6f, timeStep: 0.1f);

            // Assert - Should be playing second loop (reversing from 10 to 0)
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be in Playing state for yoyo loop.");

            // Act - Run halfway through second loop
            _runner.RunForDuration(flow, 0.25f, timeStep: 0.1f);

            // Assert - Value should be between 10 and 0
            Assert.That(_testValue, Is.LessThan(10f), "Value should be decreasing during yoyo.");
            Assert.That(_testValue, Is.GreaterThan(0f), "Value should not have reached start yet.");
        }

        #endregion

        #region Pause/Resume

        /// <summary>
        /// Tests that pausing a Flow stops its progress.
        /// </summary>
        [Test]
        public void Flow_Pause_ShouldStopProgress()
        {
            // Arrange
            var flow = CreateFlow(0f, 10f, 1f);
            flow.Start();
            _runner.UpdateFlux(flow, 0.3f); // Get to 30%

            // Act
            flow.Pause();
            var valueBeforePause = _testValue;
            _runner.UpdateFlux(flow, 0.5f); // Try to advance while paused

            // Assert
            Assert.AreEqual(FluxState.Paused, flow.CurrentState, "Flow should be in Paused state.");
            Assert.AreEqual(valueBeforePause, _testValue, 0.001f, "Value should not change while paused.");

            // Act - Resume
            flow.Resume();
            _runner.UpdateFlux(flow, 0.2f);

            // Assert
            Assert.AreEqual(FluxState.Playing, flow.CurrentState, "Flow should be Playing after resume.");
            Assert.That(_testValue, Is.GreaterThan(valueBeforePause), "Value should change after resume.");
        }

        #endregion

        #region Kill

        /// <summary>
        /// Tests that killing a Flow sets IsPendingKill to true.
        /// </summary>
        [Test]
        public void Flow_Kill_ShouldSetPendingKill()
        {
            // Arrange
            var flow = CreateFlow(0f, 10f, 1f);
            flow.Start();

            // Act
            flow.Kill();
            flow.HandleKill(); // Simulate engine processing

            // Assert
            Assert.IsTrue(flow.IsPendingKill, "IsPendingKill should be true.");
            Assert.AreEqual(FluxState.Killed, flow.CurrentState, "State should be Killed.");
        }

        /// <summary>
        /// Tests that killing a Flow stops its progress.
        /// </summary>
        [Test]
        public void Flow_Killed_ShouldStopProgress()
        {
            // Arrange
            var flow = CreateFlow(0f, 10f, 1f);
            flow.Start();
            _runner.UpdateFlux(flow, 0.3f);

            // Act
            flow.Kill();
            flow.HandleKill();
            var valueWhenKilled = _testValue;

            // Try to update after kill
            _runner.UpdateFlux(flow, 0.5f);

            // Assert
            Assert.AreEqual(valueWhenKilled, _testValue, 0.001f, "Value should not change after kill.");
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates a Flow instance for testing.
        /// </summary>
        private Flow<float> CreateFlow(float startValue, float endValue, float duration)
        {
            // Set initial value
            _testValue = startValue;

            var flow = new Flow<float>
            {
                UnityObject = new GameObject("TestObject") // Required by Flow
            };

            flow.Apply(
                () => _testValue,
                value => _testValue = value,
                endValue
            );
            flow.SetDuration(duration);

            return flow;
        }

        #endregion
    }
}
