// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyGenetic
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9934BEA5-68E8-4B2F-A67B-419551EEF0D8
// Assembly location: D:\OneDrive - 9a1b\00 - Apps\StockSharpApps\Terminal\StockSharp.Web.DomainModel.dll

using Ecng.Serialization;
using System;

namespace StockSharp.Web.DomainModel
{
    public class StrategyGenetic : IPersistable
    {
        public string Fitness { get; set; }

        public int Population { get; set; }

        public int PopulationMax { get; set; }

        public int GenerationsMax { get; set; }

        public int GenerationsStagnation { get; set; }

        public Decimal MutationProbability { get; set; }

        public Decimal CrossoverProbability { get; set; }

        public StrategyGeneticReinsertions Reinsertion { get; set; }

        public StrategyGeneticMutations Mutation { get; set; }

        public StrategyGeneticCrossovers Crossover { get; set; }

        public StrategyGeneticSelections Selection { get; set; }

        public virtual void Load( SettingsStorage storage )
        {
            this.Fitness = ( string ) storage.GetValue<string>( "Fitness", null );
            this.Population = ( int ) storage.GetValue<int>( "Population", 0 );
            this.PopulationMax = ( int ) storage.GetValue<int>( "PopulationMax", 0 );
            this.GenerationsMax = ( int ) storage.GetValue<int>( "GenerationsMax", 0 );
            this.GenerationsStagnation = ( int ) storage.GetValue<int>( "GenerationsStagnation", 0 );
            this.MutationProbability = ( Decimal ) storage.GetValue<Decimal>( "MutationProbability", Decimal.Zero );
            this.CrossoverProbability = ( Decimal ) storage.GetValue<Decimal>( "CrossoverProbability", Decimal.Zero );
            this.Reinsertion = ( StrategyGeneticReinsertions ) storage.GetValue<StrategyGeneticReinsertions>( "Reinsertion", 0 );
            this.Mutation = ( StrategyGeneticMutations ) storage.GetValue<StrategyGeneticMutations>( "Mutation", 0 );
            this.Crossover = ( StrategyGeneticCrossovers ) storage.GetValue<StrategyGeneticCrossovers>( "Crossover", 0 );
            this.Selection = ( StrategyGeneticSelections ) storage.GetValue<StrategyGeneticSelections>( "Selection", 0 );
        }

        public virtual void Save( SettingsStorage storage )
        {
            storage.Set<string>( "Fitness", this.Fitness ).Set<int>( "Population", this.Population ).Set<int>( "PopulationMax", this.PopulationMax ).Set<int>( "GenerationsMax", this.GenerationsMax ).Set<int>( "GenerationsStagnation", this.GenerationsStagnation ).Set<Decimal>( "MutationProbability", this.MutationProbability ).Set<Decimal>( "CrossoverProbability", this.CrossoverProbability ).Set<StrategyGeneticReinsertions>( "Reinsertion", this.Reinsertion ).Set<StrategyGeneticMutations>( "Mutation", this.Mutation ).Set<StrategyGeneticCrossovers>( "Crossover", this.Crossover ).Set<StrategyGeneticSelections>( "Selection", this.Selection );
        }
    }
}
