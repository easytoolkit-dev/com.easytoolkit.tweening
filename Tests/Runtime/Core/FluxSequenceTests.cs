using NUnit.Framework;
using UnityEngine;
using EasyToolkit.Fluxion.Core;
using EasyToolkit.Fluxion.Core.Implementations;
using EasyToolkit.Fluxion.Tests;

namespace EasyToolkit.Fluxion.Core.Tests
{
    /// <summary>
    /// Unit tests for FluxSequence functionality.
    /// </summary>
    [TestFixture]
    public class FluxSequenceTests
    {
        private FluxTestRunner _runner;
        private FloatValueHolder _valueHolder1;
        private FloatValueHolder _valueHolder2;

        /// <summary>
        /// Wrapper class to hold float values by reference for test scenarios.
        /// </summary>
        private class FloatValueHolder
        {
            public float Value;
        }

        [SetUp]
        public void SetUp()
        {
            _runner = new FluxTestRunner();
            _valueHolder1 = new FloatValueHolder { Value = 0f };
            _valueHolder2 = new FloatValueHolder { Value = 0f };
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
            var flow1 = CreateFlow(_valueHolder1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(_valueHolder2, 0f, 20f, 0.5f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxAsNewClip(flow2);
            sequence.Start();

            // Act - Run halfway through first flow
            _runner.UpdateFlux(sequence, 0.25f);

            // Assert - First flow should be running, second should not have started
            Assert.AreEqual(FluxState.Playing, flow1.CurrentState, "First flow should be playing.");
            Assert.AreEqual(FluxState.Idle, flow2.CurrentState, "Second flow should be idle.");
            Assert.That(_valueHolder1.Value, Is.GreaterThan(0f), "First value should have changed.");
            Assert.AreEqual(0f, _valueHolder2.Value, 0.001f, "Second value should not have changed yet.");

            // Act - Complete first flow and start second
            _runner.UpdateFlux(sequence, 0.5f);

            // Assert - First flow should be killed (completed fluxes are killed), second should be playing
            Assert.AreEqual(FluxState.Killed, flow1.CurrentState, "First flow should be killed.");
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
            var flow1 = CreateFlow(_valueHolder1, 0f, 10f, 0.3f);
            var flow2 = CreateFlow(_valueHolder2, 0f, 20f, 0.3f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxAsNewClip(flow2);
            sequence.Start();

            // Act
            _runner.RunToCompletion(sequence, timeStep: 0.1f, maxTime: 5f);

            // Assert
            Assert.AreEqual(FluxState.Completed, sequence.CurrentState, "Sequence should be completed.");
            Assert.AreEqual(FluxState.Killed, flow1.CurrentState, "First flow should be killed (completed fluxes are killed).");
            Assert.AreEqual(FluxState.Killed, flow2.CurrentState, "Second flow should be killed (completed fluxes are killed).");
            Assert.AreEqual(10f, _valueHolder1.Value, 0.01f, "First value should reach end.");
            Assert.AreEqual(20f, _valueHolder2.Value, 0.01f, "Second value should reach end.");
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
            var flow1 = CreateFlow(_valueHolder1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(_valueHolder2, 0f, 20f, 0.5f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxToLastClip(flow2); // Add to same clip
            sequence.Start();

            // Act - Run halfway through
            _runner.UpdateFlux(sequence, 0.25f);

            // Assert - Both flows should be running in parallel
            Assert.AreEqual(FluxState.Playing, flow1.CurrentState, "First flow should be playing.");
            Assert.AreEqual(FluxState.Playing, flow2.CurrentState, "Second flow should be playing.");
            Assert.That(_valueHolder1.Value, Is.GreaterThan(0f), "First value should have changed.");
            Assert.That(_valueHolder2.Value, Is.GreaterThan(0f), "Second value should have changed.");
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
            var flow1 = CreateFlow(_valueHolder1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(_valueHolder2, 0f, 20f, 0.5f);

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
            var flow1 = CreateFlow(_valueHolder1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(_valueHolder2, 0f, 20f, 0.5f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxAsNewClip(flow2);
            sequence.Start();
            _runner.UpdateFlux(sequence, 0.2f);

            // Act
            sequence.Kill();
            sequence.HandleKill();
            var value1WhenKilled = _valueHolder1.Value;
            var value2WhenKilled = _valueHolder2.Value;

            _runner.UpdateFlux(sequence, 0.5f);

            // Assert
            Assert.AreEqual(value1WhenKilled, _valueHolder1.Value, 0.001f, "First value should not change after kill.");
            Assert.AreEqual(value2WhenKilled, _valueHolder2.Value, 0.001f, "Second value should not change after kill.");
        }

        #endregion

        #region Context Decoupling

        /// <summary>
        /// Tests that FluxSequenceClip uses Context to Detach Flux when adding to a sequence.
        /// </summary>
        [Test]
        public void FluxSequenceClip_AddFlux_ShouldDetachFromContext()
        {
            // Arrange
            var mockContext = new MockFluxContext();
            var sequence = new FluxSequence();
            ((IFluxEntity)sequence).Context = mockContext;

            var flow = CreateFlow(_valueHolder1, 0f, 10f, 0.5f);
            ((IFluxEntity)flow).Context = mockContext;

            // Simulate flow was attached to engine
            mockContext.Lifecycle.Attach(flow);

            // Act
            sequence.AddFluxAsNewClip(flow);

            // Assert - The flow should have been detached from the context
            var mockLifecycle = (MockLifecycleManager)mockContext.Lifecycle;
            Assert.Contains(flow, mockLifecycle.DetachedFluxes,
                "Flow should be detached from context when added to sequence.");
        }

        /// <summary>
        /// Tests that AddFluxToLastClip also detaches Flux from Context.
        /// </summary>
        [Test]
        public void AddFluxToLastClip_ShouldDetachFromContext()
        {
            // Arrange
            var mockContext = new MockFluxContext();
            var sequence = new FluxSequence();
            ((IFluxEntity)sequence).Context = mockContext;

            var flow1 = CreateFlow(_valueHolder1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(_valueHolder2, 0f, 20f, 0.5f);

            ((IFluxEntity)flow1).Context = mockContext;
            ((IFluxEntity)flow2).Context = mockContext;

            mockContext.Lifecycle.Attach(flow1);
            mockContext.Lifecycle.Attach(flow2);

            // Act - Add first as new clip, second to same clip
            sequence.AddFluxAsNewClip(flow1);
            var mockLifecycle = (MockLifecycleManager)mockContext.Lifecycle;
            mockLifecycle.DetachedFluxes.Clear(); // Clear first detachment
            sequence.AddFluxToLastClip(flow2);

            // Assert
            Assert.Contains(flow2, mockLifecycle.DetachedFluxes,
                "Second flow should be detached from context when added to last clip.");
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates a Flow instance for testing.
        /// </summary>
        private Flow<float> CreateFlow(FloatValueHolder holder, float startValue, float endValue, float duration)
        {
            // Set initial value
            holder.Value = startValue;

            var flow = new Flow<float>
            {
                UnityObject = new GameObject("TestObject") // Required by Flow
            };

            flow.Apply(
                () => holder.Value,
                v => holder.Value = v,
                endValue
            );
            flow.SetDuration(duration);

            return flow;
        }

        #endregion
    }
}
