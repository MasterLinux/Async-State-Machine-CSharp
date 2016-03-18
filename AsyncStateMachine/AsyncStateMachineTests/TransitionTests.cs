using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AsyncStateMachine;

namespace AsyncStateMachineTests
{
    [TestClass]
    public class TransitionTests
    {
        private Transition TransitionUnderTest { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            TransitionUnderTest = new Transition();
        }

        [TestMethod]
        public void TestInitializingTransition()
        {       
            Assert.IsTrue(TransitionUnderTest.IsPending);            
        }

        [TestMethod]
        public void TestExecutingTransition()
        {
            Assert.IsFalse(TransitionUnderTest.IsRunning);

            TransitionUnderTest.Execute();
            Assert.IsTrue(TransitionUnderTest.IsRunning);
        }

        [TestMethod]
        public void TestInterruptingTransition()
        {
            Assert.IsFalse(TransitionUnderTest.IsInterrupted);

            TransitionUnderTest.Interrupt();
            Assert.IsTrue(TransitionUnderTest.IsInterrupted);
        }

        [TestMethod]
        public void TestCompletingTransition()
        {
            TransitionUnderTest.Execute();
            Assert.IsFalse(TransitionUnderTest.IsCompleted);

            TransitionUnderTest.Complete();
            Assert.IsTrue(TransitionUnderTest.IsCompleted);
        }

        [TestMethod]
        public void TestThatTransitionCannotBeCompletedWhenPending()
        {
            Assert.IsTrue(TransitionUnderTest.IsPending);

            TransitionUnderTest.Complete();
            Assert.IsFalse(TransitionUnderTest.IsCompleted);
            Assert.IsTrue(TransitionUnderTest.IsPending);
        }

        [TestMethod]
        public void TestThatTransitionCannotBeCompletedIfInterrupted()
        {
            TransitionUnderTest.Interrupt();
            Assert.IsTrue(TransitionUnderTest.IsInterrupted);

            TransitionUnderTest.Complete();
            Assert.IsFalse(TransitionUnderTest.IsCompleted);
            Assert.IsTrue(TransitionUnderTest.IsInterrupted);
        }
    }

  
}
