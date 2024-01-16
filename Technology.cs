namespace Tech
{
    using System.Diagnostics;

    [DebuggerDisplay("{Name} {SciencePackPrio} {Order}")]
    public partial class Technology
    {
        public List<Technology> Children { get; } = new List<Technology>();

        public bool Visited { get; set; }

        public bool IsResearched {get; set;}

        public bool IsDirectNext {get; set;}

        public decimal LabCountFor1SPM {get; set;}

        public string? NeededFor { get; set; }

        public int SciencePackPrio {get; set;}

        public string Order1 
        {
            get
            {
                return Order?.Replace("[", "z[") ?? "";
            }
        }

        public string? RealTechOrderPrio {get; set;}

        public string[] GetPrequisitesSafe()
        {
            return Prerequisites.StringArray ?? Array.Empty<string>();
        }

        public int AutomaticSciencePacks {get; set;}
        public int LogisticSciencePacks {get; set;}
        public int BioSciencePacks {get; set;}
        public int MilitarySciencePacks {get; set;}
        public int ChemicalSciencePacks {get; set;}
        public int ProductionSciencePacks {get; set;}
        public int AdvancedLogisticSciencePacks {get; set;}
        public int UtilitySciencePacks {get; set;}
        public int SpaceSciencePacks {get; set;}

        public int ScienceCycles {get; set;}

        public int ScienceTime {get; set;}

        public int ScienceTechTime 
        {
            get
            {
                return ScienceCycles*ScienceTime;
            }

        }

        private static string CountOrEmpty(int count)
        {
            return count > 0 ? count.ToString() : "";
        }
        public string A => CountOrEmpty(AutomaticSciencePacks);
        public string L => CountOrEmpty(LogisticSciencePacks);
        public string B => CountOrEmpty(BioSciencePacks);
        public string M => CountOrEmpty(MilitarySciencePacks);
        public string C => CountOrEmpty(ChemicalSciencePacks);
        public string P => CountOrEmpty(ProductionSciencePacks);
        public string AL => CountOrEmpty(AdvancedLogisticSciencePacks);
        public string U => CountOrEmpty(UtilitySciencePacks);
        public string S => CountOrEmpty(SpaceSciencePacks);

        private int CountOrEmptyOnlyNeededFor(int count)
        {
            return string.IsNullOrEmpty(NeededFor) 
                ? 0 
                : count > 0 
                    ? (count*ScienceCycles) 
                    : 0;
        }
        public int A1 => CountOrEmptyOnlyNeededFor(AutomaticSciencePacks);
        public int L1 => CountOrEmptyOnlyNeededFor(LogisticSciencePacks);
        public int B1 => CountOrEmptyOnlyNeededFor(BioSciencePacks);
        public int M1 => CountOrEmptyOnlyNeededFor(MilitarySciencePacks);
        public int C1 => CountOrEmptyOnlyNeededFor(ChemicalSciencePacks);
        public int P1 => CountOrEmptyOnlyNeededFor(ProductionSciencePacks);
        public int AL1 => CountOrEmptyOnlyNeededFor(AdvancedLogisticSciencePacks);
        public int U1 => CountOrEmptyOnlyNeededFor(UtilitySciencePacks);
        public int S1 => CountOrEmptyOnlyNeededFor(SpaceSciencePacks);


    }
}