// namespace Tech
// {
//     using System;
//     using System.Collections.Generic;

//     using System.Globalization;
//     using Newtonsoft.Json;
//     using Newtonsoft.Json.Converters;

//     public partial class Technologies
//     {
//         [JsonProperty("technology")]
//         public Dictionary<string, Technology> Technology { get; set; }
//     }

//     public partial class Technology
//     {
//         [JsonProperty("name")]
//         public string Name { get; set; }

//         [JsonProperty("localised_name")]
//         public LocalisedName[] LocalisedName { get; set; }

//         [JsonProperty("hidden")]
//         public bool Hidden { get; set; }

//         [JsonProperty("order")]
//         public string Order { get; set; }

//         [JsonProperty("effects")]
//         public EffectsUnion Effects { get; set; }

//         [JsonProperty("research_unit_ingredients")]
//         public ResearchUnitIngredients ResearchUnitIngredients { get; set; }

//         [JsonProperty("research_unit_count")]
//         public long ResearchUnitCount { get; set; }

//         [JsonProperty("research_unit_energy")]
//         public long ResearchUnitEnergy { get; set; }

//         [JsonProperty("max_level")]
//         public long MaxLevel { get; set; }

//         [JsonProperty("prerequisites")]
//         public Prerequisites Prerequisites { get; set; }

//         [JsonProperty("translated_name")]
//         public string TranslatedName { get; set; }

//         [JsonProperty("research_unit_count_formula", NullValueHandling = NullValueHandling.Ignore)]
//         public string ResearchUnitCountFormula { get; set; }
//     }

//     public partial class Effect
//     {
//         [JsonProperty("type")]
//         public EffectType Type { get; set; }

//         [JsonProperty("recipe", NullValueHandling = NullValueHandling.Ignore)]
//         public string Recipe { get; set; }

//         [JsonProperty("modifier", NullValueHandling = NullValueHandling.Ignore)]
//         public Modifier? Modifier { get; set; }

//         [JsonProperty("ammo_category", NullValueHandling = NullValueHandling.Ignore)]
//         public string AmmoCategory { get; set; }

//         [JsonProperty("turret_id", NullValueHandling = NullValueHandling.Ignore)]
//         public string TurretId { get; set; }
//     }

//     public partial class EffectsClass
//     {
//     }

//     public partial class ResearchUnitIngredient
//     {
//         [JsonProperty("type")]
//         public ResearchUnitIngredientType Type { get; set; }

//         [JsonProperty("name")]
//         public Name Name { get; set; }

//         [JsonProperty("amount")]
//         public long Amount { get; set; }
//     }

//     public enum EffectType { AmmoDamage, ArtilleryRange, CharacterInventorySlotsBonus, CharacterLogisticRequests, CharacterLogisticTrashSlots, CharacterMiningSpeed, GhostTimeToLive, GunSpeed, InserterStackSizeBonus, LaboratorySpeed, MaximumFollowingRobotsCount, StackInserterCapacityBonus, TrainBrakingForceBonus, TurretAttack, UnlockRecipe, WorkerRobotSpeed, WorkerRobotStorage };

//     public enum Name { AdvancedLogisticSciencePack, AlienArtifactBlueTool, AlienArtifactGreenTool, AlienArtifactOrangeTool, AlienArtifactPurpleTool, AlienArtifactRedTool, AlienArtifactTool, AlienArtifactYellowTool, AlienSciencePack, AlienSciencePackBlue, AlienSciencePackGreen, AlienSciencePackOrange, AlienSciencePackPurple, AlienSciencePackRed, AlienSciencePackYellow, AutomationSciencePack, ChemicalSciencePack, LogisticSciencePack, MilitarySciencePack, ProductionSciencePack, SbAngelsore3Tool, SbBasicCircuitBoardTool, SbLabTool, SciencePackGold, SctBioSciencePack, SpaceSciencePack, UtilitySciencePack };

//     public enum ResearchUnitIngredientType { Item };

//     public partial struct Modifier
//     {
//         public bool? Bool;
//         public double? Double;

//         public static implicit operator Modifier(bool Bool) => new Modifier { Bool = Bool };
//         public static implicit operator Modifier(double Double) => new Modifier { Double = Double };
//     }

