﻿/*
 * TODO: 
 * * Change magic numbers to constants wherever it's not a pain in the ass
 * * Error handling, error handling, error handling. Wrap file writes in try/catch, handle malformed pokemon, incomplete writes, patterns not found, etc.
 * * Bug - shiny pid calculation hangs on < Gen6 pokemon. The bug is in PKHeX, but we might manage to do it ourselves.
 */
using ntrbase.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;

namespace ntrbase
{
    public partial class MainForm : Form
    {
        //A "waiting room", where functions wait for data to be acquired.
        //Entries are indexed by their sequence number. Once a request with a given sequence number
        //is fulfilled, handleDataReady() uses information in DataReadyWaiting object to process the data.
        static Dictionary<uint, DataReadyWaiting> waitingForData = new Dictionary<uint, DataReadyWaiting>();

        public enum GameType {None, X, Y, OR, AS};
        public const int BOXES = 31;
        public const int BOXSIZE = 30;
        public const int POKEBYTES = 232;
        public const string FOLDERPOKE = "Pokemon";
        public const string FOLDERDELETE = "Deleted";
        PKHeX dumpedPKHeX = new PKHeX();

        UpdateDetails foundUpdate = null;

        public byte[] selectedCloneData  = new byte[232];
        public bool   selectedCloneValid = false;
        
        //public int tradedumpcount = 0;

        //Game information
        public int pid;
        public int hid_pid = Convert.ToInt32("0x10", 16);
        public byte lang;
        public string pname;
        public GameType game = GameType.None;
        //Offsets for basic data
        public uint nameoff;
        public uint tidoff;
        public uint sidoff;
        public uint hroff;
        public uint langoff;
        public uint moneyoff;
        public uint milesoff;
        public uint bpoff;
        //Offsets for items data
        public uint itemsoff;
        public uint medsoff;
        public uint keysoff;
        public uint tmsoff;
        public uint bersoff;
        //Offsets for Pokemon sources
        public uint tradeoffrg;
        public uint partyOff;
        public uint boxOff;
        public uint daycare1Off;
        public uint daycare2Off;
        public uint battleBoxOff;
        // Offsets for buttons
        public uint buttonAOff = 0x10000028;
        //TODO: add opponent data offset (right now it's a constant)
        
        private byte[] itemData = new byte[1600];
        private byte[] keyData = new byte[384];
        private byte[] tmData = new byte[432];
        private byte[] medData = new byte[256];
        private byte[] berryData = new byte[288];
        public byte[] items;

        public int numofItems;
        public int numofKeys;
        public int numofTMs;
        public int numofMeds;
        public int numofBers;
        
        public uint itemsfinal;
        public uint amountfinal;
        public uint keysfinal;
        public uint keysamountfinal;
        public uint tmsfinal;
        public uint tmsamountfinal;
        public uint medsfinal;
        public uint medsamountfinal;
        public uint bersfinal;
        public uint bersamountfinal;

        //Data for an empty slot
        public static byte[] emptyData = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x83, 0x07, 0x00, 0x00, 0x7E, 0xE9, 0x71, 0x52, 0xB0, 0x31, 0x42, 0x8E, 0xCC, 0xE2, 0xC5, 0xAF, 0xDB, 0x67, 0x33, 0xFC, 0x2C, 0xEF, 0x5E, 0xFC, 0xC5, 0xCA, 0xD6, 0xEB, 0x3D, 0x99, 0xBC, 0x7A, 0xA7, 0xCB, 0xD6, 0x5D, 0x78, 0x91, 0xA6, 0x27, 0x8D, 0x61, 0x92, 0x16, 0xB8, 0xCF, 0x5D, 0x37, 0x80, 0x30, 0x7C, 0x40, 0xFB, 0x48, 0x13, 0x32, 0xE7, 0xFE, 0xE6, 0xDF, 0x0E, 0x3D, 0xF9, 0x63, 0x29, 0x1D, 0x8D, 0xEA, 0x96, 0x62, 0x68, 0x92, 0x97, 0xA3, 0x49, 0x1C, 0x03, 0x6E, 0xAA, 0x31, 0x89, 0xAA, 0xC5, 0xD3, 0xEA, 0xC3, 0xD9, 0x82, 0xC6, 0xE0, 0x5C, 0x94, 0x3B, 0x4E, 0x5F, 0x5A, 0x28, 0x24, 0xB3, 0xFB, 0xE1, 0xBF, 0x8E, 0x7B, 0x7F, 0x00, 0xC4, 0x40, 0x48, 0xC8, 0xD1, 0xBF, 0xB6, 0x38, 0x3B, 0x90, 0x23, 0xFB, 0x23, 0x7D, 0x34, 0xBE, 0x00, 0xDA, 0x6A, 0x70, 0xC5, 0xDF, 0x84, 0xBA, 0x14, 0xE4, 0xA1, 0x60, 0x2B, 0x2B, 0x38, 0x8F, 0xA0, 0xB6, 0x60, 0x41, 0x36, 0x16, 0x09, 0xF0, 0x4B, 0xB5, 0x0E, 0x26, 0xA8, 0xB6, 0x43, 0x7B, 0xCB, 0xF9, 0xEF, 0x68, 0xD4, 0xAF, 0x5F, 0x74, 0xBE, 0xC3, 0x61, 0xE0, 0x95, 0x98, 0xF1, 0x84, 0xBA, 0x11, 0x62, 0x24, 0x80, 0xCC, 0xC4, 0xA7, 0xA2, 0xB7, 0x55, 0xA8, 0x5C, 0x1C, 0x42, 0xA2, 0x3A, 0x86, 0x05, 0xAD, 0xD2, 0x11, 0x19, 0xB0, 0xFD, 0x57, 0xE9, 0x4E, 0x60, 0xBA, 0x1B, 0x45, 0x2E, 0x17, 0xA9, 0x34, 0x93, 0x2D, 0x66, 0x09, 0x2D, 0x11, 0xE0, 0xA1, 0x74, 0x42, 0xC4, 0x73, 0x65, 0x2F, 0x21, 0xF0, 0x43, 0x28, 0x54, 0xA6 };
        
