namespace Tech
{
    using System.Data;
    using System.Diagnostics;

    public class TechnologyTraverser
    {
        public TechnologyTraverser(Dictionary<string, Technology> technologiesByName, string[] realTechOrder, string[] neededTechs)
        {
            TechnologiesByName = technologiesByName
                .Values
                .Where(t => !t.Hidden)
                .ToDictionary(t => t.Name);

            ResearchedTechs = new HashSet<string>(realTechOrder);
            NeededTechs = new HashSet<string>(neededTechs);
            RealTechOrder = realTechOrder.ToList();

            foreach(var technology in TechnologiesByName.Values)
            {
                var prerequisites = technology.GetPrequisitesSafe();
                var name = technology.Name;

                if (!prerequisites.Any())
                {
                    Roots.Add(technology);
                }

                DependenciesByName[name] = new HashSet<string>(prerequisites);

                var sciencePackDependencies = GetSciencePackDependencies(technology);

                foreach(var spd in sciencePackDependencies)
                {
                    DependenciesByName[name].Add(spd);
                }

                technology.SciencePackPrio = GetSciencePackPrio(technology);

                FillScienceAmounts(technology);

                technology.IsResearched = ResearchedTechs.Contains(technology.Name);
            }

            int neededTechPrio = 0;
            foreach(var neededTech in NeededTechs)
            {
                if (!TechnologiesByName.ContainsKey(neededTech))
                {
                    Console.WriteLine($"neededTech: {neededTech} not exist");
                    continue;
                }
                ++neededTechPrio;
                
                var start = TechnologiesByName[neededTech];
                MarkNeededTechs(start, neededTech, neededTechPrio);
            }

            int realTechOrderPrio = 0;
            foreach(var tech in RealTechOrder)
            {
                if (!TechnologiesByName.ContainsKey(tech))
                {
                    Console.WriteLine($"neededTech: {tech} not exist");
                    continue;
                }
                ++realTechOrderPrio;
                
                var start = TechnologiesByName[tech];
                start.RealTechOrderPrio = $"{realTechOrderPrio:00000}";
            }
        }

        private void MarkNeededTechs(Technology start, string neededTech, int prio)
        {
            if (start.Visited)
                return;

            start.Visited = true;
            start.NeededFor = $"{prio:0000}_{neededTech}";
            if (DependenciesByName.TryGetValue(start.Name, out var dependencies))
            {
                foreach(var next in dependencies.Select(d => TechnologiesByName[d]))
                {
                    MarkNeededTechs(next, neededTech, prio);
                }
            }
        }

        public Dictionary<string, Technology> TechnologiesByName { get; }

        private Dictionary<string, HashSet<string>> DependenciesByName { get; } = new Dictionary<string, HashSet<string>>();

        public List<Technology> Roots { get; } = new List<Technology>();

        public HashSet<string> ResearchedTechs { get; }
        public HashSet<string> NeededTechs { get; }
        public List<string> RealTechOrder { get; }

        public IEnumerable<Technology> Traverse(Technology root)
        {
            var remainingList = TechnologiesByName.Values.ToList();
            var visitedTechNames = new HashSet<string>();
            var currentLabFactor = 1M;
            
            var currentTech = root;
            visitedTechNames.Add(currentTech.Name);
            CheckIsDirectNext(currentTech);
            CalcLabsAt1SPM(currentTech, ref currentLabFactor);

            yield return currentTech;
            remainingList.Remove(currentTech);

            while (remainingList.Count > 0)
            {
                bool found = false;
                foreach(var next in remainingList
                    .OrderBy(r => string.IsNullOrEmpty(r.RealTechOrderPrio) ? "99999" : r.RealTechOrderPrio)
                    .ThenBy(r => string.IsNullOrEmpty(r.NeededFor) ? "9999_zzzzzzzzzz" : r.NeededFor)
                    .ThenBy(r => r.SciencePackPrio)
                    .ThenByDescending(r => r.IsResearched)
                    .ThenBy(r => r.Order1))
                {
                    if (DependenciesByName.TryGetValue(next.Name, out var dependencies))
                    {
                        if (dependencies.All(visitedTechNames.Contains))
                        {
                            currentTech = next;
                            found = true;
                            break;
                        }
                    }
                }

                if (found)
                {
                    visitedTechNames.Add(currentTech.Name);
                    CheckIsDirectNext(currentTech);
                    CalcLabsAt1SPM(currentTech, ref currentLabFactor);

                    yield return currentTech;
                    remainingList.Remove(currentTech);
                }
                else
                {
                    Debug.Fail("No next found");
                    break;
                }
            }
        }

        private void CheckIsDirectNext(Technology currentTech)
        {
            if (!currentTech.IsResearched)
            {
                currentTech.IsDirectNext = currentTech.GetPrequisitesSafe()
                    .All(pre => TechnologiesByName.TryGetValue(pre, out Technology? value) ? value.IsResearched : false);
            }
        }