//     public partial struct EffectsUnion
//     {
//         public Effect[] EffectArray;
//         public EffectsClass EffectsClass;

//         public static implicit operator EffectsUnion(Effect[] EffectArray) => new EffectsUnion { EffectArray = EffectArray };
//         public static implicit operator EffectsUnion(EffectsClass EffectsClass) => new EffectsUnion { EffectsClass = EffectsClass };
//     }

//     public partial struct LocalisedName
//     {
//         public string String;
//         public string[] StringArray;

//         public static implicit operator LocalisedName(string String) => new LocalisedName { String = String };
//         public static implicit operator LocalisedName(string[] StringArray) => new LocalisedName { StringArray = StringArray };
//     }

//     public partial struct Prerequisites
//     {
//         public EffectsClass EffectsClass;
//         public string[] StringArray;

//         public static implicit operator Prerequisites(EffectsClass EffectsClass) => new Prerequisites { EffectsClass = EffectsClass };
//         public static implicit operator Prerequisites(string[] StringArray) => new Prerequisites { StringArray = StringArray };
//     }

//     public partial struct ResearchUnitIngredients
//     {
//         public EffectsClass EffectsClass;
//         public ResearchUnitIngredient[] ResearchUnitIngredientArray;

//         public static implicit operator ResearchUnitIngredients(EffectsClass EffectsClass) => new ResearchUnitIngredients { EffectsClass = EffectsClass };
//         public static implicit operator ResearchUnitIngredients(ResearchUnitIngredient[] ResearchUnitIngredientArray) => new ResearchUnitIngredients { ResearchUnitIngredientArray = ResearchUnitIngredientArray };
//     }

//     internal static class Converter
//     {
//         public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
//         {
//             MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
//             DateParseHandling = DateParseHandling.None,
//             Converters =
//             {
//                 EffectsUnionConverter.Singleton,
//                 ModifierConverter.Singleton,
//                 EffectTypeConverter.Singleton,
//                 LocalisedNameConverter.Singleton,
//                 PrerequisitesConverter.Singleton,
//                 ResearchUnitIngredientsConverter.Singleton,
//                 NameConverter.Singleton,
//                 ResearchUnitIngredientTypeConverter.Singleton,
//                 new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
//             },
//         };
//     }

//     internal class EffectsUnionConverter : JsonConverter
//     {
//         public override bool CanConvert(Type t) => t == typeof(EffectsUnion) || t == typeof(EffectsUnion?);

//         public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//         {
//             switch (reader.TokenType)
//             {
//                 case JsonToken.StartObject:
//                     var objectValue = serializer.Deserialize<EffectsClass>(reader);
//                     return new EffectsUnion { EffectsClass = objectValue };
//                 case JsonToken.StartArray:
//                     var arrayValue = serializer.Deserialize<Effect[]>(reader);
//                     return new EffectsUnion { EffectArray = arrayValue };
//             }
//             throw new Exception("Cannot unmarshal type EffectsUnion");
//         }

//         public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//         {
//             var value = (EffectsUnion)untypedValue;
//             if (value.EffectArray != null)
//             {
//                 serializer.Serialize(writer, value.EffectArray);
//                 return;
//             }
//             if (value.EffectsClass != null)
//             {
//                 serializer.Serialize(writer, value.EffectsClass);
//                 return;
//             }
//             throw new Exception("Cannot marshal type EffectsUnion");
//         }

//         public static readonly EffectsUnionConverter Singleton = new EffectsUnionConverter();
//     }

//     internal class ModifierConverter : JsonConverter
//     {
//         public override bool CanConvert(Type t) => t == typeof(Modifier) || t == typeof(Modifier?);

//         public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//         {
//             switch (reader.TokenType)
//             {
//                 case JsonToken.Integer:
//                 case JsonToken.Float:
//                     var doubleValue = serializer.Deserialize<double>(reader);
//                     return new Modifier { Double = doubleValue };
//                 case JsonToken.Boolean:
//                     var boolValue = serializer.Deserialize<bool>(reader);
//                     return new Modifier { Bool = boolValue };
//             }
//             throw new Exception("Cannot unmarshal type Modifier");
//         }