        //Lookup tables
        public static readonly string[] itemList = { "[None]", "Master Ball", "Ultra Ball", "Great Ball", "Poke Ball", "Safari Ball", "Net Ball", "Dive Ball", "Nest Ball", "Repeat Ball", "Timer Ball", "Luxury Ball", "Premier Ball", "Dusk Ball", "Heal Ball", "Quick Ball", "Cherish Ball", "Potion", "Antidote", "Burn Heal", "Ice Heal", "Awakening", "Paralyze Heal", "Full Restore", "Max Potion", "Hyper Potion", "Super Potion", "Full Heal", "Revive", "Max Revive", "Fresh Water", "Soda Pop", "Lemonade", "Moomoo Milk", "Energy Powder", "Energy Root", "Heal Powder", "Revival Herb", "Ether", "Max Ether", "Elixir", "Max Elixir", "Lava Cookie", "Berry Juice", "Sacred Ash", "HP Up", "Protein", "Iron", "Carbos", "Calcium", "Rare Candy", "PP Up", "Zinc", "PP Max", "Old Gateau", "Guard Spec.", "Dire Hit", "X Attack", "X Defense", "X Speed", "X Accuracy", "X Sp. Atk", "X Sp. Def", "Poke Doll", "Fluffy Tail", "Blue Flute", "Yellow Flute", "Red Flute", "Black Flute", "White Flute", "Shoal Salt", "Shoal Shell", "Red Shard", "Blue Shard", "Yellow Shard", "Green Shard", "Super Repel", "Max Repel", "Escape Rope", "Repel", "Sun Stone", "Moon Stone", "Fire Stone", "Thunder Stone", "Water Stone", "Leaf Stone", "Tiny Mushroom", "Big Mushroom", "Pearl", "Big Pearl", "Stardust", "Star Piece", "Nugget", "Heart Scale", "Honey", "Growth Mulch", "Damp Mulch", "Stable Mulch", "Gooey Mulch", "Root Fossil", "Claw Fossil", "Helix Fossil", "Dome Fossil", "Old Amber", "Armor Fossil", "Skull Fossil", "Rare Bone", "Shiny Stone", "Dusk Stone", "Dawn Stone", "Oval Stone", "Odd Keystone", "Griseous Orb", "???", "???", "???", "Douse Drive", "Shock Drive", "Burn Drive", "Chill Drive", "???", "???", "???", "???", "???", "???", "???", "???", "???", "???", "???", "???", "???", "???", "Sweet Heart", "Adamant Orb", "Lustrous Orb", "Greet Mail", "Favored Mail", "RSVP Mail", "Thanks Mail", "Inquiry Mail", "Like Mail", "Reply Mail", "Bridge Mail S", "Bridge Mail D", "Bridge Mail T", "Bridge Mail V", "Bridge Mail M", "Cheri Berry", "Chesto Berry", "Pecha Berry", "Rawst Berry", "Aspear Berry", "Leppa Berry", "Oran Berry", "Persim Berry", "Lum Berry", "Sitrus Berry", "Figy Berry", "Wiki Berry", "Mago Berry", "Aguav Berry", "Iapapa Berry", "Razz Berry", "Bluk Berry", "Nanab Berry", "Wepear Berry", "Pinap Berry", "Pomeg Berry", "Kelpsy Berry", "Qualot Berry", "Hondew Berry", "Grepa Berry", "Tamato Berry", "Cornn Berry", "Magost Berry", "Rabuta Berry", "Nomel Berry", "Spelon Berry", "Pamtre Berry", "Watmel Berry", "Durin Berry", "Belue Berry", "Occa Berry", "Passho Berry", "Wacan Berry", "Rindo Berry", "Yache Berry", "Chople Berry", "Kebai Berry", "Shuca Berry", "Coba Berry", "Payapa Berry", "Tanga Berry", "Charti Berry", "Kasib Berry", "Haban Berry", "Colbur Berry", "Babiri Berry", "Chilan Berry", "Liechi Berry", "Ganlon Berry", "Salac Berry", "Petaya Berry", "Apicot Berry", "Lansat Berry", "Starf Berry", "Enigma Berry", "Micle Berry", "Custap Berry", "Jaboca Berry", "Rowap Berry", "Bright Powder", "White Herb", "Macho Brace", "Exp. Share", "Quick Claw", "Soothe Bell", "Mental Herb", "Choice Band", "King's Rock", "Silver Powder", "Amulet Coin", "Cleanse Tag", "Soul Dew", "Deep Sea Tooth", "Deep Sea Scale", "Smoke Ball", "Everstone", "Focus Band", "Lucky Egg", "Scope Lens", "Metal Coat", "Leftovers", "Dragon Scale", "Light Ball", "Soft Sand", "Hard Stone", "Miracle Seed", "Black Glasses", "Black Belt", "Magnet", "Mystic Water", "Sharp Beak", "Poison Barb", "Never-Melt Ice", "Spell Tag", "Twisted Spoon", "Charcoal", "Dragon Fang", "Silk Scarf", "Up-Grade", "Shell Bell", "Sea Incense", "Lax Incense", "Lucky Punch", "Metal Powder", "Thick Club", "Stick", "Red Scarf", "Blue Scarf", "Pink Scarf", "Green Scarf", "Yellow Scarf", "Wide Lens", "Muscle Band", "Wise Glasses", "Expert Belt", "Light Clay", "Life Orb", "Power Herb", "Toxic Orb", "Flame Orb", "Quick Powder", "Focus Sash", "Zoom Lens", "Metronome", "Iron Ball", "Lagging Tail", "Destiny Knot", "Black Sludge", "Icy Rock", "Smooth Rock", "Heat Rock", "Damp Rock", "Grip Claw", "Choice Scarf", "Sticky Barb", "Power Bracer", "Power Belt", "Power Lens", "Power Band", "Power Anklet", "Power Weight", "Shed Shell", "Big Root", "Choice Specs", "Flame Plate", "Splash Plate", "Zap Plate", "Meadow Plate", "Icicle Plate", "Fist Plate", "Toxic Plate", "Earth Plate", "Sky Plate", "Mind Plate", "Insect Plate", "Stone Plate", "Spooky Plate", "Draco Plate", "Dread Plate", "Iron Plate", "Odd Incense", "Rock Incense", "Full Incense", "Wave Incense", "Rose Incense", "Luck Incense", "Pure Incense", "Protector", "Electrizer", "Magmarizer", "Dubious Disc", "Reaper Cloth", "Razor Claw", "Razor Fang", "Hone Claws", "Dragon Claw", "Psyshock", "Calm Mind", "Roar", "Toxic", "Hail", "Bulk Up", "Venoshock", "Hidden Power", "Sunny Day", "Taunt", "Ice Beam", "Blizzard", "Hyper Beam", "Light Screen", "Protect", "Rain Dance", "Roost", "Safeguard", "Frustration", "Solar Beam", "Smack Down", "Thunderbolt", "Thunder", "Earthquake", "Return", "Dig", "Psychic", "Shadow Ball", "Brick Break", "Double Team", "Reflect", "Sludge Wave", "Flamethrower", "Sludge Bomb", "Sandstorm", "Fire Blast", "Rock Tomb", "Aerial Ace", "Torment", "Facade", "Flame Charge", "Rest", "Attract", "Thief", "Low Sweep", "Round", "Echoed Voice", "Overheat", "Steel Wing", "Focus Blast", "Energy Ball", "False Swipe", "Scald", "Fling", "Charge Beam", "Sky Drop", "Incinerate", "Quash", "Will-O-Wisp", "Acrobatics", "Embargo", "Explosion", "Shadow Claw", "Payback", "Retaliate", "Giga Impact", "Rock Polish", "Flash", "Stone Edge", "Volt Switch", "Thunder Wave", "Gyro Ball", "Swords Dance", "Struggle Bug", "Psych Up", "Bulldoze", "Frost Breath", "Rock Slide", "X-Scissor", "Dragon Tail", "Infestation", "Poison Jab", "Dream Eater", "Grass Knot", "Swagger", "Sleep Talk", "U-turn", "Substitute", "Flash Cannon", "Trick Room", "Cut", "Fly", "Surf", "Strength", "Waterfall", "Rock Smash", "???", "???", "Explorer Kit", "Loot Sack", "Rule Book", "Poke Radar", "Point Card", "Journal", "Seal Case", "Fashion Case", "Seal Bag", "Pal Pad", "Works key", "Old Charm", "Galactic Key", "Red Chain", "Town Map", "Vs. Seeker", "Coin Case", "Old Rod", "Good Rod", "Super Rod", "Sprayduck", "Poffin Case", "Bike", "Suite Key", "Oak's Letter", "Lunar Wing", "Member Card", "Azure Flute", "S.S. Ticket", "Contest Pass", "Magma Stone", "Parcel", "Coupon 1", "Coupon 2", "Coupon 3", "Storage Key", "Secret Potion", "Vs. Recorder", "Gracidea", "Secret Key", "Apricorn Box", "Unown Report", "Berry Pots", "Dowsing Machine", "Blue Card", "Slowpoke Tail", "Clear Bell", "Card Key", "Basement Key", "Squirt Bottle", "Red Scale", "Lost Item", "Pass", "Machine Part", "Silver Wing", "Rainbow Wing", "Mystery Egg", "Red Apricorn", "Blue Apricorn", "Yellow Apricorn", "Green Apricorn", "Pink Apricorn", "White Apricorn", "Black Apricorn", "Fast Ball", "Level Ball", "Lure Ball", "Heavy Ball", "Love Ball", "Friend Ball", "Moon Ball", "Sport Ball", "Park Ball", "Photo Album", "GB Sounds", "Tidal Bell", "Rage Candy Bar", "Data Card 01", "Data Card 02", "Data Card 03", "Data Card 04", "Data Card 05", "Data Card 06", "Data Card 07", "Data Card 08", "Data Card 09", "Data Card 10", "Data Card 11", "Data Card 12", "Data Card 13", "Data Card 14", "Data Card 15", "Data Card 16", "Data Card 17", "Data Card 18", "Data Card 19", "Data Card 20", "Data Card 21", "Data Card 22", "Data Card 23", "Data Card 24", "Data Card 25", "Data Card 26", "Data Card 27", "Jade Orb", "Lock Capsule", "Red Orb", "Blue Orb", "Enigma Stone", "Prism Scale", "Eviolite", "Float Stone", "Rocky Helmet", "Air Balloon", "Red Card", "Ring Target", "Binding Band", "Absorb Bulb", "Cell Battery", "Eject Button", "Fire Gem", "Water Gem", "Electric Gem", "Grass Gem", "Ice Gem", "Fighting Gem", "Poison Gem", "Ground Gem", "Flying Gem", "Psychic Gem", "Bug Gem", "Rock Gem", "Ghost Gem", "Dragon Gem", "Dark Gem", "Steel Gem", "Normal Gem", "Health Wing", "Muscle Wing", "Resist Wing", "Genius Wing", "Clever Wing", "Swift Wing", "Pretty Wing", "Cover Fossil", "Plume Fossil", "Libery Pass", "Pass Orb", "Dream Ball", "Poke Toy", "Prop Case", "Dragon Skull", "Balm Mushroom", "Big Nugget", "Pearl String", "Comet Shard", "Relic Copper", "Relic Silver", "Relic Gold", "Relic Vase", "Relic Band", "Relic Statue", "Relic Crown", "Casteliacone", "Dire Hit 2", "X Speed 2", "X Sp. Atk 2", "X Sp. Def 2", "X Defense 2", "X Attack 2", "X Accuracy 2", "X Speed 3", "X Sp. Atk 3", "X Sp. Def 3", "X Defense 3", "X Attack 3", "X Accuracy 3", "X Speed 6", "X Sp. Atk 6", "X Sp. Def 6", "X Defense 6", "X Attack 6", "X Accuracy 6", "Ability Urge", "Item Drop", "Item Urge", "Reset Urge", "Dire Hit 3", "Light Stone", "Dark Stone", "Wild Charge", "Secret Power", "Snarl", "Xtransceiver(Male)", "???", "Gram 1", "Gram 2", "Gram 3", "Xtransceiver(Female)", "Medal Box", "DNA Splicers(Fuses)", "DNA Splicers(Seperates)", "Permit", "Oval Charm", "Shiny Charm", "Plasma Card", "Grubby Hanky", "Colress Machine", "Dropped Item (Xtransceiver Male)", "Dropped Item (Xtransceiver Female)", "Reveal Glass", "Weakness Policy", "Assault Vest", "Holo Caster", "Prof's Letter", "Roller Skates", "Pixie Plate", "Ability Capsule", "Whipped Dream", "Sachet", "Luminous Moss", "Snowball", "Safety Goggles", "Poke Flute", "Rich Mulch", "Surprise Mulch", "Boost Mulch", "Amaze Mulch", "Gengarite", "Gardevoirite", "Ampharosite", "Venusaurite", "Charizardite X", "Blastoisinite", "Mewtwonite X", "Mewtwonite Y", "Blazikenite", "Medichamite", "Houndoominite", "Aggronite", "Banettite", "Tyranitarite", "Scizorite", "Pinsirite", "Aerodactylite", "Lucarionite", "Abomasite", "Kangaskhanite", "Gyaradosite", "Absolite", "Charizardite Y", "Alakazite", "Heracronite", "Mawilite", "Manectite", "Garchompite", "Latiasite", "Latiosite", "Roseli Berry", "Kee Berry", "Maranga Berry", "Sprinklotad", "Nature Power", "Dark Pulse", "Power-Up Punch", "Dazzling Gleam", "Confide", "Power Plant Pass", "Mega Ring", "Intruiging Stone", "Common Stone", "Discount Coupon", "Elevator Key", "TMV Pass", "Honor of Kalos", "Adventure Rules", "Strange Souvenir", "Lens Case", "Travel Trunk (Silver)", "Travel Trunk (Gold)", "Lumiose Galette", "Shalour Sable", "Jaw Fossil", "Sail Fossil", "Looker Ticket", "Bike", "Holo Caster", "Fairy Gem", "Mega Charm", "Mega Glove", "Mach Bike", "Acro Bike", "Wailmer Pail", "Devon Parts", "Soot Sack", "Basement Key", "Pokeblock Kit", "Letter", "Eon Ticket", "Scanner", "Go-Goggles", "Meteorite (originally found)", "Key to Room 1", "Key to Room 2", "Key to Room 4", "Key to Room 6", "Storage Key", "Devon Scope", "S.S. Ticket", "Dive", "Devon Scuba Gear", "Contest Costume (Male)", "Contest Costume (Female)", "Magma Suit", "Aqua Suit", "Pair of Tickets", "Mega Bracelet", "Mega Pendant", "Mega Glasses", "Mega Anchor", "Mega Stickpin", "Mega Tiara", "Mega Anklet", "Meteorite (faint glow)", "Swampertite", "Sceptilite", "Sablenite", "Altarianite", "Galladite", "Audinite", "Metagrossite", "Sharpedonite", "Slowbronite", "Steelixite", "Pidgeotite", "Glalitite", "Diancite", "Prison Bottle", "Mega Cuff", "Cameruptite", "Lopunnite", "Salamencite", "Beedrillite", "Meteorite (1)", "Meteorite (2)", "Key Stone", "Meteorite Shard", "Eon Flute" };
        public static readonly string[] abilityList = { "Stench", "Drizzle", "Speed Boost", "Battle Armor", "Sturdy", "Damp", "Limber", "Sand Veil", "Static", "Volt Absorb", "Water Absorb", "Oblivious", "Cloud Nine", "Compound Eyes", "Insomnia", "Color Change", "Immunity", "Flash Fire", "Shield Dust", "Own Tempo", "Suction Cups", "Intimidate", "Shadow Tag", "Rough Skin", "Wonder Guard", "Levitate", "Effect Spore", "Synchronize", "Clear Body", "Natural Cure", "Lightning Rod", "Serene Grace", "Swift Swim", "Chlorophyll", "Illuminate", "Trace", "Huge Power", "Poison Point", "Inner Focus", "Magma Armor", "Water Veil", "Magnet Pull", "Soundproof", "Rain Dish", "Sand Stream", "Pressure", "Thick Fat", "Early Bird", "Flame Body", "Run Away", "Keen Eye", "Hyper Cutter", "Pickup", "Truant", "Hustle", "Cute Charm", "Plus", "Minus", "Forecast", "Sticky Hold", "Shed Skin", "Guts", "Marvel Scale", "Liquid Ooze", "Overgrow", "Blaze", "Torrent", "Swarm", "Rock Head", "Drought", "Arena Trap", "Vital Spirit", "White Smoke", "Pure Power", "Shell Armor", "Air Lock", "Tangled Feet", "Motor Drive", "Rivalry", "Steadfast", "Snow Cloak", "Gluttony", "Anger Point", "Unburden", "Heatproof", "Simple", "Dry Skin", "Download", "Iron Fist", "Poison Heal", "Adaptability", "Skill Link", "Hydration", "Solar Power", "Quick Feet", "Normalize", "Sniper", "Magic Guard", "No Guard", "Stall", "Technician", "Leaf Guard", "Klutz", "Mold Breaker", "Super Luck", "Aftermath", "Anticipation", "Forewarn", "Unaware", "Tinted Lens", "Filter", "Slow Start", "Scrappy", "Storm Drain", "Ice Body", "Solid Rock", "Snow Warning", "Honey Gather", "Frisk", "Reckless", "Multitype", "Flower Gift", "Bad Dreams", "Pickpocket", "Sheer Force", "Contrary", "Unnerve", "Defiant", "Defeatist", "Cursed Body", "Healer", "Friend Guard", "Weak Armor", "Heavy Metal", "Light Metal", "Multiscale", "Toxic Boost", "Flare Boost", "Harvest", "Telepathy", "Moody", "Overcoat", "Poison Touch", "Regenerator", "Big Pecks", "Sand Rush", "Wonder Skin", "Analytic", "Illusion", "Imposter", "Infiltrator", "Mummy", "Moxie", "Justified", "Rattled", "Magic Bounce", "Sap Sipper", "Prankster", "Sand Force", "Iron Barbs", "Zen Mode", "Victory Star", "Turboblaze", "Teravolt", "Aroma Veil", "Flower Veil", "Cheek Pouch", "Protean", "Fur Coat", "Magician", "Bulletproof", "Competitive", "Strong Jaw", "Refrigerate", "Sweet Veil", "Stance Change", "Gale Wings", "Mega Launcher", "Grass Pelt", "Symbiosis", "Tough Claws", "Pixilate", "Gooey", "Aerilate", "Parental Bond", "Dark Aura", "Fairy Aura", "Aura Break", "Primordial Sea", "Desolate Land", "Delta Stream" };
        public static readonly string[] speciesList = { "Bulbasaur", "Ivysaur", "Venusaur", "Charmander", "Charmeleon", "Charizard", "Squirtle", "Wartortle", "Blastoise", "Caterpie", "Metapod", "Butterfree", "Weedle", "Kakuna", "Beedrill", "Pidgey", "Pidgeotto", "Pidgeot", "Rattata", "Raticate", "Spearow", "Fearow", "Ekans", "Arbok", "Pikachu", "Raichu", "Sandshrew", "Sandslash", "Nidoran♀", "Nidorina", "Nidoqueen", "Nidoran♂", "Nidorino", "Nidoking", "Clefairy", "Clefable", "Vulpix", "Ninetales", "Jigglypuff", "Wigglytuff", "Zubat", "Golbat", "Oddish", "Gloom", "Vileplume", "Paras", "Parasect", "Venonat", "Venomoth", "Diglett", "Dugtrio", "Meowth", "Persian", "Psyduck", "Golduck", "Mankey", "Primeape", "Growlithe", "Arcanine", "Poliwag", "Poliwhirl", "Poliwrath", "Abra", "Kadabra", "Alakazam", "Machop", "Machoke", "Machamp", "Bellsprout", "Weepinbell", "Victreebel", "Tentacool", "Tentacruel", "Geodude", "Graveler", "Golem", "Ponyta", "Rapidash", "Slowpoke", "Slowbro", "Magnemite", "Magneton", "Farfetch’d", "Doduo", "Dodrio", "Seel", "Dewgong", "Grimer", "Muk", "Shellder", "Cloyster", "Gastly", "Haunter", "Gengar", "Onix", "Drowzee", "Hypno", "Krabby", "Kingler", "Voltorb", "Electrode", "Exeggcute", "Exeggutor", "Cubone", "Marowak", "Hitmonlee", "Hitmonchan", "Lickitung", "Koffing", "Weezing", "Rhyhorn", "Rhydon", "Chansey", "Tangela", "Kangaskhan", "Horsea", "Seadra", "Goldeen", "Seaking", "Staryu", "Starmie", "Mr-Mime", "Scyther", "Jynx", "Electabuzz", "Magmar", "Pinsir", "Tauros", "Magikarp", "Gyarados", "Lapras", "Ditto", "Eevee", "Vaporeon", "Jolteon", "Flareon", "Porygon", "Omanyte", "Omastar", "Kabuto", "Kabutops", "Aerodactyl", "Snorlax", "Articuno", "Zapdos", "Moltres", "Dratini", "Dragonair", "Dragonite", "Mewtwo", "Mew", "Chikorita", "Bayleef", "Meganium", "Cyndaquil", "Quilava", "Typhlosion", "Totodile", "Croconaw", "Feraligatr", "Sentret", "Furret", "Hoothoot", "Noctowl", "Ledyba", "Ledian", "Spinarak", "Ariados", "Crobat", "Chinchou", "Lanturn", "Pichu", "Cleffa", "Igglybuff", "Togepi", "Togetic", "Natu", "Xatu", "Mareep", "Flaaffy", "Ampharos", "Bellossom", "Marill", "Azumarill", "Sudowoodo", "Politoed", "Hoppip", "Skiploom", "Jumpluff", "Aipom", "Sunkern", "Sunflora", "Yanma", "Wooper", "Quagsire", "Espeon", "Umbreon", "Murkrow", "Slowking", "Misdreavus", "Unown", "Wobbuffet", "Girafarig", "Pineco", "Forretress", "Dunsparce", "Gligar", "Steelix", "Snubbull", "Granbull", "Qwilfish", "Scizor", "Shuckle", "Heracross", "Sneasel", "Teddiursa", "Ursaring", "Slugma", "Magcargo", "Swinub", "Piloswine", "Corsola", "Remoraid", "Octillery", "Delibird", "Mantine", "Skarmory", "Houndour", "Houndoom", "Kingdra", "Phanpy", "Donphan", "Porygon2", "Stantler", "Smeargle", "Tyrogue", "Hitmontop", "Smoochum", "Elekid", "Magby", "Miltank", "Blissey", "Raikou", "Entei", "Suicune", "Larvitar", "Pupitar", "Tyranitar", "Lugia", "Ho-Oh", "Celebi", "Treecko", "Grovyle", "Sceptile", "Torchic", "Combusken", "Blaziken", "Mudkip", "Marshtomp", "Swampert", "Poochyena", "Mightyena", "Zigzagoon", "Linoone", "Wurmple", "Silcoon", "Beautifly", "Cascoon", "Dustox", "Lotad", "Lombre", "Ludicolo", "Seedot", "Nuzleaf", "Shiftry", "Taillow", "Swellow", "Wingull", "Pelipper", "Ralts", "Kirlia", "Gardevoir", "Surskit", "Masquerain", "Shroomish", "Breloom", "Slakoth", "Vigoroth", "Slaking", "Nincada", "Ninjask", "Shedinja", "Whismur", "Loudred", "Exploud", "Makuhita", "Hariyama", "Azurill", "Nosepass", "Skitty", "Delcatty", "Sableye", "Mawile", "Aron", "Lairon", "Aggron", "Meditite", "Medicham", "Electrike", "Manectric", "Plusle", "Minun", "Volbeat", "Illumise", "Roselia", "Gulpin", "Swalot", "Carvanha", "Sharpedo", "Wailmer", "Wailord", "Numel", "Camerupt", "Torkoal", "Spoink", "Grumpig", "Spinda", "Trapinch", "Vibrava", "Flygon", "Cacnea", "Cacturne", "Swablu", "Altaria", "Zangoose", "Seviper", "Lunatone", "Solrock", "Barboach", "Whiscash", "Corphish", "Crawdaunt", "Baltoy", "Claydol", "Lileep", "Cradily", "Anorith", "Armaldo", "Feebas", "Milotic", "Castform", "Kecleon", "Shuppet", "Banette", "Duskull", "Dusclops", "Tropius", "Chimecho", "Absol", "Wynaut", "Snorunt", "Glalie", "Spheal", "Sealeo", "Walrein", "Clamperl", "Huntail", "Gorebyss", "Relicanth", "Luvdisc", "Bagon", "Shelgon", "Salamence", "Beldum", "Metang", "Metagross", "Regirock", "Regice", "Registeel", "Latias", "Latios", "Kyogre", "Groudon", "Rayquaza", "Jirachi", "Deoxys", "Turtwig", "Grotle", "Torterra", "Chimchar", "Monferno", "Infernape", "Piplup", "Prinplup", "Empoleon", "Starly", "Staravia", "Staraptor", "Bidoof", "Bibarel", "Kricketot", "Kricketune", "Shinx", "Luxio", "Luxray", "Budew", "Roserade", "Cranidos", "Rampardos", "Shieldon", "Bastiodon", "Burmy", "Wormadam", "Mothim", "Combee", "Vespiquen", "Pachirisu", "Buizel", "Floatzel", "Cherubi", "Cherrim", "Shellos", "Gastrodon", "Ambipom", "Drifloon", "Drifblim", "Buneary", "Lopunny", "Mismagius", "Honchkrow", "Glameow", "Purugly", "Chingling", "Stunky", "Skuntank", "Bronzor", "Bronzong", "Bonsly", "Mime-Jr.", "Happiny", "Chatot", "Spiritomb", "Gible", "Gabite", "Garchomp", "Munchlax", "Riolu", "Lucario", "Hippopotas", "Hippowdon", "Skorupi", "Drapion", "Croagunk", "Toxicroak", "Carnivine", "Finneon", "Lumineon", "Mantyke", "Snover", "Abomasnow", "Weavile", "Magnezone", "Lickilicky", "Rhyperior", "Tangrowth", "Electivire", "Magmortar", "Togekiss", "Yanmega", "Leafeon", "Glaceon", "Gliscor", "Mamoswine", "Porygon-Z", "Gallade", "Probopass", "Dusknoir", "Froslass", "Rotom", "Uxie", "Mesprit", "Azelf", "Dialga", "Palkia", "Heatran", "Regigigas", "Giratina", "Cresselia", "Phione", "Manaphy", "Darkrai", "Shaymin", "Arceus", "Victini", "Snivy", "Servine", "Serperior", "Tepig", "Pignite", "Emboar", "Oshawott", "Dewott", "Samurott", "Patrat", "Watchog", "Lillipup", "Herdier", "Stoutland", "Purrloin", "Liepard", "Pansage", "Simisage", "Pansear", "Simisear", "Panpour", "Simipour", "Munna", "Musharna", "Pidove", "Tranquill", "Unfezant", "Blitzle", "Zebstrika", "Roggenrola", "Boldore", "Gigalith", "Woobat", "Swoobat", "Drilbur", "Excadrill", "Audino", "Timburr", "Gurdurr", "Conkeldurr", "Tympole", "Palpitoad", "Seismitoad", "Throh", "Sawk", "Sewaddle", "Swadloon", "Leavanny", "Venipede", "Whirlipede", "Scolipede", "Cottonee", "Whimsicott", "Petilil", "Lilligant", "Basculin", "Sandile", "Krokorok", "Krookodile", "Darumaka", "Darmanitan", "Maractus", "Dwebble", "Crustle", "Scraggy", "Scrafty", "Sigilyph", "Yamask", "Cofagrigus", "Tirtouga", "Carracosta", "Archen", "Archeops", "Trubbish", "Garbodor", "Zorua", "Zoroark", "Minccino", "Cinccino", "Gothita", "Gothorita", "Gothitelle", "Solosis", "Duosion", "Reuniclus", "Ducklett", "Swanna", "Vanillite", "Vanillish", "Vanilluxe", "Deerling", "Sawsbuck", "Emolga", "Karrablast", "Escavalier", "Foongus", "Amoonguss", "Frillish", "Jellicent", "Alomomola", "Joltik", "Galvantula", "Ferroseed", "Ferrothorn", "Klink", "Klang", "Klinklang", "Tynamo", "Eelektrik", "Eelektross", "Elgyem", "Beheeyem", "Litwick", "Lampent", "Chandelure", "Axew", "Fraxure", "Haxorus", "Cubchoo", "Beartic", "Cryogonal", "Shelmet", "Accelgor", "Stunfisk", "Mienfoo", "Mienshao", "Druddigon", "Golett", "Golurk", "Pawniard", "Bisharp", "Bouffalant", "Rufflet", "Braviary", "Vullaby", "Mandibuzz", "Heatmor", "Durant", "Deino", "Zweilous", "Hydreigon", "Larvesta", "Volcarona", "Cobalion", "Terrakion", "Virizion", "Tornadus", "Thundurus", "Reshiram", "Zekrom", "Landorus", "Kyurem", "Keldeo", "Meloetta", "Genesect", "Chespin", "Quilladin", "Chesnaught", "Fennekin", "Braixen", "Delphox", "Froakie", "Frogadier", "Greninja", "Bunnelby", "Diggersby", "Fletchling", "Fletchinder", "Talonflame", "Scatterbug", "Spewpa", "Vivillon", "Litleo", "Pyroar", "Flabebe", "Floette", "Florges", "Skiddo", "Gogoat", "Pancham", "Pangoro", "Furfrou", "Espurr", "Meowstic", "Honedge", "Doublade", "Aegislash", "Spritzee", "Aromatisse", "Swirlix", "Slurpuff", "Inkay", "Malamar", "Binacle", "Barbaracle", "Skrelp", "Dragalge", "Clauncher", "Clawitzer", "Helioptile", "Heliolisk", "Tyrunt", "Tyrantrum", "Amaura", "Aurorus", "Sylveon", "Hawlucha", "Dedenne", "Carbink", "Goomy", "Sliggoo", "Goodra", "Klefki", "Phantump", "Trevenant", "Pumpkaboo", "Gourgeist", "Bergmite", "Avalugg", "Noibat", "Noivern", "Xerneas", "Yveltal", "Zygarde", "Diancie", "Hoopa", "Volcanion", "Egg" };
        public static readonly string[] moveList = { "[None]", "Pound", "Karate Chop", "Double Slap", "Comet Punch", "Mega Punch", "Pay Day", "Fire Punch", "Ice Punch", "Thunder Punch", "Scratch", "Vice Grip", "Guillotine", "Razor Wind", "Swords Dance", "Cut", "Gust", "Wing Attack", "Whirlwind", "Fly", "Bind", "Slam", "Vine Whip", "Stomp", "Double Kick", "Mega Kick", "Jump Kick", "Rolling Kick", "Sand Attack", "Headbutt", "Horn Attack", "Fury Attack", "Horn Drill", "Tackle", "Body Slam", "Wrap", "Take Down", "Thrash", "Double-Edge", "Tail Whip", "Poison Sting", "Twineedle", "Pin Missile", "Leer", "Bite", "Growl", "Roar", "Sing", "Supersonic", "Sonic Boom", "Disable", "Acid", "Ember", "Flamethrower", "Mist", "Water Gun", "Hydro Pump", "Surf", "Ice Beam", "Blizzard", "Psybeam", "Bubble Beam", "Aurora Beam", "Hyper Beam", "Peck", "Drill Peck", "Submission", "Low Kick", "Counter", "Seismic Toss", "Strength", "Absorb", "Mega Drain", "Leech Seed", "Growth", "Razor Leaf", "Solar Beam", "Poison Powder", "Stun Spore", "Sleep Powder", "Petal Dance", "String Shot", "Dragon Rage", "Fire Spin", "Thunder Shock", "Thunderbolt", "Thunder Wave", "Thunder", "Rock Throw", "Earthquake", "Fissure", "Dig", "Toxic", "Confusion", "Psychic", "Hypnosis", "Meditate", "Agility", "Quick Attack", "Rage", "Teleport", "Night Shade", "Mimic", "Screech", "Double Team", "Recover", "Harden", "Minimize", "Smokescreen", "Confuse Ray", "Withdraw", "Defense Curl", "Barrier", "Light Screen", "Haze", "Reflect", "Focus Energy", "Bide", "Metronome", "Mirror Move", "Self-Destruct", "Egg Bomb", "Lick", "Smog", "Sludge", "Bone Club", "Fire Blast", "Waterfall", "Clamp", "Swift", "Skull Bash", "Spike Cannon", "Constrict", "Amnesia", "Kinesis", "Soft-Boiled", "High Jump Kick", "Glare", "Dream Eater", "Poison Gas", "Barrage", "Leech Life", "Lovely Kiss", "Sky Attack", "Transform", "Bubble", "Dizzy Punch", "Spore", "Flash", "Psywave", "Splash", "Acid Armor", "Crabhammer", "Explosion", "Fury Swipes", "Bonemerang", "Rest", "Rock Slide", "Hyper Fang", "Sharpen", "Conversion", "Tri Attack", "Super Fang", "Slash", "Substitute", "Struggle", "Sketch", "Triple Kick", "Thief", "Spider Web", "Mind Reader", "Nightmare", "Flame Wheel", "Snore", "Curse", "Flail", "Conversion 2", "Aeroblast", "Cotton Spore", "Reversal", "Spite", "Powder Snow", "Protect", "Mach Punch", "Scary Face", "Feint Attack", "Sweet Kiss", "Belly Drum", "Sludge Bomb", "Mud-Slap", "Octazooka", "Spikes", "Zap Cannon", "Foresight", "Destiny Bond", "Perish Song", "Icy Wind", "Detect", "Bone Rush", "Lock-On", "Outrage", "Sandstorm", "Giga Drain", "Endure", "Charm", "Rollout", "False Swipe", "Swagger", "Milk Drink", "Spark", "Fury Cutter", "Steel Wing", "Mean Look", "Attract", "Sleep Talk", "Heal Bell", "Return", "Present", "Frustration", "Safeguard", "Pain Split", "Sacred Fire", "Magnitude", "Dynamic Punch", "Megahorn", "Dragon Breath", "Baton Pass", "Encore", "Pursuit", "Rapid Spin", "Sweet Scent", "Iron Tail", "Metal Claw", "Vital Throw", "Morning Sun", "Synthesis", "Moonlight", "Hidden Power", "Cross Chop", "Twister", "Rain Dance", "Sunny Day", "Crunch", "Mirror Coat", "Psych Up", "Extreme Speed", "Ancient Power", "Shadow Ball", "Future Sight", "Rock Smash", "Whirlpool", "Beat Up", "Fake Out", "Uproar", "Stockpile", "Spit Up", "Swallow", "Heat Wave", "Hail", "Torment", "Flatter", "Will-O-Wisp", "Memento", "Facade", "Focus Punch", "Smelling Salts", "Follow Me", "Nature Power", "Charge", "Taunt", "Helping Hand", "Trick", "Role Play", "Wish", "Assist", "Ingrain", "Superpower", "Magic Coat", "Recycle", "Revenge", "Brick Break", "Yawn", "Knock Off", "Endeavor", "Eruption", "Skill Swap", "Imprison", "Refresh", "Grudge", "Snatch", "Secret Power", "Dive", "Arm Thrust", "Camouflage", "Tail Glow", "Luster Purge", "Mist Ball", "Feather Dance", "Teeter Dance", "Blaze Kick", "Mud Sport", "Ice Ball", "Needle Arm", "Slack Off", "Hyper Voice", "Poison Fang", "Crush Claw", "Blast Burn", "Hydro Cannon", "Meteor Mash", "Astonish", "Weather Ball", "Aromatherapy", "Fake Tears", "Air Cutter", "Overheat", "Odor Sleuth", "Rock Tomb", "Silver Wind", "Metal Sound", "Grass Whistle", "Tickle", "Cosmic Power", "Water Spout", "Signal Beam", "Shadow Punch", "Extrasensory", "Sky Uppercut", "Sand Tomb", "Sheer Cold", "Muddy Water", "Bullet Seed", "Aerial Ace", "Icicle Spear", "Iron Defense", "Block", "Howl", "Dragon Claw", "Frenzy Plant", "Bulk Up", "Bounce", "Mud Shot", "Poison Tail", "Covet", "Volt Tackle", "Magical Leaf", "Water Sport", "Calm Mind", "Leaf Blade", "Dragon Dance", "Rock Blast", "Shock Wave", "Water Pulse", "Doom Desire", "Psycho Boost", "Roost", "Gravity", "Miracle Eye", "Wake-Up Slap", "Hammer Arm", "Gyro Ball", "Healing Wish", "Brine", "Natural Gift", "Feint", "Pluck", "Tailwind", "Acupressure", "Metal Burst", "U-turn", "Close Combat", "Payback", "Assurance", "Embargo", "Fling", "Psycho Shift", "Trump Card", "Heal Block", "Wring Out", "Power Trick", "Gastro Acid", "Lucky Chant", "Me First", "Copycat", "Power Swap", "Guard Swap", "Punishment", "Last Resort", "Worry Seed", "Sucker Punch", "Toxic Spikes", "Heart Swap", "Aqua Ring", "Magnet Rise", "Flare Blitz", "Force Palm", "Aura Sphere", "Rock Polish", "Poison Jab", "Dark Pulse", "Night Slash", "Aqua Tail", "Seed Bomb", "Air Slash", "X-Scissor", "Bug Buzz", "Dragon Pulse", "Dragon Rush", "Power Gem", "Drain Punch", "Vacuum Wave", "Focus Blast", "Energy Ball", "Brave Bird", "Earth Power", "Switcheroo", "Giga Impact", "Nasty Plot", "Bullet Punch", "Avalanche", "Ice Shard", "Shadow Claw", "Thunder Fang", "Ice Fang", "Fire Fang", "Shadow Sneak", "Mud Bomb", "Psycho Cut", "Zen Headbutt", "Mirror Shot", "Flash Cannon", "Rock Climb", "Defog", "Trick Room", "Draco Meteor", "Discharge", "Lava Plume", "Leaf Storm", "Power Whip", "Rock Wrecker", "Cross Poison", "Gunk Shot", "Iron Head", "Magnet Bomb", "Stone Edge", "Captivate", "Stealth Rock", "Grass Knot", "Chatter", "Judgment", "Bug Bite", "Charge Beam", "Wood Hammer", "Aqua Jet", "Attack Order", "Defend Order", "Heal Order", "Head Smash", "Double Hit", "Roar of Time", "Spacial Rend", "Lunar Dance", "Crush Grip", "Magma Storm", "Dark Void", "Seed Flare", "Ominous Wind", "Shadow Force", "Hone Claws", "Wide Guard", "Guard Split", "Power Split", "Wonder Room", "Psyshock", "Venoshock", "Autotomize", "Rage Powder", "Telekinesis", "Magic Room", "Smack Down", "Storm Throw", "Flame Burst", "Sludge Wave", "Quiver Dance", "Heavy Slam", "Synchronoise", "Electro Ball", "Soak", "Flame Charge", "Coil", "Low Sweep", "Acid Spray", "Foul Play", "Simple Beam", "Entrainment", "After You", "Round", "Echoed Voice", "Chip Away", "Clear Smog", "Stored Power", "Quick Guard", "Ally Switch", "Scald", "Shell Smash", "Heal Pulse", "Hex", "Sky Drop", "Shift Gear", "Circle Throw", "Incinerate", "Quash", "Acrobatics", "Reflect Type", "Retaliate", "Final Gambit", "Bestow", "Inferno", "Water Pledge", "Fire Pledge", "Grass Pledge", "Volt Switch", "Struggle Bug", "Bulldoze", "Frost Breath", "Dragon Tail", "Work Up", "Electroweb", "Wild Charge", "Drill Run", "Dual Chop", "Heart Stamp", "Horn Leech", "Sacred Sword", "Razor Shell", "Heat Crash", "Leaf Tornado", "Steamroller", "Cotton Guard", "Night Daze", "Psystrike", "Tail Slap", "Hurricane", "Head Charge", "Gear Grind", "Searing Shot", "Techno Blast", "Relic Song", "Secret Sword", "Glaciate", "Bolt Strike", "Blue Flare", "Fiery Dance", "Freeze Shock", "Ice Burn", "Snarl", "Icicle Crash", "V-create", "Fusion Flare", "Fusion Bolt", "Flying Press", "Mat Block", "Belch", "Rototiller", "Sticky Web", "Fell Stinger", "Phantom Force", "Trick-or-Treat", "Noble Roar", "Ion Deluge", "Parabolic Charge", "Forest’s Curse", "Petal Blizzard", "Freeze-Dry", "Disarming Voice", "Parting Shot", "Topsy-Turvy", "Draining Kiss", "Crafty Shield", "Flower Shield", "Grassy Terrain", "Misty Terrain", "Electrify", "Play Rough", "Fairy Wind", "Moonblast", "Boomburst", "Fairy Lock", "King’s Shield", "Play Nice", "Confide", "Diamond Storm", "Steam Eruption", "Hyperspace Hole", "Water Shuriken", "Mystical Fire", "Spiky Shield", "Aromatic Mist", "Eerie Impulse", "Venom Drench", "Powder", "Geomancy", "Magnetic Flux", "Happy Hour", "Electric Terrain", "Dazzling Gleam", "Celebrate", "Hold Hands", "Baby-Doll Eyes", "Nuzzle", "Hold Back", "Infestation", "Power-Up Punch", "Oblivion Wing", "Thousand Arrows", "Thousand Waves", "Land’s Wrath", "Light of Ruin", "Origin Pulse", "Precipice Blades", "Dragon Ascent", "Hyperspace Fury" };
        public static readonly string[] hiddenPowerString = { "Fighting", "Flying", "Poison", "Ground", "Rock", "Bug", "Ghost", "Steel", "Fire", "Water", "Grass", "Electric", "Psychic", "Ice", "Dragon", "Dark", };
        public static readonly Color[]  hiddenPowerColor = { Color.FromArgb(192, 48, 40), Color.FromArgb(168, 144, 240), Color.FromArgb(160, 64, 160), Color.FromArgb(224, 192, 104), Color.FromArgb(184, 160, 56), Color.FromArgb(168, 184, 32), Color.FromArgb(112, 88, 152), Color.FromArgb(184, 184, 208), Color.FromArgb(240, 128, 48), Color.FromArgb(104, 144, 240), Color.FromArgb(120, 200, 80), Color.FromArgb(248, 208, 48), Color.FromArgb(248, 88, 136), Color.FromArgb(152, 216, 216), Color.FromArgb(112, 56, 248), Color.FromArgb(112, 88, 72), };
        public static readonly Bitmap[] ballImages = { Resources._0, Resources._1, Resources._2, Resources._3, Resources._4, Resources._5, Resources._6, Resources._7, Resources._8, Resources._9, Resources._10, Resources._11, Resources._12, Resources._13, Resources._14, Resources._15, Resources._16, Resources._17, Resources._18, Resources._19, Resources._20, Resources._21, Resources._22, Resources._23, Resources._24, };

