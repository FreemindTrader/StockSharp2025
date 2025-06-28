// Decompiled with JetBrains decompiler
// Type: StockSharp.Web.DomainModel.StrategyGenetic
// Assembly: StockSharp.Web.DomainModel, Version=5.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A12D0EDB-6AAE-47CD-AD7D-1699114722F7
// Assembly location: C:\Users\tonyfreemind\AppData\Local\StockSharp\products\apps_terminal\StockSharp.Web.DomainModel.dll

using System;
using Ecng.Serialization;

#nullable disable
namespace StockSharp.Web.DomainModel;

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

    public virtual void Load(SettingsStorage storage)
    {
        this.Fitness = storage.GetValue<string>("Fitness", (string)null);
        this.Population = storage.GetValue<int>("Population", 0);
        this.PopulationMax = storage.GetValue<int>("PopulationMax", 0);
        this.GenerationsMax = storage.GetValue<int>("GenerationsMax", 0);
        this.GenerationsStagnation = storage.GetValue<int>("GenerationsStagnation", 0);
        this.MutationProbability = storage.GetValue<Decimal>("MutationProbability", 0M);
        this.CrossoverProbability = storage.GetValue<Decimal>("CrossoverProbability", 0M);
        this.Reinsertion = storage.GetValue<StrategyGeneticReinsertions>("Reinsertion", StrategyGeneticReinsertions.Elitist);
        this.Mutation = storage.GetValue<StrategyGeneticMutations>("Mutation", StrategyGeneticMutations.Displacement);
        this.Crossover = storage.GetValue<StrategyGeneticCrossovers>("Crossover", StrategyGeneticCrossovers.AlternatingPosition);
        this.Selection = storage.GetValue<StrategyGeneticSelections>("Selection", StrategyGeneticSelections.Elite);
    }

    public virtual void Save(SettingsStorage storage)
    {
        storage.Set<string>("Fitness", this.Fitness).Set<int>("Population", this.Population).Set<int>("PopulationMax", this.PopulationMax).Set<int>("GenerationsMax", this.GenerationsMax).Set<int>("GenerationsStagnation", this.GenerationsStagnation).Set<Decimal>("MutationProbability", this.MutationProbability).Set<Decimal>("CrossoverProbability", this.CrossoverProbability).Set<StrategyGeneticReinsertions>("Reinsertion", this.Reinsertion).Set<StrategyGeneticMutations>("Mutation", this.Mutation).Set<StrategyGeneticCrossovers>("Crossover", this.Crossover).Set<StrategyGeneticSelections>("Selection", this.Selection);
    }
}
