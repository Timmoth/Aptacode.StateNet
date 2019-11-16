﻿namespace Aptacode.StateNet.TransitionResult
{
    public class NaryTransitionResult : TransitionResult
    {
        /// <summary>
        /// Tells a UnaryTransition if the Transition was successful and if so whether to move into the left or right
        /// state
        /// </summary>
        /// <param name="message"></param>
        /// <param name="success"></param>
        public NaryTransitionResult(string choice, string message, bool success) : base(message, success) => Choice =
            choice;

        /// <summary>
        /// Tells a UnaryTransition if the Transition was successful and if so whether to move into the left or right
        /// state
        /// </summary>
        /// <param name="message"></param>
        /// <param name="success"></param>
        public NaryTransitionResult(string choice, string message) : this(choice, message, true) { }

        public string Choice { get; }
    }
}