        //This array will contain controls that should be enabled when connected and disabled when disconnected.
        Control[] enableWhenConnected = new Control[] { };

        public DataGridViewComboBoxColumn itemItem;
        public DataGridViewColumn itemAmount;
        public DataGridViewComboBoxColumn keyItem;
        public DataGridViewColumn keyAmount;
        public DataGridViewComboBoxColumn tmItem;
        public DataGridViewColumn tmAmount;
        public DataGridViewComboBoxColumn medItem;
        public DataGridViewColumn medAmount;
        public DataGridViewComboBoxColumn berItem;
        public DataGridViewColumn berAmount;
        public System.Windows.Forms.ToolTip ToolTipTSVtt = new System.Windows.Forms.ToolTip();
        public System.Windows.Forms.ToolTip ToolTipTSVss = new System.Windows.Forms.ToolTip();
        public System.Windows.Forms.ToolTip ToolTipTSVt = new System.Windows.Forms.ToolTip();
        public System.Windows.Forms.ToolTip ToolTipTSVs = new System.Windows.Forms.ToolTip();
        public System.Windows.Forms.ToolTip ToolTipPSV = new System.Windows.Forms.ToolTip();


        private void MainForm_Load(object sender, EventArgs e)
        {
            groupBox1.Size = new System.Drawing.Size(154, 74);
            groupBox1.Location = new System.Drawing.Point(744, 339);

            if (UpdateAvailable())
            {
                groupBox1.Size = new System.Drawing.Size(154, 97);
                groupBox1.Location = new System.Drawing.Point(744, 316);
                versionCheck.Visible = true;
            }

            species.Items.AddRange(speciesList);
            ability.Items.AddRange(abilityList);
            heldItem.Items.AddRange(itemList);
            move1.Items.AddRange(moveList);
            move2.Items.AddRange(moveList);
            move3.Items.AddRange(moveList);
            move4.Items.AddRange(moveList);

            DataGridViewComboBoxColumn itemItem = new DataGridViewComboBoxColumn
            {
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                DisplayIndex = 0,
                FlatStyle = FlatStyle.Flat,
                HeaderText = "Items",
                Width = 120,
            };

            DataGridViewColumn itemAmount = new DataGridViewTextBoxColumn
            {
                HeaderText = "Amount",
                DisplayIndex = 1,
                Width = 51,
            };

            DataGridViewComboBoxColumn keyItem = new DataGridViewComboBoxColumn
            {
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                DisplayIndex = 0,
                FlatStyle = FlatStyle.Flat,
                HeaderText = "Items",
                Width = 120,
            };

            DataGridViewColumn keyAmount = new DataGridViewTextBoxColumn
            {
                HeaderText = "Amount",
                DisplayIndex = 1,
                Width = 51,
            };

            DataGridViewComboBoxColumn tmItem = new DataGridViewComboBoxColumn
            {
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                DisplayIndex = 0,
                FlatStyle = FlatStyle.Flat,
                HeaderText = "Items",
                Width = 120,
            };

            DataGridViewColumn tmAmount = new DataGridViewTextBoxColumn
            {
                HeaderText = "Amount",
                DisplayIndex = 1,
                Width = 51,
            };

            DataGridViewComboBoxColumn medItem = new DataGridViewComboBoxColumn
            {
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                DisplayIndex = 0,
                FlatStyle = FlatStyle.Flat,
                HeaderText = "Items",
                Width = 120,
            };

            DataGridViewColumn medAmount = new DataGridViewTextBoxColumn
            {
                HeaderText = "Amount",
                DisplayIndex = 1,
                Width = 51,
            };

            DataGridViewComboBoxColumn berItem = new DataGridViewComboBoxColumn
            {
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                DisplayIndex = 0,
                FlatStyle = FlatStyle.Flat,
                HeaderText = "Items",
                Width = 120,
            };

            DataGridViewColumn berAmount = new DataGridViewTextBoxColumn
            {
                HeaderText = "Amount",
                DisplayIndex = 1,
                Width = 51,
            };

            dataGridView1.Columns.Add(itemItem);
            dataGridView1.Columns.Add(itemAmount);
            dataGridView2.Columns.Add(keyItem);
            dataGridView2.Columns.Add(keyAmount);
            dataGridView3.Columns.Add(tmItem);
            dataGridView3.Columns.Add(tmAmount);
            dataGridView4.Columns.Add(medItem);
            dataGridView4.Columns.Add(medAmount);
            dataGridView5.Columns.Add(berItem);
            dataGridView5.Columns.Add(berAmount);
            foreach (string t in itemList)
            {
                itemItem.Items.Add(t);
                keyItem.Items.Add(t);
                tmItem.Items.Add(t);
                medItem.Items.Add(t);
                berItem.Items.Add(t);
            }
            host.Text = Settings.Default.IP;
        }

        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {

            }
            return pingable;
        }

        class UpdateDetails
        {
            public Version v;
            public string url;
            public string about;
        }

        public bool UpdateAvailable()
        {
            Version netVersion = null;
            string netUrl = "";
            string netAbout = "";
            if (PingHost("fadx.co.uk") == true)
            {
                string xmlUrl = "http://fadx.co.uk/PKMN-NTR/update.xml";
                XmlTextReader reader = null;
                try
                {
                    reader = new XmlTextReader(xmlUrl);
                    reader.MoveToContent();
                    string elementName = "";
                    if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "appinfo"))
                    {
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                elementName = reader.Name;
                            }
                            else
                            {
                                if ((reader.NodeType == XmlNodeType.Text) && (reader.HasValue))
                                    switch (elementName)
                                    {
                                        case "version":
                                            netVersion = new Version(reader.Value);
                                            break;
                                        case "url":
                                            netUrl = reader.Value;
                                            break;
                                        case "about":
                                            netAbout = reader.Value;
                                            break;

                                    }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while checking for updates:\r\n" + ex.Message);
                }
                finally
                {
                    if (reader != null)
                        reader.Close();
                }

                Version applicationVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                if (applicationVersion.CompareTo(netVersion) < 0)
                {
                    foundUpdate = new UpdateDetails();
                    foundUpdate.v = netVersion;
                    foundUpdate.url = netUrl;
                    foundUpdate.about = netAbout;
                    return true;
                }
            }
            return false;
        }

        public void AskToUpdate()
        {
            if (foundUpdate != null)
            {
                Version applicationVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string str = String.Format("Current Version: {0}.\nLatest Vesion: {1}. \n\nWhat's new: {2} ", applicationVersion, foundUpdate.v, foundUpdate.about);
                if (DialogResult.No != MessageBox.Show(str + "\n\nDownload now?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    try
                    {
                        Process.Start(foundUpdate.url);
                    }
                    catch { }
                    return;
                }
            }
        }

        public delegate void LogDelegate(string l);
        public LogDelegate delAddLog;

        public MainForm()
        {
            Program.ntrClient.DataReady += handleDataReady;
            Program.ntrClient.Connected += connectCheck;
            Program.ntrClient.InfoReady += getGame;
            delAddLog = new LogDelegate(Addlog);
            InitializeComponent();
            enableWhenConnected = new Control[] { pokeMoney, pokeMiles, pokeBP, moneyNum, milesNum, bpNum, slotDump, boxDump, nameek6, dumpPokemon, dumpBoxes, radioBoxes, radioDaycare, radioOpponent, radioTrade, pokeName, playerName, pokeTID, TIDNum, pokeSID, SIDNum, hourNum, minNum, secNum, pokeTime, dataGridView1, dataGridView2, dataGridView3, dataGridView4, dataGridView5, showItems, showMedicine, showTMs, showBerries, showKeys, itemAdd, itemWrite, dataGridView1, dataGridView2, dataGridView3, dataGridView4, dataGridView5, delPkm, deleteBox, deleteSlot, deleteAmount, Lang, pokeLang, ivHPNum, ivATKNum, ivDEFNum, ivSPENum, ivSPANum, ivSPDNum, evHPNum, evATKNum, evDEFNum, evSPENum, evSPANum, evSPDNum, isEgg, nickname, nature, button1, heldItem, species, ability, move1, move2, move3, move4, ball, radioParty, dTIDNum, dSIDNum, otName, dPID, setShiny, onlyView, gender, friendship, randomPID, radioBattleBox, cloneDoIt, cloneSlotFrom, cloneBoxFrom, cloneCopiesNo, cloneSlotTo, cloneBoxTo, writeDoIt, writeBrowse, writeAutoInc, writeCopiesNo, writeSlotTo, writeBoxTo, deleteKeepBackup, ExpPoints };
            foreach (Control c in enableWhenConnected)
            {
                c.Enabled = false;
            }
        }

        public void Addlog(string l)
        {
            if (!l.Contains("\r\n"))
            {
                l = l.Replace("\n", "\r\n");
            }
            if (!l.EndsWith("\n"))
            {
                l += "\r\n";
            }
            txtLog.AppendText(l);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Program.ntrClient.sendHeartbeatPacket();
            }
            catch (Exception)
            {
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.ntrClient.disconnect();
        }


        public void startAutoDisconnect()
        {
            disconnectTimer.Enabled = true;
        }


        private void disconnectTimer_Tick(object sender, EventArgs e)
        {
            disconnectTimer.Enabled = false;
            Program.ntrClient.disconnect();
            game = GameType.None;
        }
        
        public void connectCheck(object sender, EventArgs e)
        {
            Program.scriptHelper.listprocess();
            buttonConnect.Text = "Connected";
            buttonConnect.Enabled = false;
            buttonDisconnect.Enabled = true;
            foreach (Control c in enableWhenConnected)
            {
                c.Enabled = true;
            }
            Settings.Default.IP = host.Text;
            Settings.Default.Save();
        }

        //This functions handles additional information events from NTR netcode.
        //We are only interested in them if they are a process list, containing
        //our game's PID and game type.
        public void getGame(object sender, EventArgs e)
        {
            InfoReadyEventArgs args = (InfoReadyEventArgs)e;
            //XY
            if (args.info.Contains("kujira-1"))
            {
                game = GameType.X;
                string log = args.info;
                pname = ", pname: kujira-1";
                string splitlog = log.Substring(log.IndexOf(pname) - 2, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 2), 16);
                moneyoff = 0x8C6A6AC;
                milesoff = 0x8C82BA0;
                bpoff = 0x8C6A6E0;
                boxOff = 0x8C861C8;
                daycare1Off = 0x8C7FF4C;
                daycare2Off = 0x8C8003C;
                itemsoff = 0x8C67564;
                medsoff = 0x8C67ECC;
                keysoff = 0x8C67BA4;
                tmsoff = 0x8C67D24;
                bersoff = 0x8C67FCC;
                nameoff = 0x8C79C84;
                tidoff = 0x8C79C3C;
                sidoff = 0x8C79C3E;
                hroff = 0x8CE2814;
                langoff = 0x8C79C69;
                tradeoffrg = 0x8500000;
                battleBoxOff = 147237932;
                //opwroff = 0x8C7D23E;
                //shoutoutOff = 0x8803CF8;
            }
            else if (args.info.Contains("kujira-2"))
            {
                game = GameType.Y;
                string log = args.info;
                pname = ", pname: kujira-2";
                string splitlog = log.Substring(log.IndexOf(pname) - 2, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 2), 16);
                moneyoff = 0x8C6A6AC;
                milesoff = 0x8C82BA0;
                bpoff = 0x8C6A6E0;
                boxOff = 0x8C861C8;
                daycare1Off = 0x8C7FF4C;
                daycare2Off = 0x8C8003C;
                itemsoff = 0x8C67564;
                medsoff = 0x8C67ECC;
                keysoff = 0x8C67BA4;
                tmsoff = 0x8C67D24;
                bersoff = 0x8C67FCC;
                nameoff = 0x8C79C84;
                tidoff = 0x8C79C3C;
                sidoff = 0x8C79C3E;
                hroff = 0x8CE2814;
                langoff = 0x8C79C69;
                tradeoffrg = 0x8500000;
                battleBoxOff = 147237932;
                //opwroff = 0x8C7D23E;
                //shoutoutOff = 0x8803CF8;
            }
            else if (args.info.Contains("sango-1")) //Omega Ruby
            {
                game = GameType.OR;
                string log = args.info;
                pname = ", pname:  sango-1";
                string splitlog = log.Substring(log.IndexOf(pname) - 2, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 2), 16);
                moneyoff = 0x8C71DC0;
                milesoff = 0x8C8B36C;
                bpoff = 0x8C71DE8;
                boxOff = 0x8C9E134;
                daycare1Off = 0x8C88370;
                daycare2Off = 0x8C88460;
                itemsoff = 0x8C6EC70;
                medsoff = 0x8C6F5E0;
                keysoff = 0x8C6F2B0;
                tmsoff = 0x8C6F430;
                bersoff = 0x8C6F6E0;
                nameoff = 0x8C81388;
                tidoff = 0x8C81340;
                sidoff = 0x8C81342;
                hroff = 0x8CFBD88;
                langoff = 0x8C8136D;
                tradeoffrg = 0x8520000;
                battleBoxOff = 147268400;
                //opwroff = 0x8C83D94;
                //shoutoutOff = 0x8803CF8;
            }
            else if (args.info.Contains("sango-2")) //Alpha Sapphire
            {
                game = GameType.AS;
                string log = args.info;
                pname = ", pname:  sango-2";
                string splitlog = log.Substring(log.IndexOf(pname) - 2, log.Length - log.IndexOf(pname));
                pid = Convert.ToInt32("0x" + splitlog.Substring(0, 2), 16);
                moneyoff = 0x8C71DC0;
                milesoff = 0x8C8B36C;
                bpoff = 0x8C71DE8;
                boxOff = 0x8C9E134;
                daycare1Off = 0x8C88370;
                daycare2Off = 0x8C88460;
                itemsoff = 0x8C6EC70;
                medsoff = 0x8C6F5E0;
                keysoff = 0x8C6F2B0;
                tmsoff = 0x8C6F430;
                bersoff = 0x8C6F6E0;
                nameoff = 0x8C81388;
                tidoff = 0x8C81340;
                sidoff = 0x8C81342;
                hroff = 0x8CFBD88;
                langoff = 0x8C8136D;
                tradeoffrg = 0x8520000;
                battleBoxOff = 147268400;
                //opwroff = 0x8C83D94;
                //shoutoutOff = 0x8803CF8;
            }
            else //not a process list or game not found - ignore packet
            {
                return;
            }

            if (game != GameType.None)
            {
                dumpAllData();
            }
        }

        public void dumpAllData()
        {
            dumpMoney();
            dumpTID();
            dumpSID();
            dumpName();
            dumpTime();
            dumpBP();
            dumpMiles();
            dumpLang();
            dumpItems();
        }

        public void dumpItems()
        {
            Program.scriptHelper.data(itemsoff, 0x640, pid, "items.temp");
            Program.scriptHelper.data(keysoff, 0x180, pid, "keys.temp");
            Program.scriptHelper.data(tmsoff, 0x1A8, pid, "tms.temp");
            Program.scriptHelper.data(medsoff , 0x100, pid, "meds.temp");
            Program.scriptHelper.data(bersoff, 0x120, pid, "bers.temp");
        }

        /*
         * Below are functions for requesting and handling data about basic info (name, money, trainer ID, etc.)
         */
        public void dumpName()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x18], handleNameData, null);
            waitingForData.Add(Program.scriptHelper.data(nameoff, 0x18, pid), myArgs);
        }

        public void handleNameData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;
            SetText(playerName, Encoding.Unicode.GetString(args.data));
        }

        public void dumpTID()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x02], handleTIDData, null);
            waitingForData.Add(Program.scriptHelper.data(tidoff, 0x02, pid), myArgs);
        }

        public void handleTIDData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;
            SetValue(TIDNum, BitConverter.ToUInt16(args.data, 0));
        }

        public void dumpSID()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x02], handleSIDData, null);
            waitingForData.Add(Program.scriptHelper.data(sidoff, 0x02, pid), myArgs);
        }

        public void handleSIDData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;
            SetValue(SIDNum, BitConverter.ToUInt16(args.data, 0));
        }

        public void dumpTime()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x04], handleHrData, null);
            waitingForData.Add(Program.scriptHelper.data(hroff, 0x04, pid), myArgs);
        }

        public void handleHrData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;
            SetValue(hourNum, BitConverter.ToUInt16(args.data, 0));
            SetValue(minNum, args.data[2]);
            SetValue(secNum, args.data[3]);
        }

        public void dumpLang()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x01], handleLangData, null);
            waitingForData.Add(Program.scriptHelper.data(langoff, 0x01, pid), myArgs);
        }

        public void handleLangData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;

            byte langbyte = args.data[0];
            int i = 0;
            switch (langbyte) { 
                case 1: i = 0; break;
                case 2: i = 1; break;
                case 3: i = 2; break;
                case 4: i = 3; break;
                case 5: i = 4; break;
                case 7: i = 5; break;
                case 8: i = 6; break;
            }
            SetSelectedIndex(Lang, i);
    }

        public void dumpMoney()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x04], handleMoneyData, null);
            waitingForData.Add(Program.scriptHelper.data(moneyoff, 0x04, pid), myArgs);
        }

        public void handleMoneyData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;
            SetValue(moneyNum,BitConverter.ToInt32(args.data, 0));
        }

        public void dumpMiles()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x04], handleMilesData, null);
            waitingForData.Add(Program.scriptHelper.data(milesoff, 0x04, pid), myArgs);
        }

        public void handleMilesData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;

            
            SetValue(milesNum, BitConverter.ToInt32(args.data,0));
        }

        public void dumpBP()
        {
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x04], handleBPData, null);
            waitingForData.Add(Program.scriptHelper.data(bpoff, 0x04, pid), myArgs);
        }

        public void handleBPData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;


            SetValue(bpNum, BitConverter.ToInt32(args.data, 0));
        }

        /*
        public static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        */

        static List<uint> findOccurences(byte[] haystack, byte[] needle)
        {
            List<uint> occurences = new List<uint>();

            for (uint i = 0; i < haystack.Length; i++)
            {
                if (needle[0] == haystack[i])
                {
                    bool found = true;
                    uint j, k;
                    for (j = 0, k = i; j < needle.Length; j++, k++)
                    {
                        if (k >= haystack.Length || needle[j] != haystack[k])
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        occurences.Add(i - 1);
                        i = k;
                    }
                }
            }
            return occurences;
        }

        private static string numberPattern = " ({0})";

        public static string NextAvailableFilename(string path)
        {
            if (!File.Exists(path))
                return path;

            if (Path.HasExtension(path))
                return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path)), numberPattern));

            return GetNextFilename(path + numberPattern);
        }

        private static string GetNextFilename(string pattern)
        {
            string tmp = string.Format(pattern, 1);
            if (tmp == pattern)
                throw new ArgumentException("The pattern must include an index place-holder", "pattern");

            if (!File.Exists(tmp))
                return tmp;

            int min = 1, max = 2;

            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }

            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot)))
                    min = pivot;
                else
                    max = pivot;
            }

            return string.Format(pattern, max);
        }

        #region oldcode
        public void txtLog_TextChanged(object sender, EventArgs e)
        {
            isItemsDumped();
        }

        public void isItemsDumped()
        {
            if (txtLog.Text.Contains("items.temp successfully") &&
                txtLog.Text.Contains("keys.temp successfully") &&
                txtLog.Text.Contains("tms.temp successfully") &&
                txtLog.Text.Contains("meds.temp successfully") &&
                txtLog.Text.Contains("bers.temp successfully"))
            {
                txtLog.Clear();
                readItems();
                RMTemp();
            }
        }

        public void readItems()
        {
            const string dumpedItems = "items.temp";
            const string dumpedKeys = "keys.temp";
            const string dumpedTMs = "tms.temp";
            const string dumpedMeds = "meds.temp";
            const string dumpedBers = "bers.temp";

            if (File.Exists(dumpedItems))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(dumpedItems, FileMode.Open)))
                {
                    const int itemsLength = 1600;
                    items = reader.ReadBytes(itemsLength);
                    string itemsstring = BitConverter.ToString(items).Replace("-", "");
                    string[] itemssplit = itemsstring.Split(new[] { "00000000" }, StringSplitOptions.None);
                    decimal numofItemsdec = itemssplit[0].Length / (Decimal)8;
                    decimal numofItemsRounded = Math.Ceiling(numofItemsdec);
                    numofItems = Convert.ToInt32(numofItemsRounded);
                    if (numofItems > 0)
                    {
                        dataGridView1.Rows.Add(numofItems);
                    }
                    for (int i = 0; i < numofItems; i++)
                    {
                        uint itemsfinal = BitConverter.ToUInt16(items, i * 4);
                        uint amountfinal = BitConverter.ToUInt16(items, (i * 4) + 2);
                        dataGridView1.Rows[i].Cells[0].Value = itemList[itemsfinal];
                        dataGridView1.Rows[i].Cells[1].Value = amountfinal;
                    }
                }
            }

            if (File.Exists(dumpedKeys))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(dumpedKeys, FileMode.Open)))
                {
                    const int itemsLength = 384;
                    byte[] keys = reader.ReadBytes(itemsLength);
                    string itemsstring = BitConverter.ToString(keys).Replace("-", "");
                    string[] itemssplit = itemsstring.Split(new[] { "00000000" }, StringSplitOptions.None);
                    decimal numofItemsdec = itemssplit[0].Length / (Decimal)8;
                    decimal numofItemsRounded = Math.Ceiling(numofItemsdec);
                    int numofKeys = Convert.ToInt32(numofItemsRounded);
                    if (numofKeys > 0)
                    {
                        dataGridView2.Rows.Add(numofKeys);
                    }
                    for (int i = 0; i < numofKeys; i++)
                    {
                        uint keysfinal = BitConverter.ToUInt16(keys, i * 4);
                        uint keysamountfinal = BitConverter.ToUInt16(keys, (i * 4) + 2);
                        dataGridView2.Rows[i].Cells[0].Value = itemList[keysfinal];
                        dataGridView2.Rows[i].Cells[1].Value = keysamountfinal;

                    }
                }
            }

            if (File.Exists(dumpedTMs))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(dumpedTMs, FileMode.Open)))
                {
                    const int itemsLength = 432;
                    byte[] tms = reader.ReadBytes(itemsLength);
                    string itemsstring = BitConverter.ToString(tms).Replace("-", "");
                    string[] itemssplit = itemsstring.Split(new[] { "00000000" }, StringSplitOptions.None);
                    decimal numofItemsdec = itemssplit[0].Length / (Decimal)8;
                    decimal numofItemsRounded = Math.Ceiling(numofItemsdec);
                    int numofTMs = Convert.ToInt32(numofItemsRounded);
                    if (numofTMs > 0)
                    {
                        dataGridView3.Rows.Add(numofTMs);
                    }
                    for (int i = 0; i < numofTMs; i++)
                    {
                        uint tmsfinal = BitConverter.ToUInt16(tms, i * 4);
                        uint tmsamountfinal = BitConverter.ToUInt16(tms, (i * 4) + 2);
                        dataGridView3.Rows[i].Cells[0].Value = itemList[tmsfinal];
                        dataGridView3.Rows[i].Cells[1].Value = tmsamountfinal;

                    }
                }
            }

            if (File.Exists(dumpedMeds))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(dumpedMeds, FileMode.Open)))
                {
                    const int itemsLength = 256;
                    byte[] meds = reader.ReadBytes(itemsLength);
                    string itemsstring = BitConverter.ToString(meds).Replace("-", "");
                    string[] itemssplit = itemsstring.Split(new[] { "00000000" }, StringSplitOptions.None);
                    decimal numofItemsdec = itemssplit[0].Length / (Decimal)8;
                    decimal numofItemsRounded = Math.Ceiling(numofItemsdec);
                    int numofMeds = Convert.ToInt32(numofItemsRounded);
                    if (numofMeds > 0)
                    {
                        dataGridView4.Rows.Add(numofMeds);
                    }
                    for (int i = 0; i < numofMeds; i++)
                    {
                        uint medsfinal = BitConverter.ToUInt16(meds, i * 4);
                        uint medsamountfinal = BitConverter.ToUInt16(meds, (i * 4) + 2);
                        dataGridView4.Rows[i].Cells[0].Value = itemList[medsfinal];
                        dataGridView4.Rows[i].Cells[1].Value = medsamountfinal;

                    }
                }
            }

            if (File.Exists(dumpedBers))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(dumpedBers, FileMode.Open)))
                {
                    const int itemsLength = 288;
                    byte[] bers = reader.ReadBytes(itemsLength);
                    string itemsstring = BitConverter.ToString(bers).Replace("-", "");
                    string[] itemssplit = itemsstring.Split(new[] { "00000000" }, StringSplitOptions.None);
                    decimal numofItemsdec = itemssplit[0].Length / (Decimal)8;
                    decimal numofItemsRounded = Math.Ceiling(numofItemsdec);
                    int numofBers = Convert.ToInt32(numofItemsRounded);
                    if (numofBers > 0)
                    {
                        dataGridView5.Rows.Add(numofBers);
                    }
                    for (int i = 0; i < numofBers; i++)
                    {
                        uint bersfinal = BitConverter.ToUInt16(bers, i * 4);
                        uint bersamountfinal = BitConverter.ToUInt16(bers, (i * 4) + 2);
                        dataGridView5.Rows[i].Cells[0].Value = itemList[bersfinal];
                        dataGridView5.Rows[i].Cells[1].Value = bersamountfinal;

                    }
                }
            }
        }

        public void RMTemp()
        {
            DirectoryInfo di = new DirectoryInfo(@Application.StartupPath);
            FileInfo[] files = di.GetFiles("*.temp")
                                 .Where(p => p.Extension == ".temp").ToArray();
            foreach (FileInfo file in files)
                try
                {
                    file.Attributes = FileAttributes.Normal;
                    File.Delete(file.FullName);
                }
                catch
                {
                }
        }
        
        #endregion oldcode
        public void getHiddenPower()
        {
            int hp = (15 * ((dumpedPKHeX.IV_HP & 1) + 2 * (dumpedPKHeX.IV_ATK & 1) + 4 * (dumpedPKHeX.IV_DEF & 1) + 8 * (dumpedPKHeX.IV_SPE & 1) + 16 * (dumpedPKHeX.IV_SPA & 1) + 32 * (dumpedPKHeX.IV_SPD & 1)) / 63);
            
            SetText(hiddenPower, hiddenPowerString[hp]);
            SetColor(hiddenPower, hiddenPowerColor[hp], true);
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
            Program.scriptHelper.connect(host.Text, 8000);
        }


        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            Program.scriptHelper.disconnect();
            game = GameType.None;
            buttonConnect.Text = "Connect";
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            foreach (Control c in enableWhenConnected)
            {
                c.Enabled = false;
            }
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
        }

        private void pokeName_Click(object sender, EventArgs e)
        {
            if (playerName.Text.Length <= 12)
            {
                string nameS = playerName.Text.PadRight(12, '\0');
                byte[] nameBytes = Encoding.Unicode.GetBytes(nameS);
                Program.scriptHelper.write(nameoff, nameBytes, pid);
            }
            else
            {
                MessageBox.Show("That name is too long, please choose a trainer name of 12 character or less.", "Name too long!");
            }
        }
        
        private void pokeTID_Click(object sender, EventArgs e)
        {
            byte[] tidbyte = BitConverter.GetBytes(Convert.ToUInt16(TIDNum.Value));
            Program.scriptHelper.write(tidoff, tidbyte, pid);
        }

        private void pokeSID_Click(object sender, EventArgs e)
        {
            byte[] sidbyte = BitConverter.GetBytes(Convert.ToUInt16(SIDNum.Value));
            Program.scriptHelper.write(sidoff, sidbyte, pid);
        }

        private void pokeTime_Click(object sender, EventArgs e)
        {
            byte[] timeData = new byte[4];
            BitConverter.GetBytes(Convert.ToUInt16(hourNum.Value)).CopyTo(timeData, 0);
            timeData[2] = Convert.ToByte(minNum.Value);
            timeData[3] = Convert.ToByte(secNum.Value);
            Program.scriptHelper.write(hroff, timeData, pid);
        }

        private void pokeMoney_Click(object sender, EventArgs e)
        {
            byte[] moneybyte = BitConverter.GetBytes(Convert.ToInt32(moneyNum.Value));
            Program.scriptHelper.write(moneyoff, moneybyte, pid);
        }

        private void pokeMiles_Click(object sender, EventArgs e)
        {
            byte[] milesbyte = BitConverter.GetBytes(Convert.ToInt32(milesNum.Value));
            Program.scriptHelper.write(milesoff, milesbyte, pid);
        }

        private void pokeBP_Click(object sender, EventArgs e)
        {
            byte[] bpbyte = BitConverter.GetBytes(Convert.ToInt32(bpNum.Value));
            Program.scriptHelper.write(bpoff, bpbyte, pid);
        }     

        private void dumpPokemon_Click(object sender, EventArgs e)
        {
            uint dumpOff = 0;

            if (radioOpponent.Checked == true)
            {
                DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x1FFFF], handleOpponentData, null);
                waitingForData.Add(Program.scriptHelper.data(0x8800000, 0x1FFFF, pid), myArgs);
            }
            else if (radioTrade.Checked == true)
            {
                DataReadyWaiting myArgs = new DataReadyWaiting(new byte[0x1FFFF], handleTradeData, null);
                waitingForData.Add(Program.scriptHelper.data(tradeoffrg, 0x1FFFF, pid), myArgs);
            }
            else { 
                if (radioBattleBox.Checked == true)
                { 
                    dumpOff = battleBoxOff + ((Decimal.ToUInt32(boxDump.Value) - 1) * POKEBYTES);
                }
                else if (radioBoxes.Checked == true)
                {
                    uint ssd = ((Decimal.ToUInt32(boxDump.Value) - 1 ) * BOXSIZE) + Decimal.ToUInt32(slotDump.Value) - 1;
                    dumpOff = boxOff + (ssd * POKEBYTES);
                }
                else if (radioDaycare.Checked == true)
                {
                    dumpOff = daycare1Off;
                }
                else if (radioParty.Checked == true)
                {
                    dumpOff = partyOff + (Decimal.ToUInt32(boxDump.Value) - 1) * 484;
                }

                DataReadyWaiting myArgs = new DataReadyWaiting(new byte[POKEBYTES], handlePkmData, null);
                uint mySeq = Program.scriptHelper.data(dumpOff, POKEBYTES, pid);
                waitingForData.Add(mySeq, myArgs);
            }
        }

        private void dumpBoxes_Click(object sender, EventArgs e)
        {
            if (radioBoxes.Checked == true)
            {
                DataReadyWaiting myArgs = new DataReadyWaiting(new byte[BOXES * BOXSIZE * POKEBYTES], handleAllBoxesData, null);
                waitingForData.Add(Program.scriptHelper.data(boxOff, BOXES * BOXSIZE * POKEBYTES, pid), myArgs);
            }
            else if (radioDaycare.Checked == true)
            {
                DataReadyWaiting myArgs = new DataReadyWaiting(new byte[POKEBYTES], handlePkmData, null);
                uint mySeq = Program.scriptHelper.data(daycare2Off, POKEBYTES, pid);
                waitingForData.Add(mySeq, myArgs);
            }
        }

        public void handleAllBoxesData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;
            string folderPath = @Application.StartupPath + "\\" + FOLDERPOKE + "\\";
            (new System.IO.FileInfo(folderPath)).Directory.Create();
            string fileName = nameek6.Text + "_boxes.ek6";
            writePokemonToFile(args.data, folderPath + fileName);
        }

        public void handleOpponentData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;

            byte[] relativePattern = null;
            uint offsetAfter = 0;

            //TODO: maybe set the relative pattern along with other variables in getGame()?
            if (game == GameType.X || game == GameType.Y)
            {
                relativePattern = new byte[] { 0x60, 0x75, 0xC6, 0x08, 0xDC, 0xA8, 0xC7, 0x08, 0xD0, 0xB6, 0xC7, 0x08 };
                offsetAfter = 637;
            }
            if (game == GameType.OR || game == GameType.AS)
            {
                relativePattern = new byte[] { 0x60, 0xE7, 0xC6, 0x08, 0x6C, 0xEC, 0xC6, 0x08, 0xE0, 0x1F, 0xC8, 0x08, 0x00, 0x39, 0xC8, 0x08 };
                offsetAfter = 673;
            }

            List<uint> occurences = findOccurences(args.data, relativePattern);
            int count = 0;
            foreach (uint occurence in occurences)
            {
                count++;
                int dataOffset = (int)(occurence + offsetAfter);
                DataReadyWaiting args_pkm = new DataReadyWaiting(args.data.Skip(dataOffset).Take(POKEBYTES).ToArray(), handlePkmData, null);
                handlePkmData(args_pkm);
            }
        }

        public void handleTradeData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;

            byte[] relativePattern = null;
            uint offsetAfter = 0;

            if (game == GameType.X || game == GameType.Y)
            {
                relativePattern = new byte[] { 0x08, 0x1C, 0x01, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xD8, 0xBE, 0x59 };
                offsetAfter += 98;
            }

            if (game == GameType.OR || game == GameType.AS)
            {
                relativePattern = new byte[] { 0x08, 0x1E, 0x01, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x9C, 0xE8, 0x5D };
                offsetAfter += 98;
            }

            List<uint> occurences = findOccurences(args.data, relativePattern);
            int count = 0;
            foreach (uint occurence in occurences)
            {
                count++;
                if (count != 2) continue;
                int dataOffset = (int)(occurence + offsetAfter);
                DataReadyWaiting args_pkm = new DataReadyWaiting(args.data.Skip(dataOffset).Take(POKEBYTES).ToArray(), handlePkmData, null);
                handlePkmData(args_pkm);
            }
        }

        public void handlePkmData(object args_obj)
        {
            try { //TODO: TEMPORARY HACK, DO PROPER ERROR HANDLING
                DataReadyWaiting args = (DataReadyWaiting)args_obj;

                //TODO: write it to a different object first, check correctness, then write it to dumpedPKHeX
                dumpedPKHeX.Data = PKHeX.decryptArray(args.data); 

                bool dataCorrect = dumpedPKHeX.Species != 0;
                if (!onlyView.Checked)
                {
                    DialogResult res = DialogResult.Cancel;
                    if (!dataCorrect)
                    {
                         res = MessageBox.Show("This Pokemon's data seems to be empty.\r\nPress OK if you want to save it, Cancel if you don't.",
                            "Empty data", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    if (dataCorrect || res == DialogResult.OK)
                    { 
                        string folderPath = @Application.StartupPath + "\\" + FOLDERPOKE + "\\";
                        (new System.IO.FileInfo(folderPath)).Directory.Create();
                        string fileName = nameek6.Text + ".pk6";
                        writePokemonToFile(dumpedPKHeX.Data, folderPath + fileName);
                    }
                }
                else if (!dataCorrect)
                {
                        MessageBox.Show("This Pokemon's data seems to be empty.", "Empty data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!dataCorrect)
                    return;

                SetSelectedIndex(species, dumpedPKHeX.Species - 1);
                SetValue(ivHPNum, dumpedPKHeX.IV_HP);
                SetValue(ivATKNum, dumpedPKHeX.IV_ATK);
                SetValue(ivDEFNum, dumpedPKHeX.IV_DEF);
                SetValue(ivSPANum, dumpedPKHeX.IV_SPA);
                SetValue(ivSPDNum, dumpedPKHeX.IV_SPD);
                SetValue(ivSPENum, dumpedPKHeX.IV_SPE);
                SetValue(evHPNum, dumpedPKHeX.EV_HP);
                SetValue(evATKNum, dumpedPKHeX.EV_ATK);
                SetValue(evDEFNum, dumpedPKHeX.EV_DEF);
                SetValue(evSPANum, dumpedPKHeX.EV_SPA);
                SetValue(evSPDNum, dumpedPKHeX.EV_SPD);
                SetValue(evSPENum, dumpedPKHeX.EV_SPE);
                SetSelectedIndex(ball, dumpedPKHeX.Ball - 1);
                SetValue(friendship, dumpedPKHeX.HT_Friendship);

                SetValue(ExpPoints, dumpedPKHeX.EXP);

                switch (dumpedPKHeX.Gender) { 
                    case 0:
                        SetColor(gender, Color.Blue, false);
                        SetText(gender, "♂");
                        break;
                    case 1:
                        SetColor(gender, Color.Red, false);
                        SetText(gender, "♀");
                        break;
                    case 2:
                        SetColor(gender, Color.Gray, false);
                        SetText(gender, "-");
                        break;
                }

                SetValue(dTIDNum, dumpedPKHeX.TID);
                SetValue(dSIDNum, dumpedPKHeX.SID);
                SetText(dPID, dumpedPKHeX.PID.ToString("X"));

                SetText(nickname, dumpedPKHeX.Nickname);
                SetText(otName, dumpedPKHeX.OT_Name);

                getHiddenPower();

                SetChecked(isEgg, dumpedPKHeX.IsEgg);

                SetSelectedIndex(heldItem, dumpedPKHeX.HeldItem);
                SetSelectedIndex(ability, dumpedPKHeX.Ability - 1);
                SetSelectedIndex(nature, dumpedPKHeX.Nature);

                SetSelectedIndex(move1, dumpedPKHeX.Move1);
                SetSelectedIndex(move2, dumpedPKHeX.Move2);
                SetSelectedIndex(move3, dumpedPKHeX.Move3);
                SetSelectedIndex(move4, dumpedPKHeX.Move4);

                //TODO: make it thread-safe!
                //ToolTipTSVt.SetToolTip(dTIDNum, "TSV: " + ((dumpedPKHeX.TID ^ dumpedPKHeX.SID) >> 4).ToString());
                //ToolTipTSVs.SetToolTip(dSIDNum, "TSV: " + ((dumpedPKHeX.TID ^ dumpedPKHeX.SID) >> 4).ToString());
                //ToolTipPSV.SetToolTip(dPID, "PSV: " + ((int)((dumpedPKHeX.PID >> 16 ^ dumpedPKHeX.PID & 0xFFFF) >> 4)).ToString());

                SetEnabled(setShiny, !dumpedPKHeX.isShiny); //If it's already shiny, the box will be disabled
                SetText(setShiny, dumpedPKHeX.isShiny ? "★" : "☆");
            } catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = true;
            boxDump.Visible = false;
            slotDump.Visible = false;
            nameek6.Visible = true;
            dumpBoxes.Visible = false;
            dumpPokemon.Location = new System.Drawing.Point(6, 61);
            nameek6.Location = new System.Drawing.Point(6, 39);
            label9.Location = new System.Drawing.Point(6, 20);
            dumpPokemon.Size = new System.Drawing.Size(197, 23);
            nameek6.Size = new System.Drawing.Size(197, 23);
            dumpPokemon.Text = "Dump";
            dumpBoxes.Enabled = true;
            nameek6.Enabled = true;
            onlyView.Visible = false;
        }

        private void radioBoxes_CheckedChanged(object sender, EventArgs e)
        {
            boxDump.Minimum = 1;
            boxDump.Maximum = 31;
            label8.Text = "Box:";
            label7.Text = "Slot:";
            label9.Text = "Filename:";
            boxDump.Visible = true;
            slotDump.Visible = true;
            dumpBoxes.Visible = true;
            nameek6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label9.Location = new System.Drawing.Point(97, 20);
            nameek6.Location = new System.Drawing.Point(100, 39);
            nameek6.Size = new System.Drawing.Size(103, 20);
            dumpPokemon.Size = new System.Drawing.Size(86, 23);
            dumpBoxes.Size = new System.Drawing.Size(105, 23);
            dumpBoxes.Location = new System.Drawing.Point(98, 61);
            dumpPokemon.Location = new System.Drawing.Point(6, 61);
            dumpPokemon.Text = "Dump";
            dumpBoxes.Text = "Dump All Boxes";
            onlyView.Visible = true;
            onlyView.Checked = false;
        }

        private void radioDaycare_CheckedChanged(object sender, EventArgs e)
        {
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = true;
            boxDump.Visible = false;
            slotDump.Visible = false;
            nameek6.Visible = true;
            dumpBoxes.Visible = true;
            dumpPokemon.Location = new System.Drawing.Point(6, 61);
            nameek6.Location = new System.Drawing.Point(6, 39);
            label9.Location = new System.Drawing.Point(6, 20);
            dumpPokemon.Size = new System.Drawing.Size(95, 23);
            dumpBoxes.Size = new System.Drawing.Size(95, 23);
            dumpBoxes.Location = new System.Drawing.Point(108, 61);
            nameek6.Size = new System.Drawing.Size(197, 23);
            dumpPokemon.Text = "Dump Slot 1";
            dumpBoxes.Text = "Dump Slot 2";
            dumpBoxes.Enabled = true;
            nameek6.Enabled = true;
            onlyView.Visible = false;
        }

        private void radioOpponent_CheckedChanged(object sender, EventArgs e)
        {
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = true;
            boxDump.Visible = false;
            slotDump.Visible = false;
            nameek6.Visible = true;
            dumpBoxes.Visible = false;
            dumpPokemon.Location = new System.Drawing.Point(6, 61);
            nameek6.Location = new System.Drawing.Point(6, 39);
            label9.Location = new System.Drawing.Point(6, 20);
            dumpPokemon.Size = new System.Drawing.Size(197, 23);
            nameek6.Size = new System.Drawing.Size(197, 23);
            dumpPokemon.Text = "Dump";
            dumpBoxes.Enabled = true;
            nameek6.Enabled = true;
            onlyView.Visible = false;
        }
        
        void writeTab_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        //Returns 0 on success, other values on failure
        private int readPokemonFromFile(string filename, out byte[] result)
        {
            string extension = Path.GetExtension(filename);
            result = new byte[POKEBYTES];

            bool isEncrypted = false;

            if (extension == ".pk6" || extension == ".pkx")
                isEncrypted = false;
            else if (extension == ".ek6" || extension == ".ekx")
                isEncrypted = true;
            else
            {
                MessageBox.Show("Please make sure you are using a valid PKX/EKX file.", "Incorrect File Size");
                return 1;
            }

            byte[] tmpBytes = File.ReadAllBytes(filename);

            if (tmpBytes.Length == 260 || tmpBytes.Length == 232)
            {
                //All OK, commit
                if (isEncrypted)
                {
                    tmpBytes.CopyTo(result, 0);
                }
                else
                {
                    PKHeX.encryptArray(tmpBytes.Take(POKEBYTES).ToArray()).CopyTo(result, 0);
                }
            }
            else
            {
                MessageBox.Show("Please make sure you are using a valid PKX/EKX file.", "Incorrect File Size");
                return 2;
            }
            return 0;
        }

        //Returns 0 on success, positive value represents how many copies could not be written.
        private int writePokemonToBox(byte[] data, uint boxFrom, uint count)
        {
            if (data.Length != POKEBYTES)
                return -1;

            int ret = 0;
            if (boxFrom + count > BOXES * BOXSIZE) 
            {
                uint newCount = BOXES * BOXSIZE - boxFrom;
                ret = (int)(count - newCount);
                count = newCount;
            }

            byte[] dataToWrite = new byte[count * POKEBYTES];
            for (int i = 0; i < count; i++)
            {
                data.CopyTo(dataToWrite, i * POKEBYTES);
            }
            uint offset = boxOff + boxFrom * POKEBYTES;
            Program.scriptHelper.write(offset, dataToWrite, pid);
            return ret;
        }

        private void writePokemonToFile(byte[] data, string fileName, bool overwrite = false)
        {
            if (!overwrite)
            {
                //if current filename is available, it won't be changed
                fileName = NextAvailableFilename(fileName);
            }

            FileStream fs = File.OpenWrite(fileName);
            fs.Write(data, 0, data.Length);
            fs.Close();
        }

        #region housekeeping for cloning
        private uint cloneGetCopies()
        {
            return Decimal.ToUInt32(cloneCopiesNo.Value);
        }

        private uint cloneGetBoxIndexTo()
        {
            return Decimal.ToUInt32((cloneBoxTo.Value - 1) * BOXSIZE + cloneSlotTo.Value - 1);
        }

        private uint cloneGetBoxIndexFrom()
        {
            return Decimal.ToUInt32((cloneBoxFrom.Value - 1) * BOXSIZE + cloneSlotFrom.Value - 1);
        }

        private void cloneBoxTo_ValueChanged(object sender, EventArgs e)
        {
            cloneCopiesNo.Maximum = BOXES * BOXSIZE - cloneGetBoxIndexTo();
        }

        private void cloneSlotTo_ValueChanged(object sender, EventArgs e)
        {
            cloneCopiesNo.Maximum = BOXES * BOXSIZE - cloneGetBoxIndexTo();
        }

        #endregion housekeeping for cloning

        private void cloneDoIt_Click(object sender, EventArgs e)
        {
            uint offset = boxOff + cloneGetBoxIndexFrom() * POKEBYTES;
            uint mySeq = Program.scriptHelper.data(offset, POKEBYTES, pid);
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[POKEBYTES], handleCloneData, null);
            waitingForData.Add(mySeq, myArgs);
        }

        private void handleCloneData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;
            writePokemonToBox(args.data, cloneGetBoxIndexTo(), cloneGetCopies());
        }

        #region housekeeping for write from file
        private uint writeGetCopies()
        {
            return Decimal.ToUInt32(writeCopiesNo.Value);
        }

        private uint writeGetBoxIndex()
        {
            return Decimal.ToUInt32((writeBoxTo.Value - 1) * BOXSIZE + writeSlotTo.Value - 1);
        }

        private void writeSetBoxIndex(uint index)
        {
            if (index >= BOXES * BOXSIZE)
                index = BOXES * BOXSIZE - 1;
            uint box = index / BOXSIZE;
            uint slot = index % BOXSIZE;
            SetValue(writeBoxTo, box + 1);
            SetValue(writeSlotTo, slot + 1);
        }

        private void writeBoxTo_ValueChanged(object sender, EventArgs e)
        {
            writeCopiesNo.Maximum = BOXES * BOXSIZE - writeGetBoxIndex();
        }

        private void writeSlotTo_ValueChanged(object sender, EventArgs e)
        {
            writeCopiesNo.Maximum = BOXES * BOXSIZE - writeGetBoxIndex();
        }

        #endregion housekeeping for write from file

        private void writeBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectWriteDialog = new OpenFileDialog();
            selectWriteDialog.Title = "Select an EKX/PKX file";
            selectWriteDialog.Filter = "EKX/PKX files|*.ek6;*.ekx;*.pk6;*.pkx";
            string path = @Application.StartupPath + "\\Pokemon";
            selectWriteDialog.InitialDirectory = path;
            if (selectWriteDialog.ShowDialog() == DialogResult.OK)
            {
                selectedCloneValid = (readPokemonFromFile(selectWriteDialog.FileName, out selectedCloneData) == 0);
            }
        }

        private void writeDoIt_Click(object sender, EventArgs e)
        {
            if (!selectedCloneValid)
            {
                MessageBox.Show("No Pokemon selected!", "Error");
                return;
            }
            int ret = writePokemonToBox(selectedCloneData, writeGetBoxIndex(), writeGetCopies());
            if (ret > 0)
                MessageBox.Show(ret + " write(s) failed because the end of boxes was reached.", "Error");
            else if (ret < 0)
            if (writeAutoInc.Checked)
            {
                writeSetBoxIndex(writeGetBoxIndex() + writeGetCopies());
            }
        }

        void writeTab_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length <= 0)
                return;
            //TODO: maybe show a message if importing multiple files?
            int fails = 0;
            foreach (string filename in files)
            {
                byte[] data = new byte[POKEBYTES];
                if (readPokemonFromFile(filename, out data) == 0)
                {
                    int ret = writePokemonToBox(data, writeGetBoxIndex(), writeGetCopies());
                    if (ret > 0)
                        fails += ret;
                    else if (ret < 0)
                        return;
                }

                if (writeAutoInc.Checked)
                {
                    writeSetBoxIndex(writeGetBoxIndex() + writeGetCopies());
                }
            }
            if (fails > 0)
            {
                MessageBox.Show(fails + " write(s) failed because end of boxes was reached.", "Error");
            }
        }

        #region housekeeping for delete
        private uint deleteGetAmount()
        {
            return Decimal.ToUInt32(deleteAmount.Value);
        }

        private uint deleteGetIndex()
        {
            return Decimal.ToUInt32((deleteBox.Value - 1) * BOXSIZE + deleteSlot.Value - 1);
        }

        private void deleteBox_ValueChanged(object sender, EventArgs e)
        {
            deleteAmount.Maximum = BOXES * BOXSIZE - deleteGetIndex();
        }

        private void deleteSlot_ValueChanged(object sender, EventArgs e)
        {
            deleteAmount.Maximum = BOXES * BOXSIZE - deleteGetIndex();
        }
        #endregion housekeeping for delete

        private void delPkm_Click(object sender, EventArgs e)
        {
            uint offset = boxOff + deleteGetIndex() * POKEBYTES;
            uint size = POKEBYTES * deleteGetAmount();
            DataReadyWaiting myArgs = new DataReadyWaiting(new byte[size], handleDeleteData, null);
            uint mySeq = Program.scriptHelper.data(offset, size, pid);
            waitingForData.Add(mySeq, myArgs);
        }

        //TODO: don't save empty spaces to file, it's pointless
        private void handleDeleteData(object args_obj)
        {
            DataReadyWaiting args = (DataReadyWaiting)args_obj;

            if (deleteKeepBackup.Checked)
            {
                string folderPath = @Application.StartupPath + "\\" + FOLDERPOKE + "\\" + FOLDERDELETE + "\\";
                System.IO.FileInfo folder = new System.IO.FileInfo(folderPath);
                folder.Directory.Create();
                PKHeX validator = new PKHeX();
                for (int i = 0; i < args.data.Length; i += POKEBYTES)
                {
                    validator.Data = PKHeX.decryptArray(args.data.Skip(i).Take(POKEBYTES).ToArray());
                    if (validator.Species == 0)
                        continue;
                    string fileName = folderPath + "backup.pk6";
                    writePokemonToFile(validator.Data, fileName);
                }
            }

            writePokemonToBox(emptyData, deleteGetIndex(), deleteGetAmount());
        }

        private void showItems_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            dataGridView4.Visible = false;
            dataGridView5.Visible = false;
            showItems.ForeColor = System.Drawing.Color.Green;
            showMedicine.ForeColor = System.Drawing.Color.Black;
            showTMs.ForeColor = System.Drawing.Color.Black;
            showBerries.ForeColor = System.Drawing.Color.Black;
            showKeys.ForeColor = System.Drawing.Color.Black;
        }

        private void showMedicine_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            dataGridView4.Visible = true;
            dataGridView5.Visible = false;
            showItems.ForeColor = System.Drawing.Color.Black;
            showMedicine.ForeColor = System.Drawing.Color.Green;
            showTMs.ForeColor = System.Drawing.Color.Black;
            showBerries.ForeColor = System.Drawing.Color.Black;
            showKeys.ForeColor = System.Drawing.Color.Black;
        }

        private void showTMs_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = true;
            dataGridView4.Visible = false;
            dataGridView5.Visible = false;
            showItems.ForeColor = System.Drawing.Color.Black;
            showMedicine.ForeColor = System.Drawing.Color.Black;
            showTMs.ForeColor = System.Drawing.Color.Green;
            showBerries.ForeColor = System.Drawing.Color.Black;
            showKeys.ForeColor = System.Drawing.Color.Black;
        }

        private void showBerries_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            dataGridView4.Visible = false;
            dataGridView5.Visible = true;
            showItems.ForeColor = System.Drawing.Color.Black;
            showMedicine.ForeColor = System.Drawing.Color.Black;
            showTMs.ForeColor = System.Drawing.Color.Black;
            showBerries.ForeColor = System.Drawing.Color.Green;
            showKeys.ForeColor = System.Drawing.Color.Black;
        }

        private void showKeys_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            dataGridView3.Visible = false;
            dataGridView4.Visible = false;
            dataGridView5.Visible = false;
            showItems.ForeColor = System.Drawing.Color.Black;
            showMedicine.ForeColor = System.Drawing.Color.Black;
            showTMs.ForeColor = System.Drawing.Color.Black;
            showBerries.ForeColor = System.Drawing.Color.Black;
            showKeys.ForeColor = System.Drawing.Color.Green;
        }


        public void itemWrite_Click(object sender, EventArgs e)
        {
            byte[] dataToWrite = new byte[0] { };
            uint offsetToWrite = 0;
            
            if (dataGridView1.Visible == true)
            {
                itemData = new byte[1600];
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string datastring = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    int itemIndex = Array.IndexOf(itemList, datastring);
                    int itemcnt;
                    itemcnt = Convert.ToUInt16(dataGridView1.Rows[i].Cells[1].Value.ToString());

                    BitConverter.GetBytes((ushort)itemIndex).CopyTo(itemData, i * 4);
                    BitConverter.GetBytes((ushort)itemcnt).CopyTo(itemData, i * 4 + 2);
                }
                dataToWrite = itemData;
                offsetToWrite = itemsoff;
            }

            if (dataGridView2.Visible == true)
            {
                keyData = new byte[384];
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    string datastring = dataGridView2.Rows[i].Cells[0].Value.ToString();
                    int itemIndex = Array.IndexOf(itemList, datastring);
                    int itemcnt;
                    itemcnt = Convert.ToUInt16(dataGridView2.Rows[i].Cells[1].Value.ToString());

                    BitConverter.GetBytes((ushort)itemIndex).CopyTo(keyData, i * 4);
                    BitConverter.GetBytes((ushort)itemcnt).CopyTo(keyData, i * 4 + 2);
                }
                dataToWrite = keyData;
                offsetToWrite = keysoff;
            }

            if (dataGridView3.Visible == true)
            {
                tmData = new byte[432];
                for (int i = 0; i < dataGridView3.RowCount; i++)
                {
                    string datastring = dataGridView3.Rows[i].Cells[0].Value.ToString();
                    int itemIndex = Array.IndexOf(itemList, datastring);
                    int itemcnt;
                    itemcnt = Convert.ToUInt16(dataGridView3.Rows[i].Cells[1].Value.ToString());

                    BitConverter.GetBytes((ushort)itemIndex).CopyTo(tmData, i * 4);
                    BitConverter.GetBytes((ushort)1).CopyTo(tmData, i * 4 + 2);
                }
                dataToWrite = tmData;
                offsetToWrite = tmsoff;
            }

            if (dataGridView4.Visible == true)
            {
                medData = new byte[256];
                for (int i = 0; i < dataGridView4.RowCount; i++)
                {
                    string datastring = dataGridView4.Rows[i].Cells[0].Value.ToString();
                    int itemIndex = Array.IndexOf(itemList, datastring);
                    int itemcnt;
                    itemcnt = Convert.ToUInt16(dataGridView4.Rows[i].Cells[1].Value.ToString());

                    BitConverter.GetBytes((ushort)itemIndex).CopyTo(medData, i * 4);
                    BitConverter.GetBytes((ushort)itemcnt).CopyTo(medData, i * 4 + 2);
                }
                dataToWrite = medData;
                offsetToWrite = medsoff;
            }

            if (dataGridView5.Visible == true)
            {
                berryData = new byte[288];
                for (int i = 0; i < dataGridView5.RowCount; i++)
                {
                    string datastring = dataGridView5.Rows[i].Cells[0].Value.ToString();
                    int itemIndex = Array.IndexOf(itemList, datastring);
                    int itemcnt;
                    itemcnt = Convert.ToUInt16(dataGridView5.Rows[i].Cells[1].Value.ToString());

                    BitConverter.GetBytes((ushort)itemIndex).CopyTo(berryData, i * 4);
                    BitConverter.GetBytes((ushort)itemcnt).CopyTo(berryData, i * 4 + 2);
                }
                dataToWrite = berryData;
                offsetToWrite = bersoff;
            }

            Program.scriptHelper.write(offsetToWrite, dataToWrite, pid);
        }

        private void itemAdd_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Visible == true)
            {
                if (dataGridView1.RowCount >= 400)
                {
                    MessageBox.Show("You already have the max amount of items!", "Too many items");
                }
                else
                {
                    dataGridView1.Rows.Add("[None]", 0);
                }
            }

            if (dataGridView2.Visible == true)
            {
                if (dataGridView2.RowCount >= 96)
                {
                    MessageBox.Show("You already have the max amount of key items!", "Too many items");
                }
                else
                {
                    dataGridView2.Rows.Add("[None]", 0);
                }
            }

            if (dataGridView3.Visible == true)
            {
                if (dataGridView3.RowCount >= 96)
                {
                    MessageBox.Show("You already have the max amount of medicine items!", "Too many items");
                }
                else
                {
                    dataGridView3.Rows.Add("[None]", 0);
                }
            }

            if (dataGridView4.Visible == true)
            {
                if (dataGridView4.RowCount >= 108)
                {
                    MessageBox.Show("You already have the max amount of TMs & HMs!", "Too many items");
                }
                else
                {
                    dataGridView4.Rows.Add("[None]", 0);
                }
            }

            if (dataGridView5.Visible == true)
            {
                if (dataGridView5.RowCount >= 72)
                {
                    MessageBox.Show("You already have the max amount of berries!", "Too many items");
                }
                else
                {
                    dataGridView5.Rows.Add("[None]", 0);
                }
            }
        }


        
        private void pokeLang_Click(object sender, EventArgs e)
        {
            switch (Lang.SelectedIndex)
            { 
                case 0: lang = 0x01; break;
                case 1: lang = 0x02; break;
                case 2: lang = 0x03; break;
                case 3: lang = 0x04; break;
                case 4: lang = 0x05; break;
                case 5: lang = 0x07; break;
                case 6: lang = 0x08; break;
            }
            Program.scriptHelper.writebyte(langoff, lang, pid);
        }
        
        //TODO: are all these Array.Copy() really necessary? Shouldn't PKHeX just handle everything?
        private void pokeEkx_Click(object sender, EventArgs e)
        {
            if (dumpedPKHeX.Data == null)
            {
                MessageBox.Show("No Pokemon data found, please dump a Pokemon first to edit!", "No data to edit");
            }
            else if (evHPNum.Value + evATKNum.Value + evDEFNum.Value + evSPENum.Value + evSPANum.Value + evSPDNum.Value >= 511)
            {
                MessageBox.Show("Pokemon EV count is too high, the sum of all EVs should be 510 or less!", "EVs too high");
            }
            //This shouldn't be possible (length limited by text field), but better leave it
            else if (nickname.Text.Length > 12)
            {
                MessageBox.Show("Pokemon name length too long! Please use a name with a length of 12 or less.", "Name too long");
            }
            else if (otName.Text.Length > 12)
            {
                MessageBox.Show("OT name length too long! Please use a name with a length of 12 or less.", "Name too long");
            }
            else 
            {
                if (evHPNum.Value + evATKNum.Value + evDEFNum.Value + evSPENum.Value + evSPANum.Value + evSPDNum.Value <= 510)
                {
                    if (nickname.Text.Length <= 12 && otName.Text.Length <= 12)
                    {
                        dumpedPKHeX.Nickname = nickname.Text.PadRight(12, '\0');
                        dumpedPKHeX.OT_Name = otName.Text.PadRight(12, '\0');
                        byte[] pkmToEdit = dumpedPKHeX.Data;
                        Array.Copy(Encoding.Unicode.GetBytes(dumpedPKHeX.Nickname), 0, pkmToEdit, 64, 24);
                        Array.Copy(BitConverter.GetBytes(dumpedPKHeX.Nature), 0, pkmToEdit, 28, 1);
                        Array.Copy(BitConverter.GetBytes(dumpedPKHeX.HeldItem), 0, pkmToEdit, 10, 2);
                        dumpedPKHeX.IV_HP = (int)ivHPNum.Value;
                        dumpedPKHeX.IV_ATK = (int)ivATKNum.Value;
                        dumpedPKHeX.IV_DEF = (int)ivDEFNum.Value;
                        dumpedPKHeX.IV_SPE = (int)ivSPENum.Value;
                        dumpedPKHeX.IV_SPA = (int)ivSPANum.Value;
                        dumpedPKHeX.IV_SPD = (int)ivSPDNum.Value;

                        dumpedPKHeX.EV_HP = (int)evHPNum.Value;
                        dumpedPKHeX.EV_ATK = (int)evATKNum.Value;
                        dumpedPKHeX.EV_DEF = (int)evDEFNum.Value;
                        dumpedPKHeX.EV_SPE = (int)evSPENum.Value;
                        dumpedPKHeX.EV_SPA = (int)evSPANum.Value;
                        dumpedPKHeX.EV_SPD = (int)evSPDNum.Value;

                        dumpedPKHeX.EXP = (uint)ExpPoints.Value;

                        dumpedPKHeX.Ball = ball.SelectedIndex + 1;

                        dumpedPKHeX.SID = (int)dSIDNum.Value;
                        dumpedPKHeX.TID = (int)dTIDNum.Value;

                        dumpedPKHeX.PID = PKHeX.getHEXval(dPID.Text);

                        if (isEgg.Checked == true) { dumpedPKHeX.IsEgg = true; }
                        if (isEgg.Checked == false) { dumpedPKHeX.IsEgg = false; }
                        dumpedPKHeX.Species = species.SelectedIndex + 1;
                        dumpedPKHeX.Nature = nature.SelectedIndex;
                        dumpedPKHeX.Ability = ability.SelectedIndex + 1;
                        dumpedPKHeX.HeldItem = heldItem.SelectedIndex;
                        dumpedPKHeX.Move1 = move1.SelectedIndex;
                        dumpedPKHeX.Move2 = move2.SelectedIndex;
                        dumpedPKHeX.Move3 = move3.SelectedIndex;
                        dumpedPKHeX.Move4 = move4.SelectedIndex;

                        Array.Copy(BitConverter.GetBytes(dumpedPKHeX.IV32), 0, pkmToEdit, 116, 4);
                        byte[] pkmEdited = PKHeX.encryptArray(pkmToEdit);
                        byte[] chkSum = BitConverter.GetBytes(PKHeX.getCHK(pkmToEdit));
                        Array.Copy(chkSum, 0, pkmEdited, 6, 2);

                        if (radioBoxes.Checked == true)
                        {
                            uint ssd = (Decimal.ToUInt32(boxDump.Value) * 30 - 30) + Decimal.ToUInt32(slotDump.Value) - 1;
                            uint ssdOff = boxOff + (ssd * 232);
                            Program.scriptHelper.write(ssdOff, pkmEdited, pid);
                            getHiddenPower();
                        }

                        if (radioBattleBox.Checked == true)
                        {
                            uint bbOff = battleBoxOff + ((Decimal.ToUInt32(boxDump.Value) - 1) * 232);
                            Program.scriptHelper.write(bbOff, pkmEdited, pid);
                            getHiddenPower();
                        }

                        if (radioParty.Checked == true)
                        {
                            uint pOff = partyOff + ((Decimal.ToUInt32(boxDump.Value) - 1) * 484);
                            string pfOff = pOff.ToString("X");
                            string ekx = BitConverter.ToString(pkmEdited).Replace("-", ", 0x");
                            Program.scriptHelper.write(pOff, pkmEdited, pid);
                            getHiddenPower();
                        }

                        if (radioOpponent.Checked == true)
                        {
                            MessageBox.Show("You can only edit Pokemon in your Boxes (for now)!", "Editing Unavailable");
                        }

                        if (radioDaycare.Checked == true)
                        {
                            MessageBox.Show("You can only edit Pokemon in your Boxes (for now)!", "Editing Unavailable");
                        }

                        if (radioTrade.Checked == true)
                        {
                            MessageBox.Show("You can only edit Pokemon in your Boxes (for now)!", "Editing Unavailable");
                        }
                    }
                }
            }
        }

        private void radioParty_CheckedChanged(object sender, EventArgs e)
        {
            boxDump.Minimum = 1;
            boxDump.Maximum = 6;
            label8.Text = "Slot:";
            label9.Text = "Filename:";
            boxDump.Visible = true;
            slotDump.Visible = true;
            dumpBoxes.Visible = true;
            nameek6.Visible = true;
            slotDump.Visible = false;
            label7.Visible = false;
            label8.Visible = true;
            label9.Visible = true;
            label9.Location = new System.Drawing.Point(50, 20);
            nameek6.Location = new System.Drawing.Point(54, 39);
            nameek6.Size = new System.Drawing.Size(149, 20);
            dumpPokemon.Size = new System.Drawing.Size(86, 23);
            dumpBoxes.Size = new System.Drawing.Size(105, 23);
            dumpBoxes.Location = new System.Drawing.Point(98, 61);
            dumpPokemon.Location = new System.Drawing.Point(6, 61);
            dumpPokemon.Text = "Dump";
            dumpBoxes.Text = "Dump All Boxes";
        }

        private void versionCheck_Click(object sender, EventArgs e)
        {
            AskToUpdate();
        }

        private void radioParty_CheckedChanged_1(object sender, EventArgs e)
        {
            boxDump.Minimum = 1;
            boxDump.Maximum = 6;
            label8.Text = "Slot:";
            label9.Text = "Filename:";
            boxDump.Visible = true;
            slotDump.Visible = false;
            dumpBoxes.Visible = false;
            nameek6.Visible = true;
            label7.Visible = false;
            label8.Visible = true;
            label9.Visible = true;
            label9.Location = new System.Drawing.Point(50, 20);
            nameek6.Location = new System.Drawing.Point(53, 39);
            nameek6.Size = new System.Drawing.Size(150, 20);
            dumpPokemon.Size = new System.Drawing.Size(197, 23);
            dumpPokemon.Location = new System.Drawing.Point(6, 61);
            dumpPokemon.Text = "Dump";
        }


        private void dTIDNum_ValueChanged(object sender, EventArgs e)
        {
            ToolTipTSVt.SetToolTip(dTIDNum, "TSV: " + (((int)dTIDNum.Value ^ (int)dSIDNum.Value) >> 4).ToString());
            ToolTipTSVs.SetToolTip(dSIDNum, "TSV: " + (((int)dTIDNum.Value ^ (int)dSIDNum.Value) >> 4).ToString());
            ToolTipPSV.SetToolTip(dPID, "PSV: " + ((int)((dumpedPKHeX.PID >> 16 ^ dumpedPKHeX.PID & 0xFFFF) >> 4)).ToString());
        }

        //TODO: are you sure it's not supposed to be the same as above?
        private void dSIDNum_ValueChanged(object sender, EventArgs e)
        {
            ToolTipTSVt.SetToolTip(dTIDNum, "TSV: " + ((dumpedPKHeX.TID ^ dumpedPKHeX.SID) >> 4).ToString());
            ToolTipTSVs.SetToolTip(dSIDNum, "TSV: " + ((dumpedPKHeX.TID ^ dumpedPKHeX.SID) >> 4).ToString());
            ToolTipPSV.SetToolTip(dPID, "PSV: " + ((int)((dumpedPKHeX.PID >> 16 ^ dumpedPKHeX.PID & 0xFFFF) >> 4)).ToString());
        }

        private void dPID_TextChanged(object sender, EventArgs e)
        {
            ToolTipTSVt.SetToolTip(dTIDNum, "TSV: " + ((dumpedPKHeX.TID ^ dumpedPKHeX.SID) >> 4).ToString());
            ToolTipTSVs.SetToolTip(dSIDNum, "TSV: " + ((dumpedPKHeX.TID ^ dumpedPKHeX.SID) >> 4).ToString());
            ToolTipPSV.SetToolTip(dPID, "PSV: " + ((int)((dumpedPKHeX.PID >> 16 ^ dumpedPKHeX.PID & 0xFFFF) >> 4)).ToString());
        }

        private void setShiny_Click(object sender, EventArgs e)
        {
            dumpedPKHeX.setShinyPID();
            dPID.Text = dumpedPKHeX.PID.ToString("X");
            if (dumpedPKHeX.isShiny == true)
            {
                setShiny.Text = "★";
            }
            else
            {
                setShiny.Text = "☆";
            }
        }
        
        private void TIDNum_ValueChanged(object sender, EventArgs e)
        {
            ToolTipTSVtt.SetToolTip(TIDNum, "TSV: " + (((int)TIDNum.Value ^ (int)SIDNum.Value) >> 4).ToString());
            ToolTipTSVss.SetToolTip(SIDNum, "TSV: " + (((int)TIDNum.Value ^ (int)SIDNum.Value) >> 4).ToString());
        }

        private void SIDNum_ValueChanged(object sender, EventArgs e)
        {
            ToolTipTSVtt.SetToolTip(TIDNum, "TSV: " + (((int)TIDNum.Value ^ (int)SIDNum.Value) >> 4).ToString());
            ToolTipTSVss.SetToolTip(SIDNum, "TSV: " + (((int)TIDNum.Value ^ (int)SIDNum.Value) >> 4).ToString());
        }
       
        private void randomPID_Click(object sender, EventArgs e)
        {
            Random theRandom = new Random();
            byte[] theBytes = new byte[4];
            theRandom.NextBytes(theBytes);
            StringBuilder buffer = new StringBuilder(8);
            for (int i = 0; i < 4; i++)
            {
                buffer.Append(theBytes[i].ToString("X").PadLeft(2));
            }

            dPID.Text = buffer.ToString().Replace(" ", "0");

            dumpedPKHeX.PID = PKHeX.getHEXval(dPID.Text);

            if (dumpedPKHeX.isShiny == true)
            {
                setShiny.Enabled = false;
                setShiny.Text = "★";
            }
            if (dumpedPKHeX.isShiny == false)
            {
                setShiny.Enabled = true;
                setShiny.Text = "☆";
            }
        }

        private void onlyView_CheckedChanged(object sender, EventArgs e)
        {
            if (onlyView.Checked == true)
            {
                dumpBoxes.Enabled = false;
                nameek6.Enabled = false;
            }
            else
            if (onlyView.Checked == false)
            {
                dumpBoxes.Enabled = true;
                nameek6.Enabled = true;
            }
        }

        private void ball_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = ballImages[ball.SelectedIndex];
        }

        //TODO: add checking if a gender is available for Pokemon
        private void gender_Click(object sender, EventArgs e)
        {
            if (dumpedPKHeX.Gender == 0)
            {
                gender.Font = new Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size, FontStyle.Bold);
                gender.ForeColor = Color.Red;
                gender.Text = "♀";
                dumpedPKHeX.Gender = 1;
            }
            else if (dumpedPKHeX.Gender == 1)
            {
                gender.Font = new Font(SystemFonts.DefaultFont.FontFamily, SystemFonts.DefaultFont.Size, FontStyle.Bold);
                gender.ForeColor = Color.Blue;
                gender.Text = "♂";
                dumpedPKHeX.Gender = 0;
            }
            else
            if (dumpedPKHeX.Gender == 2)
            {
                //If a Pokemon is genderless, there's nothing you can do...
            }
        }

        private void radioBattleBox_CheckedChanged(object sender, EventArgs e)
        {
            boxDump.Minimum = 1;
            boxDump.Maximum = 6;
            label8.Text = "Slot:";
            label9.Text = "Filename:";
            boxDump.Visible = true;
            slotDump.Visible = false;
            dumpBoxes.Visible = false;
            nameek6.Visible = true;
            label7.Visible = false;
            label8.Visible = true;
            label9.Visible = true;
            label9.Location = new System.Drawing.Point(50, 20);
            nameek6.Location = new System.Drawing.Point(53, 39);
            nameek6.Size = new System.Drawing.Size(150, 20);
            dumpPokemon.Size = new System.Drawing.Size(197, 23);
            dumpPokemon.Location = new System.Drawing.Point(6, 61);
            dumpPokemon.Text = "Dump";
        }

        static void handleDataReady(object sender, DataReadyEventArgs e)
        {
            //We move data processing to a separate thread
            //This way even if processing takes a long time, the netcode doesn't hang
            DataReadyWaiting args;
            if (waitingForData.TryGetValue(e.seq, out args))
            {
                Array.Copy(e.data, args.data, Math.Min(e.data.Length, args.data.Length));
                Thread t = new Thread(new ParameterizedThreadStart(args.handler));
                t.Start(args);
                waitingForData.Remove(e.seq);
            }
        }

        #region fucking thread safety
        //Hooray for forced thread-safety!
        //If Visual C# didn't throw an exception every time you tried to access controls from a different thread
        //this wouldn't be necessary. We're not sending a rocket into space, dammit, we're just messing around with Pokemon.
        //I appreciate your care, Microsoft, but we really don't need this.
        //And if you're wondering - no, this is not because I decided to run threads in handleDataReady().
        //If handleDataReady() just called the function directly, it would still run in packetRecvThreadStart()'s thread
        //which is different from the GUI thread.
        delegate void SetTextDelegate(Control ctrl, string text);
        
        public static void SetText(Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                SetTextDelegate del = new SetTextDelegate(SetText);
                ctrl.Invoke(del, ctrl, text);
            }
            else
            {
                ctrl.Text = text;
            }
        }

        delegate void SetEnabledDelegate(Control ctrl, bool en);

        public static void SetEnabled(Control ctrl, bool en)
        {
            if (ctrl.InvokeRequired)
            {
                SetEnabledDelegate del = new SetEnabledDelegate(SetEnabled);
                ctrl.Invoke(del, ctrl, en);
            }
            else
            {
                ctrl.Enabled = en;
            }
        }

        delegate void SetCheckedDelegate(CheckBox ctrl, bool en);

        public static void SetChecked(CheckBox ctrl, bool en)
        {
            if (ctrl.InvokeRequired)
            {
                SetCheckedDelegate del = new SetCheckedDelegate(SetChecked);
                ctrl.Invoke(del, ctrl, en);
            }
            else
            {
                ctrl.Checked = en;
            }
        }

        delegate void SetValueDelegate(NumericUpDown ctrl, decimal val);

        public static void SetValue(NumericUpDown ctrl, decimal val)
        {
            if (ctrl.InvokeRequired)
            {
                SetValueDelegate del = new SetValueDelegate(SetValue);
                ctrl.Invoke(del, ctrl, val);
            }
            else
            {
                ctrl.Value =  val;
            }
        }

        delegate void SetSelectedIndexDelegate(ComboBox ctrl, int i);

        public static void SetSelectedIndex(ComboBox ctrl, int i)
        {
            if (ctrl.InvokeRequired)
            {
                SetSelectedIndexDelegate del = new SetSelectedIndexDelegate(SetSelectedIndex);
                ctrl.Invoke(del, ctrl, i);
            }
            else
            {
                ctrl.SelectedIndex = i;
            }
        }

        delegate void SetColorDelegate(Control ctrl, Color c, bool back);

        public static void SetColor(Control ctrl, Color c, bool back)
        {
            if (ctrl.InvokeRequired)
            {
                SetColorDelegate del = new SetColorDelegate(SetColor);
                ctrl.Invoke(del, ctrl, c, back);
            }
            else
            {
                if (back)
                    ctrl.BackColor = c;
                else
                    ctrl.ForeColor = c;
            }
        }
        #endregion fucking thread safety

        // Test for remote control

        // A button
        private void manualA_Click(object sender, EventArgs e)
        {
            byte[] buttonByte = BitConverter.GetBytes(1);
            for (int i = 0; i < 300; i++)
            {
                Program.scriptHelper.write(buttonAOff, buttonByte, hid_pid);
            }
        }
    }


    //Objects of this class contains an array for data that have been acquired, a delegate function 
    //to handle them and any additional arguments it might require.
    class DataReadyWaiting
    {
        public byte[] data;
        public object arguments;
        public delegate void DataHandler(object data_arguments);
        public DataHandler handler;

        public DataReadyWaiting(byte[] data_, DataHandler handler_, object arguments_)
        {
            this.data = data_;
            this.handler = handler_;
            this.arguments = arguments_;
        }
    }
}