//         public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//         {
//             var value = (Modifier)untypedValue;
//             if (value.Double != null)
//             {
//                 serializer.Serialize(writer, value.Double.Value);
//                 return;
//             }
//             if (value.Bool != null)
//             {
//                 serializer.Serialize(writer, value.Bool.Value);
//                 return;
//             }
//             throw new Exception("Cannot marshal type Modifier");
//         }

//         public static readonly ModifierConverter Singleton = new ModifierConverter();
//     }

//     internal class EffectTypeConverter : JsonConverter
//     {
//         public override bool CanConvert(Type t) => t == typeof(EffectType) || t == typeof(EffectType?);

//         public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//         {
//             if (reader.TokenType == JsonToken.Null) return null;
//             var value = serializer.Deserialize<string>(reader);
//             switch (value)
//             {
//                 case "ammo-damage":
//                     return EffectType.AmmoDamage;
//                 case "artillery-range":
//                     return EffectType.ArtilleryRange;
//                 case "character-inventory-slots-bonus":
//                     return EffectType.CharacterInventorySlotsBonus;
//                 case "character-logistic-requests":
//                     return EffectType.CharacterLogisticRequests;
//                 case "character-logistic-trash-slots":
//                     return EffectType.CharacterLogisticTrashSlots;
//                 case "character-mining-speed":
//                     return EffectType.CharacterMiningSpeed;
//                 case "ghost-time-to-live":
//                     return EffectType.GhostTimeToLive;
//                 case "gun-speed":
//                     return EffectType.GunSpeed;
//                 case "inserter-stack-size-bonus":
//                     return EffectType.InserterStackSizeBonus;
//                 case "laboratory-speed":
//                     return EffectType.LaboratorySpeed;
//                 case "maximum-following-robots-count":
//                     return EffectType.MaximumFollowingRobotsCount;
//                 case "stack-inserter-capacity-bonus":
//                     return EffectType.StackInserterCapacityBonus;
//                 case "train-braking-force-bonus":
//                     return EffectType.TrainBrakingForceBonus;
//                 case "turret-attack":
//                     return EffectType.TurretAttack;
//                 case "unlock-recipe":
//                     return EffectType.UnlockRecipe;
//                 case "worker-robot-speed":
//                     return EffectType.WorkerRobotSpeed;
//                 case "worker-robot-storage":
//                     return EffectType.WorkerRobotStorage;
//             }
//             throw new Exception("Cannot unmarshal type EffectType");
//         }

//         public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//         {
//             if (untypedValue == null)
//             {
//                 serializer.Serialize(writer, null);
//                 return;
//             }
//             var value = (EffectType)untypedValue;
//             switch (value)
//             {
//                 case EffectType.AmmoDamage:
//                     serializer.Serialize(writer, "ammo-damage");
//                     return;
//                 case EffectType.ArtilleryRange:
//                     serializer.Serialize(writer, "artillery-range");
//                     return;
//                 case EffectType.CharacterInventorySlotsBonus:
//                     serializer.Serialize(writer, "character-inventory-slots-bonus");
//                     return;
//                 case EffectType.CharacterLogisticRequests:
//                     serializer.Serialize(writer, "character-logistic-requests");
//                     return;
//                 case EffectType.CharacterLogisticTrashSlots:
//                     serializer.Serialize(writer, "character-logistic-trash-slots");
//                     return;
//                 case EffectType.CharacterMiningSpeed:
//                     serializer.Serialize(writer, "character-mining-speed");
//                     return;
//                 case EffectType.GhostTimeToLive:
//                     serializer.Serialize(writer, "ghost-time-to-live");
//                     return;
//                 case EffectType.GunSpeed:
//                     serializer.Serialize(writer, "gun-speed");
//                     return;
//                 case EffectType.InserterStackSizeBonus:
//                     serializer.Serialize(writer, "inserter-stack-size-bonus");
//                     return;
//                 case EffectType.LaboratorySpeed:
//                     serializer.Serialize(writer, "laboratory-speed");
//                     return;
//                 case EffectType.MaximumFollowingRobotsCount:
//                     serializer.Serialize(writer, "maximum-following-robots-count");
//                     return;
//                 case EffectType.StackInserterCapacityBonus:
//                     serializer.Serialize(writer, "stack-inserter-capacity-bonus");
//                     return;
//                 case EffectType.TrainBrakingForceBonus:
//                     serializer.Serialize(writer, "train-braking-force-bonus");
//                     return;
//                 case EffectType.TurretAttack:
//                     serializer.Serialize(writer, "turret-attack");
//                     return;
//                 case EffectType.UnlockRecipe:
//                     serializer.Serialize(writer, "unlock-recipe");
//                     return;
//                 case EffectType.WorkerRobotSpeed:
//                     serializer.Serialize(writer, "worker-robot-speed");
//                     return;
//                 case EffectType.WorkerRobotStorage:
//                     serializer.Serialize(writer, "worker-robot-storage");
//                     return;
//             }
//             throw new Exception("Cannot marshal type EffectType");
//         }

