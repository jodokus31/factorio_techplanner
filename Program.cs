namespace Tech
{
    using Newtonsoft.Json;

    public static class Techlist
    {
        public static void Main(string[] args)
        {
            var allTechs = JsonConvert
                .DeserializeObject<Technologies>(
                    File.ReadAllText("technology_0.5.12.json"), 
                    Converter.Settings)
                ?.Technology 
                ?? new Dictionary<string, Technology>();
                
            var neededTechs = ReadLinesFiltered("needed_techs_0.5.12.txt");
            //var neededTechs = ReadLinesFiltered("needed_techs_0.5.12.txt");
            //var neededTechs = ReadLinesFiltered("needed_techs_pre_spacex.txt");

            //var realTechOrder = Array.Empty<string>();
            var realTechOrder = ReadLinesFiltered("real_tech_order.txt");
            
            using (var outputFile = new StreamWriter("techs.csv"))
            {
                var traverser = new TechnologyTraverser(allTechs, realTechOrder, neededTechs);
                foreach(var root in traverser.Roots)
                {
                    int i=0;
                    outputFile.WriteLine($"RealOrder;InternalName;Name;SciencePackPrio;Order;Researched;IsDirectNext;NeededFor;A;L;C;P;AL;U;S;B;M;Count;Time;LabCountFor1SPM;TechTime;A1;L1;C1;P1;AL1;U1;S1;B1;M1");
                    int a1=0,l1=0,c1=0,p1=0,al1=0,u1=0,s1=0,b1=0,m1=0;
                    foreach(var t in traverser.Traverse(root))
                    {
                        ++i;
                        a1+=t.A1;l1+=t.L1;c1+=t.C1;p1+=t.P1;al1+=t.AL1;u1+=t.U1;s1+=t.S1;b1+=t.B1;m1+=t.M1;
                        //Console.WriteLine($"{i}. Tech: {t.Name}");
                        outputFile.WriteLine($"{t.RealTechOrderPrio};{t.Name};{t.TranslatedName};{t.SciencePackPrio};{t.Order};{(t.IsResearched ? "X" : "")};{(t.IsDirectNext ? "X" : "")};{t.NeededFor}"+
                            $";{t.A};{t.L};{t.C};{t.P};{t.AL};{t.U};{t.S};{t.B};{t.M};{t.ScienceCycles};{t.ScienceTime};{t.LabCountFor1SPM:F2};{t.ScienceTechTime}"+
                            //$";{t.A1};{t.L1};{t.C1};{t.P1};{t.AL1};{t.U1};{t.S1};{t.B1};{t.M1}");
                            $";{a1};{l1};{c1};{p1};{al1};{u1};{s1};{b1};{m1}");
                    }
                }
                var allCount = traverser.TechnologiesByName.Values.Count;
                Console.WriteLine($"{allCount} technologies.");
            }
        }  

        private static string[] ReadLinesFiltered(string filename)
        {
            return File.ReadAllLines(filename)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => l.Trim())
                .Where(l => !l.StartsWith("--") && !l.StartsWith("//"))
                .ToArray();
        }
    }
}