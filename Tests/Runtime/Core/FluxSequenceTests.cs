using NUnit.Framework;
using UnityEngine;
using EasyToolkit.Fluxion;
using EasyToolkit.Fluxion.Core;

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
            var sequence = FluxFactory.Sequence();
            var flow1 = CreateFlow(_valueHolder1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(_valueHolder2, 0f, 20f, 0.5f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxAsNewClip(flow2);

            // Act - Run halfway through first flow
            _runner.UpdateEngine(0.25f);

            // Assert - First flow should be running, second should not have started
            Assert.AreEqual(FluxState.Playing, flow1.CurrentState, "First flow should be playing.");
            Assert.AreEqual(FluxState.Idle, flow2.CurrentState, "Second flow should be idle.");
            Assert.That(_valueHolder1.Value, Is.GreaterThan(0f), "First value should have changed.");
            Assert.AreEqual(0f, _valueHolder2.Value, 0.001f, "Second value should not have changed yet.");

            // Act - Complete first flow and start second
            _runner.UpdateEngine(0.5f);

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
            var sequence = FluxFactory.Sequence();
            var flow1 = CreateFlow(_valueHolder1, 0f, 10f, 0.3f);
            var flow2 = CreateFlow(_valueHolder2, 0f, 20f, 0.3f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxAsNewClip(flow2);

            // Act
            _runner.UpdateEngine(0f);
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
            var sequence = FluxFactory.Sequence();
            var flow1 = CreateFlow(_valueHolder1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(_valueHolder2, 0f, 20f, 0.5f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxToLastClip(flow2); // Add to same clip

            // Act - Run halfway through
            _runner.UpdateEngine(0.25f);

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
            var sequence = FluxFactory.Sequence();
            var flow1 = CreateFlow(_valueHolder1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(_valueHolder2, 0f, 20f, 0.5f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxAsNewClip(flow2);
            _runner.UpdateEngine(0.2f); // Start first flow

            // Act
            sequence.Kill();

            // Assert
            Assert.IsTrue(sequence.IsPendingKill, "Sequence should be pending kill.");

            _runner.UpdateEngine();
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
            var sequence = FluxFactory.Sequence();
            var flow1 = CreateFlow(_valueHolder1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(_valueHolder2, 0f, 20f, 0.5f);

            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxAsNewClip(flow2);
            _runner.UpdateEngine(0.2f);

            // Act
            sequence.Kill();
            var value1WhenKilled = _valueHolder1.Value;
            var value2WhenKilled = _valueHolder2.Value;

            _runner.UpdateEngine(0.5f);

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
            var sequence = FluxFactory.Sequence();

            var flow = CreateFlow(_valueHolder1, 0f, 10f, 0.5f);

            // Act
            sequence.AddFluxAsNewClip(flow);

            // Assert - The flow should have been detached from the context
            Assert.IsNull(sequence.Context.Registry.GetFluxById(flow.Id),
                "Flow should be detached from context when added to sequence.");
        }

        /// <summary>
        /// Tests that AddFluxToLastClip also detaches Flux from Context.
        /// </summary>
        [Test]
        public void AddFluxToLastClip_ShouldDetachFromContext()
        {
            // Arrange
            var sequence = FluxFactory.Sequence();

            var flow1 = CreateFlow(_valueHolder1, 0f, 10f, 0.5f);
            var flow2 = CreateFlow(_valueHolder2, 0f, 20f, 0.5f);

            // Act - Add first as new clip, second to same clip
            sequence.AddFluxAsNewClip(flow1);
            sequence.AddFluxToLastClip(flow2);

            // Assert
            Assert.IsNull(sequence.Context.Registry.GetFluxById(flow2.Id),
                "Second flow should be detached from context when added to last clip.");
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates a Flow instance for testing.
        /// </summary>
        private IFlow<float> CreateFlow(FloatValueHolder holder, float startValue, float endValue, float duration)
        {
            // Set initial value
            holder.Value = startValue;

            var flow = FluxFactory.To(
                () => holder.Value,
                v => holder.Value = v,
                endValue,
                duration
            );
            flow.UnityObject = new GameObject("TestObject");

            return flow;
        }

        #endregion
    }
}