//         public static readonly EffectTypeConverter Singleton = new EffectTypeConverter();
//     }

//     internal class LocalisedNameConverter : JsonConverter
//     {
//         public override bool CanConvert(Type t) => t == typeof(LocalisedName) || t == typeof(LocalisedName?);

//         public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//         {
//             switch (reader.TokenType)
//             {
//                 case JsonToken.String:
//                 case JsonToken.Date:
//                     var stringValue = serializer.Deserialize<string>(reader);
//                     return new LocalisedName { String = stringValue };
//                 case JsonToken.StartArray:
//                     var arrayValue = serializer.Deserialize<string[]>(reader);
//                     return new LocalisedName { StringArray = arrayValue };
//             }
//             throw new Exception("Cannot unmarshal type LocalisedName");
//         }

//         public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//         {
//             var value = (LocalisedName)untypedValue;
//             if (value.String != null)
//             {
//                 serializer.Serialize(writer, value.String);
//                 return;
//             }
//             if (value.StringArray != null)
//             {
//                 serializer.Serialize(writer, value.StringArray);
//                 return;
//             }
//             throw new Exception("Cannot marshal type LocalisedName");
//         }

//         public static readonly LocalisedNameConverter Singleton = new LocalisedNameConverter();
//     }

//     internal class PrerequisitesConverter : JsonConverter
//     {
//         public override bool CanConvert(Type t) => t == typeof(Prerequisites) || t == typeof(Prerequisites?);

//         public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//         {
//             switch (reader.TokenType)
//             {
//                 case JsonToken.StartObject:
//                     var objectValue = serializer.Deserialize<EffectsClass>(reader);
//                     return new Prerequisites { EffectsClass = objectValue };
//                 case JsonToken.StartArray:
//                     var arrayValue = serializer.Deserialize<string[]>(reader);
//                     return new Prerequisites { StringArray = arrayValue };
//             }
//             throw new Exception("Cannot unmarshal type Prerequisites");
//         }

//         public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//         {
//             var value = (Prerequisites)untypedValue;
//             if (value.StringArray != null)
//             {
//                 serializer.Serialize(writer, value.StringArray);
//                 return;
//             }
//             if (value.EffectsClass != null)
//             {
//                 serializer.Serialize(writer, value.EffectsClass);
//                 return;
//             }
//             throw new Exception("Cannot marshal type Prerequisites");
//         }

//         public static readonly PrerequisitesConverter Singleton = new PrerequisitesConverter();
//     }

//     internal class ResearchUnitIngredientsConverter : JsonConverter
//     {
//         public override bool CanConvert(Type t) => t == typeof(ResearchUnitIngredients) || t == typeof(ResearchUnitIngredients?);

//         public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//         {
//             switch (reader.TokenType)
//             {
//                 case JsonToken.StartObject:
//                     var objectValue = serializer.Deserialize<EffectsClass>(reader);
//                     return new ResearchUnitIngredients { EffectsClass = objectValue };
//                 case JsonToken.StartArray:
//                     var arrayValue = serializer.Deserialize<ResearchUnitIngredient[]>(reader);
//                     return new ResearchUnitIngredients { ResearchUnitIngredientArray = arrayValue };
//             }
//             throw new Exception("Cannot unmarshal type ResearchUnitIngredients");
//         }