        private void CalcLabsAt1SPM(Technology currentTech, ref decimal currentLabFactor)
        {
            currentTech.LabCountFor1SPM = currentTech.ScienceTime / currentLabFactor;
            if (currentTech.Name.StartsWith("research-speed"))
            {
                switch (currentTech.Name)
                {
                    case "research-speed-1": 
                        currentLabFactor += 0.2M;
                        break;
                    case "research-speed-2": 
                        currentLabFactor += 0.3M;
                        break;
                    case "research-speed-3": 
                        currentLabFactor += 0.4M;
                        break;
                    case "research-speed-4": 
                        currentLabFactor += 0.5M;
                        break;
                    case "research-speed-5": 
                        currentLabFactor += 0.5M;
                        break;
                    case "research-speed-6": 
                        currentLabFactor += 0.6M;
                        break;
                }
            }
        }

        private static readonly Dictionary<Name, string> SpdMapping = new()
        {
            { Name.AutomationSciencePack, "sct-automation-science-pack" },
            { Name.LogisticSciencePack, "logistic-science-pack" },
            { Name.ChemicalSciencePack, "chemical-science-pack" },
            { Name.MilitarySciencePack, "military-science-pack" },
            { Name.ProductionSciencePack, "production-science-pack" },
            { Name.AdvancedLogisticSciencePack, "advanced-logistic-science-pack" },
            { Name.UtilitySciencePack, "utility-science-pack" },
            { Name.SpaceSciencePack, "space-science-pack" },
            { Name.SctBioSciencePack, "sct-bio-science-pack" },
        };

        private static List<string> GetSciencePackDependencies(Technology technology)
        {
            var spds = new List<string>();

            foreach (var ingredient in technology.ResearchUnitIngredients.ResearchUnitIngredientArray ?? Array.Empty<ResearchUnitIngredient>())
            {
                var sciencePack = ingredient?.Name ?? Name.AlienSciencePack;

                if (SpdMapping.TryGetValue(sciencePack, out var name))
                {
                    spds.Add(name);
                }
            }
            return spds;
        }

        private static readonly Dictionary<Name, int> SpdPrioMapping = new()
        {
            { Name.AutomationSciencePack, 1 },
            { Name.LogisticSciencePack, 10 },
            { Name.SctBioSciencePack, 20 },
            { Name.MilitarySciencePack, 50 },
            { Name.ChemicalSciencePack, 100 },
            { Name.ProductionSciencePack, 4000 },
            { Name.AdvancedLogisticSciencePack, 8000 },
            { Name.UtilitySciencePack, 10000 },
            { Name.SpaceSciencePack, 100000 },
        };

        private static int GetSciencePackPrio(Technology technology)
        {
            var prioSum = 0;
            foreach (var ingredient in technology.ResearchUnitIngredients.ResearchUnitIngredientArray ?? Array.Empty<ResearchUnitIngredient>())
            {
                var sciencePack = ingredient?.Name ?? Name.AlienSciencePack;

                if (SpdPrioMapping.TryGetValue(sciencePack, out var prio))
                {
                    prioSum += prio;
                }
            }
            return prioSum;
        }

        private static void FillScienceAmounts(Technology technology)
        {
            foreach (var ingredient in technology.ResearchUnitIngredients.ResearchUnitIngredientArray ?? Array.Empty<ResearchUnitIngredient>())
            {
                var sciencePack = ingredient?.Name ?? Name.AlienSciencePack;
                
                int count = (int)(ingredient?.Amount ?? 0);

                switch(sciencePack)
                {
                    case Name.AutomationSciencePack:
                        technology.AutomaticSciencePacks = count;
                        break;
                    case Name.LogisticSciencePack:
                        technology.LogisticSciencePacks = count;
                        break;
                    case Name.SctBioSciencePack:
                        technology.BioSciencePacks = count;
                        break;
                    case Name.ChemicalSciencePack:
                        technology.ChemicalSciencePacks = count;
                        break;
                    case Name.MilitarySciencePack:
                        technology.MilitarySciencePacks = count;
                        break;
                    case Name.ProductionSciencePack:
                        technology.ProductionSciencePacks = count;
                        break;
                    case Name.AdvancedLogisticSciencePack:
                        technology.AdvancedLogisticSciencePacks = count;
                        break;
                    case Name.UtilitySciencePack:
                        technology.UtilitySciencePacks = count;
                        break;
                    case Name.SpaceSciencePack:
                        technology.SpaceSciencePacks = count;
                        break;
                }
            }

            technology.ScienceCycles = (int)technology.ResearchUnitCount;
            technology.ScienceTime = (int)technology.ResearchUnitEnergy/60;
        }

    }
}