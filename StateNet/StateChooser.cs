﻿using System.Collections.Generic;
using System.Linq;
using Aptacode.StateNet.Interfaces;

namespace Aptacode.StateNet
{
    public class StateChooser
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly List<State> _stateHistory;

        public StateChooser(IRandomNumberGenerator randomNumberGenerator, List<State> stateHistory)
        {
            _randomNumberGenerator = randomNumberGenerator;
            _stateHistory = stateHistory;
        }

        /// <summary>
        /// Chooses the next state based on the given StateDistribution
        /// </summary>
        /// <param name="connections"></param>
        /// <returns></returns>
        public State Choose(StateDistribution connections)
        {
            var connectionWeights = GetConnectionWeights(connections).ToList();

            //If the total weight is 0 no state can be entered
            var totalWeight = TotalWeight(connectionWeights);
            if (totalWeight == 0)
            {
                return null;
            }

            //Get a random number between 1 and the totalWeight + 1
            var choice = _randomNumberGenerator.Generate(1, totalWeight + 1);

            //Loop over each connection weight pair keeping a tally of the weight of each connection passed
            //Return the state where weightCounter >= choice
            using (var iterator = connectionWeights.GetEnumerator())
            {
                var weightCounter = 0;
                while (weightCounter < choice && iterator.MoveNext())
                {
                    weightCounter += iterator.Current.Item2;
                }

                return iterator.Current.Item1;
            }
        }

        /// <summary>
        /// Calculates the weight of each connections in the StateDistribution given the current state history and returns a list of State,Weight pairs
        /// </summary>
        /// <param name="connections"></param>
        /// <returns></returns>
        private IEnumerable<(State, int)> GetConnectionWeights(StateDistribution connections) =>
            connections
                .GetAll()
                .Select(f => (f.Key, f.Value.GetConnectionWeight(_stateHistory)));

        /// <summary>
        /// Calculates the sum of each connection in the given StateDistribution 
        /// </summary>
        /// <param name="weights"></param>
        private int TotalWeight(IEnumerable<(State, int)> weights) => 
            weights
                .Sum(f => f.Item2 >= 0 ? f.Item2 : 0);


        /// <summary>
        /// Calculates the sum of each connections weight in the StateDistribution given the current StateHistory
        /// </summary>
        /// <param name="connections"></param>
        /// <returns></returns>
        public int TotalWeight(StateDistribution connections) => TotalWeight(GetConnectionWeights(connections));



    }
}