//         public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//         {
//             var value = (ResearchUnitIngredients)untypedValue;
//             if (value.ResearchUnitIngredientArray != null)
//             {
//                 serializer.Serialize(writer, value.ResearchUnitIngredientArray);
//                 return;
//             }
//             if (value.EffectsClass != null)
//             {
//                 serializer.Serialize(writer, value.EffectsClass);
//                 return;
//             }
//             throw new Exception("Cannot marshal type ResearchUnitIngredients");
//         }

//         public static readonly ResearchUnitIngredientsConverter Singleton = new ResearchUnitIngredientsConverter();
//     }

//     internal class NameConverter : JsonConverter
//     {
//         public override bool CanConvert(Type t) => t == typeof(Name) || t == typeof(Name?);

//         public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//         {
//             if (reader.TokenType == JsonToken.Null) return null;
//             var value = serializer.Deserialize<string>(reader);
//             switch (value)
//             {
//                 case "advanced-logistic-science-pack":
//                     return Name.AdvancedLogisticSciencePack;
//                 case "alien-artifact-blue-tool":
//                     return Name.AlienArtifactBlueTool;
//                 case "alien-artifact-green-tool":
//                     return Name.AlienArtifactGreenTool;
//                 case "alien-artifact-orange-tool":
//                     return Name.AlienArtifactOrangeTool;
//                 case "alien-artifact-purple-tool":
//                     return Name.AlienArtifactPurpleTool;
//                 case "alien-artifact-red-tool":
//                     return Name.AlienArtifactRedTool;
//                 case "alien-artifact-tool":
//                     return Name.AlienArtifactTool;
//                 case "alien-artifact-yellow-tool":
//                     return Name.AlienArtifactYellowTool;
//                 case "alien-science-pack":
//                     return Name.AlienSciencePack;
//                 case "alien-science-pack-blue":
//                     return Name.AlienSciencePackBlue;
//                 case "alien-science-pack-green":
//                     return Name.AlienSciencePackGreen;
//                 case "alien-science-pack-orange":
//                     return Name.AlienSciencePackOrange;
//                 case "alien-science-pack-purple":
//                     return Name.AlienSciencePackPurple;
//                 case "alien-science-pack-red":
//                     return Name.AlienSciencePackRed;
//                 case "alien-science-pack-yellow":
//                     return Name.AlienSciencePackYellow;
//                 case "automation-science-pack":
//                     return Name.AutomationSciencePack;
//                 case "chemical-science-pack":
//                     return Name.ChemicalSciencePack;
//                 case "logistic-science-pack":
//                     return Name.LogisticSciencePack;
//                 case "military-science-pack":
//                     return Name.MilitarySciencePack;
//                 case "production-science-pack":
//                     return Name.ProductionSciencePack;
//                 case "sb-angelsore3-tool":
//                     return Name.SbAngelsore3Tool;
//                 case "sb-basic-circuit-board-tool":
//                     return Name.SbBasicCircuitBoardTool;
//                 case "sb-lab-tool":
//                     return Name.SbLabTool;
//                 case "science-pack-gold":
//                     return Name.SciencePackGold;
//                 case "sct-bio-science-pack":
//                     return Name.SctBioSciencePack;
//                 case "space-science-pack":
//                     return Name.SpaceSciencePack;
//                 case "utility-science-pack":
//                     return Name.UtilitySciencePack;
//             }
//             throw new Exception("Cannot unmarshal type Name");
//         }

