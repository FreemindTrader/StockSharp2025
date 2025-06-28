using Ecng.Collections;
using GeneticSharp;
using StockSharp.Localization;
using StockSharp.Web.DomainModel;
using System;

#nullable disable
namespace StockSharp.Studio.Core;

[CLSCompliant(false)]
public static class GeneticExtensions
{
    private static readonly PairSet<Type, StrategyGeneticReinsertions> _reinsertions;
    private static readonly PairSet<Type, StrategyGeneticMutations> _mutations;
    private static readonly PairSet<Type, StrategyGeneticCrossovers> _crossovers;
    private static readonly PairSet<Type, StrategyGeneticSelections> _selections;

    public static Type ToType(this StrategyGeneticReinsertions value)
    {
        Type type;
        if (GeneticExtensions._reinsertions.TryGetKey(value, out type))
            return type;
        throw new ArgumentOutOfRangeException(nameof(value), (object)value, LocalizedStrings.InvalidValue);
    }

    public static StrategyGeneticReinsertions ReinsertionToEnum(this Type value)
    {
        if ((object)value == null)
            throw new ArgumentNullException(nameof(value));
        StrategyGeneticReinsertions geneticReinsertions;
        if (((KeyedCollection<Type, StrategyGeneticReinsertions>)GeneticExtensions._reinsertions).TryGetValue(value, out geneticReinsertions))
            return geneticReinsertions;
        throw new ArgumentOutOfRangeException(nameof(value), (object)value, LocalizedStrings.InvalidValue);
    }

    public static Type ToType(this StrategyGeneticMutations v)
    {
        Type type;
        if (GeneticExtensions._mutations.TryGetKey(v, out type))
            return type;
        throw new ArgumentOutOfRangeException(nameof(v), (object)v, LocalizedStrings.InvalidValue);
    }

    public static StrategyGeneticMutations MutationToEnum(this Type value)
    {
        if ((object)value == null)
            throw new ArgumentNullException(nameof(value));
        StrategyGeneticMutations geneticMutations;
        if (((KeyedCollection<Type, StrategyGeneticMutations>)GeneticExtensions._mutations).TryGetValue(value, out geneticMutations))
            return geneticMutations;
        throw new ArgumentOutOfRangeException(nameof(value), (object)value, LocalizedStrings.InvalidValue);
    }

    public static Type ToType(this StrategyGeneticCrossovers v)
    {
        Type type;
        if (GeneticExtensions._crossovers.TryGetKey(v, out type))
            return type;
        throw new ArgumentOutOfRangeException(nameof(v), (object)v, LocalizedStrings.InvalidValue);
    }

    public static StrategyGeneticCrossovers CrossoverToEnum(this Type value)
    {
        if ((object)value == null)
            throw new ArgumentNullException(nameof(value));
        StrategyGeneticCrossovers geneticCrossovers;
        if (((KeyedCollection<Type, StrategyGeneticCrossovers>)GeneticExtensions._crossovers).TryGetValue(value, out geneticCrossovers))
            return geneticCrossovers;
        throw new ArgumentOutOfRangeException(nameof(value), (object)value, LocalizedStrings.InvalidValue);
    }

    public static Type ToType(this StrategyGeneticSelections v)
    {
        Type type;
        if (GeneticExtensions._selections.TryGetKey(v, out type))
            return type;
        throw new ArgumentOutOfRangeException(nameof(v), (object)v, LocalizedStrings.InvalidValue);
    }

    public static StrategyGeneticSelections SelectionToEnum(this Type value)
    {
        if ((object)value == null)
            throw new ArgumentNullException(nameof(value));
        StrategyGeneticSelections geneticSelections;
        if (((KeyedCollection<Type, StrategyGeneticSelections>)GeneticExtensions._selections).TryGetValue(value, out geneticSelections))
            return geneticSelections;
        throw new ArgumentOutOfRangeException(nameof(value), (object)value, LocalizedStrings.InvalidValue);
    }

