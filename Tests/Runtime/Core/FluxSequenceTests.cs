using NUnit.Framework;
using UnityEngine;
using EasyToolkit.Fluxion.Core;
using EasyToolkit.Fluxion.Core.Implementations;

namespace EasyToolkit.Fluxion.Core.Tests
{
    /// <summary>
    /// Unit tests for FluxSequence functionality.
    /// </summary>
    [TestFixture]
    public class FluxSequenceTests
    {
        private FluxTestRunner _runner;
        private float _value1;
        private float _value2;

        [SetUp]
        public void SetUp()
        {
            _runner = new FluxTestRunner();
            _value1 = 0f;
            _value2 = 0f;
        }

        #region Sequential Execution

        /// <summary>
        /// Tests that FluxSequence executes Fluxes sequentially when added as new clips.
        /// </summary>
        [Test]
        public void Sequence_ShouldExecuteFluxesInOrder()
        {
            // Arrange
            var sequence = new FluxSequence();
            var flow1 = CreateFlow(ref _value1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(ref _value2, 0f, 20f, 0.5f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxAsNewClip(flow2);
            sequence.Start();

            // Act - Run halfway through first flow
            _runner.UpdateFlux(sequence, 0.25f);

            // Assert - First flow should be running, second should not have started
            Assert.AreEqual(FluxState.Playing, flow1.CurrentState, "First flow should be playing.");
            Assert.AreEqual(FluxState.Idle, flow2.CurrentState, "Second flow should be idle.");
            Assert.That(_value1, Is.GreaterThan(0f), "First value should have changed.");
            Assert.AreEqual(0f, _value2, 0.001f, "Second value should not have changed yet.");

            // Act - Complete first flow and start second
            _runner.UpdateFlux(sequence, 0.5f);

            // Assert - First flow should be completed, second should be playing
            Assert.AreEqual(FluxState.Completed, flow1.CurrentState, "First flow should be completed.");
            Assert.AreEqual(FluxState.Playing, flow2.CurrentState, "Second flow should be playing.");
        }

        /// <summary>
        /// Tests that FluxSequence completes when all Fluxes complete.
        /// </summary>
        [Test]
        public void Sequence_ShouldComplete_WhenAllFluxesComplete()
        {
            // Arrange
            var sequence = new FluxSequence();
            var flow1 = CreateFlow(ref _value1, 0f, 10f, 0.3f);
            var flow2 = CreateFlow(ref _value2, 0f, 20f, 0.3f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxAsNewClip(flow2);
            sequence.Start();

            // Act
            _runner.RunToCompletion(sequence, timeStep: 0.1f, maxTime: 5f);

            // Assert
            Assert.AreEqual(FluxState.Completed, sequence.CurrentState, "Sequence should be completed.");
            Assert.AreEqual(FluxState.Completed, flow1.CurrentState, "First flow should be completed.");
            Assert.AreEqual(FluxState.Completed, flow2.CurrentState, "Second flow should be completed.");
            Assert.AreEqual(10f, _value1, 0.01f, "First value should reach end.");
            Assert.AreEqual(20f, _value2, 0.01f, "Second value should reach end.");
        }

        #endregion

        #region Parallel Execution

        /// <summary>
        /// Tests that Fluxes added to the same clip run in parallel.
        /// </summary>
        [Test]
        public void Sequence_AddFluxToLastClip_ShouldRunInParallel()
        {
            // Arrange
            var sequence = new FluxSequence();
            var flow1 = CreateFlow(ref _value1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(ref _value2, 0f, 20f, 0.5f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxToLastClip(flow2); // Add to same clip
            sequence.Start();

            // Act - Run halfway through
            _runner.UpdateFlux(sequence, 0.25f);

            // Assert - Both flows should be running in parallel
            Assert.AreEqual(FluxState.Playing, flow1.CurrentState, "First flow should be playing.");
            Assert.AreEqual(FluxState.Playing, flow2.CurrentState, "Second flow should be playing.");
            Assert.That(_value1, Is.GreaterThan(0f), "First value should have changed.");
            Assert.That(_value2, Is.GreaterThan(0f), "Second value should have changed.");
        }

        #endregion

        #region Kill

        /// <summary>
        /// Tests that killing a FluxSequence kills all its child Fluxes.
        /// </summary>
        [Test]
        public void Sequence_Kill_ShouldKillAllChildren()
        {
            // Arrange
            var sequence = new FluxSequence();
            var flow1 = CreateFlow(ref _value1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(ref _value2, 0f, 20f, 0.5f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxAsNewClip(flow2);
            sequence.Start();
            _runner.UpdateFlux(sequence, 0.2f); // Start first flow

            // Act
            sequence.Kill();
            sequence.HandleKill();

            // Assert
            Assert.IsTrue(sequence.IsPendingKill, "Sequence should be pending kill.");
            Assert.AreEqual(FluxState.Killed, flow1.CurrentState, "First flow should be killed.");
            Assert.AreEqual(FluxState.Killed, flow2.CurrentState, "Second flow should be killed.");
        }

        /// <summary>
        /// Tests that killing a FluxSequence stops all progress.
        /// </summary>
        [Test]
        public void Sequence_Killed_ShouldStopAllProgress()
        {
            // Arrange
            var sequence = new FluxSequence();
            var flow1 = CreateFlow(ref _value1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(ref _value2, 0f, 20f, 0.5f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxAsNewClip(flow2);
            sequence.Start();
            _runner.UpdateFlux(sequence, 0.2f);

            // Act
            sequence.Kill();
            sequence.HandleKill();
            var value1WhenKilled = _value1;
            var value2WhenKilled = _value2;

            _runner.UpdateFlux(sequence, 0.5f);

            // Assert
            Assert.AreEqual(value1WhenKilled, _value1, 0.001f, "First value should not change after kill.");
            Assert.AreEqual(value2WhenKilled, _value2, 0.001f, "Second value should not change after kill.");
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates a Flow instance for testing.
        /// </summary>
        private Flow<float> CreateFlow(ref float value, float startValue, float endValue, float duration)
        {
            // Set initial value
            value = startValue;

            // Capture a local reference to avoid capturing ref parameter in lambda
            float valueRef = value;

            var flow = new Flow<float>
            {
                UnityObject = new GameObject("TestObject") // Required by Flow
            };

            flow.Apply(
                () => valueRef,
                v => valueRef = v,
                endValue
            );
            flow.SetDuration(duration);

            // Update the ref parameter with final value
            value = valueRef;

            return flow;
        }

        #endregion
    }
}