//         public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//         {
//             if (untypedValue == null)
//             {
//                 serializer.Serialize(writer, null);
//                 return;
//             }
//             var value = (Name)untypedValue;
//             switch (value)
//             {
//                 case Name.AdvancedLogisticSciencePack:
//                     serializer.Serialize(writer, "advanced-logistic-science-pack");
//                     return;
//                 case Name.AlienArtifactBlueTool:
//                     serializer.Serialize(writer, "alien-artifact-blue-tool");
//                     return;
//                 case Name.AlienArtifactGreenTool:
//                     serializer.Serialize(writer, "alien-artifact-green-tool");
//                     return;
//                 case Name.AlienArtifactOrangeTool:
//                     serializer.Serialize(writer, "alien-artifact-orange-tool");
//                     return;
//                 case Name.AlienArtifactPurpleTool:
//                     serializer.Serialize(writer, "alien-artifact-purple-tool");
//                     return;
//                 case Name.AlienArtifactRedTool:
//                     serializer.Serialize(writer, "alien-artifact-red-tool");
//                     return;
//                 case Name.AlienArtifactTool:
//                     serializer.Serialize(writer, "alien-artifact-tool");
//                     return;
//                 case Name.AlienArtifactYellowTool:
//                     serializer.Serialize(writer, "alien-artifact-yellow-tool");
//                     return;
//                 case Name.AlienSciencePack:
//                     serializer.Serialize(writer, "alien-science-pack");
//                     return;
//                 case Name.AlienSciencePackBlue:
//                     serializer.Serialize(writer, "alien-science-pack-blue");
//                     return;
//                 case Name.AlienSciencePackGreen:
//                     serializer.Serialize(writer, "alien-science-pack-green");
//                     return;
//                 case Name.AlienSciencePackOrange:
//                     serializer.Serialize(writer, "alien-science-pack-orange");
//                     return;
//                 case Name.AlienSciencePackPurple:
//                     serializer.Serialize(writer, "alien-science-pack-purple");
//                     return;
//                 case Name.AlienSciencePackRed:
//                     serializer.Serialize(writer, "alien-science-pack-red");
//                     return;
//                 case Name.AlienSciencePackYellow:
//                     serializer.Serialize(writer, "alien-science-pack-yellow");
//                     return;
//                 case Name.AutomationSciencePack:
//                     serializer.Serialize(writer, "automation-science-pack");
//                     return;
//                 case Name.ChemicalSciencePack:
//                     serializer.Serialize(writer, "chemical-science-pack");
//                     return;
//                 case Name.LogisticSciencePack:
//                     serializer.Serialize(writer, "logistic-science-pack");
//                     return;
//                 case Name.MilitarySciencePack:
//                     serializer.Serialize(writer, "military-science-pack");
//                     return;
//                 case Name.ProductionSciencePack:
//                     serializer.Serialize(writer, "production-science-pack");
//                     return;
//                 case Name.SbAngelsore3Tool:
//                     serializer.Serialize(writer, "sb-angelsore3-tool");
//                     return;
//                 case Name.SbBasicCircuitBoardTool:
//                     serializer.Serialize(writer, "sb-basic-circuit-board-tool");
//                     return;
//                 case Name.SbLabTool:
//                     serializer.Serialize(writer, "sb-lab-tool");
//                     return;
//                 case Name.SciencePackGold:
//                     serializer.Serialize(writer, "science-pack-gold");
//                     return;
//                 case Name.SctBioSciencePack:
//                     serializer.Serialize(writer, "sct-bio-science-pack");
//                     return;
//                 case Name.SpaceSciencePack:
//                     serializer.Serialize(writer, "space-science-pack");
//                     return;
//                 case Name.UtilitySciencePack:
//                     serializer.Serialize(writer, "utility-science-pack");
//                     return;
//             }
//             throw new Exception("Cannot marshal type Name");
//         }

//         public static readonly NameConverter Singleton = new NameConverter();
//     }

//     internal class ResearchUnitIngredientTypeConverter : JsonConverter
//     {
//         public override bool CanConvert(Type t) => t == typeof(ResearchUnitIngredientType) || t == typeof(ResearchUnitIngredientType?);

//         public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//         {
//             if (reader.TokenType == JsonToken.Null) return null;
//             var value = serializer.Deserialize<string>(reader);
//             if (value == "item")
//             {
//                 return ResearchUnitIngredientType.Item;
//             }
//             throw new Exception("Cannot unmarshal type ResearchUnitIngredientType");
//         }

//         public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//         {
//             if (untypedValue == null)
//             {
//                 serializer.Serialize(writer, null);
//                 return;
//             }
//             var value = (ResearchUnitIngredientType)untypedValue;
//             if (value == ResearchUnitIngredientType.Item)
//             {
//                 serializer.Serialize(writer, "item");
//                 return;
//             }
//             throw new Exception("Cannot marshal type ResearchUnitIngredientType");
//         }

//         public static readonly ResearchUnitIngredientTypeConverter Singleton = new ResearchUnitIngredientTypeConverter();
//     }
// }