    static GeneticExtensions()
    {
        PairSet<Type, StrategyGeneticReinsertions> pairSet1 = new PairSet<Type, StrategyGeneticReinsertions>();
        ((KeyedCollection<Type, StrategyGeneticReinsertions>)pairSet1).Add(typeof(ElitistReinsertion), StrategyGeneticReinsertions.Elitist);
        ((KeyedCollection<Type, StrategyGeneticReinsertions>)pairSet1).Add(typeof(FitnessBasedReinsertion), StrategyGeneticReinsertions.FitnessBased);
        ((KeyedCollection<Type, StrategyGeneticReinsertions>)pairSet1).Add(typeof(PureReinsertion), StrategyGeneticReinsertions.Pure);
        ((KeyedCollection<Type, StrategyGeneticReinsertions>)pairSet1).Add(typeof(UniformReinsertion), StrategyGeneticReinsertions.Uniform);
        GeneticExtensions._reinsertions = pairSet1;
        PairSet<Type, StrategyGeneticMutations> pairSet2 = new PairSet<Type, StrategyGeneticMutations>();
        ((KeyedCollection<Type, StrategyGeneticMutations>)pairSet2).Add(typeof(PartialShuffleMutation), StrategyGeneticMutations.PartialShuffle);
        ((KeyedCollection<Type, StrategyGeneticMutations>)pairSet2).Add(typeof(FlipBitMutation), StrategyGeneticMutations.FlipBit);
        ((KeyedCollection<Type, StrategyGeneticMutations>)pairSet2).Add(typeof(TworsMutation), StrategyGeneticMutations.Twors);
        ((KeyedCollection<Type, StrategyGeneticMutations>)pairSet2).Add(typeof(UniformMutation), StrategyGeneticMutations.Uniform);
        ((KeyedCollection<Type, StrategyGeneticMutations>)pairSet2).Add(typeof(DisplacementMutation), StrategyGeneticMutations.Displacement);
        ((KeyedCollection<Type, StrategyGeneticMutations>)pairSet2).Add(typeof(InsertionMutation), StrategyGeneticMutations.Insertion);
        ((KeyedCollection<Type, StrategyGeneticMutations>)pairSet2).Add(typeof(ReverseSequenceMutation), StrategyGeneticMutations.ReverseSequence);
        GeneticExtensions._mutations = pairSet2;
        PairSet<Type, StrategyGeneticCrossovers> pairSet3 = new PairSet<Type, StrategyGeneticCrossovers>();
        ((KeyedCollection<Type, StrategyGeneticCrossovers>)pairSet3).Add(typeof(AlternatingPositionCrossover), StrategyGeneticCrossovers.AlternatingPosition);
        ((KeyedCollection<Type, StrategyGeneticCrossovers>)pairSet3).Add(typeof(CutAndSpliceCrossover), StrategyGeneticCrossovers.CutAndSplice);
        ((KeyedCollection<Type, StrategyGeneticCrossovers>)pairSet3).Add(typeof(CycleCrossover), StrategyGeneticCrossovers.Cycle);
        ((KeyedCollection<Type, StrategyGeneticCrossovers>)pairSet3).Add(typeof(OnePointCrossover), StrategyGeneticCrossovers.OnePoint);
        ((KeyedCollection<Type, StrategyGeneticCrossovers>)pairSet3).Add(typeof(TwoPointCrossover), StrategyGeneticCrossovers.TwoPoint);
        ((KeyedCollection<Type, StrategyGeneticCrossovers>)pairSet3).Add(typeof(OrderBasedCrossover), StrategyGeneticCrossovers.OrderBased);
        ((KeyedCollection<Type, StrategyGeneticCrossovers>)pairSet3).Add(typeof(OrderedCrossover), StrategyGeneticCrossovers.Ordered);
        ((KeyedCollection<Type, StrategyGeneticCrossovers>)pairSet3).Add(typeof(PartiallyMappedCrossover), StrategyGeneticCrossovers.PartiallyMapped);
        ((KeyedCollection<Type, StrategyGeneticCrossovers>)pairSet3).Add(typeof(PositionBasedCrossover), StrategyGeneticCrossovers.PositionBased);
        ((KeyedCollection<Type, StrategyGeneticCrossovers>)pairSet3).Add(typeof(ThreeParentCrossover), StrategyGeneticCrossovers.ThreeParent);
        ((KeyedCollection<Type, StrategyGeneticCrossovers>)pairSet3).Add(typeof(UniformCrossover), StrategyGeneticCrossovers.Uniform);
        ((KeyedCollection<Type, StrategyGeneticCrossovers>)pairSet3).Add(typeof(VotingRecombinationCrossover), StrategyGeneticCrossovers.VotingRecombination);
        GeneticExtensions._crossovers = pairSet3;
        PairSet<Type, StrategyGeneticSelections> pairSet4 = new PairSet<Type, StrategyGeneticSelections>();
        ((KeyedCollection<Type, StrategyGeneticSelections>)pairSet4).Add(typeof(EliteSelection), StrategyGeneticSelections.Elite);
        ((KeyedCollection<Type, StrategyGeneticSelections>)pairSet4).Add(typeof(RankSelection), StrategyGeneticSelections.Rank);
        ((KeyedCollection<Type, StrategyGeneticSelections>)pairSet4).Add(typeof(RouletteWheelSelection), StrategyGeneticSelections.RouletteWheel);
        ((KeyedCollection<Type, StrategyGeneticSelections>)pairSet4).Add(typeof(StochasticUniversalSamplingSelection), StrategyGeneticSelections.StochasticUniversalSampling);
        ((KeyedCollection<Type, StrategyGeneticSelections>)pairSet4).Add(typeof(TournamentSelection), StrategyGeneticSelections.Tournament);
        ((KeyedCollection<Type, StrategyGeneticSelections>)pairSet4).Add(typeof(TruncationSelection), StrategyGeneticSelections.Truncation);
        GeneticExtensions._selections = pairSet4;
    }
}
